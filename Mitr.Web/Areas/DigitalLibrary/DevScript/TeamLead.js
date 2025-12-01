$(document).ready(function ()
{
   BindSubcategory();
    var obj = {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.Project,
        manualTableId: 0
    }
    LoadMasterDropdown('ddlCategory', obj, 'Select', false);

    var obj1 = {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.Employee,
        manualTableId: 0
    }
    LoadMasterDropdown('ddlEdOd', obj1, 'Select', false);
    
    
});
function SaveSubCategory()
{
    if (checkValidationOnSubmit('Mandatory') == true)
    {
        var ID = 0;
        if ($('#hdnMasterTableId').val() != "") {
            ID = $('#hdnMasterTableId').val();
        }
       
        var obj = {
            Id: ID,
            ProjectId: $('#ddlCategory').val(),
            UserId: loggedinUserid,
            EoUserId: $('#ddlEdOd').val()            
        }
        CommonAjaxMethod(virtualPath + 'DigitalLibrary/SaveProjectTeamLead', obj, 'POST', function (response)
        {
            BindSubcategory();
            ClearFormControl();       
            $('#sc').modal('hide')
        });
    }
}
function ClearFormControl() {
    $('#ddlCategory').val('0');
    $('#ddlEdOd').val('0');
    $('#hdnMasterTableId').val('');
    $('#hdnCateId').val('');

    $('#select2-ddlCategory-container').text('Select');
    $('#select2-ddlEdOd-container').text('Select');
   
    $("#dvProjectList").children().prop('disabled', false);
}

function BindSubcategory()
{
    CommonAjaxMethod(virtualPath + 'DigitalLibrary/BindProjectTeamLead', null, 'GET', function (response) {


        $('#table').DataTable({
            "processing": true, // for show progress bar    
            "destroy": true,
            "data": response.data.data.Table,

            "columns": [
                { "data": "RowNum" },
                { "data": "ProjectName" },
                { "data": "Name" },
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
                            strReturn = "<a title='Click to DeActivate' data-toggle='tooltip' data-original-title='Click to DeActivate' class='AIsActive'  onclick='Activate(" + row.ID + ")' ><i class='fa fa-check-circle checkgreen' aria-hidden='true'  ></i> </a><a title='Edit' data-toggle='modal' data-target='#sc'  onclick='EditSubCate(" + row.ID + "," + row.Project_id + ")' ><i class='fas fa-edit checkgreen' aria-hidden='true' ></i> </a> ";
                        }
                        if (row.Status == "Deactive") {
                            strReturn = "<a title='Click to Activate' data-toggle='tooltip' data-original-title='Click to Activate' class='AIsActive' onclick='Activate(" + row.ID + ")' ><i class='fa fa-times-circle crossred' aria-hidden='true'  ></i> </a><a title='Edit' data-toggle='modal' data-target='#sc'  onclick='EditSubCate(" + row.ID + "," + row.Project_id + ")' ><i class='fas fa-edit checkgreen' aria-hidden='true' ></i> </a> ";
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
    CommonAjaxMethod(virtualPath + 'DigitalLibrary/GetProjectTeamLeadById', { id: id }, 'GET', function (response)
    {        
        var data = response.data.data.Table;
        $('#hdnMasterTableId').val(id);    
        $('#hdnCateId').val(CategoryID);     
        $('#ddlCategory').val(data[0].Project_id).trigger('change');        
        $('#ddlEdOd').val(data[0].TeamleadId).trigger('change');
       
        $("#dvProjectList").children().prop('disabled', true);
    });
   
    
}
function Activate(id)
{
  
    var x = confirm("Do you want to change the status of this record?");

    if (x)
    { 
        var obj = {
            Id: id,
            IsActive: 1,
            IPAdress: $('#hdnIP').val()    
        }
        CommonAjaxMethod(virtualPath + 'DigitalLibrary/SaveProjectTeamLead', obj, 'POST', function (response) {
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

