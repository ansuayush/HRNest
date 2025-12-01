$(document).ready(function () {
    BindState();
    $(".applyselect").select2();
    $(function () {
        $('.datepicker').datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: "dd-mm-yy",
            yearRange: "-90:+10"
        });

    });
    LoadMasterDropdown('ddlEmpCommitteMembers', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.Employee,
        manualTableId: 0
    }, 'Select', false);
    FillDropdown();




    CommonAjaxMethod(virtualPath + 'ProcureVendorRegis/GetProcureVendorRegis', { id: $('#hdnContentId').val() }
        , 'GET', function (response) {

            BankArray = [];
            BankArrayId = 0;

            DocArray = [];

            MSMEArray = [];
            MSMEArrayId = 0;

            AuthorisedSignatoriesArray = [];
            AuthorisedSignatoriesArrayId = 0;

            var dProcureRegis = response.data.data.Table;

            var dAttachements = response.data.data.Table1;
            if (dProcureRegis[0].Rating == "0.5") {
                document.getElementById("star0.5").click();
            }
            else if (dProcureRegis[0].Rating == "1.0") {
                document.getElementById("star1").click();
            }
            else if (dProcureRegis[0].Rating == "1.5") {
                document.getElementById("star1.5").click();
            }
            else if (dProcureRegis[0].Rating == "2.0") {
                document.getElementById("star2").click();
            }
            else if (dProcureRegis[0].Rating == "2.5") {
                document.getElementById("star2.5").click();
            }
            else if (dProcureRegis[0].Rating == "3.0") {
                document.getElementById("star3").click();
            }
            else if (dProcureRegis[0].Rating == "3.5") {
                document.getElementById("star3.5").click();
            }
            else if (dProcureRegis[0].Rating == "4.0") {
                document.getElementById("star4").click();
            }
            else if (dProcureRegis[0].Rating == "4.5") {
                document.getElementById("star4.5").click();
            }
            else if (dProcureRegis[0].Rating == "5.0") {
                document.getElementById("star5").click();
            }

            for (var i = 0; i < dAttachements.length; i++) {
                var fileId = i + 1;
                if (i < 16) {
                    $('#hdnUploadActualFileName' + fileId).val(dAttachements[i].AttachmentActualName);
                    $('#hdnUploadNewFileName' + fileId).val(dAttachements[i].AttachmentNewName);
                    $('#hdnNature' + fileId).val(dAttachements[i].NatureofDocuments);
                    $('#hdnUploadFileUrl' + fileId).val(dAttachements[i].AttachmentPath);
                    $('#txtAttach' + fileId).val(dAttachements[i].Remarks);
                    $('#lblAttachement' + fileId).text(dAttachements[i].AttachmentActualName);

                    if (dAttachements[i].AttachmentActualName == null || dAttachements[i].AttachmentActualName == undefined || dAttachements[i].AttachmentActualName == '') {
                        $('#btnDownload' + fileId).hide();
                    }


                }
                else {
                    DocArrayId = dAttachements[i].SRNo;
                    var objAttach =
                    {
                        SRNo: DocArrayId,
                        Id: dAttachements[i].id,
                        AttachmentActualName: dAttachements[i].AttachmentActualName,
                        AttachmentNewName: dAttachements[i].AttachmentNewName,
                        NatureofDocuments: dAttachements[i].NatureofDocuments,
                        AttachmentPath: dAttachements[i].AttachmentPath,
                        Remarks: dAttachements[i].Remarks
                    };

                    DocArray.push(objAttach);

                }
            }

            var dVRegistrationAuthorised = response.data.data.Table2;
            for (var i = 0; i < dVRegistrationAuthorised.length; i++) {
                AuthorisedSignatoriesArrayId = i + 1;
                var objAuth = {

                    Id: AuthorisedSignatoriesArrayId,
                    Name: dVRegistrationAuthorised[i].Name,
                    Designation: dVRegistrationAuthorised[i].Designation,
                    Email: dVRegistrationAuthorised[i].Email,
                    MobileNo: dVRegistrationAuthorised[i].MobileNo,
                    Purposes: dVRegistrationAuthorised[i].PurposesDetail,
                    PurposesId: dVRegistrationAuthorised[i].Purposes
                }
                AuthorisedSignatoriesArray.push(objAuth);
            }

            var dVRegistrationBankDetails = response.data.data.Table3;
            for (var i = 0; i < dVRegistrationBankDetails.length; i++) {
                BankArrayId = i + 1;
                var objbank =
                {
                    Id: BankArrayId,
                    BankName: dVRegistrationBankDetails[i].BandDetails,
                    BankId: dVRegistrationBankDetails[i].BankName,
                    AcNo: dVRegistrationBankDetails[i].AcNo,
                    IFSCCode: dVRegistrationBankDetails[i].IFSCCode,
                    TypeofAc: dVRegistrationBankDetails[i].TypeofAc,
                    Classification: dVRegistrationBankDetails[i].Classification
                }
                BankArray.push(objbank);

            }

            var dVRegistrationMSMEOtherDetail = response.data.data.Table4;
            for (var i = 0; i < dVRegistrationMSMEOtherDetail.length; i++) {
                MSMEArrayId = i + 1;
                var objMSEB = {
                    Id: MSMEArrayId,
                    Details: dVRegistrationMSMEOtherDetail[i].Details,
                    Remarks: dVRegistrationMSMEOtherDetail[i].Remarks
                }
                MSMEArray.push(objMSEB);


            }


            FillDataForEdit(dProcureRegis);
            BindAuthorisedSignatoriesArray(AuthorisedSignatoriesArray);
            BindBankArray(BankArray);
            BindDocumentGrid(DocArray);
            BindMSMEArray(MSMEArray);
        });




});

function BindState() {
    LoadMasterDropdown('txtState',
        {
            ParentId: 0,
            masterTableType: 0,
            isMasterTableType: false,
            isManualTable: true,
            manualTable: ManaulTableEnum.State,
            manualTableId: 0
        }, 'Select', false);
}


var BankArray = [];
var BankArrayId = 0;

var DocArray = [];
var DocArrayId = 16;

var MSMEArray = [];
var MSMEArrayId = 0;

var AuthorisedSignatoriesArray = [];
var AuthorisedSignatoriesArrayId = 0;

function BindBankArray(array) {
    for (var i = 0; i < array.length; i++) {
        var newtbblData = "<tr><td>" + array[i].BankName + "</td><td>" + array[i].AcNo + "</td><td>" + array[i].IFSCCode + "</td><td>" + array[i].TypeofAc + "</td><td>" + array[i].Classification + "</td></tr>";

        $("#tblBankDetails").find('tbody').append(newtbblData);
    }


}

function BindMSMEArray(array) {
    for (var i = 0; i < array.length; i++) {
        var newtbblData = "<tr><td>" + array[i].Details + "</td><td>" + array[i].Remarks + "</td></tr>";

        $("#tblActivity").find('tbody').append(newtbblData);
    }

}

function BindAuthorisedSignatoriesArray(array) {

    for (var i = 0; i < array.length; i++) {
        var newtbblData = "<tr><td>" + array[i].Name + "</td><td>" + array[i].Designation + "</td><td>" + array[i].Email + "</td><td>" + array[i].MobileNo + "</td><td>" + array[i].Purposes + "</td>" +
            "</tr > ";

        $("#tblAuthSignature").find('tbody').append(newtbblData);
    }


}

function FillDropdown() {


    var obj = {
        ParentId: 0,
        masterTableType: DropDownTypeEnum.ProcurementBankDetails,
        isMasterTableType: false,
        isManualTable: false,
        manualTable: 0,
        manualTableId: 0
    }
    LoadMasterDropdown('ddlBankDetails', obj, 'Select', false);

    LoadMasterDropdown('vtnngo',
        {
            ParentId: 0,
            masterTableType: 0,
            isMasterTableType: false,
            isManualTable: true,
            manualTable: ManaulTableEnum.ProcurementVenderTypeNo,
            manualTableId: 0
        }, 'Select', false);

    LoadMasterDropdown('vtngo',
        {
            ParentId: 0,
            masterTableType: 0,
            isMasterTableType: false,
            isManualTable: true,
            manualTable: ManaulTableEnum.ProcurementVenderTypeYes,
            manualTableId: 0
        }, 'Select', false);







    var obj = {
        ParentId: 0,
        masterTableType: DropDownTypeEnum.ProcurementRelationshipMaster,
        isMasterTableType: false,
        isManualTable: false,
        manualTable: 0,
        manualTableId: 0
    }
    LoadMasterDropdown('ddlC3Relation', obj, 'Select', false);



    var obj = {
        ParentId: 0,
        masterTableType: DropDownTypeEnum.ProcurementGoodAndServicesCategory,
        isMasterTableType: false,
        isManualTable: false,
        manualTable: 0,
        manualTableId: 0
    }
    LoadMasterDropdown('ddlGoodsNService', obj, 'Select', false);


    var obj = {
        ParentId: 0,
        masterTableType: DropDownTypeEnum.ProcurementPurpose,
        isMasterTableType: false,
        isManualTable: false,
        manualTable: 0,
        manualTableId: 0
    }
    LoadMasterDropdown('ddlPurpose', obj, 'Select', false);




    LoadMasterDropdown('ddlAreaOfOperation',
        {
            ParentId: 0,
            masterTableType: 0,
            isMasterTableType: false,
            isManualTable: true,
            manualTable: ManaulTableEnum.MasterLocation,
            manualTableId: 0
        }, 'Select', false);
}

function BindDocumentGrid(array) {
    for (var i = 0; i < array.length; i++) {
        var newtbblData = "<tr><td>" + array[i].SRNo + "</td><td>" + array[i].NatureofDocuments + "</td><td><button   onclick='DownloadFileFromDb(" + array[i].Id + ")'  target='_blank' class='green-clr'><i class='fas fa-download'></i>Download</button></td><td>" + array[i].AttachmentActualName + "</td><td>" + array[i].Remarks + "</td>" +
            "</tr > ";

        $("#tblDocumentList").find('tbody').append(newtbblData);
    }

}

function Redirect() {
    var btn = document.getElementById("btnVendorRegistrationReturn");
    btn.click();
}

function Fillvtngo(ctrl) {
    $('.ngoother').css('display', 'none');
    var x = ctrl.options[ctrl.selectedIndex].text;
    if (x === 'Others (pl specify)') {
        $('.ngoother').show();

        LoadMasterDropdown('vtnngo',
            {
                ParentId: 0,
                masterTableType: 0,
                isMasterTableType: false,
                isManualTable: true,
                manualTable: ManaulTableEnum.ProcurementVenderTypeNo,
                manualTableId: 0
            }, 'Select', false);

    }
}

function Fillvtnngo(ctrl) {

    $('.nngoother').css('display', 'none');
    var x = ctrl.options[ctrl.selectedIndex].text;
    if (x === 'Others (pl specify)') {
        $('.nngoother').show();


        LoadMasterDropdown('vtngo',
            {
                ParentId: 0,
                masterTableType: 0,
                isMasterTableType: false,
                isManualTable: true,
                manualTable: ManaulTableEnum.ProcurementVenderTypeYes,
                manualTableId: 0
            }, 'Select', false);

    }
}
function ClearFormData() {

    $('#txtPartName').val('');
    $('#txtVendorName').val('');
    $('#txtOtherC3Relation').val('');
    $('#txtEntityRegistrationNo').val('');
    $('#txtRegistrationDate').val('');
    $('#txtAct').val('');
    $('#txtValidUpDate').val('');
    $('#txtFCRARegistration').val('');
    $('#txtFCRARegistrationDate').val('');
    $('#txtFCRAValidUpDate').val('');
    $('#txt12ARegistrationNo').val('');
    $('#txt12ARegistrationDate').val('');
    $('#txt12AValidDateUp').val('');
    $('#txt80GRegistrationNo').val('');
    $('#txt80GRegistrationDate').val('');
    $('#txt80GValidUpTo').val('');
    $('#txtAddress').val('');
    $('#txtPhoneNo').val('');
    BindState();
    $('#txtRelatedEntities').val('');
    $('#txtShareMoredetails').val('');
    $('#txtPan').val('');
    $('#txtPanHolder').val('');
    $('#txtGST').val('');
    $('#txtMSMENo').val('');

    docArrayList = [];
    BankArray = [];
    AuthorisedSignatoriesArray = [];
    MSMEArray = [];
    commonArray = [];
    BindAuthorisedSignatoriesArray(AuthorisedSignatoriesArray);
    BindBankArray(BankArray);
    BindMSMEArray(MSMEArray);
    BindDocumentGrid(commonArray);
    FillDropdown();
    CommonAjaxMethod(virtualPath + 'ProcureVendorRegis/GetMaxReqNo', null, 'GET', function (response) {
        $('#ReqNo').val(response.data.data.Table[0].ReqNo);
    });
}

function checkValidationOnProcureSubmitYes(Mandatory) {
    var isValidData = true;
    var x = document.getElementsByClassName(Mandatory);
    x.innerHTML = '';
    var isMandatory = true;
    for (var i = 0; i < x.length; i++) {
        var isValidData = true;
        for (var j = 0; j < x[i].classList.length; j++) {
            if (x[i].classList[j] == 'VN') {
                isValidData = false;
            }
        }
        if (isValidData) {
            var extactValue = jQuery.trim(x[i].value);
            if ((extactValue == "") || (extactValue == "Select")) {

                if ($('#dv' + x[i].id).length > 0) {
                    if ($('#dv' + x[i].id).css('display') != 'none') {

                        $('#sp' + x[i].id).show();
                        x[i].classList.add("errorValidation");
                        isMandatory = false;
                    }

                }
                else {
                    $('#sp' + x[i].id).show();
                    x[i].classList.add("errorValidation");
                    isMandatory = false;
                }

            }
            else {
                x[i].classList.remove("errorValidation");
                $('#sp' + x[i].id).hide();
            }
        }
    }
    if (!isMandatory) {
        return false;
    }
    return isMandatory;
}
function checkValidationOnProcureSubmitNo(Mandatory) {

    var x = document.getElementsByClassName(Mandatory);
    x.innerHTML = '';
    var isMandatory = true;
    for (var i = 0; i < x.length; i++) {
        var isValidData = true;
        for (var j = 0; j < x[i].classList.length; j++) {
            if (x[i].classList[j] == 'VY') {
                isValidData = false;
            }
        }
        if (isValidData) {
            var extactValue = jQuery.trim(x[i].value);
            if ((extactValue == "") || (extactValue == "Select")) {

                if ($('#dv' + x[i].id).length > 0) {
                    if ($('#dv' + x[i].id).css('display') != 'none') {

                        $('#sp' + x[i].id).show();
                        x[i].classList.add("errorValidation");
                        isMandatory = false;
                    }

                }
                else {
                    $('#sp' + x[i].id).show();
                    x[i].classList.add("errorValidation");
                    isMandatory = false;
                }

            }
            else {
                x[i].classList.remove("errorValidation");
                $('#sp' + x[i].id).hide();
            }
        }
    }
    if (!isMandatory) {
        return false;
    }
    return isMandatory;
}

function CheckMandedForRadio() {
    var isValid = true

    var radios1 = document.getElementById('yes13');
    var radios2 = document.getElementById('yes14');
    var radios3 = document.getElementById('yes25');

    var radios5 = document.getElementById('yes2');
    if (radios5.checked == true) {
        if ($('#txtAreaOfOperationDetails').val() == '') {
            $('#sptxtAreaOfOperationDetails').show();
            isValid == false;
        }
        else {
            $('#sptxtAreaOfOperationDetails').hide();
        }
    }


    if (radios3.checked == true) {
        if ($('#txtShareMoredetails').val() == '') {
            $('#sptxtShareMoredetails').show();
            isValid == false;
        }
        else {
            $('#sptxtShareMoredetails').hide();
        }
    }
    if (radios1.checked == true) {
        if ($('#txtFCRARegistration').val() == '') {
            $('#sptxtFCRARegistration').show();
            isValid == false;
        }
        else {
            $('#sptxtFCRARegistration').hide();
        }
        if ($('#txtFCRARegistrationDate').val() == '') {
            $('#sptxtFCRARegistrationDate').show();
            isValid == false;
        }
        else {
            $('#sptxtFCRARegistrationDate').hide();
        }

        if ($('#txtFCRAValidUpDate').val() == '') {
            $('#sptxtFCRAValidUpDate').show();
            isValid == false;
        }
        else {
            $('#sptxtFCRAValidUpDate').hide();
        }
    }

    if (radios2.checked == true) {
        if ($('#txt12ARegistrationNo').val() == '') {
            $('#sptxt12ARegistrationNo').show();
            isValid == false;
        }
        else {
            $('#sptxt12ARegistrationNo').hide();
        }
        if ($('#txt12ARegistrationDate').val() == '') {
            $('#sptxt12ARegistrationDate').show();
            isValid == false;
        }
        else {
            $('#sptxt12ARegistrationDate').hide();
        }

        if ($('#txt12AValidDateUp').val() == '') {
            $('#sptxt12AValidDateUp').show();
            isValid == false;
        }
        else {
            $('#sptxt12AValidDateUp').hide();
        }
    }
    return isValid;
}

function FillDataForEdit(data) {
    $('#youngo').val(data[0].IsNGO).trigger('change');



    if (data[0].IsNGO == 'yes') {
        $('#vtngo').val(data[0].VendorType).trigger('change');
        $('.ngoother').css('display', 'none');
        if (data[0].VendorValue != null || data[0].VendorValue != "") {
            if (data[0].VendorValue.indexOf('Other') !== -1) {
                $('.ngoother').show();

                $('#vtngoOther').val(data[0].OtherVendor);

            }
        }


    }
    else {

        $('#vtnngo').val(data[0].VendorType).trigger('change');
        $('.nngoother').css('display', 'none');
        if (data[0].VendorValue != null || data[0].VendorValue != "") {
            if (data[0].VendorValue.indexOf('Other') !== -1) {

                $('#vtnngoOther').val(data[0].OtherVendor);
                $('.nngoother').show();
            }
        }
    }
    if (data[0].AreyouregisteredunderFCRA == true) {
        document.getElementById('yes13').checked = true;
        $('#yes13').click();
    }
    else {

        document.getElementById('no14').checked = true;
        $('#no14').click();
    }

    if (data[0].AreyouregisteredunderSection12Aand80G == true) {
        document.getElementById('yes14').checked = true;
        $('#yes14').click();
    }
    else {

        document.getElementById('no15').checked = true;
        $('#no15').click();
    }

    if (data[0].HasthevendorworkedwithC3 == true) {
        document.getElementById('yes25').checked = true;
        $('#yes25').click();
    }
    else {

        document.getElementById('no23').checked = true;
        $('#no23').click();
    }

    if (data[0].IsthisentityegisteredunderMSME == true) {
        document.getElementById('yes17').checked = true;
        $('#yes17').click();
    }
    else {

        document.getElementById('no18').checked = true;
        $('#no18').click();
    }


    if (data[0].Areyouawareofanyconflictofinterest == true) {
        document.getElementById('yes2').checked = true;
        $('#yes2').click();
    }
    else {

        document.getElementById('no2').checked = true;
        $('#no2').click();
    }
    $('#Reqby').val(data[0].Req_By);
    $('#ReqNo').val(data[0].Req_No);
    $('#ReqDate').val(ChangeDateFormatToddMMYYY(data[0].Req_Date));
    $('#txtPartName').val(data[0].PartnerName);
    $('#txtVendorName').val(data[0].VendorName);
    if (data[0].RelationshipwithC3 != null) {
        if (data[0].RelationshipwithC3.length > 0) {
            let pdPlaceOfOriginArray = data[0].RelationshipwithC3.split(',');

            $("#ddlC3Relation").select2({
                multiple: true,
            });
            $('#ddlC3Relation').val(pdPlaceOfOriginArray).trigger('change');
            if (data[0].RelationshipwithC3Values != null || data[0].RelationshipwithC3Values != "") {
                if (data[0].RelationshipwithC3Values.indexOf('Other') !== -1) {
                    $('.dvRelationshipOther').show();

                    $('#txtOtherC3Relation').val(data[0].SpecifyRelationshipwithC3);

                }
            }
        }
    }
    $('#txtOtherC3Relation').val(data[0].SpecifyRelationshipwithC3);
    $('#txtEntityRegistrationNo').val(data[0].EntityRegistrationNo);
    $('#txtRegistrationDate').val(ChangeDateFormatToddMMYYY(data[0].RegistrationDate));
    $('#txtAct').val(data[0].Act);
    $('#txtValidUpDate').val(ChangeDateFormatToddMMYYY(data[0].ValidUpto));
    $('#txtFCRARegistration').val(data[0].FCRARegistrationNo);
    $('#txtFCRARegistrationDate').val(ChangeDateFormatToddMMYYY(data[0].FCRARegistrationDate));
    $('#txtFCRAValidUpDate').val(ChangeDateFormatToddMMYYY(data[0].FCRAValidUpto));
    $('#txt12ARegistrationNo').val(data[0].RegistrationNo12A);
    $('#txt12ARegistrationDate').val(ChangeDateFormatToddMMYYY(data[0].RegistrationDate12A));
    $('#txt12AValidDateUp').val(ChangeDateFormatToddMMYYY(data[0].ValidUpto12A));
    $('#txt80GRegistrationNo').val(data[0].RegistrationNo80G);
    $('#txt80GRegistrationDate').val(ChangeDateFormatToddMMYYY(data[0].RegistrationDateDatetime80G));
    $('#txt80GValidUpTo').val(ChangeDateFormatToddMMYYY(data[0].ValidUpto80G));
    $('#txtAddress').val(data[0].Address);
    $('#txtPhoneNo').val(data[0].Phone);
    $('#txtState').val(data[0].State).trigger('change');
    $('#txtRelatedEntities').val(data[0].RelatedEntities);
    $('#txtShareMoredetails').val(data[0].Pleasesharemoredetails);
    $('#txtPan').val(data[0].PANNo);
    $('#txtPanHolder').val(data[0].NameonPANCard);
    $('#txtGST').val(data[0].GST);

    if (data[0].Status == "Blacklisted")
    {
        $('#txtReason').val(data[0].BlacklistReason);
    }
    else if (data[0].Status == "Rejected")
    {
        $('#txtReason').val(data[0].Reason);
    }
    else if (data[0].Status == "Deactivated")
    {
        $('#txtReason').val(data[0].DeactivateReason);
    }
    
    if (data[0].CategoriesofGoodServices != null) {
        if (data[0].CategoriesofGoodServices.length > 0) {
            let pnArray = data[0].CategoriesofGoodServices.split(',');

            $("#ddlGoodsNService").select2({
                multiple: true,
            });
            $('#ddlGoodsNService').val(pnArray).trigger('change');
        }
    }
    $('#txtMSMENo').val(data[0].MSMENo);
    if (data[0].AreaofOperations != null) {
        if (data[0].AreaofOperations.length > 0) {
            let pnArray1 = data[0].AreaofOperations.split(',');

            $("#ddlAreaOfOperation").select2({
                multiple: true,
            });
            $('#ddlAreaOfOperation').val(pnArray1).trigger('change');
        }
    }
    $('#txtAreaOfOperationDetails').val(data[0].Conflictofinterestdetails);
    if (data[0].IsEmpaneled == null || data[0].IsEmpaneled == undefined) {

    }
    else {
        if (data[0].IsEmpaneled == true) {
            document.getElementById('yes41').checked = true;
            $('#yes41').click();
            $('#txtEmpStartDate').val(ChangeDateFormatToddMMYYY(data[0].EmpStartDate));
            $('#txtEmpEndDate').val(ChangeDateFormatToddMMYYY(data[0].EmpEndDate));
            $('#txtEmpDateOfMeeting').val(ChangeDateFormatToddMMYYY(data[0].EmpDateOfMeeting));

            if (data[0].EmpCommitteMembers != null) {
                if (data[0].EmpCommitteMembers.length > 0) {
                    let pnArray1 = data[0].EmpCommitteMembers.split(',');

                    $("#ddlEmpCommitteMembers").select2({
                        multiple: true,
                    });
                    $('#ddlEmpCommitteMembers').val(pnArray1).trigger('change');
                }
            }
            $('#lblfileUploadEmpanelName').text(data[0].EmpAttachmentFileName);
            $('#fileUploadEmpanelURL').val(data[0].EmpAttachmentPath);
            $('#fileUploadEmpanelName').val(data[0].EmpAttachmentFileName);
            $('#txtEmpRemark').val(data[0].EmpRemark);
        }
        else {
            document.getElementById('no42').checked = true;
            $('#no42').click();
        }
    }






}
function DownloadFileEmpanl() {
    var fileURl = $('#fileUploadEmpanelURL').val();
    var fileName = $('#fileUploadEmpanelName').val();
    if (fileURl != null || fileURl != undefined) {
        var stSplitFileName = fileName.split(".");
        var link = document.createElement("a");
        link.download = stSplitFileName[0];
        link.href = fileURl;
        link.click();
    }
}

function RegistrationDeactivate() {
   
    var registrationObject =
    {
        Id: $('#hdnContentId').val(),
        Reason: $('#sptxtDeactivateReason').val(),
        Status: 3
    }
    CommonAjaxMethod(virtualPath + 'ProcureVendorRegis/ApproveReject', registrationObject, 'POST', function (response) {
        Redirect();

    });
}

function DownloadFile(id) {

    var fileURl = $('#hdnUploadFileUrl' + id).val();
    var fileName = $('#hdnUploadActualFileName' + id).val();
    if (fileURl != null || fileURl != undefined) {
        var stSplitFileName = fileName.split(".");
        var link = document.createElement("a");
        link.download = stSplitFileName[0];
        link.href = fileURl;
        link.click();
    }

}

function DownloadFileFromDb(id) {
    $.ajax({
        url: virtualPath + 'ProcureVendorRegis/GetDownloadFileDetails?Id=' + id,
        type: "GET",
        contentType: 'application/json',
        success: function (response) {
            var obj = JSON.parse(response);
            var d = obj.data.Table[0];
            var stSplitFileName = d.AttachmentActualName.split(".");

            var link = document.createElement("a");
            link.download = stSplitFileName[0];
            link.href = d.AttachmentPath;
            link.click();
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
function UploadocumentImpanelDoc() {



    var fileUpload = $("#fileUploadEmpanel").get(0);

    var files = fileUpload.files;
    if (files.length > 0) {

        // Create FormData object
        var fileData = new FormData();

        // Looping over all files and add it to FormData object
        for (var i = 0; i < files.length; i++) {
            fileData.append(files[i].name, files[i]);
        }

        $.ajax({
            url: virtualPath + 'CommonMethod/UploadOtherDocument',
            type: "POST",
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            data: fileData,

            success: function (response) {
                var result = JSON.parse(response);

                if (result.ErrorMessage == "") {

                    $('#fileUploadEmpanelName').val(result.FileModel.ActualFileName);
                    $('#fileUploadEmpanelURL').val(result.FileModel.FileUrl);
                    $('#lblfileUploadEmpanelName').text(result.FileModel.ActualFileName);


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


function RegistrationBlacklist() {
    var isValidEmpan = true;
    if ($('#txtBlacklistReason').val() == "") {
        isValidEmpan = false;
        $('#sptxtBlacklistReason').show();
    }
    else {
        $('#sptxtBlacklistReason').hide();
    }
    if (isValidEmpan == true) {
        var obj = {
            Id: $('#hdnContentId').val(),
            Action: 3,
            BlacklistReason: $('#txtBlacklistReason').val()

        }
        CommonAjaxMethod(virtualPath + 'ProcureVendorRegis/SaveImpanelDetails', obj, 'POST', function (response) {
            Redirect();
        });

    }
}

function RegistrationEmpnal() {

    var yes41 = document.getElementById('yes41');
    if (yes41.checked == true) {
        var isValidEmpan = true;
        if ($('#txtEmpStartDate').val() == "") {
            isValidEmpan = false;
            $('#sptxtEmpStartDate').show();
        }
        else {
            $('#sptxtEmpStartDate').hide();
        }
        if ($('#txtEmpEndDate').val() == "") {
            isValidEmpan = false;
            $('#sptxtEmpEndDate').show();
        }
        else {
            $('#sptxtEmpEndDate').hide();
        }
        if ($('#txtEmpDateOfMeeting').val() == "") {
            isValidEmpan = false;
            $('#sptxtEmpDateOfMeeting').show();
        }
        else {
            $('#sptxtEmpDateOfMeeting').hide();
        }
        if (isValidEmpan == true) {
            var registrationObject = {
                Id: $('#hdnContentId').val(),
                IsEmpaneled: yes41.checked == true ? true : false,
                EmpStartDate: $('#txtEmpStartDate').val() == "" ? null : ChangeDateFormat($('#txtEmpStartDate').val()),
                EmpEndDate: $('#txtEmpEndDate').val() == "" ? null : ChangeDateFormat($('#txtEmpEndDate').val()),
                EmpDateOfMeeting: $('#txtEmpDateOfMeeting').val() == "" ? null : ChangeDateFormat($('#txtEmpDateOfMeeting').val()),
                EmpCommitteMembers: $('#ddlEmpCommitteMembers').val().join(),
                EmpAttachmentPath: $('#fileUploadEmpanelURL').val(),
                EmpAttachmentFileName: $('#fileUploadEmpanelName').val(),
                EmpRemark: $('#txtEmpRemark').val(),
                Action: 1,

            }
            CommonAjaxMethod(virtualPath + 'ProcureVendorRegis/SaveImpanelDetails', registrationObject, 'POST', function (response) {
                Redirect();
            });

        }


    }
}

function DeleteAttachmentImp() {
    var url = $("#fileUploadEmpanelURL").val();
    if (url != '') {
        var result = confirm("Are you sure want to delete this attach file?");
        if (result) {
            CommonAjaxMethod(virtualPath + 'CommonMethod/DeleteFile', { FileUrl: url }, 'POST', function (response) {
                $('#fileUploadEmpanelName').val('');
                $('#lblfileUploadEmpanelName').text('');
                $('#fileUploadEmpanelURL').val('');

            });
        }
    }
    else {
        FailToaster("File is not uploaded");
        //document.getElementById('hReturnMessage').innerHTML = "File is not uploaded";
        //$('#btnShowModel').click();
    }

}

function DisplayOther(ctrl) {

    $('.Relationshipoother').css('display', 'none');
    // var x = ctrl.options[ctrl.selectedIndex].text;
    var x = $("#" + ctrl.id + " option:selected").text();
    if (x.indexOf('Other') !== -1) {
        $('.Relationshipoother').show();
    }
}