$(document).ready(function () {


   
    FillVendor();
   // GetQuotationMember();
   // GetQuotationMessage();
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
    disableInputs('datatableApproved');
    disableInputs('datatableQoutationEntry');
    disableInputs('tbEstimatedStartDate');
    disableInputs('tbEstimatedEndDate');
    disableInputs('fileScopeofwork');
    disableInputs('tblPaymentTerm');  
    disableInputs('btnAddPaymentButton');

});
$('body').on('focus', ".datepicker1", function () {
    $(this).datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: "dd-mm-yy",
        yearRange: "-90:+10"
    });
});
function getUnmatchedItems(string1, string2) {
    const array1 = string1.split(",");
    const array2 = string2.split(",");

    const unmatchedInArray1 = array1.filter(item => !array2.includes(item));    

    return unmatchedInArray1;
   
}

 
function compareArrays(arr1, arr2) {
    // Convert arrays to sets for easier comparison
    const set1 = new Set(arr1);
    const set2 = new Set(arr2);

    // Find added items by checking elements present in set2 but not in set1
    const addedItems = arr2.filter(item => !set1.has(item));

    // Find removed items by checking elements present in set1 but not in set2
    const removedItems = arr1.filter(item => !set2.has(item));

    return { addedItems, removedItems };
}
var PreArray = [];
function FillQuotationProcureData(ctrl)
{
    
    const string1 = "all,valueofcontract,contractenddate,scopeofwork,paymentterms";   
    var data = $('#ddlAmend').val().join(",");
   // const string2 = data;
    //var d = getUnmatchedItems(string1, string2);
    var d = data.split(',');  
    var strSpli = string1.split(',');  
    const { addedItems, removedItems } = compareArrays(PreArray, d);
    //console.log("Added items:", addedItems); // Output: [4]
   // console.log("Removed items:", removedItems); // Output: [1]


    PreArray = d;

    var isAll = false;
    for (var j = 0; j < d.length; j++) {
        var all = d[j]; 
        if (all == 'all') {
            isAll = true;
        }
    }
    if (isAll == true) {
        enableInputs('datatableApproved');
        enableInputs('datatableQoutationEntry');
       // enableInputs('tbEstimatedStartDate');
        enableInputs('tbEstimatedEndDate');
        enableInputs('fileScopeofwork');
        enableInputs('tblPaymentTerm');
        enableInputs('btnAddPaymentButton');
        GetQuotationValueOfContract();
        GetQuotationPaymentTerm();
        GetQuotationScope();
        GetContractDate();
        BindProcureValuOfContract();
    }
    else {
        if (addedItems == 'paymentterms' || removedItems == 'paymentterms')
        {
            GetQuotationPaymentTerm();
            if (addedItems == 'paymentterms') {
                enableInputs('tblPaymentTerm');
                enableInputs('btnAddPaymentButton');
            }
            if (removedItems == 'paymentterms')
            {

                disableInputs('tblPaymentTerm');
                disableInputs('btnAddPaymentButton');
            }
        }

        if (addedItems == 'contractenddate' || removedItems == 'contractenddate')
        {
            GetContractDate();
            GetQuotationPaymentTerm();
            if (addedItems == 'contractenddate') {
                
                enableInputs('tbEstimatedEndDate');
                enableInputs('tblPaymentTerm');
                enableInputs('btnAddPaymentButton');
            }
            if (removedItems == 'contractenddate') {
                
                disableInputs('tbEstimatedEndDate');
                disableInputs('tblPaymentTerm');
                disableInputs('btnAddPaymentButton');
            }
        }

        if (addedItems == 'scopeofwork' || removedItems == 'scopeofwork') {
            GetQuotationScope();
            if (addedItems == 'scopeofwork') {
                enableInputs('fileScopeofwork');
                enableInputs('tblPaymentTerm');
                enableInputs('btnAddPaymentButton');
            }
            if (removedItems == 'scopeofwork') {
                disableInputs('fileScopeofwork');
                disableInputs('tblPaymentTerm');
                disableInputs('btnAddPaymentButton');
            }
        }

        if (addedItems == 'valueofcontract' || removedItems == 'valueofcontract') {
            BindProcureValuOfContract();
            GetQuotationValueOfContract();
            if (addedItems == 'valueofcontract') {
                enableInputs('fileScopeofwork');
                enableInputs('datatableApproved');
                enableInputs('datatableQoutationEntry');
                enableInputs('tblPaymentTerm');
                enableInputs('btnAddPaymentButton');
            }
            if (removedItems == 'valueofcontract') {
                disableInputs('fileScopeofwork');
                disableInputs('datatableApproved');
                disableInputs('datatableQoutationEntry');
                disableInputs('tblPaymentTerm');
                disableInputs('btnAddPaymentButton');
            }
        }

        if (removedItems == 'all')
        {
            for (var k = 0; k < strSpli.length; k++)
            {
                var all = strSpli[k];
                if (all != 'all')
                {
                    if (data.indexOf(all)!== -1)
                    {
                        //Nothing
                    }
                    else
                    {
                        if (all=='valueofcontract')
                        {
                            BindProcureValuOfContract();
                            GetQuotationValueOfContract();
                            disableInputs('datatableApproved');
                            disableInputs('datatableQoutationEntry');
                        }
                        if (all=='scopeofwork') {
                            GetQuotationScope();
                            disableInputs('fileScopeofwork');
                        }
                        if (all=='contractenddate') {
                            GetContractDate();
                            disableInputs('tbEstimatedEndDate');
                        }
                        if (all=='paymentterms') {
                            GetQuotationPaymentTerm();
                            disableInputs('tblPaymentTerm');
                            disableInputs('btnAddPaymentButton');
                        }
                    }
                }
               
            }
        }
    }
    
    
}
function disableInputs(tbl)
{
    const table = document.getElementById(tbl);
    table.classList.add('disabled');
    
    
}

function enableInputs(tbl)
{
    const table = document.getElementById(tbl);
    table.classList.remove('disabled');
}
function ClearData(from) {
    if (from == 2) {
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
    else {
        document.getElementById('ddlType').value = $('#hdnUnitType').val();
        $('#select2-ddlType-container').text($('#lblUnitType_0').text());
    }
}
function SetQuotationUnit(ctrl) {

    var a = ctrl.value;
    if (a != "Select") {
        if ($('#lblUnitType_0').text() != '') {

            $("#btnSingleUnit").click();

        }
        else {

            var text = ctrl.options[ctrl.selectedIndex].text;

            $('#hdnUnitType').val(a);
            $('#lblUnitType_0').text(text);
            SetMultiValue(ctrl);
        }
    }
}
function OpenModelPopupSingle(ctrl) {
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
function OpenModelPopupMulti(ctrl) {
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
function SetUnitEnable(from) {
    fromMulitUnit = 0;
    var controlNo = $("#hdnMultiValue").val();
    if (from == 1) {
        $("#txtUnit_" + controlNo).val('0');
        $("#txtUnit_" + controlNo).prop('disabled', false);
    }
    if (from == 3) {

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
    if (from == 2) {
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
    if (from == 4) {
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
function SetMultiValue(ctrl) {

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

    if (ctrl.value == 'Lumpsum') {
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

    if (a == "rcmultiple") {

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
    if (a == "rcsingle") {

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
    if (a == "SingleUnit") {

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
    if (a == "MultipleUnit") {

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
function DownloadFileRFP() {
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

function DownloadFileQuotation(ctrl) {
    var id = ctrl.id.split('_');
    var controlNo = id[1];
    var fileURl = $('#hdnUploadFileUrl_' + controlNo).text();
    var fileName = $('#hdnUploadActualFileName_' + controlNo).text();
    if (fileURl != null || fileURl != undefined) {        
        var fpath = fileName.replace(/\\/g, '/');
        var fname = fpath.substring(fpath.lastIndexOf('/') + 1, fpath.lastIndexOf('.'));         
        var link = document.createElement("a");
        link.download = fname;
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
function DownloadQuoteFiles(id) {
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

var QuotationProcurementData;
function BindProcureValuOfContract() {
 
    var response = QuotationProcurementData;
            var data1 = response.data.data.Table;            
   

            var data2 = response.data.data.Table1;


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

                            return '<input value="' + row.BudgetAmount + '" type="hidden" id="hdnBudgetID_' + row.RowNum + '" /><label class="text-right">' + NumberWithComma(row.BudgetAmount) + '</label>';


                        }
                    },



                    {
                        "orderable": false,
                        data: null, render: function (data, type, row) {

                            // return '<label class="BudgetAmountData text-right">' + NumberWithComma(row.RequiredAmount) + '</label>';
                            return '<input value="' + row.Id + '" type="hidden" id="hdnAmountID_' + row.RowNum + '" /><input type="text" alt="" value="' + row.RequiredAmount + '"   onchange="SetRequiredBudgetValue(); HideErrorMessage(this)" onkeypress="validate(event)" onpaste="validate(event)" class="form-control BudgetAmountDataText AmendSubmit" placeholder="Enter" id="tbRequiredAmt_' + row.RowNum + '"> <span id="sptbRequiredAmt_' + row.RowNum + '" class="text-danger field-validation-error" style="display:none;">Hey, You missed this field!!</span>';
                        }
                    },

                    {
                        "orderable": false,
                        data: null, render: function (data, type, row) {

                            //  return '<label>' + row.Remark + '</label>';
                            return '<textarea onchange="HideErrorMessage(this)" id="tbRemarkAmount_' + row.RowNum + '" class="form-contorl h-60" placeholder="Enter Comment"></textarea><span id="sptbRemarkAmount_' + row.RowNum + '" class="text-danger field-validation-error" style="display:none;">Hey, You missed this field!!</span>';
                        }
                    },

                    { "data": "Status" },
                    { "data": "ApproveRejectReason" }


                ]
            });




}
function BindProcureRequest()
{

    CommonAjaxMethod(virtualPath + 'ProcurementRequest/BindProjectDetails', { id: RequestId, IsBindLine: 9 }
        , 'GET', function (response) {
            QuotationProcurementData = response;
            var data1 = response.data.data.Table;
         
            if (data1[0].Status == 35)
            {                
                $("#btnAmendSubmit").hide();
                $('#AmendReqNo').text(data1[0].AmendReq_No);
                $('#txtAmendReason').val(data1[0].Remark);
                var strAmendRe = data1[0].NatureOfAmemdment.split(',');                

                CommonAjaxMethod(virtualPath + 'ProcurementRequest/GetQuotationEntry', { Id: RequestId }
                    , 'GET', function (response) {
                        QuotationData = response;
                        $("#ddlAmend").select2({
                            multiple: true,
                        });
                        $('#ddlAmend').val(strAmendRe).trigger('change');
                    });


            }
            else {
                CommonAjaxMethod(virtualPath + 'ProcureVendorRegis/GetMaxReqNoForAmend', { procureId: RequestId }
                    , 'GET', function (response) {
                        $('#AmendReqNo').text(response.data.data.Table[0].ReqNo);
                    });
            }

            var data2 = response.data.data.Table1;             


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

                            return '<input value="' + row.BudgetAmount + '" type="hidden" id="hdnBudgetID_' + row.RowNum + '" /><label class="text-right">' + NumberWithComma(row.BudgetAmount) + '</label>';
                            

                        }    
                    },



                    {
                        "orderable": false,
                        data: null, render: function (data, type, row) {

                           // return '<label class="BudgetAmountData text-right">' + NumberWithComma(row.RequiredAmount) + '</label>';
                            return '<input value="' + row.Id + '" type="hidden" id="hdnAmountID_' + row.RowNum + '" /><input type="text" alt="" value="' + row.RequiredAmount + '"   onchange="SetRequiredBudgetValue(); HideErrorMessage(this)" onkeypress="validate(event)" onpaste="validate(event)" class="form-control BudgetAmountDataText AmendSubmit" placeholder="Enter" id="tbRequiredAmt_' + row.RowNum + '"> <span id="sptbRequiredAmt_' + row.RowNum + '" class="text-danger field-validation-error" style="display:none;">Hey, You missed this field!!</span>';
                        }
                    },

                    {
                        "orderable": false,
                        data: null, render: function (data, type, row) {

                          //  return '<label>' + row.Remark + '</label>';
                            return '<textarea onchange="HideErrorMessage(this)" id="tbRemarkAmount_' + row.RowNum + '" class="form-contorl h-60" placeholder="Enter Comment"></textarea><span id="sptbRemarkAmount_' + row.RowNum + '" class="text-danger field-validation-error" style="display:none;">Hey, You missed this field!!</span>';
                        }
                    },

                    { "data": "Status" },
                    { "data": "ApproveRejectReason" }


                ]
            });

        



        });


}
function SetRequiredBudgetValue() {
    var str1 = "";
    var Total = 0
    $(".BudgetAmountDataText").each(function () {
        str1 = $(this).val();
        str1 = str1.replace(/,/g, '');
        var current = Number(str1);
        Total += parseFloat(current);

    });    
    $("#lblTotal2A").text(NumberWithComma(Total));
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
                newtbblData = "<tr><td>" + array[i].Paymentinfavour + "</td><td>" + array[i].RecipientBankDetails + "</td><td>" + NumberWithComma(array[i].Amount) + "</td><td></td></tr>";

            }
            else {
                newtbblData = "<tr><td>" + array[i].Paymentinfavour + "</td><td>" + array[i].RecipientBankDetails + "</td><td>" + NumberWithComma(array[i].Amount) + "</td><td><a class='HideClass' title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='DeleteBankDetails(this," + array[i].Id + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr>";

            }

            $("#tblActivity1").find('tbody').append(newtbblData);

        }
    }

}

 
 


var BankDetailsArray = [];
var BankDetailsArrayId = 0;


var MSMEArray = [];
var MSMEArrayId = 0;


function AddNewRows() {
    if (checkValidationOnSubmit('type1') == true) {
        btnrowactivity1();
    }


}
var rowId = 0
 
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
                    delRow++;
                });


            });

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

        for (var i = 0; i < nArray.length; i++)
        {
            totalUnitRate += parseInt(nArray[i].UnitRate);
            totaltax += parseInt(nArray[i].Tax_GST);
        }
        var ctrlId = $("#hdnMultiValue").val();
        if (ctrlId != '')
        {
            $("#txtUnitRate_" + ctrlId).val(totalUnitRate);
            $("#txtGST_" + ctrlId).val(totaltax); 
            $("#txtTotal_" + ctrlId).val(NumberWithComma(totalUnitRate + totaltax)); 
            
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
            QuoteEntryId: $("#lblSRNo_" + $("#hdnMultiValue").val()).text(),
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


        if (ctrlId != '') {
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
            IsSubmit: 0,


        }
        BankArray.push(objarrayinner);
        var newtbblData = "<tr><td>" + $("#txtPaymentTerm").val() + "</td><td>" + $("#txtPaymentTermAmount").val() + "</td><td>" + $("#txtPaymentTermDueOn").val() + "</td><td><a class='HideClass'  title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='DeletePayment(this," + objarrayinner.Id + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr>";

        $("#tblPaymentTerm").find('tbody').append(newtbblData);


        $("#txtPaymentTerm").val('');
        $("#txtPaymentTermAmount").val('');
        $("#txtPaymentTermDueOn").val('');

    }

}
function FillTax() {
    var txtGST_ = $('#txtUnitRate').val() == "" ? '0' : $('#txtUnitRate').val();
    var txtFreight_ = $('#txtTax').val() == "" ? '0' : $('#txtTax').val();

    var gstamt1 = parseInt(((txtGST_ * txtFreight_) / 100), 10);
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
    var gstamt1 = parseInt(((tCal1 * parseInt(txtUnitTaxPer)) / 100), 10);
    $('#txtUnitTaxCalcu').val(Math.round(gstamt1));

}
function FillTotal(ctrl, from) {
    if (fromMulitUnit == 0) {
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
            'text': ''
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
    $trNew.find("input.recomandation").each(function (i) {
        $(this).attr({

            'id': "recomandation_" + (rowId),
            "checked": false
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


function Redirect() {

    var url = "/Procurement/ProcurementUserRequest";
    window.location.href = url;
}
var QuotationData;

var QDArray = [];
function GetQuotationEntry() {

    CommonAjaxMethod(virtualPath + 'ProcurementRequest/GetQuotationEntry', { Id: RequestId }
        , 'GET', function (response) {
            QuotationData = response;
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
 
            var data5 = response.data.data.Table4;//Procure_QuotationRateContractMultiple

            var data6 = response.data.data.Table5;//Procure_QuotationRateContractMultipleUnit








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


           // var newData = data2.filter(function (itemParent) { return (itemParent.IsRecommend == true); });
            QDArray = data2.filter(function (itemParent) { return (itemParent.IsRecommend != true); });
            data2 = data2.filter(function (itemParent) { return (itemParent.IsRecommend == true); });

            for (var j = 0; j < data2.length - 1; j++) {
                btnrowactivity1();
            }
            // data5 = data5.filter(function (itemParent) { return (itemParent.QuoteEntryId == data2[0].Id); });
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
            // data6 = data6.filter(function (itemParent) { return (itemParent.QuoteEntryId == data2[0].Id); });
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

                $("#hdnIsQuotationId").val(data2[h].Id);
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
                    PaymentId: data3[i].Id,
                    PaymentTerms: data3[i].PaymentTerms,
                    Amount: data3[i].Amount,
                    DueOn: data3[i].DueOn,
                    IsApproved: data3[i].Status,
                    IsSubmit: data3[i].IsSubmit,

                }
                BankArray.push(objarrayinner);
            }
            BindBankArray(BankArray);

           
            FillQuotationProcureData();
            
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
        });
}
function GetQuotationValueOfContract()
{
    QDArray = [];
            var response = QuotationData;
        MultiUnitArray = [];
        MultiUnitArrayId = 0;

        UnitArray = [];
        UnitArrayId = 0;
        var data1 = response.data.data.Table; //Procure_QuotationEntry
        var data2 = response.data.data.Table1;//Procure_QuotationEntryDetail
       
        var data5 = response.data.data.Table4;//Procure_QuotationRateContractMultiple

        var data6 = response.data.data.Table5;//Procure_QuotationRateContractMultipleUnit
        


         

      
            

            $("#hdnQuoteEntryId").val(data1[0].Id);
           
            $("#txtSpecialCond").val(data1[0].SpecialConditions);
           // $("#ddlType").val(data1[0].UnitType).trigger('change');
            $('#txtItemDescription').val(data1[0].ItemDescription);
             
 
           // var newData = data2.filter(function (itemParent) { return (itemParent.IsRecommend == true); });
            QDArray = data2.filter(function (itemParent) { return (itemParent.IsRecommend != true); });
     data2 = data2.filter(function (itemParent) { return (itemParent.IsRecommend == true); });


    for (var j = 0; j < data2.length - 1; j++)
            {
               btnrowactivity1();
            }
           // data5 = data5.filter(function (itemParent) { return (itemParent.QuoteEntryId == data2[0].Id); });
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
           // data6 = data6.filter(function (itemParent) { return (itemParent.QuoteEntryId == data2[0].Id); });
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

           
            for (var h = 0; h < data2.length; h++)
            {
            var i = h;

            $("#hdnIsQuotationId").val(data2[h].Id);
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
   
}
function GetContractDate() {

 
            var response = QuotationData;
            var data1 = response.data.data.Table; //Procure_QuotationEntry
            $('#tbEstimatedStartDate').val(ChangeDateFormatToddMMYYY(data1[0].EstimatedStartDate));
            $('#tbEstimatedEndDate').val(ChangeDateFormatToddMMYYY(data1[0].EstimatedEndDate));
            
        
}
function GetQuotationScope() {            
            var response = QuotationData;
            var data1 = response.data.data.Table; //Procure_QuotationEntry
                      
            $("#hdnfileScopeNewName").val(data1[0].ScopeofworkNewFileName);
            $("#hdnfileScopeActualName").val(data1[0].ScopeofworkActualFileName);
            $("#lblAttachementRFPDownload").text(data1[0].ScopeofworkActualFileName);
            
        
}
function GetQuotationPaymentTerm() {

 
            var response = QuotationData;
            BankArray = [];
            BankArrayId = 0;            
          
            var data3 = response.data.data.Table2;//Procure_QuotationEntryDetailsofDeliverable
         
            for (var i = 0; i < data3.length; i++) {
                BankArrayId = i + 1;

                var objarrayinner =
                {
                    Id: BankArrayId,
                    PaymentId: data3[i].Id,
                    PaymentTerms: data3[i].PaymentTerms,
                    Amount: data3[i].Amount,
                    DueOn: data3[i].DueOn,
                    IsApproved: data3[i].Status,
                    IsSubmit: data3[i].IsSubmit,

                }
                BankArray.push(objarrayinner);
            }
            BindBankArray(BankArray);
      
  }

function BindBankArray(array) {
    $("#tblPaymentTerm").find("tr:gt(1)").remove();
    for (var i = 0; i < array.length; i++)
    {
        var newtbblData = "";
        if (array[i].IsSubmit == "1") {
            newtbblData = "<tr><td>" + array[i].PaymentTerms + "</td><td>" + NumberWithComma(array[i].Amount) + "</td><td>" + ChangeDateFormatToddMMYYY(array[i].DueOn) + "</td><td></td></tr>";
        }
        else {
            newtbblData = "<tr><td>" + array[i].PaymentTerms + "</td><td>" + NumberWithComma(array[i].Amount) + "</td><td>" + ChangeDateFormatToddMMYYY(array[i].DueOn) + "</td><td><a class='HideClass' title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='DeletePayment(this," + array[i].Id + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr>";

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
 
function RedirectForCommitee() {

    var url = "/Procurement/ProcurementUserRequest";
    window.location.href = url;
}

 
var UploadSignedDocuments = [];
var UploadSignedId = 0;
 

 
function CancelAmendment() {
    var url = "/Procurement/ProcurementUserRequest";
    window.location.href = url;
}

 

 
function OnClose() {
    var url = "/Procurement/QuotationEntry"
    window.location.href = url;
} 
function ValidateProcureAmount(recAmount) {
    var pAmount = $("#lblTotal2A").text();
    if (recAmount > pAmount) {

    }
}

function SaveAmendment()
{   
    if (checkValidationOnSubmit('AmendSubmit') == true)
    {
        var from = 2;
        var principleAmount = []
        var isValidRequiredAmount = true;
        $('#datatableApproved tbody tr').each(function (i) {
            var nI = i + 1;
            var d1 = $("#tbRequiredAmt_" + nI).val();
            var d2 = $("#tbRemarkAmount_" + nI).val();
            var d3 = $("#hdnAmountID_" + nI).val();
            var d4 = $("#hdnBudgetID_" + nI).val();
            var procPRojectLine =
            {
                Id: d3,
                RequiredAmount: d1,
                Remark: d2
            }

            var budgetValue = Number(d4);
            if (budgetValue > 0) {
                var budgetPer = (budgetValue * 25) / 100;
                var TotalBudgetValue = budgetValue + budgetPer;

                if (Number(d1) > TotalBudgetValue) {
                    isValidRequiredAmount = false;

                    // $('#txtReqBudget_' + i).classList.add("errorValidation");
                    var ctrl = document.getElementById("tbRequiredAmt_" + nI);
                    ctrl.classList.add("errorValidation");
                }
            }

            principleAmount.push(procPRojectLine);
        });
        if (isValidRequiredAmount != true) {
            FailToaster("Required amount is greater than the budget amount.");
        }
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
                Empanelled: $("#lblEmp_" + i).text().toLowerCase() == 'yes' ? true : false,
                UnitType: $("#lblUnitType_" + i).text(),
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
            if (procPRojectLine.AttachQuotationActualName == "" && from == 2) {
                isLineItemdocumentUpload = false;
            }

            if (procPRojectLine.VendorId == null || procPRojectLine.VendorId == 'Select' || procPRojectLine.TotalValue == '0' || procPRojectLine.TotalValue == '') {
                isValid = 0;
            }
            lieitem.push(procPRojectLine);


        });
        if ($('#ddlType').val() == "Select" || $('#ddlType').val() == "") {
            isValid = 0;
        }
        for (var n = 0; n < QDArray.length; n++)
        {
            var procPRojectLine =
            {
                Id: QDArray[n].Id,
                VendorId: QDArray[n].VendorId,
                Rating: QDArray[n].Rating,
                Empanelled: QDArray[n].Empanelled , 
                UnitType: QDArray[n].UnitType,
                Units: QDArray[n].Units, 
                UnitRate: QDArray[n].UnitRate, 
                Amount: QDArray[n].Amount, 
                Tax: QDArray[n].Tax,
                GSTEtc: QDArray[n].GSTEtc, 
                Freight_TPT: QDArray[n].Freight_TPT,
                TotalValue: QDArray[n].TotalValue,
                AttachQuotationActualName: QDArray[n].AttachQuotationActualName, 
                AttachQuotationNewName: QDArray[n].AttachQuotationNewName,
                AttachQuotationUrl: QDArray[n].AttachQuotationUrl, 
                IsRecommend: QDArray[n].IsRecommend,
                Remark: QDArray[n].Remark
            }
            lieitem.push(procPRojectLine);
        }
        var isValiddeliverables = true
        if (BankArray.length == 0) {
            isValiddeliverables = false;
            FailToaster("Please fill details of deliverables.");
        }

        if (isLineItemdocumentUpload == false && from == 2) {
            FailToaster("Please attach Document in quoation entry details.");
        }
        var isValidfileScopeNewName = true
        if ($('#hdnfileScopeActualName').val() == '' && from == 2) {
            isValidfileScopeNewName = false;
            FailToaster("Please attach Document for Scope.");
        }
        var isValidJust = true;

        var isValidIsRecommend = true;

        if ($('#ddlType').val() == 'rcsingle' || $('#ddlType').val() == 'SingleUnit' || $('#ddlType').val() == 'Lumpsum') {
            if ($('#txtItemDescription').val() == "") {
                isValidItemDescription = false;
                FailToaster("Please fill item description.");
            }
        }

        if (isValidJust) {
            var checkDate3 = IsGreaterThanCurrentDate(ChangeDateFormat($('#tbEstimatedStartDate').val()), ChangeDateFormat($('#tbEstimatedEndDate').val()), 'Estimated start date should be always greater than Estimated End date.');


            if (checkDate3 == true && isValiddeliverables == true && isValidRequiredAmount==true)
            {
                
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
                        UnitType: $('#ddlType').val(),
                        Status: from,
                        ItemDescription: $('#txtItemDescription').val(),
                        QuotationEntryDetailList: lieitem,
                        QuotationEntryDetailsofDeliverableList: BankArray,
                        QuotationRateContractMultipleList: MultiUnitArray,
                        QuotationRateContractUnitMultipleList: UnitArray,
                        AmendReqNo: $('#AmendReqNo').text(),
                        NatureofAmendment: $('#ddlAmend').val().join(),
                        AmendReason: $('#txtAmendReason').val(),
                        TotalRequiredAmount: $('#lblTotal2A').text(),
                        AmendmentProcurementDataList: principleAmount,
                    }

                    CommonAjaxMethod(virtualPath + 'ProcurementRequest/SaveQuotationAmendmentEntry', objQuotation, 'POST', function (response) {
                        if (response.ValidationInput == 1) {
                            CancelAmendment();
                        }
                    });
                 
            }
        }
    }
    
}