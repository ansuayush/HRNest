$(document).ready(function ()
{
    BindProcureRequest();    
});

function ApproveReject(from)
{
    var checkedArr = $("#tblPending").find("input[type=checkbox]:checked").map(function () {
        return this.id;
    }).get();
    var RequestApprovalArray = [];
    if (from == "1")
    {
        ConfirmMsgBox("Pls Reconfirm", '', function ()
        {
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

                ProcessType: 6,
                RFPPaymentTermsEntryList: RequestApprovalArray

            }
            CommonAjaxMethod(virtualPath + 'ProcurementRequest/SaveRFPEntry', obj, 'POST', function (response) {
                window.location.reload();
            });

        });
    }
   
    if (from == "2") {
        ConfirmMsgBox("Pls Reconfirm", '', function () {
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

                ProcessType: 6,
                RFPPaymentTermsEntryList: RequestApprovalArray

            }
            CommonAjaxMethod(virtualPath + 'ProcurementRequest/SaveRFPEntry', obj, 'POST', function (response) {
                window.location.reload();
            });

        });
    }
    
}

function BindProcureRequest() {
    CommonAjaxMethod(virtualPath + 'ProcurementRequest/BindAuthorisedSignatory', null, 'GET', function (response) {
        var dataPending = response.data.data.Table;
        var dataArroved = response.data.data.Table1;
        var dataRejected = response.data.data.Table2;
        
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
                { "data": "Status" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {
                        var strReturn = "";
                        if (row.ContractType == 'Consultant') {
                            if (row.DataFrom == 1) {
                                strReturn = '<a href="AuthorisedSignatoryQuotationConsultantRFP?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }
                            else if (row.DataFrom == 2) {
                                strReturn = '<a href="AuthorisedSignatoryQuotationConsultant?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';
                            }
                        }
                        else if (row.ContractType == 'Sub-grant') {
                            if (row.DataFrom == 1) {
                                strReturn = '<a href="AuthorisedSignatoryQuotationSubgrantRFP?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';
                            }
                            else if (row.DataFrom == 2) {
                                strReturn = '<a href="AuthorisedSignatoryQuotationSubgrant?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }
                        }
                        else {
                            if (row.DataFrom == 1) {
                                strReturn = '<a href="AuthorisedSignatoryQuotationRFP?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }
                            else if (row.DataFrom == 2) {
                                strReturn = '<a href="AuthorisedSignatoryQuotation?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }

                        }

                        return strReturn;
                    }
                }
                ,
                 {
                    "orderable": false,
                    data: null, render: function (data, type, row) {

                        var strReturn = '<input type="checkbox" id="chk-' + row.Id + '" class="selectedId sltchk" name="select" onchange="valueChanged()"><label for= "chk-' + row.Id + '" class= "m-0" ></label>';

                        return strReturn;
                    }
                }
            ]
        });


        $('#tblUnderprocessed').DataTable({
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
                { "data": "Status" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {
                        var strReturn = "";
                        if (row.ContractType == 'Consultant') {
                            if (row.DataFrom == 1) {
                                strReturn = '<a href="AuthorisedSignatoryQuotationConsultantRFP?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }
                            else if (row.DataFrom == 2) {
                                strReturn = '<a href="AuthorisedSignatoryQuotationConsultant?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';
                            }
                        }
                        else if (row.ContractType == 'Sub-grant') {
                            if (row.DataFrom == 1) {
                                strReturn = '<a href="AuthorisedSignatoryQuotationSubgrantRFP?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';
                            }
                            else if (row.DataFrom == 2) {
                                strReturn = '<a href="AuthorisedSignatoryQuotationSubgrant?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }
                        }
                        else {
                            if (row.DataFrom == 1) {
                                strReturn = '<a href="AuthorisedSignatoryQuotationRFP?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }
                            else if (row.DataFrom == 2) {
                                strReturn = '<a href="AuthorisedSignatoryQuotation?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }

                        }

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
                { "data": "Status" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {
                        var strReturn = "";
                        if (row.ContractType == 'Consultant') {
                            if (row.DataFrom == 1) {
                                strReturn = '<a href="AuthorisedSignatoryQuotationConsultantRFP?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }
                            else if (row.DataFrom == 2) {
                                strReturn = '<a href="AuthorisedSignatoryQuotationConsultant?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';
                            }
                        }
                        else if (row.ContractType == 'Sub-grant') {
                            if (row.DataFrom == 1) {
                                strReturn = '<a href="AuthorisedSignatoryQuotationSubgrantRFP?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';
                            }
                            else if (row.DataFrom == 2) {
                                strReturn = '<a href="AuthorisedSignatoryQuotationSubgrant?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }
                        }
                        else {
                            if (row.DataFrom == 1) {
                                strReturn = '<a href="AuthorisedSignatoryQuotationRFP?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }
                            else if (row.DataFrom == 2) {
                                strReturn = '<a href="AuthorisedSignatoryQuotation?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }

                        }

                        return strReturn;
                    }
                }


            ]
        });

    });
}