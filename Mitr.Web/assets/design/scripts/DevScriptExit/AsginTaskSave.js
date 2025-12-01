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
                { "data": "ID", "visible": false }

            ]
            ,

        });






    });



}




