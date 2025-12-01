$(document).ready(function ()
{
    BindSubcategory();
    var obj = {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.MasterThematicArea,
        manualTableId: 0
    }
    LoadMasterDropdown('ddlThematic', obj,'Select',false);
    
});
function SaveSubCategory()
{
    if (checkValidationOnSubmit('Mandatory') == true)
    {
        var catId = '';
        if ($('#hdnMasterTableId').val() != '')
        {
            catId = $('#hdnThematicId').val();
        }
        else {
            catId = $('#ddlThematic').val();
        }
        var obj = {
            Id: $('#hdnMasterTableId').val(),
            Tag: $('#txtTag').val(),             
            Thematic_id: catId,
            UserId: loggedinUserid,
            IPAdress: $('#hdnIP').val()    
        }
        CommonAjaxMethod(virtualPath + 'DigitalLibrary/SaveTag', obj, 'POST', function (response)
        {
            BindSubcategory();
            ClearFormControl();
            $('#sc').modal('hide')
            
        });
    }
}
function ClearFormControl() {
    $('#ddlThematic').val('0');
    $('#txtTag').val('');
    $('#hdnMasterTableId').val('');
    $('#hdnThematicId').val('');
    $('#txtthematic').val(''); 
    $('#select2-ddlThematic-container').text('Select');

}

function BindSubcategory()
{
    CommonAjaxMethod(virtualPath + 'DigitalLibrary/BindTagMaster', null, 'GET', function (response)
    {       
     
        $('#table').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "info": false,
            "lengthChange": false,
            "bFilter": false,
            "data": response.data.data.Table,

            "columns": [
                { "data": "RowNum" },
                { "data": "thematicarea_code" },
                { "data": "Tag" },
                
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
                    data: null, render: function (data, type, row)
                    {
                        var strReturn = "";                        
                       //Click to DeActivate                     
                       //Click to Activate                      
                        if(row.Status == "Active") {
                            strReturn = "<a title='Click to DeActivate' data-toggle='tooltip' data-original-title='Click to DeActivate' class='AIsActive'  onclick='Activate(" + row.id + ")' ><i class='fa fa-check-circle checkgreen' aria-hidden='true'  ></i> </a><a title='Edit' data-toggle='modal' data-target='#sc'  onclick='EditSubCate(" + row.id + "," + row.Thematic_id + ")' ><i class='fas fa-edit checkgreen' aria-hidden='true' ></i> </a> ";
                        }
                        if (row.Status == "Deactive") {
                            strReturn = "<a title='Click to Activate' data-toggle='tooltip' data-original-title='Click to Activate' class='AIsActive' onclick='Activate(" + row.id + ")' ><i class='fa fa-times-circle crossred' aria-hidden='true'  ></i> </a><a title='Edit' data-toggle='modal' data-target='#sc'  onclick='EditSubCate(" + row.id + "," + row.Thematic_id + ")' ><i class='fas fa-edit checkgreen' aria-hidden='true' ></i> </a> ";
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
    CommonAjaxMethod(virtualPath + 'DigitalLibrary/GetTagMasterById', { id: id }, 'GET', function (response)
    {  
        $('#txtthematic').show();
        var data = response.data.data.Table;
        $('#hdnMasterTableId').val(id);    
        $('#hdnThematicId').val(CategoryID);    
        $('#ddlThematic').val(CategoryID); 
        $('.selection').hide();    
        $('#txtthematic').val(data[0].thematicarea_code);
        $('#txtTag').val(data[0].Tag);
        
    });
   
    
}
function Activate(id)
{
    
    var x = confirm("Do you want to change the status of this record?");

    if (x)
    { 
        var obj = {
            Id: id,
            SubCategory: '',
            TableType: '',
            Code: '',
            Thematic_id: id,
            IsActive: 1,
            IPAdress: $('#hdnIP').val()    
        }
        CommonAjaxMethod(virtualPath + 'DigitalLibrary/SaveTag', obj, 'POST', function (response) {
            BindSubcategory();
            ClearFormControl();
        });
    }
}
function SubCateGredOut()
{
    $('#txtthematic').hide();
    ClearFormControl();
    $('.selection').show();    
 
}

