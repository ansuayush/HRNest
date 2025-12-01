$(document).ready(function ()
{
    BindScreen();
  LoadMasterDropdown('ddllavel', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.Employee,
        manualTableId: 0
    }, 'Select', false);

  let HOD=$("#hodid").val();
  $('#ddllavel').val(HOD).trigger('change'); 
     LoadMasterDropdown('ddllaveltwo', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.Employee,
        manualTableId: 0
    }, 'Select', false);
 let L2=$("#L2").val();
  $('#ddllaveltwo').val(L2).trigger('change'); 
  LoadMasterDropdown('ddllavelthree', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.Employee,
        manualTableId: 0
    }, 'Select', false);
 let L3=$("#L3").val();
  $('#ddllavelthree').val(L3).trigger('change'); 
  LoadMasterDropdown('ddllavelfour', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.Employee,
        manualTableId: 0
    }, 'Select', false);
 let L4=$("#L4").val();
  $('#ddllavelfour').val(L4).trigger('change'); 
  LoadMasterDropdown('ddllavelfive', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.Employee,
        manualTableId: 0
    }, 'Select', false);
    let L5=$("#L5").val();
  $('#ddllavelfive').val(L5).trigger('change'); 
});

function BindScreen()
{

    var id=$("#hdfid").val();
     var model = {
        Id: id
     
      }
     const jsonString = JSON.stringify(model);
      var ScreenID = "HRAction";
    CommonAjaxMethod(virtualPath + 'Generic/GetRecords', { modelData: jsonString, screenId: ScreenID }, 'GET', function (response) {
        debugger;
       var data = response.data.data.Table;
       var dataR = response.data.data.Table1;
       $("#labelreq").text(data[0].ReqNo);
       $("#labelreqdate").text(data[0].ReqDate);
       $("#labelreqby").text(data[0].Reqby);
       $("#labelloc").text(data[0].Location);  
       $("#labeldept").text(data[0].Department);
       $("#labeldesg").text(data[0].Designation);
       $("#pReason").text(data[0].ReasonforResignation);
        $("#spanComment").text(data[0].comment.substring(0, 10));
        $("#divR").hide();
       $("#spanFullComment").text(data[0].comment);

         if (data[0].noticeperiodserve == "1") {
             $("#NPAP").text(data[0].relievingday);
             $("#RDateP").text(data[0].relievingdate);
             $("#divEPRD").hide();
             $("#divEPNP").hide();
             $("#divRNANP").hide();
             
         }
         else {
             $("#NPAP").text(data[0].noticeperiod);
             $("#EPNP").text(data[0].relievingday);
             $("#ERDate").text(data[0].relievingdate);
             $("#RDateP").text(data[0].RelievingdatePolicy);
             $("#divERDAP").hide();
             $("#divRNANP").show();
         }
     

       $("#spanReasonNP").text(data[0].reasonNP.substring(0, 50));
       $("#spanFullReasonNP").text(data[0].reasonNP);

       $("#hodid").val(data[0].HOD);
    
       $("#L2").val(data[0].L2);
       $("#L3").val(data[0].L3);
       $("#L4").val(data[0].L4);
       $("#L5").val(data[0].L5);
       if(data[0].statusflag==0){
        $("#btnR").hide();
        $("#btnF").hide();
        $("#btnA").hide();
        $("#divR").show();
        $("#spanRReason").text(dataR[0].Reason.substring(0, 50));
        $("#spanFullRReason").text(dataR[0].Reason);
       $('#hdnUploadActualFileName').val(dataR[0].ActualFileName);
       $('#hdnUploadNewFileName').val(dataR[0].NewFileName);
       $('#hdnUploadFileUrl').val(dataR[0].FileUrl);

       }
       else if (data[0].statusflag == 2 || data[0].statusflag == 3 || data[0].statusflag == 4 || data[0].statusflag == 5 || data[0].statusflag == 6 ){
       $("#btnR").hide();
        $("#btnF").hide();
         $("#btnA").hide();
           $("#divR").hide();
           if (data[0].ResolutionStatus == "1") {
          
               $("#spanRReason").text(dataR[0].Reason.substring(0, 50));
               $("#spanFullRReason").text(dataR[0].Reason);
               $('#hdnUploadActualFileName').val(dataR[0].ActualFileName);
               $('#hdnUploadNewFileName').val(dataR[0].NewFileName);
               $('#hdnUploadFileUrl').val(dataR[0].FileUrl);
               $("#divR").show();
               $("#Pfile").text(dataR[0].ActualFileName.split('.')[0]);


           }
           else {
               $("#divR").hide();
           }
       }
       else if ( data[0].statusflag == 7) {
           $("#btnR").hide();
           $("#btnF").hide();
           $("#btnA").hide();
           $("#divR").hide();
           $("#divlevelComment").show();

           if (data[0].ResolutionStatus == "1") {
               debugger;
               $("#spanRReason").text(dataR[0].Reason.substring(0, 50));
               $("#spanFullRReason").text(dataR[0].Reason);
               $('#hdnUploadActualFileName').val(dataR[0].ActualFileName != null ? dataR[0].ActualFileName : '');
               $('#hdnUploadNewFileName').val(dataR[0].NewFileName != null ? dataR[0].NewFileName : '');
               $('#hdnUploadFileUrl').val(dataR[0].FileUrl != null ? dataR[0].FileUrl : '');
              // $('#hdnUploadActualFileName').val(dataR[0].ActualFileName);
              // $('#hdnUploadNewFileName').val(dataR[0].NewFileName);
              // $('#hdnUploadFileUrl').val(dataR[0].FileUrl);

               $("#divR").show();
               $("#Pfile").text(dataR[0].ActualFileName.split('.')[0]);
           }
           else {
               $("#divR").hide();
           }

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
           
       }
       else if (data[0].statusflag == 1 && data[0].ResolutionStatus == "1") {
           $("#btnR").hide();
           $("#btnF").hide();
           $("#btnA").hide();
           $("#divR").show();
           $("#spanRReason").text(dataR[0].Reason.substring(0, 50));
           $("#spanFullRReason").text(dataR[0].Reason);
           $('#hdnUploadActualFileName').val(dataR[0].ActualFileName);
           $('#hdnUploadNewFileName').val(dataR[0].NewFileName);
           $('#hdnUploadFileUrl').val(dataR[0].FileUrl);
           $("#Pfile").text(dataR[0].ActualFileName.split('.')[0]);
       }
       else if (data[0].statusflag == 8) {
           $("#btnR").hide();
           $("#btnF").hide();
           $("#btnA").hide();
       }

      else{
        $("#btnR").show();
        $("#btnF").show();
        $("#btnA").show();
       }

    });
   
}



function SaveProcessflow()
{
    debugger;
    $("#ddlEMP").removeClass("Mandatory");
    $("#ddlP").removeClass("Mandatory")
    $("#txtAttachmentfile").removeClass("Mandatory");
    $("#txtReason").removeClass("Mandatory");
    if (checkValidationOnSubmit('Mandatory') == true) {
     ShowLoadingDialog();
       var model = {
            Id: $("#hdfid").val(),
            LevelOne: $('#ddllavel').val(),            
            LevelTwo: $('#ddllaveltwo').val(), 
            LevelThree: $('#ddllavelthree').val(),  
            LevelFour: $('#ddllavelfour').val(), 
            LevelFive: $('#ddllavelfive').val() 
     }
    const jsonString = JSON.stringify(model);
     
     let GenericModeldata =
    {
        ScreenID: "HRAction",
        Operation: "ADD",
        ModelData: jsonString,
       
    };    

      
  CommonAjaxMethod(virtualPath + 'Generic/PerformOperation', GenericModeldata, 'POST', function (response) {
  BindScreen();
   $('#fts').modal('hide')
       $("#btnR").hide();
        $("#btnF").hide();
        $("#btnA").hide();
       // $("#divR").show();
         CloseLoadingDialog();
    });
  }

    
}
   

function SaveRecord()
{
    //$("#txtReason").addClass("Mandatory");
    $("#ddlEMP").removeClass("Mandatory");
    $("#ddlP").removeClass("Mandatory");
         if (checkValidationOnSubmit('Mandatory')) {
              ShowLoadingDialog();
       var model = {
            ReqID: $("#hdfid").val(),
            Reason: $('#txtReason').val(),            
           ActualFileName:$('#hdnUploadActualFileName').val(),
           NewFileName: $('#hdnUploadNewFileName').val(),
           FileUrl:  $('#hdnUploadFileUrl').val()  ,
         
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

function UploadDocument() 
{
 
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
function DownloadFileRFP() {
    debugger;
    var fileURl = $('#hdnUploadFileUrl').val();
    var fileName = $('#hdnUploadActualFileName').val();
    if (fileURl != "" || fileURl != "") {
        var stSplitFileName = fileName.split(".");
        var link = document.createElement("a");
        link.download = stSplitFileName[0];
        link.href = fileURl;
        link.click();
    }
}
function DisplaydataDirect() {
    debugger;
    var dateText = $("#RDateP").text().trim(); 
     var parts = dateText.split("/"); // ["13", "02", "2025"]
    var day = parts[0];
    var month = parts[1];
    var year = parts[2];
    var formattedDate = `${year}-${month}-${day}`;
    $("#txtRelievingDate").val(formattedDate);
    LoadMasterDropdown('ddlEMP', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.Employee,
        manualTableId: 0
    }, 'Select', false);

    var id = $("#hdfid").val();
    var model = {
        Id: id,
        EMPID: 0

    }
    const jsonString = JSON.stringify(model);
    var ScreenID = "HRDirectConfirmation";
    CommonAjaxMethod(virtualPath + 'Generic/GetRecords', { modelData: jsonString, screenId: ScreenID }, 'GET', function (response) {
        debugger;
        var data = response.data.data.Table;
        var dataArry = data;
        $('#tblInstructions').html('');
        var newtbblData1 = '<table id="tblInstructionsdata" class="table table-striped mb-0 mt-0 vt-a" >' +
            ' <thead>' +
            ' <tr>' +
            ' <th width="33">Action</th>' +
            ' <th width="150">Department</th>' +
            ' <th width="150">Location</th>' +
            ' <th>HOD</th>' +
            ' <th width="500" >Remarks</th>' +
            ' <th width="150">Notification CC</th>' +
            ' </tr>' +
            ' </thead>';
        var html1 = "</table>";
        var tableData = "";
        for (let i = 0; i < dataArry.length; i++) {
            var newtbblData = "<tr><td class='text-center'><input type='checkbox' id='Check_" + i + "'  value=''><label for='Check_" + i + "'></label></td><td>" + dataArry[i].Department + "</td><td> <select class='form-control applyselect' id='ddlLocation_" + i + "'>  </select></td><td> <select class='form-control applyselect' id='ddlHOD_" + i + "' >  </select></td><td><textarea class='form-control' placeholder='Enter Instructions'></textarea></td><td><select class='form-control applyselect' multiple='' id='ddlNotification_" + i + "'>  </select></td></tr>";
            var allstring = newtbblData;
            tableData += allstring;
        }
        $('#tblInstructions').html(newtbblData1 + tableData + html1);
        for (let i = 0; i < dataArry.length; i++) {
            LoadMasterDropdown('ddlLocation_' + i, {
                ParentId: 0,
                masterTableType: 0,
                isMasterTableType: false,
                isManualTable: true,
                manualTable: ManaulTableEnum.MasterLocation,
                manualTableId: 0
            }, 'Select', false);

            LoadMasterDropdown('ddlHOD_' + i, {
                ParentId: 0,
                masterTableType: 0,
                isMasterTableType: false,
                isManualTable: true,
                manualTable: ManaulTableEnum.Employee,
                manualTableId: 0
            }, 'Select', false);
            LoadMasterDropdown('ddlNotification_' + i, {
                ParentId: 0,
                masterTableType: 0,
                isMasterTableType: false,
                isManualTable: true,
                manualTable: 64,
                manualTableId: 0
            }, 'Select', false);
            $('#ddlLocation_' + i).val(dataArry[i].Location).trigger('change'); 
            $('#ddlHOD_' + i).val(dataArry[i].HOD).trigger('change'); 
        }
        $(".applyselect").select2();

    });
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
var DocAArray = [];
var DocAArrayId = 0;
function AddDocuments() {
 
        var valid = true;
        if (valid == true) {
            var fileInput = document.getElementById('fileAttachmentScopeofwork');
            var filename = fileInput.files[0].name;
            DocAArrayId = DocAArrayId + 1;
            var loop = DocAArrayId;
            var objarrayinner =
            {
                ID: loop,
                Attachment: filename,
                AttachmentActualName: $('#hdnfileAttachmentScopeActualName').val(),
                AttachmentNewName: $('#hdnfileAttachmentScopeNewName').val(),
                AttachmentUrl: $('#hdnfileAttachmentScopeFileUrl').val(),
                Description: $('#txtDescription').val(),
                Action: 'Remove',
                DocumentId: loop
            }

            DocAArray.push(objarrayinner);
            var dataArry = DocAArray;
            $('#tblUploadingDocument').html('');
            var newtbblData1 = '<table id="tblPreRegistrationUploadingDocuments" class="table table-striped m-0 " >' +
                ' <thead>' +
                ' <tr>' +
                ' <th width="33">S.No</th>' +
                ' <th width="150">Attachment</th>' +
                ' <th>Description</th>' +
                ' <th width="50" class=" text-center">Action</th>' +
                ' </tr>' +
                ' </thead>';
            var html1 = "</table>";
            var tableData = "";
            for (let i = 0; i < dataArry.length; i++) {
                var newtbblData = "<tr><td>" + parseInt(i + 1) + "</td><td><a target='_blank' href=" + dataArry[i].AttachmentUrl + ">" + dataArry[i].AttachmentActualName + "</a></td><td>" + dataArry[i].Description + "</td><td class='text-center' ><a class='HideClass'  title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='deleteDocArrayRows(this," + dataArry[i].DocumentId + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a> </td></tr>";
                var allstring = newtbblData;
                tableData += allstring;
            }
            $('#tblUploadingDocument').html(newtbblData1 + tableData + html1);
            $('#txtDescription').val('');
            $("#ddlDocumentType").val('0').trigger('change');
            $('#fileAttachmentScopeofwork').val('');
          
        }
   
}
function UploadAttachmentFileScope() {
    var fileUpload = $("#fileAttachmentScopeofwork").get(0);

    var files = fileUpload.files;
    if (files.length > 0) {
        // Create FormData object
        var fileData = new FormData();

        // Looping over all files and add it to FormData object
        for (var i = 0; i < files.length; i++) {
            fileData.append(files[i].name, files[i]);
        }

        $.ajax({
            url: virtualPath + 'CommonMethod/UploadExitDocument',
            type: "POST",
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            data: fileData,

            success: function (response) {
                var result = JSON.parse(response);
                if (result.ErrorMessage == "") {
                    $('#hdnfileAttachmentScopeActualName').val(result.FileModel.ActualFileName);
                    $('#hdnfileAttachmentScopeNewName').val(result.FileModel.NewFileName);
                    $('#hdnfileAttachmentScopeFileUrl').val(result.FileModel.FileUrl);
                }
                else {
                    FailToaster(result.ErrorMessage);
                    //document.getElementById('hReturnMessage').innerHTML = result.ErrorMessage;
                    //$('#btnShowModel').click();
                }
            }
            ,
            error: function (error) {
                FailToaster(error);
                //document.getElementById('hReturnMessage').innerHTML = error;
                //$('#btnShowModel').click();
                isSuccess = false;
            }
        });
    }
    else {
        FailToaster("Please select file to attach!");
        //document.getElementById('hReturnMessage').innerHTML = "Please select file to attach!";
        //$('#btnShowModel').click();
    }
}
function deleteDocArrayRows(obj, num) {

    var count = 0;
    var TotalRowCount = $('#tblUploadingDocument').find("tbody tr").length;
    var NUM = num;
    var NARR = DocAArray;
    ConfirmMsgBox("Are you sure you want to delete", '', function () {

        $(obj).closest('tr').remove();

        for (let i = 0; i < NARR.length; i++) {
            if (NARR[i].DocumentId == NUM) {
                NARR.splice(i, 1);
                break;
            }
        }
        DocAArray = NARR;
        $("#tblUploadingDocument TBODY TR").each(function (i) {
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
        var form = $("#HRApproveAction").closest("form");
        form.removeData('validator');
        form.removeData('unobtrusiveValidation');
        $.validator.unobtrusive.parse(form);

    })
}

function SaveInstrunction() {
    let tableDataArray = [];
    debugger;
    // Iterate over each row in the table body
    $('#tblInstructionsdata tbody tr').each(function () {
        debugger;
        // Initialize an object to store the row's data
        let rowData = {};
        // Extract data from each cell of the row
        rowData.Action = $(this).find("input[type='checkbox']").prop('checked'); // Checkbox state
        rowData.Department = $(this).find('td:nth-child(2)').text(); // Department text
        rowData.Location = $(this).find("select[id^='ddlLocation_']").val(); // Location dropdown value
        rowData.HOD = $(this).find("select[id^='ddlHOD_']").val(); // HOD dropdown value
        rowData.Remarks = $(this).find('textarea').val(); // Remarks textarea value
        var notificationCCValues = $(this).find("select[id^='ddlNotification_']").val();
        rowData.NotificationCC = notificationCCValues && notificationCCValues.length > 0 ? notificationCCValues.join(',') : ''; 
        //rowData.NotificationCC = notificationCCValues && notificationCCValues.length > 0 ? notificationCCValues : ''; // Empty string if no selection
       // rowData.NotificationCC = $(this).find("select[id^='ddlNotification_']").val(); // Notification dropdown values (as array for multiple)
        // Push the row data into the array
        tableDataArray.push(rowData);
    });

    ShowLoadingDialog();
    var model = {
        ReqID: $("#hdfid").val(),
        RelievingDate: $('#txtRelievingDate').val(),
        DirectConfirmationRemarks: $('#txtDCRemark').val(),
        isCheckedExit: $('#annoance').prop('checked'),
        Document: DocAArray,
        HandoverTask: DocArray,
        Instructions: tableDataArray
    }
    const jsonString = JSON.stringify(model);

    let GenericModeldata =
    {
        ScreenID: "HRDirectConfirmation",
        Operation: "ADD",
        ModelData: jsonString,

    };


    CommonAjaxMethod(virtualPath + 'Generic/PerformOperation', GenericModeldata, 'POST', function (response) {
        $('#int1').modal('hide')
        BindScreen();
        CloseLoadingDialog();
    });
}




