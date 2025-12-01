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
                        var strReturn = '<strong><a href="#" data-toggle="modal" data-target="#fn" onclick="ViewData(' + row.ID + ')"><i class="fas fa-eye"></i>View</a></strong>';
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
                { "data": "NofAssetDeficient" },
                {
                    "orderable": false,
                    "data": null,
                    "render": function (data, type, row) {
                        var strReturn = '<strong><a href="#" data-toggle="modal" data-target="#fnA"  onclick="ViewSavedata(' + row.ID + ')"><i class="fas fa-rupee-sign"></i>Enter Cost Breakup</a></strong>';
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


        var tableIdC = '#tabelCompleted';
        var tabelCompleted = $('#tabelCompleted').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": response.data.data.Table2,
            "stateSave": true, // Enable state saving
            "columns": [
                { "data": "RowNumber" },
                { "data": "emp_code" },
                { "data": "emp_name" },
                { "data": "Location" },
                { "data": "Department" },
                { "data": "Designation" },
                { "data": "relievingdate" },
            
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
                        var strReturn = '<strong><a href="#" data-toggle="modal" data-target="#fnA"  onclick="ViewSavedata(' + row.ID + ')"><i class="fas fa-rupee-sign"></i>View</a></strong>';
                        return strReturn;
                    }

                }

            ]
            ,

        });

        tabelCompleted.destroy();

        // Initialize tooltips for the initial set of rows
        $('[data-toggle="tooltip"]').tooltip();

        // Re-initialize tooltips every time the table is redrawn
        tabelCompleted.on('draw.dt', function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        DatatableScriptsWithColumnSearch(tableIdC.substring(1), tabelCompleted);




    });



}

function ViewData(Id,) {

    $("#hdfid").val(Id);
    $("#fn").show();
    BindEmployeeNOC(Id);
}
function ViewSavedata(Id,) {
    debugger;
    $("#hdfid").val(Id);
    $("#fnA").show();
    BindEmployeeSaveNOC(Id);
}
function BindEmployeeSaveNOC(Id) {

    debugger;
    var model = {
        ReqId: Id,
        DocType: $("#hdfdoctype").val()

    }

    const jsonString = JSON.stringify(model);
    var ScreenID = "NOC101";
    CommonAjaxMethod(virtualPath + 'Generic/GetRecords', { modelData: jsonString, screenId: ScreenID }, 'GET', function (response) {
        debugger;
        var data = response.data.data.Table;
        var FinData = response.data.data.Table2;
        $("#labelsreq").text(data[0].ReqNo);
        $("#labelsreqdate").text(data[0].ReqDate);
        $("#labelsreqby").text(data[0].emp_name);
        $("#labelsloc").text(data[0].Location);
        $("#labelsdept").text(data[0].Department);
        $("#labelsdesg").text(data[0].Designation);
        $("#labelsRdate").text(data[0].relievingdate);
        if (data[0].statusflag == "9") {
            $("#btnSendHR").show();
            $("#btnSaveHR").show();
        }
        else {
            $("#btnSendHR").hide();
            $("#btnSaveHR").hide();
        }
    

        var tabelapproved = $('#tabelHeadsaveFinance').DataTable({
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
                            strReturn = " <input type='hidden' class='sno' value=" + row.ID + " /> " + row.RowNumber + "";
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
                        var selectedYes = row.Recovered === "Yes" ? "selected" : "";
                        var selectedNo = row.Recovered === "No" ? "selected" : "";
                        var strReturn = "";
                        //if (row.Recovered == "") {
                        //strReturn = "<select class='form - control applyselect'>  < option > Select</option > <option selected=''>Yes</option> <option>No</option> </select > ";
                        //}
                        // return strReturn;

                        return `<select class='form-control applyselect'>
                    <option value=''>Select</option>
                    <option value='Yes' ${selectedYes}>Yes</option>
                    <option value='No' ${selectedNo}>No</option>
                </select>`;

                    }
                },

                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {

                        var strReturn = "";
                        strReturn = "<input type='number' class='form - control text - right f - width' value=" + row.AmountofRecovery + " placeholder='0' name=''> ";
                        return strReturn;
                    }
                },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {

                        var strReturn = "";
                        strReturn = "<input type='number' class='form - control text - right f - width' value=" + row.WriteOffAmount + " placeholder='0' name=''> ";
                        return strReturn;
                    }
                },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {
                        var selectedValue = row.Component;
                        let options = response.data.data.Table2;

                        // Build dropdown options
                        let dropdownHtml = `<select class='form-control ddlOther applyselect'>`;
                        dropdownHtml += `<option value=''>Select</option>`;
                        options.forEach(option => {
                            let isSelected = selectedValue === option.Id ? "selected" : "";
                            dropdownHtml += `<option value='${option.Id}' ${isSelected}>${option.field_name}</option>`;
                        });
                        dropdownHtml += `</select>`;

                        return dropdownHtml;

                    }
                },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {

                        var strReturn = "";
                        strReturn = "<input type='text' class='form - control text - right f - width' value=" + row.HeadFinanceRemarks + "  > ";
                        return strReturn;
                    }
                }


            ]
            ,

        });



    });


}
function BindEmployeeNOC(Id) {

    debugger;
    var model = {
        ReqId: Id,
        DocType: $("#hdfdoctype").val()

    }

    const jsonString = JSON.stringify(model);
    var ScreenID = "NOC101";
    CommonAjaxMethod(virtualPath + 'Generic/GetRecords', { modelData: jsonString, screenId: ScreenID }, 'GET', function (response) {
        debugger;
        var data = response.data.data.Table;
        var FinData = response.data.data.Table2;
        $("#labelreq").text(data[0].ReqNo);
        $("#labelreqdate").text(data[0].ReqDate);
        $("#labelreqby").text(data[0].emp_name);
        $("#labelloc").text(data[0].Location);
        $("#labeldept").text(data[0].Department);
        $("#labeldesg").text(data[0].Designation);
        $("#labelRdate").text(data[0].relievingdate);
        $("#labelStatus").text(data[0].HRStatus);
        if (data[0].Status == "1") {
            $("#btnsave").show();
            $("#btnsubmit").show();
        }
        else {
            $("#btnsave").hide();
            $("#btnsubmit").hide();
        }
    
        var tabelapproved = $('#tabelHeadFinance').DataTable({
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
                                strReturn = " <input type='hidden' class='sno' value=" + row.ID + " /> " + row.RowNumber + "";
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
                            var selectedYes = row.Recovered === "Yes" ? "selected" : "";
                            var selectedNo = row.Recovered === "No" ? "selected" : "";
                            var strReturn = "";
                            //if (row.Recovered == "") {
                                //strReturn = "<select class='form - control applyselect'>  < option > Select</option > <option selected=''>Yes</option> <option>No</option> </select > ";
                            //}
                            // return strReturn;

                            return `<select class='form-control applyselect'>
                    <option value=''>Select</option>
                    <option value='Yes' ${selectedYes}>Yes</option>
                    <option value='No' ${selectedNo}>No</option>
                </select>`;

                        }
                    },

                    {
                        "orderable": false,
                        data: null, render: function (data, type, row) {

                            var strReturn = "";
                            strReturn = "<input type='number' class='form - control text - right f - width' value=" + row.AmountofRecovery +" placeholder='0' name=''> ";
                            return strReturn;
                        }
                    },
                     {
                        "orderable": false,
                        data: null, render: function (data, type, row) {

                            var strReturn = "";
                            strReturn = "<input type='number' class='form - control text - right f - width' value=" + row.WriteOffAmount + " placeholder='0' name=''> ";
                            return strReturn;
                        }
                    },
                    {
                        "orderable": false,
                        data: null, render: function (data, type, row) {
                            var selectedValue = row.Component;
                            let options = response.data.data.Table2;

                            // Build dropdown options
                            let dropdownHtml = `<select class='form-control ddlOther applyselect'>`;
                            dropdownHtml += `<option value=''>Select</option>`;
                            options.forEach(option => {
                                let isSelected = selectedValue === option.Id ? "selected" : "";
                                dropdownHtml += `<option value='${option.Id}' ${isSelected}>${option.field_name}</option>`;
                            });
                            dropdownHtml += `</select>`;

                            return dropdownHtml;

                        }
                    },
                    {
                        "orderable": false,
                        data: null, render: function (data, type, row) {

                            var strReturn = "";
                            strReturn = "<input type='text' class='form - control text - right f - width' value=" + row.HeadFinanceRemarks + "  > ";
                            return strReturn;
                        }
                    }
                   

                ]
                ,

            });
       


    });


}
function SaveFinancedata() {

    debugger;
   
    let tableData = [];

    $('#tabelHeadFinance tbody  tr').each(function () {
        let row = $(this);
        debugger;
        // Fetch data from the row

        let WriteOffAmount = row.find('td:nth-child(11) input').val();
        let Component = row.find('td:nth-child(12) select').val();
        let HeadFinanceRemarks = row.find('td:nth-child(13) input').val();
       

        // Prepare row data if valid
        let rowData = {
            sno: row.find('.sno').val(),
            WriteOffAmount: WriteOffAmount,
            Component: Component,
            HeadFinanceRemarks: HeadFinanceRemarks
          
        };

        // Avoid adding the header row
        if (rowData.sno) {
            tableData.push(rowData);
        }
    });

    

    // Send data using AJAX if validation passes


    if (tableData.length > 0) {
        ShowLoadingDialog();
        var model = {
            ReqID: $("#hdfid").val(),
            DocType: "HeadFinance",
            Status: "1",
            NOCAsset: tableData

        }
        const jsonString = JSON.stringify(model);

        let GenericModeldata =
        {
            ScreenID: "HeadFinanceAction",
            Operation: "ADD",
            ModelData: jsonString,

        };


        CommonAjaxMethod(virtualPath + 'Generic/PerformOperation', GenericModeldata, 'POST', function (response) {
            BindScreen();
            $('#fn').modal('hide')
            CloseLoadingDialog();
        });
    }
    else {
        CloseLoadingDialog();
    }

}

function SubmitFinancedata() {



    let tableData = [];

    $('#tabelHeadFinance tbody  tr').each(function () {
        let row = $(this);

        // Fetch data from the row
        
        let WriteOffAmount = row.find('td:nth-child(11) input').val();
        let Component = row.find('td:nth-child(12) select').val();
        let HeadFinanceRemarks = row.find('td:nth-child(13) input').val();


        // Prepare row data if valid
        let rowData = {
            sno: row.find('.sno').val(),
            WriteOffAmount: WriteOffAmount,
            Component: Component,
            HeadFinanceRemarks: HeadFinanceRemarks
           
        };

        // Avoid adding the header row
        if (rowData.sno) {
            tableData.push(rowData);
        }
    });



    // Send data using AJAX if validation passes


    if (tableData.length > 0) {
        ShowLoadingDialog();
        var model = {
            ReqID: $("#hdfid").val(),
            DocType: "HeadFinance",
            Status: "2",
            NOCAsset: tableData

        }
        const jsonString = JSON.stringify(model);

        let GenericModeldata =
        {
            ScreenID: "HeadFinanceAction",
            Operation: "ADD",
            ModelData: jsonString,

        };


        CommonAjaxMethod(virtualPath + 'Generic/PerformOperation', GenericModeldata, 'POST', function (response) {
            BindScreen();
            $('#fn').modal('hide')
            CloseLoadingDialog();
        });
    }
    else {
        CloseLoadingDialog();
    }

}


function SaveBandBFinancedata() {

    debugger;

    let tableData = [];

    $('#tabelHeadsaveFinance tbody  tr').each(function () {
        let row = $(this);
        debugger;
        // Fetch data from the row

        let WriteOffAmount = row.find('td:nth-child(11) input').val();
        let Component = row.find('td:nth-child(12) select').val();
        let HeadFinanceRemarks = row.find('td:nth-child(13) input').val();


        // Prepare row data if valid
        let rowData = {
            sno: row.find('.sno').val(),
            WriteOffAmount: WriteOffAmount,
            Component: Component,
            HeadFinanceRemarks: HeadFinanceRemarks

        };

        // Avoid adding the header row
        if (rowData.sno) {
            tableData.push(rowData);
        }
    });



    // Send data using AJAX if validation passes


    if (tableData.length > 0) {
        ShowLoadingDialog();
        var model = {
            ReqID: $("#hdfid").val(),
            DocType: "HeadFinance",
            Status: "1",
            NOCAsset: tableData

        }
        const jsonString = JSON.stringify(model);

        let GenericModeldata =
        {
            ScreenID: "HeadFinanceAction",
            Operation: "ADD",
            ModelData: jsonString,

        };


        CommonAjaxMethod(virtualPath + 'Generic/PerformOperation', GenericModeldata, 'POST', function (response) {
            BindScreen();
            $('#fnA').modal('hide')
            CloseLoadingDialog();
        });
    }
    else {
        CloseLoadingDialog();
    }

}


function ProceedFinancedata() {

 

    let tableData = [];

    $('#tabelHeadsaveFinance tbody  tr').each(function () {
        let row = $(this);
        debugger;
        // Fetch data from the row

        let WriteOffAmount = row.find('td:nth-child(11) input').val();
        let Component = row.find('td:nth-child(12) select').val();
        let HeadFinanceRemarks = row.find('td:nth-child(13) input').val();


        // Prepare row data if valid
        let rowData = {
            sno: row.find('.sno').val(),
            WriteOffAmount: WriteOffAmount,
            Component: Component,
            HeadFinanceRemarks: HeadFinanceRemarks

        };

        // Avoid adding the header row
        if (rowData.sno) {
            tableData.push(rowData);
        }
    });



    // Send data using AJAX if validation passes


    if (tableData.length > 0) {
        ShowLoadingDialog();
        var model = {
            ReqID: $("#hdfid").val(),
            DocType: "HeadFinance",
            Status: "3",
            NOCAsset: tableData

        }
        const jsonString = JSON.stringify(model);

        let GenericModeldata =
        {
            ScreenID: "HeadFinanceAction",
            Operation: "ADD",
            ModelData: jsonString,

        };


        CommonAjaxMethod(virtualPath + 'Generic/PerformOperation', GenericModeldata, 'POST', function (response) {
            BindScreen();
            $('#fnA').modal('hide')
            CloseLoadingDialog();
        });
    }
    else {
        CloseLoadingDialog();
    }

}

