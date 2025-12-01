$(document).ready(function () {
    $(".allow_numeric").on("input", function (evt) {
        var self = $(this);
        self.val(self.val().replace(/\D/g, ""));
        if ((evt.which < 48 || evt.which > 57)) {
            evt.preventDefault();
        }
    });

    //$(".allow_decimal").on("input", function (evt) {
    //    var self = $(this);
    //    self.val(self.val().replace(/[^0-9\.]/g, ''));
    //    if ((evt.which != 46 || self.val().indexOf('.') != -1) && (evt.which < 48 || evt.which > 57)) {
    //        evt.preventDefault();
    //    }
    //});


    BindProcureRequest();
    $(function () {
        $('.datepicker').datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: "dd-mm-yy",
            yearRange: "-90:+10"
        });

    });

    LoadMasterDropdown('ddlProjectCode',
        {
            ParentId: 0,
            masterTableType: 0,
            isMasterTableType: false,
            isManualTable: true,
            manualTable: ManaulTableEnum.Project,
            manualTableId: 0
        }, 'Select', false);


    LoadMasterDropdown('ddlPOC', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.EmployeeWithoutLWD,
        manualTableId: 0
    }, 'Select', false);

    LoadMasterDropdown('ddlPOCA', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.EmployeeWithoutLWD,
        manualTableId: 0
    }, 'Select', false);

    LoadMasterDropdown('ddlProject_0',
        {
            ParentId: 0,
            masterTableType: 0,
            isMasterTableType: false,
            isManualTable: true,
            manualTable: ManaulTableEnum.Project,
            manualTableId: 0
        }, 'Select', false);

    CommonAjaxMethod(virtualPath + 'ProcurementRequest/GetMaxReqNo', null, 'GET', function (response) {
        $('#ReqNo').val(response.data.data.Table[0].ReqNo);
    });
    var dt = new Date();
    var newDate = ChangeDateFormatToddMMYYY(dt);
    $('#ReqDate').val(newDate);
    $('#hdnLoggedInUser').val(loggedinUserid);
    $('#Reqby').val(loggedinUserName);



});
$('body').on('focus', ".datepicker1", function () {
    $(this).datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: "dd-mm-yy",
        yearRange: "-90:+10"
    });
});

function ShowTab(from) {



    if (from == 1) {
        $("#ShowTab2").removeClass("active");
        $("#ShowTab1").addClass("active");
        $("#pcTab").hide();
        $("#pmTab").show();
    }
    else {
        $("#ShowTab1").removeClass("active");
        $("#ShowTab2").addClass("active");
        $("#pcTab").show();
        $("#pmTab").hide();
    }
}
function AppoveRequest(from) {
    if (from == 2) {
        var RequestApprovalArray = [];

        var checkedArr = $("#datatableLineItemPr").find("input[type=checkbox]:checked").map(function () {
            return this.id;
        }).get();
        for (var i = 0; i < checkedArr.length; i++) {
            if (checkedArr[i].includes('chk')) {
                var ids = checkedArr[i].split('-');
                var obj =
                {
                    Id: ids[1],
                    UserId: loggedinUserid,
                    ProcureId: $("#hdnProcureRequestId").val(),
                    Reason: "",
                    Status: 1
                }
                RequestApprovalArray.push(obj);
            }
        }



        CommonAjaxMethod(virtualPath + 'ProcurementRequest/ApproveRejectProcurementRegistration', RequestApprovalArray, 'POST', function (response) {
            Redirect();
        });
    }
}
function SaveRFP() {

    var radios13 = document.getElementById('yes13');

    var RequestApprovalArray = [];
    var obj =
    {
        Id: 0,
        UserId: loggedinUserid,
        ProcureId: $("#hdnProcureRequestId").val(),
        Reason: '',
        Status: 4,
        IsMoreThanFiveLakh: radios13.checked == true ? true : false,
        IsFollowRFPRFQ: true,
        Requiredjustificationforwaiver: ''
    }
    RequestApprovalArray.push(obj);
    CommonAjaxMethod(virtualPath + 'ProcurementRequest/ApproveRejectProcurementRegistration', RequestApprovalArray, 'POST', function (response) {
        Redirect();
    });
}
function SaveRFPED() {


    if (checkValidationOnSubmit('RFPED') == true) {
        var RequestApprovalArray = [];
        var obj =
        {
            Id: 0,
            UserId: loggedinUserid,
            ProcureId: $("#hdnProcureRequestId").val(),
            Reason: '',
            Status: 6,
            IsMoreThanFiveLakh: true,
            IsFollowRFPRFQ: false,
            Requiredjustificationforwaiver: $("#txtJustification").val()
        }
        RequestApprovalArray.push(obj);
        CommonAjaxMethod(virtualPath + 'ProcurementRequest/ApproveRejectProcurementRegistration', RequestApprovalArray, 'POST', function (response) {
            window.location.reload();
        });
    }
}
function SaveRFPQuatation() {

    var RequestApprovalArray = [];
    var obj =
    {
        Id: 0,
        UserId: loggedinUserid,
        ProcureId: $("#hdnProcureRequestId").val(),
        Reason: '',
        Status: 7,
        IsMoreThanFiveLakh: false,
        IsFollowRFPRFQ: false,
        Requiredjustificationforwaiver: ""
    }
    RequestApprovalArray.push(obj);
    CommonAjaxMethod(virtualPath + 'ProcurementRequest/ApproveRejectProcurementRegistration', RequestApprovalArray, 'POST', function (response) {

        var strReturn = "";

        if ($("#ddlAgreementA").val() == 'Consultant') {

            strReturn = '/Procurement/QuotationEntryPendingConsultant?id=' + $("#hdnProcureRequestId").val();

        }
        else if ($("#ddlAgreementA").val() == 'Sub-grant') {

            strReturn = '/Procurement/QuotationEntryPendingSubgrant?id=' + $("#hdnProcureRequestId").val();


        }
        else {

            strReturn = '/Procurement/QuotationEntryPending?id=' + $("#hdnProcureRequestId").val();



        }
        window.location.href = strReturn;

    });

}
function SaveVendorRating() {

    var isRadiotrue = false;
    var isResiontrue = true;
    if ($('input[name=rating]:checked').length > 0) {
        isRadiotrue = true;
    }
    else {

        $('#spRdRating').show();
    }
    if ($('#txtRatingReason').val() == '') {
        isResiontrue = false;
        isValid = false
        $('#sptxtRatingReason').show();
    }


    if (isRadiotrue == true && isResiontrue == true) {
        var rating = document.querySelector('input[name = "rating"]:checked');
        var RequestApprovalArray = [];
        var obj =
        {
            Id: 0,
            UserId: loggedinUserid,
            ProcureId: $("#hdnProcureId").val(),
            Reason: $('#txtRatingReason').val(),
            Status: 14,
            Requiredjustificationforwaiver: rating.value
        }
        RequestApprovalArray.push(obj);
        CommonAjaxMethod(virtualPath + 'ProcurementRequest/ApproveRejectProcurementRegistration', RequestApprovalArray, 'POST', function (response) {
            var controlNo = $("#InvoiceId").val();
            var from = $("#Invoicefrom").val();
            SavetbldodData(controlNo, from, 1);
            //window.location.reload();
        }, true);
    }
}
function Redirect() {

    var url = "/Procurement/RFPEntry?id=" + $("#hdnProcureRequestId").val();
    window.location.href = url;

    //var btn = document.getElementById("btnRFPEntry");
    //btn.href = "~/Procurement/RFPEntry"
    //btn.click();
}
function RedirectProcurementUserRequest() {

    var url = "/Procurement/ProcurementUserRequest";
    window.location.href = url;

}
function HideErrorMessageOnRadio(ctr) {
    $('#spRdRating').hide();
}
function FillProjectDetails(ctrl) {
    var id = ctrl.id.split('_');
    var controlNo = id[1];
    var prjId = $('#' + 'ddlProject_' + controlNo).val();
    CommonAjaxMethod(virtualPath + 'ProcurementRequest/BindProjectDetails', { id: prjId, IsBindLine: 4, lineITems: '', SublineItem: ctrl.value }
        , 'GET', function (response) {
            var data2 = response.data.data.Table;
            var k = 0;
            var m = controlNo;
            $('#' + 'lblProjectLineDesc_' + m).text(data2[k].ProjectLineDesc);
            $('#' + 'lblProjectManager_' + m).text(data2[k].ProjectManager);
            $('#' + 'lblQuantity_' + m).text(data2[k].Quantity);
            $('#' + 'lblBudget_' + m).text(data2[k].total_budget);
            $('#' + 'txtReqBudget_' + m).val('');
            $('#' + 'txtRemark_' + m).val('');
            // $('#' + 'ddlProject_' + m).val(data2[k].ProjectCode).trigger('change');
            // $('#' + 'ddlProjectLine_' + m).val(data2[k].SublineItem).trigger('change');
            calculateBudget();
            calculateTotal();
        });
}
function FillProjectLine(ctrl) {
    var id = ctrl.id.split('_');
    var controlNo = id[1];
    var isEditable = $('#' + 'lblProjectLineId_' + controlNo).text();
    if (isEditable == "0") {
        CommonAjaxMethod(virtualPath + 'ProcurementRequest/BindProjectDetails', { id: ctrl.value, IsBindLine: 6, lineITems: '', SublineItem: '' }
            , 'GET', function (response) {
                var dataPending = response.data.data.Table;
                var $ele = $('#' + 'ddlProjectLine_' + controlNo);
                $ele.empty();
                $ele.append($('<option/>').val('Select').text('Select'));
                $.each(dataPending, function (ii, vall) {
                    $ele.append($('<option/>').val(vall.SublineItem).text(vall.SublineItem));
                })
            });

    }
    else {
        CommonAjaxMethod(virtualPath + 'ProcurementRequest/BindProjectDetails', { id: ctrl.value, IsBindLine: 0, lineITems: '', SublineItem: '' }
            , 'GET', function (response) {
                var dataPending = response.data.data.Table;
                var $ele = $('#' + 'ddlProjectLine_' + controlNo);
                $ele.empty();
                $ele.append($('<option/>').val('Select').text('Select'));
                $.each(dataPending, function (ii, vall) {
                    $ele.append($('<option/>').val(vall.SublineItem).text(vall.SublineItem));
                })
            });

    }
}
function SetWithdrawData(ctrl, from, Rid) {
    $('#txtWithdraw').val('');

    $('#hdnProcureIdForWithdrawFromInprogress').val(from);

    if (from != 1) {
        var id = ctrl.id.split('-');
        var controlNo = id[1];
        if (from == 3) {
            $('#hdnProcureLineId').val('');
            $('#hdnProcureIdForWithdraw').val(Rid);
        }
        else {
            $('#hdnProcureLineId').val(controlNo);
            $('#hdnProcureIdForWithdraw').val('');
        }
    }
    else {
        $('#hdnProcureLineId').val('');
        $('#hdnProcureIdForWithdraw').val(Rid);
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
    var total2 = 0;

    $(".BudgetAmountData").each(function () {
        str1 = $(this).text();
        str1 = str1.replace(/,/g, '');
        var current1 = Number(str1);
        total2 += parseFloat(current1);

    });
    var grandTotal = Total + total2;
    $("#lblTotal2A").text(NumberWithComma(grandTotal));
}

function SubmitPendingData() {
    var resubmitCollection = [];
    if (checkValidationOnSubmit('BudgetAmountDataText') == true) {
        $(".BudgetAmountDataText").each(function (i) {
            var tValue = $(this).val();
            tValue = tValue.replace(/,/g, '');
            var currentValue = Number(tValue);
            var id = $(this)[0].id.split('_');
            var lineId = id[1];
            var remark = $('#' + 'ApprovedRemarkId_' + lineId).val();

            var obj1 =
            {
                RequiredAmount: currentValue,
                LineId: lineId,
                Remark: remark,
                TotalValue: $("#lblTotal2A").text()
            }
            resubmitCollection.push(obj1);

        });

        var obj =
        {
            IsResubmit: 1,
            ProcureRequestProjectDetailList: resubmitCollection

        }
        CommonAjaxMethod(virtualPath + 'ProcurementRequest/SaveProcurementRegistration', obj, 'POST', function (response) {
            $("#hdnProcureRequestId").val(0);
            window.location.reload();
        });

    }

}
function SubmitWithdrawLineItem() {
    var isResubmit = 2;
    if ($('#hdnProcureIdForWithdrawFromInprogress').val() == "3") {
        isResubmit = 5;
    }
    var resubmitCollection = [];
    var from = 0;
    if ($('#hdnProcureLineId').val() != '') {
        RId = $('#hdnProcureLineId').val();
        from = 1;
    }
    else {
        RId = $('#hdnProcureIdForWithdraw').val();
        from = 2;
    }

    if (checkValidationOnSubmit('Withdraw') == true) {
        var obj = {

            LineId: RId,
            Remark: $('#txtWithdraw').val(),
            SourceData: from
        }
        resubmitCollection.push(obj);

        var obj =
        {
            IsResubmit: isResubmit,
            ProcureRequestProjectDetailList: resubmitCollection

        }
        CommonAjaxMethod(virtualPath + 'ProcurementRequest/SaveProcurementRegistration', obj, 'POST', function (response) {
            $("#hdnProcureRequestId").val('0');
            $('#hdnProcureIdForWithdrawFromInprogress').val('0');
            window.location.reload();

        });

    }
}
function CloseServicePopup() {
    window.location.reload();
}
function LoadLineItemData(id, reqId, from) {
    rowId = 0;
    $("#hdnProcureRequestId").val(id);

    if (from == 1) {
        CommonAjaxMethod(virtualPath + 'ProcurementRequest/BindProjectDetails', { id: id, IsBindLine: 3 }
            , 'GET', function (response) {
                var data1 = response.data.data.Table;
                var data2 = response.data.data.Table1;
                // var data3 = response.data.data.Table2;
                $("#ddlAgreement").val(data1[0].ContractType).trigger('change');
                if (data1[0].FromAsset == true) {
                    $("#ddlAgreement").prop('disabled', true);
                }
                else {
                    $("#ddlAgreement").prop('disabled', false);
                }
                $("#ddlPOC").val(data1[0].POC).trigger('change');
                //$("#lblTotal1").text(data1[0].BudgetAmount);
                // $("#lblTotal2").text(data1[0].RequiredAmount);
                $('#Reqby').val(data1[0].POCName);
                $('#ReqNo').val(data1[0].Req_No);
                $('#ReqDate').val(ChangeDateFormatToddMMYYY(data1[0].Req_Date));

                $('#tableToModify tr').each(function (i) {
                    if (i > 1)
                        $(this).closest('tr').remove();

                });

                for (var j = 0; j < data2.length - 1; j++) {
                    btnrowactivity1(from);
                }

                for (var h = 0; h < data2.length; h++) {
                    var m = h;

                    $('#' + 'lblProjectLineId_' + m).text(data2[h].Id);
                    $('#' + 'ddlProject_' + m).val(data2[h].ProjectCode).trigger('change');
                    $('#' + 'ddlProjectLine_' + m).val(data2[h].ProjectSubLineItem).trigger('change');

                    $('#' + 'lblProjectLineDesc_' + m).text(data2[h].ProjectLineDesc);
                    $('#' + 'lblProjectManager_' + m).text(data2[h].ProjectManager);
                    $('#' + 'lblQuantity_' + m).text(data2[h].Qty);
                    $('#' + 'lblBudget_' + m).text(data2[h].BudgetAmount);
                    $('#' + 'txtReqBudget_' + m).val(data2[h].RequiredAmount);
                    $('#' + 'txtRemark_' + m).val(data2[h].Remark);
                    //if (data2[h].Status == 'Rejected')
                    //{
                    //    $('#' + 'txtReqBudget_' + m).val('0');
                    //}
                    //if (data2[h].Status == 'Rejected' || data2[h].Status =='Approved')
                    //{
                    //    $('#' + 'ddlProject_' + m).prop("disabled", true);
                    //    $('#' + 'ddlProjectLine_' + m).prop("disabled", true);
                    //    $('#' + 'txtReqBudget_' + m).prop("disabled", true);
                    //    $('#' + 'txtRemark_' + m).prop("disabled", true);


                    //}
                    //if (data2[h].Status == 'Pending') {
                    //    $('#' + 'RemoveAnchor_' + m).show();
                    //    $('#' + 'WithdrawAnchor_' + m).hide();
                    //}
                    //else {
                    //    $('#' + 'RemoveAnchor_' + m).hide();
                    //    $('#' + 'WithdrawAnchor_' + m).show();
                    //}

                }
                //if (from != 1) {
                //    $('#btnSubmitData').hide();
                //    $('#btndvAdd').hide();
                //}
                calculateBudget();
                calculateTotalText();

            });
    }
    else if (from == 2) {

        $("#divActionData").show();

        CommonAjaxMethod(virtualPath + 'ProcurementRequest/BindProjectDetails', { id: id, IsBindLine: 7 }
            , 'GET', function (response) {
                var data1 = response.data.data.Table;

                if (data1[0].IsRFPProceed == 1 || data1[0].RFPMorethanFivelakh != null || data1[0].IsFollowRFPRFQ != null) {

                    $("#divProceed").show();
                    $("#btnSaveRFP").hide();
                    $("#btnSaveRFPED").hide();

                    if (data1[0].RFPMorethanFivelakh == true) {
                        document.getElementById('yes13').checked = true;
                        $('#yes13').click();
                        $("#yes13").prop('disabled', true);
                        $("#no14").prop('disabled', true);

                    }
                    else {

                        document.getElementById('no14').checked = true;
                        $("#yes13").prop('disabled', true);
                        $("#no14").prop('disabled', true);

                    }

                    if (data1[0].IsFollowRFPRFQ == true) {

                        $("#no17").prop('disabled', true);
                        $("#yes16").prop('disabled', true);
                        document.getElementById('no17').checked = true;
                        $('#no17').click();
                    }
                    else {
                        $("#no17").prop('disabled', true);
                        $("#yes16").prop('disabled', true);
                        $("#DivQuotation").hide();
                        document.getElementById('yes16').checked = true;
                        $('#yes16').click();
                    }


                }

                var data2 = response.data.data.Table1;
                var totalCount1 = 0;
                var totalWithDrwCount = 0;
                for (var m = 0; m < data2.length; m++) {
                    if (data2[m].Status == 'Approved') {
                        totalCount1++;
                    }

                    if (data2[m].Status == 'Withdrawn') {
                        totalWithDrwCount++;
                    }
                    if (data2[m].Status == 'Resubmitted' || data2[m].Status == 'Pending' || data2[m].Status == 'Rejected') {
                        $("#divProceed").hide();
                    }

                    if (data2[m].Status == 'Resubmitted') {
                        $("#dvSubmit").show();
                    }

                }
                if (totalCount1 != 0 && totalCount1 == data2.length) {
                    $("#dvSubmit").hide();
                }
                if (totalWithDrwCount != 0 && totalWithDrwCount == totalWithDrwCount) {
                    $("#dvSubmit").hide();
                    $("#divProceed").hide();
                }


                $('#txtJustification').val(data1[0].Requiredjustificationforwaiver);
                // var data3 = response.data.data.Table2;
                $("#ddlAgreementA").val(data1[0].ContractType).trigger('change');
                if (data1[0].FromAsset == true) {
                    $("#ddlAgreementA").prop('disabled', true);
                }
                else {
                    $("#ddlAgreementA").prop('disabled', false);
                }

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

                                var strReturn = '<input onchange="SetRequiredBudgetValue();HideErrorMessage(this)"  value="' + row.RequiredAmount + '" type="number"  id="ApprovedAmountId_' + row.Id + '" class="form-control text-right allow_numeric BudgetAmountDataText">'
                                    + '<span id="spApprovedAmountId_' + row.Id + '" class="text-danger field-validation-error Project" style="display:none;">Hey, You missed this field!!</span>';

                                if (row.Status == 'Resubmitted') {
                                    return strReturn;
                                }
                                else {
                                    return '<label class="BudgetAmountData text-right">' + NumberWithComma(row.RequiredAmount) + '</label>';
                                }
                            }
                        },

                        {
                            "orderable": false,
                            data: null, render: function (data, type, row) {

                                var strReturn = '<textarea class="form-control maxSize[1200]" id="ApprovedRemarkId_' + row.Id + '" placeholder="Enter">' + row.Remark + '</textarea>';

                                if (row.Status == 'Resubmitted') {
                                    return strReturn;
                                }
                                else {
                                    return '<label>' + row.Remark + '</label>';
                                }
                            }
                        },

                        { "data": "Status" },
                        { "data": "ApproveRejectReason" },
                        {
                            "orderable": false,
                            data: null, render: function (data, type, row) {


                                var strReturn = '<a id="chk-' + row.Id + '-' + row.Procure_Request_Id + '" onclick="SetWithdrawData(this,2,' + row.Id + ')" title="" data-toggle="modal" data-target="#reason" class="red-clr"><i class="fas fa-trash-alt" data-toggle="tooltip" title="Withdraw "></i></a>';
                                //if (row.IsManager == 1 && row.Status == 'Pending') {
                                //    return strReturn;
                                //}
                                //else {
                                //    return "";
                                //}

                                if (data1[0].IsRFPProceed == 1 || data1[0].IsFollowRFPRFQ != null) {
                                    strReturn = "";


                                }
                                return strReturn;
                            }
                        }



                    ]
                });


                SetRequiredBudgetValue();
                calculateBudget();
                //  calculateTotal();

            });

    }

    else if (from == 3) {
        $("#divActionData").hide();
        CommonAjaxMethod(virtualPath + 'ProcurementRequest/BindProjectDetails', { id: id, IsBindLine: 14 }
            , 'GET', function (response) {
                var data1 = response.data.data.Table;
                var data3 = response.data.data.Table2;
                if (data3[0].Status == 6 || data3[0].Status == 5) {
                    $("#divProceed").hide();
                    $("#dvSubmit").hide();
                }
                else if (data1[0].IsRFPProceed == 1 || data1[0].RFPMorethanFivelakh != null || data1[0].IsFollowRFPRFQ != null) {

                    $("#divProceed").show();
                    $("#btnSaveRFP").hide();
                    $("#btnSaveRFPED").hide();

                    if (data1[0].RFPMorethanFivelakh == true) {
                        document.getElementById('yes13').checked = true;
                        $('#yes13').click();
                    }
                    else {
                        document.getElementById('no14').checked = true;
                        $('#no14').click();
                    }

                    if (data1[0].IsFollowRFPRFQ == true) {
                        document.getElementById('no17').checked = true;
                        $('#no17').click();
                    }
                    else {
                        document.getElementById('yes16').checked = true;
                        $('#yes16').click();
                    }


                }

                var data2 = response.data.data.Table1;
                var totalCount = 0;
                for (var m = 0; m < data2.length; m++) {
                    if (data2[m].Status == 'Approved') {
                        totalCount++;
                    }
                    if (data2[m].Status == 'Resubmitted' || data2[m].Status == 'Pending' || data2[m].Status == 'Rejected') {
                        $("#divProceed").hide();
                    }

                    if (data2[m].Status == 'Resubmitted') {
                        $("#dvSubmit").show();
                    }

                }
                if (totalCount == data2.length) {
                    $("#dvSubmit").hide();
                }

                $("#dvSubmit").hide();
                $('#txtJustification').val(data1[0].Requiredjustificationforwaiver);
                // var data3 = response.data.data.Table2;
                $("#ddlAgreementA").val(data1[0].ContractType).trigger('change');
                if (data1[0].FromAsset == true) {
                    $("#ddlAgreementA").prop('disabled', true);
                }
                else {
                    $("#ddlAgreementA").prop('disabled', false);
                }

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
                        }
                        ,

                        {
                            "orderable": false,
                            data: null, render: function (data, type, row) {

                                var strReturn = '<input onchange="SetRequiredBudgetValue();HideErrorMessage(this)"  value="' + row.RequiredAmount + '" type="number"  id="ApprovedAmountId_' + row.Id + '" class="form-control text-right allow_numeric BudgetAmountDataText">'
                                    + '<span id="spApprovedAmountId_' + row.Id + '" class="text-danger field-validation-error Project" style="display:none;">Hey, You missed this field!!</span>';

                                if (row.Status == 'Resubmitted') {
                                    return strReturn;
                                }
                                else {
                                    return '<label class="BudgetAmountData text-right">' + NumberWithComma(row.RequiredAmount) + '</label>';
                                }
                            }
                        },

                        {
                            "orderable": false,
                            data: null, render: function (data, type, row) {

                                var strReturn = '<textarea class="form-control maxSize[1200]" id="ApprovedRemarkId_' + row.Id + '" placeholder="Enter">' + row.Remark + '</textarea>';

                                if (row.Status == 'Resubmitted') {
                                    return strReturn;
                                }
                                else {
                                    return '<label>' + row.Remark + '</label>';
                                }
                            }
                        },

                        { "data": "Status" },
                        { "data": "ApproveRejectReason" },
                        {
                            "orderable": false,
                            data: null, render: function (data, type, row) {


                                var strReturn = '<a id="chk-' + row.Id + '-' + row.Procure_Request_Id + '" onclick="SetWithdrawData(this,2,' + row.Id + ')" title="" data-toggle="modal" data-target="#reason" class="red-clr"><i class="fas fa-trash-alt" data-toggle="tooltip" title="Withdraw "></i></a>';
                                //if (row.IsManager == 1 && row.Status == 'Pending') {
                                //    return strReturn;
                                //}
                                //else {
                                //    return "";
                                //}

                                if (data1[0].IsRFPProceed == 1 || row.Status == 'Rejected' || data3[0].Status == 6 || data3[0].Status == 5) {
                                    strReturn = "";


                                }
                                return strReturn;
                            }
                        }



                    ]
                });


                SetRequiredBudgetValue();
                calculateBudget();
                calculateTotal();

            });


    }

    else if (from == 4) {
        CommonAjaxMethod(virtualPath + 'ProcurementRequest/BindProjectDetails', { id: id, IsBindLine: 7 }
            , 'GET', function (response) {
                var data1 = response.data.data.Table;

                if (data1[0].IsRFPProceed == 1 || data1[0].RFPMorethanFivelakh != null || data1[0].IsFollowRFPRFQ != null) {

                    $("#divProceed").show();
                    $("#btnSaveRFP").hide();
                    $("#btnSaveRFPED").hide();

                    if (data1[0].RFPMorethanFivelakh == true) {
                        document.getElementById('yes13').checked = true;
                        $('#yes13').click();
                    }
                    else {
                        document.getElementById('no14').checked = true;
                        $('#no14').click();
                    }

                    if (data1[0].IsFollowRFPRFQ == true) {
                        document.getElementById('no17').checked = true;
                        $('#no17').click();
                    }
                    else {
                        document.getElementById('yes16').checked = true;
                        $('#yes16').click();
                    }


                }

                var data2 = response.data.data.Table1;
                var totalCount = 0;
                for (var m = 0; m < data2.length; m++) {
                    if (data2[m].Status == 'Approved') {
                        totalCount++;
                    }
                    if (data2[m].Status == 'Resubmitted' || data2[m].Status == 'Pending' || data2[m].Status == 'Rejected') {
                        $("#divProceed").hide();
                    }

                    if (data2[m].Status == 'Resubmitted') {
                        $("#dvSubmit").show();
                    }

                }
                if (totalCount == data2.length) {
                    $("#dvSubmit").hide();
                }

                $("#dvSubmit").hide();
                $('#txtJustification').val(data1[0].Requiredjustificationforwaiver);
                // var data3 = response.data.data.Table2;
                $("#ddlAgreementA").val(data1[0].ContractType).trigger('change');
                if (data1[0].FromAsset == true) {
                    $("#ddlAgreementA").prop('disabled', true);
                }
                else {
                    $("#ddlAgreementA").prop('disabled', false);
                }

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
                        }
                        ,

                        {
                            "orderable": false,
                            data: null, render: function (data, type, row) {

                                var strReturn = '<input onchange="SetRequiredBudgetValue();HideErrorMessage(this)"  value="' + row.RequiredAmount + '" type="number"  id="ApprovedAmountId_' + row.Id + '" class="form-control text-right allow_numeric BudgetAmountDataText">'
                                    + '<span id="spApprovedAmountId_' + row.Id + '" class="text-danger field-validation-error Project" style="display:none;">Hey, You missed this field!!</span>';

                                if (row.Status == 'Resubmitted') {
                                    return strReturn;
                                }
                                else {
                                    return '<label class="BudgetAmountData text-right">' + NumberWithComma(row.RequiredAmount) + '</label>';
                                }
                            }
                        },

                        {
                            "orderable": false,
                            data: null, render: function (data, type, row) {

                                var strReturn = '<textarea class="form-control maxSize[1200]" id="ApprovedRemarkId_' + row.Id + '" placeholder="Enter">' + row.Remark + '</textarea>';

                                if (row.Status == 'Resubmitted') {
                                    return strReturn;
                                }
                                else {
                                    return '<label>' + row.Remark + '</label>';
                                }
                            }
                        },

                        { "data": "Status" },
                        { "data": "ApproveRejectReason" },
                        {
                            "orderable": false,
                            data: null, render: function (data, type, row) {


                                var strReturn = '<a id="chk-' + row.Id + '-' + row.Procure_Request_Id + '" onclick="SetWithdrawData(this,2,' + row.Id + ')" title="" data-toggle="modal" data-target="#reason" class="red-clr"><i class="fas fa-trash-alt" data-toggle="tooltip" title="Withdraw "></i></a>';
                                //if (row.IsManager == 1 && row.Status == 'Pending') {
                                //    return strReturn;
                                //}
                                //else {
                                //    return "";
                                //}

                                if (data1[0].IsRFPProceed == 1) {
                                    strReturn = "";


                                }
                                return strReturn;
                            }
                        }



                    ]
                });


                SetRequiredBudgetValue();
                calculateBudget();
                calculateTotal();
            });

    }

    else if (from == 5) {
        CommonAjaxMethod(virtualPath + 'ProcurementRequest/BindProjectDetails', { id: id, IsBindLine: 7 }
            , 'GET', function (response) {
                var data1 = response.data.data.Table;
                var data2 = response.data.data.Table1;
                $("#divProceed").hide();
                $("#btnSaveRFP").hide();
                $("#btnSaveRFPED").hide();
                $("#dvSubmit").hide();
                $('#txtJustification').val(data1[0].Requiredjustificationforwaiver);
                // var data3 = response.data.data.Table2;
                $("#ddlAgreementA").val(data1[0].ContractType).trigger('change');
                $("#ddlAgreementA").prop('disabled', true);

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

                                var strReturn = '<input onchange="SetRequiredBudgetValue();HideErrorMessage(this)"  value="' + row.RequiredAmount + '" type="number"  id="ApprovedAmountId_' + row.Id + '" class="form-control text-right allow_numeric BudgetAmountDataText">'
                                    + '<span id="spApprovedAmountId_' + row.Id + '" class="text-danger field-validation-error Project" style="display:none;">Hey, You missed this field!!</span>';

                                if (row.Status == 'Resubmitted') {
                                    return strReturn;
                                }
                                else {
                                    return '<label class="BudgetAmountData text-right">' + NumberWithComma(row.RequiredAmount) + '</label>';
                                }
                            }
                        },

                        {
                            "orderable": false,
                            data: null, render: function (data, type, row) {

                                var strReturn = '<textarea class="form-control maxSize[1200]" id="ApprovedRemarkId_' + row.Id + '" placeholder="Enter">' + row.Remark + '</textarea>';

                                if (row.Status == 'Resubmitted') {
                                    return strReturn;
                                }
                                else {
                                    return '<label>' + row.Remark + '</label>';
                                }
                            }
                        },

                        { "data": "Status" },
                        { "data": "ApproveRejectReason" },
                        {
                            "orderable": false,
                            data: null, render: function (data, type, row) {


                                var strReturn = '<a id="chk-' + row.Id + '-' + row.Procure_Request_Id + '" onclick="SetWithdrawData(this,2,' + row.Id + ')" title="" data-toggle="modal" data-target="#reason" class="red-clr"><i class="fas fa-trash-alt" data-toggle="tooltip" title="Withdraw "></i></a>';
                                //if (row.IsManager == 1 && row.Status == 'Pending') {
                                //    return strReturn;
                                //}
                                //else {
                                //    return "";
                                //}

                                if (data1[0].IsRFPProceed == 1) {
                                    strReturn = "";


                                }
                                return strReturn;
                            }
                        }



                    ]
                });


                SetRequiredBudgetValue();
                calculateBudget();
                calculateTotal();
            });

    }
}

var rowId = 0

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

        this.cells[0].innerText = i + 1;
        //    var textinput = $(this).find('input');
        //var textarea = $(this).find('textarea');
        //selectinput.eq(0).attr('id', 'FinancialItem' + i);
        //textinput.eq(0).attr('id', 'FinancialAmount' + i);
        //textarea.eq(0).attr('id', 'FinancialDescription' + i);
    });

    calculateTotal();
    calculateBudget();
    $('#tableToModify tr').each(function (j) {


        $(this).find("span.Budget").each(function (i) {
            $(this).attr({
                'id': "sptxtReqBudget_" + delRow
            });

        });
        $(this).find("span.ProjectLine").each(function (i) {
            $(this).attr({
                'id': "spddlProjectLine_" + delRow
            });

        });
        $(this).find("span.Project").each(function (i) {
            $(this).attr({
                'id': "spddlProject_" + delRow
            });

        });
        $(this).find("label.lblProjectLineDesc").each(function (i) {
            $(this).attr({
                'id': "lblProjectLineDesc_" + delRow
            });

        });

        $(this).find("label.lblProjectManager").each(function (i) {
            $(this).attr({
                'id': "lblProjectManager_" + delRow
            });

        });

        $(this).find("label.lblQuantity").each(function (i) {
            $(this).attr({
                'id': "lblQuantity_" + delRow
            });

        });

        $(this).find("label.lblBudget").each(function (i) {
            $(this).attr({
                'id': "lblBudget_" + delRow
            });

        });


        $(this).find("select.ddlProject").each(function (i) {
            $(this).attr({
                'id': "ddlProject_" + delRow,
                'name': "ddlProject_" + delRow
            });

        });
        $(this).find("select.ddlProjectLine").each(function (i) {
            $(this).attr({

                'id': "ddlProjectLine_" + delRow,
                'name': "ddlProjectLine_" + delRow
            });

        });
        $(this).find("input").each(function (i) {
            $(this).attr({

                'id': "txtReqBudget_" + delRow,
                'name': "txtReqBudget_" + delRow
            });

        });
        $(this).find("textarea").each(function (i) {
            $(this).attr({
                'id': "txtRemark_" + delRow,
                'name': "txtRemark_" + delRow

            });

        });
        delRow = delRow + 1;
    });

});


function cloneRow() {

    $("select.select2-hidden-accessible").select2('destroy');
    rowId = rowId + 1;
    var row = document.getElementById("rowToClone"); // find row to copy
    var table = document.getElementById("tableToModify"); // find table to append to



    var clone = row.cloneNode(true); // copy children too
    clone.id = rowId; // change id or other attributes/contents    
    table.appendChild(clone); // add new row to end of table



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

        this.cells[0].innerText = i + 1;

    });


    $(".applyselect").select2();


}
var isFromAsset = false;
function SaveProjectLine() {

    if (checkValidationOnSubmitWithoutMessage('mandate') == true) {
        var lieitem = [];
        var isValid = true;


        $('#tableToModify tr').each(function (i) {
            $('#ssptxtReqBudget_' + i).hide();
            var procPRojectLine =
            {
                ProjectCode: $("#ddlProject_" + i).val(),
                ProjectSubLineItem: $("#ddlProjectLine_" + i).val(),
                ProjectLineDesc: $("#lblProjectLineDesc_" + i).text(),
                ProjectManager: $("#lblProjectManager_" + i).text(),
                Qty: $("#lblQuantity_" + i).text(),
                BudgetAmount: $("#lblBudget_" + i).text(),
                RequiredAmount: $("#txtReqBudget_" + i).val(),
                Remark: $("#txtRemark_" + i).val()
            }
            var budgetValue = Number(procPRojectLine.BudgetAmount);
            if (budgetValue > 0) {
                var budgetPer = (budgetValue * 25) / 100;
                var TotalBudgetValue = budgetValue + budgetPer;

                if (Number(procPRojectLine.RequiredAmount) > TotalBudgetValue) {
                    isValid = false;

                    // $('#txtReqBudget_' + i).classList.add("errorValidation");
                    var ctrl = document.getElementById("txtReqBudget_" + i);
                    ctrl.classList.add("errorValidation");
                }
            }
            lieitem.push(procPRojectLine);


        });
        const duplicateObjects = findDuplicateObjects(lieitem, 'ProjectSubLineItem');
        if (duplicateObjects.length > 0) {

            for (var n = 0; n < duplicateObjects.length; n++) {
                for (var l = 0; l < lieitem.length; l++) {
                    if (duplicateObjects[n].ProjectSubLineItem == lieitem[l].ProjectSubLineItem) {

                        var rows = document.getElementById('datatableProject').getElementsByTagName('tbody')[0].getElementsByTagName('tr');

                        // Loop through each row and remove highlighting
                        for (var o = 0; o < rows.length; o++) {
                            rows[o].classList.remove('highlighted');
                        }

                        // Highlight the row with the error
                        rows[l].classList.add('highlighted');
                    }
                }
            }
            FailToaster("Duplicate line item found, Please remove and submit!");

        }
        else {
            var obj = {
                IsResubmit: 3,
                Id: $("#hdnProcureRequestId").val(),
                ContractType: $("#ddlAgreement").val(),
                POC: $("#ddlPOC").val(),
                BudgetAmount: $("#lblTotal1").text(),
                RequiredAmount: $("#lblTotal2").text(),
                ProcureRequestProjectDetailList: lieitem,
                FromAsset: isFromAsset



            }
            if (isValid == true) {
                CommonAjaxMethod(virtualPath + 'ProcurementRequest/SaveProcurementRegistration', obj, 'POST', function (response) {
                    $("#hdnProcureRequestId").val(0);
                    window.location.reload();
                });
            }
            else {
                FailToaster("Your request is more than the budgeted amount!");
            }
        }



    }
}
function findDuplicateObjects(array, key) {
    let duplicates = {};
    let result = [];

    array.forEach(function (item) {
        let itemKey = item[key];
        if (duplicates[itemKey]) {
            if (duplicates[itemKey] === 1) {
                result.push(item);
            }
            duplicates[itemKey] += 1;
        } else {
            duplicates[itemKey] = 1;
        }
    });

    return result;
}
function AddNewRows() {
    btnrowactivity1(1);
}
function btnrowactivity1(from) {
    rowId = rowId + 1;

    $('.applyselect').select2("destroy");
    // $("#listBudgetSublineActivity_" + (parseInt(LastTRCount) + 1) + "__SubActivityirowId + 1d").empty();
    var $tableBody = $('#datatableProject').find("tbody"),
        $trLast = $tableBody.find("tr:last"),
        $trNew = $trLast.clone();
    // $trNew.find("td:last").html('<a onclick="DeleteRow(this)" class="remove"><i class="fas fa-window-close red-clr" aria-hidden="true"></i></a>')


    $trNew.find("span.Budget").each(function (i) {
        $(this).attr({
            'id': "sptxtReqBudget_" + (rowId)
        });

    });
    $trNew.find("span.ProjectLine").each(function (i) {
        $(this).attr({
            'id': "spddlProjectLine_" + rowId
        });

    });
    $trNew.find("span.Project").each(function (i) {
        $(this).attr({
            'id': "spddlProject_" + (rowId)
        });

    });
    $trNew.find("label.lblProjectLineDesc").each(function (i) {
        $(this).attr({
            'id': "lblProjectLineDesc_" + (rowId)
        });

    });
    $trNew.find("label.lblProjectLineId").each(function (i) {
        $(this).attr({
            'id': "lblProjectLineId_" + (rowId)
        });
        $(this).text('0')
    });


    $trNew.find("label.lblProjectManager").each(function (i) {
        $(this).attr({
            'id': "lblProjectManager_" + (rowId)
        });

    });

    $trNew.find("label.lblQuantity").each(function (i) {
        $(this).attr({
            'id': "lblQuantity_" + (rowId)
        });

    });

    $trNew.find("label.lblBudget").each(function (i) {
        $(this).attr({
            'id': "lblBudget_" + (rowId)
        });

    });


    $trNew.find("select.ddlProject").each(function (i) {
        $(this).attr({
            'id': "ddlProject_" + (rowId),
            'name': "ddlProject_" + (rowId)
        });

    });
    $trNew.find("select.ddlProjectLine").each(function (i) {
        $(this).attr({

            'id': "ddlProjectLine_" + (rowId),
            'name': "ddlProjectLine_" + (rowId)
        });

    });
    $trNew.find("input").each(function (i) {
        $(this).attr({

            'id': "txtReqBudget_" + (rowId),
            'name': "txtReqBudget_" + (rowId)
        });
        $(this).val('')
    });
    $trNew.find("textarea").each(function (i) {
        $(this).attr({
            'id': "txtRemark_" + (rowId),
            'name': "txtRemark_" + (rowId)

        });
        $(this).val('')
    });


    //$trNew.find("a.DataRemove").each(function (i) {
    //    $(this).attr({
    //        'id': "RemoveAnchor_" + (rowId)
    //    });

    //});
    //$trNew.find("a.DataWithdraw").each(function (i) {
    //    $(this).attr({
    //        'id': "WithdrawAnchor_" + (rowId)
    //    });

    //});
    //$trNew.find("span").each(function (i) {
    //    if ($(this).attr("data-valmsg-for")) {
    //        $(this).attr({
    //            'data-valmsg-for': function (_, name) { return name.replace('[' + LastTRCount + ']', '[' + (parseInt(LastTRCount) + 1) + ']'); }
    //        });
    //    }
    //    if ($(this).attr("for")) {
    //        $(this).attr({
    //            'for': function (_, name) { return name.replace('_' + LastTRCount + '_', '_' + (parseInt(LastTRCount) + 1) + '_'); }
    //        });
    //    }
    //});
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


        this.cells[0].innerText = i + 1;

    });
    // clearlist(parseInt(LastTRCount) + 1);

    $(".applyselect").select2();



}
//ActivityAmount
function ActivityAmount() {

    //var amount = obj.val();
    var Total = 0
    $(".AAmount").each(function () {
        var str1 = $(this).val();
        str1 = str1.replace(/,/g, '');
        var current = Number(str1);
        Total += parseFloat(current);
    });
    $("#total").text(NumberWithComma(Total));
    $("#total").val(Total);
}
function calculateTotal() {
    var Total = 0
    $(".AAmount").each(function () {
        var str1 = $(this).val();
        str1 = str1.replace(/,/g, '');
        var current = Number(str1);
        Total += parseFloat(current);

    });

    $("#lblTotal2").text(NumberWithComma(Total));
}
function calculateTotalText() {
    var Total = 0
    $(".AAmount").each(function () {
        var str1 = $(this).val();
        str1 = str1.replace(/,/g, '');
        var current = Number(str1);
        Total += parseFloat(current);

    });

    $("#lblTotal2").text(NumberWithComma(Total));
}
function calculateBudget() {
    var Total = 0
    $(".lblBudget").each(function () {
        var str1 = $(this).text();
        str1 = str1.replace(/,/g, '');
        var current = Number(str1);

        Total += parseFloat(current);

    });
    $("#lblTotal1").text(NumberWithComma(Total));
}
var RequestApprovalArray = [];
function SetProjectLineItem() {
    isFromAsset = true;
    $("#ddlAgreement").val('Purchase Order (PO)').trigger('change');

    $("#ddlAgreement").prop('disabled', true);
    $('#tableToModify tr').each(function (i) {
        if (i > 1)
            $(this).closest('tr').remove();

    });
    rowId = 0;
    RequestApprovalArray = [];
    var checkedArr = $("#datatableFillProject").find("input[type=checkbox]:checked").map(function () {
        return this.id;
    }).get();
    for (var i = 0; i < checkedArr.length; i++) {
        if (checkedArr[i].includes('chk')) {
            var ids = checkedArr[i].split('-');

            RequestApprovalArray.push(ids[2]);

        }
    }
    for (var j = 0; j < checkedArr.length - 1; j++) {
        btnrowactivity1();
    }
    CommonAjaxMethod(virtualPath + 'ProcurementRequest/BindProjectDetails', { id: 0, IsBindLine: 2, lineITems: RequestApprovalArray.join() }
        , 'GET', function (response) {
            var dataPending = response.data.data.Table;
            for (var k = 0; k < dataPending.length; k++) {
                var m = k;
                $('#' + 'lblProjectLineDesc_' + m).text(dataPending[k].ProjectLineDesc);
                $('#' + 'lblProjectManager_' + m).text(dataPending[k].ProjectManager);
                $('#' + 'lblQuantity_' + m).text(dataPending[k].Quantity);
                $('#' + 'lblBudget_' + m).text(dataPending[k].total_budget);

                $('#' + 'ddlProject_' + m).val(dataPending[k].ID).trigger('change');
                $('#' + 'ddlProjectLine_' + m).val(dataPending[k].SublineItem).trigger('change');

            }


        });





}
function ResetProjectList() {
    $("#hdnProcureRequestId").val('0');
    $('#tableToModify tr').each(function (i) {
        if (i > 1)
            $(this).closest('tr').remove();

    });
    $("#ddlAgreement").val('Select').trigger('change');
    $("#ddlPOC").val('Select').trigger('change');
    $("#lblTotal1").text('0');
    $("#lblTotal2").text('0');
    $("#ddlProject_0").val('Select').trigger('change');

    CommonAjaxMethod(virtualPath + 'ProcurementRequest/GetMaxReqNo', null, 'GET', function (response) {
        $('#ReqNo').val(response.data.data.Table[0].ReqNo);
    });
    var dt = new Date();
    var newDate = ChangeDateFormatToddMMYYY(dt);
    $('#ReqDate').val(newDate);

    $('#' + 'lblProjectLineId_0').text('0');
    $('#' + 'lblProjectLineDesc_0').text('');
    $('#' + 'lblProjectManager_0').text('');
    $('#' + 'lblQuantity_0').text('');
    $('#' + 'lblBudget_0').text('');
    $('#' + 'txtReqBudget_0').val('');
    $('#' + 'txtRemark_0').val('');
    var $ele = $('#' + 'ddlProjectLine_0');
    $ele.empty();
    $ele.append($('<option/>').val('Select').text('Select'));

    LoadMasterDropdown('ddlProjectCode',
        {
            ParentId: 0,
            masterTableType: 0,
            isMasterTableType: false,
            isManualTable: true,
            manualTable: ManaulTableEnum.Project,
            manualTableId: 0
        }, 'Select', false);
    BindProjectDetail(0, 1);
}
function BindProjectDetail(pid, isBindLine) {

    CommonAjaxMethod(virtualPath + 'ProcurementRequest/BindProjectDetails', { id: pid, IsBindLine: isBindLine }
        , 'GET', function (response) {
            var dataPending = response.data.data.Table;
            $('#datatableFillProject').DataTable({
                "processing": true, // for show progress bar           
                "destroy": true,
                "data": dataPending,
                "paging": false,
                "info": false,


                "columns": [



                    {
                        "orderable": false,
                        data: null, render: function (data, type, row) {

                            var strReturn = '<input type="checkbox" id="chk-' + row.ID + '-' + row.BudgetSublineId + '" class="selectedId sltchk" name="select" onchange="valueChanged()"><label for= "chk-' + row.ID + '-' + row.BudgetSublineId + '" class= "m-0" ></label>';

                            return strReturn;
                        }
                    },
                    { "data": "RowNum" },


                    { "data": "doc_no" },
                    { "data": "proj_name" },
                    { "data": "thematicarea_code" },
                    { "data": "Ledger" },
                    { "data": "Category" },
                    { "data": "SublineItem" }



                ]
            });
        });
}

function FillProjectLineGrid(ctrl) {

    BindProjectDetail(ctrl.value, 1)
}

function AddService() {
    $("#ddlAgreement").val('Select').trigger('change');
    $("#ddlAgreement").prop('disabled', false);
    //$("#hdnProcureRequestId").val('0');
    if ($("#hdnProcureRequestId").val() != 0) {
        $("#hdnProcureRequestId").val('0');
        $('#tableToModify tr').each(function (i) {
            if (i > 0)
                $(this).closest('tr').remove();

        });

        $("#ddlAgreement").val('Select').trigger('change');
        $("#ddlPOC").val('Select').trigger('change');
        $("#lblTotal1").text('0');
        $("#lblTotal2").text('0');
        $("#ddlProject_0").val('Select').trigger('change');

        CommonAjaxMethod(virtualPath + 'ProcurementRequest/GetMaxReqNo', null, 'GET', function (response) {
            $('#ReqNo').val(response.data.data.Table[0].ReqNo);
        });
        var dt = new Date();
        var newDate = ChangeDateFormatToddMMYYY(dt);
        $('#ReqDate').val(newDate);

        $('#' + 'lblProjectLineId_0').text('0');
        $('#' + 'lblProjectLineDesc_0').text('');
        $('#' + 'lblProjectManager_0').text('');
        $('#' + 'lblQuantity_0').text('');
        $('#' + 'lblBudget_0').text('');
        $('#' + 'txtReqBudget_0').val('');
        $('#' + 'txtRemark_0').val('');
        var $ele = $('#' + 'ddlProjectLine_0');
        $ele.empty();
        $ele.append($('<option/>').val('Select').text('Select'));
    }




    //ddlProject_0
    //ddlProjectLine_0
    //lblProjectLineDesc_0
    //lblProjectManager_0
    //lblQuantity_0
    //lblBudget_0
    //txtReqBudget_0
    //txtRemark_0

    //RequestApprovalArray = [];
    //$("#lblTotal1").text('0');
    //$("#lblTotal2").text('0');
}
function ReturnLI(row, from) {
    var actionStr = ""
    if (from == 1)//Pending
    {
        actionStr = '<a   data-toggle="modal" data-target="#reason" onclick="SetWithdrawData(this,1,' + row.Id + ')" title="Withdraw " class="btn-c  deleteRequest red-clr"><i class="fa fa-times"></i></a> <a class="btn-g viewRequest" onclick="LoadLineItemData(' + row.Id + ', ' + row.RequestId + ',1)" data-target="#avpr" data-toggle="modal" title="View"><i class="fas fa-ellipsis-h"></i></a>';
    }
    else if (from == 2)//Approved
    {
        actionStr = '<a class="btn-g viewRequest" onclick="LoadLineItemData(' + row.Id + ', ' + row.RequestId + ',2)" data-target="#pr-a" data-toggle="modal" title="View"><i class="fas fa-ellipsis-h"></i></a>';

    }
    else if (from == 3)//Rejected
    {
        actionStr = '<a class="btn-g viewRequest" onclick="LoadLineItemData(' + row.Id + ', ' + row.RequestId + ',3)" data-target="#pr-a" data-toggle="modal" title="View"><i class="fas fa-ellipsis-h"></i></a>';

    }

    else if (from == 4)//in process
    {
        var withButton = '<a   data-toggle="modal" data-target="#reason" onclick="SetWithdrawData(this,3,' + row.Id + ')" title="Withdraw " class="btn-c  deleteRequest red-clr"><i class="fa fa-times"></i></a>';
        //9	Pending at Module Admin	RFP Submit
        //10	Forwarded to Finance	RFP Approved Module Admin
        //11	Payment processed by Finance	Finance  approved
        if (row.StatusCode == 9 || row.StatusCode == 10 || row.StatusCode == 11 || row.StatusCode == 30) {
            actionStr = '<a class="btn-g viewRequest" href="RFPEntry?id=' + row.Id + '"  title="View"><i class="fas fa-ellipsis-h"></i></a>';

        }

        //14	Waiver request pending with ED	RFP ED Submit

        //16	Waiver request rejected	Ed Reject  RFP 		

        if (row.StatusCode == 14) {
            //Popup
            actionStr = '<a class="btn-g viewRequest" onclick="LoadLineItemData(' + row.Id + ', ' + row.RequestId + ',2)" data-target="#pr-a" data-toggle="modal" title="View"><i class="fas fa-ellipsis-h"></i></a>';

        }
        if (row.StatusCode == 16) {
            actionStr = '<a class="btn-g viewRequest" href="RFPEntry?id=' + row.Id + '"  title="View"><i class="fas fa-ellipsis-h"></i></a>';
        }

        //12	 RFP Is Live
        //15	Waiver request approved	ED Approved Qutation Entry
        //17	RFP No for Quotation RFP No
        //18	Draft saved	Quotation Entry Save by user
        //19	Pending with PC	Quotation Entry Submit by user
        //20	Approved by PC	Quotation Entry approved by Commetee member
        //21	Approval in Process	Aproved by one
        //24	With Authorised Signatory	Quotation Entry fwd by module admin to auth sign
        //25	Approved by Authorised Signatory	Auth sign approved
        //27	Draft saved	Module Admin Contract sign Save
        if (row.StatusCode == 12 || row.StatusCode == 15 || row.StatusCode == 17 || row.StatusCode == 18 || row.StatusCode == 19 || row.StatusCode == 20 || row.StatusCode == 21 || row.StatusCode == 24 || row.StatusCode == 25 || row.StatusCode == 27 || row.StatusCode == 35) {
            var moreInfo = "";
            if (row.StatusCode == 35 && row.Parent == 1) {
                if (row.ContractType == 'Consultant') {

                    actionStr = moreInfo + '<a class="btn-g viewRequest" href="UserAmendmentQuotationConsultant?id=' + row.Id + '"  title="View"><i class="fas fa-ellipsis-h"></i></a>';

                }

                else {
                    actionStr = moreInfo + '<a class="btn-g viewRequest" href="UserAmendmentQuotation?id=' + row.Id + '"  title="View"><i class="fas fa-ellipsis-h"></i></a>';

                }
            }
            else {
                if (row.StatusCode == 19 || row.StatusCode == 20 || row.StatusCode == 21 || row.StatusCode == 24 || row.StatusCode == 25 || row.StatusCode == 27 || row.StatusCode == 35) {
                    if (row.MessageSeen == 'Y')
                        moreInfo = '<a class="btn-c " onclick="AskQuery(' + row.Id + ',1)" data-toggle="modal" data-target="#mgs"><i class="fas fa-info-circle darkclr " data-toggle="tooltip" title="More Info"></i></a>';
                    else
                        moreInfo = '<a class="btn-c " onclick="AskQuery(' + row.Id + ',1)" data-toggle="modal" data-target="#mgs"><i class="fas fa-info-circle darkclrNewMsg " data-toggle="tooltip" title="More Info"></i></a>';

                }

                if (row.ContractType == 'Consultant') {
                    if (row.DataFrom == 1) {

                        actionStr = moreInfo + '<a class="btn-g viewRequest" href="QuotationEntryPendingConsultantRFP?id=' + row.Id + '"  title="View"><i class="fas fa-ellipsis-h"></i></a>';

                    }
                    else if (row.DataFrom == 2) {

                        actionStr = moreInfo + '<a class="btn-g viewRequest" href="QuotationEntryPendingConsultant?id=' + row.Id + '"  title="View"><i class="fas fa-ellipsis-h"></i></a>';

                    }
                }
                else if (row.ContractType == 'Sub-grant') {
                    if (row.DataFrom == 1) {
                        actionStr = moreInfo + '<a class="btn-g viewRequest" href="QuotationEntryPendingSubgrantRFP?id=' + row.Id + '"  title="View"><i class="fas fa-ellipsis-h"></i></a>';

                    }
                    else if (row.DataFrom == 2) {
                        actionStr = moreInfo + '<a class="btn-g viewRequest" href="QuotationEntryPendingSubgrant?id=' + row.Id + '"  title="View"><i class="fas fa-ellipsis-h"></i></a>';

                    }
                }
                else {
                    if (row.DataFrom == 1) {
                        actionStr = moreInfo + '<a class="btn-g viewRequest" href="QuotationEntryPendingRFP?id=' + row.Id + '"  title="View"><i class="fas fa-ellipsis-h"></i></a>';

                    }
                    else if (row.DataFrom == 2) {
                        actionStr = moreInfo + '<a class="btn-g viewRequest" href="QuotationEntryPending?id=' + row.Id + '"  title="View"><i class="fas fa-ellipsis-h"></i></a>';

                    }

                }
            }

        }

        if (row.IsDownload == true) {
            withButton = "";
        }
        actionStr = withButton + actionStr;

    }
    else if (from == 5)//Live
    {
        //  actionStr = '<a class="btn-g viewRequest" onclick="LoadLineItemData(' + row.Id + ', ' + row.RequestId + ',3)" data-target="#avpr" data-toggle="modal" title="View"><i class="fas fa-ellipsis-h"></i></a>';


        if (row.ContractType == 'Consultant') {
            if (row.IsAmendment == 'Y') {
                actionStr = '<a class="btn-g viewRequest"   data-toggle="dropdown" title="View"><i class="fas fa-ellipsis-h"></i></a>' +
                    '<ul class="dropdown-menu  dw-listdtl">' +

                    ' <li><a class="dropdown-item" href="#" onclick="GetQuotationContract1(' + row.Id + ', ' + row.RequestId + ',5,1)" data-toggle="modal" data-target="#dod1"><i class="fas fa-list  dw-icon"></i> Submit Invoice/Deliverables</a></li>' +

                    '<li><a class="dropdown-item" href="#" onclick="GetContractDetails(' + row.Id + ', ' + row.RequestId + ',5)" data-toggle="modal" data-target="#cod"><i class="fas fa-info-circle  dw-icon"></i> Signed PO/Agreement ' +
                    '</a></li>' +
                    '<li><a class="dropdown-item" href="#" data-toggle="modal" data-target="#pd2" onclick="GetPaymentDetails2(' + row.Id + ')"><i class="fas fa-rupee-sign  dw-icon"></i> View Payment Details</a></li>' +

                    '<li><a class="dropdown-item" href="#" data-toggle="modal" data-target="#cnl"><i class="fas fa-times dw-icon"></i> Request For Cancellation</a></li>' +
                    '<li><a class="dropdown-item" href="#" onclick="ViewQutationData(' + row.Id + ', ' + row.DataFrom + ',1)"><i class="fas fa-eye  dw-icon"></i>View PO/Contract Details</a></li>' +
                    '</ul>';
            }
            else {
                actionStr = '<a class="btn-g viewRequest"   data-toggle="dropdown" title="View"><i class="fas fa-ellipsis-h"></i></a>' +
                    '<ul class="dropdown-menu  dw-listdtl">' +

                    ' <li><a class="dropdown-item" href="#" onclick="GetQuotationContract1(' + row.Id + ', ' + row.RequestId + ',5,2)" data-toggle="modal" data-target="#dod1"><i class="fas fa-list  dw-icon"></i> Submit Invoice/Deliverables</a></li>' +

                    '<li><a class="dropdown-item" href="#" onclick="GetContractDetails(' + row.Id + ', ' + row.RequestId + ',5)" data-toggle="modal" data-target="#cod"><i class="fas fa-info-circle  dw-icon"></i> Signed PO/Agreement ' +
                    '</a></li>' +
                    '<li><a class="dropdown-item" href="#" data-toggle="modal" data-target="#pd2" onclick="GetPaymentDetails2(' + row.Id + ')"><i class="fas fa-rupee-sign  dw-icon"></i> View Payment Details</a></li>' +
                    '<li><a class="dropdown-item" href="#" onclick="ViewQutationAmendData(' + row.Id + ', ' + row.DataFrom + ',1)" ><i class="fas fa-edit dw-icon"></i> Request For Amendment</a></li>' +
                    '<li><a class="dropdown-item" href="#" data-toggle="modal" data-target="#cnl"><i class="fas fa-times dw-icon"></i> Request For Cancellation</a></li>' +
                    '<li><a class="dropdown-item" href="#" onclick="ViewQutationData(' + row.Id + ', ' + row.DataFrom + ',1)"><i class="fas fa-eye  dw-icon"></i>View PO/Contract Details</a></li>' +
                    '</ul>';
            }


        }
        else if (row.ContractType == 'Sub-grant') {

            actionStr = '<a class="btn-g viewRequest"   data-toggle="dropdown" title="View"><i class="fas fa-ellipsis-h"></i></a>' +

                '<ul class="dropdown-menu  dw-listdtl">' +

                ' <li><a class="dropdown-item" href="#" onclick="GetQuotationEntrySubgrant(' + row.Id + ', ' + row.RequestId + ',5)" data-toggle="modal" data-target="#dodSubGrant"><i class="fas fa-list  dw-icon"></i> Submit Invoice/Deliverables</a></li>' +
                '<li><a class="dropdown-item" href="#" onclick="GetContractDetails(' + row.Id + ', ' + row.RequestId + ',5)" data-toggle="modal" data-target="#cod"><i class="fas fa-info-circle  dw-icon"></i> Signed PO/Agreement ' +
                '</a></li>' +
                '<li><a class="dropdown-item" href="#" data-toggle="modal" data-target="#pd3" onclick="GetPaymentDetails3(' + row.Id + ')"><i class="fas fa-rupee-sign  dw-icon"></i> View Payment Details</a></li>' +
                '<li><a class="dropdown-item" href="#" onclick="ViewQutationAmendData(' + row.Id + ', ' + row.DataFrom + ',2)" ><i class="fas fa-edit dw-icon"></i> Request For Amendment</a></li>' +
                '<li><a class="dropdown-item" href="#" data-toggle="modal" data-target="#cnl"><i class="fas fa-times dw-icon"></i> Request For Cancellation</a></li>' +
                '<li><a class="dropdown-item" href="#" onclick="ViewQutationData(' + row.Id + ', ' + row.DataFrom + ',2)" href="#"><i class="fas fa-eye  dw-icon"></i>View PO/Contract Details</a></li>' +
                '</ul>';

        }
        else {
            if (row.IsAmendment == 'Y') {
                actionStr = '<a class="btn-g viewRequest"   data-toggle="dropdown" title="View"><i class="fas fa-ellipsis-h"></i></a>' +

                    '<ul class="dropdown-menu  dw-listdtl">' +

                    ' <li><a class="dropdown-item" href="#" onclick="GetQuotationEntryDetailsofDeliverable(' + row.Id + ', ' + row.RequestId + ',5,1)" data-toggle="modal" data-target="#dod"><i class="fas fa-list  dw-icon"></i> Submit Invoice/Deliverables</a></li>' +
                    '<li><a class="dropdown-item" href="#" onclick="GetContractDetails(' + row.Id + ', ' + row.RequestId + ',5)" data-toggle="modal" data-target="#cod"><i class="fas fa-info-circle  dw-icon"></i> Signed PO/Agreement ' +
                    '</a></li>' +
                    '<li><a class="dropdown-item" href="#" data-toggle="modal" data-target="#pd1" onclick="GetPaymentDetails1(' + row.Id + ')" ><i class="fas fa-rupee-sign  dw-icon"></i> View Payment Details</a></li>' +

                    '<li><a class="dropdown-item" href="#" data-toggle="modal" data-target="#cnl"><i class="fas fa-times dw-icon"></i> Request For Cancellation</a></li>' +
                    '<li><a class="dropdown-item" href="#" onclick="ViewQutationData(' + row.Id + ', ' + row.DataFrom + ',3 )" href="#"><i class="fas fa-eye  dw-icon"></i>View PO/Contract Details</a></li>' +
                    '</ul>';
            }
            else {
                actionStr = '<a class="btn-g viewRequest"   data-toggle="dropdown" title="View"><i class="fas fa-ellipsis-h"></i></a>' +

                    '<ul class="dropdown-menu  dw-listdtl">' +

                    ' <li><a class="dropdown-item" href="#" onclick="GetQuotationEntryDetailsofDeliverable(' + row.Id + ', ' + row.RequestId + ',5,2)" data-toggle="modal" data-target="#dod"><i class="fas fa-list  dw-icon"></i> Submit Invoice/Deliverables</a></li>' +
                    '<li><a class="dropdown-item" href="#" onclick="GetContractDetails(' + row.Id + ', ' + row.RequestId + ',5)" data-toggle="modal" data-target="#cod"><i class="fas fa-info-circle  dw-icon"></i> Signed PO/Agreement ' +
                    '</a></li>' +
                    '<li><a class="dropdown-item" href="#" data-toggle="modal" data-target="#pd1" onclick="GetPaymentDetails1(' + row.Id + ')" ><i class="fas fa-rupee-sign  dw-icon"></i> View Payment Details</a></li>' +
                    '<li><a class="dropdown-item" href="#" onclick="ViewQutationAmendData(' + row.Id + ', ' + row.DataFrom + ',3)" ><i class="fas fa-edit dw-icon"></i> Request For Amendment</a></li>' +
                    '<li><a class="dropdown-item" href="#" data-toggle="modal" data-target="#cnl"><i class="fas fa-times dw-icon"></i> Request For Cancellation</a></li>' +
                    '<li><a class="dropdown-item" href="#" onclick="ViewQutationData(' + row.Id + ', ' + row.DataFrom + ',3 )" href="#"><i class="fas fa-eye  dw-icon"></i>View PO/Contract Details</a></li>' +
                    '</ul>';
            }


        }

    }
    else if (from == 6)//Close
    {
        var wStatus = false;
        var moreInfo = "";
        
        if (row.StatusCode == 34) {
            row.StatusCode = row.PreStatus
            wStatus = true;
            moreInfo = '<a class="btn-c " onclick="ShowWdrData(' + row.Id + ')" data-toggle="modal" data-target="#wdReason"><i class="fas fa-info-circle darkclr " data-toggle="tooltip" title="Withdrawn Reason"></i></a><label   style="display:none;" id="hdnWdrReason_' + row.Id + '" >' + row.WdrReason + '</label >';
        }
        //9
        //10
        //11
        //12
        //13
        //14
        if (row.StatusCode == 33) {
            if (row.ContractType == 'Consultant') {
                if (row.IsAmendment == 'Y') {
                    actionStr = '<a class="btn-g viewRequest"   data-toggle="dropdown" title="View"><i class="fas fa-ellipsis-h"></i></a>' +
                        '<ul class="dropdown-menu  dw-listdtl">' +

                        ' <li><a class="dropdown-item" href="#" onclick="GetQuotationContract1(' + row.Id + ', ' + row.RequestId + ',5,1)" data-toggle="modal" data-target="#dod1"><i class="fas fa-list  dw-icon"></i> Submit Invoice/Deliverables</a></li>' +

                        '<li><a class="dropdown-item" href="#" onclick="GetContractDetails(' + row.Id + ', ' + row.RequestId + ',5)" data-toggle="modal" data-target="#cod"><i class="fas fa-info-circle  dw-icon"></i> Signed PO/Agreement ' +
                        '</a></li>' +
                        '<li><a class="dropdown-item" href="#" data-toggle="modal" data-target="#pd2" onclick="GetPaymentDetails2(' + row.Id + ')"><i class="fas fa-rupee-sign  dw-icon"></i> View Payment Details</a></li>' +


                        '<li><a class="dropdown-item" href="#" onclick="ViewQutationData(' + row.Id + ', ' + row.DataFrom + ',1)"><i class="fas fa-eye  dw-icon"></i>View PO/Contract Details</a></li>' +
                        '</ul>';
                }
                else {
                    actionStr = '<a class="btn-g viewRequest"   data-toggle="dropdown" title="View"><i class="fas fa-ellipsis-h"></i></a>' +
                        '<ul class="dropdown-menu  dw-listdtl">' +

                        ' <li><a class="dropdown-item" href="#" onclick="GetQuotationContract1(' + row.Id + ', ' + row.RequestId + ',5,2)" data-toggle="modal" data-target="#dod1"><i class="fas fa-list  dw-icon"></i> Submit Invoice/Deliverables</a></li>' +

                        '<li><a class="dropdown-item" href="#" onclick="GetContractDetails(' + row.Id + ', ' + row.RequestId + ',5)" data-toggle="modal" data-target="#cod"><i class="fas fa-info-circle  dw-icon"></i> Signed PO/Agreement ' +
                        '</a></li>' +
                        '<li><a class="dropdown-item" href="#" data-toggle="modal" data-target="#pd2" onclick="GetPaymentDetails2(' + row.Id + ')"><i class="fas fa-rupee-sign  dw-icon"></i> View Payment Details</a></li>' +

                        '<li><a class="dropdown-item" href="#" onclick="ViewQutationData(' + row.Id + ', ' + row.DataFrom + ',1)"><i class="fas fa-eye  dw-icon"></i>View PO/Contract Details</a></li>' +
                        '</ul>';
                }


            }
            else if (row.ContractType == 'Sub-grant') {

                actionStr = '<a class="btn-g viewRequest"   data-toggle="dropdown" title="View"><i class="fas fa-ellipsis-h"></i></a>' +

                    '<ul class="dropdown-menu  dw-listdtl">' +

                    ' <li><a class="dropdown-item" href="#" onclick="GetQuotationEntrySubgrant(' + row.Id + ', ' + row.RequestId + ',5)" data-toggle="modal" data-target="#dodSubGrant"><i class="fas fa-list  dw-icon"></i> Submit Invoice/Deliverables</a></li>' +
                    '<li><a class="dropdown-item" href="#" onclick="GetContractDetails(' + row.Id + ', ' + row.RequestId + ',5)" data-toggle="modal" data-target="#cod"><i class="fas fa-info-circle  dw-icon"></i> Signed PO/Agreement ' +
                    '</a></li>' +
                    '<li><a class="dropdown-item" href="#" data-toggle="modal" data-target="#pd3" onclick="GetPaymentDetails3(' + row.Id + ')"><i class="fas fa-rupee-sign  dw-icon"></i> View Payment Details</a></li>' +

                    '<li><a class="dropdown-item" href="#" onclick="ViewQutationData(' + row.Id + ', ' + row.DataFrom + ',2)" href="#"><i class="fas fa-eye  dw-icon"></i>View PO/Contract Details</a></li>' +
                    '</ul>';

            }
            else {
                if (row.IsAmendment == 'Y') {
                    actionStr = '<a class="btn-g viewRequest"   data-toggle="dropdown" title="View"><i class="fas fa-ellipsis-h"></i></a>' +

                        '<ul class="dropdown-menu  dw-listdtl">' +

                        ' <li><a class="dropdown-item" href="#" onclick="GetQuotationEntryDetailsofDeliverable(' + row.Id + ', ' + row.RequestId + ',5,1)" data-toggle="modal" data-target="#dod"><i class="fas fa-list  dw-icon"></i> Submit Invoice/Deliverables</a></li>' +
                        '<li><a class="dropdown-item" href="#" onclick="GetContractDetails(' + row.Id + ', ' + row.RequestId + ',5)" data-toggle="modal" data-target="#cod"><i class="fas fa-info-circle  dw-icon"></i> Signed PO/Agreement ' +
                        '</a></li>' +
                        '<li><a class="dropdown-item" href="#" data-toggle="modal" data-target="#pd1" onclick="GetPaymentDetails1(' + row.Id + ')" ><i class="fas fa-rupee-sign  dw-icon"></i> View Payment Details</a></li>' +


                        '<li><a class="dropdown-item" href="#" onclick="ViewQutationData(' + row.Id + ', ' + row.DataFrom + ',3 )" href="#"><i class="fas fa-eye  dw-icon"></i>View PO/Contract Details</a></li>' +
                        '</ul>';
                }
                else {
                    actionStr = '<a class="btn-g viewRequest"   data-toggle="dropdown" title="View"><i class="fas fa-ellipsis-h"></i></a>' +

                        '<ul class="dropdown-menu  dw-listdtl">' +

                        ' <li><a class="dropdown-item" href="#" onclick="GetQuotationEntryDetailsofDeliverable(' + row.Id + ', ' + row.RequestId + ',5,2)" data-toggle="modal" data-target="#dod"><i class="fas fa-list  dw-icon"></i> Submit Invoice/Deliverables</a></li>' +
                        '<li><a class="dropdown-item" href="#" onclick="GetContractDetails(' + row.Id + ', ' + row.RequestId + ',5)" data-toggle="modal" data-target="#cod"><i class="fas fa-info-circle  dw-icon"></i> Signed PO/Agreement ' +
                        '</a></li>' +
                        '<li><a class="dropdown-item" href="#" data-toggle="modal" data-target="#pd1" onclick="GetPaymentDetails1(' + row.Id + ')" ><i class="fas fa-rupee-sign  dw-icon"></i> View Payment Details</a></li>' +

                        '<li><a class="dropdown-item" href="#" onclick="ViewQutationData(' + row.Id + ', ' + row.DataFrom + ',3 )" href="#"><i class="fas fa-eye  dw-icon"></i>View PO/Contract Details</a></li>' +
                        '</ul>';
                }


            }
        }
        else if (row.StatusCode == 9 || row.StatusCode == 10 || row.StatusCode == 11 || row.StatusCode == 12 || row.StatusCode == 13 || row.StatusCode == 14) {

            if (row.StatusCode == 12) {
                actionStr = '<a class="btn-g viewRequest" href="RPFProcesslive?id=' + row.Id + '"  title="View"><i class="fas fa-ellipsis-h"></i></a>';
            }
            else {

                actionStr = '<a class="btn-g viewRequest" href="RFPEntry?id=' + row.Id + '"  title="View"><i class="fas fa-ellipsis-h"></i></a>';
            }
        }
        else {
            if (row.ContractType == 'Consultant') {
                if (row.DataFrom == 1) {

                    actionStr = '<a class="btn-g viewRequest"  href="QuotationEntryPendingConsultantRFP?id=' + row.Id + '" title="View"><i class="fas fa-ellipsis-h"></i></a>';

                }
                else if (row.DataFrom == 2) {

                    actionStr = '<a class="btn-g viewRequest"  href="QuotationEntryPendingConsultant?id=' + row.Id + '" title="View"><i class="fas fa-ellipsis-h"></i></a>';

                }
            }
            else if (row.ContractType == 'Sub-grant') {
                if (row.DataFrom == 1) {

                    actionStr = '<a class="btn-g viewRequest"  href="QuotationEntryPendingSubgrantRFP?id=' + row.Id + '" title="View"><i class="fas fa-ellipsis-h"></i></a>';


                }
                else if (row.DataFrom == 2) {
                    actionStr = '<a class="btn-g viewRequest"  href="QuotationEntryPendingSubgrant?id=' + row.Id + '" title="View"><i class="fas fa-ellipsis-h"></i></a>';

                }
            }
            else {
                if (row.DataFrom == 1) {
                    actionStr = '<a class="btn-g viewRequest"  href="QuotationEntryPendingRFP?id=' + row.Id + '" title="View"><i class="fas fa-ellipsis-h"></i></a>';

                }
                else if (row.DataFrom == 2) {
                    actionStr = '<a class="btn-g viewRequest"  href="QuotationEntryPending?id=' + row.Id + '" title="View"><i class="fas fa-ellipsis-h"></i></a>';

                }

            }
        }


        if (wStatus == true) {
            actionStr = moreInfo + actionStr;
        }

    }

   
    return '<li>' +
        '<div class="row d-flex" >' +
        '<div class="col-sm-4 align-self-center">' +
        '<div class="left-dtl-r">' +
        '<small class="lgt-text">Req#' + row.Req_No +


        '<span>|</span> <i class="fas fa-rupee-sign"></i>' + row.RequiredAmount +
        '</small>' +
        '</div>' +
        '</div>' +
        '<div class="col-sm-5 align-self-start">' +
        '<div class="cntr-dtl-r ">' +

        '<div class="cntr-dtl-r w-177">' +
        '<strong>' + row.ProjectCode + '<br >' + row.ContractType +
        '</strong>' +

        '</div>' +
        '<div class="cntr-dtl-pst"> <strong class="trvlstatusclr">' + row.Status + '</strong> </div>' +
        '</div>' +
        '</div>' +
        '<div class="col-sm-3 align-self-center mb-flwdt">' +
        '<div class="right-dtl-r pt-0">' + actionStr +
        '</div>' +
        '</div>' +
        '</div>' +
        '</li >';

}
function ShowWdrData(d)
{
    $("#txtWReason").text($("#hdnWdrReason_" + d).text());
    
}
function BindProcureRequest() {
    CommonAjaxMethod(virtualPath + 'ProcurementRequest/BindProcureRequest', null, 'GET', function (response) {
        var dataPending = response.data.data.Table;//Pending
        var dataArroved = response.data.data.Table1;//Approved
        var dataRejected = response.data.data.Table2;//Rejected
        var dataProcess = response.data.data.Table3;//In process
        var dataLive = response.data.data.Table4;//Live
        var dataClose = response.data.data.Table5;//Closed
        for (var i = 0; i < dataPending.length; i++) {
            var liData = "";
            if (dataPending[i].StatusCode != "1") {
                liData = ReturnLI(dataPending[i], 2);
            }
            else {
                liData = ReturnLI(dataPending[i], 1);
            }


            $("#ulPMTabPending").append(liData);
        }

        for (var i = 0; i < dataArroved.length; i++) {
            var liData = ReturnLI(dataArroved[i], 2);
            $("#ulPMTabApproved").append(liData);
        }

        for (var i = 0; i < dataRejected.length; i++) {
            var liData = ReturnLI(dataRejected[i], 3);
            $("#ulPMTabRejected").append(liData);
        }

        for (var i = 0; i < dataProcess.length; i++) {
            var liData = ReturnLI(dataProcess[i], 4);
            $("#ulPCOpenRequest").append(liData);
        }
        for (var i = 0; i < dataLive.length; i++) {
            var liData = ReturnLI(dataLive[i], 5);
            $("#ulPCLive").append(liData);
        }
        for (var i = 0; i < dataClose.length; i++) {
            var liData = ReturnLI(dataClose[i], 6);
            $("#ulPCClosed").append(liData);
        }

    });
}
function GetPaymentDetails1(id) {

    CommonAjaxMethod(virtualPath + 'ProcurementRequest/BindProjectDetails', { id: id, IsBindLine: 3 }
        , 'GET', function (response) {
            var data4 = response.data.data.Table4;
            var array = data4;
            $('#tblpd1').DataTable({
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
                                ' <label style="display:none;"  id="hdnUploadActualFileName_' + row.Id + '">' + row.InvoiceAttachmentActualName + '</label>' +
                                '<a id="ancUploadActualFileName_' + row.Id + '" href="" onclick="DownloadFileQuotation(this)">Download <i class="fas fa-download float-right" data-toggle="tooltip" title="Download"></i></a>' +
                                ' <label  style="display:none;" id="hdnUploadNewFileName_' + row.Id + '">' + row.InvoiceAttachmentNewName + '</label>';



                        }
                    },

                    {
                        "orderable": false,
                        data: null, render: function (data, type, row) {

                            if (row.InvoiceAttachmentActualNameD != undefined) {
                                return '<label   style="display:none;" id="hdnUploadFileUrlD_' + row.Id + '" >' + row.InvoiceAttachmentURLD + '</label >' +
                                    ' <label style="display:none;" onclick="DownloadFileQuotationD(this)"  id="hdnUploadActualFileNameD_' + row.Id + '">' + row.InvoiceAttachmentActualNameD + '</label>' +
                                    '<a id="ancUploadActualFileNameD_' + row.Id + '" href="" onclick="DownloadFileQuotationD(this)">Download <i class="fas fa-download float-right" data-toggle="tooltip" title="Download"></i></a>' +
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

                            var PaidDate = row.PaidDate != null || row.PaidDate != "" ? ChangeDateFormatToddMMYYY(row.PaidDate) : "";
                            if (PaidDate == "01-01-1900") {
                                PaidDate = "";
                            }
                            return '<label>' + PaidDate + '</label>';


                        }
                    },
                    {
                        "orderable": false,
                        "className": 'text-right',
                        data: null, render: function (data, type, row) {

                            return '<label class="text-right">' + NumberWithComma(row.PaidAmount) + '</label>';


                        }
                    },
                    {
                        "orderable": false,
                        "className": 'text-right',
                        data: null, render: function (data, type, row) {

                            return '<label class="text-right">' + NumberWithComma(row.TDS) + '</label>';
                        }
                    },
                    { "data": "Remark" },
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
                            return '<label>' + fStatus + '</label>';


                        }
                    }
                ]
            });
        });
}
function GetPaymentDetails2(id) {

    CommonAjaxMethod(virtualPath + 'ProcurementRequest/BindProjectDetails', { id: id, IsBindLine: 3 }
        , 'GET', function (response) {
            var data4 = response.data.data.Table5;
            var data5 = response.data.data.Table6;
            BindPaymentDetails1(data4);
            BindPaymentDetails2(data5);
        });
}

function BindPaymentDetails1(array) {
    $('#tblpd2').DataTable({
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
                        ' <label style="display:none;"   id="hdnUploadActualFileName_' + row.Id + '">' + row.InvoiceAttachmentActualName + '</label>' +
                        '<a id="ancUploadActualFileName_' + row.Id + '" href="" onclick="DownloadFileQuotation(this)">Download <i class="fas fa-download float-right" data-toggle="tooltip" title="Download"></i></a>' +
                        ' <label  style="display:none;" id="hdnUploadNewFileName_' + row.Id + '">' + row.InvoiceAttachmentNewName + '</label>';



                }
            },

            {
                "orderable": false,
                data: null, render: function (data, type, row) {

                    if (row.InvoiceAttachmentActualNameD != undefined) {
                        return '<label   style="display:none;" id="hdnUploadFileUrlD_' + row.Id + '" >' + row.InvoiceAttachmentURLD + '</label >' +
                            ' <label style="display:none;" id="hdnUploadActualFileNameD_' + row.Id + '">' + row.InvoiceAttachmentActualNameD + '</label>' +
                            '<a id="ancUploadActualFileNameD_' + row.Id + '" href="" onclick="DownloadFileQuotationD(this)">Download <i class="fas fa-download float-right" data-toggle="tooltip" title="Download"></i></a>' +
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

                    var PaidDate = row.PaidDate != null || row.PaidDate != "" ? ChangeDateFormatToddMMYYY(row.PaidDate) : "";
                    if (PaidDate == "01-01-1900") {
                        PaidDate = "";
                    }
                    return '<label>' + PaidDate + '</label>';


                }
            },
            {
                "orderable": false,
                "className": 'text-right',
                data: null, render: function (data, type, row) {

                    return '<label class="text-right">' + NumberWithComma(row.PaidAmount) + '</label>';


                }
            },
            {
                "orderable": false,
                "className": 'text-right',
                data: null, render: function (data, type, row) {

                    return '<label class="text-right">' + NumberWithComma(row.TDS) + '</label>';
                }
            },
            { "data": "Remark" },
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
                    return '<label>' + fStatus + '</label>';


                }
            }
        ]
    });

}
function BindPaymentDetails2(array) {

    $('#tblpd3').DataTable({
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
                        ' <label style="display:none;"  id="hdnUploadActualFileName_' + row.Id + '">' + row.InvoiceAttachmentActualName + '</label>' +
                        '<a id="ancUploadActualFileName_' + row.Id + '" href="" onclick="DownloadFileQuotation(this)">Download <i class="fas fa-download float-right" data-toggle="tooltip" title="Download"></i></a>' +
                        ' <label  style="display:none;" id="hdnUploadNewFileName_' + row.Id + '">' + row.InvoiceAttachmentNewName + '</label>';



                }
            },

            {
                "orderable": false,
                data: null, render: function (data, type, row) {

                    if (row.InvoiceAttachmentActualNameD != undefined) {
                        return '<label   style="display:none;" id="hdnUploadFileUrlD_' + row.Id + '" >' + row.InvoiceAttachmentURLD + '</label >' +
                            ' <label  style="display:none;"  id="hdnUploadActualFileNameD_' + row.Id + '">' + row.InvoiceAttachmentActualNameD + '</label>' +
                            '<a id="ancUploadActualFileNameD_' + row.Id + '" href="" onclick="DownloadFileQuotationD(this)">Download <i class="fas fa-download float-right" data-toggle="tooltip" title="Download"></i></a>' +
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

                    var PaidDate = row.PaidDate != null || row.PaidDate != "" ? ChangeDateFormatToddMMYYY(row.PaidDate) : "";
                    if (PaidDate == "01-01-1900") {
                        PaidDate = "";
                    }
                    return '<label>' + PaidDate + '</label>';


                }
            },
            {
                "orderable": false,
                "className": 'text-right',
                data: null, render: function (data, type, row) {

                    return '<label class="text-right">' + NumberWithComma(row.PaidAmount) + '</label>';


                }
            },
            {
                "orderable": false,
                "className": 'text-right',
                data: null, render: function (data, type, row) {

                    return '<label class="text-right">' + NumberWithComma(row.TDS) + '</label>';
                }
            },
            { "data": "Remark" },
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
                    return '<label>' + fStatus + '</label>';


                }
            }
        ]
    });
}
function GetPaymentDetails3(id) {

    CommonAjaxMethod(virtualPath + 'ProcurementRequest/BindProjectDetails', { id: id, IsBindLine: 3 }
        , 'GET', function (response) {
            var data4 = response.data.data.Table7;
            var array = data4;
            $('#tblpd4').DataTable({
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
                                ' <label style="display:none;"  id="hdnUploadActualFileName_' + row.Id + '">' + row.InvoiceAttachmentActualName + '</label>' +
                                '<a id="ancUploadActualFileName_' + row.Id + '" href="" onclick="DownloadFileQuotation(this)">Download <i class="fas fa-download float-right" data-toggle="tooltip" title="Download"></i></a>' +
                                ' <label  style="display:none;" id="hdnUploadNewFileName_' + row.Id + '">' + row.InvoiceAttachmentNewName + '</label>';



                        }
                    },

                    {
                        "orderable": false,
                        data: null, render: function (data, type, row) {

                            if (row.InvoiceAttachmentActualNameD != undefined) {
                                return '<label   style="display:none;" id="hdnUploadFileUrlD_' + row.Id + '" >' + row.InvoiceAttachmentURLD + '</label >' +
                                    ' <label style="display:none;"  id="hdnUploadActualFileNameD_' + row.Id + '">' + row.InvoiceAttachmentActualNameD + '</label>' +
                                    '<a id="ancUploadActualFileNameD_' + row.Id + '" href="" onclick="DownloadFileQuotationD(this)">Download <i class="fas fa-download float-right" data-toggle="tooltip" title="Download"></i></a>' +
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

                            var PaidDate = row.PaidDate != null || row.PaidDate != "" ? ChangeDateFormatToddMMYYY(row.PaidDate) : "";
                            if (PaidDate == "01-01-1900") {
                                PaidDate = "";
                            }
                            return '<label>' + PaidDate + '</label>';


                        }
                    },
                    {
                        "orderable": false,
                        "className": 'text-right',
                        data: null, render: function (data, type, row) {

                            return '<label class="text-right">' + NumberWithComma(row.PaidAmount) + '</label>';


                        }
                    },
                    {
                        "orderable": false,
                        "className": 'text-right',
                        data: null, render: function (data, type, row) {

                            return '<label class="text-right">' + NumberWithComma(row.TDS) + '</label>';
                        }
                    },
                    { "data": "Remark" },
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
                            return '<label>' + fStatus + '</label>';


                        }
                    }
                ]
            });
        });
}
function ViewQutationData(id, datafrom, type) {
    var url = "";
    if (type == 1) {
        if (datafrom == 1) {
            url = "/Procurement/UserModuleAdminQuotationConsultantRFP?id=" + id;
            window.location.href = url;
        }
        else if (datafrom == 2) {
            url = "/Procurement/UserModuleAdminQuotationConsultant?id=" + id;
            window.location.href = url;
        }
    }
    else if (type == 2) {
        if (datafrom == 1) {
            url = "/Procurement/UserModuleAdminQuotationSubgrantRFP?id=" + id;
            window.location.href = url;

        }
        else if (datafrom == 2) {
            url = "/Procurement/UserModuleAdminQuotationSubgrant?id=" + id;
            window.location.href = url;
        }
    }
    else {
        if (datafrom == 1) {
            url = "/Procurement/UserModuleAdminQuotationRFP?id=" + id;
            window.location.href = url;
        }
        else if (datafrom == 2) {
            url = "/Procurement/UserModuleAdminQuotation?id=" + id;
            window.location.href = url;
        }

    }
}
function GetContractDetails(id, requestId, from) {
    CommonAjaxMethod(virtualPath + 'ProcurementRequest/BindProjectDetails', { id: id, IsBindLine: 3 }
        , 'GET', function (response) {
            var data4 = response.data.data.Table3;
            BindContract(data4);
        });
}
function GetQuotationEntryDetailsofDeliverable(id, requestId, from, isAmend) {
    CommonAjaxMethod(virtualPath + 'ProcurementRequest/BindProjectDetails', { id: id, IsBindLine: 3 }
        , 'GET', function (response) {
            var data4 = response.data.data.Table4;

            $('#hdnProcureId').val(id);
            Bindtbldod(data4, isAmend);

        });
}
function GetQuotationContract1(id, requestId, from, isAmend) {
    CommonAjaxMethod(virtualPath + 'ProcurementRequest/BindProjectDetails', { id: id, IsBindLine: 3 }
        , 'GET', function (response) {
            $('#hdnProcureId').val(id);
            var data4 = response.data.data.Table5;
            var data5 = response.data.data.Table6;
            Bindtbldod1(data4, isAmend);
            Bindtbldod2(data5, isAmend);
        });


}
function GetQuotationContract2(id, requestId, from) {
    CommonAjaxMethod(virtualPath + 'ProcurementRequest/BindProjectDetails', { id: id, IsBindLine: 3 }
        , 'GET', function (response) {
            $('#hdnProcureId').val(id);
            var data4 = response.data.data.Table5;
            Bindtbldod(data4);
        });
}

function GetQuotationEntrySubgrant(id, requestId, from) {
    CommonAjaxMethod(virtualPath + 'ProcurementRequest/BindProjectDetails', { id: id, IsBindLine: 3 }
        , 'GET', function (response) {
            $('#hdnProcureId').val(id);
            var data4 = response.data.data.Table7;
            Bindtbldod3(data4);
        });
}

function BindContract(publish) {

    $('#tblUploadSignedDocuments').DataTable({
        "processing": true, // for show progress bar           
        "destroy": true,
        "data": publish,
        "paging": false,
        "info": false,
        "columns": [
            {
                "orderable": false,
                data: null, render: function (data, type, row) {

                    var strReturn = '<a href="#" onclick="DownloadAttachFile(' + row.Id + ')" ><i class="fas fa-paperclip"></i>' + row.UploadSignedActualName + '</a><input value="' + row.UploadSignedActualName + '" type="hidden" id="hdnContractAttachActulName_' + row.Id + '" /><input value="' + row.UploadSignedUrl + '" type="hidden" id="hdnContractAttachUrl_' + row.Id + '" />';
                    return strReturn;
                }
            },
            { "data": "Remarks" }
        ]
    });
}


function DownloadAttachFile(id) {
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



var PaymentDetails1 = [];
var PaymentDetails2 = [];

function Savetbldod2Data(ctrl) {
    var id = ctrl.id.split('_');
    var controlNo = id[1];
    var data1 = [];
    var isValid = true;
    if ($("#tbInvoice2_" + controlNo).val() == "") {
        $("#sptbInvoice2_" + controlNo).show();
        isValid = false;
    }
    if ($("#tbInvoiceDate2_" + controlNo).val() == "") {
        $("#sptbInvoiceDate2_" + controlNo).show();
        isValid = false;
    }
    if ($("#tbAmount2_" + controlNo).val() == "") {
        $("#sptbAmount2_" + controlNo).show();
        isValid = false;
    }

    if (isValid == true) {

        var obj2 =
        {
            Id: controlNo,
            Status: false,
            InvoiceNo: $("#tbInvoice2_" + controlNo).val(),
            InvoiceDate: ChangeDateFormat($("#tbInvoiceDate2_" + controlNo).val()),
            InvoiceAmount: $("#tbAmount2_" + controlNo).val(),
            InvoiceAttachmentActualName: $("#hdnUploadActualFileName2_" + controlNo).text(),
            InvoiceAttachmentNewName: $("#hdnUploadNewFileName2_" + controlNo).text(),
            InvoiceAttachmentURL: $("#hdnUploadFileUrl2_" + controlNo).text(),
        }

        data1.push(obj2);


        var obj =
        {
            DetailsofDeliverableList: data1,
            ProcessType: 11

        }
        CommonAjaxMethod(virtualPath + 'ProcurementRequest/SaveRFPEntry', obj, 'POST', function (response) {
            if (response.ValidationInput == 1) {
                RedirectProcurementUserRequest();
            }
            if (response.ValidationInput == 3) {
                ConfirmMsgBox("Are you sure want to proceed without amendmend", '', function () {
                    data1 = [];
                    var obj3 =
                    {
                        Id: controlNo,
                        Status: true,
                        InvoiceNo: $("#tbInvoice2_" + controlNo).val(),
                        InvoiceDate: ChangeDateFormat($("#tbInvoiceDate2_" + controlNo).val()),
                        InvoiceAmount: $("#tbAmount2_" + controlNo).val(),
                        InvoiceAttachmentActualName: $("#hdnUploadActualFileName2_" + controlNo).text(),
                        InvoiceAttachmentNewName: $("#hdnUploadNewFileName2_" + controlNo).text(),
                        InvoiceAttachmentURL: $("#hdnUploadFileUrl2_" + controlNo).text(),
                    }

                    data1.push(obj3);
                    var objnew =
                    {
                        DetailsofDeliverableList: data1,
                        ProcessType: 11

                    }
                    CommonAjaxMethod(virtualPath + 'ProcurementRequest/SaveRFPEntry', objnew, 'POST', function (response) {
                        if (response.ValidationInput == 1) {
                            RedirectProcurementUserRequest();
                        }
                    });
                });
            }
        });
    }


}

function Savetbldod1Data(ctrl) {
    var id = ctrl.id.split('_');
    var controlNo = id[1];

    var data = [];

    var isValid = true;

    if ($("#tbInvoice1_" + controlNo).val() == "") {
        $("#sptbInvoice1_" + controlNo).show();
        isValid = false;
    }
    if ($("#tbInvoiceDate1_" + controlNo).val() == "") {
        $("#sptbInvoiceDate1_" + controlNo).show();
        isValid = false;
    }
    if ($("#tbAmount1_" + controlNo).val() == "") {
        $("#sptbAmount1_" + controlNo).show();
        isValid = false;
    }
    if (isValid == true) {

        var obj1 =
        {
            Id: controlNo,
            Status: false,
            InvoiceNo: $("#tbInvoice1_" + controlNo).val(),
            InvoiceDate: ChangeDateFormat($("#tbInvoiceDate1_" + controlNo).val()),
            InvoiceAmount: $("#tbAmount1_" + controlNo).val(),
            InvoiceAttachmentActualName: $("#hdnUploadActualFileName1_" + controlNo).text(),
            InvoiceAttachmentNewName: $("#hdnUploadNewFileName1_" + controlNo).text(),
            InvoiceAttachmentURL: $("#hdnUploadFileUrl1_" + controlNo).text(),
        }

        data.push(obj1);





        var obj =
        {
            DetailsofDeliverableList: data,
            ProcessType: 10

        }
        CommonAjaxMethod(virtualPath + 'ProcurementRequest/SaveRFPEntry', obj, 'POST', function (response) {

            if (response.ValidationInput == 1) {
                RedirectProcurementUserRequest();
            }
            if (response.ValidationInput == 3) {
                ConfirmMsgBox("Are you sure want to proceed without amendmend", '', function () {
                    data1 = [];
                    var obj3 =
                    {
                        Id: controlNo,
                        Status: true,
                        InvoiceNo: $("#tbInvoice1_" + controlNo).val(),
                        InvoiceDate: ChangeDateFormat($("#tbInvoiceDate1_" + controlNo).val()),
                        InvoiceAmount: $("#tbAmount1_" + controlNo).val(),
                        InvoiceAttachmentActualName: $("#hdnUploadActualFileName1_" + controlNo).text(),
                        InvoiceAttachmentNewName: $("#hdnUploadNewFileName1_" + controlNo).text(),
                        InvoiceAttachmentURL: $("#hdnUploadFileUrl1_" + controlNo).text(),
                    }

                    data1.push(obj3);
                    var objnew =
                    {
                        DetailsofDeliverableList: data1,
                        ProcessType: 10

                    }
                    CommonAjaxMethod(virtualPath + 'ProcurementRequest/SaveRFPEntry', objnew, 'POST', function (response) {
                        if (response.ValidationInput == 1) {
                            RedirectProcurementUserRequest();
                        }
                    });
                });
            }

        });
    }


}
function Bindtbldod1(array, isAmend) {
    PaymentDetails1 = array;
    $('#tbldod1').DataTable({
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
                    if (row.InvoiceNo != '')
                        return '<label>' + row.InvoiceNo + '</label>';
                    else {
                        return '<input type="text" alt=""   onchange="HideErrorMessage(this)" class="form-control Finance" placeholder="Enter" id="tbInvoice_' + row.Id + '"> <span id="sptbInvoice_' + row.Id + '" class="text-danger field-validation-error" style="display:none;">Hey, You missed this field!!</span>';
                    }
                }
            },
            {
                "orderable": false,
                data: null, render: function (data, type, row) {

                    var invoiceDate = row.InvoiceDate != null || row.InvoiceDate != "" ? ChangeDateFormatToddMMYYY(row.InvoiceDate) : "";
                    if (invoiceDate == "01-01-1900") {
                        invoiceDate = "";
                    }
                    if (row.InvoiceNo != '')
                        return '<label>' + invoiceDate + '</label>';
                    else {
                        return '<input type="text" autocomplete="off"   class="form-control Finance datepicker1" placeholder="Enter" id="tbInvoiceDate_' + row.Id + '" onchange="HideErrorMessage(this)"> <span id="sptbInvoiceDate_' + row.Id + '" class="text-danger field-validation-error" style="display:none;">Hey, You missed this field!!</span>'
                    }


                }
            },

            {
                "orderable": false,
                "className": 'text-right',
                data: null, render: function (data, type, row) {

                    if (row.InvoiceNo != '')
                        return '<label class="text-right">' + NumberWithComma(row.InvoiceAmount) + '</label>';
                    else {
                        return '<input type="text" alt=""   onchange="HideErrorMessage(this)"  class="form-control Finance" placeholder="Enter" id="tbAmount_' + row.Id + '"> <span id="sptbAmount_' + row.Id + '" class="text-danger field-validation-error" style="display:none;">Hey, You missed this field!!</span>'
                    }


                }
            },

            {
                "orderable": false,
                data: null, render: function (data, type, row) {

                    if (row.InvoiceNo != '')
                        return '<label   style="display:none;" id="hdnUploadFileUrl_' + row.Id + '" >' + row.InvoiceAttachmentURL + '</label >' +
                            ' <label style="display:none;"  id="hdnUploadActualFileName_' + row.Id + '">' + row.InvoiceAttachmentActualName + '</label>' +
                            '<a id="ancUploadActualFileName_' + row.Id + '" href="" onclick="DownloadFileQuotation(this)">Download <i class="fas fa-download float-right" data-toggle="tooltip" title="Download"></i></a>' +
                            ' <label  style="display:none;" id="hdnUploadNewFileName_' + row.Id + '">' + row.InvoiceAttachmentNewName + '</label>';
                    else {
                        return '<input type="file" class="form-contoral  text-right Attach" placeholder="0" id="Attach_' + row.Id + '" onchange="Uploadocumenttbldod(this)">' +
                            '<label class="Fileurl" style="display:none;" id="hdnUploadFileUrl_' + row.Id + '" ></label >' +
                            ' <label class="FileActualName" onclick="DownloadFileQuotation(this)" style="display:none;" id="hdnUploadActualFileName_' + row.Id + '"></label>' +
                            ' <label class="FileNewName" style="display:none;" id="hdnUploadNewFileName_' + row.Id + '"></label>';
                    }


                }
            },

            {
                "orderable": false,
                data: null, render: function (data, type, row) {

                    if (row.InvoiceNo != '') {
                        if (row.InvoiceAttachmentActualNameD != undefined) {
                            return '<label   style="display:none;" id="hdnUploadFileUrlD_' + row.Id + '" >' + row.InvoiceAttachmentURLD + '</label >' +
                                ' <label style="display:none;" id="hdnUploadActualFileNameD_' + row.Id + '">' + row.InvoiceAttachmentActualNameD + '</label>' +
                                '<a id="ancUploadActualFileNameD_' + row.Id + '" href="" onclick="DownloadFileQuotationD(this)">Download <i class="fas fa-download float-right" data-toggle="tooltip" title="Download"></i></a>' +
                                ' <label  style="display:none;" id="hdnUploadNewFileNameD_' + row.Id + '">' + row.InvoiceAttachmentNewNameD + '</label>';
                        }
                        else {
                            return '';
                        }
                    }
                    else {
                        return '<input type="file" class="form-contoral  text-right AttachD" placeholder="0" id="AttachD_' + row.Id + '" onchange="UploadocumenttbldodD(this)">' +
                            '<label class="Fileurl" style="display:none;" id="hdnUploadFileUrlD_' + row.Id + '" ></label >' +
                            ' <label class="FileActualName" onclick="DownloadFileQuotationD(this)" style="display:none;" id="hdnUploadActualFileNameD_' + row.Id + '"></label>' +
                            ' <label class="FileNewName" style="display:none;" id="hdnUploadNewFileNameD_' + row.Id + '"></label>';
                    }


                }
            },
            {
                "orderable": false,
                data: null, render: function (data, type, row) {
                    if (row.InvoiceNo != '' || isAmend == 1)
                        return '';
                    else {
                        return '<button  id="btnSubmit_' + row.Id + '"  type="button" onclick="SavetbldodData(this,2)" class="bg-none" name="Command" value="Add" ><i class="fa fa-paper-plane"></i> Submit</button> ';
                    }
                }
            }
        ]
    });

}
function DownloadFileQuotationPD1(ctrl) {
    var id = ctrl.id.split('_');
    var controlNo = id[1];
    var fileURl = $('#hdnUploadFileUrlPD1_' + controlNo).text();
    var fileName = $('#hdnUploadActualFileNamePD1_' + controlNo).text();
    if (fileURl != null || fileURl != undefined) {
        var stSplitFileName = fileName.split(".");
        var link = document.createElement("a");
        link.download = stSplitFileName[0];
        link.href = fileURl;
        link.click();
    }
}
function DownloadFileQuotationPD2(ctrl) {
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
function DownloadFileQuotationPD3(ctrl) {
    var id = ctrl.id.split('_');
    var controlNo = id[1];
    var fileURl = $('#hdnUploadFileUrlPD3_' + controlNo).text();
    var fileName = $('#hdnUploadActualFileNamePD3_' + controlNo).text();
    if (fileURl != null || fileURl != undefined) {
        var stSplitFileName = fileName.split(".");
        var link = document.createElement("a");
        link.download = stSplitFileName[0];
        link.href = fileURl;
        link.click();
    }
}

function DownloadFileQuotationPD4(ctrl) {
    var id = ctrl.id.split('_');
    var controlNo = id[1];
    var fileURl = $('#hdnUploadFileUrlPD4_' + controlNo).text();
    var fileName = $('#hdnUploadActualFileNamePD4_' + controlNo).text();
    if (fileURl != null || fileURl != undefined) {
        var stSplitFileName = fileName.split(".");
        var link = document.createElement("a");
        link.download = stSplitFileName[0];
        link.href = fileURl;
        link.click();
    }
}
function DownloadFileQuotation1(ctrl) {
    var id = ctrl.id.split('_');
    var controlNo = id[1];
    var fileURl = $('#hdnUploadFileUrl1_' + controlNo).text();
    var fileName = $('#hdnUploadActualFileName1_' + controlNo).text();
    if (fileURl != null || fileURl != undefined) {
        var stSplitFileName = fileName.split(".");
        var link = document.createElement("a");
        link.download = stSplitFileName[0];
        link.href = fileURl;
        link.click();
    }
}
function Uploadocumenttbldod1(ctrl) {
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

                    $('#hdnUploadActualFileName1_' + controlNo).text(result.FileModel.ActualFileName);
                    $('#hdnUploadNewFileName1_' + controlNo).text(result.FileModel.NewFileName);
                    $('#hdnUploadFileUrl1_' + controlNo).text(result.FileModel.FileUrl);


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


function Bindtbldod2(array, isAmend) {
    PaymentDetails2 = array;

    $('#tbldod2').DataTable({
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
                    if (row.InvoiceNo != '')
                        return '<label>' + row.InvoiceNo + '</label>';
                    else {
                        return '<input type="text" alt=""  class="form-control Finance" placeholder="Enter" id="tbInvoice_' + row.Id + '"> <span id="sptbInvoice_' + row.Id + '" class="text-danger field-validation-error" style="display:none;">Hey, You missed this field!!</span>';
                    }
                }
            },
            {
                "orderable": false,
                data: null, render: function (data, type, row) {

                    var invoiceDate = row.InvoiceDate != null || row.InvoiceDate != "" ? ChangeDateFormatToddMMYYY(row.InvoiceDate) : "";
                    if (invoiceDate == "01-01-1900") {
                        invoiceDate = "";
                    }
                    if (row.InvoiceNo != '')
                        return '<label>' + invoiceDate + '</label>';
                    else {
                        return '<input type="text" autocomplete="off"   class="form-control Finance datepicker1" placeholder="Enter" id="tbInvoiceDate_' + row.Id + '" onchange="HideErrorMessage(this)"> <span id="sptbInvoiceDate_' + row.Id + '" class="text-danger field-validation-error" style="display:none;">Hey, You missed this field!!</span>'
                    }


                }
            },

            {
                "orderable": false,
                "className": 'text-right',
                data: null, render: function (data, type, row) {

                    if (row.InvoiceNo != '')
                        return '<label class="text-right">' + NumberWithComma(row.InvoiceAmount) + '</label>';
                    else {
                        return '<input type="text" alt=""   onchange="HideErrorMessage(this)" onkeypress="validate(event)" onpaste="validate(event)" class="form-control Finance" placeholder="Enter" id="tbAmount_' + row.Id + '"> <span id="sptbAmount_' + row.Id + '" class="text-danger field-validation-error" style="display:none;">Hey, You missed this field!!</span>'
                    }


                }
            },

            {
                "orderable": false,
                data: null, render: function (data, type, row) {

                    if (row.InvoiceNo != '')
                        return '<label   style="display:none;" id="hdnUploadFileUrl_' + row.Id + '" >' + row.InvoiceAttachmentURL + '</label >' +
                            ' <label style="display:none;"  id="hdnUploadActualFileName_' + row.Id + '">' + row.InvoiceAttachmentActualName + '</label>' +
                            '<a id="ancUploadActualFileName_' + row.Id + '" href="" onclick="DownloadFileQuotation(this)">Download <i class="fas fa-download float-right" data-toggle="tooltip" title="Download"></i></a>' +
                            ' <label  style="display:none;" id="hdnUploadNewFileName_' + row.Id + '">' + row.InvoiceAttachmentNewName + '</label>';
                    else {
                        return '<input type="file" class="form-contoral  text-right Attach" placeholder="0" id="Attach_' + row.Id + '" onchange="Uploadocumenttbldod(this)">' +
                            '<label class="Fileurl" style="display:none;" id="hdnUploadFileUrl_' + row.Id + '" ></label >' +
                            ' <label class="FileActualName" onclick="DownloadFileQuotation(this)" style="display:none;" id="hdnUploadActualFileName_' + row.Id + '"></label>' +
                            ' <label class="FileNewName" style="display:none;" id="hdnUploadNewFileName_' + row.Id + '"></label>';
                    }


                }
            },

            {
                "orderable": false,
                data: null, render: function (data, type, row) {

                    if (row.InvoiceNo != '') {
                        if (row.InvoiceAttachmentActualNameD != undefined) {
                            return '<label   style="display:none;" id="hdnUploadFileUrlD_' + row.Id + '" >' + row.InvoiceAttachmentURLD + '</label >' +
                                ' <label style="display:none;" id="hdnUploadActualFileNameD_' + row.Id + '">' + row.InvoiceAttachmentActualNameD + '</label>' +
                                '<a id="ancUploadActualFileNameD_' + row.Id + '" href="" onclick="DownloadFileQuotationD(this)">Download <i class="fas fa-download float-right" data-toggle="tooltip" title="Download"></i></a>' +
                                ' <label  style="display:none;" id="hdnUploadNewFileNameD_' + row.Id + '">' + row.InvoiceAttachmentNewNameD + '</label>';
                        }
                        else {
                            return '';
                        }
                    }
                    else {
                        return '<input type="file" class="form-contoral  text-right AttachD" placeholder="0" id="AttachD_' + row.Id + '" onchange="UploadocumenttbldodD(this)">' +
                            '<label class="Fileurl" style="display:none;" id="hdnUploadFileUrlD_' + row.Id + '" ></label >' +
                            ' <label class="FileActualName" onclick="DownloadFileQuotationD(this)" style="display:none;" id="hdnUploadActualFileNameD_' + row.Id + '"></label>' +
                            ' <label class="FileNewName" style="display:none;" id="hdnUploadNewFileNameD_' + row.Id + '"></label>';
                    }


                }
            },
            {
                "orderable": false,
                data: null, render: function (data, type, row) {
                    if (row.InvoiceNo != '' || isAmend == 1)
                        return '';
                    else {
                        return '<button  id="btnSubmit_' + row.Id + '"  type="button" onclick="SavetbldodData(this,3)" class="bg-none" name="Command" value="Add" ><i class="fa fa-paper-plane"></i> Submit</button> ';
                    }
                }
            }
        ]
    });
}
function DownloadFileQuotation2(ctrl) {
    var id = ctrl.id.split('_');
    var controlNo = id[1];
    var fileURl = $('#hdnUploadFileUrl2_' + controlNo).text();
    var fileName = $('#hdnUploadActualFileName2_' + controlNo).text();
    if (fileURl != null || fileURl != undefined) {
        var stSplitFileName = fileName.split(".");
        var link = document.createElement("a");
        link.download = stSplitFileName[0];
        link.href = fileURl;
        link.click();
    }
}
function Uploadocumenttbldod2(ctrl) {
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

                    $('#hdnUploadActualFileName2_' + controlNo).text(result.FileModel.ActualFileName);
                    $('#hdnUploadNewFileName2_' + controlNo).text(result.FileModel.NewFileName);
                    $('#hdnUploadFileUrl2_' + controlNo).text(result.FileModel.FileUrl);


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


var PaymentDetails3 = [];
function Savetbldod3Data(ctrl) {

    var id = ctrl.id.split('_');
    var controlNo = id[1];
    var isValid = true;
    var data = [];
    if ($("#tbInvoice3_" + controlNo).val() == "") {
        $("#sptbInvoice3_" + controlNo).show();
        isValid = false;
    }
    if ($("#tbInvoiceDate3_" + controlNo).val() == "") {
        $("#sptbInvoiceDate3_" + controlNo).show();
        isValid = false;
    }
    if ($("#tbAmount3_" + controlNo).val() == "") {
        $("#sptbAmount3_" + controlNo).show();
        isValid = false;
    }
    if (isValid == true) {

        var obj =
        {
            Id: controlNo,
            Status: false,
            InvoiceNo: $("#tbInvoice3_" + controlNo).val(),
            InvoiceDate: ChangeDateFormat($("#tbInvoiceDate3_" + controlNo).val()),
            InvoiceAmount: $("#tbAmount3_" + controlNo).val(),
            InvoiceAttachmentActualName: $("#hdnUploadActualFileName3_" + controlNo).text(),
            InvoiceAttachmentNewName: $("#hdnUploadNewFileName3_" + controlNo).text(),
            InvoiceAttachmentURL: $("#hdnUploadFileUrl3_" + controlNo).text(),
        }

        data.push(obj);
        var obj =
        {
            DetailsofDeliverableList: data,
            ProcessType: 12

        }
        CommonAjaxMethod(virtualPath + 'ProcurementRequest/SaveRFPEntry', obj, 'POST', function (response) {
            if (response.ValidationInput == 1) {
                RedirectProcurementUserRequest();
            }
            if (response.ValidationInput == 3) {
                ConfirmMsgBox("Are you sure want to proceed without amendmend", '', function () {
                    data1 = [];
                    var obj3 =
                    {
                        Id: controlNo,
                        Status: true,
                        InvoiceNo: $("#tbInvoice3_" + controlNo).val(),
                        InvoiceDate: ChangeDateFormat($("#tbInvoiceDate3_" + controlNo).val()),
                        InvoiceAmount: $("#tbAmount3_" + controlNo).val(),
                        InvoiceAttachmentActualName: $("#hdnUploadActualFileName3_" + controlNo).text(),
                        InvoiceAttachmentNewName: $("#hdnUploadNewFileName3_" + controlNo).text(),
                        InvoiceAttachmentURL: $("#hdnUploadFileUrl3_" + controlNo).text(),
                    }

                    data1.push(obj3);
                    var objnew =
                    {
                        DetailsofDeliverableList: data1,
                        ProcessType: 12

                    }
                    CommonAjaxMethod(virtualPath + 'ProcurementRequest/SaveRFPEntry', objnew, 'POST', function (response) {
                        if (response.ValidationInput == 1) {
                            RedirectProcurementUserRequest();
                        }
                    });
                });
            }
        });
    }


}
function Bindtbldod3(array) {
    PaymentDetails3 = array;
    $('#tbldodSubGrant').DataTable({
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
                    if (row.InvoiceNo != '')
                        return '<label>' + row.InvoiceNo + '</label>';
                    else {
                        return '<input type="text" alt=""   onchange="HideErrorMessage(this)"  class="form-control Finance" placeholder="Enter" id="tbInvoice_' + row.Id + '"> <span id="sptbInvoice_' + row.Id + '" class="text-danger field-validation-error" style="display:none;">Hey, You missed this field!!</span>';
                    }
                }
            },
            {
                "orderable": false,
                data: null, render: function (data, type, row) {

                    var invoiceDate = row.InvoiceDate != null || row.InvoiceDate != "" ? ChangeDateFormatToddMMYYY(row.InvoiceDate) : "";
                    if (invoiceDate == "01-01-1900") {
                        invoiceDate = "";
                    }
                    if (row.InvoiceNo != '')
                        return '<label>' + invoiceDate + '</label>';
                    else {
                        return '<input type="text" autocomplete="off"   class="form-control Finance datepicker1" placeholder="Enter" id="tbInvoiceDate_' + row.Id + '" onchange="HideErrorMessage(this)"> <span id="sptbInvoiceDate_' + row.Id + '" class="text-danger field-validation-error" style="display:none;">Hey, You missed this field!!</span>'
                    }


                }
            },

            {
                "orderable": false,
                "className": 'text-right',
                data: null, render: function (data, type, row) {

                    if (row.InvoiceNo != '')
                        return '<label class="text-right">' + NumberWithComma(row.InvoiceAmount) + '</label>';
                    else {
                        return '<input type="text" alt=""   onchange="HideErrorMessage(this)" onkeypress="validate(event)" onpaste="validate(event)" class="form-control Finance" placeholder="Enter" id="tbAmount_' + row.Id + '"> <span id="sptbAmount_' + row.Id + '" class="text-danger field-validation-error" style="display:none;">Hey, You missed this field!!</span>'
                    }


                }
            },

            {
                "orderable": false,
                data: null, render: function (data, type, row) {

                    if (row.InvoiceNo != '')
                        return '<label   style="display:none;" id="hdnUploadFileUrl_' + row.Id + '" >' + row.InvoiceAttachmentURL + '</label >' +
                            ' <label style="display:none;"  id="hdnUploadActualFileName_' + row.Id + '">' + row.InvoiceAttachmentActualName + '</label>' +
                            '<a id="ancUploadActualFileName_' + row.Id + '" href="" onclick="DownloadFileQuotation(this)">Download <i class="fas fa-download float-right" data-toggle="tooltip" title="Download"></i></a>' +
                            ' <label  style="display:none;" id="hdnUploadNewFileName_' + row.Id + '">' + row.InvoiceAttachmentNewName + '</label>';
                    else {
                        return '<input type="file" class="form-contoral  text-right Attach" placeholder="0" id="Attach_' + row.Id + '" onchange="Uploadocumenttbldod(this)">' +
                            '<label class="Fileurl" style="display:none;" id="hdnUploadFileUrl_' + row.Id + '" ></label >' +
                            ' <label class="FileActualName" onclick="DownloadFileQuotation(this)" style="display:none;" id="hdnUploadActualFileName_' + row.Id + '"></label>' +
                            ' <label class="FileNewName" style="display:none;" id="hdnUploadNewFileName_' + row.Id + '"></label>';
                    }


                }
            },

            {
                "orderable": false,
                data: null, render: function (data, type, row) {

                    if (row.InvoiceNo != '') {
                        if (row.InvoiceAttachmentActualNameD != undefined) {
                            return '<label   style="display:none;" id="hdnUploadFileUrlD_' + row.Id + '" >' + row.InvoiceAttachmentURLD + '</label >' +
                                ' <label onclick="DownloadFileQuotationD(this)"  id="hdnUploadActualFileNameD_' + row.Id + '">' + row.InvoiceAttachmentActualNameD + '</label>' +
                                ' <label  style="display:none;" id="hdnUploadNewFileNameD_' + row.Id + '">' + row.InvoiceAttachmentNewNameD + '</label>';

                        }
                        else {
                            return '';
                        }
                    }
                    else {
                        return '<input type="file" class="form-contoral  text-right AttachD" placeholder="0" id="AttachD_' + row.Id + '" onchange="UploadocumenttbldodD(this)">' +
                            '<label class="Fileurl" style="display:none;" id="hdnUploadFileUrlD_' + row.Id + '" ></label >' +
                            ' <label class="FileActualName" onclick="DownloadFileQuotationD(this)" style="display:none;" id="hdnUploadActualFileNameD_' + row.Id + '"></label>' +
                            ' <label class="FileNewName" style="display:none;" id="hdnUploadNewFileNameD_' + row.Id + '"></label>';
                    }


                }
            },
            {
                "orderable": false,
                data: null, render: function (data, type, row) {
                    if (row.InvoiceNo != '')
                        return '';
                    else {
                        return '<button  id="btnSubmit_' + row.Id + '"  type="button" onclick="SavetbldodData(this,4)" class="bg-none" name="Command" value="Add" ><i class="fa fa-paper-plane"></i> Submit</button> ';
                    }
                }
            }
        ]
    });
}
function DownloadFileQuotation3(ctrl) {
    var id = ctrl.id.split('_');
    var controlNo = id[1];
    var fileURl = $('#hdnUploadFileUrl3_' + controlNo).text();
    var fileName = $('#hdnUploadActualFileName3_' + controlNo).text();
    if (fileURl != null || fileURl != undefined) {
        var stSplitFileName = fileName.split(".");
        var link = document.createElement("a");
        link.download = stSplitFileName[0];
        link.href = fileURl;
        link.click();
    }
}
function Uploadocumenttbldod3(ctrl) {
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

                    $('#hdnUploadActualFileName3_' + controlNo).text(result.FileModel.ActualFileName);
                    $('#hdnUploadNewFileName3_' + controlNo).text(result.FileModel.NewFileName);
                    $('#hdnUploadFileUrl3_' + controlNo).text(result.FileModel.FileUrl);


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






var PaymentDetails = [];
function SavetbldodData(ctrl, from, isVendorPopup) {
    var isSubmit = 0;
    var id = "";
    var controlNo = "";

    if (isVendorPopup == 1) {
        isSubmit = 1;
        controlNo = ctrl;
        from = from;
    }
    else {
        id = ctrl.id.split('_');
        controlNo = id[1];
    }

    var pType = 0;
    if (from == 1) {
        pType = 9;
    }
    else if (from == 2) {
        pType = 10;
    }
    else if (from == 3) {
        pType = 11;
    }
    else if (from == 4) {
        pType = 12;
    }

    var data = [];
    var isValid = true;
    if ($("#tbInvoice_" + controlNo).val() == "") {
        $("#sptbInvoice_" + controlNo).show();
        isValid = false;
    }
    if ($("#tbInvoiceDate_" + controlNo).val() == "") {
        $("#sptbInvoiceDate_" + controlNo).show();
        isValid = false;
    }
    if ($("#tbAmount_" + controlNo).val() == "") {
        $("#sptbAmount_" + controlNo).show();
        isValid = false;
    }
    if (from != 1) {
        if ($("#hdnUploadActualFileName_" + controlNo).text() == "" && isValid == true) {
            FailToaster("Please upload invoice document.")
            isValid = false;
        }
        if ($("#hdnUploadActualFileNameD_" + controlNo).text() == "" && isValid == true) {
            FailToaster("Please upload deliverable document.")
            isValid = false;
        }
    }
    if (isValid == true) {

        var obj =
        {
            Id: controlNo,
            Status: false,
            InvoiceNo: $("#tbInvoice_" + controlNo).val(),
            InvoiceDate: ChangeDateFormat($("#tbInvoiceDate_" + controlNo).val()),
            InvoiceAmount: $("#tbAmount_" + controlNo).val(),
            InvoiceAttachmentActualName: $("#hdnUploadActualFileName_" + controlNo).text(),
            InvoiceAttachmentNewName: $("#hdnUploadNewFileName_" + controlNo).text(),
            InvoiceAttachmentURL: $("#hdnUploadFileUrl_" + controlNo).text(),
            InvoiceAttachmentActualNameD: $("#hdnUploadActualFileNameD_" + controlNo).text(),
            InvoiceAttachmentNewNameD: $("#hdnUploadNewFileNameD_" + controlNo).text(),
            InvoiceAttachmentURLD: $("#hdnUploadFileUrlD_" + controlNo).text(),
            IsSubmit: isSubmit
        }

        data.push(obj);




        var obj =
        {
            DetailsofDeliverableList: data,
            ProcessType: pType

        }
        CommonAjaxMethod(virtualPath + 'ProcurementRequest/SaveRFPEntry', obj, 'POST', function (response) {
            if (response.ValidationInput == 1) {
                RedirectProcurementUserRequest();
            }
            debugger;
            if (response.ValidationInput == 5) {
                $("#InvoiceId").val(controlNo);
                $("#Invoicefrom").val(from);
                $("#btnDodClose").trigger("click");
                $("#btnDod1Close").trigger("click");
                $("#btnShowVendorRating").trigger("click");
            }
            if (response.ValidationInput == 3) {
                ConfirmMsgBox("Are you sure want to proceed without amendmend", '', function () {
                    data1 = [];
                    var obj3 =
                    {
                        Id: controlNo,
                        Status: true,
                        InvoiceNo: $("#tbInvoice_" + controlNo).val(),
                        InvoiceDate: ChangeDateFormat($("#tbInvoiceDate_" + controlNo).val()),
                        InvoiceAmount: $("#tbAmount_" + controlNo).val(),
                        InvoiceAttachmentActualName: $("#hdnUploadActualFileName_" + controlNo).text(),
                        InvoiceAttachmentNewName: $("#hdnUploadNewFileName_" + controlNo).text(),
                        InvoiceAttachmentURL: $("#hdnUploadFileUrl_" + controlNo).text(),
                        InvoiceAttachmentActualNameD: $("#hdnUploadActualFileNameD_" + controlNo).text(),
                        InvoiceAttachmentNewNameD: $("#hdnUploadNewFileNameD_" + controlNo).text(),
                        InvoiceAttachmentURLD: $("#hdnUploadFileUrlD_" + controlNo).text(),
                        IsSubmit: isSubmit
                    }

                    data1.push(obj3);
                    var objnew =
                    {
                        DetailsofDeliverableList: data1,
                        ProcessType: pType

                    }
                    CommonAjaxMethod(virtualPath + 'ProcurementRequest/SaveRFPEntry', objnew, 'POST', function (response) {
                        if (response.ValidationInput == 1) {
                            RedirectProcurementUserRequest();
                        }
                        if (response.ValidationInput == 5) {
                            $("#InvoiceId").val(controlNo);
                            $("#Invoicefrom").val(from);
                            $("#btnShowVendorRating").trigger("click");
                        }
                    });
                });
            }
        });
    }


}
function Bindtbldod(array, isAmend) {
    PaymentDetails = array;


    $('#tbldod').DataTable({
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
                    if (row.InvoiceNo != '')
                        return '<label>' + row.InvoiceNo + '</label>';
                    else {
                        return '<input type="text" alt="" onchange="HideErrorMessage(this)" class="form-control Finance" placeholder="Enter" id="tbInvoice_' + row.Id + '"> <span id="sptbInvoice_' + row.Id + '" class="text-danger field-validation-error" style="display:none;">Hey, You missed this field!!</span>';
                    }
                }
            },
            {
                "orderable": false,
                data: null, render: function (data, type, row) {

                    var invoiceDate = row.InvoiceDate != null || row.InvoiceDate != "" ? ChangeDateFormatToddMMYYY(row.InvoiceDate) : "";
                    if (invoiceDate == "01-01-1900") {
                        invoiceDate = "";
                    }
                    if (row.InvoiceNo != '')
                        return '<label>' + invoiceDate + '</label>';
                    else {
                        return '<input type="text" autocomplete="off"   class="form-control Finance datepicker1" placeholder="Enter" id="tbInvoiceDate_' + row.Id + '" onchange="HideErrorMessage(this)"> <span id="sptbInvoiceDate_' + row.Id + '" class="text-danger field-validation-error" style="display:none;">Hey, You missed this field!!</span>'
                    }


                }
            },

            {
                "orderable": false,
                "className": 'text-right',
                data: null, render: function (data, type, row) {

                    if (row.InvoiceNo != '')
                        return '<label class="text-right">' + NumberWithComma(row.InvoiceAmount) + '</label>';
                    else {
                        return '<input type="text" alt=""   onchange="HideErrorMessage(this)" onkeypress="validate(event)" onpaste="validate(event)" class="form-control Finance" placeholder="Enter" id="tbAmount_' + row.Id + '"> <span id="sptbAmount_' + row.Id + '" class="text-danger field-validation-error" style="display:none;">Hey, You missed this field!!</span>'
                    }


                }
            },

            {
                "orderable": false,
                data: null, render: function (data, type, row) {

                    if (row.InvoiceNo != '')
                        return '<label   style="display:none;" id="hdnUploadFileUrl_' + row.Id + '" >' + row.InvoiceAttachmentURL + '</label >' +
                            ' <label style="display:none;"  id="hdnUploadActualFileName_' + row.Id + '">' + row.InvoiceAttachmentActualName + '</label>' +
                            '<a id="ancUploadActualFileName_' + row.Id + '" href="" onclick="DownloadFileQuotation(this)">Download <i class="fas fa-download float-right" data-toggle="tooltip" title="Download"></i></a>' +
                            ' <label  style="display:none;" id="hdnUploadNewFileName_' + row.Id + '">' + row.InvoiceAttachmentNewName + '</label>';
                    else {
                        return '<input type="file" class="form-contoral  text-right Attach" placeholder="0" id="Attach_' + row.Id + '" onchange="Uploadocumenttbldod(this)">' +
                            '<label class="Fileurl" style="display:none;" id="hdnUploadFileUrl_' + row.Id + '" ></label >' +
                            ' <label class="FileActualName" onclick="DownloadFileQuotation(this)" style="display:none;" id="hdnUploadActualFileName_' + row.Id + '"></label>' +
                            ' <label class="FileNewName" style="display:none;" id="hdnUploadNewFileName_' + row.Id + '"></label>';
                    }


                }
            },

            {
                "orderable": false,
                data: null, render: function (data, type, row) {

                    if (row.InvoiceNo != '')
                        return '<label   style="display:none;" id="hdnUploadFileUrlD_' + row.Id + '" >' + row.InvoiceAttachmentURLD + '</label >' +
                            ' <label style="display:none;"  id="hdnUploadActualFileNameD_' + row.Id + '">' + row.InvoiceAttachmentActualNameD + '</label>' +
                            '<a id="ancUploadActualFileNameD_' + row.Id + '" href="" onclick="DownloadFileQuotationD(this)">Download <i class="fas fa-download float-right" data-toggle="tooltip" title="Download"></i></a>' +
                            ' <label  style="display:none;" id="hdnUploadNewFileNameD_' + row.Id + '">' + row.InvoiceAttachmentNewNameD + '</label>';
                    else {
                        return '<input type="file" class="form-contoral  text-right AttachD" placeholder="0" id="AttachD_' + row.Id + '" onchange="UploadocumenttbldodD(this)">' +
                            '<label class="Fileurl" style="display:none;" id="hdnUploadFileUrlD_' + row.Id + '" ></label >' +
                            ' <label class="FileActualName" onclick="DownloadFileQuotationD(this)" style="display:none;" id="hdnUploadActualFileNameD_' + row.Id + '"></label>' +
                            ' <label class="FileNewName" style="display:none;" id="hdnUploadNewFileNameD_' + row.Id + '"></label>';
                    }


                }
            },
            {
                "orderable": false,
                data: null, render: function (data, type, row) {
                    if (row.InvoiceNo != '' || isAmend == 1)
                        return '';
                    else {
                        return '<button  id="btnSubmit_' + row.Id + '"  type="button" onclick="SavetbldodData(this,1)" class="bg-none" name="Command" value="Add" ><i class="fa fa-paper-plane"></i> Submit</button> ';
                    }
                }
            }
        ]
    });

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
function Uploadocumenttbldod(ctrl) {
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

function UploadocumenttbldodD(ctrl) {
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

                    $('#hdnUploadActualFileNameD_' + controlNo).text(result.FileModel.ActualFileName);
                    $('#hdnUploadNewFileNameD_' + controlNo).text(result.FileModel.NewFileName);
                    $('#hdnUploadFileUrlD_' + controlNo).text(result.FileModel.FileUrl);


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

function PMApproval() {
    $("#DivPMApproval").show();
    $("#DivPCApproval").hide();
    $("#DivEdApproval").hide();

    $("#btnPMApproval").addClass("active");
    $("#btnPCApproval").removeClass("active");
    $("#btnEdApproval").removeClass("active");
    $("#DivModuleApproval").hide();
    $("#DivModuleApproval1").hide();
    $("#btnModuleApproval").removeClass("active");
    $("#ModuleHeader").text("Project Manager Approval");

}
function PCApproval() {
    $("#btnPCApproval").addClass("active");
    $("#btnPMApproval").removeClass("active");
    $("#btnEdApproval").removeClass("active");
    $("#DivPCApproval").show();
    $("#DivPMApproval").hide();
    $("#DivEdApproval").hide()
    $("#DivModuleApproval").hide();
    $("#DivModuleApproval1").hide();
    $("#btnModuleApproval").removeClass("active");
    $("#ModuleHeader").text("PC Member Approval");
}
function PMEdproval() {
    $("#btnEdApproval").addClass("active");
    $("#btnPMApproval").removeClass("active");
    $("#btnPCApproval").removeClass("active");
    $("#DivEdApproval").show()
    $("#DivPCApproval").hide();
    $("#DivPMApproval").hide();

    $("#DivModuleApproval").hide();
    $("#DivModuleApproval1").hide();
    $("#btnModuleApproval").removeClass("active");
    $("#ModuleHeader").text("Approval");

}
function ModuleApproval() {

    $("#DivModuleApproval").show();
    $("#btnModuleApproval").addClass("active");

    $("#btnEdApproval").removeClass("active");
    $("#btnPMApproval").removeClass("active");
    $("#btnPCApproval").removeClass("active");
    $("#DivEdApproval").hide()
    $("#DivPCApproval").hide();
    $("#DivPMApproval").hide();

    $("#ModuleHeader").text("Module Administrator");
}
function ChangeRFP(ctrl) {

    if (ctrl == 1) {
        $("#DivModuleApproval").hide();
        $("#DivModuleApproval1").show();

    }
    else {
        $("#DivModuleApproval").show();
        $("#DivModuleApproval1").hide();
    }
}

function GetQuotationMessage() {

    CommonAjaxMethod(virtualPath + 'ProcurementRequest/BindProjectDetails', { id: RequestId, IsBindLine: 16 }
        , 'GET', function (response) {
            var data1 = response.data.data.Table;
            BindQuoutationMessage(data1);

        });
}
function DisplayMessageDiv(ctrl) {
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
function SendMessage() {

    if (checkValidationOnSubmit('Message') == true) {
        var objdata =
        {
            Id: 0,
            Procure_Request_Id: $("#hdMoreInfoId").val(),
            ParentId: 0,
            Message: $('#txtMessage').val(),
            ReciverId: $('#ddlMessageMember').val().join()
        }
        var obj =
        {
            QuotationMessage: objdata,
            ProcessType: 17
        }
        CommonAjaxMethod(virtualPath + 'ProcurementRequest/SaveRFPEntry', obj, 'POST', function (response) {
            $('#ddlMessageMember').val('0').trigger('change');
            $('#txtMessage').val();

            $('#btnModelPopupClose').trigger('click');

        });
    }
}
function SendMessageToUser(ctrl) {
    var reqInfoId = $("#hdMoreInfoId").val();
    var id = ctrl.id.split('_');
    var controlNo = id[1];
    if (checkValidationOnSubmit('Message_' + controlNo) == true) {
        var objdata =
        {
            Id: $('#hdnMessageId_' + controlNo).val(),
            Procure_Request_Id: reqInfoId,
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
            AskQuery(reqInfoId);
        });
    }
}
function AskQuery(id, from) {

    var isBindLine = 16;
    if (from == 1) {
        isBindLine = 17;

    }
    $('#hdnIsModuleAdminMessage').val(id);
    $("#hdMoreInfoId").val(id);
    GetQuotationMember(id);

    CommonAjaxMethod(virtualPath + 'ProcurementRequest/BindProjectDetails', { id: id, IsBindLine: isBindLine }
        , 'GET', function (response) {
            var data1 = response.data.data.Table;
            BindQuoutationMessage(data1);

        });
}
function BindMessageMember(d) {

    var $ele = $('#ddlMessageMember');
    $ele.empty();
    $ele.append($('<option/>').val('0').text('Select'));
    $.each(d, function (ii, vall) {
        $ele.append($('<option/>').val(vall.ID).text(vall.ValueName));

    })

}
function GetQuotationMember(rid) {

    CommonAjaxMethod(virtualPath + 'ProcurementRequest/BindProjectDetails', { id: rid, IsBindLine: 15 }
        , 'GET', function (response) {
            var data1 = response.data.data.Table;
            BindMessageMember(data1);

        });
}

function MessagePopupClose() {
    window.location.reload();
}

function GenerateAmendment(id, reId, from) {
    $("#btnadt").trigger("click");
}

function ViewQutationAmendData(id, datafrom, type) {
    var url = "";
    if (type == 1) {
        if (datafrom == 1) {
            url = "/Procurement/UserAmendmentQuotationConsultant?id=" + id;
            window.location.href = url;
        }
        else if (datafrom == 2) {
            url = "/Procurement/UserAmendmentQuotationConsultant?id=" + id;
            window.location.href = url;
        }
    }
    else if (type == 2) {
        if (datafrom == 1) {
            url = "/Procurement/UserAmendmentQuotationSubgrantRFP?id=" + id;
            window.location.href = url;

        }
        else if (datafrom == 2) {
            url = "/Procurement/UserAmendmentQuotationSubgrant?id=" + id;
            window.location.href = url;
        }
    }
    else {
        if (datafrom == 1) {
            url = "/Procurement/UserAmendmentQuotation?id=" + id;
            window.location.href = url;
        }
        else if (datafrom == 2) {
            url = "/Procurement/UserAmendmentQuotation?id=" + id;
            window.location.href = url;
        }

    }
}