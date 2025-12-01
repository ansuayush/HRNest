$(document).ready(function () {
    AssessmentRequestPending();
    //AssessmentRequestCompleted();
})

function AssessmentRequestPending() {
    CommonAjaxMethod(virtualPath + 'HRRequest/GetHRAssessmentRequestPending', { Id: 0, EMPId: EmpID, inputData: 1 }, 'GET', function (response) {
        var dataArry = response.data.data.Table;
        if (dataArry != undefined) {
            AssessmentRequestList = $('#tblAssessmentRequestList').DataTable({
                "processing": true, // for show progress bar           
                "destroy": true,
                "data": dataArry,
                "pageLength": 15,
                columnDefs: [
                    { targets: [9], visible: false } // Hides the Employee ID column (index 6)
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
                    { "data": "TrainingTypeName" },
                    
                    /*{ "data": "AttendeesName" },*/
                    {
                        "orderable": false,
                        "data": null,
                        "render": function (data, type, row) {
                            var strReturn = "<div class='d-flex justify-content-between' >" + row.AttendeesName + "<i class='fas fa-info-circle info align-self-center' data-placement='right' data-toggle='tooltip'  title='" + row.AllAttendeesName + "'></i></div>";
                            return strReturn;
                        }
                    },
                    { "data": "Mentors" },
                    {
                        "orderable": true,
                        data: null, render: function (data, type, row) {
                            return "<label>" + ChangeDateFormatToddMMYYY(row.FromDate) +' - '+ ChangeDateFormatToddMMYYY(row.ToDate)+ "</label>";
                        }
                    },
                    {
                        "orderable": false,
                        "data": null,
                        "render": function (data, type, row) {
                            var ReqID = "";
                            ReqID = row.ClubbedID == null ? row.ReqID : row.ClubbedID;
                            var strReturn = "";
                            if (row.DateOfAssessmentStatus == 1) {
                                var strReturn = "<div class='text-center' ><a  href='#' onclick='ViewAssessmentRequest(\"" + row.ReqID + "\")'  ><i class='fas fa-plus'></i>Add Comment</a></div>";
                            }
                            //else {
                            //    var strReturn = "<div class='text-center' ><a  href='#' ><i class='fas fa-eye'></i>Add Comment</a></div>";
                            //}
                            return strReturn;
                        }
                    },
                    { "data": "ReqID" }
                ]
            });
        }
    });
}
function AssessmentRequestCompletedList() {
    CommonAjaxMethod(virtualPath + 'HRRequest/GetHRAssessmentRequestPending', { Id: 0, EMPId: EmpID, inputData: 5 }, 'GET', function (response) {
        var dataArry = response.data.data.Table;
        $('#tblAssessmentRequestCompletedList').DataTable({
            "processing": true,
            "destroy": true,
            "pageLength": 15,
            "data": dataArry,
            "pageLength": 15,
            columnDefs: [
                { targets: [9], visible: false } // Hides the Employee ID column (index 6)
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
                { "data": "TrainingTypeName" },

                /*{ "data": "AttendeesName" },*/
                {
                    "orderable": false,
                    "data": null,
                    "render": function (data, type, row) {
                        var strReturn = "<div class='d-flex justify-content-between' >" + row.AttendeesName + "<i class='fas fa-info-circle info align-self-center' data-placement='right' data-toggle='tooltip'  title='" + row.AllAttendeesName + "'></i></div>";
                        return strReturn;
                    }
                },
                { "data": "Mentors" },
                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {
                        return "<label>" + ChangeDateFormatToddMMYYY(row.FromDate) + ' - ' + ChangeDateFormatToddMMYYY(row.ToDate) + "</label>";
                    }
                },
                {
                    "orderable": false,
                    "data": null,
                    "render": function (data, type, row) {
                        var ReqID = "";
                        ReqID = row.ClubbedID == null ? row.ReqID : row.ClubbedID;
                        var strReturn = "";
                        if (row.DateOfAssessmentStatus == 1) {
                            var strReturn = "<div class='text-center' ><a  href='#' onclick='ViewAssessmentRequestView(\"" + row.ReqID + "\")'  ><i class='fas fa-eye'></i>View</a></div>";
                        }
                        //else {
                        //    var strReturn = "<div class='text-center' ><a  href='#' ><i class='fas fa-eye'></i>Add Comment</a></div>";
                        //}
                        return strReturn;
                    }
                },
                { "data": "ReqID" }
            ]
        });
    });
}


