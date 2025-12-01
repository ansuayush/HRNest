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
                $("#fileAttach").val(null);
                $("#fileAttach").val('');
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


function BindProcureRequest() {
    CommonAjaxMethod(virtualPath + 'ProcurementRequest/BindProjectDetails', { id: RequestId, IsBindLine: 8 }
        , 'GET', function (response) {
            var data1 = response.data.data.Table;

            if (data1[0].Remark != null) {
                $("#divRejectedReason").show();
                $("#divRejected").text(data1[0].Remark);

            }

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
                if (data3[0].Status >= 2) {
                    $('#dvNarration').hide();
                    $('#dvNarrationSave1').hide();
                    $('#dvNarrationSave2').hide();
                    $('#divDeleteAttach').hide();
                    $('#fileAttach').hide();
                    ignorRemove = 1;
                    $("#tbLastdateofRFP").prop('disabled', true);
                    $("#tbEstimatedStartDate").prop('disabled', true);
                    $("#tbEstimatedEndDate").prop('disabled', true);
                    $("#txtSpecialCond").prop('disabled', true);


                }
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
var MSMEArray = [];
var MSMEArrayId = 0;
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
function TDate(fromDate, todate, message) {
    var date = new Date();

    // add a day
    date.setDate(todate.getDate() + 1);

    var fromD = new Date(fromDate).getTime();
    var tod = new Date(date).getTime();
    if (fromD > tod) {
        FailToaster(message);
        return false;
    }
    return true;
}

function SaveRFPEntry(from) {
    var isValid = true;
    if (from == 1) {
        var obj =
        {
            IsResubmit: 1,
            Procure_Request_Id: RequestId,
            LastdateofRFP: ChangeDateFormat($('#tbLastdateofRFP').val()),
            AgreedscopeofworkNewFileName: $('#hdnUploadNewFileName').val(),
            AgreedscopeofworkActualFileName: $('#hdnUploadActualFileName').val(),
            AgreedscopeofworkActualFileUrl: $('#hdnUploadFileUrl').val(),
                EstimatedStartDate: $('#tbEstimatedStartDate').val()==""?null: ChangeDateFormat($('#tbEstimatedStartDate').val()),
                EstimatedEndDate: $('#tbEstimatedEndDate').val()==""?null: ChangeDateFormat($('#tbEstimatedEndDate').val()),
            PaymentTerms: $('#txtSpecialCond').val(),
            RFPPaymentTermsEntryList: MSMEArray,
            Status: from,
            ProcessType: 1

        }
        CommonAjaxMethod(virtualPath + 'ProcurementRequest/SaveRFPEntry', obj, 'POST', function (response) {

            
            window.location.href = "/Procurement/ProcurementUserRequest";
        });
    }
    else {

        if ($('#hdnUploadNewFileName').val() == "") {
            $('#spfileAttach').show();
            isValid = false;
        }
        else {
            $('#spfileAttach').hide();
        }

        var checkDate1 = false;
        var checkDate2 = false;
        var checkDate3 = false;
        if (checkValidationOnSubmit('Mandatory') == true && isValid == true) {


            checkDate1 = IsGreaterThanCurrentDate(ChangeDateFormat($('#tbLastdateofRFP').val()), ChangeDateFormat($('#tbEstimatedStartDate').val()), 'Error, please check start/end date.');
            if (checkDate1)
                checkDate2 = IsGreaterThanCurrentDate(ChangeDateFormat($('#tbLastdateofRFP').val()), ChangeDateFormat($('#tbEstimatedEndDate').val()), 'Error, please check start/end date.');
            if (checkDate2)
                checkDate3 = IsGreaterThanCurrentDate(ChangeDateFormat($('#tbEstimatedStartDate').val()), ChangeDateFormat($('#tbEstimatedEndDate').val()), 'Estimated start date should be always greater than Estimated End date.');

            if (checkDate1 == true && checkDate2 == true && checkDate3 == true) {
                var obj =
                {
                    IsResubmit: 1,
                    Procure_Request_Id: RequestId,
                    LastdateofRFP: ChangeDateFormat($('#tbLastdateofRFP').val()),
                    AgreedscopeofworkNewFileName: $('#hdnUploadNewFileName').val(),
                    AgreedscopeofworkActualFileName: $('#hdnUploadActualFileName').val(),
                    AgreedscopeofworkActualFileUrl: $('#hdnUploadFileUrl').val(),
                    EstimatedStartDate: ChangeDateFormat($('#tbEstimatedStartDate').val()),
                    EstimatedEndDate: ChangeDateFormat($('#tbEstimatedEndDate').val()),
                    PaymentTerms: $('#txtSpecialCond').val(),
                    RFPPaymentTermsEntryList: MSMEArray,
                    Status: from,
                    ProcessType: 1

                }
                CommonAjaxMethod(virtualPath + 'ProcurementRequest/SaveRFPEntry', obj, 'POST', function (response) {

                    window.location.href = "/Procurement/ProcurementUserRequest";
                });
            }

        }
    }
}
function OnClose() {
    var url = "/Procurement/ProcurementUserRequest";
    window.location.href = url;
}