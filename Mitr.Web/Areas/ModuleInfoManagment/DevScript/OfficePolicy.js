$(document).ready(function() {
  ShowandCloseLoader();
    GetOfficePolicy();
});

function OfficePolicy() {
 ;
  if (checkValidationOnSubmit('Mandatory')) {
   ShowLoadingDialog();
      var obj = {
        ID: $('#hdnMasterTableId').val(),
        DocumentName: $('#txtPolicy').val(),
        Priority: $('#txtPriority').val(),
        ActualFileName:$('#hdnUploadActualFileName').val(),
        NewFileName: $('#hdnUploadNewFileName').val(),
        FileUrl:  $('#hdnUploadFileUrl').val()     
      };
      CommonAjaxMethod(virtualPath + 'ModuleInfoRequest/AddOfficePolicy', obj, 'POST', function(response) {
        if (response.ValidationInput === 1) {
          ClearFormControl();
          GetOfficePolicy();
          $('#sc').modal('hide');
        } 
        CloseLoadingDialog();
      });
   

  } else {
    CloseLoadingDialog();
  }
}

function UploadDocumentPolicy() 
{
        var fileUpload = $("#txtAttachment").get(0);
        var files = fileUpload.files;
        if (files.length > 0) {
            var fileData = new FormData();
            fileData.append(files[0].name, files[0]);
            $.ajax({
                url: virtualPath + 'CommonMethod/UploadPolicyDocument',
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


function ClearFormControl() {
    $('#txtPolicy').val('');
    $('#hdnMasterTableId').val('');
    $('#txtAttachment').val('');
    $('#attachmentName').val('');
    $('#txtPriority').val('');
    $('#sptxtPolicy').hide();
    $('#sptxtAttachmentfile').hide();
    $('#sptxtPriority').hide();
    $('#txtAttachmentfile').hide();
    $('#txtAttachmentfile').val('');
     $('#btnDownload1').show();
  $('#lblAttachementRFPDownload').show();
}

function GetOfficePolicy() {
    var tableId = '#tableOfficePolicy';
    CommonAjaxMethod(virtualPath + 'ModuleInfoRequest/GetOfficePolicy', null, 'GET', function(response) {
        var table = $(tableId).DataTable({
            "processing": true,
            "destroy": true,
            "info": false,
            "lengthChange": false,
            "bFilter": false,
            "data": response.data.data.Table,
            "columns": [
                {
                    "orderable": true,
                    "data": null,
                    "render": function(data, type, row) {
                        return '<label style="text-align: right; display: inline-block;padding-top: 6px; width: 100%;">' + row.RowNum + '</label>';
                    }
                },
                { "data": "DocumentName" },
				
                {
                  "data": "ActualFileName",
                     "render": function(data, type, row) {
                     return '<a href="' + row.FileUrl + '" download="' + data + '">' + data + '</a>';
                   }
                },

                {
                    "orderable": true,
                    "data": null,
                    "render": function(data, type, row) {
                        return '<label style="text-align: right; display: inline-block; width: 100%;">' + row.Priority + '</label>';
                    }
                },
                {
                    "orderable": true,
                    "data": null,
                    "render": function(data, type, row) {
                        return "<label>" + getdatetimewithoutJson(row.createdat) + "</label>";
                    }
                },
                {
                    "orderable": true,
                    "data": null,
                    "render": function(data, type, row) {
                        return "<label>" + getdatetimewithoutJson(row.modifiedat) + "</label>";
                    }
                },
                { "data": "Status" },
                {
                    "orderable": false,
                    "data": null,
                    "render": function(data, type, row) {
                        var strReturn = "";
                        if (row.Status === "Active") {
                            strReturn = "<a title='Click to Deactivate' data-toggle='tooltip' data-original-title='Click to Deactivate' class='AIsActive' onclick='Activate(" + row.ID + ", 1)'><i class='fa fa-check-circle checkgreen' aria-hidden='true'></i></a><a title='Edit' data-toggle='modal' data-target='#sc' onclick='EditOfficePolicy(" + row.ID + ")'><i class='fas fa-edit checkgreen' aria-hidden='true'></i></a>";
                        } else if (row.Status === "Deactive") {
                            strReturn = "<a title='Click to Activate' data-toggle='tooltip' data-original-title='Click to Activate' class='AIsActive' onclick='Activate(" + row.ID + ", 0)'><i class='fa fa-times-circle crossred' aria-hidden='true'></i></a><a title='Edit' data-toggle='modal' data-target='#sc' onclick='EditOfficePolicy(" + row.ID + ")'><i class='fas fa-edit checkgreen' aria-hidden='true'></i></a>";
                        }
                        return strReturn;
                    }
                }
            ],
            "initComplete": function () {
                initCompleteCallback(tableId.substring(1));
            }
        });
        table.destroy();
        $('[data-toggle="tooltip"]').tooltip();
        table.on('draw.dt', function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
        DatatableScriptsWithColumnSearch(tableId.substring(1), table);
    });
}



function Activate(ID, no) {
    var actionType = "Activate";
    if (no == "1") {
        actionType = "Deactivate";
    }
    CommonAjaxMethod(virtualPath + 'ModuleInfoRequest/GetOfficePolicyByID', { ID: ID }, 'GET', function(response) {
        var data = response.data.data.Table;
        ConfirmMsgBox("Are you sure want to " + actionType + " " + data[0].DocumentName + ".", '', function () {
            var obj = {
                ID: data[0].ID,
                DocumentName: data[0].DocumentName,
                IsActive: 1,
            }
            CommonAjaxMethod(virtualPath + 'ModuleInfoRequest/AddOfficePolicy', obj, 'POST', function(response) {
                GetOfficePolicy();
                ClearFormControl();
            });
        })
    });
}

function EditOfficePolicy(ID) {
    ClearFormControl();
    CommonAjaxMethod(virtualPath + 'ModuleInfoRequest/GetOfficePolicyByID', { ID: ID }, 'GET', function(response) {
        $('#txtPolicy').show();
        $('#txtAttachment').show();
        $('#txtPriority').show();
        var data = response.data.data.Table;
        if (data.length > 0) {
            var policy = data[0]; 
            $('#hdnMasterTableId').val(policy.ID);
            $('#txtPolicy').val(policy.DocumentName);
            $('#lblAttachementRFPDownload').text(policy.ActualFileName);
             
            $('#txtPriority').val(policy.Priority);
            $("#txtAttachmentfile").val(policy.ActualFileName);

            $('#hdnUploadActualFileName').val(policy.ActualFileName);
                        $('#hdnUploadNewFileName').val(policy.NewFileName);
                        $('#hdnUploadFileUrl').val(policy.FileUrl);
          
        }
    });
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

function SubCateGredOut()
{ 
  $('#lblAttachementRFPDownload').hide();
    $('#txtPolicy').val('');
    $('#hdnMasterTableId').val('');
    $('#txtAttachment').val('');
    $('#attachmentName').val('');
    $('#txtPriority').val('');
    $('#sptxtPolicy').hide();
    $('#sptxtAttachmentfile').hide();
    $('#sptxtPriority').hide();
    $('#txtAttachmentfile').hide();
    $('#btnDownload1').hide();
    $('#txtAttachmentfile').val('');
}
 



