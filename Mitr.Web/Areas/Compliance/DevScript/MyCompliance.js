$(document).ready(function () {

    BindCompliance();


});

function BindCompliance() {

    CommonAjaxMethod(virtualPath + 'ComplianceTransaction/GetMyComplianceTrans', null, 'GET', function (response) {

        var tableIdP = '#tablePending';
        var tablePending = $('#tablePending').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": response.data.data.Table,
            "stateSave": true, // Enable state saving
            "columns": [
                { "data": "RowNumber" },

                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {
                        return "<label>" + formatNumber(row.Doc_No) + "</label>";
                    }
                },
                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {
                        return "<label>" + ChangeDateFormatToddMMYYY(row.DueOn) + "</label>";
                    }
                },
                { "data": "ComplianceName" },
                { "data": "Frequency" },
                { "data": "Department" },
                { "data": "Level1" },
                { "data": "DelayDays" },
                { "data": "Status" },


                {
                    "orderable": false,
                    "data": null,
                    "render": function (data, type, row) {

                        var strReturn = '<div class="text-center" ><a href="#" onclick="ViewCompliance(' + row.ID + ')" ><i class="fas fa-eye" data-toggle="tooltip" title="" data-original-title="View"></i></a></div>';


                        return strReturn;
                    }
                }

            ]
            ,
            "initComplete": function () {
                initCompleteCallback(tableIdP.substring(1)); // Remove the leading # from tableId
            }
        });

        tablePending.destroy();

        // Initialize tooltips for the initial set of rows
        $('[data-toggle="tooltip"]').tooltip();

        // Re-initialize tooltips every time the table is redrawn
        tablePending.on('draw.dt', function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        DatatableScriptsWithColumnSearch(tableIdP.substring(1), tablePending);

        var tableIdIP = '#tableInProcess';

        var tableProcess = $('#tableInProcess').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": response.data.data.Table1,
            "stateSave": true, // Enable state saving
            "columns": [
                { "data": "RowNumber" },

                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {
                        return "<label>" + formatNumber(row.Doc_No) + "</label>";
                    }
                },
                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {
                        return "<label>" + ChangeDateFormatToddMMYYY(row.DueOn) + "</label>";
                    }
                },
                { "data": "ComplianceName" },
                { "data": "Frequency" },
                { "data": "Department" },
                { "data": "Level1" },
                { "data": "DelayDays" },
                { "data": "Status" },


                {
                    "orderable": false,
                    "data": null,
                    "render": function (data, type, row) {

                        var strReturn = '<div class="text-center" ><a href="#" onclick="ViewCompliance(' + row.ID + ')" ><i class="fas fa-eye" data-toggle="tooltip" title="" data-original-title="View"></i></a></div>';


                        return strReturn;
                    }
                }

            ]
            ,
            "initComplete": function () {
                initCompleteCallback(tableIdIP.substring(1)); // Remove the leading # from tableId
            }
        });

        tableProcess.destroy();

        // Initialize tooltips for the initial set of rows
        $('[data-toggle="tooltip"]').tooltip();

        // Re-initialize tooltips every time the table is redrawn
        tableProcess.on('draw.dt', function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        DatatableScriptsWithColumnSearch(tableIdIP.substring(1), tableProcess);

        var tableIdCompleted = '#tableCompleted';
        var tableC = $('#tableCompleted').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": response.data.data.Table2,
            "stateSave": true, // Enable state saving
            "columns": [
                { "data": "RowNumber" },

                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {
                        return "<label>" + formatNumber(row.Doc_No) + "</label>";
                    }
                },
                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {
                        return "<label>" + ChangeDateFormatToddMMYYY(row.DueOn) + "</label>";
                    }
                },
                { "data": "ComplianceName" },
                { "data": "Frequency" },
                { "data": "Department" },
                { "data": "Level1" },
                { "data": "DelayDays" },
                { "data": "Status" },


                {
                    "orderable": false,
                    "data": null,
                    "render": function (data, type, row) {

                        var strReturn = '<div class="text-center" ><a href="#" onclick="ViewCompliance(' + row.ID + ')" ><i class="fas fa-eye" data-toggle="tooltip" title="" data-original-title="View"></i></a></div>';


                        return strReturn;
                    }
                }

            ]
            ,
            "initComplete": function () {
                initCompleteCallback(tableIdCompleted.substring(1)); // Remove the leading # from tableId
            }
        });
        tableC.destroy();

        // Initialize tooltips for the initial set of rows
        $('[data-toggle="tooltip"]').tooltip();

        // Re-initialize tooltips every time the table is redrawn
        tableC.on('draw.dt', function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        DatatableScriptsWithColumnSearch(tableIdCompleted.substring(1), tableC);

    });

}



function formatNumber(value) {
    // Check if the number is an integer
    if (Number.isInteger(value)) {
        return value.toFixed(1); // Append .0 to integer values
    }
    return value.toString(); // Leave decimal values as they are
}