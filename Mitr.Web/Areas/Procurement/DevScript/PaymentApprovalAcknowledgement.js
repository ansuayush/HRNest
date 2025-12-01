$(document).ready(function ()
{
    BindProcureRequest();    
});


function BindProcureRequest() {
    CommonAjaxMethod(virtualPath + 'ProcurementRequest/PaymentApprovalAcknowledgement', null, 'GET', function (response) {
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


                { "data": "ProjectCode" },

                { "data": "RequiredAmount", "className": "text-right" },
                { "data": "Status" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {

                        var strReturn = '<a href="FinancePaymentApprovalPending?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';
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



        var tableIdR = '#tblApproved';
        var tblUnderprocessed = $('#tblApproved').DataTable({
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

                { "data": "ProjectCode" },

                { "data": "RequiredAmount", "className": "text-right" },

                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {

                        var strReturn = '<a href="FinancePaymentApprovalPending?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';
                        return strReturn;
                    }
                }


            ],
        });

        tblUnderprocessed.destroy();
        // Initialize tooltips for the initial set of rows
        $('[data-toggle="tooltip"]').tooltip();

        // Re-initialize tooltips every time the table is redrawn
        tblUnderprocessed.on('draw.dt', function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        DatatableScriptsWithColumnSearch(tableIdR.substring(1), tblUnderprocessed);

    });



}
