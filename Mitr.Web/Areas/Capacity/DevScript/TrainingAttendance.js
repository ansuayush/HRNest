$(document).ready(function () {
    BindTrainingRequestPending();
    BindTrainingRequestLegacy();
});
var dataTrainingAttendanceList = "";
function BindTrainingRequestPending() {
    CommonAjaxMethod(virtualPath + 'HRRequest/GetAllTrainingAttendancePending', { Id: 0, inputData: 1 }, 'GET', function (response) {
        var dataTrainingRequest = response.data.data.Table;
        $('#tblTrainingAttendancePending').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": dataTrainingRequest,
            "pageLength": 10,
            "columnDefs": [
                {
                    "targets": [12], // Index of the column you want to hide (e.g., the 4th column)
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
                { "data": "TypeOfTraining" },
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
                { "data": "TrainingModeName" },
                { "data": "Status" },
                {
                    "orderable": false,
                    "data": null,
                    "render": function (data, type, row) {
                        var strReturn = "<div class='text-center' ><a href='#' onclick='ViewTrainingAttendanceDeatils(\"" + row.ReqID + "\",\"" + row.Status + "\",\"" + row.Source + "\",\"" + false + "\")' ><i class='fas fa-eye'></i>View</a></div>";
                        return strReturn;
                    }
                },
                { "data": "ReqID" }

            ]
        });
    });
}
function BindTrainingRequestLegacy() {
    CommonAjaxMethod(virtualPath + 'HRRequest/GetAllTrainingAttendanceLegace', { Id: 0, inputData: 2 }, 'GET', function (response) {
        var dataTrainingRequest = response.data.data.Table;
        $('#tblTrainingAttendanceLegacy').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": dataTrainingRequest,
            "pageLength": 10,
            "columnDefs": [
                {
                    "targets": [11], // Index of the column you want to hide (e.g., the 4th column)
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
                { "data": "TypeOfTraining" },
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
                { "data": "ToTime" },
                { "data": "TrainingModeName" },
                { "data": "Status" },
                {
                    "orderable": false,
                    "data": null,
                    "render": function (data, type, row) {
                        var strReturn = "<div class='text-center' ><a href='#' onclick='TrainingAttendanceDeatilsView(\"" + row.ReqID + "\",\"" + row.Status + "\",\"" + row.Source + "\",\"" + true + "\")' ><i class='fas fa-eye'></i>View</a></div>";
                        return strReturn;
                    }
                },
                { "data": "ReqID" }

            ]
        });
    });
}
function ViewTrainingAttendanceDeatils(ReqID, Status, Source, IsDisable) {
    
    if (ReqID != undefined) {
        CommonAjaxMethod(virtualPath + 'HRRequest/GetTrainingAttendanceDetails', { Id: ReqID, inputData: 3 }, 'GET', function (response) {
             dataTrainingAttendanceList = response.data.data.Table;
            var dataTrainingRequest = response.data.data.Table1;
            if (dataTrainingRequest != undefined) {
                document.getElementById('lblReqNo').innerHTML = response.data.data.Table1[0].ReqNo;
                document.getElementById('lblReqDate').innerHTML = ChangeDateFormatToddMMYYY(response.data.data.Table1[0].ReqDate)
                document.getElementById('lblTrainingName').innerHTML = response.data.data.Table1[0].NameOfTraining;
                document.getElementById('lblMentorName').innerHTML = response.data.data.Table3[0].MentorName;
                document.getElementById('lblFromDate').innerHTML = ChangeDateFormatToddMMYYY(response.data.data.Table1[0].FromDate)
                document.getElementById('lblToDate').innerHTML = ChangeDateFormatToddMMYYY(response.data.data.Table1[0].ToDate)
                document.getElementById('lblTrainingType').innerHTML = response.data.data.Table1[0].TrainingType;
                $('#lblToolTipMentorName').attr('data-original-title', response.data.data.Table3[0].AllMentorName);
                $('#lblToolTipMentorName').tooltip();
                if (response.data.data.Table1[0].TrainingMode == 143) {
                    document.getElementById('lblTrainingMode').innerHTML = "Virtual";
                    document.getElementById('lblLocation').innerHTML = response.data.data.Table1[0].Location;
                }
                if (response.data.data.Table1[0].TrainingMode == 145) {
                    document.getElementById('lblTrainingMode').innerHTML = "Physical";
                    document.getElementById('lblLocation').innerHTML = response.data.data.Table1[0].Location;
                }
            }
            if (dataTrainingAttendanceList != undefined) {
                $('#tblAttendanceList').DataTable({
                    "processing": true, // for show progress bar           
                    "destroy": true,
                    "data": dataTrainingAttendanceList,
                    "pageLength": 10,
                    "columnDefs": [
                        {
                            "targets": [4, 5], // Index of the column you want to hide (e.g., the 4th column)
                            "visible": false
                        }
                    ],
                    "columns": [
                        { "data": "EMPCode" },
                        { "data": "EMPName" },
                        { "data": "AttendanceStatus" },
                        {
                            /*"data": "FinalAttendance"*/
                            "orderable": false,
                            "data": null,
                            "render": function (data, type, row) {
                                /*var strReturn = "<div><select onchange='HideErrorMessage(this)'  id='ddlFinalAttendance_" + row.RowNum + "' class='multi-select-dropdown Mandatorypld' data-select2-id='1' tabindex='-1' aria-hidden='true'></select ><span  id='spddlFinalAttendance_" + i + "' class='text-danger field-validation-error' style='display: none;'>Hey, You missed this field!!</span></div>";*/
                                var strReturn = "<div class='text-center' ><select  id='ddlFinalAttendance_" + parseInt( row.RowNum-1) + "' class='form-control'>" +
                                    "< option value = '0' > Select</option >" +
                                    "<option value='1'>P</option>" +
                                    "<option value='2' selected=''>A</option>" +
                                    "</select ></div>";
                                return strReturn;
                            }
                        },
                        { "data": "UserTrainingID" },
                        { "data": "ReqID" }
                    ]
                });

                for (var i = 0; i < response.data.data.Table.length; i++) {
                    var AttendanceStatus = response.data.data.Table[i].IsConfirm == true ? 1 : 2;
                    if (AttendanceStatus != null) {

                        $('#ddlFinalAttendance_' + i).val(AttendanceStatus).trigger('change');
                    }
                }
            }
            if (IsDisable == 'true') {
                $('input, textarea, select, button').prop('disabled', true);
                $('#btnClose').prop('disabled', false);
                document.getElementById('btnConfirm').style.display = 'none';
                for (var i = 0; i < response.data.data.Table.length; i++) {
                    $('#ddlFinalAttendance_'+i).attr('disabled', true);
                }
            }
            else {
                $('input, textarea, select, button').prop('disabled', false);
                $('#btnClose').prop('disabled', false);
                document.getElementById('btnConfirm').style.display = 'block';
                for (var i = 0; i < response.data.data.Table.length; i++) {
                    $('#ddlFinalAttendance_' + i).attr('disabled', false);
                }
            }
            $('#modalTrainingAttendance').modal('show');
        });
    }
    else {
        alert('Request id is empty or null');
    }
}
function TrainingAttendanceDeatilsView(ReqID, Status, Source, IsDisable) {

    if (ReqID != undefined) {
        CommonAjaxMethod(virtualPath + 'HRRequest/GetTrainingAttendanceDetails', { Id: ReqID, inputData: 3 }, 'GET', function (response) {
            dataTrainingAttendanceList = response.data.data.Table;
            var dataTrainingRequest = response.data.data.Table1;
            if (dataTrainingRequest != undefined) {
                document.getElementById('lblReqNo').innerHTML = response.data.data.Table1[0].ReqNo;
                document.getElementById('lblReqDate').innerHTML = ChangeDateFormatToddMMYYY(response.data.data.Table1[0].ReqDate)
                document.getElementById('lblTrainingName').innerHTML = response.data.data.Table1[0].NameOfTraining;
                document.getElementById('lblMentorName').innerHTML = response.data.data.Table3[0].MentorName;
                document.getElementById('lblFromDate').innerHTML = ChangeDateFormatToddMMYYY(response.data.data.Table1[0].FromDate)
                document.getElementById('lblToDate').innerHTML = ChangeDateFormatToddMMYYY(response.data.data.Table1[0].ToDate)
                document.getElementById('lblTrainingType').innerHTML = response.data.data.Table1[0].TrainingType;
                $('#lblToolTipMentorName').attr('data-original-title', response.data.data.Table3[0].AllMentorName);
                $('#lblToolTipMentorName').tooltip();
                if (response.data.data.Table1[0].TrainingMode == 143) {
                    document.getElementById('lblTrainingMode').innerHTML = "Virtual";
                    document.getElementById('lblLocation').innerHTML = response.data.data.Table1[0].Location;
                }
                if (response.data.data.Table1[0].TrainingMode == 145) {
                    document.getElementById('lblTrainingMode').innerHTML = "Physical";
                    document.getElementById('lblLocation').innerHTML = response.data.data.Table1[0].Location;
                }
            }
            if (dataTrainingAttendanceList != undefined) {
                $('#tblAttendanceList').DataTable({
                    "processing": true, // for show progress bar           
                    "destroy": true,
                    "data": dataTrainingAttendanceList,
                    "pageLength": 10,
                    "columnDefs": [
                        {
                            "targets": [4, 5], // Index of the column you want to hide (e.g., the 4th column)
                            "visible": false
                        }
                    ],
                    "columns": [
                        { "data": "EMPCode" },
                        { "data": "EMPName" },
                        { "data": "AttendanceStatus" },
                        {
                            /*"data": "FinalAttendance"*/
                            "orderable": false,
                            "data": null,
                            "render": function (data, type, row) {
                                /*var strReturn = "<div><select onchange='HideErrorMessage(this)'  id='ddlFinalAttendance_" + row.RowNum + "' class='multi-select-dropdown Mandatorypld' data-select2-id='1' tabindex='-1' aria-hidden='true'></select ><span  id='spddlFinalAttendance_" + i + "' class='text-danger field-validation-error' style='display: none;'>Hey, You missed this field!!</span></div>";*/
                                var strReturn = "<div class='text-center' ><select  id='ddlFinalAttendance_" + parseInt(row.RowNum - 1) + "' class='form-control'>" +
                                    "< option value = '0' > Select</option >" +
                                    "<option value='1'>P</option>" +
                                    "<option value='2' selected=''>A</option>" +
                                    "</select ></div>";
                                return strReturn;
                            }
                        },
                        { "data": "UserTrainingID" },
                        { "data": "ReqID" }
                    ]
                });

                for (var i = 0; i < response.data.data.Table.length; i++) {
                    var AttendanceStatus = response.data.data.Table[i].FinalAttendance == true ? 1 : 2;
                    if (AttendanceStatus != null) {

                        $('#ddlFinalAttendance_' + i).val(AttendanceStatus).trigger('change');
                    }
                }
            }
            if (IsDisable == 'true') {
                $('input, textarea, select, button').prop('disabled', true);
                $('#btnClose').prop('disabled', false);
                document.getElementById('btnConfirm').style.display = 'none';
                for (var i = 0; i < response.data.data.Table.length; i++) {
                    $('#ddlFinalAttendance_' + i).attr('disabled', true);
                }
            }
            else {
                $('input, textarea, select, button').prop('disabled', false);
                $('#btnClose').prop('disabled', false);
                document.getElementById('btnConfirm').style.display = 'block';
                for (var i = 0; i < response.data.data.Table.length; i++) {
                    $('#ddlFinalAttendance_' + i).attr('disabled', false);
                }
            }
            $('#modalTrainingAttendance').modal('show');
        });
    }
    else {
        alert('Request id is empty or null');
    }
}
function SaveFinalConfirm() {
    if (dataTrainingAttendanceList != undefined) {
        for (var i = 0; i < dataTrainingAttendanceList.length; i++) {
            dataTrainingAttendanceList[i].FinalAttendance = $('#ddlFinalAttendance_' + i).val();
        }
    }
    var obj = {
        trainingUserLists: dataTrainingAttendanceList
    }
    CommonAjaxMethod(virtualPath + 'SupervisorRequest/SaveFinalAttendance', obj, 'POST', function (response) {
        FailToaster(response.SuccessMessage);
        $('#modalTrainingAttendance').modal('hide');
        BindTrainingRequestPending();
    });
}
