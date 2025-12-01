$(document).ready(function () {

    BindScreen()


});

function BindScreen() {
    debugger;
    var id = $("#hdfEMPID").val();
    var DocType = $("#hdfdoctype").val();
    var model = {
        EMPID: id,
        DocType: DocType

    }

    const jsonString = JSON.stringify(model);
    var ScreenID = "NOCRequestList";
    CommonAjaxMethod(virtualPath + 'Generic/GetRecords', { modelData: jsonString, screenId: ScreenID }, 'GET', function (response) {


        var tableIdP = '#tabelpending';
        var tablePending = $('#tabelpending').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": response.data.data.Table,
            "stateSave": true, // Enable state saving
            "columns": [
                { "data": "RowNumber" },
                { "data": "emp_code" },
                { "data": "emp_name" },
                { "data": "Location" },
                { "data": "Department" },
                { "data": "Designation" },
                { "data": "relievingdate" },
                //  { "data": "relievingday" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {

                        var strReturn = "";
                        if (row.Daysleft <= 7) {
                            strReturn = "<strong class='badge badge - secondary mtr - y - bg'>" + row.Daysleft + " Days</strong> ";
                        }
                        else {
                            strReturn = "<strong class='badge badge - secondary'>" + row.Daysleft + " Days</strong> ";

                        }


                        return strReturn;
                    }
                },
                {
                    "orderable": false,
                    "data": null,
                    "render": function (data, type, row) {
                        var strReturn = '<strong><a href="#" data-toggle="modal" data-target="#hv" onclick="ViewData(' + row.ID + ')"><i class="fas fa-eye"></i>View</a></strong>';
                        return strReturn;
                    }
                }

            ]
            ,

        });
        tablePending.destroy();

        // Initialize tooltips for the initial set of rows
        $('[data-toggle="tooltip"]').tooltip();

        //  Re-initialize tooltips every time the table is redrawn
        tablePending.on('draw.dt', function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        DatatableScriptsWithColumnSearch(tableIdP.substring(1), tablePending);



        var tableIdA = '#tabelapproved';
        var tabelapproved = $('#tabelapproved').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": response.data.data.Table1,
            "stateSave": true, // Enable state saving
            "columns": [
                { "data": "RowNumber" },
                { "data": "emp_code" },
                { "data": "emp_name" },
                { "data": "Location" },
                { "data": "Department" },
                { "data": "Designation" },
                { "data": "relievingdate" },
                //  { "data": "relievingday" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {

                        var strReturn = "";
                        if (row.Daysleft <= 7) {
                            strReturn = "<strong class='badge badge - secondary mtr - y - bg'>" + row.Daysleft + " Days</strong> ";
                        }
                        else {
                            strReturn = "<strong class='badge badge - secondary'>" + row.Daysleft + " Days</strong> ";

                        }


                        return strReturn;
                    }
                },
                {
                    "orderable": false,
                    "data": null,
                    "render": function (data, type, row) {
                        var strReturn = '<strong><a href="#" data-toggle="modal" data-target="#hvD"  onclick="ViewSavedata(' + row.ID + ')"><i class="fas fa-eye"></i>View</a></strong>';
                        return strReturn;
                    }
                }

            ]
            ,

        });

        tabelapproved.destroy();

        // Initialize tooltips for the initial set of rows
        $('[data-toggle="tooltip"]').tooltip();

        // Re-initialize tooltips every time the table is redrawn
        tabelapproved.on('draw.dt', function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        DatatableScriptsWithColumnSearch(tableIdA.substring(1), tabelapproved);






    });



}






function ViewSavedata(Id) {
   
    BindSaveEmployeeNOC(Id);
}

function BindSaveEmployeeNOC(Id) {


    var model = {
        ReqId: Id,
        DocType: $("#hdfdoctype").val()

            
    }

    const jsonString = JSON.stringify(model);
    var ScreenID = "NOC101";
    CommonAjaxMethod(virtualPath + 'Generic/GetRecords', { modelData: jsonString, screenId: ScreenID }, 'GET', function (response) {
        debugger;
        var data = response.data.data.Table;
        $("#labelsreq").text(data[0].ReqNo);
        $("#labelsreqdate").text(data[0].ReqDate);
        $("#labelsreqby").text(data[0].Reqby);
        $("#labelsloc").text(data[0].Location);
        $("#labelsdept").text(data[0].Department);
        $("#labelsdesg").text(data[0].Designation);
        $("#labelsRdate").text(data[0].relievingdate);
        if ($("#hdfdoctype").val() == "Finance") {

        
            var tabelapproved = $('#tabelFinanceD').DataTable({
                "processing": true, // for show progress bar           
                "destroy": true,
                "data": response.data.data.Table1,
                "stateSave": true, // Enable state saving
                "columns": [
                    { "data": "RowNumber" },
                    { "data": "DocType" },
                    { "data": "AssetID" },
                    { "data": "DateOfReturn" },
                    { "data": "Description" },
                    { "data": "Type" },
                    { "data": "Status" },
                    { "data": "Remarks" },
                    { "data": "Recovered" },
                    { "data": "AmountofRecovery" },


                ]
                ,

            });
        }
        else {
            var table = $('#TabelDataNOC').DataTable({
                "processing": true,
                "destroy": true,
                "info": false,
                "lengthChange": false,
                "bFilter": false,
                "data": response.data.data.Table1,
                "columns": [

                    { "data": "RowNumber" },
                    { "data": "AssetID" },
                    { "data": "DateOfReturn" },
                    { "data": "Description" },
                    { "data": "Type" },
                    { "data": "Status" },
                    { "data": "Remarks" }



                ]
                ,

            });
        }
   

    });


}
function ViewData(Id,) {

    $("#hdfid").val(Id);
    $("#hv").show();
  
    BindEmployeeNOC(Id);
}

function BindEmployeeNOC(Id) {

    debugger;
    var model = {
        ReqId: Id,
        DocType: $("#hdfdoctype").val()

    }

    const jsonString = JSON.stringify(model);
    var ScreenID = "NOC101";
    DocArray = [];
    $('#tblPreRegistrationUploadingDocuments').html('');
    CommonAjaxMethod(virtualPath + 'Generic/GetRecords', { modelData: jsonString, screenId: ScreenID }, 'GET', function (response) {
    
        var data = response.data.data.Table;
        var FinData = response.data.data.Table1;
        $("#labelreq").text(data[0].ReqNo);
        $("#labelreqdate").text(data[0].ReqDate);
        $("#labelreqby").text(data[0].Reqby);
        $("#labelloc").text(data[0].Location);
        $("#labeldept").text(data[0].Department);
        $("#labeldesg").text(data[0].Designation);
        $("#labelRdate").text(data[0].relievingdate);
        debugger;
        if ($("#hdfdoctype").val() == "Finance") {
            if (response.data.data.Table1.length > 0) {
             
                var item = $(response.data.data.Table1).filter(function (index, row) {
                    return row.DocType === "HR";
                })[0] || "";
                if (item.DocType == "HR") {
                    $("#btnFinance").show();
                }
                else {
                    $("#btnFinance").hide();
                }
                
            }
            else {
                $("#btnFinance").hide();
            }
            var tabelapproved = $('#tabelFinance').DataTable({
                "processing": true, // for show progress bar           
                "destroy": true,
                "data": response.data.data.Table1,
                "stateSave": true, // Enable state saving
                "columns": [
                    //{ "data": "RowNumber" },
                    {
                        "orderable": false,
                        data: null, render: function (data, type, row) {

                            var strReturn = "";
                            if (row.RowNumber != "") {
                                strReturn = " <input type='hidden' class='sno' value=" + row .ID+" /> " + row.RowNumber +"";
                            }
                            return strReturn;
                        }
                    },
                    { "data": "DocType" },
                    { "data": "AssetID" },
                    { "data": "DateOfReturn" },
                    { "data": "Description" },
                    { "data": "Type" },
                    { "data": "Status" },
                    { "data": "Remarks" },
                    {
                        "orderable": false,
                        data: null, render: function (data, type, row) {

                            var strReturn = "";
                            if (row.Recovered == "") {
                                strReturn = "<select class='form - control applyselect'>  < option > Select</option > <option selected=''>Yes</option> <option>No</option> </select > ";
                            }
                            return strReturn;
                        }
                    },

                    {
                        "orderable": false,
                        data: null, render: function (data, type, row) {

                            var strReturn = "";
                            if (row.AmountofRecovery == "0") {
                                strReturn = "<input type='number' class='form - control text - right f - width' value='' placeholder='0' name=''> ";
                            }
                            return strReturn;
                        }
                    }


                ]
                ,

            });
        }


    });


}


var DocArray = [];
var DocArrayId = 0;
function Addrow() {



    var valid = true;

    if (valid == true) {
        var AssetID = $("#txtAssetid").val();
        var Issuedate = $("#txtissueDate").val();
        var Description = $("#txtdesc").val();
        // var Type = $("#txtType").val();ddlImpact
        var Type = $("#ddlImpact option:selected").text();
        var Remarks = $("#txtRemarks").val();
        var Status = $("#ddlStatus option:selected").text();
        DocArrayId = DocArrayId + 1;
        var loop = DocArrayId;


        var objarrayinner =
        {
            ID: loop,
            AssetID: AssetID,
            Issuedate: Issuedate,
            Description: Description,
            Type: Type,
            Remarks: Remarks,
            Status: Status,
            Action: 'Remove',
            DocumentId: loop,

        }

        DocArray.push(objarrayinner);
        var dataArry = DocArray;
        $('#tblnotify').html('');


        var newtbblData1 = '<table id="tblPreRegistrationUploadingDocuments" class="table table-striped m-0 " >' +
            ' <thead>' +
            ' <tr>' +
            ' <th width="33">S.No</th>' +
            ' <th width="150">Asset ID</th>' +
            ' <th width="150">Date of Return</th>' +
            ' <th width="150">Description</th>' +
            ' <th width="150">Impact</th>' +
            ' <th width="150">Status</th>' +
            ' <th width="150">Remarks</th>' +
            ' <th width="50" class=" text-center">Action</th>' +
            ' </tr>' +
            ' </thead>';
        var html1 = "</table>";
        var tableData = "";
        for (let i = 0; i < dataArry.length; i++) {
            var newtbblData = "<tr><td>" + parseInt(i + 1) + "</td><td>" + dataArry[i].AssetID + "</td><td>" + dataArry[i].Issuedate + "</td><td>" + dataArry[i].Description + "</td><td>" + dataArry[i].Type + "</td><td>" + dataArry[i].Status + "</td><td>" + dataArry[i].Remarks + "</td><td class='text-center' ><a class='HideClass'  title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='DeleteRow(this," + dataArry[i].DocumentId + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a> </td></tr>";
            var allstring = newtbblData;
            tableData += allstring;
        }
        $('#tblnotify').html(newtbblData1 + tableData + html1);
        $("#ddlStatus").val('0').trigger('change');
        $("#txtAssetid").val('');
        $("#txtissueDate").val('');
        $("#txtdesc").val('');
        $("#txtType").val('');
        $("#txtRemarks").val('');
    }


}


function DeleteRow(obj, num) {

    var count = 0;
    var TotalRowCount = $('#tblHandOvertask').find("tbody tr").length;
    var NUM = num;
    var NARR = DocArray;
    ConfirmMsgBox("Are you sure you want to delete", '', function () {

        $(obj).closest('tr').remove();

        for (let i = 0; i < NARR.length; i++) {
            if (NARR[i].DocumentId == NUM) {
                NARR.splice(i, 1);
                break;
            }
        }
        DocArray = NARR;
        $("#tblHandOvertask TBODY TR").each(function (i) {
            $(this).closest("tr").find("label").each(function () {
                $(this).attr({
                    'id': function (_, id) { var arr = id.split('_'); return id.replace(arr[1], i); },
                });
                $(this).html(i + 1)
            });

            $(this).closest("tr").find("div").each(function () {
                $(this).attr({
                    'id': function (_, id) { var arr = id.split('_'); return id.replace(arr[1], i); },
                });
            });


            $(this).closest("tr").find("input").each(function () {
                $(this).attr({
                    'id': function (_, id) { return id.replace('_' + (parseInt(i) + 1) + '_', '_' + i + '_'); },
                    'name': function (_, name) { return name.replace('[' + (parseInt(i) + 1) + ']', '[' + i + ']'); },
                });

            });

            $(this).closest("tr").find("select").each(function () {
                $(this).attr({
                    'id': function (_, id) { return id.replace('_' + (parseInt(i) + 1) + '_', '_' + i + '_'); },
                    'name': function (_, name) { return name.replace('[' + (parseInt(i) + 1) + ']', '[' + i + ']'); },
                });

            });

            $(this).closest("tr").find("textarea").each(function () {
                $(this).attr({
                    'id': function (_, id) { return id.replace('_' + (parseInt(i) + 1) + '_', '_' + i + '_'); },
                    'name': function (_, name) { return name.replace('[' + (parseInt(i) + 1) + ']', '[' + i + ']'); },
                });

            });


            $(this).closest("tr").find("span").each(function () {
                if ($(this).attr("data-valmsg-for")) {
                    $(this).attr({
                        'data-valmsg-for': function (_, name) { return name.replace('[' + (parseInt(i) + 1) + ']', '[' + i + ']'); },
                    });
                }
                if ($(this).attr("for")) {
                    $(this).attr({
                        'for': function (_, id) { return id.replace('_' + (parseInt(i) + 1) + '_', '_' + i + '_'); },
                    });
                }
            });
        });
        var form = $("#LevelApproveActionFrom").closest("form");
        form.removeData('validator');
        form.removeData('unobtrusiveValidation');
        $.validator.unobtrusive.parse(form);

    })
}


function submitAdmindata() {
    debugger;
    if (DocArray.length > 0) {
        ShowLoadingDialog();
        var model = {
            ReqID: $("#hdfid").val(),
            DocType: "Admin",
            NOCAsset: DocArray

        }
        const jsonString = JSON.stringify(model);

        let GenericModeldata =
        {
            ScreenID: "NOC101",
            Operation: "ADD",
            ModelData: jsonString,

        };


        CommonAjaxMethod(virtualPath + 'Generic/PerformOperation', GenericModeldata, 'POST', function (response) {
            BindScreen();
            $('#hv').modal('hide')
            CloseLoadingDialog();
        });
    }
    else {
        CloseLoadingDialog();
    }
}
function submitHRdata() {
  
    if (DocArray.length > 0) {
        ShowLoadingDialog();
        var model = {
            ReqID: $("#hdfid").val(),
            DocType: "HR",
            NOCAsset: DocArray

        }
        const jsonString = JSON.stringify(model);

        let GenericModeldata =
        {
            ScreenID: "NOC101",
            Operation: "ADD",
            ModelData: jsonString,

        };


        CommonAjaxMethod(virtualPath + 'Generic/PerformOperation', GenericModeldata, 'POST', function (response) {
            BindScreen();
            $('#hv').modal('hide')
            CloseLoadingDialog();
        });
    }
    else {
        CloseLoadingDialog();
    }
}


function submitITdata() {

    if (DocArray.length > 0) {
        ShowLoadingDialog();
        var model = {
            ReqID: $("#hdfid").val(),
            DocType: "IT",
            NOCAsset: DocArray

        }
        const jsonString = JSON.stringify(model);

        let GenericModeldata =
        {
            ScreenID: "NOC101",
            Operation: "ADD",
            ModelData: jsonString,

        };


        CommonAjaxMethod(virtualPath + 'Generic/PerformOperation', GenericModeldata, 'POST', function (response) {
            BindScreen();
            $('#hv').modal('hide')
            CloseLoadingDialog();
        });
    }
    else {
        CloseLoadingDialog();
    }
}




function submitFinancedata() {
    debugger;
 
        let isValid = true; // Track validation status
        let tableData = [];

       $('#tabelFinance tbody  tr').each(function () {
            let row = $(this);
            
             // Fetch data from the row
        
            let recovered = row.find('td:nth-child(9) select').val();
            let amountOfRecovery = row.find('td:nth-child(10) input').val();

            // Validation: If "Recovered" is "Yes", "Amount of Recovery" must not be empty or zero
            if (recovered === "Yes" && (!amountOfRecovery || parseFloat(amountOfRecovery) <= 0)) {
                isValid = false;
                row.find('td:nth-child(8) input').addClass('error'); // Highlight error field
            } else {
                row.find('td:nth-child(8) input').removeClass('error'); // Remove error highlight
            }

            // Prepare row data if valid
            let rowData = {
                sno: row.find('.sno').val(),
                recovered: recovered,
                amountOfRecovery: amountOfRecovery,
            };

            // Avoid adding the header row
            if (rowData.sno) {
                tableData.push(rowData);
            }
        });

        // If validation fails, show an alert and stop submission
        if (!isValid) {
            alert('Please ensure that Amount of Recovery is filled for all rows where Recovered is "Yes".');
            return;
        }

    // Send data using AJAX if validation passes


    if (tableData.length > 0) {
        ShowLoadingDialog();
        var model = {
            ReqID: $("#hdfid").val(),
            DocType: "Finance",
            NOCAsset: tableData

        }
        const jsonString = JSON.stringify(model);

        let GenericModeldata =
        {
            ScreenID: "NOC102",
            Operation: "ADD",
            ModelData: jsonString,

        };


        CommonAjaxMethod(virtualPath + 'Generic/PerformOperation', GenericModeldata, 'POST', function (response) {
            BindScreen();
            $('#hv').modal('hide')
            CloseLoadingDialog();
        });
    }
    else {
        CloseLoadingDialog();
    }
      
    }

