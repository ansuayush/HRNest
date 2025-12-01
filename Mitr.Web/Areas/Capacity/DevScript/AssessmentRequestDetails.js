$(document).ready(function () {
    LoadMasterDropdown('ddlSkillsTagged',
        {
            ParentId: 0,
            masterTableType: 0,
            isMasterTableType: false,
            isManualTable: true,
            manualTable: 25,
            manualTableId: 0
        }, 'Select', false);
    LoadMasterDropdown('ddlSkillsGained',
        {
            ParentId: 0,
            masterTableType: 0,
            isMasterTableType: false,
            isManualTable: true,
            manualTable: 25,
            manualTableId: 0
        }, 'Select', false);
    AssessmentRequestPending();
})
var dataAssessmentArry = "";
function AssessmentRequestPending() {
     
    CommonAjaxMethod(virtualPath + 'HRRequest/GetTrainingAssessmentRequestDetails', { Id: TraingReqID, inputData: 4 }, 'GET', function (response) {
        //var TrainingUserListArry = response.data.data.Table;
        dataAssessmentArry = response.data.data.Table;
        var data = response.data.data.Table1;
        if (data != undefined) {
            document.getElementById('lblTrainingName').innerHTML = response.data.data.Table1[0].NameOfTraining;
            document.getElementById('lblTrainingType').innerHTML = response.data.data.Table1[0].TrainingTypeName;
            document.getElementById('lblMentorName').innerHTML = response.data.data.Table1[0].MentorNames;
            $('#lblToolTipMentorName').attr('data-original-title', response.data.data.Table1[0].AllMentorNames);
            $('#lblToolTipMentorName').tooltip();
        }
        if (dataAssessmentArry != undefined) {
            $('#tblAssessmentRequest').DataTable({
                "processing": true, // for show progress bar           
                "destroy": true,
                "data": dataAssessmentArry,
                "pageLength": 10,
                "columnDefs": [
                    {
                        "targets": [7], // Index of the column you want to hide (e.g., the 4th column)
                        "visible": false
                    }
                ],
                "columns": [
                    { "data": "RowNum" },
                    { "data": "emp_code" },
                    { "data": "emp_name" },
                    { "data": "DepartmentName" },
                    { "data": "design_name" },
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
                            var strReturn = "<div class='text-center' ><a href='#' onclick='ViewAssessmentRequest(\"" + row.UserTrainingID + "\")' ><i class='fas fa-eye'></i>View</a></div>";
                            return strReturn;
                        }
                    },
                    { "data": "UserTrainingID" }
                ]
            });
        }
    });
}
function AssessmentRequestCompletedList() {
    CommonAjaxMethod(virtualPath + 'HRRequest/GetAllTrainingAssessmentRequestCompleted', { Id: 0, inputData: 2 }, 'GET', function (response) {
        var dataArry = response.data.data.Table;
        $('#tblAssessmentRequestCompletedList').DataTable({
            "processing": true,
            "destroy": true,
            "data": dataArry,
            "columnDefs": [
                {
                    "targets": [5], // Index of the column you want to hide (e.g., the 4th column)
                    "visible": false
                }
            ],
            "columns": [
                { "data": "RowNum" },
                { "data": "NameOfTraining" },
                { "data": "TrainingTypeName" },
                { "data": "RequestedByName" },
                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {
                        return "<label>" + ChangeDateFormatToddMMYYY(row.createdat) + "</label>";
                    }
                },
                { "data": "ID" }
            ]
        });
    });
}

function RedirectToList() {
    window.location.href = virtualPath + 'HR/AssessmentRequest?id=' + 0;
}
function ViewAssessmentRequest(UserTrainingID) {
    if (UserTrainingID != null) {
        CommonAjaxMethod(virtualPath + 'HRRequest/GetTrainingAssessmentRequestDetails', { Id: UserTrainingID, inputData: 5 }, 'GET', function (response) {
            var data = response.data.data.Table;
            if (data != undefined) {
                document.getElementById('lblEmployeeCode').innerHTML = response.data.data.Table[0].emp_code;
                document.getElementById('lblEmployeeName').innerHTML = response.data.data.Table[0].emp_name;
                document.getElementById('lblDepartment').innerHTML = response.data.data.Table[0].DepartmentName;
                document.getElementById('lblDesignation').innerHTML = response.data.data.Table[0].design_name;
                document.getElementById('lblPTrainingName').innerHTML = response.data.data.Table[0].NameOfTraining;
                document.getElementById('lblTrainingDate').innerHTML = ChangeDateFormatToddMMYYY(response.data.data.Table[0].FromDate) + ' - ' + ChangeDateFormatToddMMYYY(response.data.data.Table[0].ToDate);
                document.getElementById('lblFeedbackByEmployee').innerHTML = response.data.data.Table[0].FeedbackForMentors;
                document.getElementById('lblMentorsName').innerHTML = response.data.data.Table[0].MentorName;
                document.getElementById('lblComment').innerHTML = response.data.data.Table[0].SpervisorComments;
                if (response.data.data.Table[0].Skills != null) {
                    let SkillsGained = response.data.data.Table[0].Skills.split(',');
                    $('#ddlSkillsGained').val(SkillsGained).trigger('change');
                }

                if (response.data.data.Table[0].SkillToBeEarned != null) {
                    let SkillsTagged = response.data.data.Table[0].SkillToBeEarned.split(',');
                    $('#ddlSkillsTagged').val(SkillsTagged).trigger('change');
                }
            }
        });
        $('#modalViewAssessment').modal('show');
    }
}
function SaveAssessmentComment() {
    var valid = true;
    if (checkValidationOnSubmit('Mandatory') == false) {
        valid = false;
    }
    if (valid == true) {
        if (dataAssessmentArry != undefined) {
            for (var i = 0; i < dataAssessmentArry.length; i++) {
                dataAssessmentArry[i].HRCommentOnAssessment = $('#txtHRCommentOnAssessment').val();
            }
        }
        var obj = {
            trainingUserLists: dataAssessmentArry
        }
        var IsSpervisorCommentsCompleted = true;

        for (var i = 0; i < dataAssessmentArry.length; i++) {
            if (dataAssessmentArry[i].SpervisorComments == null || dataAssessmentArry[i].SpervisorComments == "") {
                IsSpervisorCommentsCompleted = false;
            }
        }
        if (IsSpervisorCommentsCompleted == true) {
            CommonAjaxMethod(virtualPath + 'SupervisorRequest/SaveHRAssessmentComments', obj, 'POST', function (response) {
                FailToaster(response.SuccessMessage);
                //AssessmentRequestPending();
                RedirectToList();
            });
        }
        else {
            alert('Spervisor comments are not completed.');
        }
    }
}

