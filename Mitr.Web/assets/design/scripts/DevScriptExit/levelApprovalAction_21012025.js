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


});

function BindScreen() {
    

    var id = $("#hdfid").val();
    var model = {
        Id: id

    }
    const jsonString = JSON.stringify(model);
    var ScreenID = "HRAction";
    CommonAjaxMethod(virtualPath + 'Generic/GetRecords', { modelData: jsonString, screenId: ScreenID }, 'GET', function (response) {
   
        var data = response.data.data.Table;
        var dataR = response.data.data.Table1;
        //  var T = response.data.data.Table2;
        $("#labelreq").text(data[0].ReqNo);
        $("#labelreqdate").text(data[0].ReqDate);
        $("#labelreqby").text(data[0].Reqby);
        $("#labelloc").text(data[0].Location);
        $("#labeldept").text(data[0].Department);
        $("#labeldesg").text(data[0].Designation);
        $("#pReason").text(data[0].ReasonforResignation);
        $("#spanComment").text(data[0].comment.substring(0, 10));
        $("#spanFullComment").text(data[0].comment);

        //$("#NPAP").text(data[0].noticeperiod);

        //$("#EPNP").text(data[0].relievingday);

        //$("#ERDate").text(data[0].relievingdate);

        //$("#RDateP").text(data[0].RelievingdatePolicy);

        if (data[0].noticeperiodserve == "1") {

            $("#NPAP").text(data[0].relievingday);

            $("#RDateP").text(data[0].RelievingdatePolicy);

            $("#divEPRD").hide();

            $("#divEPNP").hide();

        }

        else {

            $("#NPAP").text(data[0].noticeperiod);

            $("#EPNP").text(data[0].relievingday);

            $("#ERDate").text(data[0].relievingdate);

            $("#RDateP").text(data[0].RelievingdatePolicy);

        }

        $('#hdfLevelId').val(data[0].PendingLevel);
        $("#spanReasonNP").text(data[0].reasonNP.substring(0, 50));
        $("#spanFullReasonNP").text(data[0].reasonNP);

        $("#hodid").val(data[0].HOD);

        var LevelForm = $("#hdfLevelForm").val().replace(/ /g, '');

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


        //if (data[0].statusflag == 0) {
        if (data[0].ResolutionStatus == 1) {
            $("#btnR").hide();
            $("#btnF").hide();
            $("#divR").show();
            $("#spanRReason").text(dataR[0].Reason.substring(0, 50));
            $("#spanFullRReason").text(dataR[0].Reason);
            $("#spanRReason").text(dataR[0].Reason.substring(0, 50));
            $("#spanFullRReason").text(dataR[0].Reason);
            $('#hdnUploadActualFileName').val(dataR[0].ActualFileName);
            $('#hdnUploadNewFileName').val(dataR[0].NewFileName);
            $('#hdnUploadFileUrl').val(dataR[0].FileUrl);
            $("#Pfile").text(dataR[0].ActualFileName.split('.')[0]);
            $("#divDecline").hide();
            $("#divCommentRes").hide();
            $("#divnComReason").show();

        }
        else if (data[0].statusflag == 2 && LevelForm == "1") {
            $("#btnR").show();
            $("#btnF").show();
            $("#divDecline").show();
            $("#divlevelComment").hide();
            $("#divnComReason").show();

        }
        else if (data[0].statusflag == 3 && LevelForm == "1") {
            $("#btnR").hide();
            $("#btnF").hide();
            $("#divDecline").hide();
            $("#divlevelComment").show();
            $("#divCommentRes").hide();
            $("#box").hide();


        }
        else if (data[0].statusflag == 3 && LevelForm == "2") {
            $("#btnR").show();
            $("#btnF").show();
            $("#divlevelComment").show();

        }
        else if (data[0].statusflag == 4 && LevelForm == "2") {
            $("#btnR").hide();
            $("#btnF").hide();
            $("#divlevelComment").show();
            $("#divCommentRes").hide();
            $("#divDecline").hide();
            $("#box").hide();


        }
        else if (data[0].statusflag == 4 && LevelForm == "3") {
            $("#btnR").show();
            $("#btnF").show();
            $("#divlevelComment").show();
        }
        else if (data[0].statusflag == 5 && LevelForm == "3") {
            $("#btnR").hide();
            $("#btnF").hide();
            $("#divlevelComment").show();
            $("#divCommentRes").hide();
            $("#divDecline").hide();
            $("#box").hide();
        }
       
        else if (data[0].statusflag == 6 && LevelForm == "4") {

            $("#btnR").hide();
            $("#btnF").hide();
            $("#divlevelComment").show();
            $("#divCommentRes").hide();
            $("#divDecline").hide();
            ("#box").hide();

        }
       else if (data[0].statusflag == 5 && LevelForm == "4") {

            $("#btnR").show();
            $("#btnF").show();
            $("#divlevelComment").show();

        }
        else if (data[0].statusflag == 6 && LevelForm == "5") {

        
            $("#divlevelComment").show();
           $("#btnR").show();
            $("#btnF").show();

        }
        else if (data[0].statusflag == 7 && LevelForm == "5") {

            $("#btnR").hide();
            $("#btnF").hide();
            $("#divlevelComment").show();
            $("#divCommentRes").hide();
            $("#divDecline").hide();
            ("#box").hide();

        }

        else {
            $("#btnR").hide();
            $("#btnF").hide();
            $("#divlevelComment").show();
            $("#divCommentRes").hide();
            $("#divDecline").hide();

        }


    });

}






function SaveRecord() {
    $("#ddlP").removeClass("Mandatory");
    $("#ddlEMP").removeClass("Mandatory");
    $("#txtreasonnfhire").removeClass("Mandatory");
    $("#txtReason").addClass("Mandatory");
    $("#txtAttachmentfile").addClass("Mandatory");
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
function AddHandoverTask() {


    $("#txtreasonnfhire").removeClass("Mandatory");
    $("#txtReason").removeClass("Mandatory");
    $("#ddlEMP").addClass("Mandatory");
    $("#ddlP").addClass("Mandatory");
    $("#txtAttachmentfile").removeClass("Mandatory");
    var valid = true;
    if (checkValidationOnSubmit('Mandatory') == false) {
        valid = false;
    }
    if (valid == true) {
        var Taskname = $('#txttask').val();
        var EMPId = parseInt($("#ddlEMP").val());
        DocArrayId = DocArrayId + 1;
        var loop = DocArrayId;
        var Priority = $("#ddlP option:selected").text();
        var EMPName = $("#ddlEMP option:selected").text();

        var objarrayinner =
        {
            ID: loop,
            Taskname: Taskname,
            EMPId: EMPId,
            EMPName: EMPName,
            Priority: Priority,
            Action: 'Remove',
            DocumentId: loop,

        }

        DocArray.push(objarrayinner);
        var dataArry = DocArray;
        $('#tblHandOvertask').html('');
        var newtbblData1 = '<table id="tblPreRegistrationUploadingDocuments" class="table table-striped m-0 " >' +
            ' <thead>' +
            ' <tr>' +
            ' <th width="33">S.No</th>' +
            ' <th width="150">Task</th>' +
            ' <th width="150">Employee Name</th>' +
            ' <th>Priority</th>' +
            ' <th width="50" class=" text-center">Action</th>' +
            ' </tr>' +
            ' </thead>';
          var html1 = "</table>";
          var tableData = "";
        for (let i = 0; i < dataArry.length; i++) {
            var newtbblData = "<tr><td>" + parseInt(i + 1) + "</td><td>" + dataArry[i].Taskname + "</td><td>" + dataArry[i].EMPName + "</td><td>" + dataArry[i].Priority + "</td><td class='text-center' ><a class='HideClass'  title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='DeleteRow(this," + dataArry[i].DocumentId + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a> </td></tr>";
            var allstring = newtbblData;
            tableData += allstring;
        }
        $('#tblHandOvertask').html(newtbblData1 + tableData + html1);
        $('#txttask').val('');
        $("#ddlEMP").val('0').trigger('change');
        $("#ddlP").val('0').trigger('change');

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



function ForwardRecord() {

  
    ShowLoadingDialog();
    var Releavingdate = "";
    var ReasonReleavingdate = "";
    var reasonnfhire = "";
    $("#txtAttachmentfile").removeClass("Mandatory");
    $("#txtReason").removeClass("Mandatory");
    $("#ddlEMP").removeClass("Mandatory");
    $("#ddlP").removeClass("Mandatory");
    if (parseInt($("#hdfDNP").val()) == 1) {
        if ($('#txtRdate').val() == "") {
            $('#txtRdate').attr({ 'class': 'form-control Mandatory ' })

        }
        else {
            $('#txtRdate').attr({ 'class': 'form-control  ' })
        }
        if ($('#txtRNRD').val() == "") {
            $('#txtRNRD').attr({ 'class': 'form-control Mandatory ' })

        }
        else {
            $('#txtRNRD').attr({ 'class': 'form-control  ' })
        }
    }
    if (parseInt($("#hdfDNP").val()) == 0) {
        $('#txtRdate').attr({ 'class': 'form-control  ' })
        $('#txtRNRD').attr({ 'class': 'form-control  ' })
        //if ($('#txtreasonnfhire').val() == "") {
        //    $('#txtreasonnfhire').attr({ 'class': 'form-control Mandatory ' })

        //}
        //else {
        //    $('#txtreasonnfhire').attr({ 'class': 'form-control  ' })
        //}

    }
    if (parseInt($("#hdfDNP").val()) == 1) {
        Releavingdate = $('#txtRdate').val();
        ReasonReleavingdate = $('#txtRNRD').val();
        // $('#txtreasonnfhire').attr({ 'class': 'form-control  ' })
    }
    else {
        ReasonReleavingdate = "";
        Releavingdate = "";
        //  reasonnfhire = $('#txtreasonnfhire').val();

    }
    if (parseInt($("#hdfPFHire").val()) == 0) {
        if ($('#txtreasonnfhire').val() == "") {
            $('#txtreasonnfhire').attr({ 'class': 'form-control Mandatory ' })

        }
        else {
            $('#txtreasonnfhire').attr({ 'class': 'form-control  ' })
        }

    }
    else {
        $('#txtreasonnfhire').attr({ 'class': 'form-control  ' })
    }
    if (parseInt($("#hdfPFHire").val()) == 0) {
        reasonnfhire = $('#txtreasonnfhire').val();
    }
    else {
        reasonnfhire = "";
    }
    if (checkValidationOnSubmit('Mandatory')) {
       
        var model = {
            ReqID: $("#hdfid").val(),
            Type: $('#hdfLevelId').val(),
            DeclineNoticePeriod: $('#hdfDNP').val(),
            Releavingdate: Releavingdate,
            ReasonReleavingdate: ReasonReleavingdate,
            Reasonnfhire: reasonnfhire,
            CommentR: $('#txtCommentR').val(),
            PFHire: $('#hdfPFHire').val(),
            HandoverTask: DocArray

        }
        const jsonString = JSON.stringify(model);

        let GenericModeldata =
        {
            ScreenID: "levelAction",
            Operation: "ADD",
            ModelData: jsonString,

        };


        CommonAjaxMethod(virtualPath + 'Generic/PerformOperation', GenericModeldata, 'POST', function (response) {
            BindScreen();
            $('#hod2').modal('hide')
           // CloseLoadingDialog();
            setTimeout(function () {
                CloseLoadingDialog();
            }, 4000);

        });
    }
    else {
        CloseLoadingDialog();
    }
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