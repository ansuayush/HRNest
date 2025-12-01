$(document).ready(function () {

    BindScreen()


});

function BindScreen() {

    var id = $("#hdfEMPID").val();
    var DocType = 'Handover Task';
    var model = {
        EMPID: id,
        DocType: DocType

    }

    const jsonString = JSON.stringify(model);
    var ScreenID = "NOCRequestList";
    CommonAjaxMethod(virtualPath + 'Generic/GetRecords', { modelData: jsonString, screenId: ScreenID }, 'GET', function (response) {


        var tableIdP = '#tabelpending';
        var tablePending = $('#tabelpending').DataTable({
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
                { "data": "relievingdate" },
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
                        var strReturn = '<strong><a href="#"  onclick="ViewData(' + row.ID + ')"><i class="fas fa-eye"></i>View</a></strong>';
                        return strReturn;
                    }
                }

            ]
            ,

        });
        tablePending.destroy();

        // Initialize tooltips for the initial set of rows
        $('[data-toggle="tooltip"]').tooltip();

        //  Re-initialize tooltips every time the table is redrawn
        tablePending.on('draw.dt', function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        DatatableScriptsWithColumnSearch(tableIdP.substring(1), tablePending);



        var tableIdA = '#tabelapproved';
        var tabelapproved = $('#tabelapproved').DataTable({
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
                { "data": "relievingdate" },
                //{ "data": "Daysleft" },
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
                        var strReturn = '<strong><a href="#"   onclick="ViewSavedata(' + row.ID + ')"><i class="fas fa-eye"></i>View</a></strong>';
                        return strReturn;
                    }
                }

            ]
            ,

        });

        tabelapproved.destroy();

        // Initialize tooltips for the initial set of rows
        $('[data-toggle="tooltip"]').tooltip();

        // Re-initialize tooltips every time the table is redrawn
        tabelapproved.on('draw.dt', function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        DatatableScriptsWithColumnSearch(tableIdA.substring(1), tabelapproved);






    });



}






