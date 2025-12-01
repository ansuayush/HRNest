$(document).ready(function () {

    BindTrainingType();
});

var table = "";
$(document).ready(function () {
     table = $('#tblTrainingType').DataTable();
    // #myInput is a <input type="text"> element
    $('#txtsearch').on('keyup', function () {
        table = $('#tblTrainingType').DataTable();
        table.search(this.value).draw();
    });
});

function BindTrainingType() {
    CommonAjaxMethod(virtualPath + 'CapacityRequest/BindTrainingType', null, 'GET', function (response) {


        $('#tblTrainingType').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": response.data.data.Table,
            "paging": true,
            "info": false, 
            "columns": [
               { "data": "RowNum" },
                { "data": "TrainingTypeName" },
                { "data": "TrainingDesc" },
                { "data": "ApplicableToCategory" },                
                { "data": "DesignName" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {
                        var strReturn = "";
                        //Click to DeActivate                     
                        //Click to Activate                      
                        if (row.Status == "Active") {
                            strReturn = "<a title='Click to DeActivate' data-toggle='tooltip' data-original-title='Click to DeActivate' class='AIsActive'  onclick='Activate(" + row.id + ",1)' ><i class='fa fa-check-circle checkgreen' aria-hidden='true'  ></i> </a><a title='Edit'   onclick='EditSubCate(" + row.id + ")' ><i class='fas fa-edit checkgreen' aria-hidden='true' ></i> </a> ";
                        }
                        if (row.Status == "Deactive") {
                            strReturn = "<a title='Click to Activate' data-toggle='tooltip' data-original-title='Click to Activate' class='AIsActive' onclick='Activate(" + row.id + ",2)' ><i class='fa fa-times-circle crossred' aria-hidden='true'  ></i> </a><a title='Edit'   onclick='EditSubCate(" + row.id + ")' ><i class='fas fa-edit checkgreen' aria-hidden='true' ></i> </a> ";
                        }

                        return strReturn;
                    }
                },
                { "data": "Status" }
            ],
            "columnDefs": [
                {
                    "targets": [6], // Index of the column you want to hide (e.g., the 4th column)
                    "visible": false
                }
            ]
        });


    });

}

function EditSubCate(id)
{
    //ClearFormControl();
    var url = "/Capacity/AddTrainingType?id=" + id;
    window.location.href = url;

}
function Activate(id, no) {

    var actionType = "Activate";

    if (no == "1") {
        actionType = "Deactivate";
    }


    //CommonAjaxMethod(virtualPath + 'CapacityRequest/GetTrainingType', { id: id }, 'GET', function (response) {

        //var data = response.data.data.Table;
        var TrainingTypeID = id;// data[0].id;
    ConfirmMsgBox("Are you sure want to " + actionType + ".", '', function () {
        CommonAjaxMethod(virtualPath + 'CapacityRequest/UpdateStatusTrainingType', { id: TrainingTypeID, inputData: 1 }, 'POST', function (response) {
            BindTrainingType();
        });
    });
   // });
}




