

function BindScreen()
{

    var id=$("#hdfEMPID").val();
     var model = {
        EMPID: id
     
      }
     const jsonString = JSON.stringify(model);
      var ScreenID = "HRDocument";
    CommonAjaxMethod(virtualPath + 'Generic/GetRecords', { modelData: jsonString, screenId: ScreenID }, 'GET', function (response) {
        debugger;
         var data = response.data.data.Table;
       var table  = $('#Historytabel').DataTable({
            "processing": true,         
            "destroy": true,
            "info": false,
            "lengthChange": false,
            "bFilter": false,
            "data": response.data.data.Table,
            "columns": [
                {
                  "orderable": true,
                  "data": null,
                  "render": function (data, type, row) {
                      return '<label style="text-align: right; display: inline-block;padding-top: 6px; width: 100%;">' + row.RowNum + '</label>';
                                }
                  },
                { "data": "requestno" },
                { "data": "requestdate" },
                { "data": "Reason" },
                { "data": "emp_name" },
                 {
                    "orderable": false,
                    "data": null,
                    "render": function(data, type, row) {
                        var strReturn = "";
                        if (row.id>0) {
                            strReturn = "<a title='View' data-toggle='modal' data-target='#sc' onclick='ViewResignation(" + row.ReqId + ")'><i class='fas fa-eye' aria-hidden='true'></i>View</a>";
                        } 
                        return strReturn;
                    }
                }
              
             
            ]
               ,
             "initComplete": function () {
                initCompleteCallback(tableId.substring(1)); // Remove the leading # from tableId
            }
        });
        table.destroy();

                // Initialize tooltips for the initial set of rows
                $('[data-toggle="tooltip"]').tooltip();

                // Re-initialize tooltips every time the table is redrawn
                table.on('draw.dt', function () {
                    $('[data-toggle="tooltip"]').tooltip();
                });

                DatatableScriptsWithColumnSearch(tableId.substring(1), table);
            });
}