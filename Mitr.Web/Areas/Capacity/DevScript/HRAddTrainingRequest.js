$(document).ready(function () {
    $(function () {
        $('.datepicker').datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: "dd-mm-yy",
            yearRange: "-90:+10"
        });
    });
    document.getElementById('sptblSelectTrainingCalendarList').style.display = 'none';
    LoadMasterDropdown('ddlTrainingMode_0', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: 27,
        manualTableId: 0
    }, 'Select', false);
    LoadMasterDropdown('ddlMentorType', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: 28,
        manualTableId: 0
    }, 'Select', false);
    LoadMasterDropdown('ddlAttendeesType', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: 29,
        manualTableId: 0
    }, 'Select', false);
    LoadMasterDropdown('ddlEmployeeAttendeesList', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: 12,
        manualTableId: 0
    }, 'Select', false);
    LoadMasterDropdownValueCode('ddlMentorName', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: 12,
        manualTableId: 0
    }, 'Select', true);
    LoadMasterDropdown('ddlAttachmentType', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: 54,
        manualTableId: 0
    }, 'Select', false);
    GetMaxRequestNumber();
    ViewTrainingCalendraList();
});

var attendeesArray = []
var trainingCalandarArray = []
var attachmentsArray = []
var mentorsArray = []
function GetMaxRequestNumber() {
    CommonAjaxMethod(virtualPath + 'CapacityRequest/GetMaxReqCode', null, 'GET', function (response) {
        response.data.data.Table;
        document.getElementById('lblReqNo').innerHTML = response.data.data.Table[0].ReqNo;
        document.getElementById('lblReqDate').innerHTML = ChangeDateFormatToddMMYYY(response.data.data.Table[0].ReqDate);
        document.getElementById('lblReqBy').innerHTML = loggedinUserName;
    });
}
function SaveHRRequest() {
    var valid = true;
    //if (checkValidationOnSubmit('MandatoryPicker') == false) {
    //    valid = false;
    //}
    //if (checkValidationOnSubmit('Mandatorypld') == false) {
    //    valid = false;
    //}
    //if (valid == true) {
    var checkDate = IsGreaterThanFromAndToDate(ChangeDateFormat($('#txtFromDate').val()), ChangeDateFormat($('#txtToDate').val()), 'from date should be always less than to date.');
    if (checkDate == false) {
        return false;
    }
   
    var inputEle = document.getElementById('txtFromTime');
    var FromTime = onTimeChange(inputEle);
    var inputEle1 = document.getElementById('txtToTime');
    var ToTime = onTimeChange(inputEle1);
    var HrRequest = {
        ReqNo: document.getElementById('lblReqNo').innerText,
        ReqDate: ChangeDateFormat(document.getElementById('lblReqDate').innerText),
        RequestedByID: loggedinUserid,
        RequestedByName: loggedinUserName,
        Source: 'HR',
        TrainingDescription: $('#txtTrainingDescritpion').val(),
        FromDate: ChangeDateFormat($('#txtFromDate').val()),
        ToDate: ChangeDateFormat($('#txtToDate').val()),
        FromTime: FromTime,
        ToTime: ToTime,
        DateOfAssessment: ChangeDateFormat($('#txtDateOfAssessment').val()),
        InstructionsForTeam: $('#txtInstructionsForTeam').val(),
        InstructionsForSupervisors: $('#txtInstructionsForSupervisors').val(),
        CreatedBy: loggedinUserid,
        ModifiedBy: loggedinUserid,
        IPAddress: IPAddress,
        SupervisorAssessment: document.getElementById('chkSupervisorsAssessment').checked,
        TypeOfTraining: TrainingTypeID,
        NameOfTraining: TrainingName,
        Status: 3,
        EmployeeID: EMPID
    }

    var obj = {
        HrRequest: HrRequest,
        trainingHRRequestCalendar: trainingCalandarArray,
        requestHRMentors: mentorsArray,
        trainingHRAttendees: attendeesArray,
        requestHRAttachment: attachmentsArray
    }
    if (trainingCalandarArray.length > 0) {
        trainingCalandarArray[0].Location = $('#txtlocation_1').val();
    }
        document.getElementById('sptblSelectTrainingCalendarList').style.display = 'none';
        CommonAjaxMethod(virtualPath + 'HRRequest/SaveHRRequest', obj, 'POST', function (response) {
            FailToaster(response.SuccessMessage);
            //FailToaster('Your request has been saved.');
            window.location.href = virtualPath + 'HR/TrainingRequest?id=' + 1;
        });
    //}
    //else {
    //    document.getElementById('sptblSelectTrainingCalendarList').style.display = 'block';
    //}
    //}
}

function compareDate() {
    var dateEntered = document.getElementById("txtDateEntered").value;
    var date = dateEntered.substring(0, 2);
    var month = dateEntered.substring(3, 5);
    var year = dateEntered.substring(6, 10);

    var dateToCompare = new Date(year, month - 1, date);
    var currentDate = new Date();

    if (dateToCompare > currentDate) {
        alert("Date Entered is greater than Current Date ");
    }
    else {
        alert("Date Entered is lesser than Current Date");
    }
}
function SubmitHRRequest() {
    var valid = true;
    if (checkValidationOnSubmit('MandatoryPicker') == false) {
        valid = false;
    }
    //if (checkValidationOnSubmit('Mandatorypld') == false) {
    //    valid = false;
    //}
    if (valid == true) {
        var checkDate = IsGreaterThanFromAndToDate(ChangeDateFormat($('#txtFromDate').val()), ChangeDateFormat($('#txtToDate').val()), 'from date should be always less than to date.');
        if (checkDate == false) {
            return false;
        }
        var inputEle = document.getElementById('txtFromTime');
        var FromTime = onTimeChange(inputEle);
        var inputEle1 = document.getElementById('txtToTime');
        var ToTime = onTimeChange(inputEle1);
        var HrRequest = {
            ReqNo: document.getElementById('lblReqNo').innerText,
            ReqDate: ChangeDateFormat(document.getElementById('lblReqDate').innerText),
            RequestedByID: loggedinUserid,
            RequestedByName: loggedinUserName,
            Source: 'HR',
            TrainingDescription: $('#txtTrainingDescritpion').val(),
            FromDate: ChangeDateFormat($('#txtFromDate').val()),
            ToDate: ChangeDateFormat($('#txtToDate').val()),
            FromTime: FromTime,
            ToTime: ToTime,
            DateOfAssessment: ChangeDateFormat($('#txtDateOfAssessment').val()),
            InstructionsForTeam: $('#txtInstructionsForTeam').val(),
            InstructionsForSupervisors: $('#txtInstructionsForSupervisors').val(),
            CreatedBy: loggedinUserid,
            ModifiedBy: loggedinUserid,
            IPAddress: IPAddress,
            SupervisorAssessment: document.getElementById('chkSupervisorsAssessment').checked,
            TypeOfTraining: TrainingTypeID,
            NameOfTraining: TrainingName,
            Status: 4,
            EmployeeID: EMPID
        }

        var obj = {
            HrRequest: HrRequest,
            trainingHRRequestCalendar: trainingCalandarArray,
            requestHRMentors: mentorsArray,
            trainingHRAttendees: attendeesArray,
            requestHRAttachment: attachmentsArray
        }
        if (attendeesArray.length>0) {
            document.getElementById('sptblAddAttendeesList').style.display = 'none';
        }
        else
        {
            document.getElementById('sptblAddAttendeesList').style.display = 'block';
            return false;
        }
        if (mentorsArray.length > 0) {
            document.getElementById('sptblMentorList').style.display = 'none';
        }
        else
        {
            document.getElementById('sptblMentorList').style.display = 'block';
            return false;
        }
        if (trainingCalandarArray.length > 0) {
            trainingCalandarArray[0].Location = $('#txtlocation_1').val();
            document.getElementById('sptblSelectTrainingCalendarList').style.display = 'none';
            CommonAjaxMethod(virtualPath + 'HRRequest/SaveHRRequest', obj, 'POST', function (response) {
                //FailToaster(response.SuccessMessage);
                //FailToaster('Your request has been submitted');
                FailToaster(response.ErrorMessage);
                if (response.ErrorMessage == 'Your request has been submitted.') {
                    window.location.href = virtualPath + 'HR/TrainingRequest?id=' + 1;
                }
            });
        }
        else {
            document.getElementById('sptblSelectTrainingCalendarList').style.display = 'block';
        }
    }
}
function CancleHRRequest() {
    window.location.href = virtualPath + 'HR/TrainingRequest?id=' + 1;
}
function SelectedTrainingCalendraList() {
    CommonAjaxMethod(virtualPath + 'HRRequest/GetAllTrainingCalendar', { id: 1 }, 'GET', function (response) {
        var dataArry = response.data.data.Table;
        $('#tblTrainingCalendar').html('');
        var newtbblData1 = '<table class="table mt-0 " >' +
            ' <thead>' +
            ' <tr>' +
            ' <th width="33"></th>' +
            ' <th width="33">S.No</th>' +
            ' <th width="150">Training Type</th>' +
            ' <th width="150">Training Name</th>' +
            ' <th width="150" >Training Mode</th>' +
            ' <th ></th>' +
            ' </tr>' +
            ' </thead>';
        var html1 = "</table>";

        var tableData = "";
        for (let i = 0; i < dataArry.length; i++) {
            var newtbblData = "<tr><td><input class=\'radio-button\' id='radio_" + dataArry[i].id + "' name=\'radio\' type=\'radio\'></td><td>" + dataArry[i].RowNum + "</td><td>" + dataArry[i].TrainingTypeName + "</td><td>" + dataArry[i].TrainingDesc + "</td><td>" + dataArry[i].TrainingMode + " </td><td>" + dataArry[i].TraininglinkorLocation + " </td></tr>";
            var allstring = newtbblData;
            tableData += allstring;
        }
        $('#tblTrainingCalendar').html(newtbblData1 + tableData + html1);
    });
}
function ViewTrainingCalendraList() {
    CommonAjaxMethod(virtualPath + 'HRRequest/GetAllTrainingCalendar', { id: 1 }, 'GET', function (response) {
        var dataArry = response.data.data.Table;
        $('#tblTrainingCalendar').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": dataArry,
            "columns": [
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {
                        var strReturn = '<div class="radio-button"><input id="radio_' + row.id + '" name=\'radio\' type=\'radio\'></input><label for="radio_' + row.id + '" class="radio-label mb-0"></label></div>';
                        return strReturn;
                    }
                },
                { "data": "RowNum" },
                { "data": "TrainingTypeName" },               
                { "data": "TrainingName" },
                { "data": "TrainingMode" },
                { "data": "TraininglinkorLocation" }               
            ]
        });
    });
}
function HtmlPaging() {
    $('#tblOnboardUsers').after('<div id="divNav" style="text-align:right"></div>');
    var rowsShown = 5;
    var rowsTotal = $('#tblOnboardUsers tbody tr').length;
    var numPages = rowsTotal / rowsShown;
    for (i = 0; i < numPages; i++) {
        var pageNum = i + 1;
        $('#divNav').append('<a class="e-link e-numericitem e-spacing e-currentitem e-active" href="#" rel="' + i + '">' + pageNum + '</a> ');
    }
    $('#tblOnboardUsers tbody tr').hide();
    $('#tblOnboardUsers tbody tr').slice(0, rowsShown).show();
    $('#divNav a:first').addClass('active');
    $('#divNav a').bind('click', function () {
        $('#divNav a').removeClass('active');
        $(this).addClass('active');
        var currPage = $(this).attr('rel');
        var startItem = currPage * rowsShown;
        var endItem = startItem + rowsShown;
        $('#tblOnboardUsers tbody tr').css('opacity', '0.0').hide().slice(startItem, endItem).
            css('display', 'table-row').animate({ opacity: 1 }, 300);
    });
}
function AddTrainingAttendeesList() {

    var valid = true;
    if (checkValidationOnSubmit('MandatoryEmployeeAttendeesList') == false) {
        valid = false;
    }
    if (valid == true) {
        document.getElementById('sptblAddAttendeesList').style.display = 'none';
        for (var i = 0; i < $('#ddlEmployeeAttendeesList').find(':selected').length; i++) {
            var selectedIndex = $('#ddlEmployeeAttendeesList').find(':selected')[i].innerHTML;
            if (selectedIndex != 'Select') {
                var selectedValue = selectedIndex;
                var rowDate = {
                    RowNum: attendeesArray.length + 1,
                    AttendeesNames: selectedValue,
                    AttendeesTypeName: $('#ddlAttendeesType').val() == 148 ? 'Mandatory' : 'Optional',
                    AttendeesType: $('#ddlAttendeesType').val(),
                    RequestSource: 'HR',
                    RequestName: 'NA',
                    CreatedBy: loggedinUserid,
                    ModifiedBy: loggedinUserid,
                    IPAddress: IPAddress,
                    AttendEmployeeID: $('#ddlEmployeeAttendeesList').val()[i]
                }
                attendeesArray.push(rowDate);
            }
        }
       
        // Remove duplicate records on the array list.
        const seen = new Set();
        attendeesArray = attendeesArray.filter(item => {
            const duplicate = seen.has(item.AttendEmployeeID);
            seen.add(item.AttendEmployeeID);
            return !duplicate;
        });
        $('#tblAddAttendeesList').html('');
        var newtbblData1 = '<table class="table mt-0 " >' +
            ' <thead>' +
            ' <tr>' +
            ' <th width="33">S.No</th>' +
            ' <th width="150">Attendees Names</th>' +
            ' <th width="150">Attendees Type</th>' +
            ' <th width="150" >Request Source</th>' +
            ' <th width="150">Request Number</th>' +
            ' <th width="150" style="display:none">Attendees TypeID</th>' +
            ' <th width="150" style="display:block">EmployeeID</th>' +
            ' <th width="150">Action</th>' +
            ' </tr>' +
            ' </thead>';
        var html1 = "</table>";

        var tableData = "";
        for (let i = 0; i < attendeesArray.length; i++) {
            var newtbblData = "<tr><td>" + parseInt(i + 1) + "</td><td>" + attendeesArray[i].AttendeesNames + "</td><td>" + attendeesArray[i].AttendeesTypeName + "</td><td>" + attendeesArray[i].RequestSource + " </td><td>" + attendeesArray[i].RequestName + " </td><td style='display: none'>" + attendeesArray[i].AttendeesType + "</td><td style='display: none'>" + attendeesArray[i].AttendEmployeeID + "</td><td><a  title='Click to Remove' onclick='DeleteAttendeesArrayRows(this," + attendeesArray[i].RowNum + ")' class=\"red-clr\"><i class=\"fas fa-trash-alt\"></i></a></td></tr>";
            var allstring = newtbblData;
            tableData += allstring;
        }
        $('#tblAddAttendeesList').html(tableData);
        $('#modalAddAttendees').modal('hide');
    }
}
var TrainingTypeID = 0;
var TrainingName = '';
function GetTrainingCalandar() {
   
    TrainingTypeID = '';
    var TrainingType = '';
    TrainingName = '';
    var TrainingMode = '';
    var Location = '';
    var TrainingDesc = '';
    var TentativeFromDate = '';
    var TentativeToDate = '';
    var SupervisorAssessmentReq = '';
    trainingCalandarArray = []
    var IsRowSelect = false;
    document.getElementById('sptblSelectTrainingCalendarList').style.display = 'none';
    var selected = $("#tblTrainingCalendar input[type='radio']:checked");
    if (selected.length > 0) {
        var id = selected[0].id.split('_')[1]
        CommonAjaxMethod(virtualPath + 'HRRequest/GetByIDTrainingCalendar', { RequIds: id, inputData: 5 }, 'GET', function (response) {
            trainingCalandarArray = response.data.data.Table;
        });

        $('#tblSelectTrainingCalendarList').html('');
        var newtbblData1 = '<table class="table mt-2 " >' +
            ' <thead>' +
            ' <tr>' +
            ' <th width="33">S.No</th>' +
            ' <th width="300">Training Type</th>' +
            ' <th width="300">Training Name</th>' +
            ' <th width="150" >Training Mode</th>' +
            ' <th ></th>' +
            ' <th style="display:none"></th>' +
            ' </tr>' +
            ' </thead>';
        var html1 = "</table>";
        var tableData = "";
        var i = 0;
        if (trainingCalandarArray.length > 0) {
            for (i; i < trainingCalandarArray.length; i++) {
                //if (trainingCalandarArray[i].TrainingMode == 143) {
                //    var newtbblData = "<tr><td>" + parseInt(i + 1) + "</td><td>" + trainingCalandarArray[i].TrainingType + "</td><td>" + trainingCalandarArray[i].TrainingName + "</td><td><select onchange=\"ChangedTrainingMode(this," + i + ")\" id='ddlTrainingMode_" + i + "' class=\"form-control dpselect \" tabindex=\"-1\" aria-hidden=\"true\"></select> </td><td><a href='#'target='_blank' id='txtlocation_" + parseInt(i + 1) + "'>" + trainingCalandarArray[i].Location + "</a></td><td style='display: none'>" + trainingCalandarArray[i].TrainingTypeID + "</td></tr>";
                //}
                //else {
                //    var newtbblData = "<tr><td>" + parseInt(i + 1) + "</td><td>" + trainingCalandarArray[i].TrainingType + "</td><td>" + trainingCalandarArray[i].TrainingName + "</td><td><select onchange=\"ChangedTrainingMode(this," + i + ")\" id='ddlTrainingMode_" + i + "' class=\"form-control dpselect \" tabindex=\"-1\" aria-hidden=\"true\"></select> </td><td><input type='text' id='txtlocation_" + parseInt(i + 1) + "' value='" + trainingCalandarArray[i].Location + "'></input></td><td style='display: none'>" + trainingCalandarArray[i].TrainingTypeID + "</td></tr>";
                //}
                var newtbblData = "<tr><td>" + parseInt(i + 1) + "</td><td>" + trainingCalandarArray[i].TrainingType + "</td><td>" + trainingCalandarArray[i].TrainingName + "</td><td><select onchange='ChangedTrainingMode(this," + i + ")' id=\"ddlTrainingMode_" + i + "\" class='form-control dpselect' tabindex='-1\'></select> </td><td><input type='text' id='txtlocation_" + parseInt(i + 1) + "' value='" + trainingCalandarArray[i].Location + "'></input></td><td style='display: none'>" + trainingCalandarArray[i].TrainingTypeId + "</td></tr>";
                var allstring = newtbblData;
                tableData += allstring;

                TrainingTypeID = trainingCalandarArray[i].TrainingTypeId;
                TrainingName = trainingCalandarArray[i].TrainingName;
                document.getElementById('txtTrainingDescritpion').value = trainingCalandarArray[i].TrainingDesc;
                $('#txtFromDate').val(ChangeDateFormatToddMMYYY(trainingCalandarArray[i].TentativeFromDate));
                $('#txtToDate').val(ChangeDateFormatToddMMYYY(trainingCalandarArray[i].TentativeToDate));
                document.getElementById('chkSupervisorsAssessment').checked = trainingCalandarArray[i].SupervisorAssessmentReq == true ? true : false;
                trainingCalandarArray[i].CreatedBy = loggedinUserid;
                trainingCalandarArray[i].ModifiedBy = loggedinUserid;
                trainingCalandarArray[i].IPAddress = IPAddress;
            }
        }
        $('#tblSelectTrainingCalendarList').html(tableData);

        if (trainingCalandarArray.length > 0) {
            for (var i = 0; i < trainingCalandarArray.length; i++) {
                if (trainingCalandarArray[i].TrainingMode == 145) {
                    document.getElementById('spLocationAndLink').innerText = 'Location';
                }
                if (trainingCalandarArray[i].TrainingMode == 143) {
                    document.getElementById('spLocationAndLink').innerText = 'Link';
                }
            }
        }
        ClickSupervisorsAssessment();
        if (trainingCalandarArray.length > 0) {
            for (var i = 0; i < trainingCalandarArray.length; i++) {
                LoadMasterDropdown('ddlTrainingMode_' + i, {
                    ParentId: 0,
                    masterTableType: 0,
                    isMasterTableType: false,
                    isManualTable: true,
                    manualTable: 27,
                    manualTableId: 0
                }, 'Select', false);
                $('#ddlTrainingMode_' + i).val(trainingCalandarArray[i].TrainingMode)
            }
        }
    }
    else {
        alert('Please the training calendar.')
    }
}
function OnChangeFromDate() {
    var currentDate = new Date();
    var fromDate = new Date(ChangeDateFormat($('#txtFromDate').val()));
    if (fromDate < currentDate) {
        $('#txtFromDate').val('');
        alert('From date should be always greater than current date.')
    }
}
function ChangeTodate() {
    var currentDate = new Date();
    var fromDate = new Date(ChangeDateFormat($('#txtToDate').val()));
    if (fromDate < currentDate) {
        $('#txtToDate').val('');
        alert('To date should be always greater than current date.')
    }
}
function AddAttachments() {
    var valid = true;

    if (checkValidationOnSubmit('MandatoryAttachmentType') == false) {
        valid = false;
    }
    //if (checkValidationOnSubmit('MandatoryAttachmentRemark') == false) {
    //    valid = false;
    //}
    if (valid == true) {
        var loop = attachmentsArray.length + 1;
        var attachmentData = {
            RowNum: loop,
            AttachmentType: $('#ddlAttachmentType').val(),
            AttachmentTypeName: $('#ddlAttachmentType option:selected').text(),
            AttachmentActualName: $('#hdnfileAttachmentScopeActualName').val(),
            Remark: $('#txtAttachmentRemark').val(),
            AttachmentNewName: $('#hdnfileAttachmentScopeNewName').val(),
            AttachmentUrl: $('#hdnfileAttachmentScopeFileUrl').val(),
            CreatedBy: loggedinUserid,
            ModifiedBy: loggedinUserid,
            IPAddress: IPAddress
        }
        attachmentsArray.push(attachmentData);
        $('#tblAttachmentsList').html('');
        var newtbblData1 = '<table class="table mt-2 " >' +
            ' <thead>' +
            ' <tr>' +
            ' <th width="33">S.No</th>' +
            ' <th width="150">Attachment Type</th>' +
            ' <th width="150">Attachment</th>' +
            ' <th width="150" >Remark</th>' +
            ' <th width="100" >Action</th>' +
            ' <th width="100" style="display:none">NewName</th>' +
            ' <th width="100" style="display:none">FileUrl</th>' +
            ' </tr>' +
            ' </thead>';
        var html1 = "</table>";
        var tableData = "";
        var i = 0;
        for (i; i < attachmentsArray.length; i++) {
            attachmentsArray[i].Remark = attachmentsArray[i].Remark == null ? '' : attachmentsArray[i].Remark;
            attachmentsArray[i].AttachmentActualName = attachmentsArray[i].AttachmentActualName == null ? '' : attachmentsArray[i].AttachmentActualName;
            var newtbblData = "<tr><td>" + parseInt(i+1) + "</td><td style='display: none'>" + attachmentsArray[i].AttachmentType + "</td><td>" + attachmentsArray[i].AttachmentTypeName + "</td><td><a target='_blank' href=" + attachmentsArray[i].AttachmentUrl + ">" + attachmentsArray[i].AttachmentActualName + "</a></td><td>" + attachmentsArray[i].Remark + " </td><td> <a  title='Click to Remove' onclick='DeleteDocArrayRows(this," + attachmentsArray[i].RowNum + ")' class=\"red-clr\"><i class=\"fas fa-trash-alt\"></i></a></td><td style=\"display: none\">" + attachmentsArray[i].AttachmentNewName + "</td><td style=\"display: none\">" + attachmentsArray[i].AttachmentUrl + "</td></tr>";
            var allstring = newtbblData;
            tableData += allstring;
        }
        $('#tblAttachmentsList').html(tableData);
        $('#ddlAttachmentType').val('0').trigger('change');
        $('#txtAttachmentRemark').val('');
        $('#hdnfileAttachmentScopeActualName').val('');
        $('#hdnfileAttachmentScopeNewName').val('');
        $('#hdnfileAttachmentScopeFileUrl').val('');
        $('#fileAttachmentScopeofwork').val('');
    }
}
function DeleteDocArrayRows(obj, id) {
    ConfirmMsgBox("Are you sure want to delete", '', function () {
        var data = attachmentsArray.filter(function (itemParent) { return (itemParent.RowNum == id); });
        //CommonAjaxMethod(virtualPath + 'OnboardingRequest/DeleteListRecordNotes', { Id: id, inputData: 4 }, 'POST', function (response) {
        $(obj).closest('tr').remove();
        attachmentsArray = attachmentsArray.filter(function (itemParent) { return (itemParent.RowNum != id); });
        //});
    });
}
function DeleteAttendeesArrayRows(obj, rowId) {
    ConfirmMsgBox("Are you sure want to delete.", '', function () {
        var data = attendeesArray.filter(function (itemParent) { return (itemParent.RowNum == rowId); });
        $(obj).closest('tr').remove();
        attendeesArray = attendeesArray.filter(function (itemParent) { return (itemParent.RowNum != rowId); });
        AfterDeleteReBuildAttendeesList();
    });
   
}
function AfterDeleteReBuildAttendeesList() {
    if (attendeesArray != undefined) {
        $('#tblAddAttendeesList').html('');
        var newtbblData1 = '<table class="table mt-0 " >' +
            ' <thead>' +
            ' <tr>' +
            ' <th width="33">S.No</th>' +
            ' <th width="150">Attendees Names</th>' +
            ' <th width="150">Attendees Type</th>' +
            ' <th width="150" >Request Source</th>' +
            ' <th width="150">Request Number</th>' +
            ' <th width="150" style="display:none">Attendees TypeID</th>' +
            ' <th width="150" style="display:block">EmployeeID</th>' +
            ' <th width="150">Action</th>' +
            ' </tr>' +
            ' </thead>';
        var html1 = "</table>";

        var tableData = "";
        for (let i = 0; i < attendeesArray.length; i++) {
            attendeesArray[i].RowNum = parseInt(i + 1);
            var newtbblData = "<tr><td>" + attendeesArray[i].RowNum + "</td><td>" + attendeesArray[i].AttendeesNames + "</td><td>" + attendeesArray[i].AttendeesTypeName + "</td><td>" + attendeesArray[i].RequestSource + " </td><td>" + attendeesArray[i].RequestName + " </td><td style='display: none'>" + attendeesArray[i].AttendeesType + "</td><td style='display: none'>" + attendeesArray[i].AttendEmployeeID + "</td><td><a  title='Click to Remove' onclick='DeleteAttendeesArrayRows(this," + attendeesArray[i].RowNum + ")' class=\"red-clr\"><i class=\"fas fa-trash-alt\"></i></a></td></tr>";
            var allstring = newtbblData;
            tableData += allstring;
        }
        $('#tblAddAttendeesList').html(tableData);
    }
}
function UploadAttachmentFileScope() {
    var fileUpload = $("#fileAttachmentScopeofwork").get(0);

    var files = fileUpload.files;
    if (files.length > 0) {
        // Create FormData object
        var fileData = new FormData();

        // Looping over all files and add it to FormData object
        for (var i = 0; i < files.length; i++) {
            fileData.append(files[i].name, files[i]);
        }

        $.ajax({
            url: virtualPath + 'CommonMethod/UploadOnBoardindDocument',
            type: "POST",
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            data: fileData,

            success: function (response) {
                var result = JSON.parse(response);
                if (result.ErrorMessage == "") {
                    $('#hdnfileAttachmentScopeActualName').val(result.FileModel.ActualFileName);
                    $('#hdnfileAttachmentScopeNewName').val(result.FileModel.NewFileName);
                    $('#hdnfileAttachmentScopeFileUrl').val(result.FileModel.FileUrl);
                }
                else {
                    FailToaster(result.ErrorMessage);
                    //document.getElementById('hReturnMessage').innerHTML = result.ErrorMessage;
                    //$('#btnShowModel').click();
                }
            }
            ,
            error: function (error) {
                FailToaster(error);
                //document.getElementById('hReturnMessage').innerHTML = error;
                //$('#btnShowModel').click();
                isSuccess = false;
            }
        });
    }
    else {
        FailToaster("Please select file to attach!");
        //document.getElementById('hReturnMessage').innerHTML = "Please select file to attach!";
        //$('#btnShowModel').click();
    }
}
function ChangeMentorType() {
    var MentorType = $('#ddlMentorType option:selected').text();
    var MentorTypeID = $('#ddlMentorType').val();
    if (MentorTypeID == "147") {
        document.getElementById('divInternal').style.display = 'block';
        document.getElementById('divInternal').style.display = 'block';
        document.getElementById('divExternal').style.display = 'none';
        document.getElementById('divExternal').style.display = 'none';

        $('#ddlMentorName').attr({ 'class': 'form-control dpselect select2-hidden-accessible Mandatorypld ' })


        $('#txtExternalMentorName').attr({ 'class': 'form-control  ' })
        $('#txtExternalMentorEmail').attr({ 'class': 'form-control  ' })
    }
    if (MentorTypeID == "146") {
        document.getElementById('divExternal').style.display = 'block';
        document.getElementById('divExternal').style.display = 'block';
        document.getElementById('divInternal').style.display = 'none';
        document.getElementById('divInternal').style.display = 'none';

        $('#txtExternalMentorName').attr({ 'class': 'form-control Mandatory ' })
        //$('#sptxtExternalMentorName').attr({ 'className': 'red-clr' }).html('*');
        $('#txtExternalMentorEmail').attr({ 'class': 'form-control Mandatory ' })
        //$('#sptxtExternalMentorEmail').attr({ 'className': 'red-clr' }).html('*');

        $('#ddlMentorName').attr({ 'class': 'form-control dpselect select2-hidden-accessible  ' })

    }
    //document.getElementById('sptxtExternalFindEmployeeValidation').innerText = '';
    //document.getElementById('sptxtExternalFindEmployeeValidation').style.display = 'none';
    //document.getElementById('sptxtInternalFindEmployeeValidation').innerText = '';
    //document.getElementById('sptxtInternalFindEmployeeValidation').style.display = 'none';
    //$('#ddlMentorName').prop('disabled', false);
    //$('#txtExternalMentorName').prop('disabled', false);
}
function ChangeMentorName() {

    $('#txtInternalEmployeeEmailID').val($('#ddlMentorName option:selected').attr("dataele"));

}
function AddMentorList() {
    var valid = true;

    if (checkValidationOnSubmit('Mandatory') == false) {
        valid = false;
    }
    if (checkValidationOnSubmit('Mandatorypld') == false) {
        valid = false;
    }
    if (valid == true) {
        if ($('#ddlMentorType').val() == '147') {
            if (InternalValidateEmail() == false) { return false; }
        }
        if ($('#ddlMentorType').val() == '146') {
            if (ExternalValidateEmail() == false) { return false; }
        }
        document.getElementById('sptblMentorList').style.display = 'none';
        var MentorTypeID = $('#ddlMentorType').val();
        var loop = mentorsArray.length + 1;
        var mentorsData = {
            RowNum: loop,
            MentorType: $('#ddlMentorType').val(),
            MentorTypeName: $('#ddlMentorType option:selected').text(),
            MentorName: $('#ddlMentorType').val() == '147' ? $('#ddlMentorName option:selected').text() : $('#txtExternalMentorName').val(),
            Email: $('#ddlMentorType').val() == '147' ? $('#txtInternalEmployeeEmailID').val() : $('#txtExternalMentorEmail').val(),
            CreatedBy: loggedinUserid,
            ModifiedBy: loggedinUserid,
            IPAddress: IPAddress,
            EmpID: $('#ddlMentorName').val(),
            EmpRowId: $('#ddlMentorName option:selected').attr("dataele")
        }
        //let findData = "";
        //if (mentorsArray.length > 0) {
        //    findData = mentorsArray.find(obj => obj.MentorName === mentorsData.MentorName);
        //}
        //if (findData == "" || findData == undefined) {
            mentorsArray.push(mentorsData);
        //    document.getElementById('sptxtExternalFindEmployeeValidation').innerText = '';
        //    document.getElementById('sptxtExternalFindEmployeeValidation').style.display = 'none';

        //    document.getElementById('sptxtInternalFindEmployeeValidation').innerText = '';
        //    document.getElementById('sptxtInternalFindEmployeeValidation').style.display = 'none';
        //}
        //else {
        //    document.getElementById('sptxtExternalFindEmployeeValidation').innerText = 'Hey, This is duplicate name!!';
        //    document.getElementById('sptxtExternalFindEmployeeValidation').style.display = 'block';

        //    document.getElementById('sptxtInternalFindEmployeeValidation').innerText = 'Hey, This is duplicate name!!';
        //    document.getElementById('sptxtInternalFindEmployeeValidation').style.display = 'block';
        //}

        $('#tblMentorList').html('');
        var newtbblData1 = '<table class="table mt-2 " >' +
            ' <thead>' +
            ' <tr>' +
            ' <th width="33">S.No</th>' +
            ' <th width="10" style="display:none">Mentor Type</th>' +
            ' <th width="100">Mentor Type</th>' +
            ' <th width="150">Name</th>' +
            ' <th width="150" >Email</th>' +
            ' <th width="60" >Action</th>' +
            ' <th width="60" style="display:none">EmpID</th>' +
            ' <th width="60" style="display:none">EmpRowId</th>' +
            ' </tr>' +
            ' </thead>';
        var html1 = "</table>";
        var tableData = "";
        var i = 0;
        for (i; i < mentorsArray.length; i++) {
            var newtbblData = "<tr><td>" + parseInt(i+1) + "</td><td style='display: none'>" + mentorsArray[i].MentorType + "</td><td>" + mentorsArray[i].MentorTypeName + "</td><td>" + mentorsArray[i].MentorName + "</td><td>" + mentorsArray[i].Email + " </td><td> <a  title='Click to Remove' onclick='EditMentorsRows(this," + mentorsArray[i].RowNum + ")' class=\"red-clr\"><i class=\"fas fa-edit m-0\"></i></a><span> | </span><a  title='Click to Remove' onclick='DeleteMentorsRows(this," + mentorsArray[i].RowNum + ")' class=\"red-clr\"><i class=\"fas fa-trash-alt\"></i></a></td><td style='display: none'>" + mentorsArray[i].EmpID + "</td><td style='display: none'>" + mentorsArray[i].EmpRowId + "</td></tr>";
            var allstring = newtbblData;
            tableData += allstring;
        }
        $('#tblMentorList').html(tableData);
        if (mentorsArray.length > 0) {
            document.getElementById('spAddedMentorCount').innerText = mentorsArray.length;
        }
        else {
            document.getElementById('spAddedMentorCount').innerText = '';
        }
        $('#txtExternalMentorName').val('');
        $('#txtExternalMentorEmail').val('');
        $("#ddlMentorName").val('0').trigger('change');
        $('#txtInternalEmployeeEmailID').val('');
    }
}
function DeleteMentorsRows(obj, id) {
    ConfirmMsgBox("Are you sure want to delete", '', function () {
        var data = mentorsArray.filter(function (itemParent) { return (itemParent.RowNum == id); });

        if (data.MentorType == "147") {
            document.getElementById('divInternal').style.display = 'block';
            document.getElementById('divInternal').style.display = 'block';
            document.getElementById('divExternal').style.display = 'none';
            document.getElementById('divExternal').style.display = 'none';

            $('#ddlMentorType').val(data.MentorType);

        }
        if (data.MentorType == "146") {
            document.getElementById('divExternal').style.display = 'block';
            document.getElementById('divExternal').style.display = 'block';
            document.getElementById('divInternal').style.display = 'none';
            document.getElementById('divInternal').style.display = 'none';
            $('#txtExternalMentorName').val(data.MentorTypeName);
            $('#txtExternalMentorEmail').val(data.Email);
        }
       
        $(obj).closest('tr').remove();
        mentorsArray = mentorsArray.filter(function (itemParent) { return (itemParent.RowNum != id); });
        if (mentorsArray.length > 0) {
            document.getElementById('spAddedMentorCount').innerText = mentorsArray.length;
        }
        else {
            document.getElementById('spAddedMentorCount').innerText = '';
        }
        AfterDeleteMentors();
        //});
    })
}
function AfterDeleteMentors() {
    $('#tblMentorList').html('');
    var newtbblData1 = '<table class="table mt-2 " >' +
        ' <thead>' +
        ' <tr>' +
        ' <th width="33">S.No</th>' +
        ' <th width="10" style="display:none">Mentor Type</th>' +
        ' <th width="100">Mentor Type</th>' +
        ' <th width="150">Name</th>' +
        ' <th width="150" >Email</th>' +
        ' <th width="60" >Action</th>' +
        ' <th width="60" style="display:none">EmpID</th>' +
        ' <th width="60" style="display:none">EmpRowId</th>' +
        ' </tr>' +
        ' </thead>';
    var html1 = "</table>";
    var tableData = "";
    var i = 0;
    for (i; i < mentorsArray.length; i++) {
        mentorsArray[i].RowNum = parseInt(i + 1);
        var newtbblData = "<tr><td>" + mentorsArray[i].RowNum + "</td><td style='display: none'>" + mentorsArray[i].MentorType + "</td><td>" + mentorsArray[i].MentorTypeName + "</td><td>" + mentorsArray[i].MentorName + "</td><td>" + mentorsArray[i].Email + " </td><td> <a  title='Click to Remove' onclick='EditMentorsRows(this," + mentorsArray[i].RowNum + ")' class=\"red-clr\"><i class=\"fas fa-edit m-0\"></i></a><span> | </span><a  title='Click to Remove' onclick='DeleteMentorsRows(this," + mentorsArray[i].RowNum + ")' class=\"red-clr\"><i class=\"fas fa-trash-alt\"></i></a></td><td style='display: none'>" + mentorsArray[i].EmpID + "</td><td style='display: none'>" + mentorsArray[i].EmpRowId + "</td></tr>";
        var allstring = newtbblData;
        tableData += allstring;
    }
    $('#tblMentorList').html(tableData);
}
var rowId = 0;
function EditMentorsRows(obj, id) {
    ConfirmMsgBox("Are you sure want to edit.", '', function () {
        rowId = id;
        document.getElementById('divAddRow').style.display = 'none';
        document.getElementById('divEditRow').style.display = 'block';
        var data = mentorsArray.filter(function (itemParent) { return (itemParent.RowNum == id); });
        if (data[0].MentorType == "146") {
            $('#ddlMentorType').val(data[0].MentorType).trigger('change');
            $('#txtExternalMentorName').val(data[0].MentorName);
            $('#txtExternalMentorEmail').val(data[0].Email);
        }
        if (data[0].MentorType == "147") {
            $('#ddlMentorType').val(data[0].MentorType).trigger('change');
            $('#ddlMentorName').val(data[0].EmpID).trigger('change');
            $('#txtInternalEmployeeEmailID').val(data[0].Email);
        }
        $('#txtExternalMentorName').prop('disabled', false);
        $('#ddlMentorName').prop('disabled', false);
    })
}
function EditRow() {
    var valid = true;

    if (checkValidationOnSubmit('Mandatory') == false) {
        valid = false;
    }
    if (checkValidationOnSubmit('Mandatorypld') == false) {
        valid = false;
    }
    if (valid == true) {
        if ($('#ddlMentorType').val() == '147') {
            if (InternalValidateEmail() == false) { return false; }
        }
        if ($('#ddlMentorType').val() == '146') {
            if (ExternalValidateEmail() == false) { return false; }
        }
        document.getElementById('divAddRow').style.display = 'block';
        document.getElementById('divEditRow').style.display = 'none';
        
        for (var i = 0; i < mentorsArray.length; i++) {
            if (rowId == parseInt(i + 1)) {
                //mentorsArray[i].RowNum = id;
                mentorsArray[i].MentorType = $('#ddlMentorType').val();
                mentorsArray[i].MentorTypeName = $('#ddlMentorType option:selected').text();
                mentorsArray[i].MentorName = $('#ddlMentorType').val() == '147' ? $('#ddlMentorName option:selected').text() : $('#txtExternalMentorName').val();
                mentorsArray[i].Email = $('#ddlMentorType').val() == '147' ? $('#txtInternalEmployeeEmailID').val() : $('#txtExternalMentorEmail').val();
                mentorsArray[i].CreatedBy = loggedinUserid;
                mentorsArray[i].ModifiedBy = loggedinUserid;
                mentorsArray[i].IPAddress = IPAddress;
                mentorsArray[i].EmpID = $('#ddlMentorName').val();
            }
        }
        $('#tblMentorList').html('');
        var newtbblData1 = '<table class="table mt-2 " >' +
            ' <thead>' +
            ' <tr>' +
            ' <th width="33">S.No</th>' +
            ' <th width="10" style="display:none">Mentor Type</th>' +
            ' <th width="100">Mentor Type</th>' +
            ' <th width="150">Name</th>' +
            ' <th width="150" >Email</th>' +
            ' <th width="60" >Action</th>' +
            ' <th width="60" style="display:none">EmpID</th>' +
            ' </tr>' +
            ' </thead>';
        var html1 = "</table>";
        var tableData = "";
        var i = 0;
        for (i; i < mentorsArray.length; i++) {
            mentorsArray[i].RowNum = parseInt(i + 1);
            var newtbblData = "<tr><td>" + mentorsArray[i].RowNum + "</td><td style='display: none'>" + mentorsArray[i].MentorType + "</td><td>" + mentorsArray[i].MentorTypeName + "</td><td>" + mentorsArray[i].MentorName + "</td><td>" + mentorsArray[i].Email + " </td><td> <a  title='Click to Remove' onclick='EditMentorsRows(this," + mentorsArray[i].RowNum + ")' class=\"red-clr\"><i class=\"fas fa-edit m-0\"></i></a><span> | </span><a  title='Click to Remove' onclick='DeleteMentorsRows(this," + mentorsArray[i].RowNum + ")' class=\"red-clr\"><i class=\"fas fa-trash-alt\"></i></a></td><td style='display: none'>" + mentorsArray[i].EmpID + "</td></tr>";
            var allstring = newtbblData;
            tableData += allstring;
        }
        $('#tblMentorList').html(tableData);
        $('#txtExternalMentorName').val('');
        $('#txtExternalMentorEmail').val('');
        $("#ddlMentorName").val('0').trigger('change');
        $('#txtInternalEmployeeEmailID').val('');
        $('#txtExternalMentorName').prop('disabled', false);
        $('#ddlMentorName').prop('disabled', false);
    }
}
function ClickSupervisorsAssessment() {
    if (document.getElementById('chkSupervisorsAssessment').checked) {
        $('#txtDateOfAssessment').attr({ 'class': 'form-control datepicker MandatoryPicker ' })
        $('#suptxtDateOfAssessment').attr({ 'className': 'red-clr' }).html('*');
        //$('#txtInstructionsForSupervisors').attr({ 'class': 'form-control h-100 maxSize[1200] MandatoryPicker ' })
        //$('#suptxtInstructionsForSupervisors').attr({ 'className': 'red-clr' }).html('*');
        $('#txtInstructionsForSupervisors').attr('readonly', false);
        $('#txtDateOfAssessment').prop('disabled', false);
    }
    else {
        $('#txtDateOfAssessment').attr({ 'class': 'form-control datepicker ' })
        $('#suptxtDateOfAssessment').attr({ 'className': 'red-clr' }).html('');
        //$('#txtInstructionsForSupervisors').attr({ 'class': 'form-control h-100 maxSize[1200] ' })
        //$('#suptxtInstructionsForSupervisors').attr({ 'className': 'red-clr' }).html('');
        $('#txtInstructionsForSupervisors').attr('readonly', 'readonly');
        $('#txtDateOfAssessment').prop('disabled', true);
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
function ChangedTrainingMode(ctrl, i) {
    if (trainingCalandarArray.length > 0) {
        for (i; i < trainingCalandarArray.length; i++) {
            trainingCalandarArray[i].TrainingMode = $('#ddlTrainingMode_0').val();
        }
        if ($('#ddlTrainingMode_0').val() == '143') {
            document.getElementById('spLocationAndLink').innerText = 'Link'
        }
        if ($('#ddlTrainingMode_0').val() == '145') {
            document.getElementById('spLocationAndLink').innerText = 'Location'
        }
    }
}

function ExternalValidateEmail(mail) {
    var email = $('#txtExternalMentorEmail').val();
    if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(email)) {
        document.getElementById("sptxtExternalMentorEmailValidation").innerText = '';
        document.getElementById("sptxtExternalMentorEmailValidation").style.display = "none";
        return (true)
    }
    else {
        document.getElementById("sptxtExternalMentorEmailValidation").innerText = 'You have entered an invalid email address!';
        document.getElementById("sptxtExternalMentorEmailValidation").style.display = "block";
        return (false)
    }
}
function InternalValidateEmail(mail) {
    var email = $('#txtInternalEmployeeEmailID').val();
    if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(email)) {
        document.getElementById("sptxtInternalEmployeeEmailIDValidation").innerText = '';
        document.getElementById("sptxtInternalEmployeeEmailIDValidation").style.display = "none";
        return (true)
    }
    else {
        document.getElementById("sptxtInternalEmployeeEmailIDValidation").innerText = 'You have entered an invalid email address!';
        document.getElementById("sptxtInternalEmployeeEmailIDValidation").style.display = "block";
        return (false)
    }
}
function HtmlPendingPaging() {
    $('#tbltrainingRequestPending').after('<div id="divPendingNav" style="text-align:right"></div>');
    var rowsShown = 15;
    var rowsTotal = $('#tbltrainingRequestPending tbody tr').length;
    var numPages = rowsTotal / rowsShown;
    for (i = 0; i < numPages; i++) {
        var pageNum = i + 1;
        $('#divPendingNav').append('<a class="e-link e-numericitem e-spacing e-currentitem e-active" href="#" rel="' + i + '">' + pageNum + '</a> ');
    }
    $('#tbltrainingRequestPending tbody tr').hide();
    $('#tbltrainingRequestPending tbody tr').slice(0, rowsShown).show();
    $('#divPendingNav a:first').addClass('active');
    $('#divPendingNav a').bind('click', function () {
        $('#divPendingNav a').removeClass('active');
        $(this).addClass('active');
        var currPage = $(this).attr('rel');
        var startItem = currPage * rowsShown;
        var endItem = startItem + rowsShown;
        $('#tbltrainingRequestPending tbody tr').css('opacity', '0.0').hide().slice(startItem, endItem).
            css('display', 'table-row').animate({ opacity: 1 }, 300);
    });
}