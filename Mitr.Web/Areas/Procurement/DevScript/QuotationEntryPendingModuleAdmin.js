$(document).ready(function () {

    $(".allow_numeric").on("input", function (evt) {
        var self = $(this);
        self.val(self.val().replace(/\D/g, ""));
        if ((evt.which < 48 || evt.which > 57)) {
            evt.preventDefault();
        }
    });

    FillVendor();
    GetQuotationMember();
    GetQuotationMessage();
    LoadMasterDropdown('ddlPOCA', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.EmployeeWithoutLWD,
        manualTableId: 0
    }, 'Select', false);

    LoadMasterDropdown('ddlVendor_0', {
        ParentId: RequestId,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: 13,
        manualTableId: 0
    }, 'Select', false);



    BindProcureRequest();
    GetQuotationEntry();
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



    $(function () {
        $('.datepicker').datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: "dd-mm-yy",
            yearRange: "-90:+10"
        });

    });




});
$('body').on('focus', ".datepicker1", function () {
    $(this).datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: "dd-mm-yy",
        yearRange: "-90:+10"
    });
});

 

function RedirectAuthSign() {

    var url = "/Procurement/ProcurementUserRequest";
    window.location.href = url;
}
function ForwardToAuthorized()
{
    ConfirmMsgBox("Are you sure want to Forward To Authorized Signatory ", '', function ()
    {       

        var RequestApprovalArray = [];
        var obj =
        {
            Id: 0,
            UserId: loggedinUserid,
            ProcureId: RequestId,
            Reason: '',
            Status: 12
        }
        RequestApprovalArray.push(obj);
        CommonAjaxMethod(virtualPath + 'ProcurementRequest/ApproveRejectProcurementRegistration', RequestApprovalArray, 'POST', function (response) {
            var url = "/Procurement/ProcurementUserRequest";
            window.location.href = url;
        });

    });
}
function ApproveReject(from)
{
 
    var RequestApprovalArray = [];
    if (from == "1") {
        ConfirmMsgBox("Pls Reconfirm", '', function () {
            var objIDs =
            {
                Procure_Request_Id: RequestId,
                Status: from
            }
            RequestApprovalArray.push(objIDs);


            var obj =
            {

                ProcessType: 6,
                RFPPaymentTermsEntryList: RequestApprovalArray

            }
            CommonAjaxMethod(virtualPath + 'ProcurementRequest/SaveRFPEntry', obj, 'POST', function (response) {
                RedirectAuthSign();
            });

        });
    }

    if (from == "2") {
        ConfirmMsgBox("Are you sure want to Reject", '', function () {
            var objIDs =
            {
                Procure_Request_Id: RequestId,
                Status: from
            }
            RequestApprovalArray.push(objIDs);


            var obj =
            {

                ProcessType: 6,
                RFPPaymentTermsEntryList: RequestApprovalArray

            }
            CommonAjaxMethod(virtualPath + 'ProcurementRequest/SaveRFPEntry', obj, 'POST', function (response) {
                RedirectAuthSign();
            });

        });
    }

}
function ClearData(from)
{
    if (from == 2)
    {
        var ctrl = document.getElementById('ddlType');
        $('#hdnUnitType').val(ctrl.value);
        isChanged = 1;
        $("#datatableQoutationEntry").find("tr:gt(1)").remove();
        var text = ctrl.options[ctrl.selectedIndex].text;
        $('#lblUnitType_0').text(text);
        SetMultiValue(ctrl);
        $("#datatableUnit").find("tr:gt(1)").remove();
        $("#datatableMultiple").find("tr:gt(1)").remove();
        UnitArray = [];
        MultiUnitArray = [];
    }
    else
    {
        document.getElementById('ddlType').value = $('#hdnUnitType').val();
        $('#select2-ddlType-container').text($('#lblUnitType_0').text());
    }
}
function SetQuotationUnit(ctrl)
{
     
    var a = ctrl.value;
    if (a!= "Select")
    {       
        if ($('#lblUnitType_0').text() != '')
        {
             
            $("#btnSingleUnit").click();
             
        }
        else
        {

            var text = ctrl.options[ctrl.selectedIndex].text;
            
            $('#hdnUnitType').val(a);
            $('#lblUnitType_0').text(text);
            SetMultiValue(ctrl);
        }
    }
}
function OpenModelPopupSingle(ctrl)
{
    var id = ctrl.id.split('_');
    var controlNo = id[1];

    var qId = $("#lblSRNo_" + controlNo).text();
    $("#hdnMultiValue").val(controlNo);
    var muArray = UnitArray.filter(function (itemParent) { return (itemParent.QuoteEntryId == qId); });
    $("#datatableUnit").find("tr:gt(1)").remove();

    for (var i = 0; i < muArray.length; i++) {


        var newtbblData = "<tr><td>" + muArray[i].Description + "</td><td>" + muArray[i].Unit + "</td><td>" + muArray[i].UnitRate + "</td><td>" + muArray[i].Amount + "</td><td>" + muArray[i].Tax + "</td><td>" + muArray[i].Tax_GST + "</td><td>" + muArray[i].Frieght + "</td><td><a class='HideClass' title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='DeleteUnitRateMulti(this," + muArray[i].Id + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr>";

        $("#datatableUnit").find('tbody').append(newtbblData);


    }
    $("#btnSingle").click();

}
function OpenModelPopupMulti(ctrl)
{
    var id = ctrl.id.split('_');
    var controlNo = id[1];
    var qId = $("#lblSRNo_" + controlNo).text();
    $("#hdnMultiValue").val(controlNo);
    $("#hdnMultiValue").val(controlNo);

    var mArray = MultiUnitArray.filter(function (itemParent) { return (itemParent.QuoteEntryId == qId); });


    $("#datatableMultiple").find("tr:gt(1)").remove();
    for (var i = 0; i < mArray.length; i++) {
        var newtbblData = "<tr><td>" + mArray[i].Description + "</td><td>" + mArray[i].UnitRate + "</td><td>" + mArray[i].Tax + "</td><td>" + mArray[i].Tax_GST + "</td><td><a class='HideClass' title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='DeleteUnitRate(this," + mArray[i].Id + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr>";

        $("#datatableMultiple").find('tbody').append(newtbblData);
    }
    $("#btnMulti").click();

}
function SetUnitEnable(from)
{
    fromMulitUnit = 0;
    var controlNo = $("#hdnMultiValue").val();
    if (from == 1)
    {
        $("#txtUnit_" + controlNo).val('0');
        $("#txtUnit_" + controlNo).prop('disabled', false);
    }
    if (from == 3)
    {

        $("#txtUnitRate_" + controlNo).val('0');
        $("#txtUnit_" + controlNo).val('0');
        $("#txtAmount_" + controlNo).val('0');
        $("#txtAmount_" + controlNo).val('0');
        $("#txtGST_" + controlNo).val('0');
        $("#txtFreight_" + controlNo).val('0');
        $("#txtTotal_" + controlNo).val('0');
        $("#txtTaxPer_" + controlNo).val('0');
        $("#txtTaxPer_" + controlNo).prop('disabled', false);
        $("#txtUnit_" + controlNo).prop('disabled', false);
        $("#txtUnitRate_" + controlNo).prop('disabled', false);
        $("#txtAmount_" + controlNo).prop('disabled', false);
        $("#txtGST_" + controlNo).prop('disabled', false);
        $("#txtFreight_" + controlNo).prop('disabled', false);
        $("#txtTotal_" + controlNo).prop('disabled', true);
    }
}
function SetUnitDisable(from) {
    fromMulitUnit = 0;
    var controlNo = '0';
    if (from == 2)
    {
        $("#txtUnit_" + controlNo).val('0');
        $("#txtUnit_" + controlNo).prop('disabled', true);

        var qId = $("#lblSRNo_" + controlNo).text();


        var mArray = MultiUnitArray.filter(function (itemParent) { return (itemParent.QuoteEntryId == qId); });


        $("#datatableMultiple").find("tr:gt(1)").remove();
        for (var i = 0; i < mArray.length; i++) {
            var newtbblData = "<tr><td>" + mArray[i].Description + "</td><td>" + mArray[i].UnitRate + "</td><td>" + mArray[i].Tax + "</td><td>" + mArray[i].Tax_GST + "</td><td><a class='HideClass' title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='DeleteUnitRate(this," + mArray[i].Id + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr>";

            $("#datatableMultiple").find('tbody').append(newtbblData);
        }
      

    }
    if (from == 4)
    {
        $("#txtUnitRate_" + controlNo).val('0');
        $("#txtUnit_" + controlNo).val('0');
        $("#txtAmount_" + controlNo).val('0');
        $("#txtGST_" + controlNo).val('0');
        $("#txtFreight_" + controlNo).val('0');
        $("#txtTotal_" + controlNo).val('0');
        $("#txtTaxPer_" + controlNo).val('0');
        $("#txtTaxPer_" + controlNo).prop('disabled', true);
        $("#txtUnit_" + controlNo).prop('disabled', true);
        $("#txtUnitRate_" + controlNo).prop('disabled', true);
        $("#txtAmount_" + controlNo).prop('disabled', true);
        $("#txtGST_" + controlNo).prop('disabled', true);
        $("#txtFreight_" + controlNo).prop('disabled', true);
        $("#txtTotal_" + controlNo).prop('disabled', true);

        var qId = $("#lblSRNo_" + controlNo).text();


        var muArray = UnitArray.filter(function (itemParent) { return (itemParent.QuoteEntryId == qId); });


        $("#datatableUnit").find("tr:gt(1)").remove();
        for (var i = 0; i < muArray.length; i++) {


            var newtbblData = "<tr><td>" + muArray[i].Description + "</td><td>" + muArray[i].Unit + "</td><td>" + muArray[i].UnitRate + "</td><td>" + muArray[i].Amount + "</td><td>" + muArray[i].Tax + "</td><td>" + muArray[i].Tax_GST + "</td><td>" + muArray[i].Frieght + "</td><td><a class='HideClass' title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='DeleteUnitRateMulti(this," + mArray[i].Id + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr>";

            $("#datatableUnit").find('tbody').append(newtbblData);
        }
      
    }



}
var setOnchange = 0;
var setOnchangeUnit = 0;
function SetMultiValue(ctrl)
{
    var controlNo = '0';
    fromMulitUnit = 0;
    $('#rcmicon_' + controlNo).hide();
    $('#uniticon_' + controlNo).hide();     
    $("#txtUnit_" + controlNo).val('0');
    $("#txtUnitRate_" + controlNo).val('0');
    $("#txtAmount_" + controlNo).val('0');
    $("#txtFreight_" + controlNo).val('0');
    $("#txtGST_" + controlNo).val('0');
    $("#txtTotal_" + controlNo).val('0');
    $("#txtTaxPer_" + controlNo).val('0');
    $("#txtTaxPer_" + controlNo).prop('disabled', false);
    $("#txtUnit_" + controlNo).prop('disabled', false);
    $("#txtUnitRate_" + controlNo).prop('disabled', false);
    $("#txtAmount_" + controlNo).prop('disabled', false);
    $("#txtGST_" + controlNo).prop('disabled', true);
    $("#txtFreight_" + controlNo).prop('disabled', false);
    $("#txtTotal_" + controlNo).prop('disabled', true);

    if (ctrl.value == 'Lumpsum')
    {
        $("#txtUnit_" + controlNo).val('0');
        $("#txtUnitRate_" + controlNo).val('0');
        $("#txtAmount_" + controlNo).val('0');
        $("#txtFreight_" + controlNo).val('0');
        $("#txtAmount_" + controlNo).prop('disabled', false);
        $("#txtUnit_" + controlNo).prop('disabled', true);
        $("#txtUnitRate_" + controlNo).prop('disabled', true);
        $("#txtTotal_" + controlNo).val('0');
    }    

    var a = ctrl.value;

    if (a == "rcmultiple")
    {       

        $('#rcmicon_' + controlNo).show();
        $('#uniticon_' + controlNo).hide();
        $("#txtTaxPer_" + controlNo).val('0');       
        $("#txtUnit_" + controlNo).val('0');
        $("#txtTaxPer_" + controlNo).prop('disabled', true);
        $("#txtUnit_" + controlNo).prop('disabled', true);
        $("#txtUnitRate_" + controlNo).prop('disabled', true);
        $("#txtAmount_" + controlNo).prop('disabled', true);
        $("#txtGST_" + controlNo).prop('disabled', true);
        $("#txtFreight_" + controlNo).prop('disabled', false);
        $("#txtTotal_" + controlNo).prop('disabled', true);
        var qId = $("#lblSRNo_" + controlNo).text();


        var mArray = MultiUnitArray.filter(function (itemParent) { return (itemParent.QuoteEntryId == qId); });


        $("#datatableMultiple").find("tr:gt(1)").remove();
        for (var i = 0; i < mArray.length; i++) {
            var newtbblData = "<tr><td>" + mArray[i].Description + "</td><td>" + mArray[i].UnitRate + "</td><td>" + mArray[i].Tax + "</td><td>" + mArray[i].Tax_GST + "</td><td><a class='HideClass' title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='DeleteUnitRate(this," + mArray[i].Id + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr>";

            $("#datatableMultiple").find('tbody').append(newtbblData);
        }

    }
    if (a == "rcsingle")
    {
       
        $("#txtUnitRate_" + controlNo).val('0');
        $("#txtUnit_" + controlNo).val('0');
        $("#txtAmount_" + controlNo).val('0');
        $("#txtAmount_" + controlNo).val('0');
        $("#txtGST_" + controlNo).val('0');
        $("#txtFreight_" + controlNo).val('0');
        $("#txtTotal_" + controlNo).val('0');
        $("#txtTaxPer_" + controlNo).val('0');
        $("#txtTaxPer_" + controlNo).prop('disabled', false);
        $("#txtUnit_" + controlNo).prop('disabled', false);
        $("#txtUnitRate_" + controlNo).prop('disabled', false);
        $("#txtAmount_" + controlNo).prop('disabled', true);
        $("#txtGST_" + controlNo).prop('disabled', true);
        $("#txtFreight_" + controlNo).prop('disabled', false);
        $("#txtTotal_" + controlNo).prop('disabled', true);

    }
    if (a == "SingleUnit")
    {
        
        $("#txtUnitRate_" + controlNo).val('0');
        $("#txtUnit_" + controlNo).val('0');
        $("#txtAmount_" + controlNo).val('0');
        $("#txtAmount_" + controlNo).val('0');
        $("#txtGST_" + controlNo).val('0');
        $("#txtFreight_" + controlNo).val('0');
        $("#txtTotal_" + controlNo).val('0');
        $("#txtTaxPer_" + controlNo).val('0');
        $("#txtTaxPer_" + controlNo).prop('disabled', false);
        $("#txtUnit_" + controlNo).prop('disabled', false);
        $("#txtUnitRate_" + controlNo).prop('disabled', false);
        $("#txtAmount_" + controlNo).prop('disabled', true);
        $("#txtGST_" + controlNo).prop('disabled', true);
        $("#txtFreight_" + controlNo).prop('disabled', false);
        $("#txtTotal_" + controlNo).prop('disabled', true);

    }
    if (a == "MultipleUnit")
    {

        $('#rcmicon_' + controlNo).hide();
        $('#uniticon_' + controlNo).show();
        $("#txtUnitRate_" + controlNo).val('0');
        $("#txtUnit_" + controlNo).val('0');
        $("#txtAmount_" + controlNo).val('0');
        $("#txtGST_" + controlNo).val('0');
        $("#txtFreight_" + controlNo).val('0');
        $("#txtTotal_" + controlNo).val('0');
        $("#txtTaxPer_" + controlNo).val('0');
        $("#txtTaxPer_" + controlNo).prop('disabled', true);
        $("#txtUnit_" + controlNo).prop('disabled', true);
        $("#txtUnitRate_" + controlNo).prop('disabled', true);
        $("#txtAmount_" + controlNo).prop('disabled', true);
        $("#txtGST_" + controlNo).prop('disabled', true);
        $("#txtFreight_" + controlNo).prop('disabled', true);
        $("#txtTotal_" + controlNo).prop('disabled', true);

        var qId = $("#lblSRNo_" + controlNo).text();


        var muArray = UnitArray.filter(function (itemParent) { return (itemParent.QuoteEntryId == qId); });


        $("#datatableUnit").find("tr:gt(1)").remove();
        for (var i = 0; i < muArray.length; i++) {


            var newtbblData = "<tr><td>" + muArray[i].Description + "</td><td>" + muArray[i].Unit + "</td><td>" + muArray[i].UnitRate + "</td><td>" + muArray[i].Amount + "</td><td>" + muArray[i].Tax + "</td><td>" + muArray[i].Tax_GST + "</td><td>" + muArray[i].Frieght + "</td><td><a class='HideClass' title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='DeleteUnitRateMulti(this," + mArray[i].Id + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr>";

            $("#datatableUnit").find('tbody').append(newtbblData);
        }
    } 
    
}
function UploadocumentProof(ctrl) {
    var id = ctrl.id.split('_');
    var controlNo = id[1];

    var ctrilId = ctrl.id;
    var fileUpload = $("#" + ctrilId).get(0);

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

                    $('#hdnUploadActualFileName_' + controlNo).text(result.FileModel.ActualFileName);
                    $('#hdnUploadNewFileName_' + controlNo).text(result.FileModel.NewFileName);
                    $('#hdnUploadFileUrl_' + controlNo).text(result.FileModel.FileUrl);


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
function DownloadFileRFP()
{
    var fileURl = $('#hdnfileScopeFileUrl').val();
    var fileName = $('#hdnfileScopeActualName').val();
    if (fileURl != null || fileURl != undefined) {
        var stSplitFileName = fileName.split(".");
        var link = document.createElement("a");
        link.download = stSplitFileName[0];
        link.href = fileURl;
        link.click();
    }
}

function DownloadFileQuotation(ctrl)
{
    var id = ctrl.id.split('_');
    var controlNo = id[1];
    var fileURl = $('#hdnUploadFileUrl_' + controlNo).text();
    var fileName = $('#hdnUploadActualFileName_' + controlNo).text();
    if (fileURl != null || fileURl != undefined) {
        var stSplitFileName = fileName.split(".");
        var link = document.createElement("a");
        link.download = stSplitFileName[0];
        link.href = fileURl;
        link.click();
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
function DownloadQuoteFiles(id)
{
    var fileURl = $('#hdnPublishUrl_' + id).val();
    var fileName = $('#hdnPublishAName_' + id).val();
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

function DeleteMSMEArray(obj, id) {

    ConfirmMsgBox("Are you sure want to delete", '', function () {
        $(obj).closest('tr').remove();
        MSMEArray = MSMEArray.filter(function (itemParent) { return (itemParent.Id != id); });

    })
}
 

function BindProcureRequest() {
    CommonAjaxMethod(virtualPath + 'ProcurementRequest/BindProjectDetails', { id: RequestId, IsBindLine: 9 }
        , 'GET', function (response) {
            var data1 = response.data.data.Table;



            var data2 = response.data.data.Table1;
            var data3 = response.data.data.Table2;


            $("#ddlAgreementA").val('Purchase Order (PO)').trigger('change');
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

                            return '<label class="BudgetAmountData text-right">' + NumberWithComma( row.RequiredAmount) + '</label>';
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

            var newtbblData = "<tr><td>" + array[i].PaymentTerms + "</td><td><a class='HideClass' title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='DeleteMSMEArray(this," + array[i].Id + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr>";

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
                '<td class="text-right">' +NumberWithComma(array[i].Amount)+ '</td>' +
                '<td>' + dbDate + '</span></td>' +
                 '<td class="text-right">' +NumberWithComma(array[i].AmountFilledByFinance)+ '</td>' +
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
                newtbblData = "<tr><td>" + array[i].Paymentinfavour + "</td><td>" + array[i].RecipientBankDetails + "</td><td>" + NumberWithComma(array[i].Amount) + "</td><td></td></tr>";

            }
            else {
                newtbblData = "<tr><td>" + array[i].Paymentinfavour + "</td><td>" + array[i].RecipientBankDetails + "</td><td>" + NumberWithComma(array[i].Amount) + "</td><td><a class='HideClass' title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='DeleteBankDetails(this," + array[i].Id + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr>";

            }

            $("#tblActivity1").find('tbody').append(newtbblData);

        }
    }

}
 
function RedirectToAgreement(ctr) {
    var ddlValue = ctr.value;
    var btn = document.getElementById("btnRedirectToRegistration");

     
    if (ddlValue == 'Consultant') {
        btn.href = "/Procurement/QuotationEntryPendingConsultant?id=" + RequestId;
        btn.click();
    }
    if (ddlValue == 'Sub-grant') {
        btn.href = "/Procurement/QuotationEntryPendingSubgrant?id=" + RequestId;
        btn.click();
    }
}
function SaveReject() {

    if (checkValidationOnSubmit('Reject') == true) {

        var RequestApprovalArray = [];
        var obj =
        {
            Id: 0,
            UserId: loggedinUserid,
            ProcureId: reId,
            Reason: $("#txtRejectComment").val(),
            Status: 4
        }
        RequestApprovalArray.push(obj);
        CommonAjaxMethod(virtualPath + 'ProcurementRequest/ApproveRejectProcurementRegistration', RequestApprovalArray, 'POST', function (response) {

        });
    }

}


var BankDetailsArray = [];
var BankDetailsArrayId = 0;


var MSMEArray = [];
var MSMEArrayId = 0;


function AddNewRows()
{
    if(checkValidationOnSubmit('type1') == true)
    {
        btnrowactivity1();
    }
   
  
}
var rowId = 0
function btnrowactivity1() {
    rowId = rowId + 1;

    $('.applyselect').select2("destroy");
    var $tableBody = $('#datatableQoutationEntry').find("tbody"),
        $trLast = $tableBody.find("tr:last"),
        $trNew = $trLast.clone();

    $trNew.find("label.lblRating").each(function (i) {
        $(this).attr({
            'id': "lblRating_" + (rowId)
        });
        $(this).text('')
    });
    $trNew.find("label.lblSRNo").each(function (i) {
        $(this).attr({
            'id': "lblSRNo_" + (rowId)
            
        });
        $(this).text(rowId + 1)
    });

    

    $trNew.find("div.singleDiv").each(function (i) {
        $(this).attr({
            'id': "singleDiv_" + (rowId)

        });
        
    });
    $trNew.find("div.multidiv").each(function (i) {
        $(this).attr({
            'id': "multidiv_" + (rowId)

        });

    });
    

    $trNew.find("label.Fileurl").each(function (i) {
        $(this).attr({
            'id': "hdnUploadFileUrl_" + (rowId)
        });

    });
    $trNew.find("div.uniticon").each(function (i) {
        $(this).attr({
            'id': "uniticon_" + (rowId)
        });
        
        
    });
    $trNew.find("div.rcmicon").each(function (i) {
        $(this).attr({
            'id': "rcmicon_" + (rowId)
        });
       

    });

    
    $trNew.find("label.FileActualName").each(function (i) {
        $(this).attr({
            'id': "hdnUploadActualFileName_" + (rowId),
            'text':''
        });

    });

    $trNew.find("label.FileNewName").each(function (i) {
        $(this).attr({
            'id': "hdnUploadNewFileName_" + (rowId),
            'text': ''
        });

    });



    $trNew.find("label.lblEmp").each(function (i) {
        $(this).attr({
            'id': "lblEmp_" + (rowId),
            'text': ''
        });
        $(this).text('')
    });

    $trNew.find("label.lblrecomandation").each(function (i) {
        $(this).attr({
            'for': "recomandation_" + (rowId)
        });

    });
    $trNew.find("input.recomandation").each(function (i)
    {
        $(this).attr({

            'id': "recomandation_" + (rowId),
            "checked":false
        });
        
        
    });
    $trNew.find("input.txtUnit").each(function (i) {
        $(this).attr({

            'id': "txtUnit_" + (rowId)
            
        });
        $(this).val('0')
    });

    $trNew.find("input.txtUnitRate").each(function (i) {
        $(this).attr({

            'id': "txtUnitRate_" + (rowId)
            
        });
        $(this).val('0')
    });

    $trNew.find("input.txtAmount").each(function (i) {
        $(this).attr({

            'id': "txtAmount_" + (rowId)
            
        });
        $(this).val('0')
    });
    $trNew.find("input.txtGST").each(function (i) {
        $(this).attr({

            'id': "txtGST_" + (rowId)
            
        });
        $(this).val('0')
    });
    $trNew.find("input.txtTaxPer").each(function (i) {
        $(this).attr({

            'id': "txtTaxPer_" + (rowId)
           
        });
        $(this).val('0')
    });
    $trNew.find("input.txtFreight").each(function (i) {
        $(this).attr({

            'id': "txtFreight_" + (rowId)
            
        });
        $(this).val('0')
    });

    $trNew.find("input.txtTotal").each(function (i) {
        $(this).attr({

            'id': "txtTotal_" + (rowId)
             
        });
        $(this).val('0')
    });

    $trNew.find("input.Attach").each(function (i) {
        $(this).attr({

            'id': "Attach_" + (rowId)
        });
        $(this).val('')
    });


    $trNew.find("select.ddlVendor").each(function (i) {
        $(this).attr({
            'id': "ddlVendor_" + (rowId),
            'name': "ddlVendor_" + (rowId)
             
        });
        $(this).val('Select').trigger('change');

    });
    //$trNew.find("select.type").each(function (i) {
    //    $(this).attr({
    //        'id': "type_" + (rowId),
    //        'name': "type_" + (rowId)
            
    //    });
        
    //    $(this).val('Select').trigger('change');
    //});
    $trNew.find("textarea").each(function (i) {
        $(this).attr({
            'id': "txtRemarks_" + (rowId),
            'name': "txtRemarks_" + (rowId)
            

        });
        $(this).val('')
    });


    $trLast.after($trNew);

    $('#tableToModify tr').each(function (i) {

        if (i == 0) {
            var len = this.cells.length;
            var dd = this.cells[len - 1];
            dd.style.visibility = 'hidden';
        }
        else {
            var len = this.cells.length;
            var dd = this.cells[len - 1];
            dd.style.visibility = 'visible';

        }
    });

    $(".applyselect").select2();



}
$(document).on('click', '.remove', function () {
    var delRow = 0;
    $(this).closest('tr').remove();
    $('#tableToModify tr').each(function (i) {
        if (i == 0) {
            var len = this.cells.length;
            var dd = this.cells[len - 1];
            dd.style.visibility = 'hidden';
        }
        else {
            var len = this.cells.length;
            var dd = this.cells[len - 1];
            dd.style.visibility = 'visible';
        }

       

    });


    $('#tableToModify tr').each(function (j) {
        $(this).find("label.lblRating").each(function (i) {
            $(this).attr({
                'id': "lblRating_" + delRow
            });

        });
        $(this).find("label.lblSRNo").each(function (i) {
            $(this).attr({
                'id': "lblSRNo_" + delRow,
                'text': delRow + 1
            });

        });

         
        $(this).find("label.singleDiv").each(function (i) {
            $(this).attr({
                'id': "singleDiv_" + delRow 
            });

        });

        $(this).find("div.multidiv").each(function (i) {
            $(this).attr({
                'id': "multidiv_" + delRow
            });

        });

         

        $(this).find("label.Fileurl").each(function (i) {
            $(this).attr({
                'id': "hdnUploadFileUrl_" + delRow
            });

        });
        
        $(this).find("div.uniticon").each(function (i) {
            $(this).attr({
                'id': "uniticon_" + delRow
            });

        });
        $(this).find("div.rcmicon").each(function (i) {
            $(this).attr({
                'id': "rcmicon_" + delRow
            });

        });

        $(this).find("label.FileActualName").each(function (i) {
            $(this).attr({
                'id': "hdnUploadActualFileName_" + delRow
            });

        });

        $(this).find("label.FileNewName").each(function (i) {
            $(this).attr({
                'id': "hdnUploadNewFileName_" + delRow
            });

        });

        $(this).find("label.lblEmp").each(function (i) {
            $(this).attr({
                'id': "lblEmp_" + delRow
            });

        });

        $(this).find("label.lblrecomandation").each(function (i) {
            $(this).attr({               
                'for': "recomandation_" + delRow
            });

        });

        $(this).find("input.recomandation").each(function (i) {
            $(this).attr({
                'id': "recomandation_" + delRow
                
            });

        });
        $(this).find("input.txtUnit").each(function (i) {
            $(this).attr({
                'id': "txtUnit_" + delRow
            });

        });

        $(this).find("input.txtUnitRate").each(function (i) {
            $(this).attr({
                'id': "txtUnitRate_" + delRow
            });

        });

        $(this).find("input.txtAmount").each(function (i) {
            $(this).attr({
                'id': "txtAmount_" + delRow
            });

        });

        $(this).find("input.txtGST").each(function (i) {
            $(this).attr({
                'id': "txtGST_" + delRow
            });

        });
        $(this).find("input.txtTaxPer").each(function (i) {
            $(this).attr({
                'id': "txtTaxPer_" + delRow
            });

        });

        
        $(this).find("input.txtFreight").each(function (i) {
            $(this).attr({
                'id': "txtFreight_" + delRow
            });

        });
        $(this).find("input.txtTotal").each(function (i) {
            $(this).attr({
                'id': "txtTotal_" + delRow
            });

        });

        $(this).find("input.Attach").each(function (i) {
            $(this).attr({
                'id': "Attach_" + delRow
            });

        });

        $(this).find("select.ddlVendor").each(function (i) {
            $(this).attr({
                'id': "ddlVendor_" + delRow
            });

        });


        //$(this).find("select.type").each(function (i) {
        //    $(this).attr({
        //        'id': "type_" + delRow
        //    });

        //});

        $(this).find("textarea").each(function (i) {
            $(this).attr({
                'id': "txtRemarks_" + delRow
            });

        });

        delRow = delRow + 1;
    });

});
function FillVendorDetails(ctrl) {
    var id = ctrl.id.split('_');
    var controlNo = id[1];
    var prjId = $('#' + 'ddlVendor_' + controlNo).val();
    if (prjId == 'Select' || prjId == '') {
        prjId = '0';
    }
    CommonAjaxMethod(virtualPath + 'ProcureVendorRegis/GetProcureVendorRegis', { id: prjId }
        , 'GET', function (response) {
            var d = response.data.data.Table;
            var isImp = false;
            if (d.length > 0) {
                isImp = d[0].IsEmpaneled;
            }
             
            $("#lblRating_" + + controlNo).text(d[0].Rating);
        if (isImp == true)
            $('#' + 'lblEmp_' + controlNo).text('Yes');
        else
            $('#' + 'lblEmp_' + controlNo).text('No');
    });
}
function FillVendor() {


    CommonAjaxMethod(virtualPath + 'ProcurementRequest/BindProjectDetails', { id: RequestId, IsBindLine: 13 }
        , 'GET', function (response) {

            var data1 = response.data.data.Table;
            var delRow = 0;
            $("#tblVendor").find("tr:gt(1)").remove();
            for (var i = 0; i < data1.length; i++) {
                var newtbblData = "<tr><td>" + data1[i].VendorName + "</td>" +
                    "<td><a class='HideClass' title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='deleteVendor(this," + data1[i].id + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr > ";
                $("#tblVendor").find('tbody').append(newtbblData);

            }
            $('#tableToModify tr').each(function (j) {
                $(this).find("select.ddlVendor").each(function (i) {
                    LoadMasterDropdown('ddlVendor_' + delRow,
                        {
                            ParentId: RequestId,
                            masterTableType: 0,
                            isMasterTableType: false,
                            isManualTable: true,
                            manualTable: 13,
                            manualTableId: 0
                        }, 'Select', false);

                });

            });

        });

}
function deleteVendor(ctrl, id) {
    ConfirmMsgBox("Are you sure want to delete", '', function () {
        var registrationObject =
        {
            VendorName: '',
            procureId: 0,
            vendorId: id,
            isdeleted: true,

        }

        CommonAjaxMethod(virtualPath + 'ProcureVendorRegis/SaveTempProcureVendorRegis', registrationObject, 'GET', function (response) {
            FillVendor();

        });
    });
}
function show2() {
    document.getElementById('div1').style.display = 'none';
    CommonAjaxMethod(virtualPath + 'ProcureVendorRegis/SaveTempProcureVendorRegis', {
        VendorName: '',
        procureId: RequestId,
        vendorId: 0,
        isdeleted: true
    }
        , 'GET', function (response) {
        FillVendor();
    });

}
function AddVendor() {



    CommonAjaxMethod(virtualPath + 'ProcureVendorRegis/SaveTempProcureVendorRegis', {
        VendorName: $('#txtTempVendor').val(),
        procureId: RequestId,
        vendorId: 0,
        isdeleted: false
    }, 'GET', function (response) {
        FillVendor();
        $('#txtTempVendor').val('');
    });
}
var BankArray = [];
var BankArrayId = 0;
var DocArray = [];
var DocArrayId = 0;
var MultiUnitArray = [];
var MultiUnitArrayId = 0;

var UnitArray = [];
var UnitArrayId = 0;

function deleteDocArrayRows(obj, id) {
    ConfirmMsgBox("Are you sure want to delete", '', function () {
        var data = DocArray.filter(function (itemParent) { return (itemParent.Id == id); });
        var url = data[0].NatureofAttachmentUrl;

        CommonAjaxMethod(virtualPath + 'CommonMethod/DeleteFile', { FileUrl: url }, 'POST', function (response) {
            $(obj).closest('tr').remove();
            DocArray = DocArray.filter(function (itemParent) { return (itemParent.Id != id); });
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
            url: virtualPath + 'CommonMethod/UploadOtherDocument',
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
function UploadOtherDocumentExtra() {


    var DocOtherNature = document.getElementById("txtNatureRemark").value;

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
                        NatureofAttachmentActualName: result.FileModel.ActualFileName,
                        NatureofAttachmentNewName: result.FileModel.NewFileName,
                        NatureofAttachmentUrl: result.FileModel.FileUrl,
                        Remarks: DocOtherNature

                    }

                    DocArray.push(objarrayinner);

                    var newtbblData = "<tr>><td>" + objarrayinner.NatureofAttachmentActualName + "</td>><td>" + objarrayinner.Remarks + "</td>" +
                        "<td><a class='HideClass'  title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='deleteDocArrayRows(this," + objarrayinner.Id + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr > ";

                    $("#tblDocumentList").find('tbody').append(newtbblData);

                    $("#fileDocOtherUpload").val('');
                    $("#fileDocOtherUpload").val(null);
                    $("#txtNatureRemark").val('');


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

function DeletePayment(obj, id) {

    ConfirmMsgBox("Are you sure want to delete", '', function () {

        $(obj).closest('tr').remove();
        BankArray = BankArray.filter(function (itemParent) { return (itemParent.Id != id); });

    })
}
function DeleteUnitRate(obj, id) {

    ConfirmMsgBox("Are you sure want to delete", '', function () {

        $(obj).closest('tr').remove();
        MultiUnitArray = MultiUnitArray.filter(function (itemParent) { return (itemParent.Id != id); });
        var totalUnitRate = 0;
        var totaltax = 0;
        var ctrlId = $("#hdnMultiValue").val();

        var textId = $("#lblSRNo_" + ctrlId).text();
        var nArray = MultiUnitArray.filter(function (itemParent) { return (itemParent.QuoteEntryId == textId); });

        for (var i = 0; i < nArray.length; i++) {
            totalUnitRate += parseInt(nArray[i].UnitRate);
            totaltax += parseInt(nArray[i].Tax_GST);
        }
        var ctrlId = $("#hdnMultiValue").val();
        if (ctrlId != '') {
            $("#txtUnitRate_" + ctrlId).val(totalUnitRate);
            $("#txtGST_" + ctrlId).val(totaltax);
            $("#txtTotal_" + ctrlId).val( NumberWithComma(totalUnitRate + totaltax));

        }
    })
}

function DeleteUnitRateMulti(obj, id) {

    ConfirmMsgBox("Are you sure want to delete", '', function () {

        $(obj).closest('tr').remove();
        UnitArray = UnitArray.filter(function (itemParent) { return (itemParent.Id != id); });

        var ctrlId = $("#hdnMultiValue").val();

        var textId = $("#lblSRNo_" + ctrlId).text();

        var totalUnitRate = 0;
        var totalUnit = 0;
        var totalFreight = 0;
        var totalGST = 0;
        var txtTotal = 0;

        var nArray = UnitArray.filter(function (itemParent) { return (itemParent.Id == textId); });
        for (var j = 0; j < nArray.length; j++) {

            totalUnitRate = totalUnitRate + parseInt(nArray[j].UnitRate, 10);
            totalGST = totalGST + parseInt(nArray[j].Tax_GST, 10);
            totalUnit = totalUnit + parseInt(nArray[j].Unit, 10);
            totalFreight = totalFreight + parseInt(nArray[j].Frieght, 10);
            txtTotal = txtTotal + parseInt(nArray[j].Amount, 10);
        }


        if (ctrlId != '') {
            fromMulitUnit = 1;
            $("#txtUnitRate_" + ctrlId).val(totalUnitRate);
            $("#txtUnit_" + ctrlId).val(totalUnit);
            $("#txtAmount_" + ctrlId).val(txtTotal);
            $("#txtGST_" + ctrlId).val(totalGST);
            $("#txtFreight_" + ctrlId).val(totalFreight);
            $("#txtTotal_" + ctrlId).val(NumberWithComma(txtTotal + totalGST + totalFreight));


        }

    })
}
function AddUnit() {
    if (checkValidationOnSubmit('MUnit') == true) {

        UnitArrayId = UnitArrayId + 1;
        var loop = UnitArrayId;
        var objarrayinner =
        {
            Id: loop,
            QuoteEntryId: $("#lblSRNo_" + $("#hdnMultiValue").val()).text(),
            Description: $("#txtTaxDescription").val(),
            Unit: $("#txtUnitmulti").val(),
            UnitRate: $("#txtUnitmultiRate").val(),
            Amount: $("#txtUnitAmount").val(),
            Tax: $("#txtUnitTaxPer").val(),
            Tax_GST: $("#txtUnitTaxCalcu").val(),
            Frieght: $("#txtUnitFrieght").val()


        }

        UnitArray.push(objarrayinner);
        var newtbblData = "<tr><td>" + $("#txtTaxDescription").val() + "</td><td>" + $("#txtUnitmulti").val() + "</td><td>" + $("#txtUnitmultiRate").val() + "</td><td>" + $("#txtUnitAmount").val() + "</td><td>" + $("#txtUnitTaxPer").val() + "</td><td>" + $("#txtUnitTaxCalcu").val() + "</td><td>" + $("#txtUnitFrieght").val() + "</td><td><a class='HideClass' title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='DeleteUnitRateMulti(this," + objarrayinner.Id + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr>";

        $("#datatableUnit").find('tbody').append(newtbblData);
        var ctrlId = $("#hdnMultiValue").val();
        var textId = $("#lblSRNo_" + ctrlId).text();

        var totalUnitRate = 0;
        var totalUnit = 0;
        var totalFreight = 0;
        var totalGST = 0;
        var txtTotal = 0;

        var nArray = UnitArray.filter(function (itemParent) { return (itemParent.QuoteEntryId == textId); });
        for (var j = 0; j < nArray.length; j++) {

            totalUnitRate = totalUnitRate + parseInt(nArray[j].UnitRate, 10);
            totalGST = totalGST + parseInt(nArray[j].Tax_GST, 10);
            totalUnit = totalUnit + parseInt(nArray[j].Unit, 10);
            totalFreight = totalFreight + parseInt(nArray[j].Frieght, 10);
            txtTotal = txtTotal + parseInt(nArray[j].Amount, 10);
        }


        if (ctrlId != '') {
            fromMulitUnit = 1;
            $("#txtUnitRate_" + ctrlId).val(totalUnitRate);
            $("#txtUnit_" + ctrlId).val(totalUnit);
            $("#txtAmount_" + ctrlId).val(txtTotal);
            $("#txtGST_" + ctrlId).val(totalGST);
            $("#txtFreight_" + ctrlId).val(totalFreight);
            $("#txtTotal_" + ctrlId).val(NumberWithComma(txtTotal + totalGST + totalFreight));


        }
        $("#txtTaxDescription").val('');
        $("#txtUnitmulti").val('');
        $("#txtUnitmultiRate").val('');
        $("#txtUnitAmount").val('');
        $("#txtUnitTaxPer").val('');
        $("#txtUnitTaxCalcu").val('');
        $("#txtUnitFrieght").val('');

    }
}
function AddMulitpleUnit() {

    if (checkValidationOnSubmit('MulitpleUnit') == true) {

        MultiUnitArrayId = MultiUnitArrayId + 1;
        var loop = MultiUnitArrayId;
        var objarrayinner =
        {
            Id: loop,
            QuoteEntryId: $("#lblSRNo_" + $("#hdnMultiValue").val()).text() ,
            Description: $("#txtDescription").val(),
            UnitRate: $("#txtUnitRate").val(),
            Tax: $("#txtTax").val(),
            Tax_GST: $("#txtTax_GST").val()


        }
       
        MultiUnitArray.push(objarrayinner);
        var newtbblData = "<tr><td>" + $("#txtDescription").val() + "</td><td>" + $("#txtUnitRate").val() + "</td><td>" + $("#txtTax").val() + "</td><td>" + $("#txtTax_GST").val() + "</td><td><a class='HideClass' title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='DeleteUnitRate(this," + objarrayinner.Id + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr>";

        $("#datatableMultiple").find('tbody').append(newtbblData);
        var ctrlId = $("#hdnMultiValue").val();
        var textId = $("#lblSRNo_" + ctrlId).text();
        var totalUnitRate = 0;
        var totalGST = 0;
        var nArray = MultiUnitArray.filter(function (itemParent) { return (itemParent.QuoteEntryId == textId); });
        for (var j = 0; j < nArray.length; j++) {

            totalUnitRate = totalUnitRate + parseInt(nArray[j].UnitRate, 10);
            totalGST = totalGST + parseInt(nArray[j].Tax_GST, 10);
        }

      
        if (ctrlId != '')
        {
            $("#txtUnitRate_" + ctrlId).val(totalUnitRate);
            $("#txtUnitRate_" + ctrlId).trigger("change");
            $("#txtGST_" + ctrlId).val(totalGST);
            $("#txtGST_" + ctrlId).trigger("change");
            

        }
        $("#txtDescription").val('');
        $("#txtUnitRate").val('');
        $("#txtTax").val('');
        $("#txtTax_GST").val('');

    }
}
function AddPaymentTerm() {

    if (checkValidationOnSubmit('PaymentTerm') == true) {

        BankArrayId = BankArrayId + 1;
        var loop = BankArrayId;
        var objarrayinner =
        {
            Id: loop,
            PaymentTerms: $("#txtPaymentTerm").val(),
            Amount: $("#txtPaymentTermAmount").val(),
            DueOn: ChangeDateFormat($("#txtPaymentTermDueOn").val()),
           PaymentId: 0,

        }
        BankArray.push(objarrayinner);
        var newtbblData = "<tr><td>" + $("#txtPaymentTerm").val() + "</td><td>" + $("#txtPaymentTermAmount").val() + "</td><td>" + $("#txtPaymentTermDueOn").val() + "</td><td><a class='HideClass'  title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='DeletePayment(this," + objarrayinner.Id + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr>";

        $("#tblPaymentTerm").find('tbody').append(newtbblData);


        $("#txtPaymentTerm").val('');
        $("#txtPaymentTermAmount").val('');
        $("#txtPaymentTermDueOn").val('');

    }

}
function FillTax()
{
    var txtGST_ = $('#txtUnitRate').val() == "" ? '0' : $('#txtUnitRate').val();
    var txtFreight_ = $('#txtTax').val() == "" ? '0' : $('#txtTax').val();

    var gstamt1 = parseInt(((txtGST_ * txtFreight_) / 100),10);
   // var total1 = (gstamt1 + parseInt(txtGST_));
    $('#txtTax_GST').val(Math.round(gstamt1));
   
}
function FillUnitTax() {



    var txtUnitmulti = $('#txtUnitmulti').val() == "" ? '0' : $('#txtUnitmulti').val();
    var txtUnitmultiRate = $('#txtUnitmultiRate').val() == "" ? '0' : $('#txtUnitmultiRate').val();
    var txtUnitTaxPer = $('#txtUnitTaxPer').val() == "" ? '0' : $('#txtUnitTaxPer').val();
    var txtUnitAmount = $('#txtUnitAmount').val() == "" ? '0' : $('#txtUnitAmount').val();
    var txtUnitTaxCalcu = $('#txtUnitTaxCalcu').val() == "" ? '0' : $('#txtUnitTaxCalcu').val();

    var tCal1 = parseInt(txtUnitmulti) * parseInt(txtUnitmultiRate);
    $('#txtUnitAmount').val(Math.round(tCal1));
    var gstamt1 = parseInt( ((tCal1 * parseInt(txtUnitTaxPer)) / 100),10);
    $('#txtUnitTaxCalcu').val(Math.round(gstamt1));

}
function FillTotal(ctrl, from)
{
    if (fromMulitUnit == 0)
    {
        var id = ctrl.id.split('_');
        var controlNo = id[1];
        var txtTaxPer_ = $('#' + 'txtTaxPer_' + controlNo).val() == "" ? '0' : $('#' + 'txtTaxPer_' + controlNo).val();
        var txtFreight_ = $('#' + 'txtFreight_' + controlNo).val() == "" ? '0' : $('#' + 'txtFreight_' + controlNo).val();


        var a = $("#ddlType").val();
        if (a == "rcmultiple") {
            var gstamt1 = $('#' + 'txtUnitRate_' + controlNo).val();
            var gst = $('#' + 'txtGST_' + controlNo).val();
            var total1 = (parseInt(gstamt1)) + parseInt(txtFreight_, 10) + (parseInt(gst));

            $('#' + 'txtTotal_' + controlNo).val(NumberWithComma(Math.round(total1)));
        }
        else if (a == "Lumpsum") {
            var gstamt1 = parseInt(($('#' + 'txtAmount_' + controlNo).val() * txtTaxPer_) / 100);
            var total1 = (gstamt1 + parseInt($('#' + 'txtAmount_' + controlNo).val())) + parseInt(txtFreight_, 10);
            $('#' + 'txtGST_' + controlNo).val(Math.round(gstamt1));

            $('#' + 'txtTotal_' + controlNo).val(NumberWithComma(Math.round(total1)));
        }
        else {

            var txtUnit_ = $('#' + 'txtUnit_' + controlNo).val() == "" ? '0' : $('#' + 'txtUnit_' + controlNo).val();
            var txtUnitRate_ = $('#' + 'txtUnitRate_' + controlNo).val() == "" ? '0' : $('#' + 'txtUnitRate_' + controlNo).val();
            // var txtAmount_ = $('#' + 'txtAmount_' + controlNo).val() == "" ? '0' : $('#' + 'txtAmount_' + controlNo).val();

            // var txtTotal_ = $('#' + 'txtTotal_' + controlNo).val() == "" ? '0' : $('#' + 'txtTotal_' + controlNo).val();

            var amt = txtUnit_ * txtUnitRate_;
            $('#' + 'txtAmount_' + controlNo).val(amt);

            var gstamt = parseInt((amt * txtTaxPer_) / 100);
            var total = (gstamt + amt) + parseInt(txtFreight_, 10);
            $('#' + 'txtGST_' + controlNo).val(Math.round(gstamt));
            $('#' + 'txtTotal_' + controlNo).val(NumberWithComma(Math.round(total)));
        }
    }

}


function SaveQuotationEntry(from) {
    var radios13 = document.getElementById('yes23');

    var lieitem = [];
    var isValid = 1;
    var isEmpnd = false;
    var isLineItemdocumentUpload = true;
    $('#tableToModify tr').each(function (i) {
        if (isEmpnd == false) {
            isEmpnd = $("#lblEmp_" + i).text().toLowerCase() == 'yes' ? true : false;

        }
        var procPRojectLine =
        {
            Id: $("#lblSRNo_" + i).text(),
            VendorId: $("#ddlVendor_" + i).val(),
            Rating: $("#lblRating_" + i).text(),
            Empanelled: $("#lblEmp_" + i).text().toLowerCase()=='yes'? true:false,
            UnitType: $("#lblUnitType_"+i).text(),
            Units: $("#txtUnit_" + i).val(),
            UnitRate: $("#txtUnitRate_" + i).val(),
            Amount: $("#txtAmount_" + i).val(),
            Tax: $("#txtTaxPer_" + i).val(),
            GSTEtc: $("#txtGST_" + i).val(),
            Freight_TPT: $("#txtFreight_" + i).val(),
            TotalValue: $("#txtTotal_" + i).val().replace(/,/g, ''),
            AttachQuotationActualName: $("#hdnUploadActualFileName_" + i).text(),
            AttachQuotationNewName: $("#hdnUploadNewFileName_" + i).text(),
            AttachQuotationUrl: $("#hdnUploadFileUrl_" + i).text(),
            IsRecommend: document.getElementById("recomandation_" + i).checked == true ? true : false,
            Remark: $("#txtRemarks_" + i).val(),

        }
        if (procPRojectLine.AttachQuotationActualName == "" && from == 2)
        {
            isLineItemdocumentUpload = false;
        }

        if (procPRojectLine.VendorId == 'Select' || procPRojectLine.UnitType == 'Select' || procPRojectLine.TotalValue == '0' || procPRojectLine.TotalValue == '') {
            isValid = 0;
        }
        lieitem.push(procPRojectLine);


    });
    var isValiddeliverables=true
    if (BankArray.length == 0)
    {
        isValiddeliverables = false;
        FailToaster("Please fill details of deliverables.");
    }
    
    if (isLineItemdocumentUpload == false && from==2)
    {
        FailToaster("Please attach Document in quoation entry details.");
    }
    var isValidfileScopeNewName = true
    if ($('#hdnfileScopeActualName').val() == '' && from==2)
    {
        isValidfileScopeNewName = false;
        FailToaster("Please attach Document for Scope.");
    }
    var isValidJust = true;
    if (lieitem.length < 3)
    {
        if ($('#txtJustfication').val() == '' && isEmpnd == false) {
            $('#divtxtJustfication').show();
            isValidJust = false;
            FailToaster("Attention!, Providing justification is mandatory if the quotes are less than 3.");
        }
    }
    var isValidIsRecommend = true;
    if (from == 2) {
        isValidIsRecommend = false
        for (var i = 0; i < lieitem.length; i++) {
            if (lieitem[i].IsRecommend == true) {
                isValidIsRecommend = true;
            }
        }
        if (isValidIsRecommend == false) {
            FailToaster("Please recommend any one..");
        }
    }
   
    if (isValidJust)
    {         
        var   checkDate3 = IsGreaterThanCurrentDate(ChangeDateFormat($('#tbEstimatedStartDate').val()), ChangeDateFormat($('#tbEstimatedEndDate').val()), 'Estimated start date should be always greater than Estimated End date.');


        if (checkValidationOnSubmit('Mandatory') == true && checkDate3 == true && isValiddeliverables == true && isValidfileScopeNewName == true && isLineItemdocumentUpload == true && isValidIsRecommend==true)
        {
            if (isValid > 0) {
                var objQuotation =
                {
                    Id: $("#hdnQuoteEntryId").val(),
                    Procure_Request_Id: RequestId,
                    EstimatedStartDate: ChangeDateFormat($('#tbEstimatedStartDate').val()),
                    EstimatedEndDate: ChangeDateFormat($('#tbEstimatedEndDate').val()),
                    ContractType: $('#ddlAgreementA').val(),
                    ScopeofworkNewFileName: $('#hdnfileScopeNewName').val(),
                    ScopeofworkActualFileName: $('#hdnfileScopeActualName').val(),
                    ScopeofworkActualFileUrl: $('#hdnfileScopeFileUrl').val(),
                    SpecialConditions: $('#txtSpecialCond').val(),
                    IsNonRegistratorVendor: radios13.checked == true ? true : false,
                    Justification: $('#txtJustfication').val(),
                    UnitType: $('#ddlType').val(),
                    Status: from,
                    ItemDescription: $('#txtItemDescription').val(),
                    QuotationEntryNatureofAttachmentList: DocArray,
                    QuotationEntryDetailList: lieitem,
                    QuotationEntryDetailsofDeliverableList: BankArray,
                    QuotationRateContractMultipleList: MultiUnitArray,
                    QuotationRateContractUnitMultipleList: UnitArray
                }
                CommonAjaxMethod(virtualPath + 'ProcurementRequest/SaveQuotationEntry', objQuotation, 'POST', function (response)
                {
                    if (response.ValidationInput == 1) {                        
                        Redirect();
                    }
                    
                });
            }
            else {
                FailToaster("Please select Quotation Entry line item!");
            }
        }
    }
 }

function Redirect() {
    
    var url = "/Procurement/ProcurementUserRequest";
    window.location.href = url;
}


function GetQuotationEntry()
{
    CommonAjaxMethod(virtualPath + 'ProcurementRequest/GetQuotationEntry', { Id: RequestId }
        , 'GET', function (response) {

        BankArray = [];
        BankArrayId = 0;
        DocArray = [];
        DocArrayId = 0;
        MultiUnitArray = [];
        MultiUnitArrayId = 0;

        UnitArray = [];
        UnitArrayId = 0;

        var data1 = response.data.data.Table; //Procure_QuotationEntry
        var data2 = response.data.data.Table1;//Procure_QuotationEntryDetail
        var data3 = response.data.data.Table2;//Procure_QuotationEntryDetailsofDeliverable
        var data4 = response.data.data.Table3;//Procure_QuotationEntryNatureofAttachment
        var data5 = response.data.data.Table4;//Procure_QuotationRateContractMultiple

        var data6 = response.data.data.Table5;//Procure_QuotationRateContractMultipleUnit
        var data7 = response.data.data.Table6;

        var data8 = response.data.data.Table7;
        var data9 = response.data.data.Table8;
        var data10 = response.data.data.Table9;

            var data11 = response.data.data.Table11;
            $('#tblEDwaiver').DataTable({
                "processing": true, // for show progress bar           
                "destroy": true,
                "data": data11,
                "paging": false,
                "info": false,


                "columns": [


                    { "data": "Justification" },

                    {
                        "orderable": false,
                        data: null, render: function (data, type, row) {

                            return '<label class="text-right">' + ChangeDateFormatToddMMYYY(row.EdApprovedDate) + '</label>';
                        }
                    },
                    { "data": "Status" },
                    { "data": "EdRemark" }

                ]
            });
        if (data9.length > 0) {
            $(".UploadSignedTab").hide();
            if (data9[0].Status == "20") {
                $("#divForwardToAuthorized").show();
                $(".UploadSignedTab").hide();

            }
            else {
                $("#divForwardToAuthorized").hide();


            }
            if (data9[0].Status == "25") {
                $(".UploadSignedTab").show();
            }
            if (data9[0].Status == "26") {
                $(".UploadSignedTab").hide();
            }
            if (data9[0].Status == "27" || data9[0].Status == "28") {
                $(".UploadSignedTab").show();
            }
        }

        if (data10.length > 0) {
            if (data10[0].Status == "2") {
                $(".UploadSigned").hide();
            }

            BindContract(data10);
        }

        if (data9.length > 0) {
            if (data9[0].Status == "24") {
                $("#AuthApproval").show();
            }
            else {
                $("#AuthApproval").hide();
            }
        }
        if ($("#hdnIsModuleAdmin").val() == "1") {
            if (data8.length > 0)
                $("#AuthorisedSignatory").text(data8[0].AuthName);
        }

        var isProcess = 0;
        if (data1.length > 0) {

            if (data1[0].Status == 2 || data1[0].Status == 3) {
                isProcess = 1;

                $("#divMoreInfo").show();
            }
            $('#txtQuotationNo').val(data1[0].QuotationNo);
            $('#txtShiping').val(data1[0].Shiping);
            $('#txtDeliveryAddress').val(data1[0].DeliveryAddress);
            $('#txtAmendmentReason').val(data1[0].AmendmentReason);
            
            $('#txtQuotationDate').val(ChangeDateFormatToddMMYYY(data1[0].QuotationDate));
            $('#txtQuotationAgreementDate').val(ChangeDateFormatToddMMYYY(data1[0].QuotationAgreementDate));
            $('#txtQuotationSignDate').val(ChangeDateFormatToddMMYYY(data1[0].QuotationSignDate));

            $("#hdnQuoteEntryId").val(data1[0].Id);
            $('#tbEstimatedStartDate').val(ChangeDateFormatToddMMYYY(data1[0].EstimatedStartDate));
            $('#tbEstimatedEndDate').val(ChangeDateFormatToddMMYYY(data1[0].EstimatedEndDate));
            $("#ddlAgreementA").val('Purchase Order (PO)').trigger('change');
            $("#hdnfileScopeNewName").val(data1[0].ScopeofworkNewFileName);
            $("#hdnfileScopeActualName").val(data1[0].ScopeofworkActualFileName);
            $("#lblAttachementRFPDownload").text(data1[0].ScopeofworkActualFileName);
            $("#hdnfileScopeFileUrl").val(data1[0].ScopeofworkActualFileUrl);
            $("#txtSpecialCond").val(data1[0].SpecialConditions);
            $("#ddlType").val(data1[0].UnitType).trigger('change');
            $('#txtItemDescription').val(data1[0].ItemDescription);

            if (data1[0].IsNonRegistratorVendor == true) {
                document.getElementById('yes23').checked = true;
                $('#yes23').click();
            }
            else {
                document.getElementById('no23').checked = true;
                $('#no23').click();
            }
            $("#txtJustfication").val(data1[0].Justification);

            if (data1[0].Justification != '') {
                $('#divtxtJustfication').show();
            }
        }

        for (var j = 0; j < data2.length - 1; j++) {
            btnrowactivity1();
        }
        for (var i = 0; i < data5.length; i++) {
            MultiUnitArrayId = i + 1;

            var objarrayinner1 =
            {
                Id: MultiUnitArrayId,
                QuoteEntryId: data5[i].QuoteEntryId,
                Description: data5[i].Description,
                UnitRate: data5[i].UnitRate,
                Tax: data5[i].Tax,
                Tax_GST: data5[i].Tax_GST

            }

            MultiUnitArray.push(objarrayinner1);
        }

        for (var i = 0; i < data6.length; i++) {
            UnitArrayId = i + 1;

            var objarrayinner2 =
            {

                Id: UnitArrayId,
                QuoteEntryId: data6[i].QuoteEntryId,
                Description: data6[i].Description,
                Unit: data6[i].Unit,
                UnitRate: data6[i].UnitRate,
                Amount: data6[i].Amount,
                Tax: data6[i].Tax,
                Tax_GST: data6[i].Tax_GST,
                Frieght: data6[i].Frieght

            }

            UnitArray.push(objarrayinner2);
        }


        for (var h = 0; h < data2.length; h++) {
            var i = h;


            $("#lblSRNo_" + i).text(data2[h].Id);
            $('#' + 'ddlVendor_' + i).val(data2[h].VendorId).trigger('change');
            $("#lblRating_" + i).text(data2[h].Rating);
            $("#lblEmp_" + i).text(data2[h].Empanelled);
            setOnchange = 1;
            setOnchangeUnit = 1;

            var mArray = MultiUnitArray.filter(function (itemParent) { return (itemParent.QuoteEntryId == data2[h].Id); });
            if (mArray.length > 0) {
                $("#txtUnit_" + i).prop('disabled', true);
            }
            var uArray = UnitArray.filter(function (itemParent) { return (itemParent.QuoteEntryId == data2[h].Id); });

            if (uArray.length > 0) {
                $("#txtTaxPer_" + i).prop('disabled', true);
                $("#txtUnit_" + i).prop('disabled', true);
                $("#txtUnitRate_" + i).prop('disabled', true);
                $("#txtAmount_" + i).prop('disabled', true);
                $("#txtGST_" + i).prop('disabled', true);
                $("#txtFreight_" + i).prop('disabled', true);
                $("#txtTotal_" + i).prop('disabled', true);
            }
            $("#txtUnit_" + i).val(data2[h].Units);
            $("#txtUnitRate_" + i).val(data2[h].UnitRate);
            $("#txtAmount_" + i).val(data2[h].Amount);
            $("#txtTaxPer_" + i).val(data2[h].Tax);

            $("#txtGST_" + i).val(data2[h].GSTEtc);
            $("#txtFreight_" + i).val(data2[h].Freight_TPT);
            $("#txtTotal_" + i).val(NumberWithComma(data2[h].TotalValue));
            $("#hdnUploadActualFileName_" + i).text(data2[h].AttachQuotationActualName);
            $("#hdnUploadNewFileName_" + i).text(data2[h].AttachQuotationNewName);
            $("#hdnUploadFileUrl_" + i).text(data2[h].AttachQuotationUrl);
            if (data2[h].IsRecommend == true) {
                document.getElementById('recomandation_' + i).checked = true;
                $('#' + 'recomandation_' + i).click();

            }
            $("#txtRemarks_" + i).val(data2[h].Remark);


        }

        for (var i = 0; i < data3.length; i++) {
            BankArrayId = i + 1;

            var objarrayinner =
            {
                Id: BankArrayId,
                PaymentTerms: data3[i].PaymentTerms,
                Amount: data3[i].Amount,
                DueOn: data3[i].DueOn,
                PaymentId: data3[i].Id,

            }
            BankArray.push(objarrayinner);
        }


        for (var i = 0; i < data4.length; i++) {
            DocArrayId = i + 1;

            var objarrayinner =
            {
                Id: DocArrayId,
                NatureofAttachmentActualName: data4[i].NatureofAttachmentActualName,
                NatureofAttachmentNewName: data4[i].NatureofAttachmentNewName,
                NatureofAttachmentUrl: data4[i].NatureofAttachmentUrl,
                Remarks: data4[i].Remarks

            }

            DocArray.push(objarrayinner);

        }
        BindDocArray(DocArray);
        if (isProcess > 0) {
            $("#divAction").hide();
            $(".remove").hide();
            $(".HideClass").hide();
            $(".Attach").hide();
            $(".FileActualName").show();
            $("#divFileScope2").show();
            $("#divFileScope1").hide();
            $("#ddlAgreementA").prop("disabled", true);
        }

        var isCommitee = $("#hdnIsProcureCommitee").val();
        if (isCommitee == '1') {
            $('#tblComitteeMember').DataTable({
                "processing": true, // for show progress bar           
                "destroy": true,
                "data": data7,
                "paging": false,
                "info": false,


                "columns": [



                    { "data": "RowNum" },
                    { "data": "ComitteeMember" },
                    { "data": "Designation" },
                    { "data": "Department" },
                    { "data": "Location" },
                    { "data": "Remark" },
                    {
                        "orderable": false,
                        data: null, render: function (data, type, row) {
                            if (row.VisibleData == 'Close') {

                                $("#divCommiteeApproval1").hide();
                                $("#divCommiteeApproval2").hide();

                            }
                            return '<label>' + row.Status + '</label>';
                        }
                    }

                ]
            });
        }
            if (data9[0].Status == "25" || data9[0].Status == "27") {

            $("#divFileScope2").show();
            $("#divFileScope1").show();
            BindBankArray(BankArray, 1);
            $("#btnAddPaymentTerm").show();
               
        }
        else {

            $("#divFileScope2").show();
            $("#divFileScope1").hide();
            BindBankArray(BankArray, 2);

            $("#btnAddPaymentTerm").hide();
          
           // $("#btnDownload").show();

        }
            if (data9[0].Status == 26) {
                $("#divAuthRejectReason").show();
                $("#txtAuthRejectedReason").val(data9[0].Remark);
            }

            if (data1[0].IsModuleAdminSubmit == 'Yes')
            {
               // $("#btnDownload").show();
                $("#btnSaveContract1").hide();
                $("#btnSaveContract2").hide();
            }
            else {
               // $("#btnDownload").hide();              
                $("#btnSaveContract1").show();
                $("#btnSaveContract2").show();
            }
            
            FillInvoiceDetail(data3);
          
    });
}
 
function BindBankArray(array, from) {
    for (var i = 0; i < array.length; i++) {

        var newtbblData = "";
        if (from == 2) {
            newtbblData = "<tr><td>" + array[i].PaymentTerms + "</td><td>" + NumberWithComma(array[i].Amount) + "</td><td>" + ChangeDateFormatToddMMYYY(array[i].DueOn) + "</td><td><a class='HideClass' title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='DeletePayment(this," + array[i].Id + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr>";
        }
        else {
            newtbblData = "<tr><td>" + array[i].PaymentTerms + "</td><td>" + NumberWithComma(array[i].Amount) + "</td><td>" + ChangeDateFormatToddMMYYY(array[i].DueOn) + "</td><td><a   title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='DeletePayment(this," + array[i].Id + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr>";

        }
        $("#tblPaymentTerm").find('tbody').append(newtbblData);
    }
}
function DownloadAttachFile(id) {
    var fileURl = $('#hdnAttachUrl_' + id).val();
    var fileName = $('#hdnAttachActulName_' + id).val();
    if (fileURl != null || fileURl != undefined) {
        var stSplitFileName = fileName.split(".");
        var link = document.createElement("a");
        link.download = stSplitFileName[0];
        link.href = fileURl;
        link.click();
    }
}
function BindDocArray(objarrayinner) {


    for (var i = 0; i < objarrayinner.length; i++) {
        var strReturn = '<tr><td><a href="#" onclick="DownloadAttachFile(' + objarrayinner[i].Id + ')" ><i class="fas fa-edit"></i>' + objarrayinner[i].NatureofAttachmentActualName + '</a><input value="' + objarrayinner[i].NatureofAttachmentActualName + '" type="hidden" id="hdnAttachActulName_' + objarrayinner[i].Id + '" /><input value="' + objarrayinner[i].NatureofAttachmentUrl + '" type="hidden" id="hdnAttachUrl_' + objarrayinner[i].Id + '" /></td>';

        var newtbblData = strReturn + "<td>" + objarrayinner[i].Remarks + "</td>" +
            "<td><a class='HideClass' title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='deleteDocArrayRows(this," + objarrayinner[i].Id + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr > ";

        $("#tblDocumentList").find('tbody').append(newtbblData);
    }
}

function SaveRejectForCommitee() {



    if (checkValidationOnSubmit('RejectCommitee') == true) {
        ConfirmMsgBox("Are you sure want to Reject", '', function () {
            var RequestApprovalArray = [];
            var obj =
            {
                Id: 0,
                UserId: loggedinUserid,
                ProcureId: RequestId,
                Reason: $("#txtRejectCommitee").val(),
                Status: 13,
                IsApproved: 19
            }
            RequestApprovalArray.push(obj);
            CommonAjaxMethod(virtualPath + 'ProcurementRequest/ApproveRejectProcurementRegistration', RequestApprovalArray, 'POST', function (response) {
                RedirectForCommitee();
            });

        })
    }

}
function SaveApproveForCommitee() {
    ConfirmMsgBox("Pls Reconfirm", '', function () {
        var RequestApprovalArray = [];
        var obj =
        {
            Id: 0,
            UserId: loggedinUserid,
            ProcureId: RequestId,
            Reason: '',
            Status: 11,
            IsApproved: 18
        }
        RequestApprovalArray.push(obj);
        CommonAjaxMethod(virtualPath + 'ProcurementRequest/ApproveRejectProcurementRegistration', RequestApprovalArray, 'POST', function (response) {
            RedirectForCommitee();
        });

    })



}
function RedirectForCommitee() {

    var url = "/Procurement/ProcurementUserRequest";
    window.location.href = url;
}


function DeleteUploadSigned(obj, id) {
    ConfirmMsgBox("Are you sure want to delete", '', function () {
        var data = UploadSignedDocuments.filter(function (itemParent) { return (itemParent.Id == id); });
        var url = data[0].UploadSignedUrl;

        CommonAjaxMethod(virtualPath + 'CommonMethod/DeleteFile', { FileUrl: url }, 'POST', function (response) {
            $(obj).closest('tr').remove();
            UploadSignedDocuments = UploadSignedDocuments.filter(function (itemParent) { return (itemParent.Id != id); });
        });

    })

}
var UploadSignedDocuments = [];
var UploadSignedId = 0;
function UploadSignedDocument() {
 
    var DocOtherNature = document.getElementById("txtUploadSignedDocuments").value;
    var fileUpload = $("#fileUploadSignedDocuments").get(0);
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

                    UploadSignedId = UploadSignedId + 1;
                    var loop = UploadSignedId;


                    var objarrayinner =
                    {
                        Id: loop,
                        UploadSignedActualName: result.FileModel.ActualFileName,
                        UploadSignedNewName: result.FileModel.NewFileName,
                        UploadSignedUrl: result.FileModel.FileUrl,
                        Remarks: DocOtherNature

                    }

                    UploadSignedDocuments.push(objarrayinner);

               
                    var strReturn = '<tr><td><a href="#" onclick="DownloadContractAttachFile(' + objarrayinner.Id + ')" ><i class="fas fa-edit"></i>' + objarrayinner.UploadSignedActualName + '</a><input value="' + objarrayinner.UploadSignedActualName + '" type="hidden" id="hdnContractAttachActulName_' + objarrayinner.Id + '" /><input value="' + objarrayinner.UploadSignedUrl + '" type="hidden" id="hdnContractAttachUrl_' + objarrayinner.Id + '" /></td>';

                    var newtbblData = strReturn + "<td>" + objarrayinner.Remarks + "</td>" +
                        "<td><a   title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='DeleteUploadSigned(this," + objarrayinner.Id + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr > ";

                    $("#tblUploadSignedDocuments").find('tbody').append(newtbblData);

                   

                    $("#fileUploadSignedDocuments").val('');
                    $("#fileUploadSignedDocuments").val(null);
                    $("#txtUploadSignedDocuments").val('');


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
 
function SaveContract(from) {
    var isRequired = true;

    var isSaved = false;

    var isValid = true;
    if (from == 2) {
        if (UploadSignedDocuments.length == 0) {
            isValid = false;
        }
        isRequired = checkValidationOnSubmit('UploadDocSign');
    }
    if (isRequired == true) {
        if (isValid == true) {

            var checkDate3 = IsGreaterThanCurrentDate(ChangeDateFormat($('#tbEstimatedStartDate').val()), ChangeDateFormat($('#tbEstimatedEndDate').val()), 'Estimated start date should be always greater than Estimated End date.');


            if (checkDate3 == true) {

                var objQuotation =
                {
                    Id: $("#hdnQuoteEntryId").val(),
                    Procure_Request_Id: RequestId,
                    EstimatedStartDate: ChangeDateFormat($('#tbEstimatedStartDate').val()),
                    EstimatedEndDate: ChangeDateFormat($('#tbEstimatedEndDate').val()),

                    ScopeofworkNewFileName: $('#hdnfileScopeNewName').val(),
                    ScopeofworkActualFileName: $('#hdnfileScopeActualName').val(),
                    ScopeofworkActualFileUrl: $('#hdnfileScopeFileUrl').val(),
                    UploadSignedDocumentList: UploadSignedDocuments,
                    QuotationEntryDetailsofDeliverableList: BankArray,
                    SpecialConditions: $('#txtSpecialCond').val(),
                    IsModuleAdminSubmit: true,
                    ModuleStatus: from,
                    QuotationNo: $('#txtQuotationNo').val(),
                    Shiping: $('#txtShiping').val(),
                    DeliveryAddress: $('#txtDeliveryAddress').val(),
                    AmendmentReason: $('#txtAmendmentReason').val(),
                    POC: $('#ddlPOCA').val(),                     
                    QuotationDate: ChangeDateFormat($('#txtQuotationDate').val()),
                    QuotationAgreementDate: ChangeDateFormat($('#txtQuotationAgreementDate').val()),
                    QuotationSignDate: ChangeDateFormat($('#txtQuotationSignDate').val()),
                }
                CommonAjaxMethod(virtualPath + 'ProcurementRequest/SaveQuotationEntry', objQuotation, 'POST', function (response) {
                    if (response.ValidationInput == 1) {
                        RedirectToModule();
                    }

                });


            }


        }
        else {
            FailToaster('Please upload  Signed Documents.');
        }
    }
    return isSaved;
}
function RedirectToModule() {
    var url = "/Procurement/ProcurementUserRequest";
    window.location.href = url;
}

function BindContract(objarrayinner) {


    for (var i = 0; i < objarrayinner.length; i++) {
        var strReturn = '<tr><td><a href="#" onclick="DownloadAttachFile(' + objarrayinner[i].Id + ')" ><i class="fas fa-edit"></i>' + objarrayinner[i].UploadSignedActualName + '</a><input value="' + objarrayinner[i].UploadSignedActualName + '" type="hidden" id="hdnAttachActulName_' + objarrayinner[i].Id + '" /><input value="' + objarrayinner[i].UploadSignedUrl + '" type="hidden" id="hdnAttachUrl_' + objarrayinner[i].Id + '" /></td>';

        var newtbblData = strReturn + "<td>" + objarrayinner[i].Remarks + "</td>" +
            "<td><a   title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='DeleteUploadSigned(this," + objarrayinner[i].Id + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr > ";

        $("#tblUploadSignedDocuments").find('tbody').append(newtbblData);
    }
}
 

function DownloadContractAttachFile(id)
{
    var fileURl = $('#hdnContractAttachUrl_' + id).val();
    var fileName = $('#hdnContractAttachActulName_' + id).val();
    if (fileURl != null || fileURl != undefined) {
        var stSplitFileName = fileName.split(".");
        var link = document.createElement("a");
        link.download = stSplitFileName[0];
        link.href = fileURl;
        link.click();
    }
}
function OnClose() {
    var url = "/Procurement/QuotationEntry"
    window.location.href = url;
}

function FillInvoiceDetail(data3)
{
    
    
    var array = data3;

    $('#tblDetailsofDeliverable').DataTable({
        "processing": true, // for show progress bar           
        "destroy": true,
        "data": array,
        "paging": false,
        "info": false,
        "columns": [
            { "data": "PaymentTerms" },
            {
                "orderable": false,
                "className": 'text-right',
                data: null, render: function (data, type, row) {

                    return '<label class="text-right">' + NumberWithComma(row.Amount) + '</label>';
                }
            },
            {
                "orderable": false,
                data: null, render: function (data, type, row) {
                    var dbDate = row.DueOn != null || row.DueOn != "" ? ChangeDateFormatToddMMYYY(row.DueOn) : "";
                    return '<label>' + dbDate + '</label>';
                }
            },
            {
                "orderable": false,
                data: null, render: function (data, type, row) {
                    return '<label>' + row.InvoiceNo + '</label>';
                }
            },
            {
                "orderable": false,
                data: null, render: function (data, type, row) {

                    var invoiceDate = row.InvoiceDate != null || row.InvoiceDate != "" ? ChangeDateFormatToddMMYYY(row.InvoiceDate) : "";
                    if (invoiceDate == "01-01-1900") {
                        invoiceDate = "";
                    }
                    return '<label>' + invoiceDate + '</label>';


                }
            },

            {
                "orderable": false,
                "className": 'text-right',
                data: null, render: function (data, type, row) {

                    return '<label class="text-right">' + NumberWithComma(row.InvoiceAmount) + '</label>';


                }
            },

            {
                "orderable": false,
                data: null, render: function (data, type, row) {


                    return '<label   style="display:none;" id="hdnUploadFileUrl_' + row.Id + '" >' + row.InvoiceAttachmentURL + '</label >' +
                        ' <label onclick="DownloadFileQuotation(this)"  id="hdnUploadActualFileName_' + row.Id + '">' + row.InvoiceAttachmentActualName + '</label>' +
                        ' <label  style="display:none;" id="hdnUploadNewFileName_' + row.Id + '">' + row.InvoiceAttachmentNewName + '</label>';



                }
            },

            {
                "orderable": false,
                data: null, render: function (data, type, row) {

                    if (row.InvoiceAttachmentActualNameD != undefined)
                    {
                        return '<label   style="display:none;" id="hdnUploadFileUrlD_' + row.Id + '" >' + row.InvoiceAttachmentURLD + '</label >' +
                            ' <label onclick="DownloadFileQuotationD(this)"  id="hdnUploadActualFileNameD_' + row.Id + '">' + row.InvoiceAttachmentActualNameD + '</label>' +
                            ' <label  style="display:none;" id="hdnUploadNewFileNameD_' + row.Id + '">' + row.InvoiceAttachmentNewNameD + '</label>';
                    }
                    else {
                        return '';
                    }


                }
            },
            {
                "orderable": false,
                data: null, render: function (data, type, row) {

                    var fStatus = "";
                    if (row.Status == null)
                    {
                        fStatus = "Pending";
                    }
                    if (row.Status == true)
                    {
                        fStatus = "Approved";
                    }
                    if (row.Status == false)
                    {
                        fStatus = "Rejected";
                    }
                    if (fStatus == 'Pending') {

                        return '<input type="text" autocomplete="off"   class="form-control  datepicker1" placeholder="Enter" id="tbPaidDate2_' + row.Id + '" onchange="HideErrorMessage(this)"> <span id="sptbPaidDate2_' + row.Id + '" class="text-danger field-validation-error" style="display:none;">Hey, You missed this field!!</span>';

                    }
                    else {
                        var PaidDate = row.PaidDate != null || row.PaidDate != "" ? ChangeDateFormatToddMMYYY(row.PaidDate) : "";
                        if (PaidDate == "01-01-1900") {
                            PaidDate = "";
                        }
                        return '<label>' + PaidDate + '</label>';
                    }

                }
            },
            {
                "orderable": false,
                "className": 'text-right',
                data: null, render: function (data, type, row) {

                    var fStatus = "";
                    if (row.Status == null) {
                        fStatus = "Pending";
                    }
                    if (row.Status == true) {
                        fStatus = "Approved";
                    }
                    if (row.Status == false) {
                        fStatus = "Rejected";
                    }
                    if (fStatus == 'Pending')
                    {
                        return '<input type="text" alt=""   onchange="HideErrorMessage(this)" onkeypress="validate(event)" onpaste="validate(event)" class="form-control Finance2" placeholder="Enter" id="tbPaidAmount2_' + row.Id + '"> <span id="sptbPaidAmount2_' + row.Id + '" class="text-danger field-validation-error" style="display:none;">Hey, You missed this field!!</span>';

                    }
                    else
                        return '<label class="text-right">' + NumberWithComma(row.PaidAmount) + '</label>';

                }
            },

            {
                "orderable": false,
               
                data: null, render: function (data, type, row) {

                    var fStatus = "";
                    if (row.Status == null) {
                        fStatus = "Pending";
                    }
                    if (row.Status == true) {
                        fStatus = "Approved";
                    }
                    if (row.Status == false) {
                        fStatus = "Rejected";
                    }
                    if (fStatus == 'Pending') {
                        
                        return '<textarea onchange="HideErrorMessage(this)" id="tbRemark2_' + row.Id + '" class="form-contorl h-60" placeholder="Enter Comment"></textarea><span id="sptbRemark2_' + row.Id + '" class="text-danger field-validation-error" style="display:none;">Hey, You missed this field!!</span>';
                    }
                    else
                        return '<label>' + row.Remark + '</label>';

                }
            },
            
            {
                "orderable": false,
                data: null, render: function (data, type, row) {
                    var fStatus = "";
                    if (row.Status == null) {
                        fStatus = "Pending";
                    }
                    if (row.Status == true) {
                        fStatus = "Approved";
                    }
                    if (row.Status == false) {
                        fStatus = "Rejected";
                    }
                    if (fStatus == 'Pending' && row.InvoiceAttachmentActualName!='')
                    {
                        
                        return '<button  id="btnSubmit21_' + row.Id + '"  type="button" onclick="InvoiceApprove(this,2,1)" class="bg-none green-clr" name="Command" value="Add" ><i class="fas fa-check"></i> Approve</button><button  id="btnSubmit22_' + row.Id + '"  type="button" onclick="InvoiceApprove(this,2,2)" class="bg-none red-clr" name="Command" value="Add" ><i class="fa fa-ban"></i> Reject</button>';
                    }
                    else {
                        return '<label>' + fStatus + '</label>';
                    }


                }
            }
        ]
    });
    

    
}
function InvoiceApprove(ctrl, from, type) {
    var id = ctrl.id.split('_');
    var controlNo = id[1];

    var tbRemark2_ = $('#tbRemark' + from + '_' + controlNo).val();
    var tbPaidAmount2_ = $('#tbPaidAmount' + from + '_' + controlNo).val();
    var tbPaidDate2_ = $('#tbPaidDate' + from + '_' + controlNo).val();
    var processType = "";
      processType = 13;
    var isValid = true;
    if (tbRemark2_ == "" && type == 2) {
        $("#sptbRemark" + from + "_" + controlNo).show();
        isValid = false;
    }
    if (tbPaidAmount2_ == "" && type == 1) {
        $("#sptbPaidAmount" + from + "_" + controlNo).show();
        isValid = false;
    }
    if (tbPaidDate2_ == "" && type == 1) {
        $("#sptbPaidDate" + from + "_" + controlNo).show();
        isValid = false;
    }
    var errorMessage = "Pls Reconfirm";
    var status = false;
    if (type == 1) {
        errorMessage = "Pls Reconfirm";
        status = true;
    }

    if (isValid == true) {

        var data = [];
        ConfirmMsgBox(errorMessage, '', function () {

            var obj =
            {
                Id: controlNo,
                PaidAmount: tbPaidAmount2_,
                PaidDate: ChangeDateFormat(tbPaidDate2_),
                Remark: tbRemark2_,
                Status: status

            }

            data.push(obj);

            var obj =
            {
                DetailsofDeliverableList: data,
                ProcessType: processType
            }
            CommonAjaxMethod(virtualPath + 'ProcurementRequest/SaveRFPEntry', obj, 'POST', function (response) {
                CommonAjaxMethod(virtualPath + 'ProcurementRequest/GetQuotationEntryConsultant', { Id: RequestId }
                    , 'GET', function (response) {
                        var data3 = response.data.data.Table2;//Procure_QuotationEntryConsultantFixedFee
                       // var data4 = response.data.data.Table3;//Procure_QuotationEntryConsultantReimbursableAmount
                        FillInvoiceDetail(data3);
                    });
            });

        });

    }
}
function DownloadFileQuotationPD2(ctrl)
{
    var id = ctrl.id.split('_');
    var controlNo = id[1];
    var fileURl = $('#hdnUploadFileUrlPD2_' + controlNo).text();
    var fileName = $('#hdnUploadActualFileNamePD2_' + controlNo).text();
    if (fileURl != null || fileURl != undefined) {
        var stSplitFileName = fileName.split(".");
        var link = document.createElement("a");
        link.download = stSplitFileName[0];
        link.href = fileURl;
        link.click();
    }
}

function BindMessageMember(d) {

    var $ele = $('#ddlMessageMember');
    $ele.empty();
    $ele.append($('<option/>').val('0').text('Select'));
    $.each(d, function (ii, vall) {
        $ele.append($('<option/>').val(vall.ID).text(vall.ValueName));
           
    })
    
}
function GetQuotationMember()
{

    CommonAjaxMethod(virtualPath + 'ProcurementRequest/BindProjectDetails', { id: RequestId, IsBindLine: 15 }
        , 'GET', function (response)
        {
            var data1 = response.data.data.Table;
            BindMessageMember(data1);
            
        });
}
function GetQuotationMessage()
{

    CommonAjaxMethod(virtualPath + 'ProcurementRequest/BindProjectDetails', { id: RequestId, IsBindLine: 16 }
        , 'GET', function (response)
    {
            var data1 = response.data.data.Table;
            BindQuoutationMessage(data1);
 
    });
}
function DisplayMessageDiv(ctrl)
{
    var id = ctrl.id.split('_');
    var controlNo = id[1];
    $('#Reply_' + controlNo).show();

}
function BindQuoutationMessage(data) {
    if ($('#hdnIsModuleAdminMessage').val() == 1) {
        $('#spMessage').text(data.length)
        const html = BindModuleAdminMessageTree(data)
        $('#divMessage').html(html);
    }
    else {
        const html = createTree(data)
        $('#divMessage').html(html);
    }
}
function BindModuleAdminMessageTree(data) {

    const nodeWithParent = data

    // Recursive function to create HTML out of node
    function getNodeHtmlModule(n) {
        const children = nodeWithParent.filter(d => d.ParentId === n.MessageId)
        var cls = "";
        if (n.SenderId == loggedinUserid) {
            cls = "mymsg";
        }
        let html = '';
        html += '<div class="d-flex commentlistdng ' + cls + '">' +
            '<div class="circle  circle-nm align-self-center mr-3" > ' + n.SName + '</div >' +
            ' <div class="flex-grow-1  " > ' +
            '<input type = "hidden" value="' + n.MessageId + '" id = "hdnMessageId_' + n.MessageId + '" /> ' +
            ' <input type = "hidden" value="' + n.SenderId + '" id = "hdnReciverId_' + n.MessageId + '" /> ' +

            ' <div class="mb-1 msgname"><strong>' + n.SenderFirstName + '</strong>' +
            '<small class="d-block msgdate">' + getdatetimewithoutJson(n.SendDate) + '</small>' +
            ' </div>' + n.Message +
            '' +
            ' <div class="row" id = "Reply_' + n.MessageId + '" style = "display:none;"  > ' +
            '<div class="col-sm-12 m-0 form-group"  > ' +
            '<textarea class="form-contorl h-60 Message_' + n.MessageId + '" placeholder="Enter Comment" id="txtSendMessage_' + n.MessageId + '" onchange="HideErrorMessage(this)"></textarea>' +
            ' <span id="sptxtSendMessage_' + n.MessageId + '" class="text-danger field-validation-error" style="display:none;">Hey, You missed this field!!</span>' +
            ' </div > ' +
            ' <div class="col-sm-12 mt-1 text-left " > ' +
            '<button type="button" class="btn btn-sm mtr-o-grn" id="btnSendMessageToUser_' + n.MessageId + '" onclick="SendMessageToUser(this)"><i class="fa fa-paper-plane" aria-hidden="true"></i>Send</button>' +

            ' </div> ' +
            ' </div> ' +
            '</div> ' +
            ' </div>';

        if (children.length > 0) {
            html += '<li><ul><li>'
                + children.map(getNodeHtmlModule).join('')
                + '</li></ul></li>'
        }
        html += ''
        return html
    }

    // Get all root nodes (without parent)
    const root = nodeWithParent.filter(d => d.ParentId === 0)

    return root.map(getNodeHtmlModule).join('')
}

function createTree(data) {
    const nodeWithParent = data

    // Recursive function to create HTML out of node
    function getNodeHtml(n) {
        const children = nodeWithParent.filter(d => d.ParentId === n.MessageId)
        var cls = "";
        if (n.SenderId == loggedinUserid) {
            cls = "mymsg";
        }
        let html = '';
        html += '<div class="d-flex commentlistdng ' + cls + '">' +
            '<div class="circle  circle-nm align-self-center mr-3" > ' + n.SName + '</div >' +
            ' <div class="flex-grow-1  " > ' +
            '<input type = "hidden" value="' + n.MessageId + '" id = "hdnMessageId_' + n.MessageId + '" /> ' +
            ' <input type = "hidden" value="' + n.SenderId + '" id = "hdnReciverId_' + n.MessageId + '" /> ' +

            ' <div class="mb-1 msgname"><strong>' + n.SenderFirstName + '</strong>' +
            '<small class="d-block msgdate">' + getdatetimewithoutJson(n.SendDate) + '</small>' +
            ' </div>' + n.Message +
            '<a href = "#" onclick = "DisplayMessageDiv(this)" id = "btnViewMessage_' + n.MessageId + '" class="mtr-o-clr" > <i class="fa fa-reply" aria-hidden="true"></i>Reply</a > ' +
            ' <div class="row" id = "Reply_' + n.MessageId + '" style = "display:none;"  > ' +
            '<div class="col-sm-12 m-0 form-group"  > ' +
            '<textarea class="form-contorl h-60 Message_' + n.MessageId + '" placeholder="Enter Comment" id="txtSendMessage_' + n.MessageId + '" onchange="HideErrorMessage(this)"></textarea>' +
            ' <span id="sptxtSendMessage_' + n.MessageId + '" class="text-danger field-validation-error" style="display:none;">Hey, You missed this field!!</span>' +
            ' </div > ' +
            ' <div class="col-sm-12 mt-1 text-left " > ' +
            '<button type="button" class="btn btn-sm mtr-o-grn" id="btnSendMessageToUser_' + n.MessageId + '" onclick="SendMessageToUser(this)"><i class="fa fa-paper-plane" aria-hidden="true"></i>Send</button>' +

            ' </div> ' +
            ' </div> ' +
            '</div> ' +
            ' </div>';

        if (children.length > 0) {
            html += '<li><ul><li>'
                + children.map(getNodeHtml).join('')
                + '</li></ul></li>'
        }
        html += ''
        return html
    }

    // Get all root nodes (without parent)
    const root = nodeWithParent.filter(d => d.ParentId === 0)

    return root.map(getNodeHtml).join('')
}
function SendMessage()
{

    if (checkValidationOnSubmit('Message') == true)
    {
        var objdata =
        {
            Id: 0,
            Procure_Request_Id: RequestId,
            ParentId: 0,
            Message: $('#txtMessage').val(),
            ReciverId: $('#ddlMessageMember').val().join()
        }
        var obj =
        {
            QuotationMessage: objdata,
            ProcessType: 17
        }
        CommonAjaxMethod(virtualPath + 'ProcurementRequest/SaveRFPEntry', obj, 'POST', function (response)
        {            
            $('#ddlMessageMember').val('0').trigger('change'); 
            $('#txtMessage').val();
            
            $('#btnModelPopupClose').trigger('click');
            
        });
    }
}
function SendMessageToUser(ctrl)
{
    var id = ctrl.id.split('_');
    var controlNo = id[1];
    if (checkValidationOnSubmit('Message_' + controlNo) == true)
    {
        var objdata =
        {
            Id: $('#hdnMessageId_' + controlNo).val(),
            Procure_Request_Id: RequestId,
            ParentId: 0,
            Message: $('#txtSendMessage_' + controlNo).val(),
            ReciverId: $('#hdnReciverId_' + controlNo).val() 
        }
        var obj =
        {
            QuotationMessage: objdata,
            ProcessType: 18
        }
        CommonAjaxMethod(virtualPath + 'ProcurementRequest/SaveRFPEntry', obj, 'POST', function (response) {
            AskQuery();
        });
    }
}
function AskQuery()
{
    CommonAjaxMethod(virtualPath + 'ProcurementRequest/BindProjectDetails', { id: RequestId, IsBindLine: 16 }
        , 'GET', function (response) {
            var data1 = response.data.data.Table;
            BindQuoutationMessage(data1);

        });
}

function ValidateProcureAmount(recAmount)
{
    var pAmount = $("#lblTotal2A").text();
    if (recAmount > pAmount) {

    }
}

function DownloadFileQuotationD(ctrl) {
    var id = ctrl.id.split('_');
    var controlNo = id[1];
    var fileURl = $('#hdnUploadFileUrlD_' + controlNo).text();
    var fileName = $('#hdnUploadActualFileNameD_' + controlNo).text();
    if (fileURl != null || fileURl != undefined) {
        var stSplitFileName = fileName.split(".");
        var link = document.createElement("a");
        link.download = stSplitFileName[0];
        link.href = fileURl;
        link.click();
    }
}