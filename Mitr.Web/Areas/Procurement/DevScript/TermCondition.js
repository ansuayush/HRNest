$(document).ready(function ()
{
     

    BindProcurApprovalAuthority();
    
    
});
function SaveProcurApprovalAuthority()
{
    var desc = CKEDITOR.instances['txtTemplate'].getData();
    if (desc != '') {
        var catId = 0;
        if ($('#hdnMasterTableId').val() != '') {
            catId = $('#hdnMasterTableId').val();
        }
        var obj =
        {
            ID: catId,
            Body: CKEDITOR.instances['txtTemplate'].getData()

        }
        CommonAjaxMethod(virtualPath + 'ProcurApprovalAuthority/SaveTemplateMaster', obj, 'POST', function (response) {
            BindProcurApprovalAuthority();
            ClearFormControl();
            $('#sc').modal('hide')
        });
    }
    else {
        $('#sptxtTemplate').show();
        
    }
}
function ClearFormControl() {
    $('#sptxtTemplate').hide();
  $('#ddlCategory').val(''),
  $('#cntsEffectiveDate').val(''),     
  $('#hdnMasterTableId').val('');
  $('#hdnCateId').val('');
    $('#txtCate').val('');
     
    $("#ddlCategory").prop("disabled", false);
}

function BindProcurApprovalAuthority()
{
    CommonAjaxMethod(virtualPath + 'ProcurApprovalAuthority/BindTemplate', null, 'GET', function (response) {


        $('#table').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": response.data.data.Table,
            "paging": false,
            "info": false,
            "columns": [
                        
                { "data": "ConstractType" } ,
                 {
                    "orderable": false,
                    data: null, render: function (data, type, row) {
                                          
                        
                            strReturn = "<a title='Edit' data-toggle='modal' data-target='#sc'  onclick='EditSubCate(" + row.ID + "," + row.CategoryID + ")' ><i class='fas fa-edit checkgreen' aria-hidden='true' ></i> </a> ";
                     
                   

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
    CommonAjaxMethod(virtualPath + 'ProcurApprovalAuthority/GetTemplate', { id: id }, 'GET', function (response)
    {  
       
        var data = response.data.data.Table;
       // $('#txtCate').show();
        $('#hdnMasterTableId').val(id);    
        $('#hdnCateId').val(CategoryID);         
        $('#ddlCategory').val(data[0].ConstractType).trigger('change'); 
        $("#ddlCategory").prop("disabled", true);
        CKEDITOR.instances['txtTemplate'].setData(data[0].Body);
    });
   
    
}
 

