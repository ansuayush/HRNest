$(document).ready(function ()
{
    BindSubcategory();
    var obj = {
        ParentId: 0,
        masterTableType: DropDownTypeEnum.Category,
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
        var catId = '';
        if ($('#hdnMasterTableId').val() != '') {
            catId = $('#hdnCateId').val();
        }
        else {
            catId = $('#ddlCategory').val();
        }
        var obj = {
            ID: catId,
            SubCategory: $('#txtSubCate').val(),
            TableType: DropDownTypeEnum.Category,
            Code: "SC",
            MasterTableId: $('#hdnMasterTableId').val(),
            UserId: loggedinUserid,
            IPAddress: $('#hdnIP').val() ? "" : " "   
        }
        CommonAjaxMethod(virtualPath + 'DigitalLibrary/SaveSubCategory', obj, 'POST', function (response)
        {
            BindSubcategory();
            ClearFormControl();       
            $('#sc').modal('hide')
        });
    }
}
function ClearFormControl() {
    $('#ddlCategory').val('0');
    $('#txtSubCate').val('');
    $('#hdnMasterTableId').val('');
    $('#hdnCateId').val('');
    $('#txtCate').val(''); 
    $('#select2-ddlCategory-container').text('Choose Your Category');

}

function BindSubcategory()
{
    CommonAjaxMethod(virtualPath + 'DigitalLibrary/BindCategory', null, 'GET', function (response) {


        $('#table').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": response.data.data.Table,

            "columns": [
                { "data": "RowNum" },
                { "data": "Category" },
                { "data": "SubCategory" },

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

                { "data": "IPAddress" },
                { "data": "Status" },
                 {
                    "orderable": false,
                    data: null, render: function (data, type, row) {
                        var strReturn = "";
                        //Click to DeActivate                     
                        //Click to Activate                      
                        if (row.Status == "Active") {
                            strReturn = "<a title='Click to DeActivate' data-toggle='tooltip' data-original-title='Click to DeActivate' class='AIsActive'  onclick='Activate(" + row.ID + ")' ><i class='fa fa-check-circle checkgreen' aria-hidden='true'  ></i> </a><a title='Edit' data-toggle='modal' data-target='#sc'  onclick='EditSubCate(" + row.ID + "," + row.CategoryID + ")' ><i class='fas fa-edit checkgreen' aria-hidden='true' ></i> </a> ";
                        }
                        if (row.Status == "Deactive") {
                            strReturn = "<a title='Click to Activate' data-toggle='tooltip' data-original-title='Click to Activate' class='AIsActive' onclick='Activate(" + row.ID + ")' ><i class='fa fa-times-circle crossred' aria-hidden='true'  ></i> </a><a title='Edit' data-toggle='modal' data-target='#sc'  onclick='EditSubCate(" + row.ID + "," + row.CategoryID + ")' ><i class='fas fa-edit checkgreen' aria-hidden='true' ></i> </a> ";
                        }

                        return strReturn;
                    }
                }

            ]
        });

        
    });
   
}

function EditSubCate(id, CategoryID)
{
    ClearFormControl();
    CommonAjaxMethod(virtualPath + 'DigitalLibrary/GetSubCategory', { id: id }, 'GET', function (response)
    {  
        $('#txtCate').show();
        var data = response.data.data.Table;
        $('#hdnMasterTableId').val(id);    
        $('#hdnCateId').val(CategoryID);     
        $('#ddlCategory').val(CategoryID); 
        $('.selection').hide();    
        $('#txtCate').val(data[0].Category);
        $('#txtSubCate').val(data[0].SubCategory);
        
    });
   
    
}
function Activate(id)
{
  
    var x = confirm("Do you want to change the status of this record?");

    if (x)
    { 
        var obj = {
            ID: 0,
            SubCategory: '',
            TableType: '',
            Code: '',
            MasterTableId: id,
            IsActive: 1,
            IPAddress: $('#hdnIP').val()?"":" "    
        }
        CommonAjaxMethod(virtualPath + 'DigitalLibrary/SaveSubCategory', obj, 'POST', function (response) {
            BindSubcategory();
            ClearFormControl();
        });
    }
}
function SubCateGredOut()
{
    $('#txtCate').hide();
    ClearFormControl();
    $('.selection').show();    
 
}

