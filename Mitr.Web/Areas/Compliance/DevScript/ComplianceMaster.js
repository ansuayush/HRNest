$(document).ready(function ()
{

    BindCompliance();    
    BindLegacy();
    
});


function SaveSubCategory()
{
    if (checkValidationOnSubmit('Mandatory') == true)
    {
        var catId = 0;
        if ($('#hdnMasterTableId').val() != '')
        {
            catId = $('#hdnCateId').val();
        }       
        var obj = {
            ID: catId,
            SubCategory: $('#txtSubCate').val(),
            TableType: 18,
            Code: "SC",
            MasterTableId: $('#hdnMasterTableId').val()           
            
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
   
    $('#txtSubCate').val('');
    $('#hdnMasterTableId').val('');
    $('#hdnCateId').val('');
    

}

function BindCompliance()
{
    var tableId = '#tableCompliance';
    CommonAjaxMethod(virtualPath + 'ComplianceTransaction/GetComplianceMaster', null, 'GET', function (response) {

        
        var table = $('#tableCompliance').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": response.data.data.Table,
            "stateSave": true, // Enable state saving
            "columns": [
                { "data": "RowNumber" },

                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {
                        return "<label>" + formatNumber(row.Doc_No) + "</label>";
                    }
                },
                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {
                        return "<label>" + ChangeDateFormatToddMMYYY(row.DocDate) + "</label>";
                    }
                },

                { "data": "Category" },

                { "data": "SubCategory" },
               
                { "data": "ComplianceName" },
                { "data": "Doer" },
                { "data": "RiskType" },
                { "data": "Frequency" },
                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {
                        return "<label>" + ChangeDateFormatToddMMYYY(row.EffectiveDate) + "</label>";
                    }
                },
                {
                    "orderable": false,
                    "data": null,
                    "render": function (data, type, row) {
                        var strReturn = "";
                        if (row.Status == 1) {
                            strReturn = "<div class='text-center' ><a title='Click to Deactivate' data-toggle='tooltip' data-original-title='Click to Deactivate' class='AIsActive' onclick='confirmmsgActi(\"" + row.Category.toString() + "\"," + row.ID + ", 2, 0)'><i class='fa fa-check-circle checkgreen' aria-hidden='true'></i></a></div>";

                        } else if (row.Status == 0) {
                            strReturn = "<div class='text-center' ><a title='Click to Activate' data-toggle='tooltip' data-original-title='Click to Activate' class='AIsActive' onclick='confirmmsgDeacti(" + row.ID + ", 2)'><i class='fa fa-times-circle crossred' aria-hidden='true'></i></a></div>";
                        }
                        return strReturn;
                    }
                },
                {
                    "orderable": false,
                    "data": null,
                    "render": function (data, type, row) {

                        var strReturn = '<div class="text-center" ><a  href="#"  onclick="EditCompliance(' + row.ID + ')"  > <i class="fas fa-edit green-clr" data-toggle="tooltip" title="" data-original-title="Update"></i></a><span class="divline">|</span><a onclick="confirmmsgDelete(\'' + row.Category.toString() + '\',' + row.ID + ', 3)"  ><i class="fa fa-times red-clr" data-toggle="tooltip" title="" data-original-title="Cancel"></i></a><span class="divline">|</span><a href="#" onclick="ViewCompliance(' + row.ID + ')" ><i class="fas fa-eye" data-toggle="tooltip" title="" data-original-title="View"></i></a></div>';
                                            
                       
                        return strReturn;
                    }
                }

            ],
             "initComplete": function () {
                initCompleteCallback(tableId.substring(1)); // Remove the leading # from tableId
            }
        });

        table.destroy();

        // Initialize tooltips for the initial set of rows
        $('[data-toggle="tooltip"]').tooltip();

        // Re-initialize tooltips every time the table is redrawn
        table.on('draw.dt', function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        DatatableScriptsWithColumnSearch(tableId.substring(1), table);
    });
   
}

function BindLegacy() {
    var tableId = '#tableLegacy';
    CommonAjaxMethod(virtualPath + 'ComplianceTransaction/GetComplianceMaster', null, 'GET', function (response) {

        var table = $('#tableLegacy').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": response.data.data.Table1,
            "stateSave": true, // Enable state saving
            "columns": [
                { "data": "RowNumber" },

                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {
                        return "<label>" + formatNumber(row.Doc_No) + "</label>";
                    }
                },
                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {
                        return "<label>" + ChangeDateFormatToddMMYYY(row.DocDate) + "</label>";
                    }
                },

                { "data": "Category" },

                { "data": "SubCategory" },

                { "data": "ComplianceName" },
                { "data": "Doer" },
                { "data": "RiskType" },
                { "data": "Frequency" },
                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {
                        return "<label>" + ChangeDateFormatToddMMYYY(row.EffectiveDate) + "</label>";
                    }
                },
                {
                    "orderable": false,
                    "data": null,
                    "render": function (data, type, row) {
                        var strReturn = "";
                        if (row.isdeleted == 1) {
                            strReturn = "<div class='text-center'>Cancelled</div>";

                        } else  {
                            strReturn = '<div class="text-center" ><a data-toggle="tooltip" data-original-title="Click to Activate" data-placement="left" list="masterall" op="0" class="AIsActive" onclick="confirmmsgActi(\'' + row.Category.toString() + '\',' + row.ID + ', 2, 1)"> <i class="fa fa-times-circle crossred" aria-hidden="true"></i></a></div>';

                        }
                        return strReturn;
                    }
                },
                {
                    "orderable": false,
                    "data": null,
                    "render": function (data, type, row) {

                        var strReturn = '<div class="text-center" ><a href="#" onclick="ViewCompliance(' + row.ID + ')" ><i class="fas fa-eye" data-toggle="tooltip" title="" data-original-title="View"></i></a></div>';


                        return strReturn;
                    }
                }

            ],
            "initComplete": function () {
                initCompleteCallback(tableId.substring(1)); // Remove the leading # from tableId
            }
        });

        table.destroy();

        // Initialize tooltips for the initial set of rows
        $('[data-toggle="tooltip"]').tooltip();

        // Re-initialize tooltips every time the table is redrawn
        table.on('draw.dt', function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        DatatableScriptsWithColumnSearch(tableId.substring(1), table);
    });

}

var isActi;
function confirmmsgActi(cate,id, Type, Acti) {


     isActi = {
        id: id,
        Type: Type,
        IsActive: Acti
    }

        if (Acti == 0) {
            $("#acti").html('Deactivate');
        }
        else {
            $("#acti").html('Activate')
        }
    
      //  $("#record").html(cate);
        $("#hiddenCateId").val(id);
        $("#confirmmsg").modal('show');



}

var isDel;
function confirmmsgDelete(cate, id, Type) {


    isDel = {
        id: id,
        Type: Type,
    }

    

   // $("#cancelRecord").html(cate);
    $("#cancel").modal('show');



}
function ChangeStatus() {

    var ComplianceModel = {
        ComplianceMasterModel: isActi,
   
        }

    CommonAjaxMethod(virtualPath + 'ComplianceTransaction/SaveComplianceMaster', { objComplianceMaster: ComplianceModel }, 'POST', function (response) {
        if (response.ValidationInput == 1) {
            HrefCompMaster();
        }

    });// 100 milliseconds delay
}

function ChangeDelete() {

    var ComplianceModel = {
        ComplianceMasterModel: isDel,

    }

    CommonAjaxMethod(virtualPath + 'ComplianceTransaction/SaveComplianceMaster', { objComplianceMaster: ComplianceModel }, 'POST', function (response) {
        if (response.ValidationInput == 1) {
            HrefCompMaster();
        }

    });// 100 milliseconds delay
}

function deletecomp() {

}


function confirmmsgDeacti(id) {

    CommonAjaxMethod(virtualPath + 'ComplianceTransaction/GetCategory', { id: id }, 'GET', function (response) {

        var data = response.data.data.Table;
       // $("#record").html(data[0].MasterValue);
        $("#acti").html('Activate');
        $("#hiddenCateId").val(id);
        $("#confirmmsg").modal('show');


    });


}


function SubCateGredOut()
{
    $('#txtCate').hide();
    ClearFormControl();
    
 
}

function formatNumber(value) {
    // Check if the number is an integer
    if (Number.isInteger(value)) {
        return value.toFixed(1); // Append .0 to integer values
    }
    return value.toString(); // Leave decimal values as they are
}

