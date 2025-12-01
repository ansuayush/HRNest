$(document).ready(function () {

    BindScreen()


});

function BindScreen() {
  
    var id = $("#hdfid").val();
    var DocType = 'Assign Task';
    var model = {
        ReqId: id,
        DocType: DocType

    }

    const jsonString = JSON.stringify(model);
    var ScreenID = "NOC103";
    CommonAjaxMethod(virtualPath + 'Generic/GetRecords', { modelData: jsonString, screenId: ScreenID }, 'GET', function (response) {

        var data = response.data.data.Table;
        $("#spRN").text(data[0].ReqNo);
        $("#spRD").text(data[0].ReqDate);
        $("#spRby").text(data[0].Reqby);
        $("#spL").text(data[0].Location);
        $("#spDept").text(data[0].Department);
        $("#spDes").text(data[0].Designation);
        $("#spARD").text(data[0].relievingdate);
        var table = $('#tabelHandover').DataTable({
            "processing": true,
            "destroy": true,
            "info": false,
            "lengthChange": false,
            "bFilter": false,
            "data": response.data.data.Table1,
            "columns": [
                {
                    "orderable": false,
                    "data": null,
                    "className": "HDFAID",
                    "render": function (data, type, row) {
                        var strReturn = '<input type="hidden" id="hdfNID" value=' + row.ID + ' />' + row.RowNumber + '';
                        return strReturn;
                    }
                },
                { "data": "TaskName" },
                { "data": "Priority" },
                {
                    "data": "Remark",
                    "className": "remark"
                },
                {
                    "orderable": false,
                    "data": null,
                    "render": function (data, type, row) {
                        var strReturn = '<strong><a href="#" data-toggle="modal" class="editRemark" data-dismiss="modal" data-target="#remark"  onclick="ViewData(' + row.NTaskName + ')"><i class="fas fa-eye"></i>Edit Remark</a></strong>';
                        return strReturn;
                    }
                }

            ]
            ,

        });






    });



}


function ViewData(task) {
 
    $("#HTask").text(task);
    
}


// jQuery
$(document).ready(function () {
    let currentRow = null; // Track the current row being edited

    // Open the modal when "Edit Remark" is clicked
    $('.editRemark').on('click', function () {
        currentRow = $(this).closest('tr'); // Get the row of the clicked button
        const existingRemark = currentRow.find('.remark').text();
        $('#remarkInput').val(existingRemark); // Pre-fill with existing remark
        $('#remarkModal').show(); // Show the modal
    });

    // Save the remark and update the table
    $('#saveRemark').on('click', function () {
        const newRemark = $('#remarkInput').val();
        if (currentRow) {
            currentRow.find('.remark').text(newRemark); // Update the table row
        }
       // $('#remark').hide(); // Hide the modal
        $('#remarkInput').val(''); // Clear the input field
        
    });

    // Close the modal
    $('#closeModal').on('click', function () {
        $('#remarkModal').hide(); // Hide the modal
        $('#remarkInput').val(''); // Clear the input field
    });
});

function finalsubmit() {
  
    var tableData = [];

    // Iterate through all rows in the table
    $('#tabelHandover tbody tr').each(function () {
        debugger;
        var row = $(this);
        var ID = row.find("td.HDFAID input[type='hidden']").val();
      
      //  var Id = row.find("td.sorting_" + RowNumber + " input[type='hidden']").val();
        // Extract data from each cell in the row
        var rowData = {
            ID: ID,                // Hidden input value
            TaskName: row.find('td:eq(1)').text().trim(), // Second column (TaskName)
            Priority: row.find('td:eq(2)').text().trim(), // Third column (Priority)
            Remark: row.find('td:eq(3)').text().trim()    // Fourth column (Remark)
        };

        tableData.push(rowData);
    });
    if (tableData.length > 0) {
        ShowLoadingDialog();
        var model = {
            ReqID: $("#hdfid").val(),
            Task: tableData

        }
        const jsonString = JSON.stringify(model);

        let GenericModeldata =
        {
            ScreenID: "NOC103",
            Operation: "ADD",
            ModelData: jsonString,

        };


        CommonAjaxMethod(virtualPath + 'Generic/PerformOperation', GenericModeldata, 'POST', function (response) {
            CloseLoadingDialog();
            $("#btnclose").click();
          
        });
    }
    else {
        CloseLoadingDialog();
    }
}

