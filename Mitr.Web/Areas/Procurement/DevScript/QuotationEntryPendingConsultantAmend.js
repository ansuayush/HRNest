$(document).ready(function () {



    FillVendor();
   // GetQuotationMember();
    //GetQuotationMessage();
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
    disableInputs('tblPaymentTermReimbursable');
    disableInputs('btntblPaymentTermReimbursable');

});

$('body').on('focus', ".datepicker1", function () {
    $(this).datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: "dd-mm-yy",
        yearRange: "-90:+10"
    });
});

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
function FillQuotationProcureData(ctrl) {

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

        enableInputs('tblPaymentTermReimbursable');
        enableInputs('btntblPaymentTermReimbursable');
        BindProcureValueOfContract();
        GetQuotationEntryValueOfContract();
        GetQuotationContractDate();
        GetQuotationScopeOfWork();
        GetQuotationPaymentTerm();

    }
    else {


        if (addedItems == 'paymentterms' || removedItems == 'paymentterms') {
            GetQuotationPaymentTerm();
            if (addedItems == 'paymentterms') {
                enableInputs('tblPaymentTerm');
                enableInputs('btnAddPaymentButton');
                enableInputs('tblPaymentTermReimbursable');
                enableInputs('btntblPaymentTermReimbursable');
            }
            if (removedItems == 'paymentterms') {

                disableInputs('tblPaymentTerm');
                disableInputs('btnAddPaymentButton');
                disableInputs('tblPaymentTermReimbursable');
                disableInputs('btntblPaymentTermReimbursable');
            }
        }

        if (addedItems == 'contractenddate' || removedItems == 'contractenddate') {
            GetQuotationContractDate();
            GetQuotationPaymentTerm();
            if (addedItems == 'contractenddate') {

                enableInputs('tbEstimatedEndDate');

                enableInputs('tblPaymentTerm');
                enableInputs('btnAddPaymentButton');
                enableInputs('tblPaymentTermReimbursable');
                enableInputs('btntblPaymentTermReimbursable');
            }
            if (removedItems == 'contractenddate') {

                disableInputs('tbEstimatedEndDate');
                disableInputs('tblPaymentTerm');
                disableInputs('btnAddPaymentButton');
                disableInputs('tblPaymentTermReimbursable');
                disableInputs('btntblPaymentTermReimbursable');
            }
        }

        if (addedItems == 'scopeofwork' || removedItems == 'scopeofwork') {
            GetQuotationScopeOfWork();
            GetQuotationPaymentTerm();
            if (addedItems == 'scopeofwork') {
                enableInputs('fileScopeofwork');
                enableInputs('tblPaymentTerm');
                enableInputs('btnAddPaymentButton');
                enableInputs('tblPaymentTermReimbursable');
                enableInputs('btntblPaymentTermReimbursable');
            }
            if (removedItems == 'scopeofwork') {
                disableInputs('fileScopeofwork');
                disableInputs('tblPaymentTerm');
                disableInputs('btnAddPaymentButton');
                disableInputs('tblPaymentTermReimbursable');
                disableInputs('btntblPaymentTermReimbursable');
            }
        }

        if (addedItems == 'valueofcontract' || removedItems == 'valueofcontract') {
            BindProcureValueOfContract();
            GetQuotationEntryValueOfContract();
            if (addedItems == 'valueofcontract')

                enableInputs('fileScopeofwork');
            enableInputs('datatableApproved');
            enableInputs('datatableQoutationEntry');
            enableInputs('tblPaymentTerm');
            enableInputs('btnAddPaymentButton');
            enableInputs('tblPaymentTermReimbursable');
            enableInputs('btntblPaymentTermReimbursable');
        }
        if (removedItems == 'valueofcontract') {

            disableInputs('fileScopeofwork');
            disableInputs('datatableApproved');
            disableInputs('datatableQoutationEntry');
            disableInputs('tblPaymentTerm');
            disableInputs('btnAddPaymentButton');
            disableInputs('tblPaymentTermReimbursable');
            disableInputs('btntblPaymentTermReimbursable');
        }
    }



    if (removedItems == 'all') {
        for (var k = 0; k < strSpli.length; k++) {
            var all = strSpli[k];
            if (all != 'all') {
                if (data.indexOf(all) !== -1) {
                    //Nothing
                }
                else {
                    if (all == 'valueofcontract') {
                        BindProcureValueOfContract();
                        GetQuotationEntryValueOfContract();
                        disableInputs('datatableApproved');
                        disableInputs('datatableQoutationEntry');
                    }
                    if (all == 'scopeofwork') {
                        GetQuotationScopeOfWork();
                        disableInputs('fileScopeofwork');
                    }
                    if (all == 'contractenddate') {
                        GetQuotationContractDate();
                        disableInputs('tbEstimatedEndDate');
                    }
                    if (all == 'paymentterms') {
                        GetQuotationPaymentTerm();
                        disableInputs('tblPaymentTerm');
                        disableInputs('btnAddPaymentButton');
                        disableInputs('tblPaymentTermReimbursable');
                        disableInputs('btntblPaymentTermReimbursable');
                    }
                }
            }

        }
    }
}
 
function disableInputs(tbl) {
    const table = document.getElementById(tbl);
    table.classList.add('disabled');


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
function enableInputs(tbl) {
    const table = document.getElementById(tbl);
    table.classList.remove('disabled');
}
function RedirectAuthSign() {

    var url = "/Procurement/ProcurementUserRequest";
    window.location.href = url;
}
 
function DownloadFileQuotation(ctrl) {
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
 
function SetQuotationddlReimbursable(ctr) {
    var a = ctr.value;
    var j = 0;
    $('#tableToModify tr').each(function (i) {
        $("#txtReimbursable_" + j).val('0');
        if (a != "No") {
            $("#txtReimbursable_" + j).prop('disabled', false);
        }
        else {
            $("#txtReimbursable_" + j).prop('disabled', true);
        }
        j++;
    });


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
function ClearData(from) {
    if (from == 2) {
        var ctrl = document.getElementById('ddlType');
        $('#hdnUnitType').val(ctrl.value);
        isChanged = 1;
        $("#datatableQoutationEntry").find("tr:gt(1)").remove();
        var text = ctrl.options[ctrl.selectedIndex].text;
        $('#lblUnitType_0').text(text);
        SetMultiValue(ctrl);
    }
    else {
        document.getElementById('ddlType').value = $('#hdnUnitType').val();
        $('#select2-ddlType-container').text($('#lblUnitType_0').text());
    }
}
function OpenModelPopupSingle(ctrl) {
    var id = ctrl.id.split('_');
    var controlNo = id[1];

    var qId = $("#lblSRNo_" + controlNo).text();

    var mArray = MultiUnitArray.filter(function (itemParent) { return (itemParent.QuoteEntryId == qId); });


    $("#datatableMultiple").find("tr:gt(1)").remove();
    for (var i = 0; i < mArray.length; i++) {
        var newtbblData = "<tr><td>" + mArray[i].Description + "</td><td>" + mArray[i].UnitRate + "</td><td>" + mArray[i].Tax + "</td><td>" + mArray[i].Tax_GST + "</td><td><a class='HideClass' title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='DeleteUnitRate(this," + mArray[i].Id + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr>";

        $("#datatableMultiple").find('tbody').append(newtbblData);
    }

    $("#btnSingle").click();

}
function OpenModelPopupMulti(ctrl) {
    var id = ctrl.id.split('_');
    var controlNo = id[1];
    var qId = $("#lblSRNo_" + controlNo).text();


    var mArray = MultiUnitArray.filter(function (itemParent) { return (itemParent.QuoteEntryId == qId); });


    $("#datatableMultiple").find("tr:gt(1)").remove();
    for (var i = 0; i < mArray.length; i++) {
        var newtbblData = "<tr><td>" + mArray[i].Description + "</td><td>" + mArray[i].UnitRate + "</td><td>" + mArray[i].Tax + "</td><td>" + mArray[i].Tax_GST + "</td><td><a class='HideClass' title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='DeleteUnitRate(this," + mArray[i].Id + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr>";

        $("#datatableMultiple").find('tbody').append(newtbblData);
    }
    $("#btnMulti").click();

}
function SetUnitEnable() {
    var controlNo = $("#hdnMultiValue").val();
    $("#txtUnit_" + controlNo).val('0');
    $("#txtUnit_" + controlNo).prop('disabled', false);
}
function SetUnitDisable() {
    var controlNo = $("#hdnMultiValue").val();
    $("#txtUnit_" + controlNo).val('0');
    $("#txtUnit_" + controlNo).prop('disabled', true);

    var qId = $("#lblSRNo_" + controlNo).text();


    var mArray = MultiUnitArray.filter(function (itemParent) { return (itemParent.QuoteEntryId == qId); });


    $("#datatableMultiple").find("tr:gt(1)").remove();
    for (var i = 0; i < mArray.length; i++) {
        var newtbblData = "<tr><td>" + mArray[i].Description + "</td><td>" + mArray[i].UnitRate + "</td><td>" + mArray[i].Tax + "</td><td>" + mArray[i].Tax_GST + "</td><td><a class='HideClass' title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='DeleteUnitRate(this," + mArray[i].Id + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr>";

        $("#datatableMultiple").find('tbody').append(newtbblData);
    }
    $("#btnMulti").click();

}
var setOnchange = 0;
function SetMultiValue(ctrl) {

    var controlNo = '0';
    $("#hdnMultiValue").val(controlNo);
    $("#txtUnit_" + controlNo).val('0');
    $("#txtUnitRate_" + controlNo).val('0');
    $("#txtFixedFee_" + controlNo).val('0');
    $("#txtReimbursable_" + controlNo).val('0');
    $("#txtGST_" + controlNo).val('0');
    $("#txtTotal_" + controlNo).val('0');
    $("#txtTaxPer_" + controlNo).val('0');
    if (ctrl.value == 'Lumpsum' || ctrl.value == 'AsPerBudget') {
        $("#txtUnit_" + controlNo).val('0');
        $("#txtUnitRate_" + controlNo).val('0');
        $("#txtFixedFee_" + controlNo).val('0');
        $("#txtReimbursable_" + controlNo).val('0');
        $("#txtFixedFee_" + controlNo).prop('disabled', false);
        $("#txtUnit_" + controlNo).prop('disabled', true);
        $("#txtUnitRate_" + controlNo).prop('disabled', true);
        $("#txtTotal_" + controlNo).val('0');

    }
    else {
        $("#txtFixedFee_" + controlNo).val('0');
        $("#txtUnit_" + controlNo).val('0');
        $("#txtUnitRate_" + controlNo).val('0');
        $("#txtReimbursable_" + controlNo).val('0');
        $("#txtFixedFee_" + controlNo).prop('disabled', true);
        $("#txtUnit_" + controlNo).prop('disabled', false);
        $("#txtUnitRate_" + controlNo).prop('disabled', false);
        $("#txtTotal_" + controlNo).val('0');
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

    var fileURl = $('#hdnUploadFileUrlRFP').val();
    var fileName = $('#hdnUploadActualFileNameRFP').val();
    if (fileURl != null || fileURl != undefined) {
        var stSplitFileName = fileName.split(".");
        var link = document.createElement("a");
        link.download = stSplitFileName[0];
        link.href = fileURl;
        link.click();
    }

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

var QuotationProcurementData;
function BindProcureValueOfContract() {
    
            var response= QuotationProcurementData;
            var data1 = response.data.data.Table;

          
            var data2 = response.data.data.Table1;
         


            $("#ddlAgreementA").val('Consultant').trigger('change');
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
function BindProcureRequest() {
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

                CommonAjaxMethod(virtualPath + 'ProcurementRequest/GetQuotationEntryConsultant', { Id: RequestId }
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
            var data3 = response.data.data.Table2;


            $("#ddlAgreementA").val('Consultant').trigger('change');
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
function btnrowactivity1() {
    rowId = rowId + 1;

    $('.applyselect').select2("destroy");
    var $tableBody = $('#datatableQoutationEntry').find("tbody"),
        $trLast = $tableBody.find("tr:last"),
        $trNew = $trLast.clone();


    $trNew.find("label.lblSRNo").each(function (i) {
        $(this).attr({
            'id': "lblSRNo_" + (rowId)

        });
        $(this).text(rowId + 1)
    });

    $trNew.find("label.lblUnitType").each(function (i) {
        $(this).attr({
            'id': "lblUnitType_" + (rowId)

        });

    });



    $trNew.find("label.Fileurl").each(function (i) {
        $(this).attr({
            'id': "hdnUploadFileUrl_" + (rowId)
        });

    });
    $trNew.find("file.Attach").each(function (i) {
        $(this).attr({
            'id': "Attach_" + (rowId)
        });
        $(this).val('')
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





    $trNew.find("label.lblrecomandation").each(function (i) {
        $(this).attr({
            'for': "recomandation_" + (rowId)
        });

    });
    $trNew.find("input.recomandation").each(function (i) {
        $(this).attr({

            'id': "recomandation_" + (rowId)
        });
        $(this).checked = false;
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

    $trNew.find("input.txtFixedFee").each(function (i) {
        $(this).attr({

            'id': "txtFixedFee_" + (rowId)

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
    $trNew.find("input.txtReimbursable").each(function (i) {
        $(this).attr({

            'id': "txtReimbursable_" + (rowId)

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
        $(this).val()
    });


    $trNew.find("select.ddlVendor").each(function (i) {
        $(this).attr({
            'id': "ddlVendor_" + (rowId),
            'name': "ddlVendor_" + (rowId)

        });
        $(this).val('Select').trigger('change');

    });

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
    rowId = rowId - 1;
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

        $(this).find("label.lblSRNo").each(function (i) {
            $(this).attr({
                'id': "lblSRNo_" + delRow,
                'text': delRow + 1
            });

        });





        $(this).find("label.Fileurl").each(function (i) {
            $(this).attr({
                'id': "hdnUploadFileUrl_" + delRow
            });

        });
        $(this).find("file.Attach").each(function (i) {
            $(this).attr({
                'id': "Attach_" + delRow
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

        $(this).find("label.lblUnitType").each(function (i) {
            $(this).attr({
                'id': "lblUnitType_" + delRow
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

        $(this).find("input.txtFixedFee").each(function (i) {
            $(this).attr({
                'id': "txtFixedFee_" + delRow
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


        $(this).find("input.txtReimbursable").each(function (i) {
            $(this).attr({
                'id': "txtReimbursable_" + delRow
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




        $(this).find("textarea").each(function (i) {
            $(this).attr({
                'id': "txtRemarks_" + delRow
            });

        });

        delRow = delRow + 1;
    });

});

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
var ReimbursableBankArray = [];
var ReimbursableBankArrayId = 0;

var DocArray = [];
var DocArrayId = 0;
var MultiUnitArray = [];
var MultiUnitArrayId = 0;

 

function DeletePayment(obj, id) {

    ConfirmMsgBox("Are you sure want to delete", '', function () {

        $(obj).closest('tr').remove();
        BankArray = BankArray.filter(function (itemParent) { return (itemParent.Id != id); });

    })
}
function ReimbursableDeletePayment(obj, id) {

    ConfirmMsgBox("Are you sure want to delete", '', function () {

        $(obj).closest('tr').remove();
        ReimbursableBankArray = ReimbursableBankArray.filter(function (itemParent) { return (itemParent.Id != id); });

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
            $("#txtTotal_" + ctrlId).val(NumberWithComma(totalUnitRate + totaltax));

        }
    })
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

function AddPaymentTermReimbursable() {

    if (checkValidationOnSubmit('PaymentTermReimbursable') == true) {

        ReimbursableBankArrayId = ReimbursableBankArrayId + 1;
        var loop = ReimbursableBankArrayId;
        var objarrayinner =
        {
            Id: loop,
            PaymentTerms: $("#txtPaymentTermReimbursable").val(),
            Amount: $("#txtPaymentTermAmountReimbursable").val(),
            DueOn: ChangeDateFormat($("#txtPaymentTermDueOnReimbursable").val()),
            PaymentId: 0,
            IsSubmit: 0,

        }
        ReimbursableBankArray.push(objarrayinner);
        var newtbblData = "<tr><td>" + $("#txtPaymentTermReimbursable").val() + "</td><td>" + $("#txtPaymentTermAmountReimbursable").val() + "</td><td>" + $("#txtPaymentTermDueOnReimbursable").val() + "</td><td><a class='HideClass'  title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='ReimbursableDeletePayment(this," + objarrayinner.Id + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr>";

        $("#tblPaymentTermReimbursable").find('tbody').append(newtbblData);


        $("#txtPaymentTermReimbursable").val('');
        $("#txtPaymentTermAmountReimbursable").val('');
        $("#txtPaymentTermDueOnReimbursable").val('');

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

    var gstamt1 = parseInt((txtGST_ * txtFreight_) / 100);
    var total1 = (gstamt1 + parseInt(txtGST_));
    $('#txtTax_GST').val(total1);

}
function FillTotal(ctrl, from) {

    var id = ctrl.id.split('_');
    var controlNo = id[1];
    var txtTaxPer_ = $('#' + 'txtTaxPer_' + controlNo).val() == "" ? '0' : $('#' + 'txtTaxPer_' + controlNo).val();
    var txtFreight_ = $('#' + 'txtReimbursable_' + controlNo).val() == "" ? '0' : $('#' + 'txtReimbursable_' + controlNo).val();

    var txtFixedFee_ = $('#' + 'txtFixedFee_' + controlNo).val() == "" ? '0' : $('#' + 'txtFixedFee_' + controlNo).val();


    var a = $("#ddlType").val();
    if (a == 'Lumpsum' || a == 'AsPerBudget') {
        var perTotal = parseInt(txtFreight_) + parseInt(txtFixedFee_);
        var perPercent = (perTotal * parseInt(txtTaxPer_)) / 100;

        var total1 = parseInt(txtFreight_) + parseInt(txtFixedFee_) + perPercent;

        $('#' + 'txtGST_' + controlNo).val(Math.round(perPercent));

        $('#' + 'txtTotal_' + controlNo).val(NumberWithComma(Math.round(total1)));
    }
    else {

        var txtUnit_ = $('#' + 'txtUnit_' + controlNo).val() == "" ? '0' : $('#' + 'txtUnit_' + controlNo).val();
        var txtUnitRate_ = $('#' + 'txtUnitRate_' + controlNo).val() == "" ? '0' : $('#' + 'txtUnitRate_' + controlNo).val();

        var amt = parseInt(txtUnit_) * parseInt(txtUnitRate_);
        $('#' + 'txtFixedFee_' + controlNo).val(amt);

        var perTotal1 = parseInt(amt) + parseInt(txtFreight_);
        var perPercent1 = (perTotal1 * parseInt(txtTaxPer_)) / 100;

        var total2 = parseInt(perTotal1) + perPercent1;

        $('#' + 'txtGST_' + controlNo).val(Math.round(perPercent1));

        $('#' + 'txtTotal_' + controlNo).val(NumberWithComma(Math.round(total2)));

    }


}
 


function SaveAmendment() {
    var from = 2;
    if (checkValidationOnSubmit('AmendSubmit') == true) {
 
    var principleAmount = []
        var lieitem = [];
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
    for (var n = 0; n < QDArray.length; n++) {
        var procPRojectLine =
        {
            Id: QDArray[n].Id,
            VendorId: QDArray[n].VendorId,
            Rating: QDArray[n].Rating,
            Empanelled: QDArray[n].Empanelled,
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


    var isValid = 1;
    var isLineItemdocumentUpload = true;
    $('#tableToModify tr').each(function (i) {
        var procPRojectLine =
        {
            Id: $("#lblSRNo_" + i).text(),
            VendorId: $("#ddlVendor_" + i).val(),
            Rating: '',
            Empanelled: false,
            UnitType: $("#lblUnitType_" + i).text(),
            Units: $("#txtUnit_" + i).val(),
            UnitRate: $("#txtUnitRate_" + i).val(),
            Amount: $("#txtFixedFee_" + i).val(),
            FixedFee: $("#txtFixedFee_" + i).val(),
            ReimbursableAmount: $("#txtReimbursable_" + i).val(),
            Tax: $("#txtTaxPer_" + i).val(),
            GSTEtc: $("#txtGST_" + i).val(),
            Freight_TPT: $("#txtReimbursable_" + i).val(),
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
        if ($('#ddlReimbursable').val() == 'Yes') {
            if (procPRojectLine.ReimbursableAmount == '' || procPRojectLine.ReimbursableAmount == '0') {
                isValid = 0;
            }
        }
        lieitem.push(procPRojectLine);


    });
    if ($('#ddlType').val() == "Select" || $('#ddlType').val() == "") {
        isValid = 0;
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

    var isValidfileBudgetNewName = true


    var isValidReimbursable = true
    if ($('#ddlReimbursable').val() == 'Yes') {
        if (ReimbursableBankArray.length == 0) {
            isValidReimbursable = false;
            FailToaster("Please fill details of Reimbursable.");
        }
    }

    var isValidJust = true;
    
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
    if (isValidJust) {
        var checkDate3 = IsGreaterThanCurrentDate(ChangeDateFormat($('#tbEstimatedStartDate').val()), ChangeDateFormat($('#tbEstimatedEndDate').val()), 'Estimated start date should be always greater than Estimated End date.');

        if (checkDate3 == true && isValidRequiredAmount==true) {

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

                BudgetAttachmentNewFileName: $('#hdnBudgetAttachmentNewName').val(),
                BudgetAttachmentActualFileName: $('#hdnBudgetAttachmentActualName').val(),
                BudgetAttachmentActualFileUrl: $('#hdnBudgetAttachmentFileUrl').val(),
                SpecialConditions: $('#txtSpecialCond').val(),
               
                
                Reimbursable: $('#ddlReimbursable').val(),
                UnitType: $('#ddlType').val(),
                Status: from,
                QuotationEntryNatureofAttachmentList: DocArray,
                QuotationEntryDetailList: lieitem,
                QuotationEntryDetailFixedConsultantList: BankArray,
                QuotationEntryDetailsReimbursableConsultantList: ReimbursableBankArray,
                AmendReqNo: $('#AmendReqNo').text(),
                NatureofAmendment: $('#ddlAmend').val().join(),
                AmendReason: $('#txtAmendReason').val(),
                TotalRequiredAmount: $('#lblTotal2A').text(),
                AmendmentProcurementDataList: principleAmount
            }
            CommonAjaxMethod(virtualPath + 'ProcurementRequest/SaveQuotationEntryAmendmentConsultant', objQuotation, 'POST', function (response) {
                if (response.ValidationInput == 1) {
                    Redirect();
                }
            });

        }
    }
}
}

function Redirect() {
    var url = "/Procurement/ProcurementUserRequest";
    window.location.href = url;
}

function DownloadFileBudget() {
    var fileURl = $('#hdnBudgetAttachmentFileUrl').val();
    var fileName = $('#hdnBudgetAttachmentActualName').val();
    if (fileURl != null || fileURl != undefined) {
        var stSplitFileName = fileName.split(".");
        var link = document.createElement("a");
        link.download = stSplitFileName[0];
        link.href = fileURl;
        link.click();
    }
}
var QuotationData;
var QDArray = [];
function GetQuotationEntry() {
    CommonAjaxMethod(virtualPath + 'ProcurementRequest/GetQuotationEntryConsultant', { Id: RequestId }
        , 'GET', function (response) {
            QuotationData = response;
            BankArray = [];
            BankArrayId = 0;
            ReimbursableBankArray = [];
            ReimbursableBankArrayId = 0;

            DocArray = [];
            DocArrayId = 0;
            MultiUnitArray = [];
            MultiUnitArrayId = 0;

            var data1 = response.data.data.Table; //Procure_QuotationEntry
            var data2 = response.data.data.Table1;//Procure_QuotationEntryDetail
            var data3 = response.data.data.Table2;//Procure_QuotationEntryConsultantFixedFee
            var data4 = response.data.data.Table3;//Procure_QuotationEntryConsultantReimbursableAmount
            

             
        
            if (data1.length > 0) {               

                $("#hdnQuoteEntryId").val(data1[0].Id);
                $('#tbEstimatedStartDate').val(ChangeDateFormatToddMMYYY(data1[0].EstimatedStartDate));
                $('#tbEstimatedEndDate').val(ChangeDateFormatToddMMYYY(data1[0].EstimatedEndDate));
                $("#ddlAgreementA").val('Consultant').trigger('change');
                $("#hdnfileScopeNewName").val(data1[0].ScopeofworkNewFileName);
                $("#hdnfileScopeActualName").val(data1[0].ScopeofworkActualFileName);
                $("#hdnfileScopeFileUrl").val(data1[0].ScopeofworkActualFileUrl);
                $("#lblAttachementRFPDownload").text(data1[0].ScopeofworkActualFileName);
                $("#hdnBudgetAttachmentNewName").val(data1[0].BudgetAttachmentNewFileName);
                $("#hdnBudgetAttachmentActualName").val(data1[0].BudgetAttachmentActualFileName);
                $("#lblAttachementBudgetDownload").text(data1[0].BudgetAttachmentActualFileName);

                $("#hdnBudgetAttachmentFileUrl").val(data1[0].BudgetAttachmentActualFileUrl);

                $("#ddlReimbursable").val(data1[0].Reimbursable).trigger('change');
                $("#ddlType").val(data1[0].UnitType).trigger('change');
                $("#txtSpecialCond").val(data1[0].SpecialConditions);
                
            }

             QDArray = data2.filter(function (itemParent) { return (itemParent.IsRecommend != true); });

              data2 = data2.filter(function (itemParent) { return (itemParent.IsRecommend == true); });
         
            for (var j = 0; j < data2.length - 1; j++) {
                btnrowactivity1();
            }

            for (var h = 0; h < data2.length; h++) {
                var i = h;


                $("#lblSRNo_" + i).text(data2[h].Id);
                $('#' + 'ddlVendor_' + i).val(data2[h].VendorId).trigger('change');


                $("#txtUnit_" + i).val(data2[h].Units);
                $("#txtUnitRate_" + i).val(data2[h].UnitRate);
                $("#txtFixedFee_" + i).val(data2[h].FixedFee);
                $("#txtTaxPer_" + i).val(data2[h].Tax);
                $("#txtGST_" + i).val(data2[h].GSTEtc);
                $("#txtReimbursable_" + i).val(data2[h].ReimbursableAmount);
                $("#txtTotal_" + i).val(NumberWithComma(data2[h].TotalValue));
                $("#hdnUploadActualFileName_" + i).text(data2[h].AttachQuotationActualName);
                $("#hdnUploadNewFileName_" + i).text(data2[h].AttachQuotationNewName);
                $("#hdnUploadFileUrl_" + i).text(data2[h].AttachQuotationUrl);
                if (data2[h].IsRecommend == true)
                {
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
                    IsSubmit: data3[i].IsSubmit,

                }
                BankArray.push(objarrayinner);
            }

            BindBankArray(BankArray);

            for (var i = 0; i < data4.length; i++) {
                ReimbursableBankArrayId = i + 1;

                var objarrayinner =
                {
                    Id: ReimbursableBankArrayId,
                    PaymentTerms: data4[i].PaymentTerms,
                    Amount: data4[i].Amount,
                    DueOn: data4[i].DueOn,
                    PaymentId: data4[i].Id,
                    IsSubmit: data4[i].IsSubmit,

                }
                ReimbursableBankArray.push(objarrayinner);
            }

            ReimbursableBindBankArray(ReimbursableBankArray);              
            FillQuotationProcureData();

            var data11 = response.data.data.Table10;

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

function GetQuotationEntryValueOfContract() {
    QDArray = [];
            var response =QuotationData;
            
 
            var data2 = response.data.data.Table1;//Procure_QuotationEntryDetail         

           
            QDArray = data2.filter(function (itemParent) { return (itemParent.IsRecommend != true); });

            data2 = data2.filter(function (itemParent) { return (itemParent.IsRecommend == true); });

            for (var j = 0; j < data2.length - 1; j++) {
                btnrowactivity1();
            }

            for (var h = 0; h < data2.length; h++) {
                var i = h;


                $("#lblSRNo_" + i).text(data2[h].Id);
                $('#' + 'ddlVendor_' + i).val(data2[h].VendorId).trigger('change');


                $("#txtUnit_" + i).val(data2[h].Units);
                $("#txtUnitRate_" + i).val(data2[h].UnitRate);
                $("#txtFixedFee_" + i).val(data2[h].FixedFee);
                $("#txtTaxPer_" + i).val(data2[h].Tax);
                $("#txtGST_" + i).val(data2[h].GSTEtc);
                $("#txtReimbursable_" + i).val(data2[h].ReimbursableAmount);
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
function ReimbursableBindBankArray(array) {
    $("#tblPaymentTermReimbursable").find("tr:gt(1)").remove();
    for (var i = 0; i < array.length; i++)
    {
        var newtbblData = "";
        if (array[i].IsSubmit == "1") {
            newtbblData = "<tr><td>" + array[i].PaymentTerms + "</td><td>" + NumberWithComma(array[i].Amount) + "</td><td>" + ChangeDateFormatToddMMYYY(array[i].DueOn) + "</td><td></td></tr>";
        }
        else {
            newtbblData = "<tr><td>" + array[i].PaymentTerms + "</td><td>" + NumberWithComma(array[i].Amount) + "</td><td>" + ChangeDateFormatToddMMYYY(array[i].DueOn) + "</td><td><a class='HideClass' title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='ReimbursableDeletePayment(this," + array[i].Id + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr>";
        }
        $("#tblPaymentTermReimbursable").find('tbody').append(newtbblData);
    }
}

function GetQuotationContractDate() {
    
            var response = QuotationData;
            
            var data1 = response.data.data.Table; //Procure_QuotationEntry
            

            if (data1.length > 0) {
                $("#hdnQuoteEntryId").val(data1[0].Id);
                $('#tbEstimatedStartDate').val(ChangeDateFormatToddMMYYY(data1[0].EstimatedStartDate));
                $('#tbEstimatedEndDate').val(ChangeDateFormatToddMMYYY(data1[0].EstimatedEndDate));               

            }          
    
}

function GetQuotationScopeOfWork() {
    
            var response = QuotationData;
             
            var data1 = response.data.data.Table; //Procure_QuotationEntry


            if (data1.length > 0) {
                               
                $("#hdnfileScopeNewName").val(data1[0].ScopeofworkNewFileName);
                $("#hdnfileScopeActualName").val(data1[0].ScopeofworkActualFileName);
                $("#hdnfileScopeFileUrl").val(data1[0].ScopeofworkActualFileUrl);
                
            }

            
            
}

function GetQuotationPaymentTerm() {
    
            var response = QuotationData;
            BankArray = [];
            BankArrayId = 0;
            ReimbursableBankArray = [];
            ReimbursableBankArrayId = 0;          
            var data3 = response.data.data.Table2;//Procure_QuotationEntryConsultantFixedFee
            var data4 = response.data.data.Table3;//Procure_QuotationEntryConsultantReimbursableAmount
            
            for (var i = 0; i < data3.length; i++) {
                BankArrayId = i + 1;

                var objarrayinner =
                {
                    Id: BankArrayId,
                    PaymentTerms: data3[i].PaymentTerms,
                    Amount: data3[i].Amount,
                    DueOn: data3[i].DueOn,
                    PaymentId: data3[i].Id,
                    IsSubmit: data3[i].IsSubmit,

                }
                BankArray.push(objarrayinner);
            }

            BindBankArray(BankArray);

            for (var i = 0; i < data4.length; i++) {
                ReimbursableBankArrayId = i + 1;

                var objarrayinner =
                {
                    Id: ReimbursableBankArrayId,
                    PaymentTerms: data4[i].PaymentTerms,
                    Amount: data4[i].Amount,
                    DueOn: data4[i].DueOn,
                    PaymentId: data4[i].Id,
                    IsSubmit: data4[i].IsSubmit,

                }
                ReimbursableBankArray.push(objarrayinner);
            }

            ReimbursableBindBankArray(ReimbursableBankArray);
     
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
 