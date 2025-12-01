$(document).ready(function () {
    BindTrainingRequestPending();
    BindTrainingRequestProcess();
    BindTrainingRequestCompleted();
});

function BindTrainingRequestPending() {
    CommonAjaxMethod(virtualPath + 'HRRequest/GetAllTrainingRequestPending', null, 'GET', function (response) {
        var dataTrainingRequest = response.data.data.Table;
        $('#tbltrainingRequestPending').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": dataTrainingRequest,
            "pageLength": 10,
            "columnDefs": [
                {
                    "targets": [10, 11, 12], // Index of the column you want to hide (e.g., the 4th column)
                    "visible": false
                }
            ],
            "columns": [
                { "data": "RowNum" },
                { "data": "ReqNo" },
                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {
                        return "<label>" + ChangeDateFormatToddMMYYY(row.ReqDate) + "</label>";
                    }
                },
                { "data": "RequestedByName" },
                { "data": "Source" },
                { "data": "TypeOfTraining" },
                { "data": "NameOfTraining" },
                {
                   /* "data": "Status"*/
                     "orderable": false,
                    data: null, render: function (data, type, row) {
                        var strReturn = "";
                        if (row.IsClubbed == true) {
                            strReturn = "<span style='color:red'>Clubbed</span>";
                        }
                        else
                        {
                            strReturn = row.Status;
                        }
                        return strReturn;
                    }
                },
                {
                    "orderable": false,
                    "data": null,
                    "render": function (data, type, row) {
                        var strReturn = "<div class='text-center' ><a href='#' onclick='ViewApplication(\"" + row.ReqID + "\",\"" + row.Status + "\",\"" + row.Source + "\")' ><i class='fas fa-eye'></i>View</a></div>";
                        return strReturn;
                    }
                },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {
                        var strReturn = "";
                        if (row.IsClubbed != true) {
                            if (row.Status == "Pending") {
                                strReturn = "<div class='checkbox-button'><input tooltip onclick='ClubbedItems(\"" + row.ReqID + "\",\"" + row.RowNum + "\",\"" + row.TypeOfTrainingID + "\")'  id='chk_" + row.ReqID + "' name='checkbox' type='checkbox'></input><label for='chk_" + row.ReqID + "' class='checkbox-label mb-0'></label></div>";
                            }
                        }
                        return strReturn;
                    }
                },
                { "data": "ReqID" },
                { "data": "TypeOfTrainingID" },
                { "data": "TrainingID" }
            ]
        });
    });
}


let IsSameTraningTypeId = [];
let ReqIdArray = [];
function ClubbedItems(ReqID, RowNum, TypeOfTrainingID) {

    var IsRowCheck = false;

    IsRowCheck = document.getElementById('chk_' + ReqID + '').checked;
    //if ($('#tbltrainingRequestPending tr')[ReqID].cells[7].innerText != 'Draft') {
        if (IsRowCheck == true) {
            if (IsSameTraningTypeId.length > 0) {
                if (IsSameTraningTypeId[0] == TypeOfTrainingID) {
                    IsSameTraningTypeId.push(TypeOfTrainingID);
                    ReqIdArray.push(ReqID);
                }
                else {
                    alert('Please select same training name.')
                    document.getElementById('chk_' + ReqID + '').checked = false;
                }
            }
            else {
                IsSameTraningTypeId.push(TypeOfTrainingID);
                ReqIdArray.push(ReqID);
            }
        }
        if (IsRowCheck == false) {
            var ReqIdPop = ReqID;
            var IsSameTraningTypeIdPop = TypeOfTrainingID;
            if (ReqIdArray.length > 0) {
                findAndRemove(ReqIdArray, ReqIdPop);
            }
            if (IsSameTraningTypeId.length > 0) {
                findAndRemove(IsSameTraningTypeId, IsSameTraningTypeIdPop);
            }
        }
    //}
    //else {
    //    alert('Request in Draft.')
    //    document.getElementById('chk_' + ReqID + '').checked = false;
    //}
}
function findAndRemove(array, value) {
    let index = array.indexOf(value);
    if (index !== -1) {
        // Remove the element using splice
        return array.splice(index, 1)[0]; // Returns the removed element
    }
    return null; // Return null if the element is not found
}
$('#SelectAll').click(function (e) {
     IsSameTraningTypeId = [];
     ReqIdArray = [];
    $('#tbltrainingRequestPending tbody :checkbox').prop('checked', $(this).is(':checked'));
    e.stopImmediatePropagation();
});

function ClubbedTrainingRequest() {
    var checkedArr = $("#tbltrainingRequestPending").find("input[type=checkbox]:checked").map(function () {
        return this.id;
    }).get();
    for (var i = 0; i < checkedArr.length; i++) {
        if (checkedArr[i].includes('chk')) {
            var ids = checkedArr[i].split('_');
            ReqIdArray.push(ids[1]);
        }
    }
    if (ReqIdArray.length > 0) {
        ReqId = ReqIdArray.toString();// ReqId.replace(/^./, '');
        CommonAjaxMethod(virtualPath + 'HRRequest/GetHRTrainingDetails', { RequIds: ReqId, inputData: 7 }, 'GET', function (response) {
            window.location.href = virtualPath + 'HR/ClubbedTrainingRequest?src=' + EncryptQueryStringJSON(ReqId);
        });
    }
}
function BindTrainingRequestProcess() {
    CommonAjaxMethod(virtualPath + 'HRRequest/GetAllTrainingRequestProcesses', null, 'GET', function (response) {
        var dataTrainingRequest = response.data.data.Table;
        var dataTrainingRequestList = response.data.data.Table1;

        $('#tbltrainingRequestProcesses').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": dataTrainingRequest,
            "pageLength": 10,
            "columnDefs": [
                {
                    "targets": [9], // Index of the column you want to hide (e.g., the 4th column)
                    "visible": false
                }
            ],
            "columns": [

                { "data": "RowNum" },
                { "data": "ReqNo" },
                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {
                        return "<label>" + ChangeDateFormatToddMMYYY(row.ReqDate) + "</label>";
                    }
                },
                { "data": "RequestedByName" },
                { "data": "Source" },
                { "data": "TypeOfTraining" },
                { "data": "NameOfTraining" },
                { "data": "Status" },
                {
                    "orderable": false,
                    "data": null,
                    "render": function (data, type, row) {
                        var strReturn = "<div class='text-center' ><a href='#' onclick='ViewProcessApplication(\"" + row.ReqID + "\")' ><i class='fas fa-eye'></i>View</a></div>";
                        return strReturn;
                    }
                },
                { "data": "ReqID" }
            ]
        });
    });
}
function BindTrainingRequestCompleted() {
    CommonAjaxMethod(virtualPath + 'HRRequest/GetAllTrainingRequestCompleted', null, 'GET', function (response) {
        var dataTrainingRequest = response.data.data.Table;
        $('#tbltrainingRequestCompleted').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": dataTrainingRequest,
            "pageLength": 10,
            "columnDefs": [
                {
                    "targets": [9], // Index of the column you want to hide (e.g., the 4th column)
                    "visible": false
                }
            ],
            "columns": [
                { "data": "RowNum" },
                { "data": "ReqNo" },
                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {
                        return "<label>" + ChangeDateFormatToddMMYYY(row.ReqDate) + "</label>";
                    }
                },
                { "data": "RequestedByName" },
                { "data": "Source" },
                { "data": "TypeOfTraining" },
                { "data": "NameOfTraining" },
                { "data": "Status" },
                {
                    "orderable": false,
                    "data": null,
                    "render": function (data, type, row) {
                        var strReturn = "<div class='text-center' ><a href='#' onclick='ViewCompletedApplication(\"" + row.ReqID + "\")'><i class='fas fa-eye'></i>View</a></div>";
                        return strReturn;
                    }
                },
                { "data": "ReqID" }
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

function HtmlPendingPaging() {
    $('#tbltrainingRequestPending').after('<div id="divPendingNav" style="text-align:right"></div>');
    var rowsShown = 15;
    var rowsTotal = $('#tbltrainingRequestPending tbody tr').length;
    var numPages = rowsTotal / rowsShown;
    for (i = 0; i < numPages; i++) {
        var pageNum = i + 1;
        $('#divPendingNav').append('<a class="e-link e-numericitem e-spacing e-currentitem e-active" href="#" rel="' + i + '">' + pageNum + '</a> ');
    }
    $('#tbltrainingRequestPending tbody tr').hide();
    $('#tbltrainingRequestPending tbody tr').slice(0, rowsShown).show();
    $('#divPendingNav a:first').addClass('active');
    $('#divPendingNav a').bind('click', function () {
        $('#divPendingNav a').removeClass('active');
        $(this).addClass('active');
        var currPage = $(this).attr('rel');
        var startItem = currPage * rowsShown;
        var endItem = startItem + rowsShown;
        $('#tbltrainingRequestPending tbody tr').css('opacity', '0.0').hide().slice(startItem, endItem).
            css('display', 'table-row').animate({ opacity: 1 }, 300);
    });
}
function HtmlProcessPaging() {
    $('#tbltrainingRequestProcesses').after('<div id="divProcessNav" style="text-align:right"></div>');
    var rowsShown = 15;
    var rowsTotal = $('#tbltrainingRequestProcesses tbody tr').length;
    var numPages = rowsTotal / rowsShown;
    for (i = 0; i < numPages; i++) {
        var pageNum = i + 1;
        $('#divProcessNav').append('<a class="e-link e-numericitem e-spacing e-currentitem e-active" href="#" rel="' + i + '">' + pageNum + '</a> ');
    }
    $('#tbltrainingRequestProcesses tbody tr').hide();
    $('#tbltrainingRequestProcesses tbody tr').slice(0, rowsShown).show();
    $('#divProcessNav a:first').addClass('active');
    $('#divProcessNav a').bind('click', function () {
        $('#divNav a').removeClass('active');
        $(this).addClass('active');
        var currPage = $(this).attr('rel');
        var startItem = currPage * rowsShown;
        var endItem = startItem + rowsShown;
        $('#tbltrainingRequestProcesses tbody tr').css('opacity', '0.0').hide().slice(startItem, endItem).
            css('display', 'table-row').animate({ opacity: 1 }, 300);
    });
}
function HtmlCompletedPaging() {
    $('#tbltrainingRequestCompleted').after('<div id="divCompletedNav" style="text-align:right"></div>');
    var rowsShown = 15;
    var rowsTotal = $('#tbltrainingRequestCompleted tbody tr').length;
    var numPages = rowsTotal / rowsShown;
    for (i = 0; i < numPages; i++) {
        var pageNum = i + 1;
        $('#divCompletedNav').append('<a class="e-link e-numericitem e-spacing e-currentitem e-active" href="#" rel="' + i + '">' + pageNum + '</a> ');
    }
    $('#tbltrainingRequestCompleted tbody tr').hide();
    $('#tbltrainingRequestCompleted tbody tr').slice(0, rowsShown).show();
    $('#divCompletedNav a:first').addClass('active');
    $('#divCompletedNav a').bind('click', function () {
        $('#divCompletedNav a').removeClass('active');
        $(this).addClass('active');
        var currPage = $(this).attr('rel');
        var startItem = currPage * rowsShown;
        var endItem = startItem + rowsShown;
        $('#tbltrainingRequestCompleted tbody tr').css('opacity', '0.0').hide().slice(startItem, endItem).
            css('display', 'table-row').animate({ opacity: 1 }, 300);
    });
}
function ViewProcessApplication(reqId) {
    if (reqId != undefined) {
        window.location.href = virtualPath + 'HR/ProcessTrainingRequest?id=' + reqId;
    }
}
function ViewCompletedApplication(reqId) {
    if (reqId != undefined) {
        window.location.href = virtualPath + 'HR/TrainingRequestCompleted?id=' + reqId;
    }
}
