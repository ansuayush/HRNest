$(document).ready(function ()
{
    BindVenderType();
   
    
});
function SaveVenderType()
{
    if (checkValidationOnSubmit('Mandatory') == true)
    {
        var radios1 = document.getElementById('chkIsNGO');



        var catId = '';        
        var obj = {
            ID: catId,
            VendorType: $('#txtSubCate').val(),
            TableType: DropDownTypeEnum.ProcurementVenderType,
            Code: radios1.checked==true?'Yes':'No',
            MasterTableId: $('#hdnMasterTableId').val(),
            UserId: loggedinUserid,
            IPAddress: $('#hdnIP').val() ? "" : " "   
        }
        CommonAjaxMethod(virtualPath + 'VenderType/SaveVenderType', obj, 'POST', function (response)
        {
            BindVenderType();
            ClearFormControl();       
            $('#sc').modal('hide')
        });
    }
}
function ClearFormControl() {
   
    $('#txtSubCate').val('');
    $('#hdnMasterTableId').val('');
    $('#hdnCateId').val('');
    $('.chkIsNGO').prop('checked', true);
  

}

function BindVenderType()
{
    CommonAjaxMethod(virtualPath + 'VenderType/BindVenderType', null, 'GET', function (response) {


        $('#table').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": response.data.data.Table,

            "columns": [
                { "data": "RowNum" },             
                { "data": "SubCategory" },
                { "data": "ValueCode" },

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
    CommonAjaxMethod(virtualPath + 'VenderType/GetVenderType', { id: id }, 'GET', function (response)
    {  
       
        var data = response.data.data.Table;
        $('#hdnMasterTableId').val(id);    
        $('#hdnCateId').val(CategoryID); 
        $('#txtSubCate').val(data[0].SubCategory);
        if (data[0].ValueCode == 'Yes')
        {
             
            document.getElementById('chkIsNGO').checked = true;
        }
        else
        {
             
            document.getElementById('chkIsNGO').checked = false;
        }
        
    });
   
    
}
function Activate(id, no) {

    var actionType = "Activate";

    if (no == "1") {
        actionType = "Deactivate";
    } 
    

    CommonAjaxMethod(virtualPath + 'VenderType/GetVenderType', { id: id }, 'GET', function (response) {

        var data = response.data.data.Table;
       
        ConfirmMsgBox("Are you sure want to " + actionType + " " + data[0].SubCategory + ".", '', function () {
            var obj = {
                ID: 0,
                VendorType: '',
                TableType: '',
                Code: '',
                MasterTableId: id,
                IsActive: 1,
                IPAddress: $('#hdnIP').val() ? "" : " "
            }
            CommonAjaxMethod(virtualPath + 'VenderType/SaveVenderType', obj, 'POST', function (response) {
                BindVenderType();
                ClearFormControl();
            });
        })
    });
}


