$(document).ready(function () {

    if ($("#hdnContentId").val() == "0") {
        $("#MainDropdown").show();
    }
    else {
        $("#MainDropdown").hide();
    }

   

    
    $(".applyselect").select2();
    $(function () {
        $('.datepicker').datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: "dd-mm-yy",
            yearRange: "-90:+10"
        });

    });

    FillDropdown();
    BindState();

    $("#fileAttach1").change(function () {
        UploadocumentReport(1);
        $('#spfileAttach1').hide();
    })
    $("#fileAttach2").change(function () {
        UploadocumentReport(2);
    })
    $("#fileAttach3").change(function () {
        UploadocumentReport(3);
    })
    $("#fileAttach4").change(function () {
        UploadocumentReport(4);
        $('#spfileAttach4').hide();
    })
    $("#fileAttach5").change(function () {
        UploadocumentReport(5);
    })
    $("#fileAttach6").change(function () {
        UploadocumentReport(6);
        $("#spfileAttach6").hide();
    })
    $("#fileAttach7").change(function () {
        UploadocumentReport(7);
        $("#spfileAttach7").hide();
        
    })
    $("#fileAttach8").change(function () {
        UploadocumentReport(8);
        
        $("#spfileAttach8").hide();
    })
    $("#fileAttach9").change(function () {
        UploadocumentReport(9);
    })
    $("#fileAttach10").change(function () {
        UploadocumentReport(10);
    })
    $("#fileAttach11").change(function () {
        UploadocumentReport(11);
    })
    $("#fileAttach12").change(function () {
        UploadocumentReport(12);
    })
    $("#fileAttach13").change(function () {
        UploadocumentReport(13);
    })
    $("#fileAttach14").change(function () {
        UploadocumentReport(14);
    })
    $("#fileAttach15").change(function () {
        UploadocumentReport(15);
    })
    $("#fileAttach16").change(function () {
        UploadocumentReport(16);
    })


    if ($('#hdnContentId').val() == "0" || $('#hdnContentId').val() == "")
    {
        CommonAjaxMethod(virtualPath + 'ProcureVendorRegis/GetMaxReqNo', null, 'GET', function (response) {
            $('#ReqNo').val(response.data.data.Table[0].ReqNo);
        });
        var dt = new Date();
        var newDate = ChangeDateFormatToddMMYYY(dt);
        $('#ReqDate').val(newDate);
        $('#hdnLoggedInUser').val(loggedinUserid);
        $('#Reqby').val(loggedinUserName);
    }
    else {
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


            for (var i = 0; i < dAttachements.length; i++) {
                var fileId = i + 1;
                if (i < 16) {
                    $('#hdnUploadActualFileName' + fileId).val(dAttachements[i].AttachmentActualName);
                    $('#hdnUploadNewFileName' + fileId).val(dAttachements[i].AttachmentNewName);
                    $('#hdnNature' + fileId).val(dAttachements[i].NatureofDocuments);
                    $('#hdnUploadFileUrl' + fileId).val(dAttachements[i].AttachmentPath);
                    $('#txtAttach' + fileId).val(dAttachements[i].Remarks);
                    $('#lblAttachement' + fileId).text(dAttachements[i].AttachmentActualName);

                }
                else {
                    DocArrayId = dAttachements[i].SRNo;
                    var objAttach =
                    {
                        SRNo: DocArrayId,
                        Id: DocArrayId,
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
    }



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
function DeleteAttachment(no) {
    var url = $("#hdnUploadFileUrl" + no).val();
    if (url != '') {
        var result = confirm("Are you sure want to delete this attach file?");
        if (result) {
            CommonAjaxMethod(virtualPath + 'CommonMethod/DeleteFile', { FileUrl: url }, 'POST', function (response) {
                $('#hdnUploadActualFileName' + no).val('');
                $('#hdnUploadNewFileName' + no).val('');
                $('#hdnUploadFileUrl' + no).val('');
                $("#fileAttach" + no).val(null);
                $("#fileAttach" + no).val('');
                $('#lblAttachement' + no).text('');
                $("#txtAttach" + no).val('');

            });
        }
    }
    else {
        //document.getElementById('hReturnMessage').innerHTML = "File is not uploaded";
        //$('#btnShowModel').click();
        FailToaster("File is not uploaded");
    }

}
function UploadocumentReport(no) {



    var fileUpload = $("#fileAttach" + no).get(0);

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
                   
                    $('#hdnUploadActualFileName' + no).val(result.FileModel.ActualFileName);
                    $('#hdnUploadNewFileName' + no).val(result.FileModel.NewFileName);
                    $('#hdnUploadFileUrl' + no).val(result.FileModel.FileUrl);
                    $('#lblAttachement' + no).text(result.FileModel.ActualFileName);

                }
                else
                {


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
function SaveBankDetails() {
    if (checkValidationOnSubmit('BankDetails') == true) {
        var catId = '';
        var obj =
        {
            ID: catId,
            VendorType: $('#txtBankName').val(),
            TableType: DropDownTypeEnum.ProcurementBankDetails,
            Code: "Bank",
            MasterTableId: 0,
            UserId: loggedinUserid,
            IPAddress: $('#hdnIP').val() ? "" : " "
        }
        CommonAjaxMethod(virtualPath + 'ProcureVendorRegis/SaveBankDetails', obj, 'POST', function (response) {
            $('#txtBankName').val('');
            var obj = {
                ParentId: 0,
                masterTableType: DropDownTypeEnum.ProcurementBankDetails,
                isMasterTableType: false,
                isManualTable: false,
                manualTable: 0,
                manualTableId: 0
            }
            LoadMasterDropdown('ddlBankDetails', obj, 'Select', false);

            $('#ab').modal('hide')
        });
    }
}
var BankArray = [];
var BankArrayId = 0;

var DocArray = [];
var DocArrayId = 16;

var MSMEArray = [];
var MSMEArrayId = 0;

var AuthorisedSignatoriesArray = [];
var AuthorisedSignatoriesArrayId = 0;
function AddBankArray() {
    if (checkValidationOnSubmit('addBank') == true)
    {
        var ddlB = document.getElementById("ddlBankDetails");
        BankArrayId = BankArrayId + 1;
        var loop = BankArrayId;
        var objarrayinner =
        {
            Id: loop,
            BankName: ddlB.options[ddlB.selectedIndex].text,
            BankId: $("#ddlBankDetails").val(),
            AcNo: $("#txtAccountNo").val(),
            IFSCCode: $("#txtIFSCCode").val(),
            TypeofAc: $("#ddlAccountType").val(),
            Classification: $("#ddlBankType").val()


        }
        BankArray.push(objarrayinner);
        var newtbblData = "<tr><td>" + objarrayinner.BankName + "</td><td>" + objarrayinner.AcNo + "</td><td>" + objarrayinner.IFSCCode + "</td><td>" + objarrayinner.TypeofAc + "</td><td>" + objarrayinner.Classification + "</td><td><a title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='DeleteMSMEArray(this," + objarrayinner.Id + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr>";

        $("#tblBankDetails").find('tbody').append(newtbblData);

    
        $("#txtAccountNo").val('');
        $("#txtIFSCCode").val('');
        $('#ddlAccountType').val('0').trigger('change');   
        $('#ddlBankType').val('0').trigger('change');
        $('#ddlBankDetails').val('0').trigger('change');
    }
}
function BindBankArray(array)
{
    for (var i = 0; i < array.length; i++) {
        var newtbblData = "<tr><td>" + array[i].BankName + "</td><td>" + array[i].AcNo + "</td><td>" + array[i].IFSCCode + "</td><td>" + array[i].TypeofAc + "</td><td>" + array[i].Classification + "</td><td><a title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='DeleteMSMEArray(this," + array[i].Id + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr>";

        $("#tblBankDetails").find('tbody').append(newtbblData);
    }

    
}

function BindMSMEArray(array)
{
    for (var i = 0; i <array.length; i++)
    {
        var newtbblData = "<tr><td>" + array[i].Details + "</td><td>" + array[i].Remarks + "</td><td><a title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='DeleteMSMEArray(this," + array[i].Id + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr>";

        $("#tblActivity").find('tbody').append(newtbblData);
    }
 
}
function DeleteMSMEArray(obj,id) {
   
    ConfirmMsgBox("Are you sure want to delete", '', function () {

        $(obj).closest('tr').remove();       
        MSMEArray = MSMEArray.filter(function (itemParent) { return (itemParent.Id != id); });

    })
}
function AddMSMEArray() {

    if (checkValidationOnSubmit('msmAdd') == true)
    {
        MSMEArrayId = MSMEArrayId + 1;
        var loop = MSMEArrayId;
        var objarrayinner =
        {
            Id: loop,
            Details: $("#txtMSMEDetail").val(),
            Remarks: $("#txtMSMERemark").val()


        }
        MSMEArray.push(objarrayinner);       

        var newtbblData = "<tr><td>" + $("#txtMSMEDetail").val() +"</td><td>" + $("#txtMSMERemark").val() + "</td><td><a title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='DeleteMSMEArray(this," + loop + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr>";

        $("#tblActivity").find('tbody').append(newtbblData);

        $("#txtMSMEDetail").val('');
        $("#txtMSMERemark").val('')
    }
}


function AddAuthorisedSignatoriesArray()
{
    if (checkValidationOnSubmit('authValid') == true)
    {
        var ddddlPurpose = document.getElementById("ddlPurpose");
        AuthorisedSignatoriesArrayId = AuthorisedSignatoriesArrayId + 1;
        var loop = AuthorisedSignatoriesArrayId;
        var objarrayinner =
        {
            Id: loop,
            Name: $("#txtAuthName").val(),
            Designation: $("#txtAuthDesig").val(),
            Email: $("#txtAuthEmai").val(),
            MobileNo: $("#txtAuthNumber").val(),
            Purposes: ddddlPurpose.options[ddddlPurpose.selectedIndex].text,
            PurposesId: $("#ddlPurpose").val()


        }
        AuthorisedSignatoriesArray.push(objarrayinner);
     
        var newtbblData = "<tr><td>" + $("#txtAuthName").val() + "</td><td>" + $("#txtAuthDesig").val() + "</td><td>" + $("#txtAuthEmai").val() + "</td><td>" + $("#txtAuthNumber").val() + "</td><td>" + ddddlPurpose.options[ddddlPurpose.selectedIndex].text +"</td>"+
            "<td><a title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='deleteAuthorisedSignatoriesRows(this," + loop + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr > ";

        $("#tblAuthSignature").find('tbody').append(newtbblData);

        $("#txtAuthName").val('');
        $("#txtAuthDesig").val('');
        $("#txtAuthEmai").val('');
        $("#txtAuthNumber").val('');       
        $('#ddlPurpose').val('0').trigger('change');
        
    }
}
function BindAuthorisedSignatoriesArray(array) {

    for (var i = 0; i < array.length; i++)
    {
        var newtbblData = "<tr><td>" + array[i].Name + "</td><td>" + array[i].Designation + "</td><td>" + array[i].Email + "</td><td>" + array[i].MobileNo + "</td><td>" + array[i].Purposes + "</td>" +
            "<td><a title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='DeleteMSMEArray(this," + array[i].Id + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr > ";

        $("#tblAuthSignature").find('tbody').append(newtbblData);
    }
  
    
}
function deleteBankArrayRows(obj, id) {
        
    ConfirmMsgBox("Are you sure want to delete", '', function () {

        $(obj).closest('tr').remove();
        BankArray = BankArray.filter(function (itemParent) { return (itemParent.Id != id); });

    });


}
 
function deleteAuthorisedSignatoriesRows(obj, id) {

    ConfirmMsgBox("Are you sure want to delete", '', function () {

        $(obj).closest('tr').remove();
        AuthorisedSignatoriesArray = AuthorisedSignatoriesArray.filter(function (itemParent) { return (itemParent.Id != id); });

    })
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

function BindDocumentGrid(array)
{
    for (var i = 0; i < array.length; i++) {
        var newtbblData = "<tr><td>" + array[i].SRNo + "</td><td>" + array[i].NatureofDocuments + "</td><td></td><td>" + array[i].AttachmentActualName + "</td><td>" + array[i].Remarks + "</td>" +
            "<td><a title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='deleteDocArrayRows(this," + array[i].Id + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr > ";

        $("#tblDocumentList").find('tbody').append(newtbblData);
    }
     
}
function deleteDocArrayRows(obj, id) {
    ConfirmMsgBox("Are you sure want to delete", '', function ()
    {
        var data = DocArray.filter(function (itemParent) { return (itemParent.Id == id); });
        var url = data[0].AttachmentPath;

        CommonAjaxMethod(virtualPath + 'CommonMethod/DeleteFile', { FileUrl: url }, 'POST', function (response) {
            $(obj).closest('tr').remove();
            DocArray = DocArray.filter(function (itemParent) { return (itemParent.Id != id); });
        });      

    })    

}
function UploadOtherDocumentExtra() {


    if (checkValidationOnSubmit('UploadOtherDocument') == true) {

        var DocOtherRemark = document.getElementById("txtDocOtherRemark").value;

        var DocOtherNature = document.getElementById("txtDocOtherNature").value;

        var fileUpload = $("#fileDocOtherUpload").get(0);

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

                        DocArrayId = DocArrayId + 1;
                        var loop = DocArrayId;

                        var objarrayinner =
                        {
                            Id: loop,
                            SRNo: loop,
                            AttachmentActualName: result.FileModel.ActualFileName,
                            AttachmentNewName: result.FileModel.NewFileName,
                            NatureofDocuments: DocOtherNature,
                            AttachmentPath: result.FileModel.FileUrl,
                            Remarks: DocOtherRemark

                        }

                        DocArray.push(objarrayinner);

                        var newtbblData = "<tr><td>" + objarrayinner.SRNo + "</td><td>" + objarrayinner.NatureofDocuments + "</td><td></td><td>" + objarrayinner.AttachmentActualName + "</td><td>" + objarrayinner.Remarks + "</td>" +
                            "<td><a title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='deleteDocArrayRows(this," + objarrayinner.Id + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr > ";

                        $("#tblDocumentList").find('tbody').append(newtbblData);
                       // BindDocumentGrid(DocArray);
                        $("#fileDocOtherUpload").val('');
                        $("#fileDocOtherUpload").val(null);
                        $("#txtDocOtherRemark").val('');
                        $("#txtDocOtherNature").val('');

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


}

function SaveData(from) {
    var radios1 = document.getElementById('yes13');
    var radios2 = document.getElementById('yes14');
    var radios3 = document.getElementById('yes25');
    var radios4 = document.getElementById('yes17');
    var radios5 = document.getElementById('yes2');
    var isValid = true;
    var isValidCheck = true;
    if ($('#hdnUploadActualFileName1').val() == "") {
        $('#spfileAttach1').show();
        isValid = false;
    }
    else {
        $('#spfileAttach1').hide();
    }
     
    if ($('#youngo').val() == 'no' && from == 2)
    {
        if ($('#hdnUploadActualFileName4').val() == "") {

            $('#spfileAttach4').show();
            isValid = false;
        }
        else {
            $('#spfileAttach4').hide();
        }
    }
    if ($('#youngo').val() == 'yes' && from == 2) {
        if ($('#hdnUploadActualFileName12').val() == "") {

            $('#spfileAttach12').show();
            isValid = false;
        }
        else {
            $('#spfileAttach4').hide();
        }
    }
    var x = $("#ddlC3Relation option:selected").text();
    var isRelationCheck = true;
    if (x.indexOf('Other')!==-1)
    {
        var d = $("#txtOtherC3Relation").val();
        if (d == "") {
            isRelationCheck = false;
            $("#sptxtOtherC3Relation").show();
        }
        else {
            $("#sptxtOtherC3Relation").hide();
        }
        
    }
    
    if (BankArray.length == 0)
    {
        $('#spddlBankDetails').show();
        $('#sptxtAccountNo').show();
        $('#sptxtIFSCCode').show();
        $('#spddlAccountType').show();
        $('#spddlBankType').show();
        isValid = false;
    }
    if (AuthorisedSignatoriesArray.length == 0 && isValid==true)
    {
        $('#sptxtAuthName').show();
        $('#sptxtAuthDesig').show();
        $('#sptxtAuthEmai').show();
        $('#sptxtAuthNumber').show();
        $('#ddlPurpose').show();
        isValid = false;
    }
    if ($('#youngo').val() == 'no')
    {
        isValidCheck = checkValidationOnProcureSubmitNo('VR');
    }
    else
    {
        isValidCheck = checkValidationOnProcureSubmitYes('VR');
    }
    var isValidcheckManded = CheckMandedForRadio();

    var checkIFValidDoc = CheckIfDocumentValidate();

    if (isValid == true && isValidCheck == true && isValidcheckManded == true && checkIFValidDoc == true && isRelationCheck==true) {

        var docArrayList = [];
        for (var i = 0; i < 16; i++) {
            var d = i + 1;
            var docObject =
            {
                SRNo: d,
                AttachmentActualName: $('#hdnUploadActualFileName' + d).val(),
                AttachmentNewName: $('#hdnUploadNewFileName' + d).val(),
                NatureofDocuments: $('#hdnNature' + d).val(),
                AttachmentPath: $('#hdnUploadFileUrl' + d).val(),
                Remarks: $('#txtAttach' + d).val(),
            }
            docArrayList.push(docObject);
        }

        for (var i = 0; i < DocArray.length; i++) {
            docArrayList.push(DocArray[i]);
        }


        var registrationObject =
        {

            Id: $('#hdnContentId').val(),
            IsNGO: $('#youngo').val(),
            Req_No: $('#ReqNo').val(),
            Req_Date: ChangeDateFormat($('#ReqDate').val()),
            Req_By: loggedinUserid,
            VendorType: $('#youngo').val() == 'yes' ? $('#vtngo').val() : $('#vtnngo').val(),
            OtherVendor: $('#youngo').val() == 'yes' ? $('#vtngoOther').val() : $('#vtnngoOther').val(),
            PartnerName: $('#txtPartName').val(),
            VendorName: $('#txtVendorName').val(),
            RelationshipwithC3: $('#ddlC3Relation').val().join(),
            SpecifyRelationshipwithC3: $('#txtOtherC3Relation').val(),
            EntityRegistrationNo: $('#txtEntityRegistrationNo').val(),
            RegistrationDate: $('#txtRegistrationDate').val() == "" ? null : ChangeDateFormat($('#txtRegistrationDate').val()),
            Act: $('#txtAct').val(),
            ValidUpto: $('#txtValidUpDate').val() == "" ? null : ChangeDateFormat($('#txtValidUpDate').val()),
            AreyouregisteredunderFCRA: radios1.checked == true ? true : false,
            FCRARegistrationNo: $('#txtFCRARegistration').val(),
            FCRARegistrationDate: $('#txtFCRARegistrationDate').val() == "" ? null : ChangeDateFormat($('#txtFCRARegistrationDate').val()),
            FCRAValidUpto: $('#txtFCRAValidUpDate').val() == "" ? null : ChangeDateFormat($('#txtFCRAValidUpDate').val()),
            AreyouregisteredunderSection12Aand80G: radios2.checked == true ? true : false,
            RegistrationNo12A: $('#txt12ARegistrationNo').val(),
            RegistrationDate12A: $('#txt12ARegistrationDate').val() == "" ? null : ChangeDateFormat($('#txt12ARegistrationDate').val()),
            ValidUpto12A: $('#txt12AValidDateUp').val() == "" ? null : ChangeDateFormat($('#txt12AValidDateUp').val()),
            RegistrationNo80G: $('#txt80GRegistrationNo').val(),
            RegistrationDateDatetime80G: $('#txt80GRegistrationDate').val() == "" ? null : ChangeDateFormat($('#txt80GRegistrationDate').val()),
            ValidUpto80G: $('#txt80GValidUpTo').val() == "" ? null : ChangeDateFormat($('#txt80GValidUpTo').val()),
            Address: $('#txtAddress').val(),
            Phone: $('#txtPhoneNo').val(),
            State: $('#txtState').val(),
            RelatedEntities: $('#txtRelatedEntities').val(),
            HasthevendorworkedwithC3: radios3.checked == true ? true : false,
            Pleasesharemoredetails: $('#txtShareMoredetails').val(),
            PANNo: $('#txtPan').val(),
            NameonPANCard: $('#txtPanHolder').val(),
            GST: $('#txtGST').val(),
            CategoriesofGoodServices: $('#ddlGoodsNService').val().join(),
            IsthisentityegisteredunderMSME: radios4.checked == true ? true : false,
            MSMENo: $('#txtMSMENo').val(),
            Anyotherdetail: '',
            AreaofOperations: $('#ddlAreaOfOperation').val().join(),
            Areyouawareofanyconflictofinterest: radios5.checked == true ? true : false,
            Conflictofinterestdetails: $('#txtAreaOfOperationDetails').val(),
            Status: from == 1 ? 'Draft' : 'Pending',
            IPAddress: $('#hdnIP').val(),
            Reason: '',
            ApproverId: '',
            ApprovarAuth: '',
            IsGrid: $('#hdnIsApproved').val() == "2" ?"1" :"0",
            VRegistrationAttachments: docArrayList,
            VRegistrationBankDetails: BankArray,
            VRegistrationAuthorisedSignatories: AuthorisedSignatoriesArray,
            VRegistrationMSMEOtherDetail: MSMEArray
        }
        CommonAjaxMethod(virtualPath + 'ProcureVendorRegis/SaveProcureVendorRegis', registrationObject, 'POST', function (response)
        {
            
            if (response.ValidationInput == 1){
                // window.location.reload();
                Redirect();
            }
        });
    }

}
function Redirect()
{     
    if ($('#hdnIsApproved').val() == "1" || $('#hdnIsApproved').val() == "2")
    {
        
        var btn1 = document.getElementById("btnVendorRegistrationArrovalReturn");
        btn1.click();
    }
    else
    {
        var btn2 = document.getElementById("btnVendorRegistrationReturn");
        btn2.click();
    }
   
}
function Fillvtngo(ctrl) {
    $('.ngoother').css('display', 'none');
    var x = ctrl.options[ctrl.selectedIndex].text;
    if (x.indexOf('Other') !== -1)
    {
        $('#vtnngoOther').show();
        $('.nngoother').show();

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
    if (ctrl.selectedIndex != -1) {
        var x = ctrl.options[ctrl.selectedIndex].text;
        if (x.indexOf('Other') !== -1) {

            $('#vtnngoOther').show();
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
}
function DisplayOther(ctrl) {

    $('.Relationshipoother').css('display', 'none');
   // var x = ctrl.options[ctrl.selectedIndex].text;
    var x =  $("#" + ctrl.id + " option:selected").text();
    if (x.indexOf('Other') !== -1)
    {
        $('.Relationshipoother').show();
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
function CheckIfDocumentValidate()
{
    var isValidDoc = true;

    var radios1 = document.getElementById('yes13');
    if (radios1.checked == true && $('#youngo').val() == 'yes')
    {
        if($('#hdnUploadActualFileName6').val() == "")
        {
            $('#spfileAttach6').show();
            isValidDoc = false;

        }
        else {
            $('#spfileAttach6').hide();
        }
        
    }
    var radios2 = document.getElementById('yes14');
    if (radios2.checked == true && $('#youngo').val() == 'yes') {
        if ($('#hdnUploadActualFileName7').val() == "") {
            $('#spfileAttach7').show();
            isValidDoc = false;
        }
        else {
            $('#spfileAttach7').hide();
        }

    }
     

     
    if (radios2.checked == true && $('#youngo').val() == 'yes') {
        if ($('#hdnUploadActualFileName8').val() == "") {
            $('#spfileAttach8').show();
            isValidDoc = false;
        }
        else {
            $('#spfileAttach8').hide();
        }

    }
    
     
    return isValidDoc;
    
    

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
        if (data[0].VendorType != "0") {
            $('#vtngo').val(data[0].VendorType).trigger('change');
            $('.ngoother').css('display', 'none');
            if (data[0].VendorValue != null || data[0].VendorValue != "") {
                if (data[0].VendorValue.indexOf('Other') !== -1) {
                    $('.ngoother').show();
                    $('#vtngoOther').show();
                    $('#vtngoOther').val(data[0].OtherVendor);

                }
            }
        }


    }
    else {
        if (data[0].VendorType != "0")
        {
            $('#vtnngo').val(data[0].VendorType).trigger('change');
            $('.nngoother').css('display', 'none');
            if (data[0].VendorValue != null || data[0].VendorValue != "") {
                if (data[0].VendorValue.indexOf('Other') !== -1) {

                    $('#vtnngoOther').val(data[0].OtherVendor);
                    
                    $('#vtnngoOther').show();
                    $('.nngoother').show();
                }
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

    if (data[0].Status == "Draft") {
        $('#btnSaveDraft').show();
    }
    else {
        $('#btnSaveDraft').hide();
    }
    
    

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



}

function SaveDataDraft(from) {
     

    var radios1 = document.getElementById('yes13');
    var radios2 = document.getElementById('yes14');
    var radios3 = document.getElementById('yes25');
    var radios4 = document.getElementById('yes17');
    var radios5 = document.getElementById('yes2');
        var docArrayList = [];
        for (var i = 0; i < 16; i++) {
            var d = i + 1;
            var docObject =
            {
                SRNo: d,
                AttachmentActualName: $('#hdnUploadActualFileName' + d).val(),
                AttachmentNewName: $('#hdnUploadNewFileName' + d).val(),
                NatureofDocuments: $('#hdnNature' + d).val(),
                AttachmentPath: $('#hdnUploadFileUrl' + d).val(),
                Remarks: $('#txtAttach' + d).val(),
            }
            docArrayList.push(docObject);
        }

        for (var i = 0; i < DocArray.length; i++) {
            docArrayList.push(DocArray[i]);
        }


        var registrationObject =
        {

            Id: $('#hdnContentId').val(),
            IsNGO: $('#youngo').val(),
            Req_No: $('#ReqNo').val(),
            Req_Date: ChangeDateFormat($('#ReqDate').val()),
            Req_By: loggedinUserid,
            VendorType: $('#youngo').val() == 'yes' ? $('#vtngo').val() : $('#vtnngo').val(),
            OtherVendor: $('#youngo').val() == 'yes' ? $('#vtngoOther').val() : $('#vtnngoOther').val(),
            PartnerName: $('#txtPartName').val(),
            VendorName: $('#txtVendorName').val(),
            RelationshipwithC3: $('#ddlC3Relation').val().join(),
            SpecifyRelationshipwithC3: $('#txtOtherC3Relation').val(),
            EntityRegistrationNo: $('#txtEntityRegistrationNo').val(),
            RegistrationDate: $('#txtRegistrationDate').val() == "" ? null : ChangeDateFormat($('#txtRegistrationDate').val()),
            Act: $('#txtAct').val(),
            ValidUpto: $('#txtValidUpDate').val() == "" ? null : ChangeDateFormat($('#txtValidUpDate').val()),
            AreyouregisteredunderFCRA: radios1.checked == true ? true : false,
            FCRARegistrationNo: $('#txtFCRARegistration').val(),
            FCRARegistrationDate: $('#txtFCRARegistrationDate').val() == "" ? null : ChangeDateFormat($('#txtFCRARegistrationDate').val()),
            FCRAValidUpto: $('#txtFCRAValidUpDate').val() == "" ? null : ChangeDateFormat($('#txtFCRAValidUpDate').val()),
            AreyouregisteredunderSection12Aand80G: radios2.checked == true ? true : false,
            RegistrationNo12A: $('#txt12ARegistrationNo').val(),
            RegistrationDate12A: $('#txt12ARegistrationDate').val() == "" ? null : ChangeDateFormat($('#txt12ARegistrationDate').val()),
            ValidUpto12A: $('#txt12AValidDateUp').val() == "" ? null : ChangeDateFormat($('#txt12AValidDateUp').val()),
            RegistrationNo80G: $('#txt80GRegistrationNo').val(),
            RegistrationDateDatetime80G: $('#txt80GRegistrationDate').val() == "" ? null : ChangeDateFormat($('#txt80GRegistrationDate').val()),
            ValidUpto80G: $('#txt80GValidUpTo').val() == "" ? null : ChangeDateFormat($('#txt80GValidUpTo').val()),
            Address: $('#txtAddress').val(),
            Phone: $('#txtPhoneNo').val(),
            State: $('#txtState').val(),
            RelatedEntities: $('#txtRelatedEntities').val(),
            HasthevendorworkedwithC3: radios3.checked == true ? true : false,
            Pleasesharemoredetails: $('#txtShareMoredetails').val(),
            PANNo: $('#txtPan').val(),
            NameonPANCard: $('#txtPanHolder').val(),
            GST: $('#txtGST').val(),
            CategoriesofGoodServices: $('#ddlGoodsNService').val().join(),
            IsthisentityegisteredunderMSME: radios4.checked == true ? true : false,
            MSMENo: $('#txtMSMENo').val(),
            Anyotherdetail: '',
            AreaofOperations: $('#ddlAreaOfOperation').val().join(),
            Areyouawareofanyconflictofinterest: radios5.checked == true ? true : false,
            Conflictofinterestdetails: $('#txtAreaOfOperationDetails').val(),
            Status: from == 1 ? 'Draft' : 'Pending',
            IPAddress: $('#hdnIP').val(),
            Reason: '',
            ApproverId: '',
            ApprovarAuth: '',
            IsGrid: '',
            VRegistrationAttachments: docArrayList,
            VRegistrationBankDetails: BankArray,
            VRegistrationAuthorisedSignatories: AuthorisedSignatoriesArray,
            VRegistrationMSMEOtherDetail: MSMEArray
        }
        CommonAjaxMethod(virtualPath + 'ProcureVendorRegis/SaveProcureVendorRegis', registrationObject, 'POST', function (response) {
            if (response.ValidationInput == 1) {
                  window.location.reload();
                //Redirect();
            }
        });
    

}