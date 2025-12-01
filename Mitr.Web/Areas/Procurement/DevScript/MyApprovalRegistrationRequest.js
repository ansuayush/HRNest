$(document).ready(function ()
{   
    BindContent();  
});

function BindContent() {
    CommonAjaxMethod(virtualPath + 'ProcureVendorRegis/GetProcureforApprovals', null, 'GET', function (response) {
        debugger;
        var A = response.data.data.Table1;
        var T = response.data.data.Table2;
        //1st table Sanjay Gupta
        var tblpending = '#tblpending';
        var tabalpending = $('#tblpending').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": response.data.data.Table,
            "stateSave": true, // Enable state saving
            "columns": [
                { "data": "Id" },
                { "data": "Req_No" },
                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {

                        var strReturn = ChangeDateFormatToddMMYYY(row.Req_Date);
                        return strReturn;
                    }
                },
                { "data": "ReqBy" },
                { "data": "VendorType" },
                { "data": "VendorName" },
                { "data": "RelationshipwithC3" },
                { "data": "Status" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {

                        var strReturn = '<a onclick="RedirectToRegistration(' + row.Id + ')" href="#"><i class="fas fa-eye"></i>View</a>';
                        return strReturn;
                    }
                }


            ]
            ,
        });

        tabalpending.destroy();
        // Initialize tooltips for the initial set of rows
        $('[data-toggle="tooltip"]').tooltip();

        // Re-initialize tooltips every time the table is redrawn
        tabalpending.on('draw.dt', function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        DatatableScriptsWithColumnSearch(tblpending.substring(1), tabalpending);

        //2nd table Sanjay Gupta
        var tblappro = '#tblapproved';
        var tblapproved = $('#tblapproved').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": response.data.data.Table1,
            "stateSave": true, // Enable state saving
            "columns": [
                { "data": "Id" },
                { "data": "Req_No" },
                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {

                        var strReturn = ChangeDateFormatToddMMYYY(row.Req_Date);
                        return strReturn;
                    }
                },


                { "data": "ReqBy" },
                { "data": "VendorType" },
                { "data": "VendorName" },
                { "data": "RelationshipwithC3" },

                { "data": "Status" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {

                        var strReturn = '<a onclick="RedirectToApproved(' + row.Id + ')" href="#"><i class="fas fa-eye"></i>View</a>';
                        return strReturn;
                    }
                }


            ],
        });

        tblapproved.destroy();
        // Initialize tooltips for the initial set of rows
        $('[data-toggle="tooltip"]').tooltip();

        // Re-initialize tooltips every time the table is redrawn
        tblapproved.on('draw.dt', function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        DatatableScriptsWithColumnSearch(tblappro.substring(1), tblapproved);


        //3th table Sanjay Gupta
        var tableIdN = '#tblrejected';
        var tblRejected = $('#tblrejected').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": response.data.data.Table2,
            "stateSave": true, // Enable state saving
            "columns": [
                { "data": "Id" },
                { "data": "Req_No" },
                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {

                        var strReturn = ChangeDateFormatToddMMYYY(row.Req_Date);
                        return strReturn;
                    }
                },


                { "data": "ReqBy" },
                { "data": "VendorType" },
                { "data": "VendorName" },
                { "data": "RelationshipwithC3" },
                { "data": "Status" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {
                        var strReturn = '<a onclick="RedirectToReject(' + row.Id + ')" href="#"><i class="fas fa-eye"></i>View</a>';
                        return strReturn;
                    }
                }


            ],
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


function RedirectToRegistration(id)
{    
    var btn = document.getElementById("btnVendorRegistration");
    btn.href = "/Procurement/VendorRegistrationApproval?id=" + id;
    btn.click();
}

function RedirectToApproved(id)
{   
    var btn = document.getElementById("btnVendorRegistration");
    btn.href = "/Procurement/VendorRegistration?id=" + id +"&IsApproved=2";
    btn.click();
}
function RedirectToReject(id)
{
    var btn = document.getElementById("btnVendorRegistration");
    btn.href = "/Procurement/VendorRegistrationRejectedByApprover?id=" + id;
    btn.click();
}
