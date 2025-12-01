$(document).ready(function () {
    BindTrainingEnrolledList();
});

function BindTrainingEnrolledList() {
    CommonAjaxMethod(virtualPath + 'HRRequest/GetAllTrainingEnrolledList', null, 'GET', function (response) {
        var dataTrainingEnrolledList = response.data.data.Table;
        $('#tblTrainingEnrolledList').html('');
        var newtbblData1 = '<table id="tblTrainingEnrolled" style="text-align: left;" class="table table-striped table-bordered dt-responsive nowrap tbltick new_width dataTable no-footer " >' +
            " <thead>" +
            " <tr>" +
            "<th width='30'>S.No </th>" +
            "<th width='200'>Training Name</th>" +
            "<th width='200'>Requested By </th>" +
            "<th width='100'>From Date</th>" +
            "<th width='100'>To Date</th>" +
            "<th width='100'>From Time</th>" +
            "<th width='100'>To Time</th>" +
            "<th width='100'>Training Mode</th>" +
            "<th width='60'>Select</th>" +
            "<th width='60' >Action</th>" +
            "</tr>" +
            "</thead>";
        var html1 = "</table>";

        var tableData = "";
        for (let i = 0; i < dataTrainingEnrolledList.length; i++) {
            var FromDate = ChangeDateFormatToddMMYYY(dataTrainingEnrolledList[i].FromDate);
            var ToDate = ChangeDateFormatToddMMYYY(dataTrainingEnrolledList[i].ToDate);
            var newtbblData = "<tr><td style='display:none'>" + dataTrainingEnrolledList[i].ReqID + "</td><td>" + dataTrainingEnrolledList[i].RowNum + "</td><td>" + dataTrainingEnrolledList[i].NameOfTraining + "</td><td>" + dataTrainingEnrolledList[i].RequestedByName + "</td><td>" + FromDate + " </td><td>" + ToDate + " </td><td>" + dataTrainingEnrolledList[i].FromTime + " </td><td>" + dataTrainingEnrolledList[i].ToTime + " </td><td>" + dataTrainingEnrolledList[i].TrainingModeName + " </td><td><input onclick=\"CheckEnrolledTrainingsItems('" + i + "')\" type='checkbox' id='chk" + i + "' class='selectedId sltchk' name='select' /><label for='chk" + i + "' class='m-0'></label></td><td><a onclick=\"ViewAttendeesConfirmation('" + dataTrainingEnrolledList[i].UserTrainingID + "');\"><i class='fas fa-eye'></i>View</a></td></tr>";
            var allstring = newtbblData;
            tableData += allstring;
        }
        $('#tblTrainingEnrolledList').html(newtbblData1 + tableData + html1);
    });
    HtmlEnrolledPaging();
}
function BindTrainingConfirmedList() {
    CommonAjaxMethod(virtualPath + 'HRRequest/GetAllTrainingConfirmedList', null, 'GET', function (response) {
        var dataTrainingConfirmedList = response.data.data.Table;
        $('#tblTrainingConfirmedList').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": response.data.data.Table,
            "pageLength": 15,
            "columnDefs": [
                {
                    "targets": [9], // Index of the column you want to hide (e.g., the 4th column)
                    "visible": false
                }
            ],
            "columns": [
                { "data": "RowNum" },
                { "data": "NameOfTraining" },
                { "data": "RequestedByName" },
                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {
                        var strReturn = ChangeDateFormatToddMMYYY(row.FromDate);
                        return strReturn;
                    }
                },
                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {
                        var strReturn = ChangeDateFormatToddMMYYY(row.ToDate);
                        return strReturn;
                    }
                },
                { "data": "FromTime" },
                { "data": "ToTime" },
                { "data": "TrainingModeName" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {
                        var strReturn = '<a href="#" onclick="return ViewAttendeesConfirmed(' + row.UserTrainingID + ');" ><i class="fas fa-eye"></i>View</a>';
                        return strReturn;
                    }
                },
                { "data": "ReqID" }
            ]
        });
        //$('#tblTrainingConfirmedList').html('');
        //var newtbblData1 = '<table id="tblTrainingConfirmed" style="text-align: left;" class="table table-striped table-bordered dt-responsive nowrap tbltick new_width dataTable no-footer " >' +
        //    " <thead>" +
        //    " <tr>" +
        //    "<th width='30'>S.No </th>" +
        //    "<th width='200'>Training Name</th>" +
        //    "<th width='200'>Requested By </th>" +
        //    "<th width='100'>From Date</th>" +
        //    "<th width='100'>To Date</th>" +
        //    "<th width='100'>From Time</th>" +
        //    "<th width='100'>To Time</th>" +
        //    "<th width='100'>Training Mode</th>" +
        //    "<th width='60' >Action</th>" +
        //    "</tr>" +
        //    "</thead>";
        //var html1 = "</table>";

        //var tableData = "";
        //for (let i = 0; i < dataTrainingConfirmedList.length; i++) {
        //    var FromDate = ChangeDateFormatToddMMYYY(dataTrainingConfirmedList[i].FromDate);
        //    var ToDate = ChangeDateFormatToddMMYYY(dataTrainingConfirmedList[i].ToDate);
        //    var newtbblData = "<tr><td style='display:none'>" + dataTrainingConfirmedList[i].ReqID + "</td><td>" + dataTrainingConfirmedList[i].RowNum + "</td><td>" + dataTrainingConfirmedList[i].NameOfTraining + "</td><td>" + dataTrainingConfirmedList[i].RequestedByName + "</td><td>" + FromDate + " </td><td>" + ToDate + " </td><td>" + dataTrainingConfirmedList[i].FromTime + " </td><td>" + dataTrainingConfirmedList[i].ToTime + " </td><td>" + dataTrainingConfirmedList[i].TrainingModeName + " </td><td><a href='#' onclick=\"ViewAttendeesConfirmed('" + dataTrainingConfirmedList[i].UserTrainingID + "')\"><i class='fas fa - eye'></i>View</a></td></tr>";
        //    var allstring = newtbblData;
        //    tableData += allstring;
        //}
        //$('#tblTrainingConfirmedList').html(newtbblData1 + tableData + html1);
    });
    //HtmlConfirmedListPaging();
}
function BindTrainingFeedbackList() {
    CommonAjaxMethod(virtualPath + 'HRRequest/GetAllTrainingFeedbackList', null, 'GET', function (response) {
        var dataTrainingFeedbackList = response.data.data.Table;
        $('#tblTrainingFeedbackList').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": response.data.data.Table,
            "pageLength": 15,
            "columnDefs": [
                {
                    "targets": [9], // Index of the column you want to hide (e.g., the 4th column)
                    "visible": false
                }
            ],
            "columns": [
                { "data": "RowNum" },
                { "data": "NameOfTraining" },
                { "data": "RequestedByName" },
                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {
                        var strReturn = ChangeDateFormatToddMMYYY(row.FromDate);
                        return strReturn;
                    }
                },
                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {
                        var strReturn = ChangeDateFormatToddMMYYY(row.ToDate);
                        return strReturn;
                    }
                },
                { "data": "FromTime" },
                { "data": "ToTime" },
                { "data": "TrainingModeName" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {
                        var strReturn = '<a href="#" onclick="return ViewAttendeesFeedback(' + row.UserTrainingID + ');" ><i class="fas fa-plus"></i>Add Feedback</a>';
                        return strReturn;
                    }
                },
                { "data": "ReqID" }
            ]
        });
        //$('#tblTrainingFeedbackList').html('');
        //var newtbblData1 = '<table id="tblTrainingFeedback" style="text-align: left;" class="table table-striped table-bordered dt-responsive nowrap tbltick new_width dataTable no-footer " >' +
        //    " <thead>" +
        //    " <tr>" +
        //    "<th width='30'>S.No </th>" +
        //    "<th width='200'>Training Name</th>" +
        //    "<th width='200'>Requested By </th>" +
        //    "<th width='100'>From Date</th>" +
        //    "<th width='100'>To Date</th>" +
        //    "<th width='100'>From Time</th>" +
        //    "<th width='100'>To Time</th>" +
        //    "<th width='100'>Training Mode</th>" +
        //    "<th width='60' >Action</th>" +
        //    "</tr>" +
        //    "</thead>";
        //var html1 = "</table>";

        //var tableData = "";
        //for (let i = 0; i < dataTrainingFeedbackList.length; i++) {
        //    var FromDate = ChangeDateFormatToddMMYYY(dataTrainingFeedbackList[i].FromDate);
        //    var ToDate = ChangeDateFormatToddMMYYY(dataTrainingFeedbackList[i].ToDate);
        //    var newtbblData = "<tr><td style='display:none'>" + dataTrainingFeedbackList[i].ReqID + "</td><td>" + dataTrainingFeedbackList[i].RowNum + "</td><td>" + dataTrainingFeedbackList[i].NameOfTraining + "</td><td>" + dataTrainingFeedbackList[i].RequestedByName + "</td><td>" + FromDate + " </td><td>" + ToDate + " </td><td>" + dataTrainingFeedbackList[i].FromTime + " </td><td>" + dataTrainingFeedbackList[i].ToTime + " </td><td>" + dataTrainingFeedbackList[i].TrainingModeName + " </td><td><a href='#' onclick=\"ViewAttendeesFeedback('" + dataTrainingFeedbackList[i].UserTrainingID + "')\"><i class='fas fa - eye'></i>View</a></td></tr>";
        //    var allstring = newtbblData;
        //    tableData += allstring;
        //}
        //$('#tblTrainingFeedbackList').html(newtbblData1 + tableData + html1);
    });
    //HtmlFeedbackListPaging();
}
function BindTrainingRejectedList() {
    CommonAjaxMethod(virtualPath + 'HRRequest/GetAllTrainingRejectedList', null, 'GET', function (response) {
        var dataTrainingRejectedList = response.data.data.Table;
        $('#tblTrainingRejectedList').html('');
        var RejectedList = '<table id="tblTrainingRejected" style="text-align: left;" class="table table-striped table-bordered dt-responsive nowrap tbltick new_width dataTable no-footer " >' +
            " <thead>" +
            " <tr>" +
            "<th width='30'>S.No </th>" +
            "<th width='200'>Training Name</th>" +
            "<th width='200'>Requested By </th>" +
            "<th width='100'>From Date</th>" +
            "<th width='100'>To Date</th>" +
            "<th width='100'>From Time</th>" +
            "<th width='100'>To Time</th>" +
            "<th width='100'>Training Mode</th>" +
            "<th width='60'>Select</th>" +
            "<th width='60' >Action</th>" +
            "</tr>" +
            "</thead>";
        var Rejectedhtml = "</table></div>";

        var RejectedtableData = "";
        for (let i = 0; i < dataTrainingRejectedList.length; i++) {
            var FromDate = ChangeDateFormatToddMMYYY(dataTrainingRejectedList[i].FromDate);
            var ToDate = ChangeDateFormatToddMMYYY(dataTrainingRejectedList[i].ToDate);
            var RejectedItemData = "<tr><td style='display:none'>" + dataTrainingRejectedList[i].ReqID + "</td><td>" + dataTrainingRejectedList[i].RowNum + "</td><td>" + dataTrainingRejectedList[i].NameOfTraining + "</td><td>" + dataTrainingRejectedList[i].RequestedByName + "</td><td>" + FromDate + " </td><td>" + ToDate + " </td><td>" + dataTrainingRejectedList[i].FromTime + " </td><td>" + dataTrainingRejectedList[i].ToTime + " </td><td>" + dataTrainingRejectedList[i].TrainingModeName + " </td><td><input onclick=\"CheckRejectTrainingsItems('" + i + "')\" type='checkbox' id='chkReject" + i + "' class='selectedId sltchk' name='select' /><label for='chkReject" + i + "' class='m-0'></label></td><td><a href='#' onclick=\"ViewAttendeesRejected('" + dataTrainingRejectedList[i].UserTrainingID + "')\"><i class='fas fa - eye'></i>View</a></td></tr>";
            var allstring = RejectedItemData;
            RejectedtableData += allstring;
        }
        $('#tblTrainingRejectedList').html(RejectedList + RejectedtableData + Rejectedhtml);
    });
    HtmlRejectedListPaging();
}
function GetAllEnrolledList() {
    ReqId = "";
    BindTrainingEnrolledList();
}
function GetAllConfirmedList() {
    ReqId = "";
    BindTrainingConfirmedList();
}
function GetAllFeedbackList() {
    ReqId = "";
    BindTrainingFeedbackList();
}
function GetAllCompletedList() {
    ReqId = "";
    CommonAjaxMethod(virtualPath + 'HRRequest/GetAllTrainingFeedbackCompleted', { InputData:1 }, 'GET', function (response) {
        var dataTrainingFeedbackList = response.data.data.Table;
        $('#tblTrainingFeedbackCompletedList').DataTable({
            "processing": true,         
            "destroy": true,
            "data": response.data.data.Table,
            "pageLength": 15,
            "columnDefs": [
                {
                    "targets": [9], 
                    "visible": false
                }
            ],
            "columns": [
                { "data": "RowNum" },
                { "data": "NameOfTraining" },
                { "data": "RequestedByName" },
                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {
                        var strReturn = ChangeDateFormatToddMMYYY(row.FromDate);
                        return strReturn;
                    }
                },
                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {
                        var strReturn = ChangeDateFormatToddMMYYY(row.ToDate);
                        return strReturn;
                    }
                },
                { "data": "FromTime" },
                { "data": "ToTime" },
                { "data": "TrainingModeName" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {
                        var strReturn = '<a href="#" onclick="return ViewAttendeesFeedback(' + row.UserTrainingID + ');" ><i class="fas fa-eye"></i>View</a>';
                        return strReturn;
                    }
                },
                { "data": "ReqID" }
            ]
        });
    });
}
function GetAllRejectedList() {
    ReqId = "";
    BindTrainingRejectedList();
}

let IsSameTraningTypeId = [];

function CheckEnrolledTrainingsItems(ctrl) {
    var IsRowCheck = false;

    IsRowCheck = document.getElementById('chk' + ctrl + '').checked;
    if (IsRowCheck == true) {
        //if (ctrl > 0) {
        ctrl = parseInt(ctrl) + 1;
        ReqId = ReqId + "," + $('#tblTrainingEnrolledList tr')[ctrl].cells[0].innerHTML;
    }
}
function CheckRejectTrainingsItems(ctrl) {
    var IsRowCheck = false;
    IsRowCheck = document.getElementById('chkReject' + ctrl + '').checked;
    if (IsRowCheck == true) {
        ctrl = parseInt(ctrl) + 1;
        ReqId = ReqId + "," + $('#tblTrainingRejectedList tr')[ctrl].cells[0].innerHTML;
    }
}
function ClubbedItems(ctrl) {
     
    var IsRowCheck = false;

    IsRowCheck = document.getElementById('chk' + ctrl + '').checked;
    if (IsRowCheck == true) {
        //if (ctrl > 0) {
        ctrl = parseInt(ctrl) + 1;
        ReqId = ReqId + "," + $('#tbltrainingRequestPending tr')[ctrl].cells[0].innerHTML;

        if (IsSameTraningTypeId.length > 0) {
            var val = $('#tbltrainingRequestPending tr')[ctrl].cells[11].innerHTML;
            if (IsSameTraningTypeId[0] == val) {
                IsSameTraningTypeId.push($('#tbltrainingRequestPending tr')[ctrl].cells[11].innerHTML);
                ReqIdArray.push($('#tbltrainingRequestPending tr')[ctrl].cells[0].innerHTML);
            }
            else {
                alert('Please select same training name.')
                var row = parseInt(ctrl) - 1;
                document.getElementById('chk' + row + '').checked = false;
            }
        }
        else {
            IsSameTraningTypeId.push($('#tbltrainingRequestPending tr')[ctrl].cells[11].innerHTML);
            ReqIdArray.push($('#tbltrainingRequestPending tr')[ctrl].cells[0].innerHTML);
        }
    }
    if (IsRowCheck == false) {
        ctrl = parseInt(ctrl) + 1;
        var ReqIdPop = $('#tbltrainingRequestPending tr')[ctrl].cells[0].innerHTML;
        var IsSameTraningTypeIdPop = $('#tbltrainingRequestPending tr')[ctrl].cells[11].innerHTML;
        if (ReqIdArray.length > 0) {
            findAndRemove(ReqIdArray, ReqIdPop);
        }
        if (IsSameTraningTypeId.length > 0) {
            findAndRemove(IsSameTraningTypeId, IsSameTraningTypeIdPop);
        }
        //if (IsSameTraningTypeId.length > 0) {
        //    var val = $('#tbltrainingRequestPending tr')[ctrl].cells[11].innerHTML;
        //    if (IsSameTraningTypeId[0] == val) {
        //        IsSameTraningTypeId.pop($('#tbltrainingRequestPending tr')[ctrl].cells[11].innerHTML);
        //        ReqIdArray.pop($('#tbltrainingRequestPending tr')[ctrl].cells[0].innerHTML);
        //    }
        //    else {
        //        alert('Please select same training name.')
        //        var row = parseInt(ctrl) - 1;
        //        document.getElementById('chk' + row + '').checked = false;
        //    }
        //}
        //else {
        //    IsSameTraningTypeId.push($('#tbltrainingRequestPending tr')[ctrl].cells[11].innerHTML);
        //}   
    }
}
function findAndRemove(array, value) {
    let index = array.indexOf(value);
    if (index !== -1) {
        // Remove the element using splice
        return array.splice(index, 1)[0]; // Returns the removed element
    }
    return null; // Return null if the element is not found
}
function SelectUHNI(obj) {
    var checkAll = $(obj).is(":checked");
    if (checkAll == true) {
        ReqId = "";
        for (var i = 0; i <= $('#tbltrainingRequestPending tr').length; i++) {
            if ($('#chk' + i + '')[0] != undefined) {
                $('#chk' + i + '')[0].checked = true;
            }
            if (i > 0) {
                if ($('#tbltrainingRequestPending tr')[i] != undefined) {
                    ReqId = ReqId + "," + $('#tbltrainingRequestPending tr')[i].cells[0].innerHTML;
                    ReqIdArray.push($('#tbltrainingRequestPending tr')[i].cells[0].innerHTML);

                }
            }
        }
    }
    if (checkAll == false) {
        for (var i = 0; i < $('#tbltrainingRequestPending tr').length; i++) {
            if ($('#chk' + i + '')[0] != undefined) {
                $('#chk' + i + '')[0].checked = false;

                var val = $('#tbltrainingRequestPending tr')[i + 1].cells[11].innerHTML;
                if (IsSameTraningTypeId[0] == val) {
                    IsSameTraningTypeId.pop($('#tbltrainingRequestPending tr')[i + 1].cells[11].innerHTML);
                }
            }
        }
    }
}
function ClubbedTrainingRequest() {

    if (ReqIdArray.length > 0) {
        ReqId = ReqIdArray.toString();// ReqId.replace(/^./, '');
        window.location.href = virtualPath + 'HR/ClubbedTrainingRequest?src=' + EncryptQueryStringJSON(ReqId);
    }
}


function HtmlConfirmedListPaging() {
    $('#tblTrainingConfirmed').after('<div id="divConfirmedListNav" style="text-align:right"></div>');
    var rowsShown = 15;
    var rowsTotal = $('#tblTrainingConfirmed tbody tr').length;
    var numPages = rowsTotal / rowsShown;
    for (i = 0; i < numPages; i++) {
        var pageNum = i + 1;
        $('#divConfirmedListNav').append('<a class="e-link e-numericitem e-spacing e-currentitem e-active" href="#" rel="' + i + '">' + pageNum + '</a> ');
    }
    $('#tblTrainingConfirmed tbody tr').hide();
    $('#tblTrainingConfirmed tbody tr').slice(0, rowsShown).show();
    $('#divConfirmedListNav a:first').addClass('active');
    $('#divConfirmedListNav a').bind('click', function () {
        $('#divNav a').removeClass('active');
        $(this).addClass('active');
        var currPage = $(this).attr('rel');
        var startItem = currPage * rowsShown;
        var endItem = startItem + rowsShown;
        $('#tblTrainingConfirmed tbody tr').css('opacity', '0.0').hide().slice(startItem, endItem).
            css('display', 'table-row').animate({ opacity: 1 }, 300);
    });
}
function HtmlFeedbackListPaging() {
    $('#tblTrainingFeedback').after('<div id="divFeedbackListNav" style="text-align:right"></div>');
    var rowsShown = 15;
    var rowsTotal = $('#tblTrainingFeedback tbody tr').length;
    var numPages = rowsTotal / rowsShown;
    for (i = 0; i < numPages; i++) {
        var pageNum = i + 1;
        $('#divFeedbackListNav').append('<a class="e-link e-numericitem e-spacing e-currentitem e-active" href="#" rel="' + i + '">' + pageNum + '</a> ');
    }
    $('#tblTrainingFeedback tbody tr').hide();
    $('#tblTrainingFeedback tbody tr').slice(0, rowsShown).show();
    $('#divFeedbackListNav a:first').addClass('active');
    $('#divCompletedNav a').bind('click', function () {
        $('#divFeedbackListNav a').removeClass('active');
        $(this).addClass('active');
        var currPage = $(this).attr('rel');
        var startItem = currPage * rowsShown;
        var endItem = startItem + rowsShown;
        $('#tblTrainingFeedback tbody tr').css('opacity', '0.0').hide().slice(startItem, endItem).
            css('display', 'table-row').animate({ opacity: 1 }, 300);
    });
}
function HtmlRejectedListPaging() {
    $('#tblTrainingRejected').after('<div id="divRejectedListNav" style="text-align:right"></div>');
    var rowsShown = 15;
    var rowsTotal = $('#tblTrainingRejected tbody tr').length;
    var numPages = rowsTotal / rowsShown;
    for (i = 0; i < numPages; i++) {
        var pageNum = i + 1;
        $('#divRejectedListNav').append('<a class="e-link e-numericitem e-spacing e-currentitem e-active" href="#" rel="' + i + '">' + pageNum + '</a> ');
    }
    $('#tblTrainingRejected tbody tr').hide();
    $('#tblTrainingRejected tbody tr').slice(0, rowsShown).show();
    $('#divRejectedListNav a:first').addClass('active');
    $('#divRejectedListNav a').bind('click', function () {
        $('#divRejectedListNav a').removeClass('active');
        $(this).addClass('active');
        var currPage = $(this).attr('rel');
        var startItem = currPage * rowsShown;
        var endItem = startItem + rowsShown;
        $('#tblTrainingRejected tbody tr').css('opacity', '0.0').hide().slice(startItem, endItem).
            css('display', 'table-row').animate({ opacity: 1 }, 300);
    });
}
function HtmlEnrolledPaging() {
    $('#tblTrainingEnrolled').after('<div id="divEnrolledListNav" style="text-align:right"></div>');
    var rowsShown = 15;
    var rowsTotal = $('#tblTrainingEnrolled tbody tr').length;
    var numPages = rowsTotal / rowsShown;
    for (i = 0; i < numPages; i++) {
        var pageNum = i + 1;
        $('#divEnrolledListNav').append('<a class="e-link e-numericitem e-spacing e-currentitem e-active" href="#" rel="' + i + '">' + pageNum + '</a> ');
    }
    $('#tblTrainingEnrolled tbody tr').hide();
    $('#tblTrainingEnrolled tbody tr').slice(0, rowsShown).show();
    $('#divEnrolledListNav a:first').addClass('active');
    $('#divEnrolledListNav a').bind('click', function () {
        $('#divEnrolledListNav a').removeClass('active');
        $(this).addClass('active');
        var currPage = $(this).attr('rel');
        var startItem = currPage * rowsShown;
        var endItem = startItem + rowsShown;
        $('#tblTrainingEnrolled tbody tr').css('opacity', '0.0').hide().slice(startItem, endItem).
            css('display', 'table-row').animate({ opacity: 1 }, 300);
    });
}
function ViewProcessApplication(reqId) {
    if (reqId != undefined) {
        window.location.href = virtualPath + 'HR/ViewProcessTrainingRequest?id=' + reqId;
    }
}
function ViewAttendeesConfirmation(reqId) {
    GetTrainingRequest(reqId);
    $('#txtPopupReasonRejection').val('')
    $('#ModalAttendeesEnrolled').modal('show');
    return false;
}
var RequestId = "";
function GetTrainingRequest(reqId) {
    RequestId = reqId;
    CommonAjaxMethod(virtualPath + 'HRRequest/GetHRTrainingRequest', { id: reqId, inputData: 1 }, 'GET', function (response) {
        dataTrainingRequest = response.data.data.Table;
        dataRequestMentors = response.data.data.Table1;
        dataRequestAttachment = response.data.data.Table2;
        //dataRequestAttendees = response.data.data.Table4;
        if (dataTrainingRequest.length > 0) {
            TrainingTypeID = response.data.data.Table[0].TypeOfTraining;
            TrainingName = response.data.data.Table[0].NameOfTraining;
            document.getElementById('lblReqNo').innerHTML = response.data.data.Table3[0].ClubbedRequest;
            document.getElementById('lblReqDate').innerHTML = ChangeDateFormatToddMMYYY(response.data.data.Table[0].ReqDate)
            document.getElementById('lblReqBy').innerHTML = response.data.data.Table3[0].TagRequest;
            document.getElementById('lblFromDate').innerHTML = ChangeDateFormatToddMMYYY(response.data.data.Table[0].FromDate)
            document.getElementById('lblToDate').innerHTML = ChangeDateFormatToddMMYYY(response.data.data.Table[0].ToDate)
            document.getElementById('lblTrainingType').innerHTML = response.data.data.Table[0].TrainingType;
            document.getElementById('lblTrainingDescritpion').innerHTML = response.data.data.Table[0].TrainingDescription;
            document.getElementById('lblInstructedByHr').innerHTML = response.data.data.Table[0].InstructionsForTeam;
            if (response.data.data.Table[0].TrainingMode == 143) {
                document.getElementById('lblTrainingMode').innerHTML = "Virtual";
                document.getElementById('divPhysical').style.display = 'none';
                document.getElementById('divVirtual').style.display = 'block';
                document.getElementById("alink").href = response.data.data.Table[0].Location;
                document.getElementById("alink").innerText = response.data.data.Table[0].Location;
            }
            if (response.data.data.Table[0].TrainingMode == 145) {
                document.getElementById('lblTrainingMode').innerHTML = "Physical";
                document.getElementById('divPhysical').style.display = 'block';
                document.getElementById('divVirtual').style.display = 'none';
                document.getElementById('lblLocation').innerHTML = response.data.data.Table[0].Location;
            }
            if (response.data.data.Table[0].TrainingMode == 0) {
                document.getElementById('lblTrainingMode').innerHTML = "";
                document.getElementById('divPhysical').style.display = 'none';
                document.getElementById('divVirtual').style.display = 'none';
                document.getElementById('lblLocation').innerHTML = '';
            }
            $('#txtInstructionsForTeam').val(response.data.data.Table[0].InstructionsForTeam);
            $('#txtInstructionsForSupervisors').val(response.data.data.Table[0].InstructionsForSupervisors);
            $('#txtDateOfAssessment').val(ChangeDateFormatToddMMYYY(response.data.data.Table[0].DateOfAssessment));
            if (response.data.data.Table[0].FromTime != "" && response.data.data.Table[0].FromTime != null) {
                var actualFromTime = response.data.data.Table[0].FromTime.split(' ')[0];
                if (actualFromTime.length == 4) {
                    actualFromTime = "0" + actualFromTime;
                }
                document.getElementById('lblFromTime').innerHTML = response.data.data.Table[0].FromTime;
            }
            else {
                document.getElementById('lblFromTime').innerHTML = '';
            }
            if (response.data.data.Table[0].ToTime != "" && response.data.data.Table[0].ToTime != null) {
                var actualToTime = response.data.data.Table[0].ToTime.split(' ')[0];
                if (actualToTime.length == 4) {
                    actualToTime = "0" + actualToTime;
                }
                document.getElementById('lblToTime').innerHTML = response.data.data.Table[0].ToTime;
            }
            else {
                document.getElementById('lblToTime').innerHTML = '';
            }


        }
        //if (dataTrainingRequestCalendar.length > 0) {
        //    $('#tblSelectTrainingCalendarList').html('');
        //    var newtbblData1 = '<table class="table mt-2 " >' +
        //        ' <thead>' +
        //        ' <tr>' +
        //        ' <th width="33">S.No</th>' +
        //        ' <th width="150">Training Type</th>' +
        //        ' <th width="150">Training Name</th>' +
        //        ' <th width="150" >Training Mode</th>' +
        //        ' <th ></th>' +
        //        ' </tr>' +
        //        ' </thead>';
        //    var html1 = "</table>";
        //    var tableData = "";
        //    var i = 0;
        //    var trainingModeType
        //    for (i; i < dataTrainingRequestCalendar.length; i++) {
        //        trainingModeType = dataTrainingRequestCalendar[i].TrainingMode;// == "145" ? "Physical" : "Virtual";
        //        var newtbblData = "<tr><td>" + parseInt(i + 1) + "</td><td>" + dataTrainingRequestCalendar[i].TrainingType + "</td><td>" + dataTrainingRequestCalendar[i].TrainingName + "</td><td><select onchange=\"ChangedTrainingMode(this," + i + ")\" id=\"ddlTrainingMode_" + i + "\" class=\"form-control dpselect \" tabindex=\"-1\" aria-hidden=\"true\"></select></td><td><input type='text' id='txtlocation_'" + parseInt(i + 1) + " value=" + dataTrainingRequestCalendar[i].Location + "></input></td><td style='display: none'>" + dataTrainingRequestCalendar[i].TrainingTypeID + "</td></tr>";
        //        var allstring = newtbblData;
        //        tableData += allstring;
        //    }
        //    $('#tblSelectTrainingCalendarList').html(tableData);

        //    LoadMasterDropdown('ddlTrainingMode_0', {
        //        ParentId: 0,
        //        masterTableType: 0,
        //        isMasterTableType: false,
        //        isManualTable: true,
        //        manualTable: 27,
        //        manualTableId: 0
        //    }, 'Select', false);


        //    $('#ddlTrainingMode_0').val(trainingModeType)
        //}
        if (dataRequestMentors != undefined) {
            if (dataRequestMentors.length > 0) {
                $('#tblMentorList').html('');
                var newtbblData1 = '<table class="table mt-2 " style="width:100%">' +
                    ' <thead>' +
                    ' <tr>' +
                    ' <th width="33" style="display: none">S.No</th>' +
                    ' <th width="10" >Mentor Name</th>' +
                    ' <th width="100">Mentor Type</th>' +
                    ' <th width="150" style="display:none">Name</th>' +
                    ' <th width="150" >Email</th>' +
                    ' <th width="60" style="display:none">Action</th>' +
                    ' </tr>' +
                    ' </thead>';
                var html1 = "</table>";
                var tableData = "";
                var i = 0;
                for (i; i < dataRequestMentors.length; i++) {
                    var newtbblData = "<tr><td style='display: none'>" + parseInt(i + 1) + "</td><td style='display: none'>" + dataRequestMentors[i].MentorType + "</td><td>" + dataRequestMentors[i].MentorName + "</td><td>" + dataRequestMentors[i].MentorTypeName + "</td><td>" + dataRequestMentors[i].Email + " </td><td style=\"display:none\"> <a  title='Click to Remove' onclick='EditMentorsRows(this," + dataRequestMentors[i].RowNum + ")' class=\"red-clr\"><i class=\"fas fa-edit m-0\"></i></a><span> | </span><a  title='Click to Remove' onclick='DeleteMentorsRows(this," + dataRequestMentors[i].RowNum + ")' class=\"red-clr\"><i class=\"fas fa-trash-alt\"></i></a></td></tr>";
                    var allstring = newtbblData;
                    tableData += allstring;
                }
                $('#tblMentorList').html(newtbblData1 + tableData + html1);
            }
        }
            //if (dataRequestAttendees.length > 0) {
        //    $('#tblAddAttendeesList').html('');
        //    var newtbblData1 = '<table class="table mt-0 " >' +
        //        ' <thead>' +
        //        ' <tr>' +
        //        ' <th width="33">S.No</th>' +
        //        ' <th width="150">Attendees Names</th>' +
        //        ' <th width="150">Attendees Type</th>' +
        //        ' <th width="150" >Request Source</th>' +
        //        ' <th width="150">Request Name</th>' +
        //        ' </tr>' +
        //        ' </thead>';
        //    var html1 = "</table>";

        //    var tableData = "";
        //    for (let i = 0; i < dataRequestAttendees.length; i++) {
        //        var AttendeesTypeName = dataRequestAttendees[i].AttendeesType == 148 ? 'Mandatory' : 'Optional'
        //        var RequestName = dataRequestAttendees[i].RequestName == null ? '' : dataRequestAttendees[i].RequestName;
        //        var newtbblData = "<tr><td>" + parseInt(i + 1) + "</td><td>" + dataRequestAttendees[i].AttendeesNames + "</td><td>" + AttendeesTypeName + "</td><td>" + dataRequestAttendees[i].RequestSource + " </td><td>" + RequestName + " </td></tr>";
        //        var allstring = newtbblData;
        //        tableData += allstring;
        //    }
        //    $('#tblAddAttendeesList').html(tableData);
        //}
        if (dataRequestAttachment != undefined) {
            if (dataRequestAttachment.length > 0) {
                $('#tblAttachmentsList').html('');
                var newtbblData1 = '<table class="table mt-2 " >' +
                    ' <thead>' +
                    ' <tr>' +
                    ' <th width="33" style="display: none">S.No</th>' +
                    ' <th width="150">Attachment Type</th>' +
                    ' <th width="150">Attachment</th>' +
                    ' <th width="150" >Remark</th>' +
                    ' <th width="100" style="display:none">Action</th>' +
                    ' <th width="100" style="display:none">NewName</th>' +
                    ' <th width="100" style="display:none">FileUrl</th>' +
                    ' </tr>' +
                    ' </thead>';
                var html1 = "</table>";
                var tableData = "";
                var i = 0;
                for (i; i < dataRequestAttachment.length; i++) {
                    dataRequestAttachment[i].Remark = dataRequestAttachment[i].Remark == null ? '' : dataRequestAttachment[i].Remark;
                    var newtbblData = "<tr><td style='display:none'>" + parseInt(i + 1) + "</td><td style='display: none'>" + dataRequestAttachment[i].AttachmentType + "</td><td>" + dataRequestAttachment[i].AttachmentTypeName + "</td><td><a target='_blank' href=" + dataRequestAttachment[i].AttachmentUrl + ">" + dataRequestAttachment[i].AttachmentActualName + "</a></td><td>" + dataRequestAttachment[i].Remark + " </td><td style='display:none'> <a  title='Click to Remove' onclick='DeleteDocArrayRows(this," + dataRequestAttachment[i].RowNum + ")' class=\"red-clr\"><i class=\"fas fa-trash-alt\"></i></a></td><td style=\"display: none\">" + dataRequestAttachment[i].AttachmentNewName + "</td><td style=\"display: none\">" + dataRequestAttachment[i].AttachmentUrl + "</td></tr>";
                    var allstring = newtbblData;
                    tableData += allstring;
                }
                $('#tblAttachmentsList').html(newtbblData1 + tableData + html1);
            }
        }
    });
}
function GetConfirmationTrainingRequest(reqId) {
    RequestId = reqId;
    if (reqId != undefined && reqId != "") {
        CommonAjaxMethod(virtualPath + 'HRRequest/GetHRTrainingRequest', { id: reqId, inputData: 1 }, 'GET', function (response) {
            dataTrainingRequest = response.data.data.Table;
            dataRequestMentors = response.data.data.Table1;
            dataRequestAttachment = response.data.data.Table2;
            //dataRequestAttendees = response.data.data.Table3;
            if (dataTrainingRequest.length > 0) {
                TrainingTypeID = response.data.data.Table[0].TypeOfTraining;
                TrainingName = response.data.data.Table[0].NameOfTraining;
                document.getElementById('lblConfirmationReqNo').innerHTML = response.data.data.Table3[0].ClubbedRequest; 
                document.getElementById('lblConfirmationReqDate').innerHTML = ChangeDateFormatToddMMYYY(response.data.data.Table[0].ReqDate)
                document.getElementById('lblConfirmationTagRequest').innerHTML = response.data.data.Table3[0].TagRequest;
                document.getElementById('lblConfirmationFromDate').innerHTML = ChangeDateFormatToddMMYYY(response.data.data.Table[0].FromDate)
                document.getElementById('lblConfirmationToDate').innerHTML = ChangeDateFormatToddMMYYY(response.data.data.Table[0].ToDate)
                document.getElementById('lblConfirmationTrainingType').innerHTML = response.data.data.Table[0].TrainingType;
                document.getElementById('lblConfirmationTrainingDescritpion').innerHTML = response.data.data.Table[0].TrainingDescription;
                document.getElementById('lblConfirmationLocation').innerHTML = response.data.data.Table[0].Location;
                document.getElementById('lblConfirmationInstructedByHr').innerHTML = response.data.data.Table[0].InstructionsForTeam;
                document.getElementById('lblConfirmationReason').innerHTML = response.data.data.Table[0].RejectionReason;
                if (response.data.data.Table[0].TrainingMode == 143) {
                    document.getElementById('lblConfirmationTrainingMode').innerHTML = "Virtual";
                    document.getElementById('divConfirmationPhysical').style.display = 'none';
                    document.getElementById('divConfirmationVirtual').style.display = 'block';
                    document.getElementById("aConfirmationlink").href = response.data.data.Table[0].Location;
                    document.getElementById("aConfirmationlink").innerText = response.data.data.Table[0].Location;
                }
                if (response.data.data.Table[0].TrainingMode == 145) {
                    document.getElementById('lblConfirmationTrainingMode').innerHTML = "Physical";
                    document.getElementById('divConfirmationPhysical').style.display = 'block';
                    document.getElementById('divConfirmationVirtual').style.display = 'none';
                    document.getElementById('lblConfirmationLocation').innerHTML = response.data.data.Table[0].Location;
                }
                if (response.data.data.Table[0].TrainingMode == 0) {
                    document.getElementById('lblConfirmationTrainingMode').innerHTML = "";
                    document.getElementById('divConfirmationPhysical').style.display = 'none';
                    document.getElementById('divConfirmationVirtual').style.display = 'none';
                    document.getElementById('lblConfirmationLocation').innerHTML = '';
                }
                //$('#txtInstructionsForTeam').val(response.data.data.Table[0].InstructionsForTeam);
                //$('#txtInstructionsForSupervisors').val(response.data.data.Table[0].InstructionsForSupervisors);
                //$('#txtDateOfAssessment').val(ChangeDateFormatToddMMYYY(response.data.data.Table[0].DateOfAssessment));
                if (response.data.data.Table[0].FromTime != "" && response.data.data.Table[0].FromTime != null) {
                    var actualFromTime = response.data.data.Table[0].FromTime.split(' ')[0];
                    if (actualFromTime.length == 4) {
                        actualFromTime = "0" + actualFromTime;
                    }
                    document.getElementById('lblConfirmationFromTime').innerHTML = response.data.data.Table[0].FromTime;
                }
                else {
                    document.getElementById('lblConfirmationFromTime').innerHTML = '';
                }
                if (response.data.data.Table[0].ToTime != "" && response.data.data.Table[0].ToTime != null) {
                    var actualToTime = response.data.data.Table[0].ToTime.split(' ')[0];
                    if (actualToTime.length == 4) {
                        actualToTime = "0" + actualToTime;
                    }
                    document.getElementById('lblConfirmationToTime').innerHTML = response.data.data.Table[0].ToTime;
                }
                else {
                    document.getElementById('lblConfirmationToTime').innerHTML = '';
                }
            }

            if (dataRequestMentors.length > 0) {
                $('#tblConfirmationMentorList').html('');
                var newtbblData1 = '<table class="table mt-2 " style="width:100%">' +
                    ' <thead>' +
                    ' <tr>' +
                    ' <th width="33" style="display: none">S.No</th>' +
                    ' <th width="10" >Mentor Name</th>' +
                    ' <th width="100">Mentor Type</th>' +
                    ' <th width="150" style="display:none">Name</th>' +
                    ' <th width="150" >Email</th>' +
                    ' <th width="60" style="display:none">Action</th>' +
                    ' </tr>' +
                    ' </thead>';
                var html1 = "</table>";
                var tableData = "";
                var i = 0;
                for (i; i < dataRequestMentors.length; i++) {
                    var newtbblData = "<tr><td style='display: none'>" + parseInt(i + 1) + "</td><td style='display: none'>" + dataRequestMentors[i].MentorType + "</td><td>" + dataRequestMentors[i].MentorName + "</td><td>" + dataRequestMentors[i].MentorTypeName + "</td><td>" + dataRequestMentors[i].Email + " </td><td style=\"display:none\"> <a  title='Click to Remove' onclick='EditMentorsRows(this," + dataRequestMentors[i].RowNum + ")' class=\"red-clr\"><i class=\"fas fa-edit m-0\"></i></a><span> | </span><a  title='Click to Remove' onclick='DeleteMentorsRows(this," + dataRequestMentors[i].RowNum + ")' class=\"red-clr\"><i class=\"fas fa-trash-alt\"></i></a></td></tr>";
                    var allstring = newtbblData;
                    tableData += allstring;
                }
                $('#tblConfirmationMentorList').html(newtbblData1 + tableData + html1);
            }

            if (dataRequestAttachment.length > 0) {
                $('#tblConfirmationAttachmentsList').html('');
                var newtbblData1 = '<table class="table mt-2 " >' +
                    ' <thead>' +
                    ' <tr>' +
                    ' <th width="33" style="display: none">S.No</th>' +
                    ' <th width="150">Attachment Type</th>' +
                    ' <th width="150">Attachment</th>' +
                    ' <th width="150" >Remark</th>' +
                    ' <th width="100" style="display:none">Action</th>' +
                    ' <th width="100" style="display:none">NewName</th>' +
                    ' <th width="100" style="display:none">FileUrl</th>' +
                    ' </tr>' +
                    ' </thead>';
                var html1 = "</table>";
                var tableData = "";
                var i = 0;
                for (i; i < dataRequestAttachment.length; i++) {
                    dataRequestAttachment[i].Remark = dataRequestAttachment[i].Remark == null ? '' : dataRequestAttachment[i].Remark;
                    var newtbblData = "<tr><td style='display:none'>" + parseInt(i + 1) + "</td><td style='display: none'>" + dataRequestAttachment[i].AttachmentType + "</td><td>" + dataRequestAttachment[i].AttachmentTypeName + "</td><td><a target='_blank' href=" + dataRequestAttachment[i].AttachmentUrl + ">" + dataRequestAttachment[i].AttachmentActualName + "</a></td><td>" + dataRequestAttachment[i].Remark + " </td><td style='display:none'> <a  title='Click to Remove' onclick='DeleteDocArrayRows(this," + dataRequestAttachment[i].RowNum + ")' class=\"red-clr\"><i class=\"fas fa-trash-alt\"></i></a></td><td style=\"display: none\">" + dataRequestAttachment[i].AttachmentNewName + "</td><td style=\"display: none\">" + dataRequestAttachment[i].AttachmentUrl + "</td></tr>";
                    var allstring = newtbblData;
                    tableData += allstring;
                }
                $('#tblConfirmationAttachmentsList').html(newtbblData1 + tableData + html1);
            }
        });
    }
}
function GetRejectedTrainingRequest(reqId) {
    RequestId = reqId;
    if (reqId != undefined && reqId != "") {
        CommonAjaxMethod(virtualPath + 'HRRequest/GetHRTrainingRequest', { id: reqId, inputData: 1 }, 'GET', function (response) {
            dataTrainingRequest = response.data.data.Table;
            dataRequestMentors = response.data.data.Table1;
            dataRequestAttachment = response.data.data.Table2;
            dataRequestAttendees = response.data.data.Table3;
            if (dataTrainingRequest.length > 0) {
                TrainingTypeID = response.data.data.Table[0].TypeOfTraining;
                TrainingName = response.data.data.Table[0].NameOfTraining;
                document.getElementById('lblRejectedReqNo').innerHTML = response.data.data.Table3[0].ClubbedRequest;
                document.getElementById('lblRejectedReqDate').innerHTML = ChangeDateFormatToddMMYYY(response.data.data.Table[0].ReqDate)
                document.getElementById('lblRejectedTagRequest').innerHTML = response.data.data.Table3[0].TagRequest;
                document.getElementById('lblRejectedFromDate').innerHTML = ChangeDateFormatToddMMYYY(response.data.data.Table[0].FromDate)
                document.getElementById('lblRejectedToDate').innerHTML = ChangeDateFormatToddMMYYY(response.data.data.Table[0].ToDate)
                document.getElementById('lblRejectedTrainingType').innerHTML = response.data.data.Table[0].TrainingType;
                document.getElementById('lblRejectedTrainingDescritpion').innerHTML = response.data.data.Table[0].TrainingDescription;
                document.getElementById('lblRejectedLocation').innerHTML = response.data.data.Table[0].Location;
                document.getElementById('lblRejectedInstructedByHr').innerHTML = response.data.data.Table[0].InstructionsForTeam;
                document.getElementById('lblRejectedReason').innerHTML = response.data.data.Table[0].RejectionReason;
                if (response.data.data.Table[0].TrainingMode == 143) {
                    document.getElementById('lblRejectedTrainingMode').innerHTML = "Virtual";
                    document.getElementById('divRejectedPhysical').style.display = 'none';
                    document.getElementById('divRejectedVirtual').style.display = 'block';
                    document.getElementById("aRejectedlink").href = response.data.data.Table[0].Location;
                    document.getElementById("aRejectedlink").innerText = response.data.data.Table[0].Location;
                }
                if (response.data.data.Table[0].TrainingMode == 145) {
                    document.getElementById('lblRejectedTrainingMode').innerHTML = "Physical";
                    document.getElementById('divRejectedPhysical').style.display = 'block';
                    document.getElementById('divRejectedVirtual').style.display = 'none';
                    document.getElementById('lblRejectedLocation').innerHTML = response.data.data.Table[0].Location;
                }
                if (response.data.data.Table[0].TrainingMode == 0) {
                    document.getElementById('lblRejectedTrainingMode').innerHTML = "";
                    document.getElementById('divRejectedPhysical').style.display = 'none';
                    document.getElementById('divRejectedVirtual').style.display = 'none';
                    document.getElementById('lblRejectedLocation').innerHTML = '';
                }
                if (response.data.data.Table[0].FromTime != "" && response.data.data.Table[0].FromTime != null) {
                    var actualFromTime = response.data.data.Table[0].FromTime.split(' ')[0];
                    if (actualFromTime.length == 4) {
                        actualFromTime = "0" + actualFromTime;
                    }
                    document.getElementById('lblRejectedFromTime').innerHTML = response.data.data.Table[0].FromTime;
                }
                else {
                    document.getElementById('lblRejectedFromTime').innerHTML = '';
                }
                if (response.data.data.Table[0].ToTime != "" && response.data.data.Table[0].ToTime != null) {
                    var actualToTime = response.data.data.Table[0].ToTime.split(' ')[0];
                    if (actualToTime.length == 4) {
                        actualToTime = "0" + actualToTime;
                    }
                    document.getElementById('lblRejectedToTime').innerHTML = response.data.data.Table[0].ToTime;
                }
                else {
                    document.getElementById('lblRejectedToTime').innerHTML = '';
                }
            }

            if (dataRequestMentors.length > 0) {
                $('#tblRejectedMentorList').html('');
                var newtbblData1 = '<table class="table mt-2 " style="width:100%">' +
                    ' <thead>' +
                    ' <tr>' +
                    ' <th width="33" style="display: none">S.No</th>' +
                    ' <th width="10" >Mentor Name</th>' +
                    ' <th width="100">Mentor Type</th>' +
                    ' <th width="150" style="display:none">Name</th>' +
                    ' <th width="150" >Email</th>' +
                    ' <th width="60" style="display:none">Action</th>' +
                    ' </tr>' +
                    ' </thead>';
                var html1 = "</table>";
                var tableData = "";
                var i = 0;
                for (i; i < dataRequestMentors.length; i++) {
                    var newtbblData = "<tr><td style='display: none'>" + parseInt(i + 1) + "</td><td style='display: none'>" + dataRequestMentors[i].MentorType + "</td><td>" + dataRequestMentors[i].MentorName + "</td><td>" + dataRequestMentors[i].MentorTypeName + "</td><td>" + dataRequestMentors[i].Email + " </td><td style=\"display:none\"> <a  title='Click to Remove' onclick='EditMentorsRows(this," + dataRequestMentors[i].RowNum + ")' class=\"red-clr\"><i class=\"fas fa-edit m-0\"></i></a><span> | </span><a  title='Click to Remove' onclick='DeleteMentorsRows(this," + dataRequestMentors[i].RowNum + ")' class=\"red-clr\"><i class=\"fas fa-trash-alt\"></i></a></td></tr>";
                    var allstring = newtbblData;
                    tableData += allstring;
                }
                $('#tblRejectedMentorList').html(newtbblData1 + tableData + html1);
            }

            if (dataRequestAttachment.length > 0) {
                $('#tblRejectedAttachmentsList').html('');
                var newtbblData1 = '<table class="table mt-2 " >' +
                    ' <thead>' +
                    ' <tr>' +
                    ' <th width="33" style="display: none">S.No</th>' +
                    ' <th width="150">Attachment Type</th>' +
                    ' <th width="150">Attachment</th>' +
                    ' <th width="150" >Remark</th>' +
                    ' <th width="100" style="display:none">Action</th>' +
                    ' <th width="100" style="display:none">NewName</th>' +
                    ' <th width="100" style="display:none">FileUrl</th>' +
                    ' </tr>' +
                    ' </thead>';
                var html1 = "</table>";
                var tableData = "";
                var i = 0;
                for (i; i < dataRequestAttachment.length; i++) {
                    dataRequestAttachment[i].Remark = dataRequestAttachment[i].Remark == null ? '' : dataRequestAttachment[i].Remark;
                    var newtbblData = "<tr><td style='display:none'>" + parseInt(i + 1) + "</td><td style='display: none'>" + dataRequestAttachment[i].AttachmentType + "</td><td>" + dataRequestAttachment[i].AttachmentTypeName + "</td><td><a target='_blank' href=" + dataRequestAttachment[i].AttachmentUrl + ">" + dataRequestAttachment[i].AttachmentActualName + "</a></td><td>" + dataRequestAttachment[i].Remark + " </td><td style='display:none'> <a  title='Click to Remove' onclick='DeleteDocArrayRows(this," + dataRequestAttachment[i].RowNum + ")' class=\"red-clr\"><i class=\"fas fa-trash-alt\"></i></a></td><td style=\"display: none\">" + dataRequestAttachment[i].AttachmentNewName + "</td><td style=\"display: none\">" + dataRequestAttachment[i].AttachmentUrl + "</td></tr>";
                    var allstring = newtbblData;
                    tableData += allstring;
                }
                $('#tblRejectedAttachmentsList').html(newtbblData1 + tableData + html1);
            }
        });
    }
}
function GetFeedbackTrainingRequest(userTrainingId) {
    RequestId = userTrainingId;
    if (userTrainingId != undefined && userTrainingId != "") {
        CommonAjaxMethod(virtualPath + 'HRRequest/GetTrainingRequestFeedback', { id: userTrainingId, inputData: 1 }, 'GET', function (response) {
            dataTrainingRequest = response.data.data.Table;
            if (dataTrainingRequest != undefined) {
                if (dataTrainingRequest.length > 0) {
                    TrainingTypeID = response.data.data.Table[0].TypeOfTraining;
                    TrainingName = response.data.data.Table[0].NameOfTraining;
                    document.getElementById('lblFeedbackTrainingName').innerHTML = response.data.data.Table[0].NameOfTraining;
                    document.getElementById('lblFeedbackFromDate').innerHTML = ChangeDateFormatToddMMYYY(response.data.data.Table[0].FromDate)
                    document.getElementById('lblFeedbackToDate').innerHTML = ChangeDateFormatToddMMYYY(response.data.data.Table[0].ToDate)
                    document.getElementById('lblFeedbackMentorName').innerHTML = response.data.data.Table[0].MentorName;
                    document.getElementById('txtFeedbackForMentors').value = response.data.data.Table[0].FeedbackForMentors;
                    if (document.getElementById('txtFeedbackForMentors').value != "") {
                        $('#btnSaveFeedback').prop('disabled', true);
                        $('#txtFeedbackForMentors').attr('readonly', 'readonly');
                        document.getElementById('btnSaveFeedback').style.display = 'none';
                    }
                    else {
                        $('#txtFeedbackForMentors').attr('readonly', false);
                        $('#btnSaveFeedback').prop('disabled', false);
                        document.getElementById('btnSaveFeedback').style.display = 'block';
                    }
                }

            }
        });
    }
}
function ViewAttendeesConfirmed(reqId) {
    GetConfirmationTrainingRequest(reqId)
    $('#ModalAttendeesConfirmation').modal('show');
}
function ViewAttendeesFeedback(reqId) {
    GetFeedbackTrainingRequest(reqId);
    $('#ModalAttendeesFeedback').modal('show');
}
function ViewAttendeesRejected(reqId) {
    GetRejectedTrainingRequest(reqId);
    $('#ModalAttendeesRejected').modal('show');
}
function PopupConfirm() {
    if (RequestId != "") {
        CommonAjaxMethod(virtualPath + 'HRRequest/TrainingRequestConfirmAndRejectPopup', { id: RequestId, inputData: 1 }, 'POST', function (response) {
            BindTrainingEnrolledList();
            $('#ModalAttendeesEnrolled').modal('hide');
        });
    }
}
function PopupReject() {
    if (checkValidationOnSubmit('MandatoryPopup') == true) {
        if (RequestId != "") {
            var reason = $('#txtPopupReasonRejection').val();
            CommonAjaxMethod(virtualPath + 'HRRequest/TrainingRequestConfirmAndRejectPopup', { id: RequestId, inputData: 2, reasonForRejection: reason }, 'POST', function (response) {
                BindTrainingEnrolledList();
                $('#ModalAttendeesEnrolled').modal('hide');
            });
        }
    }
}

function EnrolledConfirm() {
    if (ReqId != "") {
        ReqId = ReqId.replace(/^./, '')
        CommonAjaxMethod(virtualPath + 'HRRequest/TrainingRequestConfirmAndRejectScreen', { id: ReqId, inputData: 3, reasonForRejection: '' }, 'POST', function (response) {
            BindTrainingEnrolledList();
        });
    }
    else {
        alert('Please select the checkbox.');
    }
}
function EnrolledReject() {
    if (checkValidationOnSubmit('Mandatory') == true) {
        if (ReqId != "") {
            ReqId = ReqId.replace(/^./, '')
            var reason = $('#txtReasonForRejection').val();
            CommonAjaxMethod(virtualPath + 'HRRequest/TrainingRequestConfirmAndRejectScreen', { id: ReqId, inputData: 4, reasonForRejection: reason }, 'POST', function (response) {
                BindTrainingEnrolledList();
                $('#txtReasonRejection').val('');
                $("#divres").toggle(); /*shows or hides #box*/
                $("#btnEdit").hide();
                document.getElementById("btnreject").innerHTML = "<i class='fas fa-ban'></i>Reject"; /*Change button text to Hide*/
                $("#btnreject").removeClass("cnl-btn")
                $("#btnreject").addClass("mtr-o-grn")
                $('#btnEnrolledConfirm').show();

            });
        }
        else {
            alert('Please select the checkbox.')
        }
    }
}
function ReconfirmTrainingRequest() {
    if (ReqId != "") {
        ConfirmMsgBox("Are you sure you want to reconfirm the request?", '', function () {

            ReqId = ReqId.replace(/^./, '')
            CommonAjaxMethod(virtualPath + 'HRRequest/TrainingRequestReconfirmScreen', { id: ReqId, inputData: 5, reasonForRejection: '' }, 'POST', function (response) {
                BindTrainingRejectedList();
            });


        })
    }
    else {
        alert('Please select the checkbox.')
    }
}
function ReconfirmTrainingRequestPopup() {
    ConfirmMsgBox("Are you sure you want to reconfirm the request?", '', function () {
        if (RequestId != "") {
            //RequestId = ReqId.replace(/^./, '')
            CommonAjaxMethod(virtualPath + 'HRRequest/TrainingRequestReconfirmScreen', { id: RequestId, inputData: 5, reasonForRejection: '' }, 'POST', function (response) {
                BindTrainingRejectedList();
                $('#ModalAttendeesRejected').modal('hide');
            });
        }
        else {
            alert('Please select the checkbox.')
        }
    })
}
function SaveTraningFeedback() {
    if (checkValidationOnSubmit('MandatoryFeedback') == true) {
        if (RequestId != "") {
            var feedback = $('#txtFeedbackForMentors').val();
            CommonAjaxMethod(virtualPath + 'HRRequest/SaveTrainingRequestFeedback', { id: RequestId, inputData: 6, reasonForRejection: feedback }, 'POST', function (response) {
                BindTrainingFeedbackList();
                $('#ModalAttendeesFeedback').modal('hide');
            });
        }
    }
}