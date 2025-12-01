$(document).ready(function ()
{
    ShowandCloseLoader();
    BindSubcategory();     
    
});


function SaveSubCategory() {
    if (checkValidationOnSubmit('Mandatory') == true) {
        ShowLoadingDialog();

        // Add a slight delay to ensure the loading dialog renders
        setTimeout(function () {
            var obj = {
                MasterValue: $('#txtSubCate').val(),
                TableType: 100,
                Code: "CC",
                ParentID: 0,
                MasterTableId: $('#hdnMasterTableId').val(),
                IsActive: 0
            };

            CommonAjaxMethod(virtualPath + 'ComplianceTransaction/SaveCategory', obj, 'POST', function (response) {
                if (response.ValidationInput == 1) {
                    BindSubcategory();
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
   
    $('#txtSubCate').val('');
    $('#hdnMasterTableId').val('');
    $('#sptxtSubCate').hide();
    
    

}

function BindSubcategory() {
    CommonAjaxMethod(virtualPath + 'ComplianceTransaction/BindCategory', null, 'GET', function (response) {
        var tableId = '#tableCategory';

     
        var table = $(tableId).DataTable({
            "processing": true, // for showing progress bar
            "destroy": true,
            "data": response.data.data.Table,
            "stateSave": true, // Enable state saving
            "columns": [
                { "data": "RowNum" },
                { "data": "MasterValue" },
                {
                    "orderable": true,
                    "data": null,
                    "render": function (data, type, row) {
                        return "<label>" + getdatetimewithoutJson(row.createdat) + "</label>";
                    }
                },
                {
                    "orderable": true,
                    "data": null,
                    "render": function (data, type, row) {
                        return "<label>" + getdatetimewithoutJson(row.modifiedat) + "</label>";
                    }
                },
                { "data": "Status" },
                {
                    "orderable": false,
                    "data": null,
                    "render": function (data, type, row) {
                        var strReturn = "";
                        if (row.Status == "Active") {
                            strReturn = "<a title='Click to DeActivate' data-toggle='tooltip' data-original-title='Click to DeActivate' class='AIsActive' onclick='confirmmsgActi(" + row.ID + ")'><i class='fa fa-check-circle checkgreen' aria-hidden='true'></i></a><a title='Edit' data-toggle='modal' data-target='#sc' onclick='EditSubCate(" + row.ID + ")'><i class='fas fa-edit checkgreen' aria-hidden='true'></i></a>";
                        } else if (row.Status == "Deactive") {
                            strReturn = "<a title='Click to Activate' data-toggle='tooltip' data-original-title='Click to Activate' class='AIsActive' onclick='confirmmsgDeacti(" + row.ID + ")'><i class='fa fa-times-circle crossred' aria-hidden='true'></i></a><a title='Edit' data-toggle='modal' data-target='#sc' onclick='EditSubCate(" + row.ID + ")'><i class='fas fa-edit checkgreen' aria-hidden='true'></i></a>";
                        }
                        return strReturn;
                    }
                }
            ],
            "initComplete": function () {
                initCompleteCallback(tableId.substring(1)); // Remove the leading # from tableId
            }
        });

        // Reinitialize column search functionality
        table.destroy();
        DatatableScriptsWithColumnSearch(tableId.substring(1), table);
    });
}





function EditSubCate(id)
{
    ClearFormControl();
    CommonAjaxMethod(virtualPath + 'ComplianceTransaction/GetCategory', { id: id }, 'GET', function (response)
    {  
        
        var data = response.data.data.Table;
        $('#hdnMasterTableId').val(id);    
             
        $('#txtSubCate').val(data[0].MasterValue);
        
    });
   
    
}

function confirmmsgActi(id) {

    CommonAjaxMethod(virtualPath + 'ComplianceTransaction/GetCategory', { id: id }, 'GET', function (response) {

        var data = response.data.data.Table;
        $("#record").html(data[0].MasterValue);
        $("#acti").html('Deactivate');
        $("#hiddenCateId").val(id);
        $("#confirmmsg").modal('show');


    });

   
}
 

function confirmmsgDeacti(id) {

    CommonAjaxMethod(virtualPath + 'ComplianceTransaction/GetCategory', { id: id }, 'GET', function (response) {

        var data = response.data.data.Table;
        $("#record").html(data[0].MasterValue);
        $("#acti").html('Activate');
        $("#hiddenCateId").val(id);
        $("#confirmmsg").modal('show');


    });

 
}
function Activate()
{
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
 ;
    $('#txtCate').hide();
    ClearFormControl();
    
 
}

