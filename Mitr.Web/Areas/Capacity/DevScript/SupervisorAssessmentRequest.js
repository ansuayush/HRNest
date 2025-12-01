$(document).ready(function () {
    AssessmentRequestPending();
    //AssessmentRequestCompleted();
    //document.getElementById('btnSupervisorCommentsSubmit').style.display = 'block';
})
var AssessmentRequestList = "";
function AssessmentRequestPending() {
    CommonAjaxMethod(virtualPath + 'SupervisorRequest/GetSupervisorAssessmentRequestPending', { Id: 0, EMPId: EmpID, inputData: 1 }, 'GET', function (response) {
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
                    { "data": "TrainingTypeName" },
                    { "data": "NameOfTraining" },
                    { "data": "Mentors" },
                    /*{ "data": "AttendeesName" },*/
                    {
                        "orderable": false,
                        "data": null,
                        "render": function (data, type, row) {
                            var strReturn = "<div class='d-flex justify-content-between' >" + row.AttendeesName + "<i class='fas fa-info-circle info align-self-center' data-placement='right' data-toggle='tooltip'  title='" + row.AllAttendeesName + "'></i></div>";
                            return strReturn;
                        }
                    },
                    {
                        "orderable": true,
                        data: null, render: function (data, type, row) {
                            return "<label>" + ChangeDateFormatToddMMYYY(row.DateOfAssessment) + "</label>";
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
                                var strReturn = "<div class='text-center' ><a  href='#' onclick='ViewSupervisorAssessmentComment(\"" + row.ReqID + "\")'  ><i class='fas fa-plus'></i>Add Comment</a></div>";
                            }
                            //else {
                            //    var strReturn = "<div class='text-center' ><a  href='#' ><i class='fas fa-eye'></i>Add Comment</a></div>";
                            //}
                            return strReturn;
                        }
                    },
                    { "data": "ReqID" }
                    //{ "data": "ClubbedID" },
                    //{ "data": "UserTrainingID" }
                ]
            });
        }
    });
}
function BindTrainingRequestCompleted() {
    CommonAjaxMethod(virtualPath + 'SupervisorRequest/GetSupervisorAssessmentRequestCompleted', { Id: 0, EMPId: EmpID, inputData: 3 }, 'GET', function (response) {
        var dataArry = response.data.data.Table;
        if (dataArry != undefined) {
            AssessmentRequestList = $('#tblAssessmentRequestCompletedList').DataTable({
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
                    { "data": "TrainingTypeName" },
                    { "data": "NameOfTraining" },
                    { "data": "Mentors" },
                    /*{ "data": "AttendeesName" },*/
                    {
                        "orderable": false,
                        "data": null,
                        "render": function (data, type, row) {
                            var strReturn = "<div class='d-flex justify-content-between' >" + row.AttendeesName + "<i class='fas fa-info-circle info align-self-center' data-placement='right' data-toggle='tooltip' title='" + row.AllAttendeesName + "' data-original-title=''></i></div>";
                            return strReturn;
                        }
                    },
                    {
                        "orderable": true,
                        data: null, render: function (data, type, row) {
                            return "<label>" + ChangeDateFormatToddMMYYY(row.DateOfAssessment) + "</label>";
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
                                var strReturn = "<div class='text-center' ><a  href='#' onclick='ViewSupervisorAssessmentCompletedView(\"" + row.ReqID + "\")'  ><i class='fas fa-eye'></i>View</a></div>";
                            }
                            //else {
                            //    var strReturn = "<div class='text-center' ><a  href='#' ><i class='fas fa-eye'></i>Add Comment</a></div>";
                            //}
                            return strReturn;
                        }
                    },
                    { "data": "ReqID" }
                    //{ "data": "ClubbedID" },
                    //{ "data": "UserTrainingID" }
                ]
            });
        }
    });
}

function SaveSupervisorComments() {
    for (var i = 0; i < EmployeeNamesList.length; i++) {
        var Skills = EmployeeNamesList[i].Skills;
        if ($('#txtAreaComment_' + i).val().length < 50) {
            document.getElementById('error-message_' + i).style.display = 'block';
            return false;
        }
    }
    var valid = true;
    if (checkValidationOnSubmit('Mandatory') == false) {
        valid = false;
    }
    if (checkValidationOnSubmit('Mandatorypld') == false) {
        valid = false;
    }
    if (valid == true) {
        if (EmployeeNamesList != undefined) {
            for (var i = 0; i < EmployeeNamesList.length; i++) {
                EmployeeNamesList[i].SpervisorComments = $('#txtAreaComment_' + i).val();
                EmployeeNamesList[i].Skills = $('#ddlSkills_' + i).val().join();
            }
        }
        var obj = {
            trainingUserLists: EmployeeNamesList
        }
        CommonAjaxMethod(virtualPath + 'SupervisorRequest/SaveSupervisorComments', obj, 'POST', function (response) {
            FailToaster(response.SuccessMessage);
            $('#ModalSupervisorAssessmentComment').modal('hide');
            AssessmentRequestPending();
        });
    }
}
var EmployeeNamesList = [];
function ViewSupervisorAssessmentComment(reqId) {
    $('input, textarea, select, button').prop('disabled', false);
    CommonAjaxMethod(virtualPath + 'SupervisorRequest/GetSupervisorAssessmentRequest', { Id: reqId, EMPId: EmpID, inputData: 2 }, 'GET', function (response) {
        var dataArry = response.data.data.Table;
        EmployeeNamesList = response.data.data.Table1
        if (dataArry != undefined) {
            document.getElementById('lblTrainingName').innerHTML = response.data.data.Table[0].NameOfTraining;
            document.getElementById('lblFromDate').innerHTML = ChangeDateFormatToddMMYYY(response.data.data.Table[0].FromDate);
            document.getElementById('lblToDate').innerHTML = ChangeDateFormatToddMMYYY(response.data.data.Table[0].ToDate);
            document.getElementById('lblMentorNames').innerHTML = response.data.data.Table[0].MentorNames;
            document.getElementById('lblInstructedByHR').innerHTML = response.data.data.Table[0].InstructionsForSupervisors;
            $('#ModalSupervisorAssessmentComment').modal('show');
        }
        //if (EmployeeNamesList.length > 0 && EmployeeNamesList != undefined) {
            
            $('#tblEmployeeNames').html('');
            var tableData = "";
            var i = 0;
            for (i; i < EmployeeNamesList.length; i++) {
                var newtbblData = "<tr><td>" + EmployeeNamesList[i].RowNum + "</td><td >" + EmployeeNamesList[i].AttendeesName + "</td><td><div class='text-center'><textarea maxlength='200' onchange='HideErrorMessage(this);HideLengthErrorMessage(this,"+i+");' minlength='50'  id='txtAreaComment_" + i + "' value='" + EmployeeNamesList[i].SpervisorComments + "' class='form-control maxSize[200] Mandatory' placeholder='Enter'></textarea><span id='sptxtAreaComment_" + i + "' class='text-danger field-validation-error' style='display:none;'>Hey, You missed this field!!</span><p class='text-danger field-validation-error' id='error-message_" + i + "' style='color: red; display: none;font-size: 8px;'>Minimum 50 characters required.</p></div></td><td><div><select onchange='HideErrorMessage(this)'  id='ddlSkills_" + i + "' multiple='multiple' class='multi-select-dropdown Mandatorypld' data-select2-id='1' tabindex='-1' aria-hidden='true'></select ><span  id='spddlSkills_" + i + "' class='text-danger field-validation-error' style='display: none;'>Hey, You missed this field!!</span></div></td><td style='display: none' >" + EmployeeNamesList[i].ReqID + "</td><td style='display: none' >" + EmployeeNamesList[i].ClubbedID + "</td><td style='display: none' >" + EmployeeNamesList[i].UserTrainingID + "</td></tr>";
                var allstring = newtbblData;
                tableData += allstring;
            }
            $('#tblEmployeeNames').html(tableData);
        //}
        if (response.data.data.Table1.length > 0) {
            for (var i = 0; i < response.data.data.Table1.length; i++) {
                $('.multi-select-dropdown').select2({
                    placeholder: "Select options",
                    allowClear: true
                });

                // Load options dynamically for each multi-select dropdown
                $('.multi-select-dropdown').each(function () {
                    var $this = $(this);
                    LoadMasterDropdown('ddlSkills_' + i,
                        {
                            ParentId: 0,
                            masterTableType: 0,
                            isMasterTableType: false,
                            isManualTable: true,
                            manualTable: 25,
                            manualTableId: 0
                        }, 'Select', false);
                });

            }
            for (var i = 0; i < response.data.data.Table1.length; i++) {
                var Skills = response.data.data.Table1[i].Skills;
                if (Skills != null) {
                    var strArr = Skills.split(',');
                    if (strArr.length > 0) {
                        $("#ddlSkills_" + i).select2({
                            multiple: true,
                        });
                        $('#ddlSkills_' + i).val(strArr).trigger('change');
                    }
                }
                $('#txtAreaComment_' + i).val(response.data.data.Table1[i].SpervisorComments);

            }
        }
    });
    document.getElementById('btnSupervisorCommentsSubmit').style.display = 'block';
}
function ViewSupervisorAssessmentCompleted(reqId) {
    CommonAjaxMethod(virtualPath + 'SupervisorRequest/GetSupervisorAssessmentRequest', { Id: reqId, EMPId: EmpID, inputData: 2 }, 'GET', function (response) {
        var dataArry = response.data.data.Table;
        EmployeeNamesList = response.data.data.Table1
        if (dataArry != undefined) {
            document.getElementById('lblTrainingName').innerHTML = response.data.data.Table[0].NameOfTraining;
            document.getElementById('lblFromDate').innerHTML = ChangeDateFormatToddMMYYY(response.data.data.Table[0].FromDate);
            document.getElementById('lblToDate').innerHTML = ChangeDateFormatToddMMYYY(response.data.data.Table[0].ToDate);
            document.getElementById('lblMentorNames').innerHTML = response.data.data.Table[0].MentorNames;
            $('#ModalSupervisorAssessmentComment').modal('show');
        }
        if (EmployeeNamesList != undefined) {
            $('#tblEmployeeNames').html('');
            var tableData = "";
            var i = 0; 
            for (i; i < EmployeeNamesList.length; i++) {
                var newtbblData = "<tr><td>" + EmployeeNamesList[i].RowNum + "</td><td >" + EmployeeNamesList[i].AttendeesName + "</td><td><div class='text-center'><textarea onchange='HideErrorMessage(this)' maxlength='200' id='txtAreaComment_" + i + "' value='" + EmployeeNamesList[i].SpervisorComments + "' class='form-control maxSize[200] Mandatory' placeholder='Enter'></textarea><span id='sptxtAreaComment_" + i + "' class='text-danger field-validation-error' style='display:none;'>Hey, You missed this field!!</span></div></td><td><div><select  id='ddlSkills_" + i + "' multiple='multiple' onchange='HideErrorMessage(this);' class='multi-select-dropdown Mandatorypld' data-select2-id='1' tabindex='-1' aria-hidden='true'></select ><span  id='spddlSkills_" + i + "' class='text-danger field-validation-error' style='display: none;'>Hey, You missed this field!!</span></div></td><td style='display: none' >" + EmployeeNamesList[i].ReqID + "</td><td style='display: none' >" + EmployeeNamesList[i].ClubbedID + "</td><td style='display: none' >" + EmployeeNamesList[i].UserTrainingID + "</td></tr>";
                var allstring = newtbblData;
                tableData += allstring;
            }
            $('#tblEmployeeNames').html(tableData);
        }
        if (response.data.data.Table1.length > 0) {
            for (var i = 0; i < response.data.data.Table1.length; i++) {
                $('.multi-select-dropdown').select2({
                    placeholder: "Select options",
                    allowClear: true
                });

                // Load options dynamically for each multi-select dropdown
                $('.multi-select-dropdown').each(function () {
                    var $this = $(this);
                    LoadMasterDropdown('ddlSkills_' + i,
                        {
                            ParentId: 0,
                            masterTableType: 0,
                            isMasterTableType: false,
                            isManualTable: true,
                            manualTable: 25,
                            manualTableId: 0
                        }, 'Select', false);
                });

            }
            for (var i = 0; i < response.data.data.Table1.length; i++) {
                var Skills = response.data.data.Table1[i].Skills;
                if (Skills != null) {
                    var strArr = Skills.split(',');
                    if (strArr.length > 0) {
                        $("#ddlSkills_" + i).select2({
                            multiple: true,
                        });
                        $('#ddlSkills_' + i).val(strArr).trigger('change');
                    }
                }
                $('#txtAreaComment_' + i).val(response.data.data.Table1[i].SpervisorComments);

            }
        }
        $('input, textarea, select, button').prop('disabled', true);
        $('#btnSupervisorCommentsClose').prop('disabled', false);
        document.getElementById('btnSupervisorCommentsSubmit').style.display = 'block';
    });
}
function ViewSupervisorAssessmentCompletedView(reqId) {
    CommonAjaxMethod(virtualPath + 'SupervisorRequest/GetSupervisorAssessmentRequest', { Id: reqId, EMPId: EmpID, inputData: 2 }, 'GET', function (response) {
        var dataArry = response.data.data.Table;
        EmployeeNamesList = response.data.data.Table1
        if (dataArry != undefined) {
            document.getElementById('lblTrainingName').innerHTML = response.data.data.Table[0].NameOfTraining;
            document.getElementById('lblFromDate').innerHTML = ChangeDateFormatToddMMYYY(response.data.data.Table[0].FromDate);
            document.getElementById('lblToDate').innerHTML = ChangeDateFormatToddMMYYY(response.data.data.Table[0].ToDate);
            document.getElementById('lblMentorNames').innerHTML = response.data.data.Table[0].MentorNames;
            document.getElementById('lblInstructedByHR').innerHTML = response.data.data.Table[0].InstructionsForSupervisors;
            $('#ModalSupervisorAssessmentComment').modal('show');
        }
        if (EmployeeNamesList != undefined) {
            $('#tblEmployeeNames').html('');
            var tableData = "";
            var i = 0; 
            for (i; i < EmployeeNamesList.length; i++) {
                var newtbblData = "<tr><td>" + EmployeeNamesList[i].RowNum + "</td><td >" + EmployeeNamesList[i].AttendeesName + "</td><td><div class='text-center'><textarea onchange='HideErrorMessage(this)' maxlength='200' id='txtAreaComment_" + i + "' value='" + EmployeeNamesList[i].SpervisorComments + "' class='form-control maxSize[200] Mandatory' placeholder='Enter'></textarea><span id='sptxtAreaComment_" + i + "' class='text-danger field-validation-error' style='display:none;'>Hey, You missed this field!!</span></div></td><td><div><select  id='ddlSkills_" + i + "' multiple='multiple' onchange='HideErrorMessage(this);' class='multi-select-dropdown Mandatorypld' data-select2-id='1' tabindex='-1' aria-hidden='true'></select ><span  id='spddlSkills_" + i + "' class='text-danger field-validation-error' style='display: none;'>Hey, You missed this field!!</span></div></td><td style='display: none' >" + EmployeeNamesList[i].ReqID + "</td><td style='display: none' >" + EmployeeNamesList[i].ClubbedID + "</td><td style='display: none' >" + EmployeeNamesList[i].UserTrainingID + "</td></tr>";
                var allstring = newtbblData;
                tableData += allstring;
            }
            $('#tblEmployeeNames').html(tableData);
        }
        if (response.data.data.Table1.length > 0) {
            for (var i = 0; i < response.data.data.Table1.length; i++) {
                $('.multi-select-dropdown').select2({
                    placeholder: "Select options",
                    allowClear: true
                });

                // Load options dynamically for each multi-select dropdown
                $('.multi-select-dropdown').each(function () {
                    var $this = $(this);
                    LoadMasterDropdown('ddlSkills_' + i,
                        {
                            ParentId: 0,
                            masterTableType: 0,
                            isMasterTableType: false,
                            isManualTable: true,
                            manualTable: 25,
                            manualTableId: 0
                        }, 'Select', false);
                });

            }
            for (var i = 0; i < response.data.data.Table1.length; i++) {
                var Skills = response.data.data.Table1[i].Skills;
                if (Skills != null) {
                    var strArr = Skills.split(',');
                    if (strArr.length > 0) {
                        $("#ddlSkills_" + i).select2({
                            multiple: true,
                        });
                        $('#ddlSkills_' + i).val(strArr).trigger('change');
                    }
                }
                $('#txtAreaComment_' + i).val(response.data.data.Table1[i].SpervisorComments);

            }
        }
        $('input, textarea, select, button').prop('disabled', true);
        $('#btnSupervisorCommentsClose').prop('disabled', false);
        document.getElementById('btnSupervisorCommentsSubmit').style.display = 'none';
    });
}
function HideLengthErrorMessage(ctrl,i) {
     
    if ($('#' + ctrl.id).val().length < 50) {
         
        document.getElementById('error-message_' + i).style.display = 'block';
    }
    else {
        document.getElementById('error-message_' + i).style.display = 'none';
    }
}