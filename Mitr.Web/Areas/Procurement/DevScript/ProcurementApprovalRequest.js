$(document).ready(function () {
    BindProcureRequest();



    LoadMasterDropdown('ddlPOC', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.EmployeeWithoutLWD,
        manualTableId: 0
    }, 'Select', false);

});
function RejectReason() {

    if (checkValidationOnSubmit('Reject') == true) {
        var RequestApprovalArray = [];
        var obj =
        {
            Id: $("#hdnLineId").val(),
            UserId: loggedinUserid,
            ProcureId: $("#hdnProcureRequestId").val(),
            Reason: $("#txtRejectReason").val(),
            Status: 2,
            BudgetAmount: NumberWithComma(totalBudgetAmount),
            RequiredAmount: NumberWithComma(totalRequiredAmount)
        }
        RequestApprovalArray.push(obj);
        CommonAjaxMethod(virtualPath + 'ProcurementRequest/ApproveRejectProcurementRegistration', RequestApprovalArray, 'POST', function (response) {
            Redirect();
        });
    }
}
function Redirect() {
    var btn = document.getElementById("btnVendorRegistrationReturn");
    btn.click();
}

function ReSubmitReason() {
    if (checkValidationOnSubmit('Resubmission') == true) {
        var RequestApprovalArray = [];
        var obj =
        {
            Id: $("#hdnLineId").val(),
            UserId: loggedinUserid,
            ProcureId: $("#hdnProcureRequestId").val(),
            Reason: $("#txtResubmitReason").val(),
            Status: 3
        }
        RequestApprovalArray.push(obj);
        CommonAjaxMethod(virtualPath + 'ProcurementRequest/ApproveRejectProcurementRegistration', RequestApprovalArray, 'POST', function (response) {
            Redirect();
        });
    }
}
function AppoveRequest(from) {
    if (from == 2) {
        var RequestApprovalArray = [];

        var checkedArr = $("#datatableLineItem").find("input[type=checkbox]:checked").map(function () {
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
function ApproveRejectRequest() {
    $('#confirmmsgrcmultiple').modal('show');

}
function ApproveRejectRequestFromGrid(lineId, from, reId) {
    $("#hdnLineId").val(lineId);
    $("#hdnLineIdFrom").val(from);
    $("#hdnProcureRequestLineId").val(reId);
    $('#confirmmsgrcmultipleGrid').modal('show');
}
function ApproveSave() {
    var RequestApprovalArray = [];
    var obj =
    {
        Id: $("#hdnLineId").val(),
        UserId: loggedinUserid,
        ProcureId: $("#hdnProcureRequestLineId").val(),
        Reason: '',
        Status: 1
    }
    RequestApprovalArray.push(obj);
    CommonAjaxMethod(virtualPath + 'ProcurementRequest/ApproveRejectProcurementRegistration', RequestApprovalArray, 'POST', function (response) {
        Redirect();
    });

}
var totalRequiredAmount = 0;
var totalBudgetAmount = 0;
function SetLineId(ctrl, lineId, from, reId) {
    totalRequiredAmount = 0;
    totalBudgetAmount = 0;
    $("#hdnLineId").val(lineId);
    $("#hdnLineIdFrom").val(from);
    if (from == 2) {
        var bid = ctrl.id.split('_');
        var bValue = $('#lblBudget_' + bid[1]).text();
        var RValue = $('#lblRequired_' + bid[1]).text();

        var id = "";
        var lineId = "";
        var lblStatus_ = "";
        var BudgetAmountDataTotal = 0;
        var RBudgetAmountDataTotal = 0;
        var CurrentBudgetAmountDataTotal = 0;
        var CurrentRBudgetAmountDataTotal = 0;
        $(".BudgetAmountDataW").each(function () {
            id = $(this)[0].id.split('_');
            lineId = id[1];
            lblStatus_ = $('#lblStatus_' + lineId).text();
            if (lblStatus_ == "Approved" || lblStatus_ == "Resubmitted")
            {
                var current = Number($('#lblBudget_' + lineId).text()); 
                BudgetAmountDataTotal += parseFloat(current);
            }
            if (lblStatus_ == "Pending") {
                var current = Number($('#lblBudget_' + lineId).text()); 
                  CurrentBudgetAmountDataTotal += parseFloat(current);                 
            }

        });
        if (BudgetAmountDataTotal == 0)
        {
            CurrentBudgetAmountDataTotal = (CurrentBudgetAmountDataTotal-Number(bValue));
            totalBudgetAmount = CurrentBudgetAmountDataTotal;
        }
        else
        {
            BudgetAmountDataTotal = (BudgetAmountDataTotal - Number(bValue));
            totalBudgetAmount = BudgetAmountDataTotal;
        }
       

        $(".BudgetAmountDataR").each(function ()
        {
            id = $(this)[0].id.split('_');
            lineId = id[1];
            lblStatus_ = $('#lblStatus_' + lineId).text();
            if (lblStatus_ == "Approved" || lblStatus_ == "Resubmitted")
            {
                var current1 = Number($('#lblRequired_' + lineId).text()); 
                RBudgetAmountDataTotal += parseFloat(current1);
            }
            if (lblStatus_ == "Pending") {
                var current1 = Number($('#lblRequired_' + lineId).text()); 
                CurrentRBudgetAmountDataTotal += parseFloat(current1);
            }
        });

        if (RBudgetAmountDataTotal == 0) {
            CurrentRBudgetAmountDataTotal = (CurrentRBudgetAmountDataTotal - Number(RValue));
            totalRequiredAmount = CurrentRBudgetAmountDataTotal;
        }
        else {
            RBudgetAmountDataTotal = (RBudgetAmountDataTotal - Number(RValue));
            totalRequiredAmount = BudgetAmountDataTotal;
        }
        
    }


}
var rowId = 0;

function LoadLineItemData(id, reqId, from) {
    rowId = 0;
    $("#hdnProcureRequestId").val(id);
    if (from != 1) {
        $("#chkAll").hide();
        $("#divApproval").hide();

    }
    else {

        $("#chkAll").show();
        $("#divApproval").show();


    }
    CommonAjaxMethod(virtualPath + 'ProcurementRequest/BindProjectDetails', { id: id, IsBindLine: 5 }
        , 'GET', function (response) {
            var data1 = response.data.data.Table;
            var data2 = response.data.data.Table1;
            // var data3 = response.data.data.Table2;
            $("#ddlAgreement").val(data1[0].ContractType).trigger('change');
            $("#ddlPOC").val(data1[0].POC).trigger('change');
            $("#lblTotal1").text(data1[0].BudgetAmount);
            $("#lblTotal2").text(data1[0].RequiredAmount);
            $('#Reqby').val(data1[0].POCName);
            $('#ReqNo').val(data1[0].Req_No);
            $('#ReqDate').val(ChangeDateFormatToddMMYYY(data1[0].Req_Date));

            $('#datatableLineItem').DataTable({
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

                            return '<label id="lblBudget_' + row.Id + '"  class="BudgetAmountDataW text-right">' + row.BudgetAmount + '</label>';
                        }
                    },
                    {
                        "orderable": false,
                        data: null, render: function (data, type, row) {

                            return '<label id="lblRequired_' + row.Id + '"  class="BudgetAmountDataR text-right">' + row.RequiredAmount + '</label>';
                        }
                    },
                    { "data": "Remark" },

                    
                    {
                        "orderable": false,
                        data: null, render: function (data, type, row) {

                            var strReturn = '<div class="icon-thread">' +
                                '<ul class="list-unstyled inlin-list">' +
                                '<li>' +
                                '<input type="radio" name="' + row.Id + '-' + row.Procure_Request_Id + '" id="slt7_' + row.Id + '" value="" title="">' +
                                '<label class="radio m-0" id="lblLine_' + row.Id + '" onclick="SetLineId(this,' + row.Id + ',2,' + row.Procure_Request_Id + ')" for="slt7_' + row.Id + '" data-toggle="modal" data-target="#rjt"><i class="fas fa-ban" data-toggle="tooltip" title="Reject"></i></label>' +
                                '</li>' +
                                '<li class="nxtrnd">' +
                                '<input type="radio" name="' + row.Id + '-' + row.Procure_Request_Id + '" id="slt8' + row.Id + '" value="slt2">' +
                                '<label class="radio m-0" onclick="SetLineId(this,' + row.Id + ',3,' + row.Procure_Request_Id + ')" for="slt8' + row.Id + '" data-toggle="modal" data-target="#rsbt"> <i class="fas fa-redo" data-toggle="tooltip" title="Resubmit"></i> </label>' +
                                '</li>' +
                                '<li class="sltcnd">' +
                                '<input type="radio" name="' + row.Id + '-' + row.Procure_Request_Id + '" id="slt9' + row.Id + '" value="">' +
                                '<label class="radio m-0" onclick="ApproveRejectRequestFromGrid(' + row.Id + ',1,' + row.Procure_Request_Id + ')" for="slt9' + row.Id + '"><i class="fas fa-check" data-toggle="tooltip" title="Approve"></i> </label>' +
                                '</li>' +
                                '</ul>' +
                                '</div>';
                            if (from == 1) {
                                if (row.IsManager == 1 && row.Status == 'Pending') {
                                    return strReturn;
                                }
                                else {
                                    var aData1 = row.Status == null ? "" : row.Status;
                                    return '<label>' + aData1 + '</label>';;
                                }
                            }
                            else {
                                var aData = row.Status == null ? "" : row.Status;
                                return '<label>' + aData + '</label>';;
                            }
                        }
                    },
                    {
                        "orderable": false,
                        data: null, render: function (data, type, row)
                        {
                            var re = row.ApproveRejectReason == null?"" : row.ApproveRejectReason;
                            return '<label>' + re + '</label><input type="hidden" value="' + row.Status + '" id="lblStatus_' + row.Id + '" />';
                        }
                    },

                    {
                        "orderable": false,
                        data: null, render: function (data, type, row) {

                            var strReturn = '<input type="checkbox" id="chk-' + row.Id + '-' + row.Procure_Request_Id + '" class="selectedId sltchk" name="select" onchange="valueChanged()"><label for= "chk-' + row.Id + '-' + row.Procure_Request_Id + '" class= "m-0" ></label>';
                            if (from == 1) {
                                if (row.IsManager == 1 && row.Status == 'Pending') {
                                    return strReturn;
                                }
                                else {
                                    return "";
                                }
                            }
                            else {
                                return "";
                            }
                        }
                    }



                ]
            });

            $('#thbutton').css('width', '60px');

        });
}
function BindProcureRequest() {
    CommonAjaxMethod(virtualPath + 'ProcurementRequest/BindProcureApproveRequest', null, 'GET', function (response) {
        var dataPending = response.data.data.Table;
        var dataArroved = response.data.data.Table1;
        var dataResubmitted = response.data.data.Table2;
        var dataRejected = response.data.data.Table3;

        $('#tblPending').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": dataPending,
            "paging": true,
            "info": false,


            "columns": [



                { "data": "RowNum" },


                { "data": "Req_No" },
                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {

                        var strReturn = ChangeDateFormatToddMMYYY(row.Req_Date);
                        return strReturn;
                    }
                },
                { "data": "Req_ByName" },
                { "data": "POCName" },
                { "data": "ProjectCode" },
                { "data": "ContractType" },
                { "data": "RequiredAmount", "className": "text-right" },
                { "data": "Status" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {

                        var strReturn = '<a href="#" data-target="#pr" data-toggle="modal" onclick="LoadLineItemData(' + row.Id + ', ' + row.RequestId + ',1)"><i class="fas fa-eye"></i>View</a>';
                        return strReturn;
                    }
                }


            ]
        });

        $('#tblApproved').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": dataArroved,
            "paging": true,
            "info": false,


            "columns": [



                { "data": "RowNum" },


                { "data": "Req_No" },
                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {

                        var strReturn = ChangeDateFormatToddMMYYY(row.Req_Date);
                        return strReturn;
                    }
                },
                { "data": "Req_ByName" },
                { "data": "POCName" },
                { "data": "ProjectCode" },
                { "data": "ContractType" },
                { "data": "RequiredAmount", "className": "text-right" },
                { "data": "Status" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {

                        var strReturn = '<a href="#" data-target="#pr" data-toggle="modal" onclick="LoadLineItemData(' + row.Id + ', ' + row.RequestId + ',2)"><i class="fas fa-eye"></i>View</a>';
                        return strReturn;
                    }
                }


            ]
        });

        $('#tblResubmitted').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": dataResubmitted,
            "paging": true,
            "info": false,


            "columns": [



                { "data": "RowNum" },


                { "data": "Req_No" },
                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {

                        var strReturn = ChangeDateFormatToddMMYYY(row.Req_Date);
                        return strReturn;
                    }
                },
                { "data": "Req_ByName" },
                { "data": "POCName" },
                { "data": "ProjectCode" },
                { "data": "ContractType" },
                { "data": "RequiredAmount", "className": "text-right" },
                { "data": "Status" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {

                        var strReturn = '<a href="#" data-target="#pr" data-toggle="modal" onclick="LoadLineItemData(' + row.Id + ', ' + row.RequestId + ',3)"><i class="fas fa-eye"></i>View</a>';
                        return strReturn;
                    }
                }


            ]
        });

        $('#tblRejected').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": dataRejected,
            "paging": true,
            "info": false,


            "columns": [



                { "data": "RowNum" },


                { "data": "Req_No" },
                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {

                        var strReturn = ChangeDateFormatToddMMYYY(row.Req_Date);
                        return strReturn;
                    }
                },
                { "data": "Req_ByName" },
                { "data": "POCName" },
                { "data": "ProjectCode" },
                { "data": "ContractType" },
                { "data": "RequiredAmount", "className": "text-right" },
                { "data": "Status" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {

                        var strReturn = '<a href="#" data-target="#pr" data-toggle="modal" onclick="LoadLineItemData(' + row.Id + ', ' + row.RequestId + ',4)"><i class="fas fa-eye"></i>View</a>';
                        return strReturn;
                    }
                }

            ]
        });



    });
}



