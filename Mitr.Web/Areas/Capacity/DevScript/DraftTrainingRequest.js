$(document).ready(function () {
    $(function () {
        $('.datepicker').datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: "dd-mm-yy",
            yearRange: "-90:+10"
        });
    });

    //LoadMasterDropdown('ddlTrainingMode_0', {
    //    ParentId: 0,
    //    masterTableType: 0,
    //    isMasterTableType: false,
    //    isManualTable: true,
    //    manualTable: 27,
    //    manualTableId: 0
    //}, 'Select', false);
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
    //GetMaxRequestNumber();
    ViewTrainingCalendraList();
});

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
        ReqID: $('#hddReqID').val(),
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
    //if (dataRequestAttendees.length > 0) {
    //    document.getElementById('sptblAddAttendeesList').style.display = 'none';
    //}
    //else {
    //    document.getElementById('sptblAddAttendeesList').style.display = 'block';
    //    return false;
    //}
    var obj = {
        HrRequest: HrRequest,
        trainingHRRequestCalendar: trainingCalandarArray,
        requestHRMentors: dataRequestMentors,
        trainingHRAttendees: dataRequestAttendees,
        requestHRAttachment: dataRequestAttachment
    }
    if (trainingCalandarArray.length > 0) {
        trainingCalandarArray[0].Location = $('#txtlocation_1').val();
    }
    document.getElementById('sptblSelectTrainingCalendarList').style.display = 'none';
    CommonAjaxMethod(virtualPath + 'HRRequest/DraftSaveHRRequest', obj, 'POST', function (response) {
        FailToaster(response.SuccessMessage);
        // FailToaster('Your request has been saved.');
        window.location.href = virtualPath + 'HR/TrainingRequest?id=' + 1;
    });
    //}
    //}
    //else {
    //    document.getElementById('sptblSelectTrainingCalendarList').style.display = 'block';
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
            ReqID: $('#hddReqID').val(),
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
            requestHRMentors: dataRequestMentors,
            trainingHRAttendees: dataRequestAttendees,
            requestHRAttachment: dataRequestAttachment
        }
        if (dataRequestAttendees.length > 0) {
            document.getElementById('sptblAddAttendeesList').style.display = 'none';
        }
        else {
            document.getElementById('sptblAddAttendeesList').style.display = 'block';
            return false;
        }
        if (dataRequestMentors.length > 0) {
            document.getElementById('sptblMentorList').style.display = 'none';
        }
        else {
            document.getElementById('sptblMentorList').style.display = 'block';
            return false;
        }
        if (trainingCalandarArray.length > 0) {
            trainingCalandarArray[0].Location = $('#txtlocation_1').val();
            document.getElementById('sptblSelectTrainingCalendarList').style.display = 'none';
            CommonAjaxMethod(virtualPath + 'HRRequest/DraftSaveHRRequest', obj, 'POST', function (response) {
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
    CommonAjaxMethod(virtualPath + 'HRRequest/GetTrainingCalendarByRequestID', { ReqId: ReqID, inputData:1 }, 'GET', function (response) {
        var dataArry = response.data.data.Table; 
        if (dataArry != undefined) {
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
        }
    });

}

function AddTrainingAttendeesList() {

    var valid = true;
    if (checkValidationOnSubmit('MandatoryEmployeeAttendeesList') == false) {
        valid = false;
    }
    if (valid == true) {
        for (var i = 0; i < $('#ddlEmployeeAttendeesList').find(':selected').length; i++) {
            var selectedIndex = $('#ddlEmployeeAttendeesList').find(':selected')[i].innerHTML;
            if (selectedIndex != 'Select') {
                var selectedValue = selectedIndex;
                var rowDate = {
                    RowNum: dataRequestAttendees.length + 1,
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
                dataRequestAttendees.push(rowDate);
            }
        }
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
            ' <th width="150">Action</th>' +
            ' </tr>' +
            ' </thead>';
        var html1 = "</table>";

        var tableData = "";
        for (let i = 0; i < dataRequestAttendees.length; i++) {
            if (dataRequestAttendees[i].AttendeesNames != null) {
                var AttendeesTypeName = dataRequestAttendees[i].AttendeesType == 148 ? 'Mandatory' : dataRequestAttendees[i].AttendeesType == 149 ? 'Optional' : '';
                dataRequestAttendees[i].RowNum = parseInt(i + 1);
                if (dataRequestAttendees[i].RequestName == 'NA') {
                    var newtbblData = "<tr><td>" + dataRequestAttendees[i].RowNum + "</td><td>" + dataRequestAttendees[i].AttendeesNames + "</td><td>" + AttendeesTypeName + "</td><td>" + dataRequestAttendees[i].RequestSource + " </td><td>" + dataRequestAttendees[i].RequestName + "</td><td style='display: none'>" + dataRequestAttendees[i].AttendeesType + "</td><td style='display: none'>" + dataRequestAttendees[i].AttendEmployeeID + "</td><td><a  title='Click to Remove' onclick='DeleteAttendeesArrayRows(this," + dataRequestAttendees[i].RowNum + ")' class=\"red-clr\"><i class=\"fas fa-trash-alt\"></i></a></td></tr>";
                }
                else {
                    var newtbblData = "<tr><td>" + dataRequestAttendees[i].RowNum + "</td><td>" + dataRequestAttendees[i].AttendeesNames + "</td><td>" + AttendeesTypeName + "</td><td>" + dataRequestAttendees[i].RequestSource + " </td><td><a href='#' data-toggle='modal' data-target='#RequestDetailsModal' onclick=\"ViewRequestDetails('" + dataRequestAttendees[i].UserTrainingID + "')\"><i class='fas fa - eye'></i>" + dataRequestAttendees[i].RequestName + "</a></td><td style='display: none'>" + dataRequestAttendees[i].AttendeesType + "</td><td style='display: none'>" + dataRequestAttendees[i].AttendEmployeeID + "</td><td></td></tr>";
                }
                var allstring = newtbblData;
                tableData += allstring;
            }
        }
        $('#tblAddAttendeesList').html(tableData);
        $('#modalAddAttendees').modal('hide');
    }
}
var TrainingTypeID = 0;
var TrainingName = '';
function DeleteAttendeesArrayRows(obj, rowId) {
    ConfirmMsgBox("Are you sure want to delete.", '', function () {
        var data = dataRequestAttendees.filter(function (itemParent) { return (itemParent.RowNum == rowId); });
        $(obj).closest('tr').remove();
        dataRequestAttendees = dataRequestAttendees.filter(function (itemParent) { return (itemParent.RowNum != rowId); });
        RebuildAfterDeleteAttendees();
    });
}
function RebuildAfterDeleteAttendees() {
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
        ' <th width="150">Action</th>' +
        ' </tr>' +
        ' </thead>';
    var html1 = "</table>";

    var tableData = "";
    for (let i = 0; i < dataRequestAttendees.length; i++) {
        if (dataRequestAttendees[i].AttendeesNames != null) {
            var AttendeesTypeName = dataRequestAttendees[i].AttendeesType == 148 ? 'Mandatory' : dataRequestAttendees[i].AttendeesType == 149 ? 'Optional' : '';
            dataRequestAttendees[i].RowNum = parseInt(i + 1);
            if (dataRequestAttendees[i].RequestName == 'NA') {
                var newtbblData = "<tr><td>" + dataRequestAttendees[i].RowNum + "</td><td>" + dataRequestAttendees[i].AttendeesNames + "</td><td>" + AttendeesTypeName + "</td><td>" + dataRequestAttendees[i].RequestSource + " </td><td>" + dataRequestAttendees[i].RequestName + "</td><td style='display: none'>" + dataRequestAttendees[i].AttendeesType + "</td><td style='display: none'>" + dataRequestAttendees[i].AttendEmployeeID + "</td><td><a  title='Click to Remove' onclick='DeleteAttendeesArrayRows(this," + dataRequestAttendees[i].RowNum + ")' class=\"red-clr\"><i class=\"fas fa-trash-alt\"></i></a></td></tr>";
            }
            else {
                var newtbblData = "<tr><td>" + dataRequestAttendees[i].RowNum + "</td><td>" + dataRequestAttendees[i].AttendeesNames + "</td><td>" + AttendeesTypeName + "</td><td>" + dataRequestAttendees[i].RequestSource + " </td><td><a href='#' data-toggle='modal' data-target='#RequestDetailsModal' onclick=\"ViewRequestDetails('" + dataRequestAttendees[i].UserTrainingID + "')\"><i class='fas fa - eye'></i>" + dataRequestAttendees[i].RequestName + "</a></td><td style='display: none'>" + dataRequestAttendees[i].AttendeesType + "</td><td style='display: none'>" + dataRequestAttendees[i].AttendEmployeeID + "</td><td></td></tr>";
            }
            var allstring = newtbblData;
            tableData += allstring;
        }
    }
    $('#tblAddAttendeesList').html(tableData);
}
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
            ' <th width="200">Training Type</th>' +
            ' <th width="200">Training Name</th>' +
            ' <th width="150" >Training Mode</th>' +
            ' <th  ><span id="spLocationAndLink">Location</span></th>' +
            ' <th style="display:none"></th>' +
            ' </tr>' +
            ' </thead>';
        var html1 = "</table>";
        var tableData = "";
        var i = 0;
        if (trainingCalandarArray.length > 0) {
            for (i; i < trainingCalandarArray.length; i++) {
                //if (trainingCalandarArray[i].TrainingMode == 143) {
                //    var newtbblData = "<tr><td>" + parseInt(i + 1) + "</td><td>" + trainingCalandarArray[i].TrainingType + "</td><td>" + trainingCalandarArray[i].TrainingName + "</td><td><select onchange=\"ChangedTrainingMode(this," + i + ")\" id=\"ddlTrainingMode_" + i + "\" class=\"form-control dpselect \" tabindex=\"-1\" aria-hidden=\"true\"></select> </td><td><a href='#'target='_blank' id='txtlocation_" + parseInt(i + 1) + "'>" + trainingCalandarArray[i].Location + "</a></td><td style='display: none'>" + trainingCalandarArray[i].TrainingTypeID + "</td></tr>";
                //}
                //else {
                //    var newtbblData = "<tr><td>" + parseInt(i + 1) + "</td><td>" + trainingCalandarArray[i].TrainingType + "</td><td>" + trainingCalandarArray[i].TrainingName + "</td><td><select onchange=\"ChangedTrainingMode(this," + i + ")\" id='ddlTrainingMode_" + i + "' class=\"form-control dpselect \" tabindex=\"-1\" aria-hidden=\"true\"></select> </td><td><input type='text' id='txtlocation_" + parseInt(i + 1) + "' value='" + trainingCalandarArray[i].Location + "'></input></td><td style='display: none'>" + trainingCalandarArray[i].TrainingTypeID + "</td></tr>";
                //}
                var newtbblData = "<tr><td>" + parseInt(i + 1) + "</td><td>" + trainingCalandarArray[i].TrainingType + "</td><td>" + trainingCalandarArray[i].TrainingName + "</td><td><select onchange=\"ChangedTrainingMode(this," + i + ")\" id=\"ddlTrainingMode_" + i + "\"  class=\"form-control dpselect \" tabindex=\"-1\" ></select> </td><td><input type='text' id='txtlocation_" + parseInt(i + 1) + "' value='" + trainingCalandarArray[i].Location + "'></input></td><td style='display: none'>" + trainingCalandarArray[i].TrainingTypeId + "</td></tr>";
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
        $('#tblSelectTrainingCalendarList').html(newtbblData1 + tableData + html1);
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
    if (checkValidationOnSubmit('MandatoryAttachmentRemark') == false) {
        valid = false;
    }
    if (valid == true) {
        var loop = dataRequestAttachment.length + 1;
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
        dataRequestAttachment.push(attachmentData);
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
        for (i; i < dataRequestAttachment.length; i++) {
            dataRequestAttachment[i].RowNum = parseInt(i + 1);
            var newtbblData = "<tr><td>" + dataRequestAttachment[i].RowNum + "</td><td style='display: none'>" + dataRequestAttachment[i].AttachmentType + "</td><td>" + dataRequestAttachment[i].AttachmentTypeName + "</td><td><a target='_blank' href=" + dataRequestAttachment[i].AttachmentUrl + ">" + dataRequestAttachment[i].AttachmentActualName + "</a></td><td>" + dataRequestAttachment[i].Remark + " </td><td> <a  title='Click to Remove' onclick='DeleteDocArrayRows(this," + dataRequestAttachment[i].RowNum + ")' class=\"red-clr\"><i class=\"fas fa-trash-alt\"></i></a></td><td style=\"display: none\">" + dataRequestAttachment[i].AttachmentNewName + "</td><td style=\"display: none\">" + dataRequestAttachment[i].AttachmentUrl + "</td></tr>";
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
        var data = dataRequestAttachment.filter(function (itemParent) { return (itemParent.RowNum == id); });
        $(obj).closest('tr').remove();
        dataRequestAttachment = dataRequestAttachment.filter(function (itemParent) { return (itemParent.RowNum != id); });
        RebuildAfterDeleteAttachment();
    })
}
function RebuildAfterDeleteAttachment() {
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
    for (i; i < dataRequestAttachment.length; i++) {
        dataRequestAttachment[i].RowNum = parseInt(i + 1);
        var newtbblData = "<tr><td>" + dataRequestAttachment[i].RowNum + "</td><td style='display: none'>" + dataRequestAttachment[i].AttachmentType + "</td><td>" + dataRequestAttachment[i].AttachmentTypeName + "</td><td><a target='_blank' href=" + dataRequestAttachment[i].AttachmentUrl + ">" + dataRequestAttachment[i].AttachmentActualName + "</a></td><td>" + dataRequestAttachment[i].Remark + " </td><td> <a  title='Click to Remove' onclick='DeleteDocArrayRows(this," + dataRequestAttachment[i].RowNum + ")' class=\"red-clr\"><i class=\"fas fa-trash-alt\"></i></a></td><td style=\"display: none\">" + dataRequestAttachment[i].AttachmentNewName + "</td><td style=\"display: none\">" + dataRequestAttachment[i].AttachmentUrl + "</td></tr>";
        var allstring = newtbblData;
        tableData += allstring;
    }
    $('#tblAttachmentsList').html(tableData);
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
    //$('#txtExternalMentorName').prop('disabled', false);
    //$('#ddlMentorName').prop('disabled', false);

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
        var MentorTypeID = $('#ddlMentorType').val();
        var loop = dataRequestMentors.length + 1;
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
        //if (dataRequestMentors.length > 0) {
        //    findData = dataRequestMentors.find(obj => obj.MentorName === mentorsData.MentorName);
        //}
        //if (findData == "" || findData == undefined) {
        dataRequestMentors.push(mentorsData);
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
        document.getElementById('sptblMentorList').style.display = 'none';
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
        for (i; i < dataRequestMentors.length; i++) {
            var newtbblData = "<tr><td>" + dataRequestMentors[i].RowNum + "</td><td style='display: none'>" + dataRequestMentors[i].MentorType + "</td><td>" + dataRequestMentors[i].MentorTypeName + "</td><td>" + dataRequestMentors[i].MentorName + "</td><td>" + dataRequestMentors[i].Email + " </td><td> <a  title='Click to Remove' onclick='EditMentorsRows(this," + dataRequestMentors[i].RowNum + ")' class=\"red-clr\"><i class=\"fas fa-edit m-0\"></i></a><span> | </span><a  title='Click to Remove' onclick='DeleteMentorsRows(this," + dataRequestMentors[i].RowNum + ")' class=\"red-clr\"><i class=\"fas fa-trash-alt\"></i></a></td><td style='display: none'>" + dataRequestMentors[i].EmpID + "</td><td style='display: none'>" + dataRequestMentors[i].EmpRowId + "</td></tr>";
            var allstring = newtbblData;
            tableData += allstring;
        }
        $('#tblMentorList').html(tableData);
        $('#txtExternalMentorName').val('');
        $('#txtExternalMentorEmail').val('');
        $("#ddlMentorName").val('0').trigger('change');
        $('#txtInternalEmployeeEmailID').val('');
        if (dataRequestMentors.length > 0) {
            document.getElementById('spAddedMentorCount').innerText = dataRequestMentors.length;
        }
        else {
            document.getElementById('spAddedMentorCount').innerText = '';
        }
    }
}
function DeleteMentorsRows(obj, id) {
    ConfirmMsgBox("Are you sure want to delete.", '', function () {
        var data = dataRequestMentors.filter(function (itemParent) { return (itemParent.RowNum == id); });

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
        dataRequestMentors = dataRequestMentors.filter(function (itemParent) { return (itemParent.RowNum != id); });
        RebuildAfterDeleteMentors()
        if (dataRequestMentors.length > 0) {
            document.getElementById('spAddedMentorCount').innerText = dataRequestMentors.length;
        }
        else {
            document.getElementById('spAddedMentorCount').innerText = '';
        }

    })
}
function RebuildAfterDeleteMentors() {
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
    for (i; i < dataRequestMentors.length; i++) {
        dataRequestMentors[i].RowNum = parseInt(i + 1);
        var newtbblData = "<tr><td>" + dataRequestMentors[i].RowNum + "</td><td style='display: none'>" + dataRequestMentors[i].MentorType + "</td><td>" + dataRequestMentors[i].MentorTypeName + "</td><td>" + dataRequestMentors[i].MentorName + "</td><td>" + dataRequestMentors[i].Email + " </td><td> <a  title='Click to Remove' onclick='EditMentorsRows(this," + dataRequestMentors[i].RowNum + ")' class=\"red-clr\"><i class=\"fas fa-edit m-0\"></i></a><span> | </span><a  title='Click to Remove' onclick='DeleteMentorsRows(this," + dataRequestMentors[i].RowNum + ")' class=\"red-clr\"><i class=\"fas fa-trash-alt\"></i></a></td><td style='display: none'>" + dataRequestMentors[i].EmpID + "</td></tr>";
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
        var data = dataRequestMentors.filter(function (itemParent) { return (itemParent.RowNum == id); });
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
        document.getElementById('divAddRow').style.display = 'block';
        document.getElementById('divEditRow').style.display = 'none';

        for (var i = 0; i < dataRequestMentors.length; i++) {
            if (rowId == parseInt(i + 1)) {
                //dataRequestMentors[i].RowNum = id;
                dataRequestMentors[i].MentorType = $('#ddlMentorType').val();
                dataRequestMentors[i].MentorTypeName = $('#ddlMentorType option:selected').text();
                dataRequestMentors[i].MentorName = $('#ddlMentorType').val() == '147' ? $('#ddlMentorName option:selected').text() : $('#txtExternalMentorName').val();
                dataRequestMentors[i].Email = $('#ddlMentorType').val() == '147' ? $('#txtInternalEmployeeEmailID').val() : $('#txtExternalMentorEmail').val();
                dataRequestMentors[i].CreatedBy = loggedinUserid;
                dataRequestMentors[i].ModifiedBy = loggedinUserid;
                dataRequestMentors[i].IPAddress = IPAddress;
                dataRequestMentors[i].EmpID = $('#ddlMentorName').val();
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
        for (i; i < dataRequestMentors.length; i++) {
            var newtbblData = "<tr><td>" + dataRequestMentors[i].RowNum + "</td><td style='display: none'>" + dataRequestMentors[i].MentorType + "</td><td>" + dataRequestMentors[i].MentorTypeName + "</td><td>" + dataRequestMentors[i].MentorName + "</td><td>" + dataRequestMentors[i].Email + " </td><td> <a  title='Click to Remove' onclick='EditMentorsRows(this," + dataRequestMentors[i].RowNum + ")' class=\"red-clr\"><i class=\"fas fa-edit m-0\"></i></a><span> | </span><a  title='Click to Remove' onclick='DeleteMentorsRows(this," + dataRequestMentors[i].RowNum + ")' class=\"red-clr\"><i class=\"fas fa-trash-alt\"></i></a></td><td style='display: none'>" + dataRequestMentors[i].EmpID + "</td></tr>";
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
function ViewRequestDetails(reqId) {
    CommonAjaxMethod(virtualPath + 'HRRequest/GetHRTrainingDetails', { RequIds: reqId, inputData: 6 }, 'GET', function (response) {
        var ReqDetails = response.data.data.Table;
        if (ReqDetails.length > 0) {
            document.getElementById('lblRequestNumber1').innerHTML = ReqDetails[0].ReqNo;
            document.getElementById('lblRequestNumber2').innerHTML = ReqDetails[0].ReqNo;
            document.getElementById('lblRequestDate').innerHTML = ChangeDateFormatToddMMYYY(ReqDetails[0].ReqDate);
            document.getElementById('lblRequestName').innerHTML = ReqDetails[0].RequestedByName;
            document.getElementById('lblRequestTrainingType').innerHTML = ReqDetails[0].TrainingTypeName;
            document.getElementById('lblRequestTrainingName').innerHTML = ReqDetails[0].NameOfTraining;
            document.getElementById('lblRequestDescription').innerHTML = ReqDetails[0].TrainingDescription;
            if (ReqDetails[0].RequestedFor == "159") {
                document.getElementById('lblRequestedFor').innerHTML = "Self";
            }
            if (ReqDetails[0].RequestedFor == "160") {
                document.getElementById('lblRequestedFor').innerHTML = "Team";
            }
        }

    });
    $('#RequestDetailsModal').modal('show');
}