$(document).ready(function () {
    document.getElementById("divEndDateofContract").style.display = "none";
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
        masterTableType: 19,
        isMasterTableType: false,
        isManualTable: false,
        manualTable: 0,
        manualTableId: 0
    }
    LoadMasterDropdown('ddlApprovedBy', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.EmployeeWithoutLWD,
        manualTableId: 0
    }, 'Select', false);
    LoadMasterDropdown('ddlEmploymentStatus', obj, 'Select', false);
    BindOnboardProcessRequest();
});
var sourceCode = "";
var isFromResponesScreen = false;
function BindOnboardProcessRequest() {
    CommonAjaxMethod(virtualPath + 'OnboardingRequest/BindonboardingProcessData', { CandidateId: CandidateId, inputData: 2 }, 'GET', function (response) {
        var dataOffer = response.data.data.Table;
        if (dataOffer.length > 0) {
            sourceCode = dataOffer[0].StatusCode;
            if (dataOffer[0].StatusCode == "2" || dataOffer[0].StatusCode == "15") {
                $(".OfferIssued").prop('disabled', true);
                $(".JoiningKit").show();
                $(".OfferAction").hide();
                $('#divExpectedLastDateofProbation').hide();
            }
            else {
                $('#divDocumentVerified').show();
            }

            $('#hdnJobId').val(dataOffer[0].JobId);
            $('#hdnPhoneNumber').val(dataOffer[0].PhoneNumber);
            const myButton = document.getElementById('btnOfferLetterReject');
            if (dataOffer[0].StatusCode > 1) {
                myButton.disabled = true;
            }
            else {
                myButton.disabled = false;
            }

            $('#hdnfileScopeActualName').val(dataOffer[0].AttachmentActualName);
            $('#hdnfileScopeNewName').val(dataOffer[0].AttachmentNewName);
            $('#hdnfileScopeFileUrl').val(dataOffer[0].AttachmentUrl);
            document.getElementById('aUploadedFilename').href = dataOffer[0].AttachmentUrl;
            document.getElementById('aUploadedFilename').innerHTML = dataOffer[0].AttachmentActualName;
            if (dataOffer[0].AttachmentActualName == "" || dataOffer[0].AttachmentActualName == null) {
                $('#fileScopeofwork').attr({ 'class': 'form-control OfferAcceptDate ' })
            }
            else {
                $('#fileScopeofwork').attr({ 'class': 'form-control ' })
            }
            $('#txtEmployeeName').val(dataOffer[0].Candidate);
            $('#txtIssuesDateofOffer').val(ChangeDateFormatToddMMYYY(dataOffer[0].IssuesDOFLetter));
            $('#txtPersonalEmailID').val(dataOffer[0].EmailID);
            $('#txtOfferAcceptDate').val(ChangeDateFormatToddMMYYY(dataOffer[0].OfferAcceptedDate));
            $('#txtEndDateofContract').val(ChangeDateFormatToddMMYYY(dataOffer[0].EndDateOfContract));
            $('#txtsEndDateofContract').val(ChangeDateFormatToddMMYYY(dataOffer[0].EndDateOfContract));
            $('#txtExpectedDateofJoining').val(ChangeDateFormatToddMMYYY(dataOffer[0].ExpectedDateOfJoining));
            $('#txtsExpectedDateofJoining').val(ChangeDateFormatToddMMYYY(dataOffer[0].ExpectedDateOfJoining));
            $('#txtExpectedLastDateofProbation').val(ChangeDateFormatToddMMYYY(dataOffer[0].ExpectedLastDateofProbation));
            if (dataOffer[0].StatusCode == 1) {
                $('#txtAnnualSalaryBenefit').val(dataOffer[0].AnnualSalaryBenefit);
            }
            else {
                $('#txtAnnualSalaryBenefit').val(dataOffer[0].OfferedAnnualSalaryBenefit);
            }
            //$('#txtAnnualSalaryBenefit').val(dataOffer[0].AnnualSalaryBenefit);
            $('#txtTermoftheEmployee').val(dataOffer[0].TermoftheEmployee);
            $('#txtApprovedOn').val(ChangeDateFormatToddMMYYY(dataOffer[0].ApprovedOn));
            $('#txtSApprovedOn').val(ChangeDateFormatToddMMYYY(dataOffer[0].ApprovedOn));
            $('#txtSApprovedBy').val(dataOffer[0].ApprovedByName);
            if (dataOffer[0].StatusCode == "2" || dataOffer[0].StatusCode == "15") {
                $('#divCtrlApprovedOn').show();
                $('#divCtrlApprovedOnBy').show();
                $('#divOfferLetterSent').show();
                $('#divExpectedDateofJoining').hide();
                $('#divEndDateofContract').hide();
            }
            else {
                $('#divCtrlApprovedOn').hide();
                $('#divCtrlApprovedOnBy').hide();
                $('#divOfferLetterSent').hide();
                $('#divExpectedDateofJoining').show();
                $('#divEndDateofContract').show();
            }
            if (dataOffer[0].DocumentVerified == true) {
                document.getElementById('chkDocumentVerified').checked = true;
                //$('#chkDocumentVerified').click();
                $("#chkDocumentVerified").prop('disabled', false);
            }
            if (dataOffer[0].ReferenceChecked == true) {
                document.getElementById('chkReferenceChecked').checked = true;
                //$('#chkReferenceChecked').click();
                $("#chkReferenceChecked").prop('disabled', false);
            }
            if (dataOffer[0].Staff_Cat != null) {
                var v = dataOffer[0].Staff_Cat == "Term Based" ? "1" : dataOffer[0].Staff_Cat == "Core Based" ? "2" : "0";
                $("#ddlEmployeeCategory").val(v).trigger('change');
            }
            if (dataOffer[0].ApprovedBy != null) {
                $('#ddlApprovedBy').val(dataOffer[0].ApprovedBy).trigger('change');
            }
            if (dataOffer[0].EmploymentStatus != null && dataOffer[0].EmploymentStatus != '0') {
                $("#ddlEmploymentStatus").val(dataOffer[0].EmploymentStatus).trigger('change');
            }
            if (dataOffer[0].StatusCode == "14" || dataOffer[0].StatusCode == "15") {
                $("#btnPreviewOfferLetter").prop('disabled', true);
                $("#btnOfferLetterRejectRespones").prop('disabled', true);
                $("#btnOfferLetterAcceptRespones").prop('disabled', true);
                $("#btnSendJoiningKitSubmit").prop('disabled', true);
                $("#btnOfferLetterSubmitted").prop('disabled', true);
            }
            else {
                $("#btnPreviewOfferLetter").prop('disabled', false);
                $("#btnOfferLetterRejectRespones").prop('disabled', false);
                $("#btnOfferLetterAcceptRespones").prop('disabled', false);
                $("#btnOfferLetterSubmitted").prop('disabled', false);
            }
        }
    });
}
function ShowDownloadOfferLetter() {
    $('#sol').modal('show');
}
function OfferLetterAcceptRespones() {
    var valid = true;
    if (checkValidationOnSubmit('OfferAcceptDate') == false) {
        valid = false;
    }
    if (valid == true) {
        var Offer = {
            CandidateId: CandidateId,
            AttachmentActualName: $('#hdnfileScopeActualName').val(),
            AttachmentNewName: $('#hdnfileScopeNewName').val(),
            AttachmentUrl: $('#hdnfileScopeFileUrl').val(),
            OfferAcceptedDate: ChangeDateFormat($('#txtOfferAcceptDate').val()),
            ExpectedDateOfJoining: ChangeDateFormat($('#txtsExpectedDateofJoining').val()),
            EndDateOfContract: ChangeDateFormat($('#txtsEndDateofContract').val()),
            ExpectedLastDateofProbation: ChangeDateFormat($('#txtExpectedLastDateofProbation').val()),
            IsAccept: 1
        }
        var obj = {
            OfferModel: Offer
        }
        CommonAjaxMethod(virtualPath + 'OnboardingRequest/SaveAcceptRejectOffer', obj, 'POST', function (response) {
            //window.location.href = virtualPath + 'Onboarding/HRScreenOnboard';
            window.location.href = virtualPath + 'Onboarding/Joiningkit?id=' + CandidateId;
        });
    }
}
function AcceptOffer(from) {
    var valid = true;
    if (checkValidationOnSubmit('MandatoryReason') == false) {
        valid = false;
    }
    if (valid == true) {
        if (from == 2) {
            var DocumentVerified = document.getElementById('chkDocumentVerified');
            var ReferenceChecked = document.getElementById('chkReferenceChecked');
            var RejectedbyC3 = document.getElementById('chkRejectedbyC3');
            var RejectedbyCandidates = document.getElementById('chkRejectedbyCandidates');
            var DonotMovetoTalentPool = document.getElementById('chkDonotMovetoTalentPool');
            var Offer = {
                CandidateId: CandidateId,
                IsAccept: from,
                JobId: $('#hdnJobId').val(),
                EmployeeName: $('#txtEmployeeName').val(),
                Phone: $('#hdnPhoneNumber').val(),
                ExpectedDateOfJoining: ChangeDateFormat($('#txtExpectedDateofJoining').val()),
                IssuesDOFLetter: ChangeDateFormat($('#txtIssuesDateofOffer').val()),
                EmailID: $('#txtPersonalEmailID').val(),
                TypeofEmployeeCategory: '',
                TermoftheEmployee: $('#txtTermoftheEmployee').val(),
                AnnualSalaryBenefit: $('#txtAnnualSalaryBenefit').val(),
                EmploymentStatus: $('#ddlEmploymentStatus').val() == 'Select' ? 0 : $('#ddlEmploymentStatus').val(),
                EmploymentCategory: $('#ddlEmployeeCategory').val() == 'Select' ? 0 : $('#ddlEmployeeCategory').val(),
                DocumentVerified: DocumentVerified.checked == true ? true : false,
                ReferenceChecked: ReferenceChecked.checked == true ? true : false,
                AttachmentActualName: '',
                AttachmentNewName: '',
                AttachmentUrl: '',
                OfferAcceptedDate: '',
                EndDateOfContract: ChangeDateFormat($('#txtEndDateofContract').val()),
                ApprovedOn: ChangeDateFormat($('#txtApprovedOn').val()),
                ApprovedBy: $('#ddlApprovedBy').val() == 'Select' ? 0 : $('#ddlApprovedBy').val(),
                Reason: $("#txtReason").val(),
                RejectedbyC3: RejectedbyC3.checked == true ? true : false,
                RejectedbyCandidates: RejectedbyCandidates.checked == true ? true : false,
                DonotMovetoTalentPool: DonotMovetoTalentPool.checked == true ? true : false
            }
            var obj = {
                OfferModel: Offer
            }
            if (isFromResponesScreen == false) {
                CommonAjaxMethod(virtualPath + 'OnboardingRequest/SaveAcceptRejectOffer', obj, 'POST', function (response) {
                    window.location.href = virtualPath + 'Onboarding/HRScreenOnboard';
                });
            }
            if (isFromResponesScreen == true) {
                CommonAjaxMethod(virtualPath + 'OnboardingRequest/SaveRejectOfferFromRespones', obj, 'POST', function (response) {
                    window.location.href = virtualPath + 'Onboarding/HRScreenOnboard';
                });
            }
        }
    }
}
function OfferLetterReject() {
    //$('#supDL').attr({ 'className': 'red-clr' }).html('');
    $('#txtOfferAcceptDate').attr({ 'class': 'form-control datepicker ' })
    $('#rfc').modal('show');
}

function OfferLetterRejectFromRespones() {
    isFromResponesScreen = true;
    $('#rfc').modal('show');
}
function SaveBeforeDownloadOfferDetails() {
    var valid = true;
    if (checkValidationOnSubmit('Mandatory') == false) {
        valid = false;
    }
    if (checkValidationOnSubmit('Mandatorypld') == false) {
        valid = false;
    }
    if (valid == true) {
        var DocumentVerified = document.getElementById('chkDocumentVerified');
        var ReferenceChecked = document.getElementById('chkReferenceChecked');
        var RejectedbyC3 = document.getElementById('chkRejectedbyC3');
        var RejectedbyCandidates = document.getElementById('chkRejectedbyCandidates');
        var DonotMovetoTalentPool = document.getElementById('chkDonotMovetoTalentPool');

        var Offer = {
            CandidateId: CandidateId,
            JobId: $('#hdnJobId').val(),
            EmployeeName: $('#txtEmployeeName').val(),
            Phone: $('#hdnPhoneNumber').val(),
            ExpectedDateOfJoining: ChangeDateFormat($('#txtExpectedDateofJoining').val()),
            IssuesDOFLetter: ChangeDateFormat($('#txtIssuesDateofOffer').val()),
            EmailID: $('#txtPersonalEmailID').val(),
            TypeofEmployeeCategory: '',
            TermoftheEmployee: $('#txtTermoftheEmployee').val(),
            AnnualSalaryBenefit: $('#txtAnnualSalaryBenefit').val(),
            EmploymentStatus: $("#ddlEmploymentStatus").val(),
            EmploymentCategory: $("#ddlEmployeeCategory").val(),
            DocumentVerified: DocumentVerified.checked == true ? true : false,
            ReferenceChecked: ReferenceChecked.checked == true ? true : false,
            AttachmentActualName: '',
            AttachmentNewName: '',
            AttachmentUrl: '',
            OfferAcceptedDate: '',
            EndDateOfContract: ChangeDateFormat($('#txtEndDateofContract').val()),
            ApprovedOn: ChangeDateFormat($('#txtApprovedOn').val()),
            ApprovedBy: $("#ddlApprovedBy").val(),
            Reason: $("#txtReason").val(),
            RejectedbyC3: RejectedbyC3.checked == true ? true : false,
            RejectedbyCandidates: RejectedbyCandidates.checked == true ? true : false,
            DonotMovetoTalentPool: DonotMovetoTalentPool.checked == true ? true : false,
            ExpectedLastDateofProbation: ChangeDateFormat($('#txtExpectedLastDateofProbation').val()),
        }
        var obj = {
            OfferModel: Offer
        }
        CommonAjaxMethod(virtualPath + 'OnboardingRequest/SaveOfferData', obj, 'POST', function (response) {
            //window.location.href = virtualPath + 'Onboarding/HRScreenOnboard';
        });

    }
}
function SaveOfferDetails() {
    var html = "";
    if ($('#ddlEmployeeCategory').val() == 1) {
        if (CKEDITOR.instances['txtTermBasedPreviewOfferLetter']) {
            CKEDITOR.instances['txtTermBasedPreviewOfferLetter'].destroy();
        }
        html = $("#txtTermBasedPreviewOfferLetter").val();
    }
    if ($('#ddlEmployeeCategory').val() == 2) {
        if (CKEDITOR.instances['CoreBasedPreviewOfferLetter']) {
            CKEDITOR.instances['CoreBasedPreviewOfferLetter'].destroy();
        }
        html = $("#CoreBasedPreviewOfferLetter").val();
    }
    if ($('#ddlEmploymentStatus option:selected').text() == "Confirmed") {
        $('#txtExpectedLastDateofProbation').attr({ 'class': 'form-control datepicker   ' });
    }
    if ($('#ddlEmploymentStatus option:selected').text() == "On Probation") {
        $('#txtExpectedLastDateofProbation').attr({ 'class': 'form-control datepicker Mandatory   ' });
    }
    if ($('#ddlEmployeeCategory option:selected').text() == "Term Based") {
        $('#txtEndDateofContract').attr({ 'class': 'form-control datepicker Mandatory   ' });
        $('#txtTermoftheEmployee').attr({ 'class': 'form-control datepicker Mandatory   ' });
    }
    else {
        $('#txtEndDateofContract').attr({ 'class': 'form-control datepicker   ' });
        $('#txtTermoftheEmployee').attr({ 'class': 'form-control datepicker   ' });
    }

    var valid = true;
    if (checkValidationOnSubmit('Mandatory') == false) {
        valid = false;
    }
    if (checkValidationOnSubmit('Mandatorypld') == false) {
        valid = false;
    }
    if (valid == true) {
        var DocumentVerified = document.getElementById('chkDocumentVerified');
        var ReferenceChecked = document.getElementById('chkReferenceChecked');
        var RejectedbyC3 = document.getElementById('chkRejectedbyC3');
        var RejectedbyCandidates = document.getElementById('chkRejectedbyCandidates');
        var DonotMovetoTalentPool = document.getElementById('chkDonotMovetoTalentPool');

        var Offer = {
            CandidateId: CandidateId,
            JobId: $('#hdnJobId').val(),
            EmployeeName: $('#txtEmployeeName').val(),
            Phone: $('#hdnPhoneNumber').val(),
            ExpectedDateOfJoining: ChangeDateFormat($('#txtExpectedDateofJoining').val()),
            IssuesDOFLetter: ChangeDateFormat($('#txtIssuesDateofOffer').val()),
            EmailID: $('#txtPersonalEmailID').val(),
            TypeofEmployeeCategory: '',
            TermoftheEmployee: $('#txtTermoftheEmployee').val(),
            AnnualSalaryBenefit: $('#txtAnnualSalaryBenefit').val(),
            EmploymentStatus: $("#ddlEmploymentStatus").val(),
            EmploymentCategory: $("#ddlEmployeeCategory").val(),
            DocumentVerified: DocumentVerified.checked == true ? true : false,
            ReferenceChecked: ReferenceChecked.checked == true ? true : false,
            AttachmentActualName: $('#hdnfileScopeActualName').val(),
            AttachmentNewName: $('#hdnfileScopeNewName').val(),
            AttachmentUrl: $('#hdnfileScopeFileUrl').val(),
            OfferAcceptedDate: ChangeDateFormat($('#txtOfferAcceptDate').val()),
            EndDateOfContract: ChangeDateFormat($('#txtEndDateofContract').val()),
            ApprovedOn: ChangeDateFormat($('#txtApprovedOn').val()),
            ApprovedBy: $("#ddlApprovedBy").val(),
            Reason: $("#txtReason").val(),
            RejectedbyC3: RejectedbyC3.checked == true ? true : false,
            RejectedbyCandidates: RejectedbyCandidates.checked == true ? true : false,
            DonotMovetoTalentPool: DonotMovetoTalentPool.checked == true ? true : false,
            ExpectedLastDateofProbation: ChangeDateFormat($('#txtExpectedLastDateofProbation').val()),
            OfferLetterBody: html
        }
        var obj = {
            OfferModel: Offer
        }
        CommonAjaxMethod(virtualPath + 'OnboardingRequest/SaveOfferData', obj, 'POST', function (response) {
            //FailToaster('Offer letter has been saved.');
            window.location.href = virtualPath + 'Onboarding/Offer?id=' + CandidateId;
        });

    }
}
function PreSaveOfferDetails() {
    var valid = true;
    if (checkValidationOnSubmit('Mandatory') == false) {
        valid = false;
    }
    if (checkValidationOnSubmit('Mandatorypld') == false) {
        valid = false;
    }
    if (valid == true) {
        var DocumentVerified = document.getElementById('chkDocumentVerified');
        var ReferenceChecked = document.getElementById('chkReferenceChecked');
        var RejectedbyC3 = document.getElementById('chkRejectedbyC3');
        var RejectedbyCandidates = document.getElementById('chkRejectedbyCandidates');
        var DonotMovetoTalentPool = document.getElementById('chkDonotMovetoTalentPool');

        var Offer = {
            CandidateId: CandidateId,
            JobId: $('#hdnJobId').val(),
            EmployeeName: $('#txtEmployeeName').val(),
            Phone: $('#hdnPhoneNumber').val(),
            ExpectedDateOfJoining: ChangeDateFormat($('#txtExpectedDateofJoining').val()),
            IssuesDOFLetter: ChangeDateFormat($('#txtIssuesDateofOffer').val()),
            EmailID: $('#txtPersonalEmailID').val(),
            TypeofEmployeeCategory: '',
            TermoftheEmployee: $('#txtTermoftheEmployee').val(),
            AnnualSalaryBenefit: $('#txtAnnualSalaryBenefit').val(),
            EmploymentStatus: $("#ddlEmploymentStatus").val(),
            EmploymentCategory: $("#ddlEmployeeCategory").val(),
            DocumentVerified: DocumentVerified.checked == true ? true : false,
            ReferenceChecked: ReferenceChecked.checked == true ? true : false,
            AttachmentActualName: '',
            AttachmentNewName: '',
            AttachmentUrl: '',
            OfferAcceptedDate: '',
            EndDateOfContract: ChangeDateFormat($('#txtEndDateofContract').val()),
            ApprovedOn: ChangeDateFormat($('#txtApprovedOn').val()),
            ApprovedBy: $("#ddlApprovedBy").val(),
            Reason: $("#txtReason").val(),
            RejectedbyC3: RejectedbyC3.checked == true ? true : false,
            RejectedbyCandidates: RejectedbyCandidates.checked == true ? true : false,
            DonotMovetoTalentPool: DonotMovetoTalentPool.checked == true ? true : false,
            ExpectedLastDateofProbation: ChangeDateFormat($('#txtExpectedLastDateofProbation').val()),
        }
        var obj = {
            OfferModel: Offer
        }
        CommonAjaxMethod(virtualPath + 'OnboardingRequest/SaveOfferData', obj, 'POST', function (response) {
            //window.location.href = virtualPath + 'Onboarding/HRScreenOnboard';
        });

    }
}
function DownloadOfferDetails() {
    var Offer =
    {
        CandidateId: CandidateId,
        IssuesDOFLetter: ChangeDateFormat($('#txtIssuesDateofOffer').val()),
    }
    var OnboardingConsultant =
    {
        CandidateId: CandidateId
    }
    var obj = {
        OfferModel: Offer,
        OnboardingConsultantData: OnboardingConsultant
    }
    //CommonAjaxMethod(virtualPath + 'OnboardingRequest/SaveConsultant', obj, 'POST', function (response) {
    if ($('#ddlEmployeeCategory').val() == 1) {
        DownloadOfferLetter(1);
    }
    if ($('#ddlEmployeeCategory').val() == 2) {
        DownloadOfferLetter(2);
    }
}

function EmployeeCategory() {
    var selectedText = $('#ddlEmployeeCategory').find("option:selected").text();
    var selectedValue = $('#ddlEmployeeCategory').val();

    if ($('#ddlEmployeeCategory option:selected').text() == "Term Based") {
        if (sourceCode == "2") {
            $('#divEndDateofContract').hide();
            $('#divTermoftheEmployee').hide();
        }
        else {
            $('#divEndDateofContract').show();
            $('#divTermoftheEmployee').show();
        }
        $('#txtsEndDateofContract').attr({ 'class': 'form-control datepicker OfferAcceptDate ' })
    }
    else if ($('#ddlEmployeeCategory option:selected').text() == "Core Based") {
        $('#divEndDateofContract').hide();
        $('#divTermoftheEmployee').hide();
        $('#divBasedOnEmpCategory').hide();
        $('#txtsEndDateofContract').attr({ 'class': 'form-control datepicker ' })
    }
    else {
        $('#divEndDateofContract').hide();
        $('#divTermoftheEmployee').hide();
    }
    return false;
}
function EmploymentStatus() {
    //if (sourceCode == "2") {
    //    return false;
    //}
    if ($('#ddlEmploymentStatus option:selected').text() == "On Probation") {
        $('#divExpectedLastDateofProbation').show();
    }
    else {
        $('#divExpectedLastDateofProbation').hide();
    }
    return false;
}
function PreviewOfferLetter(isPreview) {
    var isEditDocument = false;
    CommonAjaxMethod(virtualPath + 'OnboardingRequest/GetOfferLetterByID', { CandidateId: CandidateId, inputData: 11 }, 'GET', function (response) {
        if (response.data.data.Table != undefined && response.data.data.Table.length > 0) {
            if (response.data.data.Table[0].OfferLetterBody != null) {
                var dataOffer = response.data.data.Table[0].OfferLetterBody;
                isEditDocument = true;
                if ($('#ddlEmployeeCategory option:selected').text() == "Term Based") {
                    if (CKEDITOR.instances['txtTermBasedPreviewOfferLetter']) {
                        CKEDITOR.instances['txtTermBasedPreviewOfferLetter'].destroy();
                    }
                    document.getElementById('TermBasedPrintDiv').innerHTML = response.data.data.Table[0].OfferLetterBody;
                    $('#modalTermBasedPreviewOfferLetter').modal('show');
                    $('#tblTermBasedPreviewOfferLetter').attr({ 'class': '' })
                    $("#txtTermBasedPreviewOfferLetter").val($('#TermBasedPrintDiv')[0].innerHTML);
                    //document.getElementById('cke_TermBasedPrintDiv').style.display = "none";
                    CKEDITOR.replace('txtTermBasedPreviewOfferLetter');
                }
                if ($('#ddlEmployeeCategory option:selected').text() == "Core Based") {
                    if (CKEDITOR.instances['CoreBasedPreviewOfferLetter']) {
                        CKEDITOR.instances['CoreBasedPreviewOfferLetter'].destroy();
                    }
                    document.getElementById('CodeBasedPrintDiv').innerHTML = response.data.data.Table[0].OfferLetterBody;
                    $('#modalCoreBasedPreviewOfferLetter').modal('show');
                    $('#tblCoreBasedPreviewOfferLetter').attr({ 'class': '' })
                    $("#CoreBasedPreviewOfferLetter").val($('#CodeBasedPrintDiv')[0].innerHTML);
                    CKEDITOR.replace('CoreBasedPreviewOfferLetter');
                }
            }
        }
    });
    if (isEditDocument == false) {
        CommonAjaxMethod(virtualPath + 'OnboardingRequest/BindonboardingProcessData', { CandidateId: CandidateId, inputData: 8 }, 'GET', function (response) {
            var dataOffer = response.data.data.Table;
            if ($('#ddlEmployeeCategory option:selected').text() == "Term Based") {
                if (CKEDITOR.instances['txtTermBasedPreviewOfferLetter']) {
                    CKEDITOR.instances['txtTermBasedPreviewOfferLetter'].destroy();
                }

                $('#modalTermBasedPreviewOfferLetter').modal('show');
                if (dataOffer.length > 0) {
                    myfunctionTermbase(dataOffer);
                    $('#tblTermBasedPreviewOfferLetter').attr({ 'class': '' })
                    $("#txtTermBasedPreviewOfferLetter").val($('#TermBasedPrintDiv')[0].innerHTML);
                    //document.getElementById('cke_TermBasedPrintDiv').style.display = "none";
                    CKEDITOR.replace('txtTermBasedPreviewOfferLetter');
                }
            }
            if ($('#ddlEmployeeCategory option:selected').text() == "Core Based") {
                if (CKEDITOR.instances['CoreBasedPreviewOfferLetter']) {
                    CKEDITOR.instances['CoreBasedPreviewOfferLetter'].destroy();
                }
                $('#modalCoreBasedPreviewOfferLetter').modal('show');
                if (dataOffer.length > 0) {
                    myfunctionCorebase(dataOffer);
                    $('#tblCoreBasedPreviewOfferLetter').attr({ 'class': '' })
                    $("#CoreBasedPreviewOfferLetter").val($('#CodeBasedPrintDiv')[0].innerHTML);
                    CKEDITOR.replace('CoreBasedPreviewOfferLetter');
                }
            }
        });
    }
}
function myfunctionTermbase(data) {
    $('#tblTermBasedPreviewOfferLetter').html('');
    var newtbblData1 = '<table style="width:100%;border: #d3d3d3 0px !important;" >' +
        ' <thead>' +
        ' <tr style="display: none;">' +
        ' <th></th>' +
        ' </tr>' +
        ' </thead>';
    var html1 = "</table>";
    var tableData = "";

    var newHeaderRow1 = "<tr><td><img id='imgTermBasedHeaderLogo' src='" + baseURL + "/assets/images/logo-c3.png' contenteditable='false'  class='offerleterlogo' /></td></tr>" + "<tr><td>&nbsp;</td></tr>";
    var detailsRow2 = "<tr><td><b>" + ChangeDateFormatToddMMYYY(data[0].CurrentDate) + "</b></td></tr>";
    var detailsRow3 = "<tr><td><b>" + data[0].Candidate + "</b></td></tr>";
    var detailsRow4 = "<tr><td style='overflow-wrap: anywhere; '><b>" + data[0].Address + "</b></br></td></tr>" + "<tr><td></td></tr>";
    var detailsRow5 = "<tr><td> Dear <b>" + data[0].Candidate + "</b></td></tr>";
    var detailsRow6 = "<tr><td style='overflow-wrap: anywhere; '> Centre for Catalyzing Change (C3) is registered as a Society under the Societies Registration Act, 1860 and has presence in India since 1987. C3 is working in the field of Reproductive Health, Youth Empowerment, Gender and Governance and Maternal Health. </td></tr>";
    var detailsRow7 = "<tr><td></td></tr>";
    var detailsRow8 = "<tr><td style='overflow-wrap: anywhere; '> We are pleased to offer you the position of <b>" + data[0].JobTitle + "</b> at our office at <b>" + data[0].Location + "</b> at an annual gross salary & benefits of  Rs. <b>" + data[0].CurrentSalary + "</b>(Rupees <b>" + price_in_words(data[0].CurrentSalary) + "</b> only). The detailed breakup of the package offered has already been shared with you on <b>" + data[0].EmailID + "</b>. You are required to join anytime on or before <b>" + ChangeDateFormatToddMMYYY(data[0].ExpectedDateOfJoining) + "</b>. </td></tr>";
    var detailsRow9 = "<tr><td></td></tr>";
    var detailsRow10 = "<tr><td style='overflow-wrap: anywhere; '>We are delighted that you have decided to join Centre for Catalyzing Change and look forward to a mutually beneficial relationship.</b> Please accord your acceptance to this offer.</td></tr>";
    var detailsRow11 = "<tr><td></td></tr>";
    var detailsRow12 = "<tr><td>We shall issue you the employment agreement on your joining C3.</td></tr>";
    var detailsRow13 = "<tr><td>Sincerely,</br> Dr. Aparajita Gogoi </td></tr>";
    var detailsRow14 = "<tr><td>Dr. Aparajita Gogoi </br>Executive Director</td></tr>";
    var detailsRow15 = "<tr><td></td></tr>";
    var emptyRow = "<tr><td></td></tr>";
    var newFooterRow1 = "<tr><td><img style='width: 90%;' id='imgTermBasedFooterLogo' src='" + baseURL + "/assets/images/WRC3.png' contenteditable='false'  class='offerleterlogo' /></td></tr>";
    var allstring = newHeaderRow1 + detailsRow2 + emptyRow + detailsRow3 + detailsRow4 + detailsRow5 + detailsRow6 + detailsRow7 + detailsRow8 + detailsRow9 + detailsRow10 + detailsRow11 + detailsRow12 + emptyRow + detailsRow13 +  newFooterRow1;
    tableData += allstring;

    $('#tblTermBasedPreviewOfferLetter').html(newtbblData1 + tableData + html1);
}
function myfunctionCorebase(data) {

    $('#tblCoreBasedPreviewOfferLetter').html('');
    var newtbblData1 = '<table  style="width:100%;border: 1px solid white;" >' +
        ' <thead>' +
        ' <tr style="display: none;">' +
        ' <th></th>' +
        ' </tr>' +
        ' </thead>';
    var html1 = "</table>";
    var tableData = "";

    var newHeaderRow1 = "<tr><td><img  id='imgCodeBasedHeaderLogo' src='" + baseURL + "/assets/images/logo-c3.png'  /></td></tr>" + "<tr><td>&nbsp;</td></tr>";
    var detailsRow2 = "<tr><td><b>" + ChangeDateFormatToddMMYYY(data[0].CurrentDate) + "</b></td></tr>";
    var detailsRow3 = "<tr><td><b>" + data[0].Candidate + "</b></td></tr>";
    var detailsRow4 = "<tr><td style='overflow-wrap: anywhere; '><b>" + data[0].Address + "</b></br></td></tr>" + "<tr><td></td></tr>";
    var detailsRow5 = "<tr><td> Dear <b>" + data[0].Candidate + "</b></td></tr>";
    var detailsRow6 = "<tr><td style='overflow-wrap: anywhere; '> Centre for Catalyzing Change (C3) is registered as a Society under the Societies Registration Act, 1860 and has presence in India since 1987. C3 is working in the field of Reproductive Health, Youth Empowerment, Gender and Governance and Maternal Health. </td></tr>";
    var detailsRow7 = "<tr><td>&nbsp;</td></tr>";
    var detailsRow8 = "<tr><td style='overflow-wrap: anywhere; '> We are pleased to offer you the position of <b>" + data[0].JobTitle + "</b> at our office at <b>" + data[0].Location + "</b> at an annual gross salary & benefits of Rs. <b>" + data[0].CurrentSalary + "</b>(Rupees Zero only). The detailed breakup of the package offered has already been shared with you on <b>" + data[0].EmailID + "</b>. You are required to join anytime on or before <b>" + ChangeDateFormatToddMMYYY(data[0].ExpectedDateOfJoining) + "</b>. </td></tr>";
    var detailsRow9 = "<tr><td>&nbsp;</td></tr>";
    var detailsRow10 = "<tr><td style='overflow-wrap: anywhere; '>We are delighted that you have decided to join Centre for Catalyzing Change and look forward to a mutually beneficial relationship.</b> Please accord your acceptance to this offer.</td></tr>";
    var detailsRow11 = "<tr><td>&nbsp;</td></tr>";
    var detailsRow12 = "<tr><td>We shall issue you the employment agreement on your joining C3.</td></tr>";
    var detailsRow13 = "<tr><td>Sincerely,</td></tr>";
    var detailsRow14 = "<tr><td style='overflow-wrap: anywhere; '>Dr. Aparajita Gogoi </br>Executive Director</td></tr>";
    var detailsRow15 = "<tr><td>Executive Director</td></tr>";
    var emptyRow = "<tr><td>&nbsp;</td></tr>";
    var newFooterRow1 = "<tr><td><img style='width:90%;' id='imgCodeBasedHeaderLogo' src='" + baseURL + "/assets/images/WRC3.png' /></td></tr>";
    var allstring = newHeaderRow1 + detailsRow2 + emptyRow + detailsRow3 + detailsRow4 + detailsRow5 + detailsRow6 + detailsRow7 + detailsRow8 + detailsRow9 + detailsRow10 + detailsRow11 + detailsRow12 + detailsRow13 + detailsRow14 + newFooterRow1;
    tableData += allstring;

    $('#tblCoreBasedPreviewOfferLetter').html(newtbblData1 + tableData + html1);
}
function DownloadEditOfferLetter(isPreview) {
    var isEditDocument = false;
    CommonAjaxMethod(virtualPath + 'OnboardingRequest/GetOfferLetterByID', { CandidateId: CandidateId, inputData: 11 }, 'GET', function (response) {
        if (response.data.data.Table.length > 0) {
            if (response.data.data.Table[0].OfferLetterBody != null) {
                isEditDocument = true;
                if ($('#ddlEmployeeCategory option:selected').text() == "Term Based") {
                    document.getElementById('TermBasedPrintDiv').innerHTML = response.data.data.Table[0].OfferLetterBody;
                    $('#modalTermBasedPreviewOfferLetter').modal('show');
                    document.getElementById("divTermBasedContent").contentEditable = "true";
                    //document.getElementById('btnTermBasedDownloadOfferLetter').style.display = "block";
                    //document.getElementById('btnCodeBasedDownloadOfferLetter').style.display = "none";
                    document.getElementById('btnCodeBasedSaveOfferLetter').style.display = "none";
                    document.getElementById('btnTermBasedSaveOfferLetter').style.display = "block";
                }
                if ($('#ddlEmployeeCategory option:selected').text() == "Core Based") {

                    document.getElementById('CodeBasedPrintDiv').innerHTML = response.data.data.Table[0].OfferLetterBody;
                    $('#modalCoreBasedPreviewOfferLetter').modal('show');
                    document.getElementById("divCodeBasedContent").contentEditable = "true";
                    //document.getElementById('btnTermBasedDownloadOfferLetter').style.display = "none";
                    //document.getElementById('btnCodeBasedDownloadOfferLetter').style.display = "block";
                    document.getElementById('btnCodeBasedSaveOfferLetter').style.display = "block";
                    document.getElementById('btnTermBasedSaveOfferLetter').style.display = "none";
                }
            }
        }
    });
    if (isEditDocument == false) {
        if (isPreview == 1) {
            document.getElementById("divTermBasedContent").contentEditable = "true";
            //document.getElementById('btnTermBasedDownloadOfferLetter').style.display = "block";
            document.getElementById('btnTermBasedSaveOfferLetter').style.display = "block";
            document.getElementById("divCodeBasedContent").contentEditable = "false";
            //document.getElementById('btnCodeBasedDownloadOfferLetter').style.display = "none";
            document.getElementById('btnCodeBasedSaveOfferLetter').style.display = "none";
        }
        if (isPreview == 2) {
            document.getElementById("divTermBasedContent").contentEditable = "false";
            //document.getElementById('btnTermBasedDownloadOfferLetter').style.display = "none";
            document.getElementById('btnTermBasedSaveOfferLetter').style.display = "none";
            document.getElementById("divCodeBasedContent").contentEditable = "true";
            //document.getElementById('btnCodeBasedDownloadOfferLetter').style.display = "block";
            document.getElementById('btnCodeBasedSaveOfferLetter').style.display = "block";
        }
        var selectedText = $('#ddlEmployeeCategory').find("option:selected").text();
        var selectedValue = $('#ddlEmployeeCategory').val();
        CommonAjaxMethod(virtualPath + 'OnboardingRequest/BindonboardingProcessData', { CandidateId: CandidateId, inputData: 8 }, 'GET', function (response) {
            var dataOffer = response.data.data.Table;
            if ($('#ddlEmployeeCategory option:selected').text() == "Term Based") {
                $('#modalTermBasedPreviewOfferLetter').modal('show');
                if (dataOffer.length > 0) {
                    var CurrentSalary = "0,00";
                    if (dataOffer[0].CurrentSalary != null && dataOffer[0].CurrentSalary != undefined) {
                        var sal = parseFloat(dataOffer[0].CurrentSalary);
                        var number = sal;
                        var formatter = new Intl.NumberFormat('en-IN');
                        CurrentSalary = formatter.format(number);
                    }
                    document.getElementById('lblTermBasedDate').innerText = ChangeDateFormatToddMMYYY(dataOffer[0].CurrentDate);
                    document.getElementById('lblTermBasedFullName').innerText = dataOffer[0].Candidate;
                    document.getElementById('lblTermBasedAddress').innerText = dataOffer[0].Address;
                    document.getElementById('lblTermBasedDearName').innerText = dataOffer[0].Candidate;
                    document.getElementById('lblTermBasedCurrentSalary').innerText = CurrentSalary;// dataOffer[0].CurrentSalary;
                    document.getElementById('lblTermBasedOfficeLocation').innerText = dataOffer[0].Location;
                    if (dataOffer[0].CurrentSalary != "") {
                        document.getElementById('lblTermBasedRupeesCurrentSalary').innerText = price_in_words(dataOffer[0].CurrentSalary);
                    } else {
                        document.getElementById('lblTermBasedRupeesCurrentSalary').innerText = "";
                    }
                    document.getElementById('lblTermBasedEmail').innerText = dataOffer[0].EmailID;
                    document.getElementById('lblTermBasedDOJ').innerText = ChangeDateFormatToddMMYYY(dataOffer[0].ExpectedDateOfJoining);
                    document.getElementById('lblTermBasedDesignation').innerText = dataOffer[0].JobTitle;
                }
            }
            if ($('#ddlEmployeeCategory option:selected').text() == "Core Based") {
                $('#modalCoreBasedPreviewOfferLetter').modal('show');
                if (dataOffer.length > 0) {
                    var CurrentSalary = "0,00";
                    if (dataOffer[0].CurrentSalary != null && dataOffer[0].CurrentSalary != undefined) {
                        var sal = parseFloat(dataOffer[0].CurrentSalary);
                        var number = sal;
                        var formatter = new Intl.NumberFormat('en-IN');
                        CurrentSalary = formatter.format(number);
                    }
                    document.getElementById('lblCoreBasedDate').innerText = ChangeDateFormatToddMMYYY(dataOffer[0].CurrentDate);
                    document.getElementById('lblCoreBasedFullName').innerText = dataOffer[0].Candidate;
                    document.getElementById('lblCoreBasedAddress').innerText = dataOffer[0].Address;
                    document.getElementById('lblCoreBasedDearName').innerText = dataOffer[0].Candidate;
                    document.getElementById('lblCoreBasedCurrentSalary').innerText = CurrentSalary;// dataOffer[0].CurrentSalary;
                    document.getElementById('lblCoreBasedOfficeLocation').innerText = dataOffer[0].Location;
                    if (dataOffer[0].CurrentSalary != "") {
                        document.getElementById('lblCoreBasedRupeesCurrentSalary').innerText = price_in_words(dataOffer[0].CurrentSalary);
                    } else {
                        document.getElementById('lblCoreBasedRupeesCurrentSalary').innerText = "";
                    }


                    document.getElementById('lblCoreBasedEmail').innerText = dataOffer[0].EmailID;
                    document.getElementById('lblCoreBasedDOJ').innerText = ChangeDateFormatToddMMYYY(dataOffer[0].ExpectedDateOfJoining);
                    document.getElementById('lblCoreBasedDesignation').innerText = dataOffer[0].JobTitle;
                }
            }
        });
    }
}

function SaveTermBasedOfferLetter(isTermOrCore) {
    var html = "";
    if (isTermOrCore == 1) {
        if (CKEDITOR.instances['txtTermBasedPreviewOfferLetter']) {
            CKEDITOR.instances['txtTermBasedPreviewOfferLetter'].destroy();
        }
        html = $("#txtTermBasedPreviewOfferLetter").val();
    }
    if (isTermOrCore == 2) {
        if (CKEDITOR.instances['CoreBasedPreviewOfferLetter']) {
            CKEDITOR.instances['CoreBasedPreviewOfferLetter'].destroy();
        }
        html = $("#CoreBasedPreviewOfferLetter").val();
    }
    var DocumentVerified = document.getElementById('chkDocumentVerified');
    var ReferenceChecked = document.getElementById('chkReferenceChecked');
    var RejectedbyC3 = document.getElementById('chkRejectedbyC3');
    var RejectedbyCandidates = document.getElementById('chkRejectedbyCandidates');
    var DonotMovetoTalentPool = document.getElementById('chkDonotMovetoTalentPool');

    var Offer = {
        CandidateId: CandidateId,
        JobId: $('#hdnJobId').val(),
        EmployeeName: $('#txtEmployeeName').val(),
        Phone: $('#hdnPhoneNumber').val(),
        ExpectedDateOfJoining: ChangeDateFormat($('#txtExpectedDateofJoining').val()),
        IssuesDOFLetter: ChangeDateFormat($('#txtIssuesDateofOffer').val()),
        EmailID: $('#txtPersonalEmailID').val(),
        TypeofEmployeeCategory: '',
        TermoftheEmployee: $('#txtTermoftheEmployee').val(),
        AnnualSalaryBenefit: $('#txtAnnualSalaryBenefit').val(),
        EmploymentStatus: $('#ddlEmploymentStatus').val() == 'Select' ? 0 : $('#ddlEmploymentStatus').val(),
        EmploymentCategory: $('#ddlEmployeeCategory').val() == 'Select' ? 0 : $('#ddlEmployeeCategory').val(),
        DocumentVerified: DocumentVerified.checked == true ? true : false,
        ReferenceChecked: ReferenceChecked.checked == true ? true : false,
        AttachmentActualName: '',
        AttachmentNewName: '',
        AttachmentUrl: '',
        OfferAcceptedDate: '',
        EndDateOfContract: ChangeDateFormat($('#txtEndDateofContract').val()),
        ApprovedOn: ChangeDateFormat($('#txtApprovedOn').val()),
        ApprovedBy: $('#ddlApprovedBy').val() == 'Select' ? 0 : $('#ddlApprovedBy').val(),
        Reason: $("#txtReason").val(),
        RejectedbyC3: RejectedbyC3.checked == true ? true : false,
        RejectedbyCandidates: RejectedbyCandidates.checked == true ? true : false,
        DonotMovetoTalentPool: DonotMovetoTalentPool.checked == true ? true : false,
        ExpectedLastDateofProbation: ChangeDateFormat($('#txtExpectedLastDateofProbation').val()),
        OfferLetterBody: html
    }
    var obj = {
        OfferModel: Offer

    }
    CommonAjaxMethod(virtualPath + 'OnboardingRequest/SaveOfferLetterBodyData', obj, 'POST', function (response) {
        FailToaster('Offer letter has been saved.');
        $('#modalTermBasedPreviewOfferLetter').modal('hide');
        $('#modalCoreBasedPreviewOfferLetter').modal('hide');
    });
}
function DownloadOfferLetter(isTermBased) {
    if (isTermBased == 1) {
        //Export2Doc('TermBasedPrintDiv');
        downloadAsDocx('TermBasedPrintDiv')
    }
    if (isTermBased == 2) {
        //Export2Doc('CodeBasedPrintDiv');
        downloadAsDocx('CodeBasedPrintDiv')
    }
}
function saveAsDocx(element) {
    // Get the HTML content
    var htmlContent = document.getElementById(element).innerHTML;

    // Convert HTML to .docx blob
    htmlDocx.asBlob(htmlContent).then(function (blob) {
        // Create a download link and trigger download
        var url = window.URL.createObjectURL(blob);
        var a = document.createElement("a");
        a.href = url;
        a.download = "document.docx";
        document.body.appendChild(a);
        a.click();
        setTimeout(function () {
            document.body.removeChild(a);
            window.URL.revokeObjectURL(url);
        }, 0);
    });
}
var fileName = 'OfferLetter.docx';
function Export2Doc(element, filename = '') {
    var html = "<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:word' xmlns='http://www.w3.org/TR/REC-html40'><head><meta><title>Export HTML To Doc</title><style>.modal-body {position: relative;    -ms-flex: 11 auto;    flex: 11 auto;    padding: 1rem;}</style></head><body>";
    var footer = "</body></html>";
    var html = html + document.getElementById(element).innerHTML + footer;


    //link url
    var url = 'data:application/vnd.ms-word;charset=utf-8,' + encodeURIComponent(html);

    //file name
    filename = filename ? filename + '.doc' : 'OfferLetter.doc';

    // Creates the  download link element dynamically
    var downloadLink = document.createElement("a");

    document.body.appendChild(downloadLink);

    //Link to the file
    downloadLink.href = url;

    //Setting up file name
    downloadLink.download = filename;

    //triggering the function
    downloadLink.click();
    //Remove the a tag after donwload starts.
    document.body.removeChild(downloadLink);
}
function downloadAsDocx(element, filename = '') {
    var html =  document.getElementById(element).innerHTML ;
    // Get the HTML content you want to convert
    const htmlContent = "<html><body>" + html +"</body></html>";

    // Convert HTML to .docx format
    const converted = htmlDocx.asBlob(htmlContent);

    // Create a blob URL for the file
    const blob = new Blob([converted], { type: 'application/vnd.openxmlformats-officedocument.wordprocessingml.document' });
    const url = URL.createObjectURL(blob);

    // Create a link and trigger the download
    const a = document.createElement('a');
    a.style.display = 'none';
    a.href = url;
    a.download = 'OfferLetter.docx';
    document.body.appendChild(a);
    a.click();
    window.URL.revokeObjectURL(url);
}
function UpdateOfferDownloadDate() {
    var Offer =
    {
        CandidateId: CandidateId,
        ApprovedOn: ChangeDateFormat($('#txtApprovedOn').val()),
        ApprovedBy: $("#ddlApprovedBy").val()
    }
    var obj = {
        OfferModel: Offer
    }
    CommonAjaxMethod(virtualPath + 'OnboardingRequest/UpdateOfferDownloadDate', obj, 'POST', function (response) {

    });

}
function UploadFileScope() {
    var fileUpload = $("#fileScopeofwork").get(0);

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
                    $('#hdnfileScopeActualName').val(result.FileModel.ActualFileName);
                    $('#hdnfileScopeNewName').val(result.FileModel.NewFileName);
                    $('#hdnfileScopeFileUrl').val(result.FileModel.FileUrl);
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
function price_in_words(price) {
    var sglDigit = ["Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine"],
        dblDigit = ["Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"],
        tensPlace = ["", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"],
        handle_tens = function (dgt, prevDgt) {
            return 0 == dgt ? "" : " " + (1 == dgt ? dblDigit[prevDgt] : tensPlace[dgt])
        },
        handle_utlc = function (dgt, nxtDgt, denom) {
            return (0 != dgt && 1 != nxtDgt ? " " + sglDigit[dgt] : "") + (0 != nxtDgt || dgt > 0 ? " " + denom : "")
        };

    var str = "",
        digitIdx = 0,
        digit = 0,
        nxtDigit = 0,
        words = [];
    if (price += "", isNaN(parseInt(price))) str = "";
    else if (parseInt(price) > 0 && price.length <= 10) {
        for (digitIdx = price.length - 1; digitIdx >= 0; digitIdx--) switch (digit = price[digitIdx] - 0, nxtDigit = digitIdx > 0 ? price[digitIdx - 1] - 0 : 0, price.length - digitIdx - 1) {
            case 0:
                words.push(handle_utlc(digit, nxtDigit, ""));
                break;
            case 1:
                words.push(handle_tens(digit, price[digitIdx + 1]));
                break;
            case 2:
                words.push(0 != digit ? " " + sglDigit[digit] + " Hundred" + (0 != price[digitIdx + 1] && 0 != price[digitIdx + 2] ? " and" : "") : "");
                break;
            case 3:
                words.push(handle_utlc(digit, nxtDigit, "Thousand"));
                break;
            case 4:
                words.push(handle_tens(digit, price[digitIdx + 1]));
                break;
            case 5:
                words.push(handle_utlc(digit, nxtDigit, "Lakh"));
                break;
            case 6:
                words.push(handle_tens(digit, price[digitIdx + 1]));
                break;
            case 7:
                words.push(handle_utlc(digit, nxtDigit, "Crore"));
                break;
            case 8:
                words.push(handle_tens(digit, price[digitIdx + 1]));
                break;
            case 9:
                words.push(0 != digit ? " " + sglDigit[digit] + " Hundred" + (0 != price[digitIdx + 1] || 0 != price[digitIdx + 2] ? " and" : " Crore") : "")
        }
        str = words.reverse().join("")
    } else str = "";
    return str

}
function PrintContent() {
    var selectedText = $('#ddlEmployeeCategory').find("option:selected").text();
    var selectedValue = $('#ddlEmployeeCategory').val();
    var DocumentContainer = "";
    if ($('#ddlEmployeeCategory option:selected').text() == "Term Based") {
        DocumentContainer = document.getElementById('TermBasedPrintDiv');
    } else if ($('#ddlEmployeeCategory option:selected').text() == "Core Based") {
        DocumentContainer = document.getElementById('CodeBasedPrintDiv');
    }
    var WindowObject = window.open("http://www.domainname.ext/path.png", "PrintWindow",
        "width=750,height=650,top=50,left=50,toolbars=no,scrollbars=yes,status=no,resizable=yes");
    WindowObject.document.write();
    WindowObject.document.write('<link rel="stylesheet" type="text/css" href="https://fonts.googleapis.com/css?family=Roboto:300,400,700&display=swap">')
    WindowObject.document.write('<link rel="stylesheet" type="text/css" href="/assets/design/css/style.css?v=1.6">')

    WindowObject.document.write('<link rel="stylesheet" type="text/css" href="/assets/design/css/stellarnav.css?v=1.6">')
    WindowObject.document.write('<link rel="stylesheet" type="text/css" href="/assets/design/css/icons.css?v=1.6">')
    WindowObject.document.write('<link rel="stylesheet" type="text/css" href="/assets/design/css/bootstrap.min.css?v=1.6">')
    WindowObject.document.write('<link rel="stylesheet" type="text/css" href="/assets/design/css/select2.min.css?v=1.6">')
    WindowObject.document.write('<link rel="stylesheet" type="text/css" href="/assets/design/css/animate.css?v=1.6">')
    WindowObject.document.write('<link rel="stylesheet" type="text/css" href="/assets/design/css/custom.css?v=1.6">')
    WindowObject.document.write('<link rel="stylesheet" type="text/css" href="/assets/design/css/responsive.css?v=1.6">')



    WindowObject.document.writeln(DocumentContainer.innerHTML);
    WindowObject.document.close();
    WindowObject.focus();
    WindowObject.print();
    WindowObject.close();
}
function RedirectToClick(View) {
    window.location.href = virtualPath + 'Onboarding/' + View + '?id=' + CandidateId;
}
function PreviewOfferLetterDocument() {
    document.getElementById('btnTermBasedSaveOfferLetter').style.display = "none";
    document.getElementById('btnCodeBasedSaveOfferLetter').style.display = "none";
    var isEditDocument = false;
    CommonAjaxMethod(virtualPath + 'OnboardingRequest/GetOfferLetterByID', { CandidateId: CandidateId, inputData: 11 }, 'GET', function (response) {
        if (response.data.data.Table[0].OfferLetterBody != null) {
            isEditDocument = true;
            if ($('#ddlEmployeeCategory option:selected').text() == "Term Based") {
                document.getElementById('TermBasedPrintDiv').innerHTML = response.data.data.Table[0].OfferLetterBody;
                document.getElementById('txtTermBasedPreviewOfferLetter').style.display = 'none'
                $('#modalTermBasedPreviewOfferLetter').modal('show');
                document.getElementById('TermBasedPrintDiv').style.display = "block";
            }
            if ($('#ddlEmployeeCategory option:selected').text() == "Core Based") {
                document.getElementById('CoreBasedPreviewOfferLetter').style.display = 'none'
                $('#modalCoreBasedPreviewOfferLetter').modal('show');
                document.getElementById('CodeBasedPrintDiv').innerHTML = response.data.data.Table[0].OfferLetterBody;
                document.getElementById('CodeBasedPrintDiv').style.display = "block";
            }
        }
    });
    if (isEditDocument == false) {
        CommonAjaxMethod(virtualPath + 'OnboardingRequest/BindonboardingProcessData', { CandidateId: CandidateId, inputData: 8 }, 'GET', function (response) {
            var dataOffer = response.data.data.Table;
            document.getElementById('btnCodeBasedSaveOfferLetter').style.display = "none";
            document.getElementById('btnTermBasedSaveOfferLetter').style.display = "none";
            if ($('#ddlEmployeeCategory option:selected').text() == "Term Based") {
                document.getElementById('txtTermBasedPreviewOfferLetter').style.display = 'none'
                $('#modalTermBasedPreviewOfferLetter').modal('show');
                if (dataOffer.length > 0) {
                    myfunctionTermbase(dataOffer);
                    document.getElementById('TermBasedPrintDiv').style.display = "block";
                }
            }
            if ($('#ddlEmployeeCategory option:selected').text() == "Core Based") {
                document.getElementById('CoreBasedPreviewOfferLetter').style.display = 'none'
                $('#modalCoreBasedPreviewOfferLetter').modal('show');
                if (dataOffer.length > 0) {
                    myfunctionCorebase(dataOffer);
                    document.getElementById('CodeBasedPrintDiv').style.display = "block";
                }
            }
        });
    }
}

function DownloadOfferLetterDocumentDetails() {
    if ($('#ddlEmployeeCategory').val() == 1) {
        DownloadOfferLetterBeforeSubmit(1)
        DownloadOfferLetter(1);        
    }
    if ($('#ddlEmployeeCategory').val() == 2) {
        DownloadOfferLetterBeforeSubmit(2)
        DownloadOfferLetter(2);
    }
}
function DownloadOfferLetterBeforeSubmit(isPreview) {
    var isEditDocument = false;
    CommonAjaxMethod(virtualPath + 'OnboardingRequest/GetOfferLetterByID', { CandidateId: CandidateId, inputData: 11 }, 'GET', function (response) {
        if (response.data.data.Table.length > 0) {
            if (response.data.data.Table[0].OfferLetterBody != null) {
                isEditDocument = true;
                if ($('#ddlEmployeeCategory option:selected').text() == "Term Based") {
                    document.getElementById('TermBasedPrintDiv').innerHTML = response.data.data.Table[0].OfferLetterBody;
                    if (CKEDITOR.instances['txtTermBasedPreviewOfferLetter']) {
                        CKEDITOR.instances['txtTermBasedPreviewOfferLetter'].destroy();
                    }
                    $('#tblTermBasedPreviewOfferLetter').attr({ 'class': '' })
                    $("#txtTermBasedPreviewOfferLetter").val($('#TermBasedPrintDiv')[0].innerHTML);
                    //document.getElementById('cke_TermBasedPrintDiv').style.display = "none";
                    CKEDITOR.replace('txtTermBasedPreviewOfferLetter');
                }
                if ($('#ddlEmployeeCategory option:selected').text() == "Core Based") {
                    document.getElementById('CodeBasedPrintDiv').innerHTML = response.data.data.Table[0].OfferLetterBody;
                    if (CKEDITOR.instances['CoreBasedPreviewOfferLetter']) {
                        CKEDITOR.instances['CoreBasedPreviewOfferLetter'].destroy();
                    }
                    document.getElementById('CodeBasedPrintDiv').innerHTML = response.data.data.Table[0].OfferLetterBody;
                    $('#tblCoreBasedPreviewOfferLetter').attr({ 'class': '' })
                    $("#CoreBasedPreviewOfferLetter").val($('#CodeBasedPrintDiv')[0].innerHTML);
                    CKEDITOR.replace('CoreBasedPreviewOfferLetter');
                }
            }
        }
    });
    if (isEditDocument == false) {
        CommonAjaxMethod(virtualPath + 'OnboardingRequest/BindonboardingProcessData', { CandidateId: CandidateId, inputData: 8 }, 'GET', function (response) {
            var dataOffer = response.data.data.Table;
            if ($('#ddlEmployeeCategory option:selected').text() == "Term Based") {
                if (CKEDITOR.instances['txtTermBasedPreviewOfferLetter']) {
                    CKEDITOR.instances['txtTermBasedPreviewOfferLetter'].destroy();
                }
                if (dataOffer.length > 0) {
                    DownloadTermbase(dataOffer);
                    $('#tblTermBasedPreviewOfferLetter').attr({ 'class': '' })
                    $("#txtTermBasedPreviewOfferLetter").val($('#TermBasedPrintDiv')[0].innerHTML);
                    document.getElementById('cke_TermBasedPrintDiv').style.display = "none";
                    CKEDITOR.replace('txtTermBasedPreviewOfferLetter');
                }
            }
            if ($('#ddlEmployeeCategory option:selected').text() == "Core Based") {
                if (dataOffer.length > 0) {
                    if (CKEDITOR.instances['CoreBasedPreviewOfferLetter']) {
                        CKEDITOR.instances['CoreBasedPreviewOfferLetter'].destroy();
                    }
                    if (dataOffer.length > 0) {
                        DownloadCorebase(dataOffer);
                        $('#tblCoreBasedPreviewOfferLetter').attr({ 'class': '' })
                        $("#CoreBasedPreviewOfferLetter").val($('#CodeBasedPrintDiv')[0].innerHTML);
                        CKEDITOR.replace('CoreBasedPreviewOfferLetter');
                    }
                }
            }
        });
    }
}
function DownloadTermbase(data) {
    $('#tblTermBasedPreviewOfferLetter').html('');
    var newtbblData1 = '<table  style="width:100%;border: 0px solid white;" >' +
        ' <thead>' +
        ' <tr style="display: none;">' +
        ' <th></th>' +
        ' </tr>' +
        ' </thead>';
    var html1 = "</table>";
    var tableData = "";

    var newHeaderRow1 = "<tr><td><img id='imgTermBasedHeaderLogo' src='" + baseURL + "/assets/images/logo-c3.png'/></td></tr>" + "<tr><td></td></tr>";
    var detailsRow2 = "<tr><td><b>" + ChangeDateFormatToddMMYYY(data[0].CurrentDate) + "</b></td></tr>";
    var detailsRow3 = "<tr><td><b>" + data[0].Candidate + "</b></td></tr>";
    var detailsRow4 = "<tr><td style='overflow-wrap: anywhere; '><b>" + data[0].Address + "</b></td></tr>" + "<tr><td></td></tr>";
    var detailsRow5 = "<tr><td> Dear <b>" + data[0].Candidate + "</b></td></tr>";
    var detailsRow6 = "<tr><td style='overflow-wrap: anywhere; '> Centre for Catalyzing Change (C3) is registered as a Society under the Societies Registration Act, 1860 and has presence in India since 1987. C3 is working in the field of Reproductive Health, Youth Empowerment, Gender and Governance and Maternal Health. </td></tr>";
    var detailsRow7 = "<tr><td>&nbsp;</td></tr>";
    var detailsRow8 = "<tr><td style='overflow-wrap: anywhere; '> We are pleased to offer you the position of <b>" + data[0].JobTitle + "</b> at our office at <b>" + data[0].Location + "</b> at an annual gross salary & benefits of Rs. <b>" + data[0].CurrentSalary + "</b>(Rupees <b>" + price_in_words(data[0].CurrentSalary) + "</b> only). The detailed breakup of the package offered has already been shared with you on <b>" + data[0].EmailID + "</b>. You are required to join anytime on or before <b>" + ChangeDateFormatToddMMYYY(data[0].ExpectedDateOfJoining) + "</b>. </td></tr>";
    var detailsRow9 = "<tr><td>&nbsp;</td></tr>";
    var detailsRow10 = "<tr ><td style='overflow-wrap: anywhere; '>We are delighted that you have decided to join Centre for Catalyzing Change and look forward to a mutually beneficial relationship.</b> Please accord your acceptance to this offer.</td></tr>";
    var detailsRow11 = "<tr><td>&nbsp;</td></tr>";
    var detailsRow12 = "<tr><td>We shall issue you the employment agreement on your joining C3.</td></tr>";
    var detailsRow13 = "<tr><td>Sincerely,</td></tr>";
    var detailsRow14 = "<tr><td style='overflow-wrap: anywhere; '>Dr. Aparajita Gogoi </br>Executive Director</td></tr>";
    var detailsRow15 = "<tr><td></td></tr>";
    var emptyRow = "<tr><td>&nbsp;</td></tr>";
    var newFooterRow1 = "<tr><td><img style='width:90% !important;' id='imgTermBasedFooterLogo' src='" + baseURL + "/assets/images/WRC3.png'/></td></tr>";
    var allstring = newHeaderRow1 + detailsRow2 + emptyRow + detailsRow3 + detailsRow4 + detailsRow5 + detailsRow6 + detailsRow7 + detailsRow8 + detailsRow9 + detailsRow10 + detailsRow11 + detailsRow12 + detailsRow13 + emptyRow + detailsRow14 + newFooterRow1;
    tableData += allstring;

    $('#tblTermBasedPreviewOfferLetter').html(newtbblData1 + tableData + html1);
}
function DownloadCorebase(data) {

    $('#tblCoreBasedPreviewOfferLetter').html('');
    var newtbblData1 = '<table  style="width:100%;border: 0px solid white;" >' +
        ' <thead>' +
        ' <tr style="display: none;">' +
        ' <th></th>' +
        ' </tr>';
    ' </thead>';
    var html1 = "</table>";
    var tableData = "";

    var newHeaderRow1 = "<tr><td><img  id='imgCodeBasedHeaderLogo' src='" + baseURL + "/assets/images/logo-c3.png'/></td></tr>" + "<tr><td>&nbsp;</td></tr>";
    var detailsRow2 = "<tr><td><b>" + ChangeDateFormatToddMMYYY(data[0].CurrentDate) + "</b></td></tr>";
    var detailsRow3 = "<tr><td><b>" + data[0].Candidate + "</b></td></tr>";
    var detailsRow4 = "<tr><td style='overflow-wrap: anywhere; '><b>" + data[0].Address + "</b></td></tr>" + "<tr><td></td></tr>";
    var detailsRow5 = "<tr><td> Dear <b>" + data[0].Candidate + "</b></td></tr>";
    var detailsRow6 = "<tr><td style='overflow-wrap: anywhere; '> Centre for Catalyzing Change (C3) is registered as a Society under the Societies Registration Act, 1860 and has presence in India since 1987. C3 is working in the field of Reproductive Health, Youth Empowerment, Gender and Governance and Maternal Health. </td></tr>";
    var detailsRow7 = "<tr><td>&nbsp;</td></tr>";
    var detailsRow8 = "<tr><td style='overflow-wrap: anywhere; '> We are pleased to offer you the position of <b>" + data[0].JobTitle + "</b> at our office at <b>" + data[0].Location + "</b> at an annual gross salary & benefits of </br> Rs. <b>" + data[0].CurrentSalary + "</b>(Rupees Zero only). The detailed breakup of the package offered has already been shared with you on <b>" + data[0].EmailID + "</b>. You are required to join anytime on or before <b>" + ChangeDateFormatToddMMYYY(data[0].ExpectedDateOfJoining) + "</b>. </td></tr>";
    var detailsRow9 = "<tr><td>&nbsp;</td></tr>";
    var detailsRow10 = "<tr><td style='overflow-wrap: anywhere; '>We are delighted that you have decided to join Centre for Catalyzing Change and look forward to a mutually beneficial relationship.</b> Please accord your acceptance to this offer.</td></tr>";
    var detailsRow11 = "<tr><td>&nbsp;</td></tr>";
    var detailsRow12 = "<tr><td>We shall issue you the employment agreement on your joining C3.</td></tr>";
    var detailsRow13 = "<tr><td>Sincerely,</td></tr>";
    var detailsRow14 = "<tr><td>Dr. Aparajita Gogoi </br>Executive Director</td></tr>";
    var detailsRow15 = "<tr><td></td></tr>";
    var emptyRow = "<tr><td>&nbsp;</td></tr>";
    var newFooterRow1 = "<tr><td><img style='width:90% !important;' id='imgCodeBasedHeaderLogo' src='" + baseURL + "/assets/images/WRC3.png'/></td></tr>";
    var allstring = newHeaderRow1 + detailsRow2 + emptyRow + detailsRow3 + detailsRow4 + detailsRow5 + detailsRow6 + detailsRow7 + detailsRow8 + detailsRow9 + detailsRow10 + detailsRow11 + detailsRow12 + detailsRow13 + emptyRow + detailsRow14 +  newFooterRow1;
    tableData += allstring;

    $('#tblCoreBasedPreviewOfferLetter').html(newtbblData1 + tableData + html1);
}
