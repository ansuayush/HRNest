var TentativeToDate = "";
var TentativeFromDate = "";
var trainingCalandarArray = []
$(document).ready(function () {
    LoadRequestedFor();
    LoadEmployees();
    $('#ddlSkills').on('change', function () {
        LoadTrainingType(this);
    });
    $('#ddlTrainingType').on('change', function () {

        if ($(this).val() === '0') {
            $('#other').css('display', 'block');
            $('#resulttm').css('display', 'none');
            $('#ReqDesc').text('');

        } else {
            LoadTrainingName(this);
            $('#other').css('display', 'none');
            $('#resulttm').css('display', 'block');
        }
        //LoadEmployees();
    });
    $('#ddltrainingName').on('change', function () {
        var obj = {
            TrainingType: $('#ddlTrainingType').val(),
            TrainingName: $('#ddltrainingName').val()
        }
        var trainingId = $('#ddltrainingName').val();
        CommonAjaxMethod(virtualPath + 'CapacityRequest/GetTrainingRequest', { id: trainingId, inputData: 2 }, 'GET', function (response) {
            response.data.data.Table;
            if (response.data.data.Table.length > 0) {
                var TrainingMode = 0;
                var strVal = response.data.data.Table[0].TrainingDesc;
                if (strVal != null) {
                    document.getElementById('lblTrainingDesc').innerText = strVal.substr(0, strVal.lastIndexOf(' ', 40));
                }
                document.getElementById('lblToolTipTrainingDesc').innerText = response.data.data.Table[0].TrainingDesc;
                if (response.data.data.Table[0].TentativeFromDate != "" && response.data.data.Table[0].TentativeFromDate != null) {
                    TentativeFromDate = ChangeDateFormatToddMMYYY(response.data.data.Table[0].TentativeFromDate);
                }
                if (response.data.data.Table[0].TentativeToDate != "" && response.data.data.Table[0].TentativeToDate != null) {
                    TentativeToDate = ChangeDateFormatToddMMYYY(response.data.data.Table[0].TentativeToDate);
                }
                if (response.data.data.Table[0].TrainingMode == "Virtual") {
                    TrainingMode = 143;
                }
                else if (response.data.data.Table[0].TrainingMode == "Physical") {
                    TrainingMode = 145;
                }
                else {
                    TrainingMode = 0;
                }
                var rowDate = {
                    TrainingTypeID: response.data.data.Table[0].TrainingTypeid,
                    TrainingType: $('#ddlTrainingType').find("option:selected").text(),
                    TrainingName: response.data.data.Table[0].TrainingName,
                    TrainingMode: TrainingMode,
                    Location: response.data.data.Table[0].TraininglinkorLocation,
                    CreatedBy: loggedinUserid,
                    ModifiedBy: loggedinUserid,
                    IPAddress: IPAddress
                }
                trainingCalandarArray.push(rowDate);
            }
        });
    });
    CommonAjaxMethod(virtualPath + 'CapacityRequest/GetMaxReqCode', null, 'GET', function (response) {
        response.data.data.Table;
        document.getElementById('lblReqNo').innerHTML = response.data.data.Table[0].ReqNo;
        document.getElementById('lblReqDate').innerHTML = ChangeDateFormatToddMMYYY(response.data.data.Table[0].ReqDate);
        document.getElementById('lblReqBy').innerHTML = loggedinUserName;
    });

    $('#ddlRequestedFor').on('change', function () {
        if ($(this).val() === '160') {
            document.getElementById('divEmployee').style.display = 'block';
            $('#ddlEmplist').attr({ 'class': 'form-control dpselect select2-hidden-accessible Mandatory ' });
            //LoadEmployees();
        }
        else {
            document.getElementById('divEmployee').style.display = 'none';
            $('#ddlEmplist').attr({ 'class': 'form-control dpselect select2-hidden-accessible ' });
        }
    });

    LoadMasterDropdown('ddlSkills',
        {
            ParentId: 0,
            masterTableType: 0,
            isMasterTableType: false,
            isManualTable: true,
            manualTable: 25,
            manualTableId: 0
        }, 'Select', false);
    if (TraingReqID == "") {
        $("#mainpnl *").prop('disabled', false);

    }
    else {

        $("#mainpnl *").prop('disabled', true);
        $("#btncancel").prop('disabled', false);

    }

});
var TrainingTypeID = 0;
function LoadEmployees() {
    TrainingTypeID = $('#ddlTrainingType').val();
    //if (TrainingTypeID != "") {
    //    EMPID = EMPID + "-" + TrainingTypeID;
    //}
    LoadMasterDropdownValueCodeWithOutDefaultValue('ddlEmplist',
        {
            ParentId: EMPID,
            masterTableType: loggedinUserid,
            isMasterTableType: false,
            isManualTable: true,
            manualTable: 62,
            manualTableId: 0
        }, 'Select', false);
}
function LoadRequestedFor() {
    LoadMasterDropdown('ddlRequestedFor',
        {
            ParentId: 0,
            masterTableType: 0,
            isMasterTableType: false,
            isManualTable: true,
            manualTable: 52,
            manualTableId: 0
        }, 'Select', false);
}

function LoadTrainingType(ctrl) {
    LoadMasterDropdown('ddlTrainingType',
        {
            ParentId: ctrl.value,
            masterTableType: 0,
            isMasterTableType: false,
            isManualTable: true,
            manualTable: 50,
            manualTableId: 0
        }, 'Select', false);
}
function LoadTrainingName(ctrl) {
    LoadMasterDropdownValueCode('ddltrainingName',
        {
            ParentId: ctrl.value,
            masterTableType: 0,
            isMasterTableType: false,
            isManualTable: true,
            manualTable: 51,
            manualTableId: 0
        }, 'Select', true);
}
function SaveTrainingRequest() {

    if (checkValidationOnSubmit('Mandatory') == true) {
        var isValid = true;

        if ($('#ddlTrainingType').val() == '0') {
            if ($('#TrainingTypeOther').val() == "" || $('#TrainingTypeOther').val() == null) {
                isValid = false;
                $('#spTrainingTypeOther').show();
            }
        }
        else {
            if ($('#ddltrainingName').val() == "" || $('#ddltrainingName').val() == null || $('#ddltrainingName').val() == 'Select') {
                isValid = false;
                $('#sptrainingName').show();
            }
        }
        var empIds = "";
        if ($('#result').val() == 'Fail') {
            if ($('#ddlEmplist :selected').length == 0) {
                isValid = false;
                $('#spddlEmplist').show();
            }
            var empIds = $('#ddlEmplist').val().join();
        }
        else {
            var empIds = EMPID;
        }

        if ($('#ddlTrainingType :selected').val() == '0') {
            var trainingNameOther = $('#TrainingTypeOther').val();

        }
        else {
            var trainingName = $('#ddltrainingName').val();
        }
        if (isValid) {
            var attendeesArray = [];
            if ($('#ddlRequestedFor').val() == '160') {
                for (var i = 0; i < $('#ddlEmplist').find(':selected').length; i++) {
                    var selectedIndex = $('#ddlEmplist').find(':selected')[i].innerHTML;
                    if (selectedIndex != 'Select') {
                        var selectedValue = selectedIndex;
                        var rowDate = {
                            AttendeesNames: selectedValue,
                            AttendeesTypeName: 'Mandatory',
                            AttendeesType: 148,
                            RequestSource: 'User',
                            RequestName: document.getElementById('lblReqNo').innerText,
                            CreatedBy: loggedinUserid,
                            ModifiedBy: loggedinUserid,
                            IPAddress: IPAddress,
                            AttendEmployeeID: $('#ddlEmplist').val()[i]
                        }
                        attendeesArray.push(rowDate);
                    }
                }
            }
            else {
                var rowDate = {
                    AttendeesNames: loggedinUserName,
                    AttendeesTypeName: 'Mandatory',
                    AttendeesType: 148,
                    RequestSource: 'Self',
                    RequestName: document.getElementById('lblReqNo').innerText,
                    CreatedBy: loggedinUserid,
                    ModifiedBy: loggedinUserid,
                    IPAddress: IPAddress,
                    AttendEmployeeID: EMPID
                }
                attendeesArray.push(rowDate);
            }

            if ($('#ddlRequestedFor').val() == '160') {
                var empIds = $('#ddlEmplist').val().join();
            }
            else {
                empIds = EMPID;
            }
            var HrRequest = {
                ReqNo: document.getElementById('lblReqNo').innerText,
                ReqDate: ChangeDateFormat(document.getElementById('lblReqDate').innerText),
                RequestedByID: loggedinUserid,
                RequestedByName: loggedinUserName,
                Source: 'Self',
                TypeOfTraining: $('#ddlTrainingType').val(),
                NameOfTraining: $('#ddltrainingName').find("option:selected").text(),
                TrainingDescription: document.getElementById('lblToolTipTrainingDesc').innerText,
                CreatedBy: loggedinUserid,
                ModifiedBy: loggedinUserid,
                IPAddress: IPAddress,
                HrOrUser: 'User',
                Status: 0,
                RequestDescription: $('#ReqDesc').val(),
                RequestedFor: $('#ddlRequestedFor').val(),
                EmployeeID: empIds,
                SkillToBeEarned: $('#ddlSkills').val(),
                FromDate: TentativeFromDate != "" || TentativeFromDate != null ? ChangeDateFormat(TentativeFromDate) : "",
                ToDate: TentativeToDate != "" || TentativeToDate != null ? ChangeDateFormat(TentativeToDate) : "",
                NameOfTrainingID: $('#ddltrainingName').val()
            }

            var obj = {
                userRequest: HrRequest,
                trainingHRAttendees: attendeesArray,
                trainingHRRequestCalendar: trainingCalandarArray
            }
            CommonAjaxMethod(virtualPath + 'CapacityRequest/SaveUserRequest', obj, 'POST', function (response) {
                //FailToaster('Your request has been submitted');
                FailToaster(response.SuccessMessage);
                if (response.ErrorMessage == 'Your request has been submitted.') {
                    window.location.href = virtualPath + 'Capacity/TrainingRequest?id=' + 1;
                }
            });
        }
    }
}




