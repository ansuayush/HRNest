$(document).ready(function () {
    BindTrainingRequestPending();
});

function BindTrainingRequestPending() {
    document.getElementById('divPending').style.display = 'inline';
    document.getElementById('divProcess').style.display = 'none';
    document.getElementById('divCompleted').style.display = 'none';
    CommonAjaxMethod(virtualPath + 'CapacityRequest/GetAllTrainingRequestPending', null, 'GET', function (response) {
        var dataTrainingRequest = response.data.data.Table;
        $('#tblTrainingRequestPending').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": dataTrainingRequest,
            "pageLength": 15,
            "columns": [
                { "data": "RowNum" },
                { "data": "ReqNo" },
                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {

                        var strReturn = ChangeDateFormatToddMMYYY(row.ReqDate);
                        return strReturn;
                    }
                },
                { "data": "RequestedByName" },
               /* { "data": "Source" },*/
                { "data": "TypeOfTraining" },
                { "data": "NameOfTraining" },
                { "data": "Status" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {

                        var strReturn = '<a href="#" onclick="return ViewPendingApplication(' + row.ReqID + ');" ><i class="fas fa-eye"></i>View</a>';
                        return strReturn;
                    }
                }
            ]
        });
    });
}
function BindTrainingRequestProcess() {
    document.getElementById('divPending').style.display = 'none';
    document.getElementById('divProcess').style.display = 'inline';
    document.getElementById('divCompleted').style.display = 'none';
    CommonAjaxMethod(virtualPath + 'CapacityRequest/GetAllTrainingRequestProcesses', null, 'GET', function (response) {
        var dataTrainingRequest = response.data.data.Table;
        $('#tblTrainingRequestProcess').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": dataTrainingRequest,

            "columns": [
                { "data": "RowNum" },
                { "data": "ReqNo" },
                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {

                        var strReturn = ChangeDateFormatToddMMYYY(row.ReqDate);
                        return strReturn;
                    }
                },
                { "data": "RequestedByName" },
                { "data": "TypeOfTraining" },
                { "data": "NameOfTraining" },
                { "data": "Status" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {

                        var strReturn = '<a href="#" onclick="return ViewPendingApplication(' + row.ReqID + ');" ><i class="fas fa-eye"></i>View</a>';
                        return strReturn;
                    }
                }
            ]
        });
    });
}
function BindTrainingRequestCompleted() {
    document.getElementById('divPending').style.display = 'none';
    document.getElementById('divProcess').style.display = 'none';
    document.getElementById('divCompleted').style.display = 'inline';
    CommonAjaxMethod(virtualPath + 'CapacityRequest/GetAllTrainingRequestCompleted', null, 'GET', function (response) {
        var dataTrainingRequest = response.data.data.Table;
        $('#tblTrainingRequestCompleted').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": dataTrainingRequest,
            "pageLength": 15,
            "columns": [
                { "data": "RowNum" },
                { "data": "ReqNo" },
                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {

                        var strReturn = ChangeDateFormatToddMMYYY(row.ReqDate);
                        return strReturn;
                    }
                },
                { "data": "RequestedByName" },
                { "data": "TypeOfTraining" },
                { "data": "NameOfTraining" },
                { "data": "Status" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {

                        var strReturn = '<a href="#" onclick="return ViewPendingApplication(' + row.ReqID + ');" ><i class="fas fa-eye"></i>View</a>';
                        return strReturn;
                    }
                }
            ]
        });
    });
}

function GetAllPending() {
    BindTrainingRequestPending();
}
function GetAllProcess() {
    BindTrainingRequestProcess();
}
function GetAllCompleted() {
    BindTrainingRequestCompleted();
}





