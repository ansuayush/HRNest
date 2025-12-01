$(document).ready(function ()
{


    ShowandCloseLoader();

    BindSubcategory();
    var obj = {
        ParentId: 0,
        masterTableType: 100,
        isMasterTableType: false,
        isManualTable: false,
        manualTable: 0,
        manualTableId: 0
    }
    LoadMasterDropdown('ddlCategory', obj,'Choose Your Category',false);
    
});
function SaveSubCategory()
{
    if (checkValidationOnSubmit('Mandatory') == true)
    {
        var catId = $('#hdnCateId').val();
        
        ShowLoadingDialog();
        setTimeout(function () {
            var obj = {
                MasterValue: $('#txtSubCate').val(),
                TableType: 101,
                Code: "CC",
                ParentID: $('#ddlCategory').val(),
                MasterTableId: $('#hdnMasterTableId').val(),
                IsActive: 0

            }
            CommonAjaxMethod(virtualPath + 'ComplianceTransaction/SaveSubCategory', obj, 'POST', function (response) {
                if (response.ValidationInput == 1) {
                    BindSubcategory();
                    ClearFormControl();
                    $('#sc').modal('hide');
                }            
                CloseLoadingDialog();
            });
        }, 1000);
    }
}
function ClearFormControl() {
    $('#ddlCategory').val('0');
    $('#txtSubCate').val('');
    $('#hdnMasterTableId').val('');
    $('#hdnCateId').val('');
    $('#txtCate').val(''); 
    $('#select2-ddlCategory-container').text('Choose Your Category');
    $('#sptxtSubCate').hide();

}


function BindSubcategory()
{
    CommonAjaxMethod(virtualPath + 'ComplianceTransaction/BindSubCategory', null, 'GET', function (response) {


        var table = $('#tableSubCategoey').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": response.data.data.Table,

            "columns": [
                { "data": "RowNum" },               
                { "data": "Parent" },
                { "data": "MasterValue" },

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
                    data: null, render: function (data, type, row) {
                        var strReturn = "";
                        //Click to DeActivate                     
                        //Click to Activate                      
                        if (row.Status == "Active") {
                            strReturn = "<a title='Click to Deactivate' data-toggle='tooltip' data-original-title='Click to DeActivate' class='AIsActive'  onclick='confirmmsgActi(" + row.ID + ")' ><i class='fa fa-check-circle checkgreen' aria-hidden='true'  ></i> </a><a title='Edit' data-toggle='modal' data-target='#sc'  onclick='EditSubCate(" + row.ID + "," + row.ParentId + ")' ><i class='fas fa-edit checkgreen' aria-hidden='true' ></i> </a> ";
                        }
                        if (row.Status == "Deactive") {
                            strReturn = "<a title='Click to Activate' data-toggle='tooltip' data-original-title='Click to Activate' class='AIsActive' onclick='confirmmsgDeacti(" + row.ID + ")' ><i class='fa fa-times-circle crossred' aria-hidden='true'  ></i> </a><a title='Edit' data-toggle='modal' data-target='#sc'  onclick='EditSubCate(" + row.ID + "," + row.ParentId + ")' ><i class='fas fa-edit checkgreen' aria-hidden='true' ></i> </a> ";
                        }

                        return strReturn;
                    }
                }

            ],
            "initComplete": function () {
                initCompleteCallback('tableSubCategoey'); // Remove the leading # from tableId
            }
        });

        table.destroy();
        DatatableScriptsWithColumnSearch('tableSubCategoey', table);

    });
   
}

function EditSubCate(id, CategoryID)
{
    ClearFormControl();
    CommonAjaxMethod(virtualPath + 'ComplianceTransaction/GetSubCategory', { id: id }, 'GET', function (response)
    {  
       // $('#txtCate').show();
        var data = response.data.data.Table;
        $('#hdnMasterTableId').val(id);    
        $('#hdnCateId').val(CategoryID);     
        
        $("#ddlCategory").val(CategoryID).trigger('change');
      //  $('.selection').hide();    
       // $('#txtCate').val(data[0].Category);
        $('#txtSubCate').val(data[0].MasterValue);
        
    });
   
    
}
function confirmmsgActi(id) {

    CommonAjaxMethod(virtualPath + 'ComplianceTransaction/GetSubCategory', { id: id }, 'GET', function (response)
    {  
        var data = response.data.data.Table;

        $("#record").html(data[0].MasterValue);
        $("#acti").html('Deactivate');
        $("#hiddenCateId").val(id);
        $("#confirmmsg").modal('show');
    });
}


function confirmmsgDeacti(id) {


    CommonAjaxMethod(virtualPath + 'ComplianceTransaction/GetSubCategory', { id: id }, 'GET', function (response) {  

        var data = response.data.data.Table;

        $("#record").html(data[0].MasterValue);
        $("#acti").html('Activate');
        $("#hiddenCateId").val(id);
        $("#confirmmsg").modal('show');

    });
}
function Activate() {
    ShowLoadingDialog();
    setTimeout(function () {
        var obj = {
            ID: 0,
            SubCategory: '',
            TableType: '',
            Code: '',
            MasterTableId: $("#hiddenCateId").val(),
            IsActive: 1,
            IPAddress: $('#hdnIP').val() ? "" : " "
        }
        CommonAjaxMethod(virtualPath + 'ComplianceTransaction/SaveCategory', obj, 'POST', function (response) {
            BindSubcategory();
            CloseLoadingDialog();
            $("#confirmmsg").modal('hide');
        });
    }, 1000);
}
function SubCateGredOut()
{
    $('#txtCate').hide();
    ClearFormControl();
    $('.selection').show();    
 
}

