$(document).ready(function () {
    $(function () {
        $('.datepicker').datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: "dd-mm-yy",
            yearRange: "-90:+10"
        });

    });
    var obj = {
        ParentId: 0,
        masterTableType: 20,
        isMasterTableType: false,
        isManualTable: false,
        manualTable: 0,
        manualTableId: 0
    }
    LoadMasterDropdown('ddlPsychometricTest', obj, 'Select', false);
    BindOnboardJoiningKitProcessRequest();
});
var candodate_Status = false;
function BindOnboardJoiningKitProcessRequest() {
    CommonAjaxMethod(virtualPath + 'OnboardingRequest/BindonboardingProcessData', { CandidateId: CandidateId, inputData: 3 }, 'GET', function (response) {

        var dataJoiningKit = response.data.data.Table;
        if (dataJoiningKit.length > 0) {
            $('#txtEmployeeName').val(dataJoiningKit[0].Candidate);
            $('#txtReason').val(dataJoiningKit[0].Reason);
            $('#txtPersonalEmailID').val(dataJoiningKit[0].EmailID);
            $('#txtPEmailId').val(dataJoiningKit[0].EmailID);
            $('#txtIssueofOfferLetter').val(ChangeDateFormatToddMMYYY(dataJoiningKit[0].IssuesDOFLetter));           
            $('#txtOfferAcceptDate').val(ChangeDateFormatToddMMYYY(dataJoiningKit[0].OfferAcceptedDate));
            $('#txtSendOn').val(ChangeDateFormatToddMMYYY(dataJoiningKit[0].SendOn));
            $('#txtExpectedDateofJoining').val(ChangeDateFormatToddMMYYY(dataJoiningKit[0].ExpectedDateOfJoining));
            if (dataJoiningKit[0].EmploymentCategory == "1") {
                $('#divEndDateofContract').show();
                $('#txtEndDateofContract').attr({ 'class': 'form-control datepicker JoiningKitValidation ' })
                $('#txtEndDateofContract').val(ChangeDateFormatToddMMYYY(dataJoiningKit[0].EndDateofContract));
            }
            else if (dataJoiningKit[0].EmploymentCategory == "2") {
                $('#divEndDateofContract').hide();
                $('#txtEndDateofContract').attr({ 'class': 'form-control datepicker ' })
            }
          
            if (dataJoiningKit[0].StatusCode == "14") {
                candodate_Status = true;
            }
            if (dataJoiningKit[0].StatusCode == "15") {
                candodate_Status = true;
            }
            if (dataJoiningKit[0].StatusCode == "16") {
                candodate_Status = true;
            }
            if (dataJoiningKit[0].StatusCode > 3) {
                $("#btnReject").prop('disabled', true);
            }
            else {
                $("#btnReject").prop('disabled', false);
            }
            if (candodate_Status == true) {
                //$("#btnReject").prop('disabled', true); 
                $("#btnSendJoiningKitSubmit").prop('disabled', true); 
            }
            else {
               // $("#btnReject").prop('disabled', false);
                $("#btnSendJoiningKitSubmit").prop('disabled', false); 
            }
            if (dataJoiningKit[0].PsychometricTest != null) {
                $("#ddlPsychometricTest").val(dataJoiningKit[0].PsychometricTest).trigger('change');
            }
            if (dataJoiningKit[0].RejectedByC3 == true) {
                document.getElementById('chkRejectedbyC3').checked = true;
                //$('#chkDocumentVerified').click();
                $("#chkRejectedbyC3").prop('disabled', false);
            }
            if (dataJoiningKit[0].RejectedByCandidates == true) {
                document.getElementById('chkRejectedbyCandidates').checked = true;
                //$('#chkDocumentVerified').click();
                $("#chkRejectedbyCandidates").prop('disabled', false);
            }
            if (dataJoiningKit[0].DoNotMoveToTalentPool == true) {
                document.getElementById('chkDonotMovetoTalentPool').checked = true;
                //$('#chkDocumentVerified').click();
                $("#chkDonotMovetoTalentPool").prop('disabled', false);
            }
        }
    });
}

function SaveJoiningKitDetails(from) {
    if (from == 1) {
        $('#txtSendOn').attr({ 'class': 'form-control Mandatory ' })
        //$('.applyselect').select2("destroy");
        $('#ddlPsychometricTest').attr({ 'class': 'form-control dpselect select2-hidden-accessible Mandatorypld ' })
        $('#txtReason').attr({ 'class': 'form-control maxSize[1200] h-100 ' })
    }
    else {
        $('#txtSendOn').attr({ 'class': 'form-control  ' })
        //$('.applyselect').select2("destroy");
        $('#ddlPsychometricTest').attr({ 'class': 'form-control dpselect select2-hidden-accessible ' })
        $('#txtReason').attr({ 'class': 'form-control maxSize[1200] h-100 Mandatory' })
    }
    
    var valid = true;
    if (checkValidationOnSubmit('Mandatory') == false) {
        valid = false;
    }
    if (checkValidationOnSubmit('Mandatorypld') == false) {
        valid = false;
    }
    if (valid == true) {
        var RejectedByC3 = document.getElementById('chkRejectedbyC3');
        var RejectedByCandidates = document.getElementById('chkRejectedbyCandidates');
        var DoNotMoveToTalentPool = document.getElementById('chkDonotMovetoTalentPool');

        var JoiningKit = {
            CandidateId: CandidateId,
            EmployeeName: $('#txtEmployeeName').val(),
            EmailID: $('#txtPersonalEmailID').val(),
            IssuesDOFLetter: ChangeDateFormat($('#txtIssueofOfferLetter').val()),
            OfferAcceptedDate: ChangeDateFormat($('#txtOfferAcceptDate').val()),
            ExpectedDOJ: ChangeDateFormat($('#txtExpectedDateofJoining').val()),
            EndDateofContract: ChangeDateFormat($('#txtEndDateofContract').val()),
            PsychometricTest: $("#ddlPsychometricTest").val(),
            RejectedByC3: RejectedByC3.checked == true ? true : false,
            RejectedByCandidates: RejectedByCandidates.checked == true ? true : false,
            DoNotMoveToTalentPool: DoNotMoveToTalentPool.checked == true ? true : false,
            Reason: $('#txtReason').val(),
            SendOn: ChangeDateFormat($('#txtSendOn').val()),
            IsAccept: from
            //JoiningKitSendOn: ChangeDateFormat($('#txtJoiningKitSendOn').val())
        }
        var obj = {
            JoiningKitModel: JoiningKit
        }

        CommonAjaxMethod(virtualPath + 'OnboardingRequest/SaveJoiningKitData', obj, 'POST', function (response) {
            if ( response.ErrorMessage == 'joiningkit has been rejected to candidate.') {
                window.location.href = virtualPath + 'Onboarding/HRScreenOnboard';
            }
            else {
                window.location.href = virtualPath + 'Onboarding/PreRegistration?id=' + CandidateId;
            }
        });
    }
    //return false;
}
function RedirectToClick(View) {
    window.location.href = virtualPath + 'Onboarding/' + View + '?id=' + CandidateId;
}
