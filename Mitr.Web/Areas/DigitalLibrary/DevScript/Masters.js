$(document).ready(function () {

  

    var obj = {
        ParentId: 0,
        masterTableType: DropDownTypeEnum.Category,
        isMasterTableType: true,
        isManualTable: false,
        manualTable: 0,
        manualTableId:0
    }
    LoadMasterDropdown('ddlMasterTable', obj, 'Select', false);

    var obj = {
        ParentId: 0,
        masterTableType: DropDownTypeEnum.Category,
        isMasterTableType: false,
        isManualTable: false,
        manualTable: 0,
        manualTableId: 0
    }
    LoadMasterDropdown('ddlParent', obj, 'Select', false);
    BindMaster();

});
function UpdateData()
{

    if (checkValidationOnSubmit('UpdateMandate') == true) {
        var obj = {
            TableType: '0',
            ParentID: '0',
            MasterValue: $('#txtEditValue').val(),
            Code: $('#txtEditCode').val(),
            MasterTableId: $('#hdnMasterTableId').val(),
            UserId: loggedinUserid,
            IsActive: 0

        }

        CommonAjaxMethod(virtualPath + 'CommonMethod/SaveMaster', obj, 'POST', function (response) {
            BindSubcategory();
            ClearFormControl();
        });

    }   
}

function SaveData() {
    if (checkValidationOnSubmit('Mandatory') == true)
    {
        var obj = {
                TableType: $('#ddlMasterTable').val(),
                ParentID: $('#ddlParent').val(),
                MasterValue: $('#txtValue').val(),
                Code: $('#txtCode').val(),
                MasterTableId: $('#hdnMasterTableId').val(),
                IsActive: 0

            }
   
        CommonAjaxMethod(virtualPath + 'CommonMethod/SaveMaster', obj, 'POST', function (response) {
            BindMaster();
            ClearFormControl();
        });
       
    }
}
function ClearFormControl() {
    $('#ddlMasterTable').val('0');
    $('#ddlParent').val('0');
    $('#txtCode').val('');
    $('#txtValue').val('');  
    $('#hdnMasterTableId').val(''); 
    $('#select2-ddlMasterTable-container').text('Select');
    $('#select2-ddlParent-container').text('Select');
    $('#txtEditValue').val('');
    $('#txtEditCode').val('');  
  

}

function BindMaster() {
    CommonAjaxMethod(virtualPath + 'CommonMethod/BindMaster', null, 'GET', function (response) {
        $("#gridContainer").dxDataGrid({
            dataSource: response.data.data.Table,
            allowColumnReordering: false,
            filterRow: { visible: true },
            showBorders: true,

            searchPanel: {
                visible: false
            },
            paging: {
                pageSize: 10
            },
            groupPanel: {
                visible: false
            },
            editing: {
                mode: "form",
                allowUpdating: false
            },

            columns: [
                { dataField: "ID", caption: "ID", visible: false },
                { dataField: "CategoryID", caption: "CategoryID", visible: false },                
                { dataField: "MasterTableName", caption: "MasterTableType" },
                { dataField: "SubCategory", caption: " Parent" },
                { dataField: "Category", caption: "MasterTableValue" },

                { dataField: "ValueCode", caption: "Code" },

              
                {
                    caption: "Status",
                    alignment: 'center',
                    cellTemplate: function (container, options) {
                        $('<a />').addClass('dx-link')
                            .text(options.data.Status)
                            .on('dxclick', function () {

                                Activate(options.data.ID, options.data.Status);

                            }).appendTo(container);
                    }
                },
                {
                    caption: "Action",
                    alignment: 'center',
                    cellTemplate: function (container, options) {
                        $('<a  data-toggle="modal" data-target="#sc" />').addClass('dx-link')
                            .text('Edit')
                            .on('dxclick', function () {

                                EditMaster(options.data.ID);

                            }).appendTo(container);
                    }
                }


            ]
        });
    });

}

function EditMaster(id) {
    ClearFormControl();
    CommonAjaxMethod(virtualPath + 'CommonMethod/GetMaster', { id: id }, 'GET', function (response)
    {      
        var data = response.data.data.Table;
        $('#hdnMasterTableId').val(id);       
        $('#txtEditCode').val(data[0].ValueCode);
        $('#txtEditValue').val(data[0].Category);
        $('#txtMasterTableType').val(data[0].MasterTableName);
        $('#txtParent').val(data[0].SubCategory);
    
        
    });


}
function Activate(id, act) {
    var tag;
    if (act == 'Active') {
        tag = 'Deactivate';
    }
    else
        tag = 'Activet';
    var x = confirm("Do you want to " + tag + " this record?");

    if (x) {
        var obj = {
            TableType:'0',
            ParentID: '0',
            MasterValue: '',
            Code: '',
            MasterTableId: id,
            IsActive:1

        }
        CommonAjaxMethod(virtualPath + 'CommonMethod/SaveMaster', obj, 'POST', function (response) {
            BindSubcategory();
            ClearFormControl();
        });
    }
}

