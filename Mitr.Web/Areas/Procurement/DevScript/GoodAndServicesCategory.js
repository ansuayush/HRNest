$(document).ready(function ()
{
    BindGoodAndServicesCategory();
   
    
});
function SaveGoodAndServicesCategory()
{
    if (checkValidationOnSubmit('Mandatory') == true)
    {
        var catId = '';        
        var obj = {
            ID: catId,
            Category: $('#txtSubCate').val(),
            TableType: DropDownTypeEnum.ProcurementGoodAndServicesCategory,
            Code: "SC",
            MasterTableId: $('#hdnMasterTableId').val(),
            UserId: loggedinUserid,
            IPAddress: $('#hdnIP').val() ? "" : " "   
        }
        CommonAjaxMethod(virtualPath + 'GoodAndServicesCategory/SaveGoodAndServicesCategory', obj, 'POST', function (response)
        {
            BindGoodAndServicesCategory();
            ClearFormControl();       
            $('#sc').modal('hide')
        });
    }
}
function ClearFormControl() {
   
    $('#txtSubCate').val('');
    $('#hdnMasterTableId').val('');
    $('#hdnCateId').val('');
  
  

}

function BindGoodAndServicesCategory()
{
    CommonAjaxMethod(virtualPath + 'GoodAndServicesCategory/BindGoodAndServicesCategory', null, 'GET', function (response) {


        $('#table').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": response.data.data.Table,
            "paging": false,
            "info": false,
            "columns": [
                { "data": "RowNum" },             
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
                            strReturn = "<a title='Click to DeActivate' data-toggle='tooltip' data-original-title='Click to DeActivate' class='AIsActive'  onclick='Activate(" + row.ID + ",1)' ><i class='fa fa-check-circle checkgreen' aria-hidden='true'  ></i> </a><a title='Edit' data-toggle='modal' data-target='#sc'  onclick='EditSubCate(" + row.ID + "," + row.CategoryID + ")' ><i class='fas fa-edit checkgreen' aria-hidden='true' ></i> </a> ";
                        }
                        if (row.Status == "Deactive") {
                            strReturn = "<a title='Click to Activate' data-toggle='tooltip' data-original-title='Click to Activate' class='AIsActive' onclick='Activate(" + row.ID + ",2)' ><i class='fa fa-times-circle crossred' aria-hidden='true'  ></i> </a><a title='Edit' data-toggle='modal' data-target='#sc'  onclick='EditSubCate(" + row.ID + "," + row.CategoryID + ")' ><i class='fas fa-edit checkgreen' aria-hidden='true' ></i> </a> ";
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
    CommonAjaxMethod(virtualPath + 'GoodAndServicesCategory/GetGoodAndServicesCategory', { id: id }, 'GET', function (response)
    {  
       
        var data = response.data.data.Table;
        $('#hdnMasterTableId').val(id);    
        $('#hdnCateId').val(CategoryID); 
        $('#txtSubCate').val(data[0].SubCategory);
        
    });
   
    
}
function Activate(id, no)
{
  
   
    var actionType = "Activate";

    if (no == "1") {
        actionType = "Deactivate";
    }
    CommonAjaxMethod(virtualPath + 'GoodAndServicesCategory/GetGoodAndServicesCategory', { id: id }, 'GET', function (response) {

        var data = response.data.data.Table;

        ConfirmMsgBox("Are you sure want to " + actionType + " " + data[0].SubCategory + ".", '', function () {
            var obj = {
                ID: 0,
                Category: '',
                TableType: '',
                Code: '',
                MasterTableId: id,
                IsActive: 1,
                IPAddress: $('#hdnIP').val() ? "" : " "
            }
            CommonAjaxMethod(virtualPath + 'GoodAndServicesCategory/SaveGoodAndServicesCategory', obj, 'POST', function (response) {
                BindGoodAndServicesCategory();
                ClearFormControl();
            });
        })
    });
}


