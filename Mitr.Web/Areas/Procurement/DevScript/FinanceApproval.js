$(document).ready(function () {
    BindProcureRequest();
});

function BindProcureRequest() {
    CommonAjaxMethod(virtualPath + 'ProcurementRequest/BindFinanceApprovals', null, 'GET', function (response) {
        var T = response.data.data.Table2;
         var tableIdP = '#tblPending';
        var tblPending = $('#tblPending').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": response.data.data.Table,
            "stateSave": true, // Enable state saving
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
                                strReturn = '<a href="FinanceQuotationConsultantRFP?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }
                            else if (row.DataFrom == 2) {
                                strReturn = '<a href="FinanceQuotationConsultant?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';
                            }
                        }
                        else if (row.ContractType == 'Sub-grant') {
                            if (row.DataFrom == 1) {
                                strReturn = '<a href="FinanceQuotationSubgrantRFP?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';
                            }
                            else if (row.DataFrom == 2) {
                                strReturn = '<a href="FinanceQuotationSubgrant?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }
                        }
                        else {
                            if (row.DataFrom == 1) {
                                strReturn = '<a href="FinanceQuotationRFP?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }
                            else if (row.DataFrom == 2) {
                                strReturn = '<a href="FinanceQuotation?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }

                        }

                        return strReturn;
                    }
                }


            ]
            ,

        });
        tblPending.destroy();

        // Initialize tooltips for the initial set of rows
        $('[data-toggle="tooltip"]').tooltip();

        //  Re-initialize tooltips every time the table is redrawn
        tblPending.on('draw.dt', function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        DatatableScriptsWithColumnSearch(tableIdP.substring(1), tblPending);



    var tableIdR = '#tblUnderprocessed';
        var tblUnderprocessed = $('#tblUnderprocessed').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": response.data.data.Table1,
            "stateSave": true, // Enable state saving
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
                            strReturn = '<a href="FinanceQuotationConsultantRFP?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                        }
                        else if (row.DataFrom == 2) {
                            strReturn = '<a href="FinanceQuotationConsultant?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';
                        }
                    }
                    else if (row.ContractType == 'Sub-grant') {
                        if (row.DataFrom == 1) {
                            strReturn = '<a href="FinanceQuotationSubgrantRFP?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';
                        }
                        else if (row.DataFrom == 2) {
                            strReturn = '<a href="FinanceQuotationSubgrant?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                        }
                    }
                    else {
                        if (row.DataFrom == 1) {
                            strReturn = '<a href="FinanceQuotationRFP?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                        }
                        else if (row.DataFrom == 2) {
                            strReturn = '<a href="FinanceQuotation?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                        }

                    }

                    return strReturn;
                }
            }


        ]
        });

        tblUnderprocessed.destroy();

        // Initialize tooltips for the initial set of rows
        $('[data-toggle="tooltip"]').tooltip();

        // Re-initialize tooltips every time the table is redrawn
        tblUnderprocessed.on('draw.dt', function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        DatatableScriptsWithColumnSearch(tableIdR.substring(1), tblUnderprocessed);


        var tableIdN = '#tblRejected';
        var tblRejected = $('#tblRejected').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": response.data.data.Table2,
            "stateSave": true, // Enable state saving
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
                                strReturn = '<a href="FinanceQuotationConsultantRFP?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }
                            else if (row.DataFrom == 2) {
                                strReturn = '<a href="FinanceQuotationConsultant?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';
                            }
                        }
                        else if (row.ContractType == 'Sub-grant') {
                            if (row.DataFrom == 1) {
                                strReturn = '<a href="FinanceQuotationSubgrantRFP?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';
                            }
                            else if (row.DataFrom == 2) {
                                strReturn = '<a href="FinanceQuotationSubgrant?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }
                        }
                        else {
                            if (row.DataFrom == 1) {
                                strReturn = '<a href="FinanceQuotationRFP?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }
                            else if (row.DataFrom == 2) {
                                strReturn = '<a href="FinanceQuotation?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }

                        }

                        return strReturn;
                    }
                }


            ]
        });

        tblRejected.destroy();

        // Initialize tooltips for the initial set of rows
        $('[data-toggle="tooltip"]').tooltip();

        // Re-initialize tooltips every time the table is redrawn
        tblRejected.on('draw.dt', function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        DatatableScriptsWithColumnSearch(tableIdN.substring(1), tblRejected);




    });



}
