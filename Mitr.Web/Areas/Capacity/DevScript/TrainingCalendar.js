$(document).ready(function () {
    document.getElementById('hddUserId').Value = loggedinUserid;
    document.getElementById('hddAddUserId').Value = loggedinUserid;
    $(function () {
        $('.datepicker').datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: "dd/mm/yy",
            yearRange: "-90:+10"
        });

    });
    //ClearFormControl();
    BindTrainingCalendar();
    //LoadMasterDropdown('ddlTrainingType',
    //    {
    //        ParentId: 0,
    //        masterTableType: 0,
    //        isMasterTableType: false,
    //        isManualTable: true,
    //        manualTable: 26,
    //        manualTableId: 0
    //    }, 'Select', false);
    
});
var table = "";
$(document).ready(function () {
     table = $('#tblTrainingCalendar').DataTable();
    // #myInput is a <input type="text"> element
    $('#txtsearch').on('keyup', function () {
        table = $('#tblTrainingCalendar').DataTable();
        table.search(this.value).draw();
    });
});
function TrainingType() {
    trainingTypeId = $('#ddlTrainingType').val();
    if (trainingTypeId != undefined) {
        CommonAjaxMethod(virtualPath + 'CapacityRequest/GetTrainingDesc', { id: trainingTypeId }, 'GET', function (response) {
            var data = response.data.data.Table;
            $('#txtTrainingDesc').val(data[0].TrainingDesc);
        });
    }
}
function AddTrainingType() {
    trainingTypeId = $('#ddlAddTrainingType').val();
    if (trainingTypeId != undefined) {
        CommonAjaxMethod(virtualPath + 'CapacityRequest/GetTrainingDesc', { id: trainingTypeId }, 'GET', function (response) {
            var data = response.data.data.Table;
            $('#txtAddTrainingDesc').val(data[0].TrainingDesc);
        });
    }
}
function TrainingMode() {
    if ($('#ddlTrainingMode').val() == 143) {
        document.getElementById('divLocation').style.display = 'none';
        document.getElementById('divLink').style.display = 'block';
        $('#txtlocation').attr({ 'class': 'form-control ' });
        $('#txtlink').attr({ 'class': 'form-control MandatoryEdit ' });
    }
    if ($('#ddlTrainingMode').val() == 145) {
        document.getElementById('divLocation').style.display = 'block';
        document.getElementById('divLink').style.display = 'none';
        $('#txtlocation').attr({ 'class': 'form-control MandatoryEdit ' });
        $('#txtlink').attr({ 'class': 'form-control ' });
    }
}
function AddTrainingMode() {
    if ($('#ddlAddTrainingMode').val() == 143) {
        document.getElementById('divAddLocation').style.display = 'none';
        document.getElementById('divAddLink').style.display = 'block';
        $('#txtAddlocation').attr({ 'class': 'form-control ' });
        $('#txtAddlink').attr({ 'class': 'form-control MandatoryAdd ' });
    }
    if ($('#ddlAddTrainingMode').val() == 145) {
        document.getElementById('divAddLocation').style.display = 'block';
        document.getElementById('divAddLink').style.display = 'none';

        $('#txtAddlocation').attr({ 'class': 'form-control MandatoryAdd ' });
        $('#txtAddlink').attr({ 'class': 'form-control ' });
    }
}


function BindTrainingDesc(trainingTypeId) {
    if (trainingTypeId != undefined) {
        CommonAjaxMethod(virtualPath + 'CapacityRequest/GetTrainingDesc', { id: trainingTypeId }, 'GET', function (response) {
            var data = response.data.data.Table;
            $('#txtTrainingDesc').val(data[0].TrainingDesc);
        });
    }
}

function BindTrainingCalendar() {

    CommonAjaxMethod(virtualPath + 'CapacityRequest/BindTrainingCalendar', null, 'GET', function (response) {
     table=   response.data.data.Table

        $('#tblTrainingCalendar').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": response.data.data.Table,
            "paging": true,
            "info": false,
            columnDefs: [
                {
                    targets: 6,
                    visible: false
                }
            ],
            "columns": [
                { "data": "RowNum" },
                { "data": "TrainingTypeName" },
                { "data": "TrainingName" },
                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {
                        return "<label>" + ChangeDateFormatToddMMYYYWithSlace(row.TentativeFromDate) + "-" + ChangeDateFormatToddMMYYYWithSlace(row.TentativeToDate)+ "</label>";
                    }
                },
                { "data": "TrainingMode" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {
                        var strReturn = "";
                        //Click to DeActivate                     
                        //Click to Activate                      
                        if (row.Status == "Active") {
                            strReturn = "<a title='Click to DeActivate' data-toggle='tooltip' data-original-title='Click to DeActivate' class='AIsActive'  onclick='Activate(" + row.id + ",1)' ><i class='fa fa-check-circle checkgreen' aria-hidden='true'  ></i> </a><a title='Edit' data-toggle='modal' data-target='#tc'  onclick='EditSubCate(" + row.id + ")' ><i class='fas fa-edit checkgreen' aria-hidden='true' ></i> </a> ";
                        }
                        if (row.Status == "Deactive") {
                            strReturn = "<a title='Click to Activate' data-toggle='tooltip' data-original-title='Click to Activate' class='AIsActive' onclick='Activate(" + row.id + ",2)' ><i class='fa fa-times-circle crossred' aria-hidden='true'  ></i> </a><a title='Edit' data-toggle='modal' data-target='#tc'  onclick='EditSubCate(" + row.id + ")' ><i class='fas fa-edit checkgreen' aria-hidden='true' ></i> </a> ";
                        }

                        return strReturn;
                    }
                },
                { "data": "Status" },

            ]
        });


    });

}
function SaveTrainingCalendar() {
    if (checkValidationOnSubmit('MandatoryEdit') == true) {

        var isValid = true;
        var currentDate = Date.parse(new Date().toLocaleDateString());
        //var currentDate = date.getDate() + '/' + (date.getMonth() + 1) + '/' + date.getFullYear(); 
        var SelectedFromDate = Date.parse(ChangeDateFormatWithSlace($('#txtFromDate').val()));
        var SelectedToDate = Date.parse(ChangeDateFormatWithSlace($('#txtToDate').val()));

        if ((SelectedFromDate < currentDate) || (SelectedToDate < currentDate)) {
            isValid = false;
            FailToaster("'From-Date OR To-Date can not be less than current Date'");
        }

        if (Date.parse(ChangeDateFormatWithSlace($('#txtFromDate').val())) > Date.parse(ChangeDateFormatWithSlace($('#txtToDate').val()))) {
            isValid = false;
            FailToaster("'From-Date' can not be greater than 'To-Date'");

        }
        var SetvalueforPhysicalOrVirtual = "";
        if ($('#ddlTrainingMode').val() == 145) {
             SetvalueforPhysicalOrVirtual = $('#txtlocation').val();
        }
        if ($('#ddlTrainingMode').val() == 143) {
             SetvalueforPhysicalOrVirtual = $('#txtlink').val();
        }
        
        if (isValid) {
            var obj = {
                id: $('#hdnIdToEditRecord').val(),
                TrainingTypeid: $('#ddlTrainingType').val(),
                TrainingDesc: $('#txtTrainingDesc').val(),
                TrainingName: $('#txtTrainingName').val(),
                TentativeFromDate: ChangeDateFormatWithSlace($('#txtFromDate').val()),
                TentativeToDate: ChangeDateFormatWithSlace($('#txtToDate').val()),
                TrainingMode: $('#ddlTrainingMode option:selected').text(),
                TrainingModeID: $('#ddlTrainingMode').val(),
                TraininglinkorLocation: SetvalueforPhysicalOrVirtual
            }
            CommonAjaxMethod(virtualPath + 'CapacityRequest/SaveTrainingCalendarDetails', obj, 'POST', function (response) {
                BindTrainingCalendar();
                ClearFormControl();
                var url = "/Capacity/TrainingCalendar";
                window.location.href = url;
            });
        }
    }
}
function AddSaveTrainingCalendar() {
    if (checkValidationOnSubmit('MandatoryAdd') == true) {

        var isValid = true;
        var currentDate = Date.parse(new Date().toLocaleDateString());
        //var currentDate = date.getDate() + '/' + (date.getMonth() + 1) + '/' + date.getFullYear(); 
        var SelectedFromDate = Date.parse(ChangeDateFormatWithSlace($('#txtAddFromDate').val()));
        var SelectedToDate = Date.parse(ChangeDateFormatWithSlace($('#txtAddToDate').val()));

        if ((SelectedFromDate < currentDate) || (SelectedToDate < currentDate)) {
            isValid = false;
            FailToaster("'From-Date OR To-Date can not be less than current Date'");
        }

        if (Date.parse(ChangeDateFormatWithSlace($('#txtAddFromDate').val())) > Date.parse(ChangeDateFormatWithSlace($('#txtAddToDate').val()))) {
            isValid = false;
            FailToaster("'From-Date' can not be greater than 'To-Date'");

        }
        var SetvalueforPhysicalOrVirtual = "";
        if ($('#ddlAddTrainingMode').val() == 145) {           
             SetvalueforPhysicalOrVirtual = $('#txtAddlocation').val();
        }
        if ($('#ddlAddTrainingMode').val() == 143) {          
             SetvalueforPhysicalOrVirtual = $('#txtAddlink').val();
        }

        if (isValid) {
            var obj = {
                id: $('#hdnIdToEditRecord').val(),
                TrainingTypeid: $('#ddlAddTrainingType').val(),
                TrainingDesc: $('#txtAddTrainingDesc').val(),
                TrainingName: $('#txtAddTrainingName').val(),
                TentativeFromDate: ChangeDateFormatWithSlace($('#txtAddFromDate').val()),
                TentativeToDate: ChangeDateFormatWithSlace($('#txtAddToDate').val()),
                TrainingMode: $('#ddlAddTrainingMode option:selected').text(),
                TrainingModeID: $('#ddlAddTrainingMode').val()  ,
                TraininglinkorLocation: SetvalueforPhysicalOrVirtual
            }
            CommonAjaxMethod(virtualPath + 'CapacityRequest/SaveTrainingCalendarDetails', obj, 'POST', function (response) {
                BindTrainingCalendar();
                AddTrainingCalendar();
                var url = "/Capacity/TrainingCalendar";
                window.location.href = url;
            });
        }
    }
}
function ClearFormControl() {
    LoadMasterDropdown('ddlTrainingType',
        {
            ParentId: 0,
            masterTableType: 0,
            isMasterTableType: false,
            isManualTable: true,
            manualTable: 26,
            manualTableId: 0
        }, 'Select', false);
    LoadMasterDropdown('ddlTrainingMode', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: 27,
        manualTableId: 0
    }, 'Select', false);
   
    $('#ddlTrainingType').val();
    $('#ddlTrainingMode').val();
    $('#txtlink').val();
    $('#txtlocation').val();
    
    document.getElementById('divLocation').style.display = 'none';
    document.getElementById('divLink').style.display = 'none';

    $('#txtTrainingDesc').val("");
    $('#txtTrainingName').val("");
    $('#txtFromDate').val("");
    $('#txtToDate').val("");
    $('#txtlink').val("");
    $('#txtlocation').val("");
    $('#divAddLink').val("");
    document.getElementById('lblTrainingNameConfirmation').innerText = "";
}
function EditSubCate(id)
{
    ClearFormControl();
    CommonAjaxMethod(virtualPath + 'CapacityRequest/GetTrainingCalendarById', { id: id }, 'GET', function (response) {
        var data = response.data.data.Table;
        $('#hdnIdToEditRecord').val(data[0].id);
        $('#ddlTrainingType').val(data[0].TrainingTypeid).trigger("change");
        $('#txtTrainingDesc').val(data[0].TrainingDesc);
        $('#txtTrainingName').val(data[0].TrainingName);
        ////Date.parse($('[id$=fromDate]').val(data[0].TentativeFromDate));
        ////Date.parse($('[id$=toDate]').val(data[0].TentativeToDate));

        $("#txtFromDate").val(ChangeDateFormatToddMMYYYWithSlace(data[0].TentativeFromDate));
        $("#txtToDate").val(ChangeDateFormatToddMMYYYWithSlace(data[0].TentativeToDate));

        $('#ddlTrainingMode').val(data[0].TrainingModeID).trigger("change");
        if (data[0].TrainingMode == 'Physical') {
            $('#txtlocation').val(data[0].TraininglinkorLocation);
        }
        else {
            $('#txtlink').val(data[0].TraininglinkorLocation);
        }
    });
}
function Activate(id, no) {

    var actionType = "Activate";

    if (no == "1") {
        actionType = "Deactivate";
    }


    CommonAjaxMethod(virtualPath + 'CapacityRequest/GetTrainingCalendarById', { id: id }, 'GET', function (response) {

        var data = response.data.data.Table;

        ConfirmMsgBox("Are you sure want to " + actionType + ".", '', function () {
            var obj = {
                id: data[0].id,
                TrainingTypeid: data[0].TrainingTypeid,
                TrainingName: data[0].TrainingName,
                IsActive: 1,
                TentativeFromDate: data[0].TentativeFromDate,
                TentativeToDate: data[0].TentativeToDate
            }
            CommonAjaxMethod(virtualPath + 'CapacityRequest/SaveTrainingCalendarDetails', obj, 'POST', function (response) {
                BindTrainingCalendar();
                ClearFormControl();
            });
        })
    });
}

function CheckTrainingNameDetailsExists(obj) {
    var ObjURL = $(obj).closest("div").find(".hdnRecordExit");
    if ($(obj).val() != "") {
        var dataObject = JSON.stringify({
            'ID': loggedinUserid,
            'Doctype': $(ObjURL).attr("tag-value"),
            'Value': $(obj).val(),

        });
        $.ajax({
            url: "/CommonAjax/CapacityTrainingNameRecordExistJSon",
            type: "Post",
            contentType: 'application/json',
            data: dataObject,
            success: function (data) {
                if (!data.Status) {

                    $(obj).val('');
                    $(obj).closest("div").find('.field-validation-valid').html(data.SuccessMessage).removeClass('color-green').addClass('text-danger')
                    document.getElementById('lblTrainingNameConfirmation').innerHTML = data.SuccessMessage;
                    $('#sptxtTrainingName').addClass("text-danger");
                }
                else {
                    $(obj).closest("div").find('.field-validation-valid').html(data.SuccessMessage).removeClass('text-danger').addClass('color-green');
                    document.getElementById('lblTrainingNameConfirmation').innerHTML = data.SuccessMessage;
                    $('#sptxtTrainingName').addClass("color-green");
                }
            },
            error: function (er) {
                alert(er);
            }
        });
    }
}
function AddCheckTrainingNameDetailsExists(obj) {
    var ObjURL = $(obj).closest("div").find(".hdnRecordExit");
    if ($(obj).val() != "") {
        var dataObject = JSON.stringify({
            'ID': loggedinUserid,
            'Doctype': $(ObjURL).attr("tag-value"),
            'Value': $(obj).val(),

        });
        $.ajax({
            url: "/CommonAjax/CapacityTrainingNameRecordExistJSon",
            type: "Post",
            contentType: 'application/json',
            data: dataObject,
            success: function (data) {
                if (!data.Status) {

                    $(obj).val('');
                    $(obj).closest("div").find('.field-validation-valid').html(data.SuccessMessage).removeClass('color-green').addClass('text-danger')
                    document.getElementById('lblAddTrainingNameConfirmation').innerHTML = data.SuccessMessage;
                    $('#sptxtAddTrainingName').addClass("text-danger");
                }
                else {
                    $(obj).closest("div").find('.field-validation-valid').html(data.SuccessMessage).removeClass('text-danger').addClass('color-green');
                    document.getElementById('lblAddTrainingNameConfirmation').innerHTML = data.SuccessMessage;
                    $('#sptxtAddTrainingName').addClass("color-green");
                }
            },
            error: function (er) {
                alert(er);
            }
        });
    }
}

function AddTrainingCalendar() {
    LoadMasterDropdown('ddlAddTrainingMode', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: 27,
        manualTableId: 0
    }, 'Select', false);
    LoadMasterDropdown('ddlAddTrainingType',
        {
            ParentId: 0,
            masterTableType: 0,
            isMasterTableType: false,
            isManualTable: true,
            manualTable: 26,
            manualTableId: 0
        }, 'Select', false);
    document.getElementById('divAddLocation').style.display = 'none';
    document.getElementById('divAddLink').style.display = 'none';

    $('#txtAddTrainingDesc').val("");
    $('#txtAddTrainingName').val("");
    $('#txtAddFromDate').val("");
    $('#txtAddToDate').val("");
    $('#txtAddlink').val("");
    $('#txtAddlocation').val("");
    $('#divAddLink').val("");
    document.getElementById('lblAddTrainingNameConfirmation').innerText="";
    $('#ModelAddTrainingCalendar').modal('show');
}

