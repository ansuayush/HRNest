$(document).ready(function () {
    FeedbackRequestsList();
})
var dataArry = "";
function FeedbackRequestsList() {
    CommonAjaxMethod(virtualPath + 'SupervisorRequest/GetALLHRFeedbackRequest', { Id: 1, EMPId: EmpID, inputData: 1 }, 'GET', function (response) {
         dataArry = response.data.data.Table;
        if (dataArry != undefined) {
            $('#tblFeedbackRequests').DataTable({
                "processing": true, // for show progress bar           
                "destroy": true,
                "data": dataArry,
                "pageLength": 15,
                "columnDefs": [
                    {
                        "targets": [10], // Index of the column you want to hide (e.g., the 4th column)
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
                    { "data": "NameOfTraining" },
                    /*{ "data": "AllMentorName" },*/
                    {
                        "orderable": false,
                        "data": null,
                        "render": function (data, type, row) {
                            var strReturn = "<div class='d-flex justify-content-between' >" + row.MentorName + "<i class='fas fa-info-circle info align-self-center' data-placement='right' data-toggle='tooltip'  title='" + row.AllMentorName + "'></i></div>";
                            return strReturn;
                        }
                    },
                    {
                        "orderable": true,
                        data: null, render: function (data, type, row) {
                            return "<label>" + ChangeDateFormatToddMMYYY(row.FromDate) + "</label>";
                        }
                    },
                    {
                        "orderable": true,
                        data: null, render: function (data, type, row) {
                            return "<label>" + ChangeDateFormatToddMMYYY(row.ToDate) + "</label>";
                        }
                    },
                    { "data": "FromTime" },
                    { "data": "ToTime" },
                    {
                        "orderable": false,
                        "data": null,
                        "render": function (data, type, row) {
                            var strReturn = "<div class='text-center' ><a href='#' onclick='AddHRAssessmentComment(\"" + row.ReqID + "\")'    ><i class='fas fa-plus'></i> Add Comments</a></div > ";
                            return strReturn;
                        }
                    },
                    { "data": "ReqID" }
                ]
            });
        }
    });
}
function FeedbackRequestsCompleted() {
    CommonAjaxMethod(virtualPath + 'SupervisorRequest/GetALLHRFeedbackRequest', { Id: 1, EMPId: EmpID, inputData: 3 }, 'GET', function (response) {
        dataArry = response.data.data.Table;
        if (dataArry != undefined) {
            $('#tblConfirmatedList').DataTable({
                "processing": true, // for show progress bar           
                "destroy": true,
                "data": dataArry,
                "pageLength": 15,
                "columnDefs": [
                    {
                        "targets": [10], // Index of the column you want to hide (e.g., the 4th column)
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
                    { "data": "NameOfTraining" },
                    /*{ "data": "AllMentorName" },*/
                    {
                        "orderable": false,
                        "data": null,
                        "render": function (data, type, row) {
                            var strReturn = "<div class='d-flex justify-content-between' >" + row.MentorName + "<i class='fas fa-info-circle info align-self-center' data-placement='right' data-toggle='tooltip'  title='" + row.AllMentorName + "'></i></div>";
                            return strReturn;
                        }
                    },
                    {
                        "orderable": true,
                        data: null, render: function (data, type, row) {
                            return "<label>" + ChangeDateFormatToddMMYYY(row.FromDate) + "</label>";
                        }
                    },
                    {
                        "orderable": true,
                        data: null, render: function (data, type, row) {
                            return "<label>" + ChangeDateFormatToddMMYYY(row.ToDate) + "</label>";
                        }
                    },
                    { "data": "FromTime" },
                    { "data": "ToTime" },
                    {
                        "orderable": false,
                        "data": null,
                        "render": function (data, type, row) {
                            var strReturn = "<div class='text-center' ><a href='#' onclick='ViewHRAssessmentComment(\"" + row.ReqID + "\")'    ><i class='fas fa-eye'></i> View</a></div > ";
                            return strReturn;
                        }
                    },
                    { "data": "ReqID" }
                ]
            });
        }
    });
}
function ConfirmatedList() {
    CommonAjaxMethod(virtualPath + 'HRRequest/GetAllTrainingCalendar', { id: 1 }, 'GET', function (response) {
        var dataArry = response.data.data.Table;
        $('#tblTrainingCalendar').DataTable({
            "processing": true,
            "destroy": true,
            "data": dataArry,
            "columns": [
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {
                        var strReturn = '<div class="radio-button"><input id="radio_' + row.id + '" name=\'radio\' type=\'radio\'></input><label for="radio_' + row.id + '" class="radio-label mb-0"></label></div>';
                        return strReturn;
                    }
                },
                { "data": "RowNum" },
                { "data": "TrainingTypeName" },
                { "data": "TrainingName" },
                { "data": "TrainingMode" },
                { "data": "TraininglinkorLocation" }
            ]
        });
    });
}
var FeedbackCommentListTable = "";
var dataArry = "";
var ALLUserFeedback = "";
function AddHRAssessmentComment(reqId) {
    $('input, textarea, select, button').prop('disabled', false);
    if (reqId != undefined && reqId != '') {
        CommonAjaxMethod(virtualPath + 'SupervisorRequest/GetALLHRFeedbackRequest', { Id: reqId, EMPId: EmpID, inputData: 2 }, 'GET', function (response) {
            ALLUserFeedback = response.data.data.Table;
            var data = response.data.data.Table1;
            if (data != undefined) {
                document.getElementById('lblTrainingName').innerHTML = response.data.data.Table1[0].NameOfTraining;
                document.getElementById('lblReqDate').innerHTML = ChangeDateFormatToddMMYYY(response.data.data.Table1[0].ReqDate);
                document.getElementById('lblMentors').innerHTML = response.data.data.Table1[0].AllMentorName;
                document.getElementById('txtaHrFeedback').value = response.data.data.Table1[0].HRFeedback;
            }
            if (ALLUserFeedback != undefined) {

                FeedbackCommentListTable = $('#tblFeedbackCommentList').DataTable({
                    "processing": true, // for show progress bar           
                    "destroy": true,
                    "data": ALLUserFeedback,
                    "pageLength": 10,
                    "columnDefs": [
                        {
                            "targets": [3, 4,5,6], // Index of the column you want to hide (e.g., the 4th column)
                            "visible": false
                        }
                    ],
                    "columns": [
                        { "data": "RowNum" },
                        { "data": "AttendeesName" },
                        { "data": "FeedbackForMentors" },
                        { "data": "UserTrainingID" },
                        { "data": "ReqID" },
                        { "data": "IsCommentsCompleted" },
                        { "data": "ClubbedID" }
                    ]
                });
                $('#AddCommentsModal').modal('show');
            }
        });
    }
    $('#btnClosed').prop('disabled', false);
    document.getElementById('btnSubmit').style.display = 'block';
}
function ViewHRAssessmentComment(reqId) {
    if (reqId != undefined && reqId != '') {
        CommonAjaxMethod(virtualPath + 'SupervisorRequest/GetALLHRFeedbackRequest', { Id: reqId, EMPId: EmpID, inputData: 2 }, 'GET', function (response) {
            ALLUserFeedback = response.data.data.Table;
            var data = response.data.data.Table1;
            if (data != undefined) {
                document.getElementById('lblTrainingName').innerHTML = response.data.data.Table1[0].NameOfTraining;
                document.getElementById('lblReqDate').innerHTML = ChangeDateFormatToddMMYYY(response.data.data.Table1[0].ReqDate);
                document.getElementById('lblMentors').innerHTML = response.data.data.Table1[0].AllMentorName;
                document.getElementById('txtaHrFeedback').value = response.data.data.Table1[0].HRFeedback; 
            }
            if (ALLUserFeedback != undefined) {

                FeedbackCommentListTable = $('#tblFeedbackCommentList').DataTable({
                    "processing": true, // for show progress bar           
                    "destroy": true,
                    "data": ALLUserFeedback,
                    "pageLength": 10,
                    "columnDefs": [
                        {
                            "targets": [3, 4, 5,6], // Index of the column you want to hide (e.g., the 4th column)
                            "visible": false
                        }
                    ],
                    "columns": [
                        { "data": "RowNum" },
                        { "data": "AttendeesName" },
                        { "data": "FeedbackForMentors" },
                        { "data": "UserTrainingID" },
                        { "data": "ReqID" },
                        { "data": "IsCommentsCompleted" },
                        { "data": "ClubbedID" }
                    ]
                });
                $('#AddCommentsModal').modal('show');
            }
        });
    }
    $('input, textarea, select, button').prop('disabled', true);
    $('#btnClosed').prop('disabled', false);
    document.getElementById('btnSubmit').style.display = 'none';
}
function SubmitComments() {
    var valid = true;
    if (checkValidationOnSubmit('Mandatory') == false) {
        valid = false;
    }
    if (valid == true) {
        var isAllUserCommentCompleted = true;
        FeedbackCommentListTable.rows().every(function (rowIdx, tableLoop, rowLoop) {
            var data = this.data();
            // Assuming we want to check the 3rd column (Office)
            if (data.FeedbackForMentors === null || data.FeedbackForMentors === '') {
                isAllUserCommentCompleted = false;
            }
        });
        if (isAllUserCommentCompleted == true) {
            for (var i = 0; i < ALLUserFeedback.length; i++) {
                ALLUserFeedback[i].HRFeedback = $('#txtaHrFeedback').val();
            }
            var obj = {
                trainingUserLists: ALLUserFeedback
            }
            CommonAjaxMethod(virtualPath + 'SupervisorRequest/SaveHrFeedbackComments', obj, 'POST', function (response) {
                FailToaster(response.SuccessMessage);
                $('#AddCommentsModal').modal('hide');
                FeedbackRequestsList();
            });
        }
        else {
            alert('Employee feedback is pending.')
        }
    }
}


