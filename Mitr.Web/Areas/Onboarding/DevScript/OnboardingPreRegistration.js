$(document).ready(function () {
    document.getElementById('hddUserId').Value = loggedinUserid;
    $(function () {
        $('.datepicker').datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: "dd-mm-yy",
            yearRange: "-90:+10"
        });

    });
    LoadMasterDropdown('ddlDesignation', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: 19,
        manualTableId: 0
    }, 'Select', false);
    LoadMasterDropdown('ddlPrimarySupervisor', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.EmployeeWithoutLWD,
        manualTableId: 0
    }, 'Select', false);
    LoadMasterDropdown('ddlSecondarySupervisor', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.EmployeeWithoutLWD,
        manualTableId: 0
    }, 'Select', false);
    LoadMasterDropdown('ddlHRPointPerson', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.EmployeeWithoutLWD,
        manualTableId: 0
    }, 'Select', false);
    LoadMasterDropdown('ddlWorkLocation', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.MasterLocation,
        manualTableId: 0
    }, 'Select', false);
    LoadMasterDropdown('ddlUserRole', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: 16,
        manualTableId: 0
    }, 'Select', false);
    LoadMasterDropdown('ddlUserType', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: 18,
        manualTableId: 0
    }, 'Select', false);
    LoadMasterDropdown('ddlExceptionalApprover', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.EmployeeWithoutLWD,
        manualTableId: 0
    }, 'Select', false);
    LoadMasterDropdown('ddlDocumentType', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: 54,
        manualTableId: 0
    }, 'Select', false);
    LoadMasterDropdown('ddlPreRequisitesType', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: 55,
        manualTableId: 0
    }, 'Select', false);
    var obj = {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.MasterThematicArea,
        manualTableId: 0
    }
    LoadMasterDropdown('ddlThematicArea', obj, 'Select', false);
    BindOnboardPreRegistrationProcessRequest();
});
var DocArray = [];
var DocArrayId = 0;
var PreRequisitesNotes = [];
var PreRequisitesNotesId = 0;
var UploadDocumentsId = 0;
var UserManID = 0;
var EmployeeTerm = "";
var PsychometricTest = 0;
var JobId = 0;
var EndDateOfContract = "";
var ExpectedLastDateofProbation = "";
var RegID = 0;
var USER_ID = 0;
var candodate_Status = false;
function BindOnboardPreRegistrationProcessRequest() {
    CommonAjaxMethod(virtualPath + 'OnboardingRequest/BindonboardingProcessData', { CandidateId: CandidateId, inputData: 4 }, 'GET', function (response) {
        var dataPreRegistration = response.data.data.Table;
        var dataUploadingDocument = response.data.data.Table1;
        var dataPreRequisites = response.data.data.Table2;
        var dataUserMan = response.data.data.Table3;
        if (dataPreRegistration[0].StatusCode == "14") {
            candodate_Status = true;
        }
        if (dataPreRegistration[0].StatusCode == "15") {
            candodate_Status = true;
        }
        if (dataPreRegistration[0].StatusCode == "16") {
            candodate_Status = true;
        }
        if (candodate_Status == true) {
            $("#btnPreRegistrationSave").prop('disabled', true);
            $("#btnPreRegistrationSubmitted").prop('disabled', true); 
            $("#btnCreateUser").prop('disabled', true);
        }
        else {
            $("#btnPreRegistrationSave").prop('disabled', false);
            $("#btnPreRegistrationSubmitted").prop('disabled', false);
            $("#btnCreateUser").prop('disabled', false);
        }
        if (dataUserMan.length > 0) {
            UserManID = dataUserMan[0].UserManID;

        }
        if (dataUploadingDocument.length > 0) {
            BindDataUploadingDocument(dataUploadingDocument)
        }
        if (dataPreRequisites.length > 0) {
            BindDataPreRequisites(dataPreRequisites)
        }
        if (dataPreRegistration.length > 0) {
            $('#hddCVAttachId').val(dataPreRegistration[0].CVAttachId);
            $('#hddPsychometricTestId').val(dataPreRegistration[0].CVAttachId);
            document.getElementById('lblAttachments').innerText = dataPreRegistration[0].CVAttachId + dataPreRegistration[0].content_type;
            document.getElementById('lblPsychometricTestAttachments').innerText = dataPreRegistration[0].AttachmentActualName;
            PsychometricTest = dataPreRegistration[0].PsychometricTest;
            JobId = dataPreRegistration[0].JobId;
            EndDateOfContract = dataPreRegistration[0].EndDateOfContract;
            ExpectedLastDateofProbation = dataPreRegistration[0].ExpectedLastDateofProbation;
            RegID = dataPreRegistration[0].Master_Emp_ID;
            USER_ID = dataPreRegistration[0].USER_ID;
            if (dataPreRegistration[0].Emp_code != null) {
                document.getElementById('lblEmployeeCode').innerText = "Emp Code: " + dataPreRegistration[0].Emp_code;
            }
            $("#aAttachments").attr("href", virtualPath + "Attachments/" + dataPreRegistration[0].CVAttachId + dataPreRegistration[0].content_type);
            if (dataPreRegistration[0].AttachmentUrl != null && dataPreRegistration[0].AttachmentUrl.length > 0) {
                document.getElementById('divPsychometricTest').style.display = 'block';
                var path = dataPreRegistration[0].AttachmentUrl.substring(1, dataPreRegistration[0].AttachmentUrl.length);
                $("#aPsychometricTest").attr("href", virtualPath + path);
            }
            else {
                document.getElementById('divPsychometricTest').style.display = 'none';
            }

            $('#txtEmployeeName').val(dataPreRegistration[0].Candidate);
            $('#txtEmailID').val(dataPreRegistration[0].EmailID);
            $('#txtDateofBirth').val(ChangeDateFormatToddMMYYY(dataPreRegistration[0].DOB));
            $('#txtOfficialEmailID').val(dataPreRegistration[0].OfficialEmailID);
            $('#txtExpectedDateofJoining').val(ChangeDateFormatToddMMYYY(dataPreRegistration[0].ExpectedDateofJoining));
            $('#hdnfileScopeActualName').val(dataPreRegistration[0].AttachmentActualName);
            $('#hdnfileScopeNewName').val(dataPreRegistration[0].AttachmentNewName);
            $('#hdnfileScopeFileUrl').val(dataPreRegistration[0].AttachmentUrl);
            if (PsychometricTest == "138") {
                $('#fileScopeofwork').attr({ 'class': 'form-control Mandatory ' })
                $('#supMandatoryPsychometric').attr({ 'className': 'red-clr' }).html('*');
            }
            else {
                $('#fileScopeofwork').attr({ 'class': 'form-control  ' })
                $('#supMandatoryPsychometric').attr({ 'className': 'red-clr' }).html('');
            }
            if (dataPreRegistration[0].Gender != null) {
                var v = dataPreRegistration[0].Gender == 'Male' ? '1' : dataPreRegistration[0].Gender == 'Female' ? '2' : dataPreRegistration[0].Gender == 'Others' ? '3' : 0;
                $("#ddlGender").val(v).trigger('change');
            }
            if (dataPreRegistration[0].Staff_Cat != null) {
                var v = dataPreRegistration[0].Staff_Cat == "Term Based" ? "1" : dataPreRegistration[0].Staff_Cat == "Core Based" ? "2" : "0";
                $("#ddlEmployeeCategory").val(v).trigger('change');
            }
            if (dataPreRegistration[0].Designation != null) {
                $("#ddlDesignation").val(dataPreRegistration[0].Designation).trigger('change');
            }
            if (dataPreRegistration[0].PrimarySupervisor != null) {
                $("#ddlPrimarySupervisor").val(dataPreRegistration[0].PrimarySupervisor).trigger('change');
            }
            if (dataPreRegistration[0].SecondarySupervisor != null) {
                $("#ddlSecondarySupervisor").val(dataPreRegistration[0].SecondarySupervisor).trigger('change');
            }
            if (dataPreRegistration[0].HRPointPerson != null) {
                $("#ddlHRPointPerson").val(dataPreRegistration[0].HRPointPerson).trigger('change');
            }
            if (dataPreRegistration[0].MetroNonMetro != null) {
                $("#ddlMetroNonMetro").val(dataPreRegistration[0].MetroNonMetro).trigger('change');
            }
            if (dataPreRegistration[0].ExceptionalApprover != null) {
                $("#ddlExceptionalApprover").val(dataPreRegistration[0].ExceptionalApprover).trigger('change');
            }
            if (dataPreRegistration[0].WorkLocation != null) {
                $("#ddlWorkLocation").val(dataPreRegistration[0].WorkLocation).trigger('change');
            }
            if (dataPreRegistration[0].ThematicArea != null) {
                if (dataPreRegistration[0].ThematicArea.length > 0) {
                    let pdPlaceOfOriginArray = dataPreRegistration[0].ThematicArea.split(',');
                    $("#ddlThematicArea").select2({
                        multiple: true,
                    });
                    $('#ddlThematicArea').val(pdPlaceOfOriginArray).trigger('change');
                }
            }
            if (dataPreRegistration[0].DocumentVerified == true) {
                document.getElementById('chkDocumentVerified').checked = true;
                //$('#chkDocumentVerified').click();
                $("#chkDocumentVerified").prop('disabled', false);
            }
            if (dataPreRegistration[0].ReferenceChecked == true) {
                document.getElementById('chkReferenceChecked').checked = true;
                //$('#chkReferenceChecked').click();
                $("#chkReferenceChecked").prop('disabled', false);
            }
            if (dataPreRegistration[0].MitrUser == true) {
                document.getElementById('mtruser').checked = true;
                document.getElementById('btnCreateUser').style.display = 'block';
                $('#txtOfficialEmailID').attr({ 'class': 'form-control ' })
                //$('#chkReferenceChecked').click();
                $("#mtruser").prop('disabled', false);
                BindUserDetails();
                MtrUserChange();
            }
            else {
                document.getElementById('btnCreateUser').style.display = 'none';
                $('#txtOfficialEmailID').attr({ 'class': 'form-control Mandatory ' })
            }
        }
    });
}
function MtrUserChange() {
    var mtruser = document.getElementById('mtruser');
    if (mtruser.checked == true) {
        $('#supUserType').attr({ 'className': 'red-clr' }).html('*');
        $('#ddlUserType').attr({ 'class': 'form-control dpselect select2-hidden-accessible Mandatoryddl' })

        $('#supUserLoginId').attr({ 'className': 'red-clr' }).html('*');
        $('#txtUserLoginId').attr({ 'class': 'form-control MandatoryUser ' })

        $('#supUserName').attr({ 'className': 'red-clr' }).html('*');
        $('#txtUserName').attr({ 'class': 'form-control MandatoryUser ' })

        $('#supUserEmailId').attr({ 'className': 'red-clr' }).html('*');
        $('#txtUserEmailId').attr({ 'class': 'form-control MandatoryUser ' })

        $('#supUserPassword').attr({ 'className': 'red-clr' }).html('*');
        $('#txtUserPassword').attr({ 'class': 'form-control MandatoryUser ' })

        $('#supUserRole').attr({ 'className': 'red-clr' }).html('*');
        $('#ddlUserRole').attr({ 'class': 'form-control dpselect select2-hidden-accessible Mandatoryddl' })
    }
    else {
        $('#supUserType').attr({ 'className': 'red-clr' }).html('');
        $('#ddlUserType').attr({ 'class': 'form-control dpselect select2-hidden-accessible' })

        $('#supUserLoginId').attr({ 'className': 'red-clr' }).html('');
        $('#txtUserLoginId').attr({ 'class': 'form-control' })

        $('#supUserName').attr({ 'className': 'red-clr' }).html('');
        $('#txtUserName').attr({ 'class': 'form-control' })

        $('#supUserEmailId').attr({ 'className': 'red-clr' }).html('');
        $('#txtUserEmailId').attr({ 'class': 'form-control' })

        $('#supUserPassword').attr({ 'className': 'red-clr' }).html('');
        $('#txtUserPassword').attr({ 'class': 'form-control' })

        $('#supUserRole').attr({ 'className': 'red-clr' }).html('');
        $('#ddlUserRole').attr({ 'class': 'form-control dpselect select2-hidden-accessible' })
    }
}
function BindUserDetails() {
    CommonAjaxMethod(virtualPath + 'OnboardingRequest/GetUserDetails', { id: USER_ID }, 'GET', function (response) {
        var UserTypeID = response.data.UserTypeList.filter(function (itemParent) { return (itemParent.Name == 'Employee'); })[0].ID;
        $("#ddlUserType").val(UserTypeID).trigger('change');
        if (response.data.RoleIDs != null) {
            if (response.data.RoleIDs.length > 0) {
                let pdPlaceOfOriginArray = response.data.RoleIDs.split(',');
                $("#ddlUserRole").select2({
                    multiple: true,
                });
                $('#ddlUserRole').val(pdPlaceOfOriginArray).trigger('change');
            }
        }
        $('#txtUserLoginId').val(response.data.user_name);
        $('#txtUserName').val(response.data.Name);
        $('#txtUserEmailId').val(response.data.email);
        $('#txtUserPassword').val(response.data.password);

        //$('#adduser').modal('show');
    });
}
function GetUserDetails() {
     ;
    if (USER_ID != null) {
        CommonAjaxMethod(virtualPath + 'OnboardingRequest/GetUserDetails', { id: USER_ID }, 'GET', function (response) {
            var UserTypeID = response.data.UserTypeList.filter(function (itemParent) { return (itemParent.Name == 'Employee'); })[0].ID;
            $("#ddlUserType").val(UserTypeID).trigger('change');
            if (response.data.RoleIDs != null) {
                if (response.data.RoleIDs.length > 0) {
                    let pdPlaceOfOriginArray = response.data.RoleIDs.split(',');
                    $("#ddlUserRole").select2({
                        multiple: true,
                    });
                    $('#ddlUserRole').val(pdPlaceOfOriginArray).trigger('change');
                }
            }
            $('#txtUserLoginId').val(response.data.user_name);
            $('#txtUserName').val(response.data.Name);
            $('#txtUserEmailId').val(response.data.email);
            $('#txtUserPassword').val(response.data.password);


        });
    }
    //document.getElementById('supMsgUserLoginId').innerText = "";
    $('#adduser').modal('show');
}
function ShowUserDetails() {
    $('#adduser').modal('show');
}
function SavePreRegistrationDetails() {
    debugger;
    if (PsychometricTest == "138") {
        if ($('#hdnfileScopeActualName').val() == "") {
            $('#fileScopeofwork').attr({ 'class': 'form-control Mandatory ' })
        }
        else {
            $('#fileScopeofwork').attr({ 'class': 'form-control  ' })
        }
        //$('#fileScopeofwork').attr({ 'class': 'form-control Mandatory ' })
        //$('#supMandatoryPsychometric').attr({ 'className': 'red-clr' }).html('*');
    }
    else {
        $('#fileScopeofwork').attr({ 'class': 'form-control  ' })
        $('#supMandatoryPsychometric').attr({ 'className': 'red-clr' }).html('');
    }

    //var valid = true;
    //if (checkValidationOnSubmit('Mandatory') == false) {
    //    valid = false;
    //}
    //if (checkValidationOnSubmit('Mandatorypld') == false) {
    //    valid = false;
    //}
    //if (valid == true) {
    var DocumentVerified = document.getElementById('chkDocumentVerified');
    var ReferenceChecked = document.getElementById('chkReferenceChecked');
    var mtruser = document.getElementById('mtruser');
    EmployeeTerm = $("#ddlEmployeeCategory").val() == 1 ? "TermBase" : $("#ddlEmployeeCategory").val() == 2 ? "CoreBase" : "";
    var registractionDetails = {
        ID: RegID,
        USER_ID: USER_ID,
        Emp_Name: $('#txtEmployeeName').val(),
        email: $('#txtEmailID').val(),
        DOJ: formatDate(ChangeDateFormat($('#txtExpectedDateofJoining').val())),
        EmploymentTerm: EmployeeTerm,
        Contract_EndDate: formatDate(EndDateOfContract),
        DepartmentID: 4,
        thematicarea_IDs: $('#ddlThematicArea').val().join(),
        WorkLocationID: $('#ddlWorkLocation').val(),
        metro: $('#ddlMetroNonMetro').val(),
        design_id: $("#ddlDesignation").val(),
        JobID: JobId,
        emp_status: 'Confirmed',
        Probation_EndDate: formatDate(ExpectedLastDateofProbation),
        doc: '',// formatDate(ExpectedLastDateofProbation),
        hod_name: 454,
        SecondaryHODID: 'a',
        ed_name: 'a',
        HRID: $('#ddlHRPointPerson').val(),
        AppraiserID: 229,
        co_ot: 'a',
        ResidentialStatus: 'a',
        SkillsIDs: '1',
        NoticePeriod: '1',
        dor: '',// formatDate(ExpectedLastDateofProbation),
        lastworking_day: '',// formatDate(ExpectedLastDateofProbation),
        PsychometricTest: PsychometricTest,
        Personalemail: $('#txtEmailID').val()
    };
    var addUser = {
        ID: USER_ID,
        CandidateId: CandidateId,
        UserType: $('#ddlUserType :selected').text(),
        user_name: $('#txtUserLoginId').val(),
        Name: $('#txtUserName').val(),
        email: $('#txtUserEmailId').val(),
        password: $('#txtUserPassword').val(),
        RoleIDs: $('#ddlUserRole').val().join()
    }

    var obj = {
        AddUser: mtruser.checked == true ? addUser : null,
        RegistrationDet: registractionDetails
    }
    CommonAjaxMethod(virtualPath + 'OnboardingRequest/SaveRegistrationDetails', obj, 'POST', function (response) {

        UserManID = response.ID;
        var PreRegistration = {
            CandidateId: CandidateId,
            EmployeeName: $('#txtEmployeeName').val(),
            EmailID: $('#txtEmailID').val(),
            Gender: $('#ddlGender').val() == 'Select' ? 0 : $('#ddlGender').val(),
            DateofBirth: ChangeDateFormat($('#txtDateofBirth').val()),
            MitrUser: mtruser.checked == true ? true : false,
            OfficialEmailID: $('#txtOfficialEmailID').val(),
            ExpectedDateofJoining: ChangeDateFormat($('#txtExpectedDateofJoining').val()),
            Designation: $('#ddlDesignation').val() == 'Select' ? 0 : $('#ddlDesignation').val(),
            PrimarySupervisor: $('#ddlPrimarySupervisor').val() == 'Select' ? 0 : $('#ddlPrimarySupervisor').val(),
            SecondarySupervisor: $('#ddlSecondarySupervisor').val() == 'Select' ? 0 : $('#ddlSecondarySupervisor').val(),
            ExceptionalApprover: $('#ddlExceptionalApprover').val() == 'Select' ? 0 : $('#ddlExceptionalApprover').val(),
            HRPointPerson: $('#ddlHRPointPerson').val() == 'Select' ? 0 : $('#ddlHRPointPerson').val(),
            WorkLocation: $('#ddlWorkLocation').val() == 'Select' ? 0 : $('#ddlWorkLocation').val(),
            MetroNonMetro: $('#ddlMetroNonMetro').val() == 'Select' ? 0 : $('#ddlMetroNonMetro').val(),
            ThematicArea: $('#ddlThematicArea').val() == 'Select' ? 0 : $('#ddlThematicArea').val().join(),
            //OfferLetter: $('#txtPersonalEmailID').val(),
            CV: $('#lblAttachments').text(),
            DocumentVerified: DocumentVerified.checked == true ? true : false,
            ReferenceChecked: ReferenceChecked.checked == true ? true : false,
            UploadingDocumentID: 0,
            PreRequisitesID: 0,
            AttachmentActualName: $('#hdnfileScopeActualName').val(),
            AttachmentNewName: $('#hdnfileScopeNewName').val(),
            AttachmentUrl: $('#hdnfileScopeFileUrl').val(),
            Master_Emp_ID: response.ID
        }
        var obj = {
            PreRegistrationModel: PreRegistration,
            UploadingDocumentModel: DocArray,
            PreRequisitesModel: PreRequisitesNotes
        }

        CommonAjaxMethod(virtualPath + 'OnboardingRequest/SavePreRegistrationData', obj, 'POST', function (response) {
            FailToaster("Pre-Registration details has been saved.");
            var user = {
                UserManID: UserManID,
                CandidateId: CandidateId,
                Status: 0,
                CreatedBy: loggedinUserid,
                ModifiedBy: loggedinUserid
            }
            var obj = {
                OnboardingUsers: user
            }
            if (mtruser.checked == true) {
                CommonAjaxMethod(virtualPath + 'OnboardingRequest/SaveOnboardMitrUserData', obj, 'POST', function (response) {
                    FailToaster("Pre-Registration details has been saved.");
                    //FailToaster("Updated Successfully.");
                    //window.location.href = virtualPath + 'Onboarding/HRScreenOnboard';
                    window.location.reload();
                });
            }
            else {
                window.location.reload();
            }
        });
    });

    //}
}
function BindDataUploadingDocument(dataUploadingDocument) {
    var dataArry = dataUploadingDocument
    for (let i = 0; i < dataArry.length; i++) {
        DocArrayId = DocArrayId + 1;
        var loop = DocArrayId;
        var objarrayinner =
        {
            ID: loop,
            CandidateId: CandidateId,
            DocumentType: dataArry[i].DocumentType,
            Attachment: dataArry[i].Attachment,
            AttachmentActualName: dataArry[i].AttachmentActualName,
            AttachmentNewName: dataArry[i].AttachmentNewName,
            AttachmentUrl: dataArry[i].AttachmentUrl,
            Description: dataArry[i].Description,
            Action: dataArry[i].Action,
            DocumentId: dataArry[i].ID
        }

        DocArray.push(objarrayinner);
    }
    dataArry = DocArray;

    $('#tblUploadingDocument').html('');
    var newtbblData1 = '<table id="tblPreRegistrationUploadingDocuments" class="table table-striped m-0 " >' +
        ' <thead>' +
        ' <tr>' +
        ' <th width="33">S.No</th>' +
        ' <th width="150">Document Type</th>' +
        ' <th width="150">Attachment</th>' +
        ' <th>Description</th>' +
        ' <th width="33" class=" text-center">Action</th>' +
        ' </tr>' +
        ' </thead>';
    var html1 = "</table>";
    var tableData = "";
    for (let i = 0; i < dataArry.length; i++) {
        var newtbblData = "<tr><td>" + parseInt(i + 1) + "</td><td>" + dataArry[i].DocumentType + "</td><td><a target='_blank' href=" + dataArry[i].AttachmentUrl + ">" + dataArry[i].AttachmentActualName + "</a></td><td>" + dataArry[i].Description + "</td><td class='text-center'><a style=\"text-align:right\" class='HideClass'  title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='deleteDocArrayRows(this," + dataArry[i].DocumentId + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr>";
        var allstring = newtbblData;
        tableData += allstring;
    }
    $('#tblUploadingDocument').html(newtbblData1 + tableData + html1);
    HtmlPagingUploadingDocuments();
}
function HtmlPagingUploadingDocuments() {
    $('#tblPreRegistrationUploadingDocuments').after('<div id="divNav" style="text-align:right"></div>');
    var rowsShown = 1;
    var rowsTotal = $('#tblPreRegistrationUploadingDocuments tbody tr').length;
    var numPages = rowsTotal / rowsShown;
    for (i = 0; i < numPages; i++) {
        var pageNum = i + 1;
        $('#divNav').append('<a class="e-link e-numericitem e-spacing e-currentitem e-active" href="#" rel="' + i + '">' + pageNum + '</a> ');
    }
    $('#tblPreRegistrationUploadingDocuments tbody tr').hide();
    $('#tblPreRegistrationUploadingDocuments tbody tr').slice(0, rowsShown).show();
    $('#divNav a:first').addClass('active');
    $('#divNav a').bind('click', function () {
        $('#divNav a').removeClass('active');
        $(this).addClass('active');
        var currPage = $(this).attr('rel');
        var startItem = currPage * rowsShown;
        var endItem = startItem + rowsShown;
        $('#tblPreRegistrationUploadingDocuments tbody tr').css('opacity', '0.0').hide().slice(startItem, endItem).
            css('display', 'table-row').animate({ opacity: 1 }, 300);
    });
}
function AddDocuments() {
    debugger;
    if (parseInt($("#ddlDocumentType").val()) > 0) {
        var valid = true;
        if (checkValidationOnSubmit('MandatoryDescription') == false) {
            valid = false;
        }
        if (valid == true) {
            var fileInput = document.getElementById('fileAttachmentScopeofwork');
            var filename = fileInput.files[0].name;
            DocArrayId = DocArrayId + 1;
            var loop = DocArrayId;
            var ddlDocumentType = $("#ddlDocumentType option:selected").text();
            var objarrayinner =
            {
                ID: loop,
                CandidateId: CandidateId,
                DocumentType: ddlDocumentType,
                Attachment: filename,
                AttachmentActualName: $('#hdnfileAttachmentScopeActualName').val(),
                AttachmentNewName: $('#hdnfileAttachmentScopeNewName').val(),
                AttachmentUrl: $('#hdnfileAttachmentScopeFileUrl').val(),
                Description: $('#txtDescription').val(),
                Action: 'Remove',
                DocumentId: loop
            }

            DocArray.push(objarrayinner);
            var dataArry = DocArray;
            $('#tblUploadingDocument').html('');
            var newtbblData1 = '<table id="tblPreRegistrationUploadingDocuments" class="table table-striped m-0 " >' +
                ' <thead>' +
                ' <tr>' +
                ' <th width="33">S.No</th>' +
                ' <th width="150">Document Type</th>' +
                ' <th width="150">Attachment</th>' +
                ' <th>Description</th>' +
                ' <th width="50" class=" text-center">Action</th>' +
                ' </tr>' +
                ' </thead>';
            var html1 = "</table>";
            var tableData = "";
            for (let i = 0; i < dataArry.length; i++) {
                var newtbblData = "<tr><td>" + parseInt(i + 1) + "</td><td>" + dataArry[i].DocumentType + "</td><td><a target='_blank' href=" + dataArry[i].AttachmentUrl + ">" + dataArry[i].AttachmentActualName + "</a></td><td>" + dataArry[i].Description + "</td><td class='text-center' ><a class='HideClass'  title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='deleteDocArrayRows(this," + dataArry[i].DocumentId + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a> </td></tr>";
                var allstring = newtbblData;
                tableData += allstring;
            }
            $('#tblUploadingDocument').html(newtbblData1 + tableData + html1);
            $('#txtDescription').val('');
            $("#ddlDocumentType").val('0').trigger('change');
            $('#fileAttachmentScopeofwork').val('');
            HtmlPagingUploadingDocuments();
        }
    }
}
function AddRequisitesNotes() {
    debugger;
    var ddlPreRequisitesType = $("#ddlPreRequisitesType option:selected").text();
    if (parseInt($("#ddlPreRequisitesType").val()) > 0) {
        var dataArry = PreRequisitesNotes
        PreRequisitesNotesId = PreRequisitesNotesId + 1;
        var loop = PreRequisitesNotesId;
        var objarrayinner =
        {
            ID: loop,
            CandidateId: CandidateId,
            PreRequisitesType: ddlPreRequisitesType,
            Remark: $('#txtaRemark').val(),
            Action: 'Remove',
            PreRequisites: loop
        }

        PreRequisitesNotes.push(objarrayinner);
        dataArry = PreRequisitesNotes;


        $('#tblPreRequisites').html('');
        var newtbblData1 = '<table id="tblPreRegistrationPreRequisites" class="table table-striped m-0" >' +
            ' <thead>' +
            ' <tr>' +
            ' <th width="33">S.No</th>' +
            ' <th width="200">Pre Requisites Type</th>' +
            ' <th>Remark</th>' +
            ' <th width="33" class=" text-center">Action</th>' +
            ' </tr>' +
            ' </thead>';
        var html1 = "</table>";

        var tableData = "";
        for (let i = 0; i < dataArry.length; i++) {
            var newtbblData = "<tr><td>" + parseInt(i + 1) + "</td><td>" + dataArry[i].PreRequisitesType + "</td><td>" + dataArry[i].Remark + "</td><td class='text-center'><a class='HideClass'  title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='deletePreRequisitesNotesRows(this," + dataArry[i].PreRequisites + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr>";
            var allstring = newtbblData;
            tableData += allstring;
        }
        $('#tblPreRequisites').html(newtbblData1 + tableData + html1);
        $('#txtaRemark').val('');
        $("#ddlPreRequisitesType").val('0').trigger('change');
        HtmlPagingPreRequisites();
    }

}
function BindDataPreRequisites(dataPreRequisites) {
    var dataArry = dataPreRequisites
    for (let i = 0; i < dataArry.length; i++) {
        PreRequisitesNotesId = PreRequisitesNotesId + 1;
        var loop = PreRequisitesNotesId;
        var objarrayinner =
        {
            ID: loop,
            CandidateId: CandidateId,
            PreRequisitesType: dataArry[i].PreRequisitesType,
            Remark: dataArry[i].Remark,
            Action: dataArry[i].Action,
            PreRequisites: dataArry[i].ID
        }

        PreRequisitesNotes.push(objarrayinner);
    }
    dataArry = PreRequisitesNotes;


    $('#tblPreRequisites').html('');
    var newtbblData1 = '<table id="tblPreRegistrationPreRequisites" class="table table-striped m-0" >' +
        ' <thead>' +
        ' <tr>' +
        ' <th width="33".No</th>' +
        ' <th width="200">Pre Requisites Type</th>' +
        ' <th>Remark</th>' +
        ' <th width="33" class="text-center">Action</th>' +
        ' </tr>' +
        ' </thead>';
    var html1 = "</table>";

    var tableData = "";
    for (let i = 0; i < dataArry.length; i++) {
        var newtbblData = "<tr><td>" + dataArry[i].ID + "</td><td>" + dataArry[i].PreRequisitesType + "</td><td>" + dataArry[i].Remark + "</td><td clas='text-center'><a class='HideClass'  title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='deletePreRequisitesNotesRows(this," + dataArry[i].PreRequisites + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr>";
        var allstring = newtbblData;
        tableData += allstring;
    }
    $('#tblPreRequisites').html(newtbblData1 + tableData + html1);
    HtmlPagingPreRequisites();
}
function RedirectToOrientationSchedule() {
    if (PsychometricTest == "138") {
        if ($('#hdnfileScopeActualName').val() == "") {
            $('#fileScopeofwork').attr({ 'class': 'form-control Mandatory ' })
        }
        else {
            $('#fileScopeofwork').attr({ 'class': 'form-control  ' })
        }
        //$('#fileScopeofwork').attr({ 'class': 'form-control Mandatory ' })
        //$('#supMandatoryPsychometric').attr({ 'className': 'red-clr' }).html('*');
    }
    else {
        $('#fileScopeofwork').attr({ 'class': 'form-control  ' })
        $('#supMandatoryPsychometric').attr({ 'className': 'red-clr' }).html('');
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
        var mtruser = document.getElementById('mtruser');
        EmployeeTerm = $("#ddlEmployeeCategory").val() == 1 ? "TermBase" : $("#ddlEmployeeCategory").val() == 2 ? "CoreBase" : "";
        var registractionDetails = {
            ID: RegID,
            USER_ID: USER_ID,
            Emp_Name: $('#txtEmployeeName').val(),
            email: $('#txtEmailID').val(),
            DOJ: formatDate(ChangeDateFormat($('#txtExpectedDateofJoining').val())),
            EmploymentTerm: EmployeeTerm,
            Contract_EndDate: formatDate(EndDateOfContract),
            DepartmentID: 4,
            thematicarea_IDs: $('#ddlThematicArea').val().join(),
            WorkLocationID: $('#ddlWorkLocation').val(),
            metro: $('#ddlMetroNonMetro').val(),
            design_id: $("#ddlDesignation").val(),
            JobID: JobId,
            emp_status: 'Confirmed',
            Probation_EndDate: formatDate(ExpectedLastDateofProbation),
            doc: '',// formatDate(ExpectedLastDateofProbation),
            hod_name: 454,
            SecondaryHODID: 'a',
            ed_name: 'a',
            HRID: $('#ddlHRPointPerson').val(),
            AppraiserID: 229,
            co_ot: 'a',
            ResidentialStatus: 'a',
            SkillsIDs: '1',
            NoticePeriod: '1',
            dor: '',// formatDate(ExpectedLastDateofProbation),
            lastworking_day:'',// formatDate(ExpectedLastDateofProbation),
            PsychometricTest: PsychometricTest,
            Personalemail: $('#txtEmailID').val()
        };
        var addUser = {
            ID: USER_ID,
            CandidateId: CandidateId,
            UserType: $('#ddlUserType :selected').text(),
            user_name: $('#txtUserLoginId').val(),
            Name: $('#txtUserName').val(),
            email: $('#txtUserEmailId').val(),
            password: $('#txtUserPassword').val(),
            RoleIDs: $('#ddlUserRole').val().join()
        }

        var obj = {
            AddUser: mtruser.checked == true ? addUser : null,
            RegistrationDet: registractionDetails
        }
        CommonAjaxMethod(virtualPath + 'OnboardingRequest/SaveRegistrationDetails', obj, 'POST', function (response) {
            UserManID = response.ID;
            var PreRegistration = {
                CandidateId: CandidateId,
                EmployeeName: $('#txtEmployeeName').val(),
                EmailID: $('#txtEmailID').val(),
                Gender: $('#ddlGender').find("option:selected").text(),
                DateofBirth: ChangeDateFormat($('#txtDateofBirth').val()),
                MitrUser: mtruser.checked == true ? true : false,
                OfficialEmailID: $('#txtOfficialEmailID').val(),
                ExpectedDateofJoining: ChangeDateFormat($('#txtExpectedDateofJoining').val()),
                Designation: $("#ddlDesignation").val(),
                PrimarySupervisor: $('#ddlPrimarySupervisor').val(),
                SecondarySupervisor: $('#ddlSecondarySupervisor').val(),
                ExceptionalApprover: $('#ddlExceptionalApprover').val(),
                HRPointPerson: $('#ddlHRPointPerson').val(),
                WorkLocation: $('#ddlWorkLocation').val(),
                MetroNonMetro: $('#ddlMetroNonMetro').val(),
                ThematicArea: $('#ddlThematicArea').val().join(),
                //OfferLetter: $('#txtPersonalEmailID').val(),
                CV: $('#lblAttachments').text(),
                DocumentVerified: DocumentVerified.checked == true ? true : false,
                ReferenceChecked: ReferenceChecked.checked == true ? true : false,
                UploadingDocumentID: 0,
                PreRequisitesID: 0,
                AttachmentActualName: $('#hdnfileScopeActualName').val(),
                AttachmentNewName: $('#hdnfileScopeNewName').val(),
                AttachmentUrl: $('#hdnfileScopeFileUrl').val(),
                Master_Emp_ID: response.ID
            }
            var obj = {
                PreRegistrationModel: PreRegistration,
                UploadingDocumentModel: DocArray,
                PreRequisitesModel: PreRequisitesNotes
            }
            CommonAjaxMethod(virtualPath + 'OnboardingRequest/SavePreRegistrationData', obj, 'POST', function (response) {
                var user = {
                    UserManID: UserManID,
                    CandidateId: CandidateId,
                    Status: 0,
                    CreatedBy: loggedinUserid,
                    ModifiedBy: loggedinUserid
                }
                var obj = {
                    OnboardingUsers: user
                }
                if (mtruser.checked == true) {
                    CommonAjaxMethod(virtualPath + 'OnboardingRequest/SaveOnboardMitrUserData', obj, 'POST', function (response) {
                        FailToaster("PreRegistration has been issued to candidate.");
                        window.location.href = virtualPath + 'Onboarding/OrientationSchedule?id=' + CandidateId;
                    });
                }
                else {
                    window.location.href = virtualPath + 'Onboarding/OrientationSchedule?id=' + CandidateId;
                    //window.location.reload();
                }
            });
        });

    }
}
function SaveUserDetails() {
    var valid = true;
    if (checkValidationOnSubmit('MandatoryUser') == false) {
        valid = false;
    }
    if (checkValidationOnSubmit('Mandatoryddl') == false) {
        valid = false;
    }
    if (valid == true) {
        var obj = {
            CandidateId: CandidateId,
            UserType: $('#ddlUserType :selected').text(),
            user_name: $('#txtUserLoginId').val(),
            Name: $('#txtUserName').val(),
            email: $('#txtUserEmailId').val(),
            password: $('#txtUserPassword').val(),
            RoleIDs: $('#ddlUserRole').val().join(),
            USER_ID: USER_ID
        }

        //CommonAjaxMethod(virtualPath + 'OnboardingRequest/SaveOnboardUser', obj, 'POST', function (response) {
        //    //window.location.href = virtualPath + 'Onboarding/HRScreenOnboard';
        //    FailToaster(response.SuccessMessage);
        //    $('#adduser').modal('hide');
        //    $('#txtOfficialEmailID').val($('#txtUserEmailId').val());
        //    USER_ID = response.ID;
        //    //RegID = 0;
        //    var user = {
        //        UserManID: response.ID,
        //        CandidateId: CandidateId,
        //        Status: 0,
        //        CreatedBy: loggedinUserid,
        //        ModifiedBy: loggedinUserid
        //    }
        //    var obj = {
        //        OnboardingUsers: user
        //    }
        //    CommonAjaxMethod(virtualPath + 'OnboardingRequest/SaveOnboardMitrUserData', obj, 'POST', function (response) {

        //    });
        //});

    }
    return false;
}
function deleteDocArrayRows(obj, id) {
    ConfirmMsgBox("Are you sure want to delete", '', function () {
        var data = DocArray.filter(function (itemParent) { return (itemParent.DocumentId == id); });
        //var url = data[0].NatureofAttachmentUrl;        
        CommonAjaxMethod(virtualPath + 'OnboardingRequest/DeleteListRecordNotes', { Id: id, inputData: 2 }, 'POST', function (response) {
            $(obj).closest('tr').remove();
            DocArray = DocArray.filter(function (itemParent) { return (itemParent.DocumentId != id); });
            AfterDeleteUploadingDocumentsRebindHTMLTable();
        });
    })
}
function deletePreRequisitesNotesRows(obj, id) {
    ConfirmMsgBox("Are you sure want to delete", '', function () {
        var data = PreRequisitesNotes.filter(function (itemParent) { return (itemParent.PreRequisites == id); });
        //var url = data[0].NatureofAttachmentUrl;
        CommonAjaxMethod(virtualPath + 'OnboardingRequest/DeleteListRecordNotes', { Id: id, inputData: 1 }, 'POST', function (response) {
            $(obj).closest('tr').remove();
            PreRequisitesNotes = PreRequisitesNotes.filter(function (itemParent) { return (itemParent.PreRequisites != id); });
            AfterDeletePreRequisitesRebindHTMLTable();
        });
    })
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
function PreviewOfferLetter() {
    var selectedText = $('#ddlEmployeeCategory').find("option:selected").text();
    var selectedValue = $('#ddlEmployeeCategory').val();

    CommonAjaxMethod(virtualPath + 'OnboardingRequest/BindonboardingProcessData', { CandidateId: CandidateId, inputData: 8 }, 'GET', function (response) {
        var dataOffer = response.data.data.Table;
        if ($('#ddlEmployeeCategory option:selected').text() == "Term Based") {
            $('#modalTermBasedPreviewOfferLetter').modal('show');
            if (dataOffer.length > 0) {
                document.getElementById('lblTermBasedDate').innerText = ChangeDateFormatToddMMYYY(dataOffer[0].CurrentDate);
                document.getElementById('lblTermBasedFullName').innerText = dataOffer[0].Candidate;
                document.getElementById('lblTermBasedAddress').innerText = dataOffer[0].Address;
                document.getElementById('lblTermBasedDearName').innerText = dataOffer[0].Candidate;
                document.getElementById('lblTermBasedCurrentSalary').innerText = dataOffer[0].CurrentSalary;
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
                document.getElementById('lblCoreBasedDate').innerText = ChangeDateFormatToddMMYYY(dataOffer[0].CurrentDate);
                document.getElementById('lblCoreBasedFullName').innerText = dataOffer[0].Candidate;
                document.getElementById('lblCoreBasedAddress').innerText = dataOffer[0].Address;
                document.getElementById('lblCoreBasedDearName').innerText = dataOffer[0].Candidate;
                document.getElementById('lblCoreBasedCurrentSalary').innerText = dataOffer[0].CurrentSalary;
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
    return false;
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
function DownloadFile() {
    var id = $('#hddCVAttachId').val();
    $.ajax({
        url: virtualPath + 'OnboardingRequest/DownloadFile?Id=' + id,
        type: "GET",
        contentType: 'application/json',
        success: function (response) {

            var obj = JSON.parse(response);
            var d = obj.data.Table[0];
            var stSplitFileName = d.ActualFileName.split(".");

            var link = document.createElement("a");
            link.download = stSplitFileName[0];
            link.href = d.FileUrl;
            link.click();
        }
        ,
        error: function (error) {
            FailToaster(error);


            isSuccess = false;
        }

    });
}
function formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2)
        month = '0' + month;
    if (day.length < 2)
        day = '0' + day;

    return [year, month, day].join('-');
}
function RedirectToClick(View) {
    window.location.href = virtualPath + 'Onboarding/' + View + '?id=' + CandidateId;
}
function RedirectToOnboard() {
    window.location.href = virtualPath + 'Onboarding/HRScreenOnboard';
}
function HtmlPagingUploadingDocuments() {
    $('#tblPreRegistrationUploadingDocuments').after('<div id="divUploadingDocumentsPaging" style="text-align:right"></div>');
    var rowsShown = 5;
    var rowsTotal = $('#tblPreRegistrationUploadingDocuments tbody tr').length;
    var numPages = rowsTotal / rowsShown;
    for (i = 0; i < numPages; i++) {
        var pageNum = i + 1;
        $('#divUploadingDocumentsPaging').append('<a class="e-link e-numericitem e-spacing e-currentitem e-active" href="#" rel="' + i + '">' + pageNum + '</a> ');
    }
    $('#tblPreRegistrationUploadingDocuments tbody tr').hide();
    $('#tblPreRegistrationUploadingDocuments tbody tr').slice(0, rowsShown).show();
    $('#divUploadingDocumentsPaging a:first').addClass('active');
    $('#divUploadingDocumentsPaging a').bind('click', function () {
        $('#divUploadingDocumentsPaging a').removeClass('active');
        $(this).addClass('active');
        var currPage = $(this).attr('rel');
        var startItem = currPage * rowsShown;
        var endItem = startItem + rowsShown;
        $('#tblPreRegistrationUploadingDocuments tbody tr').css('opacity', '0.0').hide().slice(startItem, endItem).
            css('display', 'table-row').animate({ opacity: 1 }, 300);
    });
}
function HtmlPagingPreRequisites() {
    $('#tblPreRegistrationPreRequisites').after('<div id="divPreRegistrationPreRequisitesPaging" style="text-align:right"></div>');
    var rowsShown = 5;
    var rowsTotal = $('#tblPreRegistrationPreRequisites tbody tr').length;
    var numPages = rowsTotal / rowsShown;
    for (i = 0; i < numPages; i++) {
        var pageNum = i + 1;
        $('#divPreRegistrationPreRequisitesPaging').append('<a class="e-link e-numericitem e-spacing e-currentitem e-active" href="#" rel="' + i + '">' + pageNum + '</a> ');
    }
    $('#tblPreRegistrationPreRequisites tbody tr').hide();
    $('#tblPreRegistrationPreRequisites tbody tr').slice(0, rowsShown).show();
    $('#divPreRegistrationPreRequisitesPaging a:first').addClass('active');
    $('#divPreRegistrationPreRequisitesPaging a').bind('click', function () {
        $('#divPreRegistrationPreRequisitesPaging a').removeClass('active');
        $(this).addClass('active');
        var currPage = $(this).attr('rel');
        var startItem = currPage * rowsShown;
        var endItem = startItem + rowsShown;
        $('#tblPreRegistrationPreRequisites tbody tr').css('opacity', '0.0').hide().slice(startItem, endItem).
            css('display', 'table-row').animate({ opacity: 1 }, 300);
    });
}
function AfterDeletePreRequisitesRebindHTMLTable() {
    dataArry = PreRequisitesNotes;
    $('#tblPreRequisites').html('');
    var newtbblData1 = '<table id="tblPreRegistrationPreRequisites" class="table table-striped m-0" >' +
        ' <thead>' +
        ' <tr>' +
        ' <th width="33".No</th>' +
        ' <th width="200">Pre Requisites Type</th>' +
        ' <th>Remark</th>' +
        ' <th width="33" class="text-center">Action</th>' +
        ' </tr>' +
        ' </thead>';
    var html1 = "</table>";

    var tableData = "";
    for (let i = 0; i < dataArry.length; i++) {
        var newtbblData = "<tr><td>" + parseInt(i + 1) + "</td><td>" + dataArry[i].PreRequisitesType + "</td><td>" + dataArry[i].Remark + "</td><td clas='text-center'><a class='HideClass'  title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='deletePreRequisitesNotesRows(this," + dataArry[i].PreRequisites + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr>";
        var allstring = newtbblData;
        tableData += allstring;
    }
    $('#tblPreRequisites').html(newtbblData1 + tableData + html1);
    HtmlPagingPreRequisites();
}
function AfterDeleteUploadingDocumentsRebindHTMLTable() {
    dataArry = DocArray;
    $('#tblUploadingDocument').html('');
    var newtbblData1 = '<table id="tblPreRegistrationUploadingDocuments" class="table table-striped m-0 " >' +
        ' <thead>' +
        ' <tr>' +
        ' <th width="33">S.No</th>' +
        ' <th width="150">Document Type</th>' +
        ' <th width="150">Attachment</th>' +
        ' <th>Description</th>' +
        ' <th width="50" class=" text-center">Action</th>' +
        ' </tr>' +
        ' </thead>';
    var html1 = "</table>";
    var tableData = "";
    for (let i = 0; i < dataArry.length; i++) {
        var newtbblData = "<tr><td>" + parseInt(i + 1) + "</td><td>" + dataArry[i].DocumentType + "</td><td><a target='_blank' href=" + dataArry[i].AttachmentUrl + ">" + dataArry[i].AttachmentActualName + "</a></td><td>" + dataArry[i].Description + "</td><td class='text-center' ><a class='HideClass'  title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='deleteDocArrayRows(this," + dataArry[i].DocumentId + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a> </td></tr>";
        var allstring = newtbblData;
        tableData += allstring;
    }
    $('#tblUploadingDocument').html(newtbblData1 + tableData + html1);
    HtmlPagingUploadingDocuments();
}
function CheckUserDetailsExists(obj) {
    var ObjURL = $(obj).closest("div").find(".hdnRecordExit");
    if ($(obj).val() != "") {
        var dataObject = JSON.stringify({
            'ID': loggedinUserid,
            'Doctype': $(ObjURL).attr("tag-value"),
            'Value': $(obj).val(),

        });
        $.ajax({
            url: "/CommonAjax/CheckOnboardingUserDetailsExistsJSon",
            type: "Post",
            contentType: 'application/json',
            data: dataObject,
            success: function (data) {
                if (!data.Status) {

                    $(obj).val('');
                    $(obj).closest("div").find('.field-validation-valid').html(data.SuccessMessage).removeClass('color-green').addClass('text-danger')
                    document.getElementById('lblUserIdConfirmation').innerHTML = data.SuccessMessage;                    
                    $('#sptxtUserLoginId').addClass("text-danger");
                }
                else {
                    $(obj).closest("div").find('.field-validation-valid').html(data.SuccessMessage).removeClass('text-danger').addClass('color-green');
                    document.getElementById('lblUserIdConfirmation').innerHTML = data.SuccessMessage;
                    //document.getElementById('supMsgUserLoginId').parentNode.firstChild.className = 'color-green';
                    $('#sptxtUserLoginId').addClass("color-green");
                }
            },
            error: function (er) {
                alert(er);
            }
        });
    }
}