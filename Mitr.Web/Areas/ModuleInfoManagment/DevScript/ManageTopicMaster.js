 $(document).ready(function () {
       BindTopicmodule();
        var obj = {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: 71,
        manualTableId: 0
    }
    LoadMasterDropdown('DDLModulename', obj,'Select',false);
 });
function SaveTopicMaster() {
    if (checkValidationOnSubmit('Mandatory') == true) {
        ShowLoadingDialog();
        setTimeout(function () {
            var selectedModuleId = $('#DDLModulename').val();
         var obj = {
            ID: $('#hdnMasterTableId').val(),
            ModuleId: selectedModuleId,
            TopicName: $('#txttopic').val(),
            Priority: $('#txtPriority').val(),
                TableType: 100,
                Code: "CC",
                ParentID: 0,
                MasterTableId: $('#hdnMasterTableId').val(),
                IsActive: 0
            };
           CommonAjaxMethod(virtualPath + 'ModuleInfoRequest/SaveTopicMaster', obj, 'POST', function(response) {
            if (response.ValidationInput == 1) {
                    BindTopicmodule();
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
    $('#txttopic').val('');
    $('#hdnMasterTableId').val('');
    $('#DDLModulename').val('');
     $('#DDLModulename').val('').trigger('change'); 
    $('#selectedModuleId').val('');
    $('#hdnMasterTableId').val('');
    $('#txtPriority').val('');
    $('#selectedModuleId').val('');
}

function BindTopicmodule()
{ 
var tableId = '#Topictable';
      CommonAjaxMethod(virtualPath + 'ModuleInfoRequest/GetTopicMaster', null, 'GET', function (response)
 {
  var table  = $('#Topictable').DataTable({
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
                {
                  "orderable": true,
                  "data": null,
                  "render": function (data, type, row) {
                      return '<label style="text-align: right; display: inline-block; width: 100%;">' + row.Priority + '</label>';
                                }
                  },
                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {
                        return "<label>" + getdatetimewithoutJson(row.createdat) + "</label>";
                    }
                },
                
                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {
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
                            strReturn = "<a title='Click to Deactivate' data-toggle='tooltip' data-original-title='Click to Deactivate' class='AIsActive' onclick='Activate(" + row.ID + ", 1)'><i class='fa fa-check-circle checkgreen' aria-hidden='true'></i></a><a title='Edit' data-toggle='modal' data-target='#sc' onclick='EditTopicmodules(" + row.ID + ")'><i class='fas fa-edit checkgreen' aria-hidden='true'></i></a>";
                        } else if (row.Status === "Deactive") {
                            strReturn = "<a title='Click to Activate' data-toggle='tooltip' data-original-title='Click to Activate' class='AIsActive' onclick='Activate(" + row.ID + ", 0)'><i class='fa fa-times-circle crossred' aria-hidden='true'></i></a><a title='Edit' data-toggle='modal' data-target='#sc' onclick='EditTopicmodules(" + row.ID + ")'><i class='fas fa-edit checkgreen' aria-hidden='true'></i></a>";
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

function EditTopicmodules(ID) {
       ClearFormControl();
       CommonAjaxMethod(virtualPath + 'ModuleInfoRequest/GetTopicModuleById', { ID: ID }, 'GET', function(response) {
       var data = response.data.data.Table[0];
            $('#hdnMasterTableId').val(ID);
            $("#DDLModulename").val(data.ModuleId).trigger('change'); 
            $('#txttopic').val(data.TopicName);
            $('#txtPriority').val(data.Priority);
    });
}
function Activate(ID, no) {
    var actionType = "Activate";
    if (no == "1") {
        actionType = "Deactivate";
    }
    CommonAjaxMethod(virtualPath + 'ModuleInfoRequest/GetTopicModuleById', { ID: ID }, 'GET', function(response) {
        var data = response.data.data.Table;

        ConfirmMsgBox("Are you sure want to " + actionType + " " + data[0].TopicName + ".", '', function () {
            var obj = {
                ID: data[0].ID,
                ModuleId: data[0].ModuleId,
                TopicName: data[0].TopicName,
                Priority: data[0].Priority,
                isActive: 1,
               
            }
            CommonAjaxMethod(virtualPath + 'ModuleInfoRequest/SaveTopicMaster', obj, 'POST', function(response) {
                BindTopicmodule();
                ClearFormControl();
            });
        })
    });
}


function SubCateGredOut()
{
    $('#DDLModulename').hide();
    ClearFormControl();
    
}