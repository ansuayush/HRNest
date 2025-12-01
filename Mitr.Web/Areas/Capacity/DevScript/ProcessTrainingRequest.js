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
    $('input, textarea, select, button').prop('disabled', true);
    $('#btnUpdate').prop('disabled', false);
    $('#btnCancel').prop('disabled', false);
    $('#txtFromDate').prop('disabled', false);
    $('#txtToDate').prop('disabled', false);
    $('#txtFromTime').prop('disabled', false);
    $('#txtToTime').prop('disabled', false);
    $('#btnRequestDetailsModalCancle').prop('disabled', false);
    
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
            Status: 3,
            EmployeeID: EMPID
        }

        var obj = {
            HrRequest: HrRequest,
            trainingHRRequestCalendar: dataTrainingRequestCalendar,
            requestHRMentors: dataRequestMentors,
            trainingHRAttendees: dataRequestAttendees,
            requestHRAttachment: dataRequestAttachment
        }
        CommonAjaxMethod(virtualPath + 'HRRequest/UpdateHRTrainingRequestProcess', obj, 'POST', function (response) {
            //FailToaster(response.SuccessMessage);
            FailToaster('Your request has been saved.');
            window.location.href = virtualPath + 'HR/TrainingRequest?id=' + 1;
        });
    }
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
            trainingHRRequestCalendar: dataTrainingRequestCalendar,
            requestHRMentors: dataRequestMentors,
            trainingHRAttendees: dataRequestAttendees,
            requestHRAttachment: dataRequestAttachment
        }
        CommonAjaxMethod(virtualPath + 'HRRequest/UpdateHRTrainingRequestProcess', obj, 'POST', function (response) {
            //FailToaster(response.SuccessMessage);
            FailToaster('Request updated successfully');
            window.location.href = virtualPath + 'HR/TrainingRequest?id=' + 1;
            //window.location.href = virtualPath + 'HR/TrainingRequestList?id=' + 1; 
        });
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
        $('#tblTrainingCalendar').html('');
        var newtbblData1 = '<table class="table mt-2 " >' +
            ' <thead>' +
            ' <tr>' +
            ' <th width="33"></th>' +
            ' <th width="33">S.No</th>' +
            ' <th width="150">Training Type</th>' +
            ' <th width="150">Training Name</th>' +
            ' <th width="150" >Training Mode</th>' +
            ' <th ></th>' +
            ' <th style="display:none"></th>' +
            ' <th style="display:none"></th>' +
            ' <th style="display:none"></th>' +
            ' <th style="display:none"></th>' +
            ' <th style="display:none"></th>' +
            ' </tr>' +
            ' </thead>';
        var html1 = "</table>";

        var tableData = "";
        for (let i = 0; i < dataArry.length; i++) {
            var newtbblData = "<tr><td><input class=\'radio-button\' id='radio_" + dataArry[i].id + "' name=\'radio\' type=\'radio\'></td><td>" + dataArry[i].RowNum + "</td><td>" + dataArry[i].TrainingTypeName + "</td><td>" + dataArry[i].TrainingName + "</td><td>" + dataArry[i].TrainingMode + " </td><td>" + dataArry[i].TraininglinkorLocation + " </td><td style='display:none'>" + dataArry[i].TrainingDesc + "</td><td style='display:none'>" + dataArry[i].TentativeFromDate + "</td><td style='display:none'>" + dataArry[i].TentativeToDate + "</td><td style='display:none'>" + dataArry[i].SupervisorAssessmentReq + "</td><td style='display:none'>" + dataArry[i].TrainingTypeid + "</td></tr>";
            var allstring = newtbblData;
            tableData += allstring;
        }
        $('#tblTrainingCalendar').html(newtbblData1 + tableData + html1);
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
                    AttendeesNames: selectedValue,
                    AttendeesTypeName: $('#ddlAttendeesType').val() == 148 ? 'Mandatory' : 'Optional',
                    AttendeesType: $('#ddlAttendeesType').val(),
                    RequestSource:  'HR' ,
                    RequestName:  'NA',
                    CreatedBy: loggedinUserid,
                    ModifiedBy: loggedinUserid,
                    IPAddress: IPAddress
                }
                dataRequestAttendees.push(rowDate);
            }
        }
        $('#tblAddAttendeesList').html('');
        var AttendeesListData = '<table class="table table-striped table-bordered dt-responsive nowrap tbltick new_width dataTable no-footer " >' +
            ' <thead>' +
            ' <tr>' +
            ' <th width="33">S.No</th>' +
            ' <th width="150">Attendees Names</th>' +
            ' <th width="150">Attendees Type</th>' +
            ' <th width="150" >Request Source</th>' +
            ' <th width="150">Request Number</th>' +
            ' <th width="150" style="display:none">Attendees TypeID</th>' +
            ' <th width="150">Status</th>' +
            ' </tr>' +
            ' </thead>';
        var Attendeeshtml = "</table>";

        var AttendeestableData = "";
        for (let i = 0; i < dataRequestAttendees.length; i++) {
            var AttendeesTypeName = dataRequestAttendees[i].AttendeesType == 148 ? 'Mandatory' : dataRequestAttendees[i].AttendeesType == 149 ? 'Optional' : '';
            var AttendeesRowData = "<tr><td>" + parseInt(i + 1) + "</td><td>" + dataRequestAttendees[i].AttendeesNames + "</td><td>" + AttendeesTypeName + "</td><td>" + dataRequestAttendees[i].RequestSource + " </td><td>" + dataRequestAttendees[i].RequestName + " </td><td style='display: none'>" + dataRequestAttendees[i].AttendeesType + "</td><td>" + dataRequestAttendees[i].Status + "</td></tr>";
            var allstring = AttendeesRowData;
            AttendeestableData += allstring;
        }
        $('#tblAddAttendeesList').html(AttendeesListData + AttendeestableData + Attendeeshtml);
        //$('#modalAddAttendees').modal('hide');
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
    //dataTrainingRequestCalendar = []
    var IsRowSelect = false;
    for (var i = 0; i < $('#tblTrainingCalendar tr').length; i++) {
        if (i > 0) {
            if (document.getElementById($('#tblTrainingCalendar tr')[i].cells[0].childNodes[0].id).checked == true) {
                for (var j = 0; j < $('#tblTrainingCalendar tr')[i].cells.length; j++) {
                    if (j > 0) {
                        if (j == 2) {
                            TrainingType = $('#tblTrainingCalendar tr')[i].cells[j].innerHTML.trim();
                        }
                        if (j == 3) {
                            TrainingName = $('#tblTrainingCalendar tr')[i].cells[j].innerHTML.trim();
                        }
                        if (j == 4) {
                            TrainingMode = $('#tblTrainingCalendar tr')[i].cells[j].innerHTML.trim() == 'Physical' ? 145 : 143;
                        }
                        if (j == 5) {
                            Location = $('#tblTrainingCalendar tr')[i].cells[j].innerHTML.trim();
                        }
                        if (j == 6) {
                            TrainingDesc = $('#tblTrainingCalendar tr')[i].cells[j].innerHTML.trim();
                        }
                        if (j == 7) {
                            TentativeFromDate = $('#tblTrainingCalendar tr')[i].cells[j].innerHTML.trim();
                        }
                        if (j == 8) {
                            TentativeToDate = $('#tblTrainingCalendar tr')[i].cells[j].innerHTML.trim();
                        }
                        if (j == 9) {
                            SupervisorAssessmentReq = $('#tblTrainingCalendar tr')[i].cells[j].innerHTML.trim();
                        }
                        if (j == 10) {
                            TrainingTypeID = $('#tblTrainingCalendar tr')[i].cells[j].innerHTML.trim();
                        }
                    }
                    IsRowSelect = true;
                }
            }
        }
    }
    if (IsRowSelect) {
        var rowDate = {
            TrainingTypeID: TrainingTypeID,
            TrainingType: TrainingType,
            TrainingName: TrainingName,
            TrainingMode: TrainingMode,
            Location: Location,
            CreatedBy: loggedinUserid,
            ModifiedBy: loggedinUserid,
            IPAddress: IPAddress
        }
        dataTrainingRequestCalendar.push(rowDate);
        $('#tblSelectTrainingCalendarList').html('');
        var newtbblData1 = '<table class="table mt-2 " >' +
            ' <thead>' +
            ' <tr>' +
            ' <th width="33">S.No</th>' +
            ' <th width="150">Training Type</th>' +
            ' <th width="150">Training Name</th>' +
            ' <th width="150" >Training Mode</th>' +
            ' <th ></th>' +
            ' <th style="display:none"></th>' +
            ' </tr>' +
            ' </thead>';
        var html1 = "</table>";
        var tableData = "";
        var i = 0;
        for (i; i < dataTrainingRequestCalendar.length; i++) {
            var newtbblData = "<tr><td>" + parseInt(i + 1) + "</td><td>" + dataTrainingRequestCalendar[i].TrainingType + "</td><td>" + dataTrainingRequestCalendar[i].TrainingName + "</td><td><select onchange=\"ChangedTrainingMode(this," + i + ")\" id=\"ddlTrainingMode_" + i + "\" class=\"form-control dpselect \" tabindex=\"-1\" aria-hidden=\"true\"></select> </td><td><input type='text' id='txtlocation_'" + parseInt(i + 1) + " value=" + dataTrainingRequestCalendar[i].Location + "></input></td><td style='display: none'>" + dataTrainingRequestCalendar[i].TrainingTypeID + "</td></tr>";
            var allstring = newtbblData;
            tableData += allstring;
        }
        $('#tblSelectTrainingCalendarList').html(tableData);
        document.getElementById('txtTrainingDescritpion').value = TrainingDesc;
        $('#txtFromDate').val(ChangeDateFormatToddMMYYY(TentativeFromDate));
        $('#txtToDate').val(ChangeDateFormatToddMMYYY(TentativeToDate));
        document.getElementById('chkSupervisorsAssessment').checked = SupervisorAssessmentReq == "true" ? true : false;
        ClickSupervisorsAssessment();
        for (var i = 0; i < dataTrainingRequestCalendar.length; i++) {
            LoadMasterDropdown('ddlTrainingMode_' + i, {
                ParentId: 0,
                masterTableType: 0,
                isMasterTableType: false,
                isManualTable: true,
                manualTable: 27,
                manualTableId: 0
            }, 'Select', false);
            $('#ddlTrainingMode_' + i).val(dataTrainingRequestCalendar[i].TrainingMode)
        }
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
    return false;
    ConfirmMsgBox("Are you sure want to delete", '', function () {
        var data = dataRequestAttachment.filter(function (itemParent) { return (itemParent.RowNum == id); });
        //CommonAjaxMethod(virtualPath + 'OnboardingRequest/DeleteListRecordNotes', { Id: id, inputData: 4 }, 'POST', function (response) {
        $(obj).closest('tr').remove();
        dataRequestAttachment = dataRequestAttachment.filter(function (itemParent) { return (itemParent.RowNum != id); });
        //});
    })
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
}
function ChangeMentorName() {

    $('#txtInternalEmployeeEmailID').val($('#ddlMentorName option:selected').attr("dataele"));
}
function AddMentorList() {
    return false;
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
        dataRequestMentors.push(mentorsData);
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
    }
}
function DeleteMentorsRows(obj, id) {
    return false;
    ConfirmMsgBox("Are you sure want to delete", '', function () {
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
        //var mentorsData = {
        //    RowNum: data.RowNum,
        //    MentorType: $('#ddlMentorType').val(),
        //    MentorTypeName: $('#ddlMentorType option:selected').text(),
        //    Name: $('#ddlMentorType').val() == '147' ? $('#ddlMentorName option:selected').text() : $('#txtExternalMentorName').val(),
        //    Email: $('#ddlMentorType').val() == '147' ? $('#txtInternalEmployeeEmailID').val() : $('#txtExternalMentorEmail').val(),
        //    CreatedBy: loggedinUserid,
        //    ModifiedBy: loggedinUserid,
        //    IPAddress: IPAddress
        //}
        //dataRequestMentors.push(mentorsData);
        //CommonAjaxMethod(virtualPath + 'OnboardingRequest/DeleteListRecordNotes', { Id: id, inputData: 4 }, 'POST', function (response) {
        $(obj).closest('tr').remove();
        dataRequestMentors = dataRequestMentors.filter(function (itemParent) { return (itemParent.RowNum != id); });
        //});
    })
}
var rowId = 0;
function EditMentorsRows(obj, id) {
    return false;
    ConfirmMsgBox("Are you sure want to delete", '', function () {
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
    }
}
function ClickSupervisorsAssessment() {
    if (document.getElementById('chkSupervisorsAssessment').checked) {
        $('#txtDateOfAssessment').attr({ 'class': 'form-control datepicker MandatoryPicker ' })
        $('#suptxtDateOfAssessment').attr({ 'className': 'red-clr' }).html('*');
    }
    else {
        $('#txtDateOfAssessment').attr({ 'class': 'form-control datepicker ' })
        $('#suptxtDateOfAssessment').attr({ 'className': 'red-clr' }).html('');
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
    if (dataTrainingRequestCalendar.length > 0) {
        for (i; i < dataTrainingRequestCalendar.length; i++) {
            dataTrainingRequestCalendar[i].TrainingMode = $('#ddlTrainingMode_0').val();
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
    CommonAjaxMethod(virtualPath + 'HRRequest/GetHRTrainingDetails', { RequIds: reqId, inputData: 4 }, 'GET', function (response) {
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