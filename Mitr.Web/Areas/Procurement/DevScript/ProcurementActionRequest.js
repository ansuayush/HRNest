$(document).ready(function ()
{
    BindProcureRequest();    
    

    LoadMasterDropdown('ddlPOCA', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.EmployeeWithoutLWD,
        manualTableId: 0
    }, 'Select', false);
});

function RejectRequest(from) {

    ConfirmMsgBox("Are you sure want to Reject", '', function () {

        var RequestApprovalArray = [];
        var objIDs =
        {
            Procure_Request_Id: $("#hdnReqId").val(),
            Status: from
        }
        RequestApprovalArray.push(objIDs);

        var obj =
        {

            ProcessType: 5,
            RFPPaymentTermsEntryList: RequestApprovalArray

        }
        CommonAjaxMethod(virtualPath + 'ProcurementRequest/SaveRFPEntry', obj, 'POST', function (response) {
            $("#hdnReqId").val('');
            window.location.reload();
        });

    })
     
}
function ApproveRejectRequest(from)
{
    var msg = "Are you sure want to Reject";
    if (from == 8) {
        msg = "Pls Reconfirm";
    }
    ConfirmMsgBox(msg, '', function () {
        var RequestApprovalArray = [];
        var objIDs =
        {
            Procure_Request_Id: $("#hdnReqId").val(),
            Status: from
        }
        RequestApprovalArray.push(objIDs);

        var obj =
        {

            ProcessType: 5,
            RFPPaymentTermsEntryList: RequestApprovalArray

        }
        CommonAjaxMethod(virtualPath + 'ProcurementRequest/SaveRFPEntry', obj, 'POST', function (response) {
            window.location.reload();
        });
    });
}
function ApprovedRejectData(from)
{
    var msg = "Are you sure want to Reject";
    if (from == 8) {
        msg = "Pls Reconfirm";
    }
    ConfirmMsgBox(msg, '', function () {
        var RequestApprovalArray = [];
        var checkedArr = $("#tblPending").find("input[type=checkbox]:checked").map(function () {
            return this.id;
        }).get();

        for (var i = 0; i < checkedArr.length; i++) {
            if (checkedArr[i].includes('chk')) {
                var ids = checkedArr[i].split('-');
                var objIDs =
                {
                    Procure_Request_Id: ids[1],
                    Status: from
                }
                RequestApprovalArray.push(objIDs);
            }
        }


        var obj =
        {

            ProcessType: 5,
            RFPPaymentTermsEntryList: RequestApprovalArray

        }
        CommonAjaxMethod(virtualPath + 'ProcurementRequest/SaveRFPEntry', obj, 'POST', function (response) {
            window.location.reload();
        });
    });
}

function LoadLineItemData(Id,RId,d)
{
    if (d == 1) {
        $("#hdnReqId").val(Id);
        $("#divApprove").show();
    }
    else {
        
        $("#divApprove").hide();
        $("#hdnReqId").val('');
    }
     
    CommonAjaxMethod(virtualPath + 'ProcurementRequest/BindProjectDetails', { id: Id, IsBindLine: 8 }
        , 'GET', function (response) {
            var data1 = response.data.data.Table;



            var data2 = response.data.data.Table1;
           


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

           

        });
}
function BindProcureRequest() {
    CommonAjaxMethod(virtualPath + 'ProcurementRequest/BindProcurementActionRequest', null, 'GET', function (response) {
        var dataPending = response.data.data.Table;
        var dataArroved = response.data.data.Table1;
        var dataResubmitted = response.data.data.Table2;        
        $('#tblPending').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": dataPending,
            "paging": true,
            "info": false,
            "order": [],

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
                { "data": "Requiredjustificationforwaiver" },
                
                { "data": "Status" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {

                        var strReturn = '<a href="#" data-target="#pr-p" data-toggle="modal" onclick="LoadLineItemData(' + row.Id + ', ' + row.RequestId + ',1)"><i class="fas fa-eye"></i>View</a>';
                        return strReturn;
                    }
                },

                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {

                        var strReturn = '<input type="checkbox" id="chk-' + row.Id + '" class="selectedId sltchk" name="select" onchange="valueChanged()"><label for= "chk-' + row.Id + '" class= "m-0" ></label>';

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
            "order": [],

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
                { "data": "Requiredjustificationforwaiver" },
                { "data": "Status" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {

                        var strReturn = '<a href="#" data-target="#pr-p" data-toggle="modal" onclick="LoadLineItemData(' + row.Id + ', ' + row.RequestId + ',2)"><i class="fas fa-eye"></i>View</a>';
                        return strReturn;
                    }
                }

                   
            ]
        });

        $('#tblRejected').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": dataResubmitted,
            "paging": true,
            "info": false,
            "order": [],

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
                { "data": "Requiredjustificationforwaiver" },
                { "data": "Status" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {

                        var strReturn = '<a href="#" data-target="#pr-p" data-toggle="modal" onclick="LoadLineItemData(' + row.Id + ', ' + row.RequestId + ',3)"><i class="fas fa-eye"></i>View</a>';
                        return strReturn;
                    }
                }

            ]
        });

      

    });
}