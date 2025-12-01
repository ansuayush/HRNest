$(document).ready(function () {

    BindExitScreen()
   
  
});

function BindExitScreen() {

    var id = $("#hdfEMPID").val();
    var model = {
        EMPID: id
      

    }

    const jsonString = JSON.stringify(model);
    var ScreenID = "Exit Process Request";
    CommonAjaxMethod(virtualPath + 'Generic/GetRecords', { modelData: jsonString, screenId: ScreenID }, 'GET', function (response) {
        debugger;
     var T=response.data.data.Table2;
        var tableIdP = '#tabelpending';
        var tablePending = $('#tabelpending').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": response.data.data.Table,
            "stateSave": true, // Enable state saving
            "columns": [
                { "data": "RowNumber" },
                { "data": "emp_code" },
                { "data": "emp_name" },
                { "data": "Location" },
                { "data": "Department" },
                { "data": "Designation" },
                { "data": "ReqDate" },
                { "data": "relievingdate" },
                { "data": "relievingday" },
                {
                    "orderable": false,
                    "data": null,
                    "render": function (data, type, row) {
                        //var strReturn = '<div class="text-center" ><a href="#" onclick="ViewResignation(' + row.ID + ')" ><i class="fas fa-cogs" data-toggle="tooltip" title="" data-original-title="View"></i>Start Exit Process</a><span class="divline">|</span><a href="#" onclick="RecordResolution(' + row.ID + ')" ><i class="fas fa-file-alt" data-toggle="tooltip" title="" data-original-title="View"></i>Record Resolution</a></div>';
                        var strReturn = '<strong><a href="#"  onclick="ViewResignation(' + row.ID + ')"><i class="fas fa-cogs"></i>Start Exit Process</a><span class="divline">|</span><a href="#"  data-toggle="modal" data-target="#rfc" onclick="RecordResolution(' + row.ID + ')"><i class="fas fa-file-alt"></i>Record Resolution</a></strong>';
                        return strReturn;
                    }
                }

            ]
            ,

        });
        tablePending.destroy();

        // Initialize tooltips for the initial set of rows
        $('[data-toggle="tooltip"]').tooltip();

       //  Re-initialize tooltips every time the table is redrawn
        tablePending.on('draw.dt', function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

       DatatableScriptsWithColumnSearch(tableIdP.substring(1), tablePending);
       


        var tableIdR = '#tabelresolution';
        var tableResolution = $('#tabelresolution').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": response.data.data.Table1,
            "stateSave": true, // Enable state saving
            "columns": [
                { "data": "RowNumber" },
                { "data": "emp_code" },
                { "data": "emp_name" },
                { "data": "Location" },
                { "data": "Department" },
                { "data": "Designation" },
                { "data": "ReqDate" },
                { "data": "relievingdate" },
                {
                    "orderable": false,
                    "data": null,
                    "render": function (data, type, row) {
                        //var strReturn = '<div class="text-center" ><a href="#" onclick="ViewResignation(' + row.ID + ')" ><i class="fas fa-cogs" data-toggle="tooltip" title="" data-original-title="View"></i>Start Exit Process</a><span class="divline">|</span><a href="#" onclick="RecordResolution(' + row.ID + ')" ><i class="fas fa-file-alt" data-toggle="tooltip" title="" data-original-title="View"></i>Record Resolution</a></div>';
                        var strReturn = '<strong><a href="#"  onclick="ViewRRResignation(' + row.ID + ')"><i class="fas fa-eye"></i>View</a></strong>';
                        return strReturn;
                    }
                }

            ]
            ,

        });

        tableResolution.destroy();

        // Initialize tooltips for the initial set of rows
        $('[data-toggle="tooltip"]').tooltip();

        // Re-initialize tooltips every time the table is redrawn
        tableResolution.on('draw.dt', function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        DatatableScriptsWithColumnSearch(tableIdR.substring(1), tableResolution);


        var tableIdN = '#tabelnocprocess';
        var tabelnocprocess = $('#tabelnocprocess').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": response.data.data.Table2,
            "stateSave": true, // Enable state saving
            "columns": [
                { "data": "RowNumber" },
                { "data": "emp_code" },
                { "data": "emp_name" },
                { "data": "Location" },
                { "data": "Department" },
                { "data": "Designation" },
         
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {
                      
                        var strReturn = "";
                        if (row.HR == "Approved") {
                            strReturn = "<a herf='#'  data-toggle='modal' data-target='#hvAdmin' onclick='DisplayData(" + row.ID + ",3)' class='green-clr'><strong><i class='fas fa - envelope - open'></i>Completed</strong></a> ";
                        }
                        else {
                            strReturn = "<a  class='red-clr'><i class='far fa-clock'></i>Pending </a> ";

                        }


                        return strReturn;
                    }
                },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {
                   
                        var strReturn = "";
                        if (row.IT == "Approved") {
                            strReturn = "<a herf='#'  data-toggle='modal' data-target='#hvAdmin' onclick='DisplayData(" + row.ID + ",2)' class='green-clr'><strong><i class='fas fa - envelope - open'></i>Completed</strong></a> ";
                        }
                        else {
                            strReturn = "<a  class='red-clr'><i class='far fa-clock'></i>Pending </a> ";

                        }


                        return strReturn;
                    }
                },

                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {
                        
                        var strReturn = "";
                        if (row.Admin == "Approved") {
                            var d = "Admin";
                            strReturn = "<a herf='#' data-toggle='modal' data-target='#hvAdmin' onclick='DisplayData(" + row.ID + ",1)' class='green-clr'><strong><i class='fas fa - envelope - open'></i>Completed</strong></a> ";
                        }
                        else {
                            strReturn = "<a  class='red-clr'><i class='far fa-clock'></i>Pending </a> ";

                        }


                        return strReturn;
                    }
                },


                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {
                     
                        var strReturn = "";
                        if (row.Finance == "Approved") {
                            strReturn = "<a herf='#'  data-toggle='modal' data-target='#hvAdmin' onclick='DisplayData(" + row.ID + ",4)' class='green-clr'><strong><i class='fas fa - envelope - open'></i>Completed</strong></a> ";
                        }
                        else {
                            strReturn = "<a  class='red-clr'><i class='far fa-clock'></i>Pending </a> ";

                        }


                        return strReturn;
                    }
                },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {

                        var strReturn = "";
                        if (row.Other == "Approved") {
                            strReturn = "<a herf='#'  data-toggle='modal' data-target='#hvAdmin' onclick='DisplayData(" + row.ID + ",5)' class='green-clr'><strong><i class='fas fa - envelope - open'></i>Completed</strong></a> ";
                        }
                        else {
                            strReturn = "<a  class='red-clr'><i class='far fa-clock'></i>Pending </a> ";

                        }


                        return strReturn;
                    }
                },

                {
                    "orderable": false,
                    "data": null,
                    "render": function (data, type, row) {
                        //var strReturn = '<div class="text-center" ><a href="#" onclick="ViewResignation(' + row.ID + ')" ><i class="fas fa-cogs" data-toggle="tooltip" title="" data-original-title="View"></i>Start Exit Process</a><span class="divline">|</span><a href="#" onclick="RecordResolution(' + row.ID + ')" ><i class="fas fa-file-alt" data-toggle="tooltip" title="" data-original-title="View"></i>Record Resolution</a></div>';
                        var strReturn = '<strong><a href="#"  onclick="ViewResignation(' + row.ID + ')"><i class="fas fa-eye"></i>View</a></strong>';
                        return strReturn;
                    }
                }

            ]
            ,

        });

        tabelnocprocess.destroy();

        // Initialize tooltips for the initial set of rows
        $('[data-toggle="tooltip"]').tooltip();

        // Re-initialize tooltips every time the table is redrawn
        tabelnocprocess.on('draw.dt', function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        DatatableScriptsWithColumnSearch(tableIdN.substring(1), tabelnocprocess);




        var tableIdpartb = '#TabelPendingpartb';
        var tablePendingpartb = $('#TabelPendingpartb').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": response.data.data.Table3,
            "stateSave": true, // Enable state saving
            "columns": [
                { "data": "RowNumber" },
                { "data": "emp_code" },
                { "data": "emp_name" },
                { "data": "Location" },
                { "data": "Department" },
                { "data": "Designation" },
                { "data": "ReqDate" },
                { "data": "relievingdate" },
                //{ "data": "relievingday" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {

                        var strReturn = "";
                        if (row.Daysleft <= 7) {
                            strReturn = "<strong class='badge badge - secondary mtr - y - bg'>" + row.Daysleft + " Days</strong> ";
                        }
                        else {
                            strReturn = "<strong class='badge badge - secondary'>" + row.Daysleft + " Days</strong> ";

                        }


                        return strReturn;
                    }
                },
            
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {
                        debugger;
                        var a = row.flag;
                        var strReturn = "";
                        if (row.flag == "Pending") {
                            strReturn = "<strong class='red-clr'>Pending </strong> ";
                        }
                        else {
                            strReturn = "<strong class='green-clr'>Recorded </strong> ";

                        }


                        return strReturn;
                    }
                },
                {
                    "orderable": false,
                    "data": null,
                    "render": function (data, type, row) {
                        var strReturn = '<strong><a href="#" data-toggle="modal" data-target="#hvBandB"  onclick="ViewFandF(' + row.ID + ')"><i class="fas fa-eye"></i>View</a></strong>';
                        return strReturn;
                    }
                }

            ]
            ,

        });
        tablePendingpartb.destroy();

        // Initialize tooltips for the initial set of rows
        $('[data-toggle="tooltip"]').tooltip();

        //  Re-initialize tooltips every time the table is redrawn
        tablePendingpartb.on('draw.dt', function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        DatatableScriptsWithColumnSearch(tableIdpartb.substring(1), tablePendingpartb);


    });



}

function RecordResolution(Id){

    $("#hdfid").val(Id);
    $('#txtReason').val("");
    $('#txtAttachment').val("");
    $('#hdnUploadNewFileName').val("");
    $('#hdnUploadFileUrl').val("");
    $('#hdnUploadActualFileName').val("");
    
  $("#rfc").show();
}

function SaveRecord()
{
      //$("#txtReason").addClass("Mandatory");
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
      BindExitScreen();
   $('#rfc').modal('hide')
  CloseLoadingDialog();
    });
  } 
 else {
    CloseLoadingDialog();
  }   
}

function DisplayData(Id, DocType) {
    DocArray = [];
    debugger;
    if (DocType == "1") {
        DocType = "Admin";
        $("#divHR").hide();
    }
    else if (DocType == "2") {
        DocType = "IT";
        $("#divHR").hide();
    }
    else if (DocType == "3") {
        DocType = "HR";
        $("#divHR").show();
        LoadMasterDropdown('ddlNotify', {
            ParentId: 0,
            masterTableType: 0,
            isMasterTableType: false,
            isManualTable: true,
            manualTable: 64,
            manualTableId: 0
        }, 'Select', false);
    }
    else if (DocType == "4") {
        DocType = "Finance";
        
    }
    else if (DocType == "5") {
        DocType = "Other";
    }
    var model = {
        ReqId: Id,
        DocType: DocType


    }

    const jsonString = JSON.stringify(model);
    var ScreenID = "NOC101";
    CommonAjaxMethod(virtualPath + 'Generic/GetRecords', { modelData: jsonString, screenId: ScreenID }, 'GET', function (response) {
        debugger;
        var data = response.data.data.Table;

        var dataUploadingDocument = response.data.data.Table3;
        var dataHR = response.data.data.Table2;
        var dataHRDocument = response.data.data.Table4;
        if (DocType == "HR") {
            if (dataHRDocument.length > 0) {
                $('#hdnUploadActualFileNameSurvey').val(dataHRDocument[0].ActualFileName);
                $('#hdnUploadNewFileNameSurvey').val(dataHRDocument[0].NewFileName);
                $('#hdnUploadFileUrlSurvey').val(dataHRDocument[0].FileUrl);
                $('#hdnUploadID').val(dataHRDocument[0].ID);
            }
            else {
                $('#hdnUploadActualFileNameSurvey').val('');
                $('#hdnUploadNewFileNameSurvey').val('');
                $('#hdnUploadFileUrlSurvey').val('');
                $('#hdnUploadID').val(0);
            }
            if (dataHR.length > 0) {
                var NotifyData = dataHR[0].Notify;
                if (NotifyData) {
                    var valuesArray = NotifyData.split(','); // Convert string to array: ["1", "2", "3"]
                    $('#ddlNotify').val(valuesArray).trigger('change'); // Assign multiple values and trigger change
                    $("#txtRemarksSurvey").val(dataHR[0].Remarks);
                }
                if (dataHR[0].Status == '2') {
                    $("#btnsave").show();
                    $("#btnsubmit").show();

                }
                else {
                    $("#btnsave").hide();
                    $("#btnsubmit").hide();
                }
            }
            else {
                $("#txtRemarksSurvey").val('');
                $("#btnsave").show();
                $("#btnsubmit").show();
            }
        }
  


       // $('#ddlNotify').val(NotifyData).trigger('change');
     
        $("#hdfRid").val(data[0].ID);
        $("#labelsreq").text(data[0].ReqNo);
        $("#labelsreqdate").text(data[0].ReqDate);
        $("#labelsreqby").text(data[0].Reqby);
        $("#labelsloc").text(data[0].Location);
        $("#labelsdept").text(data[0].Department);
        $("#labelsdesg").text(data[0].Designation);
        $("#labelsRdate").text(data[0].relievingdate);
        if (DocType == "Finance") {
            $("#divTabelDataNOC").hide();
            $("#divTabelDataNOCOther").hide();
            $("#divTabelDataNOCFin").show();
            $("#divHR").hide();
         
            var table = $('#TabelDataNOCFin').DataTable({
                "processing": true,
                "destroy": true,
                "info": false,
                "lengthChange": false,
                "bFilter": false,
                "data": response.data.data.Table1,
                "columns": [

                    { "data": "RowNumber" },
                    { "data": "DocType" },
                    { "data": "AssetID" },
                    { "data": "DateOfReturn" },
                    { "data": "Description" },
                    { "data": "Type" },
                    { "data": "Status" },
                    { "data": "Remarks" },
                     { "data": "Recovered" },
                    { "data": "AmountofRecovery" }


                ]
                ,

            });
        }
        else if (DocType == "Other") {
            $("#divTabelDataNOC").hide();
            $("#divTabelDataNOCFin").hide();
            $("#divTabelDataNOCOther").show();

            var table = $('#TabelDataNOCOther').DataTable({
                "processing": true,
                "destroy": true,
                "info": false,
                "lengthChange": false,
                "bFilter": false,
                "data": response.data.data.Table1,
                "columns": [

                    { "data": "RowNumber" },
                    { "data": "TaskName" },
                    { "data": "emp_name" },
                    { "data": "Priority" }

                ]
                ,

            });
        }
        else {
            $("#divTabelDataNOC").show();
            $("#divTabelDataNOCFin").hide();
            $("#divTabelDataNOCOther").hide();
            var table = $('#TabelDataNOC').DataTable({
                "processing": true,
                "destroy": true,
                "info": false,
                "lengthChange": false,
                "bFilter": false,
                "data": response.data.data.Table1,
                "columns": [

                    { "data": "RowNumber" },
                    { "data": "AssetID" },
                    { "data": "DateOfReturn" },
                    { "data": "Description" },
                    { "data": "Type" },
                    { "data": "Status" },
                    { "data": "Remarks" }



                ]
                ,

            });
        }
        if (dataUploadingDocument.length > 0) {
        
            DocArray = [];
            BindDataUploadingDocument(dataUploadingDocument)
        }
        else {
           
            $('#tblnotify').html('');
        }

    });


}


function DeleteRow(obj, num) {
    debugger;
    var count = 0;
    var TotalRowCount = $('#tblPreRegistrationUploadingDocuments').find("tbody tr").length;
    var NUM = num;
    var NARR = DocArray;
    if (parseInt(NUM) == 0) {
        ConfirmMsgBox("Are you sure you want to delete", '', function () {

            $(obj).closest('tr').remove();

            for (let i = 0; i < NARR.length; i++) {
                if (NARR[i].DocumentId == NUM) {
                    NARR.splice(i, 1);
                    break;
                }
            }
            DocArray = NARR;
            $("#tblPreRegistrationUploadingDocuments TBODY TR").each(function (i) {
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
    else {
        ConfirmMsgBox("Are you sure you want to delete", '', function () {
            $(obj).closest('tr').remove();

            for (let i = 0; i < NARR.length; i++) {
                if (NARR[i].DocumentId == NUM) {
                    NARR.splice(i, 1);
                    break;
                }
            }
            DocArray = NARR;
            $(obj).closest('tr').remove();
            $("#tblPreRegistrationUploadingDocuments TBODY TR").each(function (i) {
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

            var dataObject = JSON.stringify({
                'ID': NUM,
                'Doctype': 'Exit',
            });
            var data = $.parseJSON($.ajax({
                url: '/CommonAjax/DelRecord_CommonJson',
                type: "post",
                data: dataObject,
                contentType: 'application/json',
                async: false
            }).responseText);
            return data;
            var form = $("#ExitProcessRequest").closest("form");
            form.removeData('validator');
            form.removeData('unobtrusiveValidation');
            $.validator.unobtrusive.parse(form);

        })
    }
   
}
function UploadDocumentSurvey() {

    var fileUpload = $("#txtAttachmentSurvey").get(0);
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
                    $('#hdnUploadActualFileNameSurvey').val(result.FileModel.ActualFileName);
                    $('#hdnUploadNewFileNameSurvey').val(result.FileModel.NewFileName);
                    $('#hdnUploadFileUrlSurvey').val(result.FileModel.FileUrl);
                    $('#txtAttachmentfileSurvey').val(result.FileModel.ActualFileName);

                }
            },

        });

    }
}
function SavedataHR() {
    debugger;
    var selectedValues = $("#ddlNotify").val();
    var Notifydata = "";
    if (selectedValues && selectedValues.length > 0) {
         Notifydata = selectedValues.join(","); // Convert array to a string like "2,12,14"
       

    }
    else {
        Notifydata = "";
    }

    if (parseInt($("#hdfRid").val())>0) {
        //ShowLoadingDialog();
        var model = {
            ReqID: $("#hdfRid").val(),
            Notify: Notifydata,
            Remark: $('#txtRemarksSurvey').val(),
            ActualFileName: $('#hdnUploadActualFileNameSurvey').val(),
            NewFileName: $('#hdnUploadNewFileNameSurvey').val(),
            FileUrl: $('#hdnUploadFileUrlSurvey').val(),
            AttachmentID: $('#hdnUploadID').val(),
            Status: "2",
            //ItemData: DocArray
            ItemData: DocArray.filter(doc => doc.DocumentId == "0")

        }
        const jsonString = JSON.stringify(model);
      
        let GenericModeldata =
        {
            ScreenID: "HeadHRAction",
            Operation: "ADD",
            ModelData: jsonString,

        };


        CommonAjaxMethod(virtualPath + 'Generic/PerformOperation', GenericModeldata, 'POST', function (response) {
            BindExitScreen();
            $('#hvAdmin').modal('hide')
            CloseLoadingDialog();
        });
    }
    else {
        CloseLoadingDialog();
    }
}
function BindDataUploadingDocument(dataUploadingDocument) {
    debugger;
    var dataArry = dataUploadingDocument
    for (let i = 0; i < dataArry.length; i++) {
        DocArrayId = DocArrayId + 1;
        var loop = DocArrayId;
        var objarrayinner =
        {
            ID: loop,
            Item: dataArry[i].item,
            Impactdef: dataArry[i].ImpactofDeficiency,
            Remarks: dataArry[i].Remarks,
            Status: dataArry[i].Status,
            Action: 'Remove',
            DocumentId: dataArry[i].ID,

        }

        DocArray.push(objarrayinner);
    }
    dataArry = DocArray;

    $('#tblnotify').html('');
   
    var newtbblData1 = '<table id="tblPreRegistrationUploadingDocuments" class="table table-striped m-0 " >' +
        ' <thead>' +
        ' <tr>' +
        ' <th width="33">S.No</th>' +
        ' <th width="150">Item</th>' +
        ' <th width="150">Impact of Deficiency</th>' +
        ' <th width="150">Status</th>' +
        ' <th width="150">Remarks</th>' +
        ' <th width="50" class=" text-center">Action</th>' +
        ' </tr>' +
        ' </thead>';
    var html1 = "</table>";
    var tableData = "";
    for (let i = 0; i < dataArry.length; i++) {
        var newtbblData = "<tr><td>" + parseInt(i + 1) + "</td><td>" + dataArry[i].Item + "</td><td>" + dataArry[i].Impactdef + "</td><td>" + dataArry[i].Status + "</td><td>" + dataArry[i].Remarks + "</td><td class='text-center' ><a class='HideClass'  title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='DeleteRow(this," + dataArry[i].DocumentId + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a> </td></tr>";
        var allstring = newtbblData;
        tableData += allstring;
    }
    $('#tblnotify').html(newtbblData1 + tableData + html1);
   // $("#ddlStatus").val('0').trigger('change');
   // HtmlPagingUploadingDocuments();
}

var DocArray = [];
var DocArrayId = 0;
function AddRowData() {



    var valid = true;

    if (valid == true) {
        var Item = $("#txtItem").val();
        var Impactdef = $("#ddlImpactdef option:selected").text();
        var Remarks = $("#txtRemark").val();
        var Status = $("#ddlStatus option:selected").text();
        DocArrayId = DocArrayId + 1;
        var loop = DocArrayId;


        var objarrayinner =
        {
            ID: loop,
            Item: Item,
            Impactdef: Impactdef,
            Remarks: Remarks,
            Status: Status,
            Action: 'Remove',
            DocumentId: 0,

        }

        DocArray.push(objarrayinner);
        var dataArry = DocArray;
        $('#tblnotify').html('');


        var newtbblData1 = '<table id="tblPreRegistrationUploadingDocuments" class="table table-striped m-0 " >' +
            ' <thead>' +
            ' <tr>' +
            ' <th width="33">S.No</th>' +
            ' <th width="150">Item</th>' +
            ' <th width="150">Impact of Deficiency</th>' +
            ' <th width="150">Status</th>' +
            ' <th width="150">Remarks</th>' +
            ' <th width="50" class=" text-center">Action</th>' +
            ' </tr>' +
            ' </thead>';
        var html1 = "</table>";
        var tableData = "";
        for (let i = 0; i < dataArry.length; i++) {
            var newtbblData = "<tr><td>" + parseInt(i + 1) + "</td><td>" + dataArry[i].Item + "</td><td>" + dataArry[i].Impactdef + "</td><td>" + dataArry[i].Status + "</td><td>" + dataArry[i].Remarks + "</td><td class='text-center' ><a class='HideClass'  title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='DeleteRow(this," + dataArry[i].DocumentId + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a> </td></tr>";
            var allstring = newtbblData;
            tableData += allstring;
        }
        $('#tblnotify').html(newtbblData1 + tableData + html1);
        $("#ddlStatus").val('0').trigger('change');
        $("#ddlImpactdef").val('0').trigger('change');
        $("#txtItem").val('');
        $("#txtRemark").val('');
    }


}
function DownloadFileRFP() {
    debugger;
    var fileURl = $('#hdnUploadFileUrlSurvey').val();
    var fileName = $('#hdnUploadActualFileNameSurvey').val();
    if ( fileURl!="" ) {
        var stSplitFileName = fileName.split(".");
        var link = document.createElement("a");
        link.download = stSplitFileName[0];
        link.href = fileURl;
        link.click();
    }
}
function SubmitdataHR() {
    debugger;
    var selectedValues = $("#ddlNotify").val();
    var Notifydata = "";
    if (selectedValues && selectedValues.length > 0) {
        Notifydata = selectedValues.join(","); // Convert array to a string like "2,12,14"


    }
    else {
        Notifydata = "";
    }

    if (parseInt($("#hdfRid").val()) > 0) {
        //ShowLoadingDialog();
        var model = {
            ReqID: $("#hdfRid").val(),
            Notify: Notifydata,
            Remark: $('#txtRemarksSurvey').val(),
            ActualFileName: $('#hdnUploadActualFileNameSurvey').val(),
            NewFileName: $('#hdnUploadNewFileNameSurvey').val(),
            FileUrl: $('#hdnUploadFileUrlSurvey').val(),
            AttachmentID: $('#hdnUploadID').val(),
            Status: "1",
            //ItemData: DocArray
            ItemData: DocArray.filter(doc => doc.DocumentId == "0")

        }
        const jsonString = JSON.stringify(model);

        let GenericModeldata =
        {
            ScreenID: "HeadHRAction",
            Operation: "ADD",
            ModelData: jsonString,

        };


        CommonAjaxMethod(virtualPath + 'Generic/PerformOperation', GenericModeldata, 'POST', function (response) {
            BindExitScreen();
            $('#hvAdmin').modal('hide')
            CloseLoadingDialog();
        });
    }
    else {
        CloseLoadingDialog();
    }
}

function ViewFandF(Id,) {
    debugger;
    $("#hdfid").val(Id);
    $("#hvBandB").show();
    BindEmployeeSaveNOC(Id);
}
function BindEmployeeSaveNOC(Id) {

    debugger;
    var model = {
        ReqId: Id,
        DocType: "HeadHR"

    }

    const jsonString = JSON.stringify(model);
    var ScreenID = "NOC101";
    CommonAjaxMethod(virtualPath + 'Generic/GetRecords', { modelData: jsonString, screenId: ScreenID }, 'GET', function (response) {
        debugger;
        var data = response.data.data.Table;
        var FinData = response.data.data.Table3;
        $("#labelssreq").text(data[0].ReqNo);
        $("#labelssreqdate").text(data[0].ReqDate);
        $("#labelssreqby").text(data[0].emp_name);
        $("#labelssloc").text(data[0].Location);
        $("#labelssdept").text(data[0].Department);
        $("#labelssdesg").text(data[0].Designation);
        $("#labelssRdate").text(data[0].relievingdate); 
        $("#pHeadRemark").text(data[0].HeadRemarks);
        if (data[0].statusflag == "10") {
           
            $("#btnFandF").show();
        }
        else {
           
            $("#btnFandF").hide();
        }
        var table = $('#TabelDataNOCOtherFandF').DataTable({
            "processing": true,
            "destroy": true,
            "info": false,
            "lengthChange": false,
            "bFilter": false,
            "data": response.data.data.Table3,
            "columns": [
                { "data": "RowNumber" },
                { "data": "item" },
                { "data": "ImpactofDeficiency" },
                { "data": "Status" },
                { "data": "Remarks" },

            ]
            ,

        });


        var tabelapproved = $('#TabelDataNOCFinBandB').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": response.data.data.Table1,
            "stateSave": true, // Enable state saving
            "columns": [
                //{ "data": "RowNumber" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {

                        var strReturn = "";
                        if (row.RowNumber != "") {
                            strReturn = " <input type='hidden' class='sno' value=" + row.ID + " /> " + row.RowNumber + "";
                        }
                        return strReturn;
                    }
                },
                { "data": "DocType" },
                { "data": "AssetID" },
                { "data": "DateOfReturn" },
                { "data": "Description" },
                { "data": "Type" },
                { "data": "Status" },
                { "data": "Remarks" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {
                        var selectedYes = row.Recovered === "Yes" ? "selected" : "";
                        var selectedNo = row.Recovered === "No" ? "selected" : "";
                        var strReturn = "";
                        //if (row.Recovered == "") {
                        //strReturn = "<select class='form - control applyselect'>  < option > Select</option > <option selected=''>Yes</option> <option>No</option> </select > ";
                        //}
                        // return strReturn;

                        return `<select class='form-control applyselect'>
                    <option value=''>Select</option>
                    <option value='Yes' ${selectedYes}>Yes</option>
                    <option value='No' ${selectedNo}>No</option>
                </select>`;

                    }
                },

                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {

                        var strReturn = "";
                        strReturn = "<input type='number' class='form - control text - right f - width' value=" + row.AmountofRecovery + " placeholder='0' name=''> ";
                        return strReturn;
                    }
                },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {

                        var strReturn = "";
                        strReturn = "<input type='number' class='form - control text - right f - width' value=" + row.WriteOffAmount + " placeholder='0' name=''> ";
                        return strReturn;
                    }
                },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {
                        var selectedValue = row.Component;
                        let options = response.data.data.Table2;

                        // Build dropdown options
                        let dropdownHtml = `<select class='form-control ddlOther applyselect'>`;
                        dropdownHtml += `<option value=''>Select</option>`;
                        options.forEach(option => {
                            let isSelected = selectedValue === option.Id ? "selected" : "";
                            dropdownHtml += `<option value='${option.Id}' ${isSelected}>${option.field_name}</option>`;
                        });
                        dropdownHtml += `</select>`;

                        return dropdownHtml;

                    }
                },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {

                        var strReturn = "";
                        strReturn = "<input type='text' class='form - control text - right f - width' value=" + row.HeadFinanceRemarks + "  > ";
                        return strReturn;
                    }
                }


            ]
            ,

        });



    });


}

function ProceedFandF() {
    debugger;


    if (parseInt($("#hdfid").val()) > 0) {
        //ShowLoadingDialog();
        var model = {
            ReqID: $("#hdfid").val(),
            Status: "2"
           

        }
        const jsonString = JSON.stringify(model);

        let GenericModeldata =
        {
            ScreenID: "HeadHRF&F",
            Operation: "ADD",
            ModelData: jsonString,

        };


        CommonAjaxMethod(virtualPath + 'Generic/PerformOperation', GenericModeldata, 'POST', function (response) {
            BindExitScreen();
            $('#hvBandB').modal('hide')
            CloseLoadingDialog();
        });
    }
    else {
        CloseLoadingDialog();
    }
}