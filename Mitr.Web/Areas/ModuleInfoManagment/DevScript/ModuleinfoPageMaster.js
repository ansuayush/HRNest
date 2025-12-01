$(document).ready(function () {
    BindPagemodule();
    var obj = {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: 70,
        manualTableId: 0
    }
    LoadMasterDropdown('topicname', obj, 'Select', false);
});

function fetchModuleName(ctrl) {
    CommonAjaxMethod(virtualPath + 'ModuleInfoRequest/GetTopicModuleById', { ID: ctrl.value }, 'GET', function(response) {
        var data = response.data.data.Table[0];
        $('#txtModule').val(data.ModuleName || '');
        $('#hdfModule').val(data.ModuleId);
    });
}
function SavePageMaster() {
    if (checkValidationOnSubmit('Mandatory') == true) {
        ShowLoadingDialog();
        setTimeout(function () {
           var selectedModuleId = $('#topicname').val();
        var obj = {
            ID: $('#hdnMasterTableId').val(),
            Topic_id: selectedModuleId,
            Module_id: $('#hdfModule').val(),
            PageName: $('#txtPage').val(),
            Video_link: $('#txtLink').val(),
            Description: CKEDITOR.instances.txtTemplate.getData(), 
            Priority: $('#txtPriority').val(),
                TableType: 100,
                Code: "CC",
                ParentID: 0,
                MasterTableId: $('#hdnMasterTableId').val(),
                IsActive: 0
            };
           CommonAjaxMethod(virtualPath + 'ModuleInfoRequest/SavePageMaster', obj, 'POST', function(response) {
            if (response.ValidationInput == 1) {
                    BindPagemodule();
                    ClearFormControl();
                    $('#sc').modal('hide');
                }
           
                CloseLoadingDialog();
            });
        }, 1000); // 100 milliseconds delay

    } else {
        CloseLoadingDialog();
    }
}

function ClearFormControl() {
    $('#hdnMasterTableId').val('');
    $('#topicname').val('').trigger('change'); // Ensure dropdown is reset
    $('#txtModule').val('');
    $('#txtPage').val('');
    $('#txtLink').val('');
    CKEDITOR.instances.txtTemplate.setData(''); // Clear CKEditor content
    $('#txtPriority').val('');
}

function BindPagemodule() {
   var tableId = '#Pagetable';
    CommonAjaxMethod(virtualPath + 'ModuleInfoRequest/GetPageMaster', null, 'GET', function (response) {
         var table  = $('#Pagetable').DataTable({
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
                  "render": function (data, type, row) {
                      return '<label style="text-align: right; display: inline-block;padding-top: 6px; width: 100%;">' + row.RowNum + '</label>';
                                }
                  },
                { "data": "ModuleName" },
                { "data": "TopicName" },
                { "data": "PageName" },
               {
                  "orderable": true,
                  "data": null,
                  "render": function (data, type, row) {
                      return '<label style="text-align: right; display: inline-block; width: 100%;">' + row.Priority + '</label>';
                                }
                  },
                {
                    "orderable": true,
                    data: null,
                    render: function (data, type, row) {
                        return "<label>" + getdatetimewithoutJson(row.createdat) + "</label>";
                    }
                },
                {
                    "orderable": true,
                    data: null,
                    render: function (data, type, row) {
                        return "<label>" + getdatetimewithoutJson(row.modifiedat) + "</label>";
                    }
                },
                { "data": "Status" },
                {
                    "orderable": false,
                    "data": null,
                    "render": function (data, type, row) {
                        var strReturn = "";
                        if (row.Status === "Active") {
                            strReturn = "<a title='Click to Deactivate' data-toggle='tooltip' data-original-title='Click to Deactivate' class='AIsActive' onclick='Activate(" + row.ID + ", 1)'><i class='fa fa-check-circle checkgreen' aria-hidden='true'></i></a><a title='Edit' data-toggle='modal' data-target='#sc' onclick='EditPagemodules(" + row.ID + ")'><i class='fas fa-edit checkgreen' aria-hidden='true'></i></a>";
                        } else if (row.Status === "Deactive") {
                            strReturn = "<a title='Click to Activate' data-toggle='tooltip' data-original-title='Click to Activate' class='AIsActive' onclick='Activate(" + row.ID + ", 0)'><i class='fa fa-times-circle crossred' aria-hidden='true'></i></a><a title='Edit' data-toggle='modal' data-target='#sc' onclick='EditPagemodules(" + row.ID + ")'><i class='fas fa-edit checkgreen' aria-hidden='true'></i></a>";
                        }
                        return strReturn;
                    }
                }
            ]
               ,
             "initComplete": function () {
                initCompleteCallback(tableId.substring(1)); // Remove the leading # from tableId
            }
        });
        table.destroy();

                // Initialize tooltips for the initial set of rows
                $('[data-toggle="tooltip"]').tooltip();

                // Re-initialize tooltips every time the table is redrawn
                table.on('draw.dt', function () {
                    $('[data-toggle="tooltip"]').tooltip();
                });

                DatatableScriptsWithColumnSearch(tableId.substring(1), table);
            });
}

function EditPagemodules(ID) {
    ClearFormControl();
    CommonAjaxMethod(virtualPath + 'ModuleInfoRequest/GetPageMasterByID', { ID: ID }, 'GET', function(response) {
        var data = response.data.data.Table[0];
        $('#hdnMasterTableId').val(ID);
        $("#topicname").val(data.Topic_id).trigger('change'); 
        $('#txtModule').val(data.ModuleName);
        $('#txtPage').val(data.PageName);
        $('#txtLink').val(data.Video_link);
        CKEDITOR.instances.txtTemplate.setData(data.Description); 
        $('#txtPriority').val(data.Priority);
    });
}

function Activate(ID, no) {
    var actionType = "Activate";
    if (no == "1") {
        actionType = "Deactivate";
    }
    CommonAjaxMethod(virtualPath + 'ModuleInfoRequest/GetPageMasterByID', { ID: ID }, 'GET', function(response) {
        var data = response.data.data.Table;
        ConfirmMsgBox("Are you sure want to " + actionType + " " + data[0].PageName + ".", '', function () {
            var obj = {
                ID: data[0].ID,
                 Module_id: data[0].Module_id,
                Topic_id: data[0].Topic_id,
                PageName: data[0].PageName,
                Video_link: data[0].Video_link,
                Description: data[0].Description,
                Priority: data[0].Priority,
                IsActive: 1,
            }
            CommonAjaxMethod(virtualPath + 'ModuleInfoRequest/SavePageMaster', obj, 'POST', function(response) {
                BindPagemodule();
                ClearFormControl();
            });
        })
    });
}

function SubCateGredOut() {
    $('#topicname').hide();
    ClearFormControl();
    $('.selection').show();
}
   