$(document).ready(function () {
    BindContent();
});

function BindContent() {
    CommonAjaxMethod(virtualPath + 'ProcureVendorRegis/BindProcureVendorRegis', null, 'GET', function (response) {
        var tableId = '#tblpending';
        var tblPending = $(tableId).DataTable({
            "processing": true,
            "destroy": true,
            "data": response.data.data.Table,
            "stateSave": true,
            "columns": [
                { "data": "RowNum" },
                { "data": "Req_No" },
                {
                    "data": "Req_Date",
                    "render": function (data) {
                        return ChangeDateFormatToddMMYYY(data);
                    }
                },
                { "data": "ReqBy" },
                { "data": "VendorType" },
                { "data": "RelationshipwithC3" },
                { "data": "Status" },
                {
                    "orderable": false,
                    "data": null,
                    "render": function (data, type, row) {
                        return '<a onclick="RedirectToRegistration(' + row.Id + ')" href="#"><i class="fas fa-eye"></i> View</a>';
                    }
                }
            ],
        });

        tblPending.destroy();
        $('[data-toggle="tooltip"]').tooltip();
        tblPending.on('draw.dt', function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        // SEARCH FILTER IMPLEMENTATION
        tblPending.columns().every(function () {
            var that = this;
            $('input', this.header()).on('keyup change', function () {
                if (that.search() !== this.value) {
                    that.search(this.value).draw();
                }
            });
        });

        DatatableScriptsWithColumnSearch(tableId.substring(1), tblPending);

        //2nd table

        var tableIdA = '#tblapproved';
        var tblapproved = $(tableIdA).DataTable({
            "processing": true,
            "destroy": true,
            "data": response.data.data.Table,
            "stateSave": true,
            "columns": [
                { "data": "RowNum" },
                { "data": "Req_No" },
                {
                    "data": "Req_Date",
                    "render": function (data) {
                        return ChangeDateFormatToddMMYYY(data);
                    }
                },
                { "data": "ReqBy" },
                { "data": "VendorType" },
                { "data": "RelationshipwithC3" },
                { "data": "Status" },
                {
                    "orderable": false,
                    "data": null,
                    "render": function (data, type, row) {
                        return '<a onclick="RedirectToRegistration(' + row.Id + ')" href="#"><i class="fas fa-eye"></i> View</a>';
                    }
                }
            ],
        });

        tblapproved.destroy();
        $('[data-toggle="tooltip"]').tooltip();
        tblapproved.on('draw.dt', function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        // SEARCH FILTER IMPLEMENTATION
        tblapproved.columns().every(function () {
            var that = this;
            $('input', this.header()).on('keyup change', function () {
                if (that.search() !== this.value) {
                    that.search(this.value).draw();
                }
            });
        });

        DatatableScriptsWithColumnSearch(tableIdA.substring(1), tblapproved);

        // 3rd  table 
        var tableIdIM = '#tblimpanel';
        var tblimpanel = $(tableIdIM).DataTable({
            "processing": true,
            "destroy": true,
            "data": response.data.data.Table,
            "stateSave": true,
            "columns": [
                { "data": "RowNum" },
                { "data": "Req_No" },
                {
                    "data": "Req_Date",
                    "render": function (data) {
                        return ChangeDateFormatToddMMYYY(data);
                    }
                },
                { "data": "ReqBy" },
                { "data": "VendorType" },
                { "data": "RelationshipwithC3" },
                { "data": "Status" },
                {
                    "orderable": false,
                    "data": null,
                    "render": function (data, type, row) {
                        return '<a onclick="RedirectToRegistration(' + row.Id + ')" href="#"><i class="fas fa-eye"></i> View</a>';
                    }
                }
            ],
        });

        tblimpanel.destroy();
        $('[data-toggle="tooltip"]').tooltip();
        tblimpanel.on('draw.dt', function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        // SEARCH FILTER IMPLEMENTATION
        tblimpanel.columns().every(function () {
            var that = this;
            $('input', this.header()).on('keyup change', function () {
                if (that.search() !== this.value) {
                    that.search(this.value).draw();
                }
            });
        });

        DatatableScriptsWithColumnSearch(tableIdIM.substring(1), tblimpanel);

        // 4th table

        var tableIdL = '#tbllegacy';
        var tbllegacy = $(tableIdL).DataTable({
            "processing": true,
            "destroy": true,
            "data": response.data.data.Table,
            "stateSave": true,
            "columns": [
                { "data": "RowNum" },
                { "data": "Req_No" },
                {
                    "data": "Req_Date",
                    "render": function (data) {
                        return ChangeDateFormatToddMMYYY(data);
                    }
                },
                { "data": "ReqBy" },
                { "data": "VendorType" },
                { "data": "RelationshipwithC3" },
                { "data": "Status" },
                {
                    "orderable": false,
                    "data": null,
                    "render": function (data, type, row) {
                        return '<a onclick="RedirectToRegistration(' + row.Id + ')" href="#"><i class="fas fa-eye"></i> View</a>';
                    }
                }
            ],
        });

        tbllegacy.destroy();
        $('[data-toggle="tooltip"]').tooltip();
        tbllegacy.on('draw.dt', function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        // SEARCH FILTER IMPLEMENTATION
        tbllegacy.columns().every(function () {
            var that = this;
            $('input', this.header()).on('keyup change', function () {
                if (that.search() !== this.value) {
                    that.search(this.value).draw();
                }
            });
        });

        DatatableScriptsWithColumnSearch(tableIdL.substring(1), tbllegacy);


    });
}


function RedirectToRegistration(id) {
            var btn = document.getElementById("btnVendorRegistration");
            btn.href = "/Procurement/VendorRegistration?id=" + id;
            btn.click();
        }

function RedirectToLegacy(id) {
            var btn = document.getElementById("btnVendorRegistration");
            btn.href = "/Procurement/VendorRegistrationLegacy?id=" + id;
            btn.click();
        }

function RedirectToRegistrationImpanel(id) {
            var btn = document.getElementById("btnVendorRegistration");
            btn.href = "/Procurement/VendorRegistrationFormngoApproved?id=" + id;
            btn.click();
        }
function RedirectToRegistrationImpanelApproved(id) {
            var btn = document.getElementById("btnVendorRegistration");
            btn.href = "/Procurement/VendorRegistrationFromngoImpanel?id=" + id;
            btn.click();
        }
