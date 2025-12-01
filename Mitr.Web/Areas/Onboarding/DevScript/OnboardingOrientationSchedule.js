$(document).ready(function () {
    $(function () {
        $('.datepicker').datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: "dd-mm-yy",
            yearRange: "-90:+10"
        });

    });

    LoadMasterDropdown('ddlNameOfMembers', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: 17,
        manualTableId: 0
    }, 'Select', false);
    var obj = {
        ParentId: 0,
        masterTableType: 21,
        isMasterTableType: false,
        isManualTable: false,
        manualTable: 0,
        manualTableId: 0
    }
    LoadMasterDropdown('ddlMode', obj, 'Select', false);
    BindOrientationScheduleRequest();
});
var OrientationSchedulesArray = [];
var OrientationSchedulesArrayId = 0;
var candodate_Status = false;
function BindOrientationScheduleRequest() {
    $('.applyselect').select2("destroy");
    var $tableBody = $('#tblOrientationSchedule').find("tbody"),
        $trLast = $tableBody.find("tr:last"),
        $trNew = $trLast.clone();

    CommonAjaxMethod(virtualPath + 'OnboardingRequest/BindonboardingProcessData', { CandidateId: CandidateId, inputData: 5 }, 'GET', function (response) {
        var mArray = response.data.data.Table;
        var statusRecord = response.data.data.Table1;
        if (statusRecord.length > 0) {
            if (statusRecord[0].Status == "14") {
                candodate_Status = true;
            }
            if (statusRecord[0].Status == "15") {
                candodate_Status = true;
            }
            if (statusRecord[0].Status == "16") {
                candodate_Status = true;
            }
            if (candodate_Status == true) {
                $("#btnOrientationSchedulesSave").prop('disabled', true);
                $("#btnOrientationSchedulesSubmitted").prop('disabled', true);
                $("#btnOrientationSchedulesAddRow").prop('disabled', true);
            }
            else {
                $("#btnOrientationSchedulesSave").prop('disabled', false);
                $("#btnOrientationSchedulesSubmitted").prop('disabled', false);
                $("#btnOrientationSchedulesAddRow").prop('disabled', false);
            }
        }
        var ddlMode = document.getElementById("ddlNameOfMembers");
        if (mArray.length > 0) {
            sourceCode = mArray[0].StatusCode;
            OrientationSchedulesArray = mArray;
            BindOrientationSchedulesArray();
        }
    });
}
function BindOrientationSchedulesArray() {
    $('#tblOrientationSchedule1').html('');
    var newtbblData1 = '<table id="tblOrientationSchedulePaging" class="table table-striped m-0" >' +
        ' <thead>' +
        ' <tr>' +
        ' <th  width="33">S.No</th>' +
        ' <th >Name of Member</th>' +
        ' <th width="100">Date</th>' +
        ' <th width="100">Time</th>' +
        ' <th width="100">Place</th>' +
        ' <th width="150">Purpose</th>' +
        ' <th width="100">Mode</th>' +
        ' <th width="50" class="text-center" >Action</th>' +
        ' <th style="display: none" class="td-50 text-center">Feedback</th>' +
        ' <th style="display: none" class="td-50 text-center">ModeId</th>' +
        ' </tr>' +
        ' </thead>';
    var html1 = "</table>";

    var tableData = "";
    for (let i = 0; i < OrientationSchedulesArray.length; i++) {
        var cData = OrientationSchedulesArray[i].OriDate == null ? "" : OrientationSchedulesArray[i].OriDate;
        cData = ChangeDateFormatToddMMYYY(cData);
        var modeId = OrientationSchedulesArray[i].Mode == "FaceToFace" ? 141 : OrientationSchedulesArray[i].Mode == "Virtual" ? 140 : '';
        var newtbblData = "<tr><td>" + OrientationSchedulesArray[i].RowNum + "</td><td>" + OrientationSchedulesArray[i].NameOfMember + "</td><td>" + cData + "</td><td>" + OrientationSchedulesArray[i].Time + "</td><td>" + OrientationSchedulesArray[i].Place + "</td><td>" + OrientationSchedulesArray[i].Purpose + "</td><td>" + OrientationSchedulesArray[i].Mode + "</td><td class='text-center'><a class='HideClass'  title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='DeleteMSMEArray(this," + OrientationSchedulesArray[i].OriID + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a><a class='HideClass'  title='Click to Reschedule' data-toggle='tooltip' data-original-title='Click to Reschedule' onclick='RescheduleMSMEArray(this," + OrientationSchedulesArray[i].RowNum + ")'><i class='fas fa-calendar-alt' aria-hidden='true'></i> </a></td><td style='display: none'>" + OrientationSchedulesArray[i].Feedback + "</td><td style='display: none'>" + modeId + "</td></tr>";
        var allstring = newtbblData;
        tableData += allstring;
    }
    $('#tblOrientationSchedule1').html(newtbblData1 + tableData + html1);
    HtmlPagingOrientationSchedule();
}
function HtmlPagingOrientationSchedule() {
    $('#tblOrientationSchedulePaging').after('<div id="divOrientationSchedulePaging" style="text-align:right"></div>');
    var rowsShown = 5;
    var rowsTotal = $('#tblOrientationSchedulePaging tbody tr').length;
    var numPages = rowsTotal / rowsShown;
    for (i = 0; i < numPages; i++) {
        var pageNum = i + 1;
        $('#divOrientationSchedulePaging').append('<a class="e-link e-numericitem e-spacing e-currentitem e-active" href="#" rel="' + i + '">' + pageNum + '</a> ');
    }
    $('#tblOrientationSchedulePaging tbody tr').hide();
    $('#tblOrientationSchedulePaging tbody tr').slice(0, rowsShown).show();
    $('#divOrientationSchedulePaging a:first').addClass('active');
    $('#divOrientationSchedulePaging a').bind('click', function () {
        $('#divOrientationSchedulePaging a').removeClass('active');
        $(this).addClass('active');
        var currPage = $(this).attr('rel');
        var startItem = currPage * rowsShown;
        var endItem = startItem + rowsShown;
        $('#tblOrientationSchedulePaging tbody tr').css('opacity', '0.0').hide().slice(startItem, endItem).
            css('display', 'table-row').animate({ opacity: 1 }, 300);
    });
}
function SavebtnReschedule() {
    $('#txtDate').val($('#txtRescheduleDate').val());
    $('#txtTime').val($('#txtRescheduleTime').val());
    $('#reschedule').modal('hide');
    return false;
}
function ReschedulePopup() {
    var ddddlNameOfMembers = document.getElementById("ddlNameOfMembers");
    var memberName = ddddlNameOfMembers.options[ddddlNameOfMembers.selectedIndex].text;
    document.getElementById('spRescheduleName').innerText = memberName;
    $('#reschedule').modal('show');

    return false;
}
function AddOrientationSchedulesArray() {
    var valid = true;
    if (checkValidationOnSubmit('Mandatory') == false) {
        valid = false;
    }
    if (checkValidationOnSubmit('Mandatorypld') == false) {
        valid = false;
    }

    if (valid == true) {
        var inputEle = document.getElementById('txtTime');
        var dd = onTimeChange(inputEle);
        var ddddlNameOfMembers = document.getElementById("ddlNameOfMembers");
        var ddddlMode = document.getElementById("ddlMode");
        OrientationSchedulesArrayId = OrientationSchedulesArray.length + 1;
        var loop = OrientationSchedulesArrayId;
        var objarrayinner =
        {
            RowNum: loop,
            CandidateId: CandidateId,
            NameOfMember: ddddlNameOfMembers.options[ddddlNameOfMembers.selectedIndex].text,
            EmpId: $("#ddlNameOfMembers").val(),
            OriDate: ChangeDateFormat($("#txtDate").val()),
            Time: $("#txtTime").val = dd,
            Place: $("#txtPlace").val(),
            Purpose: $("#txtPurpose").val(),
            Mode: ddddlMode.options[ddddlMode.selectedIndex].text,
            Feedback: '',
            ModeId: $('#ddlMode').val(),
            Id: loop
        }

        OrientationSchedulesArray.push(objarrayinner);

        $('#tblOrientationSchedule1').html('');
        var newtbblData1 = '<table id="tblOrientationSchedulePaging" class="table table-striped m-0" >' +
            ' <thead>' +
            ' <tr>' +
            ' <th class="td-50">S.No</th>' +
            ' <th class="td-250">Name of Member</th>' +
            ' <th>Date</th>' +
            ' <th>Time</th>' +
            ' <th>Place</th>' +
            ' <th>Purpose</th>' +
            ' <th>Mode</th>' +
            ' <th class="td-50 text-center">Action</th>' +
            ' <th style="display: none" class="td-50 text-center">Feedback</th>' +
            ' <th style="display: none" class="td-50 text-center">Mode ID</th>' +
            ' </tr>' +
            ' </thead>';
        var html1 = "</table>";

        var tableData = "";
        for (let i = 0; i < OrientationSchedulesArray.length; i++) {
            var newtbblData = "<tr><td>" + parseInt(i + 1) + "</td><td>" + OrientationSchedulesArray[i].NameOfMember + "</td><td>" + ChangeDateFormatToddMMYYY(OrientationSchedulesArray[i].OriDate) + "</td><td>" + OrientationSchedulesArray[i].Time + "</td><td>" + OrientationSchedulesArray[i].Place + "</td><td>" + OrientationSchedulesArray[i].Purpose + "</td><td>" + OrientationSchedulesArray[i].Mode + "</td><td style='align-items:center'><a class='HideClass'  title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='DeleteMSMEArray(this," + OrientationSchedulesArray[i].Id + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a><a class='HideClass'  title='Click to Reschedule' data-toggle='tooltip' data-original-title='Click to Reschedule' onclick='RescheduleMSMEArray(this," + OrientationSchedulesArray[i].RowNum + ")'><i class='fas fa-calendar-alt' aria-hidden='true'></i> </a></td><td style='display: none'>" + OrientationSchedulesArray[i].Feedback + "</td><td style='display: none'>" + OrientationSchedulesArray[i].ModeId + "</td></tr>";
            var allstring = newtbblData;
            tableData += allstring;
        }
        $('#tblOrientationSchedule1').html(newtbblData1 + tableData + html1);
        document.getElementById('lblSNo').innerText = 1;
        $("#txtDate").val('');
        $("#txtTime").val('');
        $("#txtPlace").val('');
        $("#txtPurpose").val('');
        $('#ddlNameOfMembers').val('0').trigger('change');
        $('#ddlMode').val('0').trigger('change');
    }
    HtmlPagingOrientationSchedule();
}

function UpdateOrientationSchedulesArray() {
    var valid = true;
    if (checkValidationOnSubmit('Mandatory') == false) {
        valid = false;
    }
    if (checkValidationOnSubmit('Mandatorypld') == false) {
        valid = false;
    }

    if (valid == true) {
        var EmpId = $("#ddlNameOfMembers").val();
        var NameOfMember = $("#ddlNameOfMembers  option:selected").text();
        var dateTime = ChangeDateFormat($("#txtDate").val());
        var inputEle = document.getElementById('txtTime');
        var Time = onTimeChange(inputEle);
        var Place = $("#txtPlace").val();
        var Purpose = $("#txtPurpose").val();
        var ModeId = $('#ddlMode').val();
        var Mode = $("#ddlMode  option:selected").text();
        var RowNum = document.getElementById('lblSNo').innerText;
        $('#tblOrientationSchedule1').html('');
        var newtbblData1 = '<table class="table table-striped m-0" >' +
            ' <thead>' +
            ' <tr>' +
            ' <th class="td-50">S.No</th>' +
            ' <th class="td-250">Name of Member</th>' +
            ' <th>Date</th>' +
            ' <th>Time</th>' +
            ' <th>Place</th>' +
            ' <th>Purpose</th>' +
            ' <th>Mode</th>' +
            ' <th class="td-50 text-center">Action</th>' +
            ' <th style="display: none" class="td-50 text-center">Feedback</th>' +
            ' <th style="display: none" class="td-50 text-center">Mode ID</th>' +
            ' </tr>' +
            ' </thead>';
        var html1 = "</table>";
        for (let i = 0; i < OrientationSchedulesArray.length; i++) {
            if (RowNum == parseInt(i + 1)) {
                OrientationSchedulesArray[i].RowNum = parseInt(RowNum);
                OrientationSchedulesArray[i].NameOfMember = NameOfMember;
                OrientationSchedulesArray[i].OriDate = dateTime;
                OrientationSchedulesArray[i].Time = Time;
                OrientationSchedulesArray[i].Place = Place;
                OrientationSchedulesArray[i].Purpose = Purpose;
                OrientationSchedulesArray[i].Mode = Mode;
                OrientationSchedulesArray[i].Feedback = '';
                OrientationSchedulesArray[i].ModeId = ModeId;
            }
        }
        var tableData = "";
        for (let i = 0; i < OrientationSchedulesArray.length; i++) {
            var newtbblData = "", allstring = "";
            newtbblData = "<tr><td>" + parseInt(i + 1) + "</td><td>" + OrientationSchedulesArray[i].NameOfMember + "</td><td>" + ChangeDateFormatToddMMYYY(OrientationSchedulesArray[i].OriDate) + "</td><td>" + OrientationSchedulesArray[i].Time + "</td><td>" + OrientationSchedulesArray[i].Place + "</td><td>" + OrientationSchedulesArray[i].Purpose + "</td><td>" + OrientationSchedulesArray[i].Mode + "</td><td style='align-items:center'><a class='HideClass'  title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='DeleteMSMEArray(this," + OrientationSchedulesArray[i].Id + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a><a class='HideClass'  title='Click to Reschedule' data-toggle='tooltip' data-original-title='Click to Reschedule' onclick='RescheduleMSMEArray(this," + OrientationSchedulesArray[i].RowNum + ")'><i class='fas fa-calendar-alt' aria-hidden='true'></i> </a></td><td style='display: none'>" + OrientationSchedulesArray[i].Feedback + "</td><td style='display: none'>" + OrientationSchedulesArray[i].ModeId + "</td></tr>";
            allstring = newtbblData;
            tableData += allstring;
        }
        $('#tblOrientationSchedule1').html(newtbblData1 + tableData + html1);
        document.getElementById('lblSNo').innerText = 1;
        $("#txtDate").val('');
        $("#txtTime").val('');
        $("#txtPlace").val('');
        $("#txtPurpose").val('');
        $('#ddlNameOfMembers').val('0').trigger('change');
        $('#ddlMode').val('0').trigger('change');
        document.getElementById('divUpdateRow').style.display = 'none';
        document.getElementById('divAddRow').style.display = 'block';
    }
}

function RescheduleMSMEArray(obj, id) {
    ConfirmMsgBox("Are you sure want to Reschedule", '', function () {
        document.getElementById('divUpdateRow').style.display = 'block';
        document.getElementById('divAddRow').style.display = 'none';
        var data = OrientationSchedulesArray.filter(function (itemParent) { return (itemParent.RowNum == id); });
        if (data.length > 0) {
            var modeId = data[0].Mode == "FaceToFace" ? 141 : data[0].Mode == "Virtual" ? 140 : '';
            var actualTime = data[0].Time.split(' ')[0];
            if (actualTime.length == 4) {
                actualTime = "0" + actualTime;
            }
            document.getElementById('lblSNo').innerText = data[0].RowNum;
            $('#ddlNameOfMembers').val(data[0].EmpId).trigger('change');
            $('#txtDate').val(ChangeDateFormatToddMMYYY(data[0].OriDate));
            $('#txtTime').val(actualTime);
            $('#txtPlace').val(data[0].Place);
            $('#txtPurpose').val(data[0].Purpose);
            $('#ddlMode').val(modeId).trigger('change');
            //CommonAjaxMethod(virtualPath + 'OnboardingRequest/DeleteOrientationSchedule', { Id: id, inputData: 3 }, 'POST', function (response) {
            //    $(obj).closest('tr').remove();
            //    OrientationSchedulesArray = OrientationSchedulesArray.filter(function (itemParent) { return (itemParent.OriID != id); });
            //});
            //$(obj).closest('tr').remove();
            //OrientationSchedulesArray = OrientationSchedulesArray.filter(function (itemParent) { return (itemParent.RowNum != id); });
            //RedirectToRegistration();
        }
    })
}
function DeleteMSMEArray(obj, id) {
    if (candodate_Status != true) {
        var isDelete = false;
        ConfirmMsgBox("Are you sure want to delete", '', function () {
            var data = OrientationSchedulesArray.filter(function (itemParent) { return (itemParent.OriID == id); });
            if (data.length > 0) {
                CommonAjaxMethod(virtualPath + 'OnboardingRequest/DeleteOrientationSchedule', { Id: id, inputData: 3 }, 'POST', function (response) {
                    $(obj).closest('tr').remove();
                    OrientationSchedulesArray = OrientationSchedulesArray.filter(function (itemParent) { return (itemParent.OriID != id); });
                    isDelete = true
                });
                if (isDelete == false) {
                    $(obj).closest('tr').remove();
                    OrientationSchedulesArray = OrientationSchedulesArray.filter(function (itemParent) { return (itemParent.OriID != id); });
                }
            }
            else {
                var data = OrientationSchedulesArray.filter(function (itemParent) { return (itemParent.RowNum == id); });
                $(obj).closest('tr').remove();
                OrientationSchedulesArray = OrientationSchedulesArray.filter(function (itemParent) { return (itemParent.RowNum != id); });
            }
            //RedirectToRegistration();
        });
    }
}
function SaveOrientationSchedules() {
    if (OrientationSchedulesArray.length > 0) {
        var obj = {
            OrientationSchedulesModel: OrientationSchedulesArray
        }

        CommonAjaxMethod(virtualPath + 'OnboardingRequest/SaveOrientationSchedule', obj, 'POST', function (response) {
            FailToaster(response.SuccessMessage);
            window.location.reload();
        });
    }
}
function RedirectToRegistration() {
    if (OrientationSchedulesArray.length > 0) {
        var obj = {
            OrientationSchedulesModel: OrientationSchedulesArray
        }

        CommonAjaxMethod(virtualPath + 'OnboardingRequest/SaveOrientationSchedule', obj, 'POST', function (response) {
            //FailToaster(response.SuccessMessage);
            FailToaster("Orientation schedule has been submitted.");
            window.location.href = virtualPath + 'Onboarding/Registration?id=' + CandidateId;
        });
    }
}
function onTimeChange(inputEle) {
    var timeSplit = inputEle.value.split(':'),
        hours,
        minutes,
        meridian;
    hours = timeSplit[0];
    minutes = timeSplit[1];
    if (hours > 12) {
        meridian = 'PM';
        hours -= 12;
    } else if (hours < 12) {
        meridian = 'AM';
        if (hours == 0) {
            hours = 12;
        }
    } else {
        meridian = 'PM';
    }
    return hours + ':' + minutes + ' ' + meridian;
}
function RedirectToClick(View) {
    window.location.href = virtualPath + 'Onboarding/' + View + '?id=' + CandidateId;
}
function RedirectToOnboard() {
    window.location.href = virtualPath + 'Onboarding/HRScreenOnboard';
}
