$(document).ready(function () {

    $(".allow_numeric").on("input", function (evt)
    {
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

function validate(evt) {
    var theEvent = evt || window.event;

    // Handle paste
    if (theEvent.type === 'paste') {
        key = event.clipboardData.getData('text/plain');
    } else {
        // Handle key press
        var key = theEvent.keyCode || theEvent.which;
        key = String.fromCharCode(key);
    }
    var regex = /[0-9]|\./;
    if (!regex.test(key)) {
        theEvent.returnValue = false;
        if (theEvent.preventDefault) theEvent.preventDefault();
    }
}
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

function Redirect() {
    var url = "/Procurement/RFPEntry?id=" + $("#hdnProcureRequestId").val();
    window.location.href = url;
}

var PaymentDetails = [];

function BindProcureRequest() {
    CommonAjaxMethod(virtualPath + 'ProcurementRequest/BindProjectDetails', { id: RequestId, IsBindLine: 10 }
        , 'GET', function (response) {
            var data1 = response.data.data.Table;



            var data2 = response.data.data.Table1;
            var data3 = response.data.data.Table2;


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
                "paging": false,
                "info": false,
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


                $('#tbLastdateofRFP').val(ChangeDateFormatToddMMYYY(data3[0].LastdateofRFP));
                $('#hdnUploadNewFileName').val(data3[0].AgreedscopeofworkNewFileName);
                $('#hdnUploadActualFileName').val(data3[0].AgreedscopeofworkActualFileName);
                $('#hdnUploadFileUrl').val(data3[0].AgreedscopeofworkActualFileUrl);
                $('#tbEstimatedStartDate').val(ChangeDateFormatToddMMYYY(data3[0].EstimatedStartDate));
                $('#tbEstimatedEndDate').val(ChangeDateFormatToddMMYYY(data3[0].EstimatedEndDate));
                $('#lblAttachement').text(data3[0].AgreedscopeofworkActualFileName);
                $('#txtSpecialCond').text(data3[0].PaymentTerms);
                $('#lblApprovedDate').text(ChangeDateFormatToddMMYYY(data3[0].modifiedat));
                $('#lblApprovedBy').text(data3[0].ApprovedBy);

                var dVRegistrationMSMEOtherDetail = response.data.data.Table3;
                for (var i = 0; i < dVRegistrationMSMEOtherDetail.length; i++) {

                    var objMSEB =
                    {
                        PaymentTerms: dVRegistrationMSMEOtherDetail[i].PaymentTerms

                    }
                    MSMEArray.push(objMSEB);


                }
                BindMSMEArray(MSMEArray);
                PaymentDetails = response.data.data.Table4;
                BindBankDetails(response.data.data.Table4);
            }

        });
}

function BindMSMEArray(array) {
    for (var i = 0; i < array.length; i++) {

        var newtbblData = "<tr><td>" + array[i].PaymentTerms + "</td>";

        $("#tblActivity").find('tbody').append(newtbblData);
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
            Status: 10
        }
        RequestApprovalArray.push(obj);
        CommonAjaxMethod(virtualPath + 'ProcurementRequest/ApproveRejectProcurementRegistration', RequestApprovalArray, 'POST', function (response) {
            Redirect();
        });
    }

}
function Redirect()
{
    var url = "/Procurement/PaymentApprovalAcknowledgement";
    window.location.href = url;
}
var MSMEArray = [];
var MSMEArrayId = 0;

var BankDetailsArray = [];
var BankDetailsArrayId = 0;

function BindBankDetails(array)
{ 
    for (var i = 0; i < array.length; i++)
    {
        var dbDate = array[i].BankDate != null || array[i].BankDate != "" ? ChangeDateFormatToddMMYYY(array[i].BankDate) : "";

        if (array[i].BankDate!= null)
        {
            $("#btnSubmitData").hide();
        }
        
        var newtbblData = '<tr><td>' + array[i].Paymentinfavour + '</td><td>' + array[i].RecipientBankDetails + '</td><td>' + array[i].BankName + '</td>' +
            '<td>' + array[i].AccountNo + '</td><td>' + array[i].IFSCCode + '</td><td class="text-right">' +NumberWithComma(array[i].Amount) + '</td>' +
            '<td><input type="text" autocomplete="off" value="' + dbDate + '" class="form-control Finance datepicker" placeholder="Enter" id="tbDate_' + array[i].Id + '" onchange="HideErrorMessage(this)"> <span id="sptbDate_' + array[i].Id + '" class="text-danger field-validation-error" style="display:none;">Hey, You missed this field!!</span></td>' +
            '<td><input type="text" alt="" value="' + array[i].AmountFilledByFinance + '" onchange="HideErrorMessage(this)" onkeypress="validate(event)" onpaste="validate(event)" class="form-control Finance" placeholder="Enter" id="tbAmount_' + array[i].Id + '"> <span id="sptbAmount_' + array[i].Id + '" class="text-danger field-validation-error" style="display:none;">Hey, You missed this field!!</span></td>' +
            '<td><input type="text" alt="" value="' + array[i].UTRNo + '" onchange="HideErrorMessage(this)" class="form-control Finance" placeholder="Enter" id="tdUtr_' + array[i].Id + '"> <span id="sptdUtr_' + array[i].Id + '" class="text-danger field-validation-error" style="display:none;">Hey, You missed this field!!</span></td>' +
            '<td><textarea class="form-control maxSize[1200]" placeholder="Explain Hare" onchange="HideErrorMessage(this)" id="tbRemark_' + array[i].Id + '">' + array[i].Remarks + '</textarea> <span id="sptbRemark_' + array[i].Id + '" class="text-danger field-validation-error" style="display:none;">Hey, You missed this field!!</span></td></tr>';

        $("#tblActivity1").find('tbody').append(newtbblData);


    }
}
function DeleteBankDetails(obj, id) {

    ConfirmMsgBox("Are you sure want to delete", '', function () {
        $(obj).closest('tr').remove();
        BankDetailsArray = BankDetailsArray.filter(function (itemParent) { return (itemParent.Id != id); });

    })
}
var MSMEArray = [];
var MSMEArrayId = 0;


function SaveRFPPublished() {
    var data = [];



    if (checkValidationOnSubmit('Finance') == true) {
        
         

        for (var i = 0; i < PaymentDetails.length; i++)
        {
            var obj =
            {
                Id: PaymentDetails[i].Id,
                Amount: $("#tbAmount_" + PaymentDetails[i].Id).val(),
                BankDate: ChangeDateFormat($("#tbDate_" + PaymentDetails[i].Id).val()),
                UTRNo: $("#tdUtr_" + PaymentDetails[i].Id).val(),
                Remarks: $("#tbRemark_" + PaymentDetails[i].Id).val(),
            }

            data.push(obj);
        }
        


        var obj =
        {
            IsResubmit: 1,
            Procure_Request_Id: RequestId,
            RFPPublishedDetailsList: data,
            Status: 5,
            ProcessType: 3

        }
        CommonAjaxMethod(virtualPath + 'ProcurementRequest/SaveRFPEntry', obj, 'POST', function (response)
        {
            Redirect();
        });
    }


}
function OnClose() {
    var url = "/Procurement/RFPEntry"
    window.location.href = url;
}