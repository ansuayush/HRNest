$(document).ready(function ()
{
     

    LoadMasterDropdown('ddlCategory', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.Employee,
        manualTableId: 0
    }, 'Select', false);

    BindProcurApprovalAuthority();
    
    
});
function SaveProcurApprovalAuthority()
{
    if (checkValidationOnSubmit('Mandatory') == true)
    {
        

            
        var catId = 0;
        if ($('#hdnMasterTableId').val() != '')
        {
            catId = $('#hdnMasterTableId').val();
        }
          
        var obj = {
            ID: catId,
            EmpId: $('#ddlCategory').val(),            
            ContractType: $('#ddlAgreement').val(),   
            Action:1
           
        }
        CommonAjaxMethod(virtualPath + 'ProcurApprovalAuthority/SaveAuthorisedSignatoryMaster', obj, 'POST', function (response)
        {
            BindProcurApprovalAuthority();
            ClearFormControl();       
            $('#sc').modal('hide')
        });
    }
}
function ClearFormControl() {
   
    $('#ddlCategory').val('Select'),
        
        $('#ddlAgreement').val('Select'),
  $('#hdnMasterTableId').val('');
  $('#hdnCateId').val('');
    $('#txtCate').val('');
    $('#select2-ddlCategory-container').text('Select');
    $('#select2-ddlAgreement-container').text('Select');
    
   // $('#ddlCategory').val(data[0].ApproverAuthId).trigger('change');
    $("#ddlCategory").prop("disabled", false);
    $("#ddlAgreement").prop("disabled", false);
}

function BindProcurApprovalAuthority()
{
    CommonAjaxMethod(virtualPath + 'ProcurApprovalAuthority/BindAuthorisedSignatoryMaster', null, 'GET', function (response) {


        $('#table').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": response.data.data.Table,
            "paging": false,
            "info": false,
            "columns": [
                { "data": "RowNum" },  
                { "data": "ContractType" },
                { "data": "Approver" } ,

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
    CommonAjaxMethod(virtualPath + 'ProcurApprovalAuthority/GetAuthorisedSignatoryMaster', { id: id }, 'GET', function (response)
    {  
       
        var data = response.data.data.Table;
       // $('#txtCate').show();
        $('#hdnMasterTableId').val(id);    
        $('#hdnCateId').val(CategoryID);         
        $('#ddlCategory').val(data[0].EmpId).trigger('change'); 
        $('#ddlAgreement').val(data[0].ContractType).trigger('change'); 
      //  $('.selection').hide();
       // $('#txtCate').val(data[0].Approver);
        $("#ddlAgreement").prop("disabled", true);
    });
   
    
}
function Activate(id, no) {

    var actionType = "Activate";

    if (no == "1") {
        actionType = "Deactivate";
    }

    

     
    CommonAjaxMethod(virtualPath + 'ProcurApprovalAuthority/GetProcurApprovalAuthority', { id: id }, 'GET', function (response) {

        var data = response.data.data.Table;
        ConfirmMsgBox("Are you sure want to " + actionType + " " + data[0].Approver + ".", '', function () {
            var dd = new Date();
            var obj = {
                ID: id,                
                UserId: loggedinUserid,
                IPAddress: $('#hdnIP').val() ? "" : " ",
                IsActive: 1
            }
            CommonAjaxMethod(virtualPath + 'ProcurApprovalAuthority/SaveProcurApprovalAuthority', obj, 'POST', function (response) {
                BindProcurApprovalAuthority();
                ClearFormControl();
            });
        })
    });
}


