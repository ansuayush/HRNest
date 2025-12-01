$(document).ready(function () {
  
    BindScreen();
   
    LoadMasterDropdown('ddlEMP', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.Employee,
        manualTableId: 0
    }, 'Select', false);
    LoadMasterDropdown('ddlDEP', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.Department,
        manualTableId: 0
    }, 'Select', false);
  


});

function BindScreen() {

    debugger;
    var id = $("#hdfid").val();
    var model = {
        Id: id

    }
    const jsonString = JSON.stringify(model);
    var ScreenID = "HRAction";
    CommonAjaxMethod(virtualPath + 'Generic/GetRecords', { modelData: jsonString, screenId: ScreenID }, 'GET', function (response) {
        debugger;
        var data = response.data.data.Table;
        if (data[0].ResolutionStatus == "1") {
            $("#btnnotify").hide();
            $("#btnstart").hide();
            $("#btnR").hide();
        }
        else {
            //$("#btnnotify").show();
            $("#btnstart").show();
            $("#btnR").show();
        }
        if (data[0].statusflag == "7") {
            $("#btnnotify").show();
        }
        else {
            $("#btnnotify").hide();
        }
        $("#labelreq").text(data[0].ReqNo);
        $("#labelreqdate").text(data[0].ReqDate);
        $("#labelreqby").text(data[0].Reqby);
        $("#labelloc").text(data[0].Location);
        $("#labeldept").text(data[0].Department);
        $("#labeldesg").text(data[0].Designation);
        $("#pReason").text(data[0].ReasonforResignation);
        $("#spanComment").text(data[0].comment.substring(0, 10));
        $("#spanFullComment").text(data[0].comment);
        $("#NPAP").text(data[0].noticeperiod);
        $("#EPNP").text(data[0].relievingday);
        $("#ERDate").text(data[0].relievingdate);
        $("#RDateP").text(data[0].RelievingdatePolicy);
        $('#hdfLevelId').val(data[0].PendingLevel);
        $("#spanReasonNP").text(data[0].reasonNP.substring(0, 50));
        $("#spanFullReasonNP").text(data[0].reasonNP);
        $("#hodid").val(data[0].HOD);
        if (data[0].noticeperiodserve == "1") {
            $("#divNF").hide();
            $("#divNFD").hide();
            $("#divlevelComment").hide();
            
        }
        else {
            $("#divNF").show();
            $("#divNFD").show();
            $("#divRDAP").hide();
            $("#divlevelComment").show();
        }
        if (data[0].Status == "P") {
       
            var table = $('#TabellevelComment').DataTable({
                "processing": true,
                "destroy": true,
                "info": false,
                "lengthChange": false,
                "bFilter": false,
                "data": response.data.data.Table2,
                "columns": [

                    { "data": "DocType" },
                    { "data": "emp_name" },
                    { "data": "SuggestedRelievingDate" },
                    { "data": "Reason" },
                    { "data": "Level" }



                ]
                ,

            });
            var table = $('#tblSuggestions').DataTable({
                "processing": true,
                "destroy": true,
                "info": false,
                "lengthChange": false,
                "bFilter": false,
                "data": response.data.data.Table4,
                "columns": [

                   /* { "data": "RowNum" },*/
                    { "data": "DocType" },
                    { "data": "emp_name" },
                    { "data": "Status" },
                    { "data": "reasonnfhire" }




                ]
                ,

            });
        }
        else {
            $("divlevelComment").hide();
        }
       
 var table = $('#tblHandovertask').DataTable({
            "processing": true,
            "destroy": true,
            "info": false,
            "lengthChange": false,
            "bFilter": false,
            "data": response.data.data.Table3,
            "columns": [

                { "data": "RowNum" },
                { "data": "taskname" },
                { "data": "emp_name" },
                { "data": "Priority" }
              



            ]
            ,

 });


   
        //$("#divlevelComment").show();
      
        var NotifyData = response.data.data.Table5;
        if (NotifyData.length > 0) {
            $("#btnadd").hide();
            $("#btnSN").hide();
            
        }
        $('#tblnotify').html('');
        var newtbblData1 = '<table id="tblPreRegistrationUploadingDocuments" class="table table-striped m-0 " >' +
            ' <thead>' +
            ' <tr>' +
            ' <th width="33">S.No</th>' +
            ' <th width="150">Department</th>' +
            ' <th width="150">Employee Name</th>' +
            ' </tr>' +
            ' </thead>';
        var html1 = "</table>";
        var tableData = "";
        for (let i = 0; i < NotifyData.length; i++) {
            var newtbblData = "<tr><td>" + NotifyData[i].RowNum + "</td><td>" + NotifyData[i].DepartmentName + "</td><td>" + NotifyData[i].emp_name + "</td></tr>";
            var allstring = newtbblData;
            tableData += allstring;
        }
        $('#tblnotify').html(newtbblData1 + tableData + html1);
        if (data[0].statusflag == "8") {
              $("#btnstart").hide();
              $("#btnR").hide();
        }

    });

}






function SaveRecord() {
    debugger;
    $("#ddlP").removeClass("Mandatory");
    $("#ddlEMP").removeClass("Mandatory");
    $("#ddlDEP").removeClass("Mandatory");
    $("#txtreasonnfhire").removeClass("Mandatory");
    $("#txtReason").addClass("Mandatory");
    if (checkValidationOnSubmit('Mandatory')) {
        ShowLoadingDialog();
        var model = {
            ReqID: $("#hdfid").val(),
            Reason: $('#txtReason').val(),
            ActualFileName: $('#hdnUploadActualFileName').val(),
            NewFileName: $('#hdnUploadNewFileName').val(),
            FileUrl: $('#hdnUploadFileUrl').val(),

        }
        const jsonString = JSON.stringify(model);

        let GenericModeldata =
        {
            ScreenID: "HRDocument",
            Operation: "ADD",
            ModelData: jsonString,

        };


        CommonAjaxMethod(virtualPath + 'Generic/PerformOperation', GenericModeldata, 'POST', function (response) {
            BindScreen();
            $('#rfc').modal('hide')
            CloseLoadingDialog();
        });
    }
    else {
        CloseLoadingDialog();
    }
}

function UploadDocument() {

    var fileUpload = $("#txtAttachment").get(0);
    var files = fileUpload.files;
    if (files.length > 0) {
        var fileData = new FormData();
        fileData.append(files[0].name, files[0]);
        $.ajax({
            url: virtualPath + 'CommonMethod/UploadExitDocument',
            type: "POST",
            contentType: false,
            processData: false,
            data: fileData,
            success: function (response) {
                var result = JSON.parse(response);
                if (result.ErrorMessage === "") {
                    $('#hdnUploadActualFileName').val(result.FileModel.ActualFileName);
                    $('#hdnUploadNewFileName').val(result.FileModel.NewFileName);
                    $('#hdnUploadFileUrl').val(result.FileModel.FileUrl);
                    $('#txtAttachmentfile').val(result.FileModel.ActualFileName);

                }
            },

        });

    }
}
var DocArray = [];
var DocArrayId = 0;
function Addrow() {


   
    var valid = true;
   
    if (valid == true) {
        var DeptId = parseInt($("#ddlDEP").val());
        var EMPId = parseInt($("#ddlEMP").val());
        DocArrayId = DocArrayId + 1;
        var loop = DocArrayId;
        var Deptname = $("#ddlDEP option:selected").text();
        var EMPName = $("#ddlEMP option:selected").text();
        if (DeptId > 0 && EMPId > 0) {
            var objarrayinner =
            {
                ID: loop,
                Deptname: Deptname,
                EMPId: EMPId,
                EMPName: EMPName,
                DeptId: DeptId,
                Action: 'Remove',
                DocumentId: loop,

            }

            DocArray.push(objarrayinner);
            var dataArry = DocArray;
                $('#tblnotify').html('');
           
           
            var newtbblData1 = '<table id="tblPreRegistrationUploadingDocuments" class="table table-striped m-0 " >' +
                ' <thead>' +
                ' <tr>' +
                ' <th width="33">S.No</th>' +
                ' <th width="150">Department</th>' +
                ' <th width="150">Employee Name</th>' +
                ' <th width="50" class=" text-center">Action</th>' +
                ' </tr>' +
                ' </thead>';
            var html1 = "</table>";
            var tableData = "";
            for (let i = 0; i < dataArry.length; i++) {
                var newtbblData = "<tr><td>" + parseInt(i + 1) + "</td><td>" + dataArry[i].Deptname + "</td><td>" + dataArry[i].EMPName + "</td><td class='text-center' ><a class='HideClass'  title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='DeleteRow(this," + dataArry[i].DocumentId + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a> </td></tr>";
                var allstring = newtbblData;
                tableData += allstring;
            }
            $('#tblnotify').html(newtbblData1 + tableData + html1);
            $("#ddlEMP").val('0').trigger('change');
            $("#ddlDEP").val('0').trigger('change');
        }
      

    }


}


function DeleteRow(obj, num) {

    var count = 0;
    var TotalRowCount = $('#tblHandOvertask').find("tbody tr").length;
    var NUM = num;
    var NARR = DocArray;
    ConfirmMsgBox("Are you sure you want to delete", '', function () {

        $(obj).closest('tr').remove();

        for (let i = 0; i < NARR.length; i++) {
            if (NARR[i].DocumentId == NUM) {
                NARR.splice(i, 1);
                break;
            }
        }
        DocArray = NARR;
        $("#tblHandOvertask TBODY TR").each(function (i) {
            $(this).closest("tr").find("label").each(function () {
                $(this).attr({
                    'id': function (_, id) { var arr = id.split('_'); return id.replace(arr[1], i); },
                });
                $(this).html(i + 1)
            });

            $(this).closest("tr").find("div").each(function () {
                $(this).attr({
                    'id': function (_, id) { var arr = id.split('_'); return id.replace(arr[1], i); },
                });
            });


            $(this).closest("tr").find("input").each(function () {
                $(this).attr({
                    'id': function (_, id) { return id.replace('_' + (parseInt(i) + 1) + '_', '_' + i + '_'); },
                    'name': function (_, name) { return name.replace('[' + (parseInt(i) + 1) + ']', '[' + i + ']'); },
                });

            });

            $(this).closest("tr").find("select").each(function () {
                $(this).attr({
                    'id': function (_, id) { return id.replace('_' + (parseInt(i) + 1) + '_', '_' + i + '_'); },
                    'name': function (_, name) { return name.replace('[' + (parseInt(i) + 1) + ']', '[' + i + ']'); },
                });

            });

            $(this).closest("tr").find("textarea").each(function () {
                $(this).attr({
                    'id': function (_, id) { return id.replace('_' + (parseInt(i) + 1) + '_', '_' + i + '_'); },
                    'name': function (_, name) { return name.replace('[' + (parseInt(i) + 1) + ']', '[' + i + ']'); },
                });

            });


            $(this).closest("tr").find("span").each(function () {
                if ($(this).attr("data-valmsg-for")) {
                    $(this).attr({
                        'data-valmsg-for': function (_, name) { return name.replace('[' + (parseInt(i) + 1) + ']', '[' + i + ']'); },
                    });
                }
                if ($(this).attr("for")) {
                    $(this).attr({
                        'for': function (_, id) { return id.replace('_' + (parseInt(i) + 1) + '_', '_' + i + '_'); },
                    });
                }
            });
        });
        var form = $("#LevelApproveActionFrom").closest("form");
        form.removeData('validator');
        form.removeData('unobtrusiveValidation');
        $.validator.unobtrusive.parse(form);

    })
}




function DownloadFileRFP() {
    var fileURl = $('#hdnUploadFileUrl').val();
    var fileName = $('#hdnUploadActualFileName').val();
    if (fileURl != null || fileURl != undefined) {
        var stSplitFileName = fileName.split(".");
        var link = document.createElement("a");
        link.download = stSplitFileName[0];
        link.href = fileURl;
        link.click();
    }
}

function SendNotification() {
  
    if (DocArray.length>0) {
        ShowLoadingDialog();
        var model = {
            ReqID: $("#hdfid").val(),
            Notify: DocArray
           

        }
        const jsonString = JSON.stringify(model);

        let GenericModeldata =
        {
            ScreenID: "HRNotify",
            Operation: "ADD",
            ModelData: jsonString,

        };


        CommonAjaxMethod(virtualPath + 'Generic/PerformOperation', GenericModeldata, 'POST', function (response) {
            BindScreen();
            $('#nty').modal('hide')
            CloseLoadingDialog();
        });
    }
    else {
        CloseLoadingDialog();
    }
}

function Startprocess() {
  
    var reqid = parseInt($("#hdfid").val());
    if (reqid > 0) {
        ShowLoadingDialog();
        var model = {
            ReqID: reqid
        }
        const jsonString = JSON.stringify(model);

        let GenericModeldata =
        {
            ScreenID: "HRStartExit",
            Operation: "ADD",
            ModelData: jsonString,

        };


        CommonAjaxMethod(virtualPath + 'Generic/PerformOperation', GenericModeldata, 'POST', function (response) {
            BindScreen();
            CloseLoadingDialog();
        });
    }
    else {
        CloseLoadingDialog();
    }
}

