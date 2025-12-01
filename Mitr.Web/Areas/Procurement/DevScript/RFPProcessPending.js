$(document).ready(function () {

    $(".allow_numeric").on("input", function (evt) {
        var self = $(this);
        self.val(self.val().replace(/\D/g, ""));
        if ((evt.which < 48 || evt.which > 57)) {
            evt.preventDefault();
        }
    });
    LoadMasterDropdown('ddlPOCA', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.EmployeeWithoutLWD,
        manualTableId: 0
    }, 'Select', false);



    BindProcureRequest();
    $(function () {
        $('.datepicker').datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: "dd-mm-yy",
            yearRange: "-90:+10"
        });

    });

    $("#fileAttach").change(function () {
        UploadocumentReport();
        $('#spfileAttach').hide();
    })




    CommonAjaxMethod(virtualPath + 'ProcurementRequest/GetMaxReqNo', null, 'GET', function (response) {
        $('#ReqNo').val(response.data.data.Table[0].ReqNo);
    });
    var dt = new Date();
    var newDate = ChangeDateFormatToddMMYYY(dt);
    $('#ReqDate').val(newDate);
    $('#hdnLoggedInUser').val(loggedinUserid);
    $('#Reqby').val(loggedinUserName);



});
function DeleteAttachment() {
    var url = $("#hdnUploadFileUrl").val();
    if (url != '') {
        var result = confirm("Are you sure want to delete this attach file?");
        if (result) {
            CommonAjaxMethod(virtualPath + 'CommonMethod/DeleteFile', { FileUrl: url }, 'POST', function (response) {
                $('#hdnUploadActualFileName').val('');
                $('#hdnUploadNewFileName').val('');
                $('#hdnUploadFileUrl').val('');

                $('#lblAttachement').text('');


            });
        }
    }
    else {

        FailToaster("File is not uploaded");
    }

}
function DownloadFile() {

    var fileURl = $('#hdnUploadFileUrl').val();
    var fileName = $('#hdnUploadActualFileName').val();
    if (fileURl != null || fileURl != undefined) {
        var stSplitFileName = fileName.split(".");
        var link = document.createElement("a");
        link.download = stSplitFileName[0];
        link.href = fileURl;
        link.click();
    }

}
function UploadocumentReport() {
    var fileUpload = $("#fileAttach").get(0);

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

                    $('#hdnUploadActualFileName').val(result.FileModel.ActualFileName);
                    $('#hdnUploadNewFileName').val(result.FileModel.NewFileName);
                    $('#hdnUploadFileUrl').val(result.FileModel.FileUrl);
                    $('#lblAttachement').text(result.FileModel.ActualFileName);

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
function AddNarration() {
    if (checkValidationOnSubmit('msmAdd') == true) {
        MSMEArrayId = MSMEArrayId + 1;
        var loop = MSMEArrayId;
        var objarrayinner =
        {
            Id: loop,
            PaymentTerms: $("#txtNarration").val(),
            Procure_Request_Id: RequestId
        }
        MSMEArray.push(objarrayinner);

        var newtbblData = "<tr><td>" + $("#txtNarration").val() + "</td><td><a title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='DeleteMSMEArray(this," + loop + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr>";

        $("#tblActivity").find('tbody').append(newtbblData);

        $("#txtNarration").val('');

    }
}
function DeleteMSMEArray(obj, id) {

    ConfirmMsgBox("Are you sure want to delete", '', function () {
        $(obj).closest('tr').remove();
        MSMEArray = MSMEArray.filter(function (itemParent) { return (itemParent.Id != id); });

    })
}
function Redirect() {
    var url = "/Procurement/ProcurementUserRequest";
    window.location.href = url;    
}


function BindProcureRequest() {
    CommonAjaxMethod(virtualPath + 'ProcurementRequest/BindProjectDetails', { id: RequestId, IsBindLine: 9 }
        , 'GET', function (response) {
            var data1 = response.data.data.Table;



            var data2 = response.data.data.Table1;
            var data3 = response.data.data.Table2;

            if (data1[0].Status == "9") {

                // $("#btnSaveRFP").show();
                $("#btnSaveLive").show();
                $("#btnRejectRFP").show();

            }
            else {
                $("#btnSaveRFP").hide();
                $("#btnSaveLive").hide();
                $("#btnRejectRFP").hide();
            }
            $("#ddlAgreementA").val(data1[0].ContractType).trigger('change');
            $("#ddlPOCA").val(data1[0].POC).trigger('change');
            $("#lblTotal1A").text(data1[0].BudgetAmount);
            $("#lblTotal2A").text(data1[0].RequiredAmount);
            $('#ReqbyA').val(data1[0].POCName);
            $('#ReqNoA').val(data1[0].Req_No);
            $('#ReqDateA').val(ChangeDateFormatToddMMYYY(data1[0].Req_Date));

            $('#datatableApproved').DataTable({
                "processing": true, // for show progress bar           
                "destroy": true,
                "data": data2,

                "columns": [
                    { "data": "RowNum" },
                    { "data": "ProjectName" },
                    { "data": "ProjectSubLineItem" },
                    { "data": "ProjectLineDesc" },
                    { "data": "ProjectManager" },
                    { "data": "Qty" },
                    {
                        "orderable": false,
                        data: null, render: function (data, type, row) {

                            return '<label class="text-right">' + NumberWithComma(row.BudgetAmount) + '</label>';
                        }
                    },



                    {
                        "orderable": false,
                        data: null, render: function (data, type, row) {

                            return '<label class="BudgetAmountData text-right">' + NumberWithComma(row.RequiredAmount) + '</label>';
                        }
                    },

                    {
                        "orderable": false,
                        data: null, render: function (data, type, row) {

                            return '<label>' + row.Remark + '</label>';
                        }
                    },

                    { "data": "Status" },
                    { "data": "ApproveRejectReason" }


                ]
            });

            if (data3.length > 0) {
                var ignorRemove = 0;

                MSMEArray = [];
                MSMEArrayId = 0;

                $('#tbLastdateofRFP').val(ChangeDateFormatToddMMYYY(data3[0].LastdateofRFP));
                $('#hdnUploadNewFileName').val(data3[0].AgreedscopeofworkNewFileName);
                $('#hdnUploadActualFileName').val(data3[0].AgreedscopeofworkActualFileName);
                $('#hdnUploadFileUrl').val(data3[0].AgreedscopeofworkActualFileUrl);
                $('#tbEstimatedStartDate').val(ChangeDateFormatToddMMYYY(data3[0].EstimatedStartDate));
                $('#tbEstimatedEndDate').val(ChangeDateFormatToddMMYYY(data3[0].EstimatedEndDate));
                $('#lblAttachement').text(data3[0].AgreedscopeofworkActualFileName);
                $('#txtSpecialCond').text(data3[0].PaymentTerms);

                if (data3[0].Status >= 3) {

                    $('#dvBankDetails').hide();
                    $('#btnSaveRFP').hide();
                    $('#btnRejectRFP').hide();
                    $('#dvNarration').hide();
                    $('#dvNarrationSave').hide();
                    $('#divDeleteAttach').hide();
                    $('#fileAttach').hide();
                    ignorRemove = 1;

                    $("#tbLastdateofRFP").prop('disabled', true);
                    $("#tbEstimatedStartDate").prop('disabled', true);
                    $("#tbEstimatedEndDate").prop('disabled', true);
                    $("#txtSpecialCond").prop('disabled', true);
                }


                var dVRegistrationMSMEOtherDetail = response.data.data.Table3;
                for (var i = 0; i < dVRegistrationMSMEOtherDetail.length; i++) {
                    MSMEArrayId = i + 1;
                    var objMSEB = {
                        Id: MSMEArrayId,
                        PaymentTerms: dVRegistrationMSMEOtherDetail[i].PaymentTerms

                    }
                    MSMEArray.push(objMSEB);


                }
                BindMSMEArray(MSMEArray, ignorRemove);
                var bb = response.data.data.Table4;
                BankDetailsArray = [];
                for (var i = 0; i < bb.length; i++) {

                    BankDetailsArrayId = i + 1;
                    var objarrayinner =
                    {
                        Id: BankDetailsArrayId,
                        Paymentinfavour: bb[i].Paymentinfavour,
                        RecipientBankDetails: bb[i].RecipientBankDetails,
                        Amount: bb[i].Amount,
                        IFSCCode: bb[i].IFSCCode,
                        AccountNo: bb[i].AccountNo,
                        BankName: bb[i].BankName,
                        BankDate: bb[i].BankDate,
                        AmountFilledByFinance: bb[i].AmountFilledByFinance,
                        UTRNo: bb[i].UTRNo,
                        Remarks: bb[i].Remarks

                    }
                    BankDetailsArray.push(objarrayinner);


                }
                BindBankDetails(BankDetailsArray, data3[0].Status);
            }



        });
}

function BindMSMEArray(array, ignorRemove) {
    if (ignorRemove == 1) {
        for (var i = 0; i < array.length; i++) {

            var newtbblData = "<tr><td>" + array[i].PaymentTerms + "</td><td></td></tr>";

            $("#tblActivity").find('tbody').append(newtbblData);
        }
    }
    else {
        for (var i = 0; i < array.length; i++) {

            var newtbblData = "<tr><td>" + array[i].PaymentTerms + "</td><td><a title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='DeleteMSMEArray(this," + array[i].Id + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr>";

            $("#tblActivity").find('tbody').append(newtbblData);
        }
    }

}
function BindBankDetails(array, status) {
    if (status >= 3) {
        $("#tblActivity1").hide();
        $("#tblFinanceFilled").show();
        for (var i = 0; i < array.length; i++) {
            var dbDate = array[i].BankDate != null || array[i].BankDate != "" ? ChangeDateFormatToddMMYYY(array[i].BankDate) : "";


            var newtbblData = '<tr><td>' + array[i].Paymentinfavour + '</td><td>' + array[i].RecipientBankDetails + '</td>' +
                '<td>' + array[i].BankName + '</td><td>' + array[i].AccountNo + '</td><td>' + array[i].IFSCCode + '</td>' +
                '<td class="text-right">' + NumberWithComma(array[i].Amount) + '</td>' +
                '<td>' + dbDate + '</span></td>' +
                '<td class="text-right">' + NumberWithComma(array[i].AmountFilledByFinance) + '</td>' +
                '<td> ' + array[i].UTRNo + '</td>' +
                '<td>' + array[i].Remarks + '</td> </tr>';

            $("#tblFinanceFilled").find('tbody').append(newtbblData);


        }
    }
    else {
        $("#tblActivity1").show();
        $("#tblFinanceFilled").hide();
        for (var i = 0; i < array.length; i++) {
            var newtbblData = "";// "<tr><td>" + array[i].Paymentinfavour + "</td><td>" + array[i].RecipientBankDetails + "</td><td>" + NumberWithComma(array[i].Amount) + "</td><td><a title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='DeleteBankDetails(this," + array[i].Id + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr>";

            if (status >= 3) {
                newtbblData = "<tr><td>" + array[i].Paymentinfavour + "</td><td>" + array[i].RecipientBankDetails + "</td><td>" + array[i].BankName + "</td><td>" + array[i].AccountNo + "</td><td>" + array[i].IFSCCode + "</td><td>" + NumberWithComma(array[i].Amount) + "</td><td></td></tr>";

            }
            else {
                newtbblData = "<tr><td>" + array[i].Paymentinfavour + "</td><td>" + array[i].RecipientBankDetails + "</td><td>" + array[i].BankName + "</td><td>" + array[i].AccountNo + "</td><td>" + array[i].IFSCCode + "</td><td>" + NumberWithComma(array[i].Amount) + "</td><td><a title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='DeleteBankDetails(this," + array[i].Id + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr>";

            }

            $("#tblActivity1").find('tbody').append(newtbblData);

        }
    }

}
function SaveReject() {

    if (checkValidationOnSubmit('Reject') == true) {

        var RequestApprovalArray = [];
        var obj =
        {
            Id: 0,
            UserId: loggedinUserid,
            ProcureId: RequestId,
            Reason: $("#txtRejectComment").val(),
            Status: 9
        }
        RequestApprovalArray.push(obj);
        CommonAjaxMethod(virtualPath + 'ProcurementRequest/ApproveRejectProcurementRegistration', RequestApprovalArray, 'POST', function (response) {
            Redirect();
        });
    }

}
var MSMEArray = [];
var MSMEArrayId = 0;

var BankDetailsArray = [];
var BankDetailsArrayId = 0;

function AddBankDetails() {
    if (checkValidationOnSubmit('msmPayment') == true) {
        if ($("#txtAccountNo").val() == '0') {
            FailToaster("Please fill account Number.");
        }
        else {
            BankDetailsArrayId = BankDetailsArrayId + 1;
            var loop = BankDetailsArrayId;



            var objarrayinner =
            {
                Id: loop,
                Paymentinfavour: $("#txtPaymentFavor").val(),
                RecipientBankDetails: $("#txtBankDetails").val(),
                Amount: $("#txtAmount").val(),
                BankName: $("#txtBankName").val(),
                IFSCCode: $("#txtIFSCCode").val(),
                AccountNo: $("#txtAccountNo").val()

            }
            BankDetailsArray.push(objarrayinner);

            var newtbblData = "<tr><td>" + objarrayinner.Paymentinfavour + "</td><td>" + objarrayinner.RecipientBankDetails + "</td><td>" + objarrayinner.BankName + "</td><td>" + objarrayinner.AccountNo + "</td><td>" + objarrayinner.IFSCCode + "</td><td>" + objarrayinner.Amount + "</td><td><a title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='DeleteBankDetails(this," + objarrayinner.Id + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr>";

            $("#tblActivity1").find('tbody').append(newtbblData);

            $("#txtPaymentFavor").val('');
            $("#txtBankDetails").val('');
            $("#txtAmount").val('');
            $("#txtBankName").val('');
            $("#txtAccountNo").val('');
            $("#txtIFSCCode").val('');
            $("#btnSaveRFP").show();
            $("#btnSaveLive").hide();

        }
    }
}
function DeleteBankDetails(obj, id) {

    ConfirmMsgBox("Are you sure want to delete", '', function ()
    {
        $(obj).closest('tr').remove();
        BankDetailsArray = BankDetailsArray.filter(function (itemParent) { return (itemParent.Id != id); });
        if (BankDetailsArray.length == 0)
        {
            $("#btnSaveRFP").hide();
            $("#btnSaveLive").show();
        }
    })
}
var MSMEArray = [];
var MSMEArrayId = 0;


function SaveRFPPublished() {
    var checkDate1 = false;
    var checkDate2 = false;
    var checkDate3 = false;

    checkDate1 = IsGreaterThanCurrentDate(ChangeDateFormat($('#tbLastdateofRFP').val()), ChangeDateFormat($('#tbEstimatedStartDate').val()), 'Error, please check start/end date.');
    if (checkDate1)
        checkDate2 = IsGreaterThanCurrentDate(ChangeDateFormat($('#tbLastdateofRFP').val()), ChangeDateFormat($('#tbEstimatedEndDate').val()), 'Error, please check start/end date.');
    if (checkDate2)
        checkDate3 = IsGreaterThanCurrentDate(ChangeDateFormat($('#tbEstimatedStartDate').val()), ChangeDateFormat($('#tbEstimatedEndDate').val()), 'Estimated start date should be always greater than Estimated End date.');

    if (checkDate1 == true && checkDate2 == true && checkDate3 == true) {
        if (BankDetailsArray.length > 0) {

            var obj =
            {
                IsResubmit: 1,
                Procure_Request_Id: RequestId,
                RFPPublishedDetailsList: BankDetailsArray,
                Status: 3,
                ProcessType: 2,
                LastdateofRFP: ChangeDateFormat($('#tbLastdateofRFP').val()),
                AgreedscopeofworkNewFileName: $('#hdnUploadNewFileName').val(),
                AgreedscopeofworkActualFileName: $('#hdnUploadActualFileName').val(),
                AgreedscopeofworkActualFileUrl: $('#hdnUploadFileUrl').val(),
                EstimatedStartDate: ChangeDateFormat($('#tbEstimatedStartDate').val()),
                EstimatedEndDate: ChangeDateFormat($('#tbEstimatedEndDate').val()),
                PaymentTerms: $('#txtSpecialCond').val(),
                RFPPaymentTermsEntryList: MSMEArray


            }
            CommonAjaxMethod(virtualPath + 'ProcurementRequest/SaveRFPEntry', obj, 'POST', function (response) {
                Redirect();
            });

        }
        else {
            FailToaster('Please add atleast record.');
        }
    }
}
function OnClose() {
    var url = "/Procurement/ProcurementUserRequest";
    window.location.href = url;
}
$("#fileDocOtherUpload").change(function () {
    $('#spfileDocOtherUpload').hide();
})
var DocArray = [];
var DocArrayId = 0;
function AddDocumentForLive() {
    var fileUpload = $("#fileDocOtherUpload").get(0);
    var files = fileUpload.files;
    if (files.length > 0) {
        $("#spfileDocOtherUpload").hide();
    }
    else {
        $("#spfileDocOtherUpload").show();
    }

    if (checkValidationOnSubmit('UploadOtherDocument') == true) {

        var DocOtherRemark = document.getElementById("txtDocOtherRemark").value;

        var DocOtherNature = document.getElementById("txtDocOtherNature").value;




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

                        var objarrayinner =
                        {
                            AttachmentActualName: result.FileModel.ActualFileName,
                            AttachmentNewName: result.FileModel.NewFileName,
                            Source: DocOtherNature,
                            AttachmentPath: result.FileModel.FileUrl,
                            Remarks: DocOtherRemark,
                            Procure_Request_Id: RequestId

                        }

                        DocArray.push(objarrayinner);

                        var newtbblData = "<tr><td>" + objarrayinner.Source + "</td><td>" + objarrayinner.AttachmentActualName + "</td><td>" + objarrayinner.Remarks + "</td>" +
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
            $("#spfileDocOtherUpload").show();

        }

    }


}
function deleteDocArrayRows(obj, id) {
    ConfirmMsgBox("Are you sure want to delete", '', function () {
        var data = DocArray.filter(function (itemParent) { return (itemParent.Id == id); });
        var url = data[0].AttachmentPath;

        CommonAjaxMethod(virtualPath + 'CommonMethod/DeleteFile', { FileUrl: url }, 'POST', function (response) {
            $(obj).closest('tr').remove();
            DocArray = DocArray.filter(function (itemParent) { return (itemParent.Id != id); });
        });

    })

}
function BindDocumentGrid(array) {
    for (var i = 0; i < array.length; i++) {
        var newtbblData = "<tr><td>" + array[i].Source + "</td><td></td><td>" + array[i].AttachmentActualName + "</td><td>" + array[i].Remarks + "</td>" +
            "<td><a title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='deleteDocArrayRows(this," + array[i].Id + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr > ";

        $("#tblDocumentList").find('tbody').append(newtbblData);
    }

}
function SaveLiveDetails() {
    if (DocArray.length > 0) {
        var obj =
        {
            IsResubmit: 1,
            Procure_Request_Id: RequestId,
            Status: 7,
            ProcessType: 4,
            RFPLiveList: DocArray


        }
        CommonAjaxMethod(virtualPath + 'ProcurementRequest/SaveRFPEntry', obj, 'POST', function (response) {
            Redirect();
        });
    }
    else {
        FailToaster("Please fill required details.");
    }


}