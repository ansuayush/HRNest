$(document).ready(function () {
    openlevel(1)
});
function openlevel(type) {
    const tabs = document.querySelectorAll('.tab-thrid li');

    // Loop through the list items and update classes
    tabs.forEach((tab, index) => {

        if (index === type - 1) {
            tab.className = 'active'; // Set the clicked tab as active
        } else {
            tab.className = 'dnactive'; // Set others as inactive
        }
    });
    BindCompliance(type);
}
function BindCompliance(type) {
    var LevelForm = type;
    var id = $("#hdfEMPID").val();
    var model = {
        EMPID: id,
        Level: type

    }

    const jsonString = JSON.stringify(model);
    var ScreenID = "levelAction";
    CommonAjaxMethod(virtualPath + 'Generic/GetRecords', { modelData: jsonString, screenId: ScreenID }, 'GET', function (response) {
        debugger;
        var Y = response.data.data.Table1;
        var tableIdP = '#tablePending';
        var tablePending = $('#tablePending').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": response.data.data.Table,
            "stateSave": true, // Enable state saving
            "columns": [
                { "data": "RowNumber" },
                { "data": "emp_code" },
                { "data": "emp_name" },
                { "data": "Location" },
                { "data": "Department" },
                { "data": "Designation" },
                { "data": "ReqDate" },
                { "data": "relievingdate" },
                /* { "data": "relievingday" },*/
               // { "data": "Daysleft" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {
                    
                        var strReturn = "";
                        if (row.Daysleft <=7) {
                            strReturn = "<strong class='badge badge - secondary mtr - y - bg'>" + row.Daysleft +" Days</strong> ";
                        }
                        else {
                            strReturn = "<strong class='badge badge - secondary'>" + row.Daysleft +" Days</strong> ";

                        }


                        return strReturn;
                    }
                },


                {
                    "orderable": false,
                    "data": null,
                    "render": function (data, type, row) {
                        var strReturn = '<div class="text-center" ><a href="#" onclick="ViewResignation(' + row.ID + ',' + LevelForm + ')" ><i class="fas fa-eye" data-toggle="tooltip" title="" data-original-title="View"></i></a></div>';
                        return strReturn;
                    }
                }

            ]
            ,
          
        });

        tablePending.destroy();

        // Initialize tooltips for the initial set of rows
        $('[data-toggle="tooltip"]').tooltip();

        // Re-initialize tooltips every time the table is redrawn
        tablePending.on('draw.dt', function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        DatatableScriptsWithColumnSearch(tableIdP.substring(1), tablePending);
        var tableIdA = '#tableapproved';
        var tableapproved = $('#tableapproved').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": response.data.data.Table1,
            "stateSave": true, // Enable state saving
            "columns": [
                { "data": "RowNumber" },
                { "data": "emp_code" },
                { "data": "emp_name" },
                { "data": "Location" },
                { "data": "Department" },
                { "data": "Designation" },
                { "data": "ReqDate" },
                { "data": "relievingdate" },
                /*{ "data": "relievingday" },*/
                // { "data": "Daysleft" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {

                        var strReturn = "";
                        if (row.Daysleft <= 7) {
                            strReturn = "<strong class='badge badge - secondary mtr - y - bg'>" + row.Daysleft + " Days</strong> ";
                        }
                        else {
                            strReturn = "<strong class='badge badge - secondary'>" + row.Daysleft + " Days</strong> ";

                        }


                        return strReturn;
                    }
                },

                {
                    "orderable": false,
                    "data": null,
                    "render": function (data, type, row) {
                        var strReturn = '<div class="text-center" ><a href="#" onclick="ViewResignation(' + row.ID + ',' + LevelForm + ')" ><i class="fas fa-eye" data-toggle="tooltip" title="" data-original-title="View"></i></a></div>';
                        return strReturn;
                    }
                }

            ]
            ,

        });

        tableapproved.destroy();

        // Initialize tooltips for the initial set of rows
        $('[data-toggle="tooltip"]').tooltip();

        // Re-initialize tooltips every time the table is redrawn
        tableapproved.on('draw.dt', function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        DatatableScriptsWithColumnSearch(tableIdA.substring(1), tableapproved);

        var tableIdC = '#tableCompleted';

        var tableCompleted = $('#tableCompleted').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": response.data.data.Table2,
            "stateSave": true, // Enable state saving
            "columns": [
                { "data": "RowNumber" },
                { "data": "emp_code" },
                { "data": "emp_name" },
                { "data": "Location" },
                { "data": "Department" },
                { "data": "Designation" },
                { "data": "ReqDate" },
                { "data": "relievingdate" },
                /*   { "data": "relievingday" },*/
                //  { "data": "Daysleft" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {

                        var strReturn = "";
                        if (row.Daysleft <= 7) {
                            strReturn = "<strong class='badge badge - secondary mtr - y - bg'>" + row.Daysleft + " Days</strong> ";
                        }
                        else {
                            strReturn = "<strong class='badge badge - secondary'>" + row.Daysleft + " Days</strong> ";

                        }


                        return strReturn;
                    }
                },

                {
                    "orderable": false,
                    "data": null,
                    "render": function (data, type, row) {

                        var strReturn = '<div class="text-center" ><a href="#" onclick="ViewResignation(' + row.ID + ',' + LevelForm + ')" ><i class="fas fa-eye" data-toggle="tooltip" title="" data-original-title="View"></i></a></div>';


                        return strReturn;
                    }
                }

            ]
            ,

        });

        tableCompleted.destroy();

        // Initialize tooltips for the initial set of rows
        $('[data-toggle="tooltip"]').tooltip();

        // Re-initialize tooltips every time the table is redrawn
        tableCompleted.on('draw.dt', function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        DatatableScriptsWithColumnSearch(tableIdC.substring(1), tableCompleted);



    });

}









