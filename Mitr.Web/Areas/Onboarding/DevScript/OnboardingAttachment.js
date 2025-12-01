$(document).ready(function () {
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
    })
    $("#fileAttach7").change(function () {
        UploadocumentReport(7);
    })
    $("#fileAttach8").change(function () {
        UploadocumentReport(8);
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
    BindOnboardAttachmentRequest();
});
var DocArray = [];
var DocArrayId;
var responseCount;
var dataRegistration = [];
var dataAttachments = [];
var candodate_Status = false;
function BindOnboardAttachmentRequest() {
    CommonAjaxMethod(virtualPath + 'OnboardingRequest/BindonboardingAttachments', { CandidateId: CandidateId, inputData: 6 }, 'GET', function (response) {
        dataAttachments = response.data.data.Table;
        dataRegistration = response.data.data.Table1;
        var candidateStatus = response.data.data.Table2;
        responseCount = dataAttachments.length;// == 0 ? 13 : dataAttachments.length;
        if (candidateStatus.length > 0) {
            if (candidateStatus[0].Status == "14") {
                candodate_Status = true;
            }
            if (candidateStatus[0].Status == "15") {
                candodate_Status = true;
            }
            if (candidateStatus[0].Status == "16") {
                candodate_Status = true;
            }
            if (candodate_Status == true) {
                $("#btnAttachmentsSubmitted").prop('disabled', true);
                $("#btnAttachmentsAddRow").prop('disabled', true);
            }
            else {
                $("#btnAttachmentsSubmitted").prop('disabled', false);
                $("#btnAttachmentsAddRow").prop('disabled', false);
            }
        }
        DocArrayId = responseCount;
        if (dataAttachments.length > 0) {
            for (var i = 0; i < dataAttachments.length; i++) {
                var fileId = i + 1;
                if (i < dataAttachments.length) {
                    $('#hdnUploadActualFileName' + fileId).val(dataAttachments[i].AttachmentActualName);
                    $('#hdnUploadNewFileName' + fileId).val(dataAttachments[i].AttachmentNewName);
                    $('#hdnNature' + fileId).val(dataAttachments[i].NatureofDocuments);
                    $('#hdnUploadFileUrl' + fileId).val(dataAttachments[i].AttachmentUrl);
                    $('#txtAttach' + fileId).val(dataAttachments[i].Remark);
                    $('#lblAttachement' + fileId).text(dataAttachments[i].AttachmentActualName);
                    if (dataAttachments[i].AttachmentUrl != null) {
                        var path = dataAttachments[i].AttachmentUrl.substring(1, dataAttachments[i].AttachmentUrl.length);
                        $('#lblAttachementView' + fileId).attr("href", virtualPath + path);
                    }
                    $('#hdnAttachmentID' + fileId).val(dataAttachments[i].AttachmentID);
                    if (i > 12) {
                        if (dataAttachments[i].AttachmentActualName != null) {
                            var fileId = i + 1;
                            var actualUrl = $('#hdnUploadFileUrl' + fileId).val();
                            var newtbblData = "<tr><td>" + fileId + "</td><td>" + dataAttachments[i].NatureofDocuments + "</td><td></td><td><input type='hidden' id='hdnNature" + fileId + "' /><input type='hidden' id='hdnUploadFileUrl" + fileId + "' /><input type='hidden' id='hdnUploadActualFileName" + fileId + "' /><input type='hidden' id='hdnUploadNewFileName" + fileId + "' /><input type='hidden' id='hdnAttachmentID" + fileId + "' /> <a id='lblAttachementView" + fileId + "' href='" + actualUrl + "' target='_blank' class='float - right' style='margin - top: -18px;'>" + dataAttachments[i].AttachmentActualName + "</a></td><td> <textarea class='form-control maxSize[1200]' placeholder='Enter' id='txtAttach" + fileId + "'></textarea></td>" +
                                "<td><a title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='DeleteAttachment(this," + fileId + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr > ";
                            $("#tblDocumentList").find('tbody').append(newtbblData);
                            if (dataAttachments[i].AttachmentUrl != null) {
                                var path = dataAttachments[i].AttachmentUrl.substring(1, dataAttachments[i].AttachmentUrl.length);
                                $('#lblAttachementView' + fileId).attr("href", virtualPath + path);
                            }
                            $('#hdnUploadActualFileName' + fileId).val(dataAttachments[i].AttachmentActualName);
                            $('#hdnUploadNewFileName' + fileId).val(dataAttachments[i].AttachmentNewName);
                            $('#hdnNature' + fileId).val(dataAttachments[i].NatureofDocuments);
                            $('#hdnUploadFileUrl' + fileId).val(dataAttachments[i].AttachmentUrl);
                            $('#txtAttach' + fileId).val(dataAttachments[i].Remark);
                            $('#lblAttachement' + fileId).text(dataAttachments[i].AttachmentActualName);
                            $('#hdnAttachmentID' + fileId).val(dataAttachments[i].AttachmentID);
                        }
                    }
                }
                else {
                    DocArrayId = dataAttachments[i].SRNo;
                    var objAttach =
                    {
                        SRNo: DocArrayId,
                        AttachmentActualName: dataAttachments[i].AttachmentActualName,
                        AttachmentNewName: dataAttachments[i].AttachmentNewName,
                        NatureofDocuments: dataAttachments[i].NatureofDocuments,
                        AttachmentPath: dataAttachments[i].AttachmentUrl,
                        Remark: dataAttachments[i].Remark
                    };
                    DocArray.push(objAttach);
                }
            }
        }
        if (dataRegistration.length > 0) {
            if (dataRegistration[0].LIFlagPan == true && dataRegistration[0].LIPan != null) {
                var childData = dataAttachments.filter(function (itemParent) { return (itemParent.NatureofDocuments == "PAN Card" && itemParent.AttachmentActualName != null); });
                if (childData.length > 0) {
                    $('#supPANCard').attr({ 'className': 'red-clr' }).html('');
                    $('#fileAttach1').attr({ 'class': 'form-control' })
                }
                else {
                    $('#supPANCard').attr({ 'className': 'red-clr' }).html('*');
                    $('#hdnUploadActualFileName1').attr({ 'class': 'form-control Mandatory' })
                }
            }
            if (dataRegistration[0].LIFlagAadhar == true && dataRegistration[0].LIAadharNo != null) {
                var childData = dataAttachments.filter(function (itemParent) { return (itemParent.NatureofDocuments == "Aadhar" && itemParent.AttachmentActualName != null); });
                if (childData.length > 0) {
                    $('#supAadhar').attr({ 'className': 'red-clr' }).html('');
                    $('#fileAttach2').attr({ 'class': 'form-control' })
                }
                else {
                    $('#supAadhar').attr({ 'className': 'red-clr' }).html('*');
                    $('#hdnUploadActualFileName2').attr({ 'class': 'form-control Mandatory' })
                }
            }
            if (dataRegistration[0].LIFlagVoterID == true && dataRegistration[0].LIVoterIDNo != null) {
                $("#fileAttach4").attr("disabled", false);
                $("#txtAttach4").attr("disabled", false);
                var childData = dataAttachments.filter(function (itemParent) { return (itemParent.NatureofDocuments == "Voter ID" && itemParent.AttachmentActualName != null); });
                if (childData.length > 0) {
                    $('#supVoterID').attr({ 'className': 'red-clr' }).html('');
                    $('#fileAttach4').attr({ 'class': 'form-control ' })
                }
                else {
                    $('#supVoterID').attr({ 'className': 'red-clr' }).html('*');
                    $('#fileAttach4').attr({ 'class': 'form-control Mandatory' })
                }
            }
            if (dataRegistration[0].LIFlagVoterID == false) {
                $("#fileAttach4").attr("disabled", true);
                $("#txtAttach4").attr("disabled", true);
            }
            if (dataRegistration[0].LIFlagPassport == true && dataRegistration[0].LIPassportNo != null) {
                $("#fileAttach5").attr("disabled", false);
                $("#txtAttach5").attr("disabled", false);
                var childData = dataAttachments.filter(function (itemParent) { return (itemParent.NatureofDocuments == "Passport" && itemParent.AttachmentActualName != null); });
                if (childData.length > 0) {
                    $('#supPassportID').attr({ 'className': 'red-clr' }).html('');
                    $('#fileAttach5').attr({ 'class': 'form-control' })
                }
                else {
                    $('#supPassportID').attr({ 'className': 'red-clr' }).html('*');
                    $('#fileAttach5').attr({ 'class': 'form-control Mandatory' });
                }
            }
            if (dataRegistration[0].LIFlagPassport == false) {
                $("#fileAttach5").attr("disabled", true);
                $("#txtAttach5").attr("disabled", true);
            }
            if (dataRegistration[0].LIFlagUAN == true && dataRegistration[0].LIUAN != null) {
                $("#fileAttach6").attr("disabled", false);
                $("#txtAttach6").attr("disabled", false);
                var childData = dataAttachments.filter(function (itemParent) { return (itemParent.NatureofDocuments == "UAN" && itemParent.AttachmentActualName != null); });
                if (childData.length > 0) {
                    $('#supUANID').attr({ 'className': 'red-clr' }).html('');
                    $('#fileAttach6').attr({ 'class': 'form-control' })
                }
                else {
                    $('#supUANID').attr({ 'className': 'red-clr' }).html('*');
                    $('#fileAttach6').attr({ 'class': 'form-control Mandatory' })
                }
            }
            if (dataRegistration[0].LIFlagUAN == false) {
                $("#fileAttach6").attr("disabled", true);
                $("#txtAttach6").attr("disabled", true);
            }
            if (dataRegistration[0].LIFlagPIOOCI == true && dataRegistration[0].LIPIOOCI != null) {
                var childData = dataAttachments.filter(function (itemParent) { return (itemParent.NatureofDocuments == "PIO, if applicable" && itemParent.AttachmentActualName != null); });
                if (childData.length > 0) {
                    $('#supPIOID').attr({ 'className': 'red-clr' }).html('');
                    $('#fileAttach7').attr({ 'class': 'form-control' })
                }
                else {
                    $('#supPIOID').attr({ 'className': 'red-clr' }).html('*');
                    $('#fileAttach7').attr({ 'class': 'form-control Mandatory' })
                }
            }
            if (dataRegistration[0].LIFlagDrivingLicense == true && dataRegistration[0].LIDrivingLicenseNo != null) {
                var childData = dataAttachments.filter(function (itemParent) { return (itemParent.NatureofDocuments == "DL" && itemParent.AttachmentActualName != null); });
                if (childData.length > 0) {
                    $('#supDL').attr({ 'className': 'red-clr' }).html('');
                    $('#fileAttach8').attr({ 'class': 'form-control' })
                }
                else {
                    $('#supDL').attr({ 'className': 'red-clr' }).html('');
                    $('#fileAttach8').attr({ 'class': 'form-control Mandatory' })
                }
            }
            if (dataRegistration[0].LIFlagDrivingLicense == false) {
                $("#fileAttach8").attr("disabled", true);
                $("#txtAttach8").attr("disabled", true);
            }
            //var childData = dataAttachments.filter(function (itemParent) { return (itemParent.NatureofDocuments == "Work Visa, if applicable" && itemParent.AttachmentActualName != null); });
            //if (childData.length > 0) {
            //    $('#supDL').attr({ 'className': 'red-clr' }).html('');
            //    $('#fileAttach12').attr({ 'class': 'form-control' })
            //}
            //else {
            //    $('#supDL').attr({ 'className': 'red-clr' }).html('');
            //    $('#fileAttach12').attr({ 'class': 'form-control Mandatory' })
            //}
            if (dataRegistration[0].Nationality == "2") {
                if (document.getElementById('lblAttachement12').innerText == "") {
                    $('#supWorkVisaifApplicable').attr({ 'className': 'red-clr' }).html('*');
                    $('#fileAttach12').attr({ 'class': 'form-control Mandatory' })
                }
                else {
                    $('#supWorkVisaifApplicable').attr({ 'className': 'red-clr' }).html('');
                    $('#fileAttach12').attr({ 'class': 'form-control' })
                }
            }
            else {
                $('#supWorkVisaifApplicable').attr({ 'className': 'red-clr' }).html('');
                $('#fileAttach12').attr({ 'class': 'form-control' })
            }
        }
        BindDocumentGrid(DocArray);
    });
}
function ResetValidations(srno) {
    if (dataRegistration.length > 0) {
        if (srno == 1) {
            if (dataRegistration[0].LIFlagPan == true) {
                $('#supPANCard').attr({ 'className': 'red-clr' }).html('*');
                $('#fileAttach1').attr({ 'class': 'form-control Mandatory' })
            }
            else {
                $('#supPANCard').attr({ 'className': 'red-clr' }).html('');
                $('#fileAttach1').attr({ 'class': 'form-control ' })
            }
        }
        if (srno == 2) {
            if (dataRegistration[0].LIFlagAadhar == true) {
                $('#supAadhar').attr({ 'className': 'red-clr' }).html('*');
                $('#fileAttach2').attr({ 'class': 'form-control Mandatory' })
            }
            else {
                $('#supAadhar').attr({ 'className': 'red-clr' }).html('');
                $('#fileAttach2').attr({ 'class': 'form-control ' })
            }
        }
        if (srno == 4) {
            if (dataRegistration[0].LIFlagVoterID == true) {
                $('#supVoterID').attr({ 'className': 'red-clr' }).html('*');
                $('#fileAttach4').attr({ 'class': 'form-control Mandatory' })
            }
            else {
                $('#supVoterID').attr({ 'className': 'red-clr' }).html('');
                $('#fileAttach4').attr({ 'class': 'form-control ' })
            }
        }
        if (srno == 5) {
            if (dataRegistration[0].LIFlagPassport == true) {
                $('#supPassportID').attr({ 'className': 'red-clr' }).html('*');
                $('#fileAttach5').attr({ 'class': 'form-control Mandatory' });
            }
            else {
                $('#supPassportID').attr({ 'className': 'red-clr' }).html('');
                $('#fileAttach5').attr({ 'class': 'form-control ' });
            }
        }
        if (srno == 6) {
            if (dataRegistration[0].LIFlagUAN == true) {
                $('#supUANID').attr({ 'className': 'red-clr' }).html('*');
                $('#fileAttach6').attr({ 'class': 'form-control Mandatory' })
            }
            else {
                $('#supUANID').attr({ 'className': 'red-clr' }).html('');
                $('#fileAttach6').attr({ 'class': 'form-control ' })
            }
        }
        if (srno == 7) {
            if (dataRegistration[0].LIFlagPIOOCI == true) {
                $('#supPIOID').attr({ 'className': 'red-clr' }).html('*');
                $('#fileAttach7').attr({ 'class': 'form-control Mandatory' })
            }
            else {
                $('#supPIOID').attr({ 'className': 'red-clr' }).html('');
                $('#fileAttach7').attr({ 'class': 'form-control ' })
            }
        }
        if (srno == 8) {
            if (dataRegistration[0].LIFlagDrivingLicense == true) {
                $('#supDL').attr({ 'className': 'red-clr' }).html('*');
                $('#fileAttach8').attr({ 'class': 'form-control Mandatory' })
            }
            else {
                $('#supDL').attr({ 'className': 'red-clr' }).html('');
                $('#fileAttach8').attr({ 'class': 'form-control ' })
            }
        }
        if (srno == 12) {
            if (dataRegistration[0].Nationality == "2") {
                if (document.getElementById('lblAttachement12').innerText == "") {
                    $('#supWorkVisaifApplicable').attr({ 'className': 'red-clr' }).html('*');
                    $('#fileAttach12').attr({ 'class': 'form-control Mandatory' })
                }
                else {
                    $('#supWorkVisaifApplicable').attr({ 'className': 'red-clr' }).html('');
                    $('#fileAttach12').attr({ 'class': 'form-control' })
                }
            }
            else {
                $('#supWorkVisaifApplicable').attr({ 'className': 'red-clr' }).html('');
                $('#fileAttach12').attr({ 'class': 'form-control' })
            }
        }
    }
}
function BindDocumentGrid(array) {
    for (var i = 0; i < array.length; i++) {
        var newtbblData = "<tr><td>" + array[i].SRNo + "</td><td>" + array[i].NatureofDocuments + "</td><td></td><td>" + array[i].AttachmentActualName + "</td><td>" + array[i].Remark + "</td>" +
            "<td><a title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='deleteDocArrayRows(this," + array[i].Id + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr > ";
        $("#tblDocumentList").find('tbody').append(newtbblData);
    }
}
function SaveAttachments() {
     ;
    var valid = true;
    if (checkValidationOnSubmit('Mandatory') == false) {
        valid = false;
    }
    if (checkValidationOnSubmit('Mandatorypld') == false) {
        valid = false;
    }

    if (valid == true) {
        var isValid = true;
        var isValidCheck = true;
        var docArrayList = [];
        for (var i = 0; i < $("#tblDocumentList tr").length - 2; i++) {
            var d = i + 1;
            var docObject =
            {
                CandidateId: CandidateId,
                SRNo: d,
                AttachmentActualName: $('#hdnUploadActualFileName' + d).val(),
                AttachmentNewName: $('#hdnUploadNewFileName' + d).val(),
                NatureofDocuments: $('#hdnNature' + d).val(),
                AttachmentUrl: $('#hdnUploadFileUrl' + d).val(),
                Remark: $('#txtAttach' + d).val(),
            }
            if (docObject.AttachmentActualName != undefined) {
                docArrayList.push(docObject);
            }
        }
        if (dataAttachments.length < 13) {
            dataAttachments = docArrayList;
        }
        if (dataAttachments.length > 0) {
            for (var i = 0; i < dataAttachments.length; i++) {
                var txtId = parseInt(+ i + 1);
                dataAttachments[i].Remark = $('#txtAttach' + txtId).val();
                dataAttachments[i].SRNo = parseInt(+ i + 1);
            }
        }
        var attachmentObject =
        {
            AttachmentsModel: dataAttachments
        }
        CommonAjaxMethod(virtualPath + 'OnboardingRequest/SaveAttachmentDocuments', attachmentObject, 'POST', function (response) {
            //window.location.href = virtualPath + 'Onboarding/HRScreenOnboard';
            window.location.href = virtualPath + 'Onboarding/UserOnboarding?id=' + CandidateId;
        });
    }
}
function DeleteAttachment(ctrl, rowNo) {
    if (candodate_Status != true) {
        var url = $("#hdnUploadFileUrl" + rowNo).val();
        if (url != '') {
            ConfirmMsgBox("Are you sure want to delete this attach file?", '', function () {
                CommonAjaxMethod(virtualPath + 'CommonMethod/DeleteFile', { FileUrl: url }, 'POST', function (response) {
                    var AttachmentID = $('#hdnAttachmentID' + rowNo).val();
                    if (AttachmentID != "") {
                        CommonAjaxMethod(virtualPath + 'OnboardingRequest/DeleteAttachmentDocuments', { Id: AttachmentID, inputData: 5 }, 'POST', function (response) {
                            $('#hdnUploadActualFileName' + rowNo).val('');
                            $('#hdnUploadNewFileName' + rowNo).val('');
                            $('#hdnUploadFileUrl' + rowNo).val('');
                            $("#fileAttach" + rowNo).val(null);
                            $("#fileAttach" + rowNo).val('');
                            $('#lblAttachement' + rowNo).text('');
                            $("#txtAttach" + rowNo).val('');
                            ResetValidations(rowNo);
                            if (dataAttachments.length > 0) {
                                for (var i = 0; i < dataAttachments.length; i++) {
                                    var txtId = parseInt(+ i + 1);
                                    if (txtId == rowNo) {
                                        dataAttachments[i].AttachmentActualName = $('#hdnUploadActualFileName' + rowNo).val();
                                        dataAttachments[i].AttachmentNewName = $('#hdnUploadNewFileName' + rowNo).val();
                                        dataAttachments[i].AttachmentUrl = $('#hdnUploadFileUrl' + rowNo).val();
                                    }
                                }
                            }
                            if (rowNo > 13) {
                                $(ctrl).closest('tr').remove();
                                dataAttachments = dataAttachments.filter(function (itemParent) { return (itemParent.RowNum != rowNo); });
                            }
                        });
                    }
                    else {
                        $('#hdnUploadActualFileName' + rowNo).val('');
                        $('#hdnUploadNewFileName' + rowNo).val('');
                        $('#hdnUploadFileUrl' + rowNo).val('');
                        $("#fileAttach" + rowNo).val(null);
                        $("#fileAttach" + rowNo).val('');
                        $('#lblAttachement' + rowNo).text('');
                        $("#txtAttach" + rowNo).val('');
                        ResetValidations(rowNo);
                    }
                });
            })
        }
        else {
            FailToaster("File is not uploaded");
        }
    }
}
function UploadocumentReport(rowNo) {
    var fileUpload = $("#fileAttach" + rowNo).get(0);
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
                    $('#hdnUploadActualFileName' + rowNo).val(result.FileModel.ActualFileName);
                    $('#hdnUploadNewFileName' + rowNo).val(result.FileModel.NewFileName);
                    $('#hdnUploadFileUrl' + rowNo).val(result.FileModel.FileUrl);
                    $('#lblAttachement' + rowNo).text(result.FileModel.ActualFileName);
                    var anchorTag = document.getElementById('lblAttachementView' + rowNo);
                    // Update the href attribute
                    anchorTag.href = result.FileModel.FileUrl;
                    if (dataAttachments.length > 0) {
                        for (var i = 0; i < dataAttachments.length; i++) {
                            var txtId = parseInt(+ i + 1);
                            if (txtId == rowNo) {
                                dataAttachments[i].AttachmentActualName = $('#hdnUploadActualFileName' + rowNo).val();
                                dataAttachments[i].AttachmentNewName = $('#hdnUploadNewFileName' + rowNo).val();
                                dataAttachments[i].AttachmentUrl = $('#hdnUploadFileUrl' + rowNo).val();
                            }
                        }
                    }
                }
                else {
                    FailToaster(result.ErrorMessage);
                }
            }
            ,
            error: function (error) {
                FailToaster(error);
                isSuccess = false;
            }
        });
    }
    else {
        FailToaster("Please select file to attach!");
    }
}
var DocumentExtra = false;
function UploadOtherDocumentExtra() {
    //if (checkValidationOnSubmit('UploadOtherDocument') == true) {
    var DocOtherRemark = document.getElementById("txtDocOtherRemark").value;
    var DocOtherNature = document.getElementById("txtDocOtherNature").value;
    var valid = true;
    if (checkValidationOnSubmit('AddRowMandatory') == false) {
        valid = false;
    }

    if (valid == true) {
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
                url: virtualPath + 'CommonMethod/UploadOnBoardindDocument',
                type: "POST",
                contentType: false, // Not to set any content header
                processData: false, // Not to process data
                data: fileData,
                success: function (response) {
                    var result = JSON.parse(response);
                    DocumentExtra = true;
                    if (result.ErrorMessage == "") {
                        DocArrayId = DocArrayId == 0 ? parseInt(13 + 1) : parseInt(DocArrayId + 1);// DocArrayId + 1;
                        var loop = DocArrayId;
                        var objarrayinner =
                        {
                            RowNum: loop,
                            AttachmentID: 0,
                            CandidateId: CandidateId,
                            SRNo: loop,
                            AttachmentActualName: result.FileModel.ActualFileName,
                            AttachmentNewName: result.FileModel.NewFileName,
                            NatureofDocuments: DocOtherNature,
                            AttachmentUrl: result.FileModel.FileUrl,
                            Remark: DocOtherRemark
                        }

                        dataAttachments.push(objarrayinner);
                        var newtbblData = "<tr><td>" + objarrayinner.RowNum + "</td><td>" + objarrayinner.NatureofDocuments + "</td><td></td><td><input type='hidden' id='hdnNature" + loop + "' /><input type='hidden' id='hdnUploadFileUrl" + loop + "' /><input type='hidden' id='hdnUploadActualFileName" + loop + "' /><input type='hidden' id='hdnUploadNewFileName" + loop + "' /><input type='hidden' id='hdnAttachmentID" + loop + "' /><input type='hidden' id='hdnRemark" + loop + "' /><a target='_blank' href=" + objarrayinner.AttachmentUrl + ">" + objarrayinner.AttachmentActualName + "</a></td><td><textarea class='form-control maxSize[1200]' placeholder='Enter' id='txtAttach" + loop + "'>" + objarrayinner.Remark + "</textarea></td>" +
                            "<td><a title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='deleteDocArrayRows(this," + objarrayinner.SRNo + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr > ";

                        $("#tblDocumentList").find('tbody').append(newtbblData);
                        $('#hdnRemark' + loop).val(DocOtherRemark);
                        $('#hdnNature' + loop).val(DocOtherNature);
                        $('#hdnUploadActualFileName' + loop).val(result.FileModel.ActualFileName);
                        $('#hdnUploadNewFileName' + loop).val(result.FileModel.NewFileName);
                        $('#hdnUploadFileUrl' + loop).val(result.FileModel.FileUrl);
                        $('#lblAttachement' + loop).text(result.FileModel.ActualFileName);
                        // BindDocumentGrid(DocArray);
                        $("#fileDocOtherUpload").val('');
                        $("#fileDocOtherUpload").val(null);
                        $("#txtDocOtherRemark").val('');
                        $("#txtDocOtherNature").val('');
                    }
                    else {
                        FailToaster(result.ErrorMessage);
                    }
                }
                ,
                error: function (error) {
                    FailToaster(error);
                    isSuccess = false;
                }
            });
        }
        else {
            FailToaster("Please select file to attach!");
        }
    }
}
function deleteDocArrayRows(obj, id) {
    if (candodate_Status != true) {
        ConfirmMsgBox("Are you sure want to delete this attach file?", '', function () {
            var data = dataAttachments.filter(function (itemParent) { return (itemParent.SRNo == id); });
            //var url = data[0].AttachmentPath;
            //CommonAjaxMethod(virtualPath + 'CommonMethod/DeleteFile', { FileUrl: url }, 'POST', function (response) {
            $(obj).closest('tr').remove();
            dataAttachments = dataAttachments.filter(function (itemParent) { return (itemParent.SRNo != id); });
            /*});*/
        })
    }
}
function RedirectToClick(View) {
    window.location.href = virtualPath + 'Onboarding/' + View + '?id=' + CandidateId;
}
