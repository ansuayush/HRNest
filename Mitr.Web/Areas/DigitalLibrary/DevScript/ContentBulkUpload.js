$(document).ready(function () {

});


function BulkUpload() {
    // Create FormData object

    var fileUpload = $("#emFileUpload").get(0);

    var files = fileUpload.files;
   
    if (files.length > 0) {
        var fileData = new FormData();
        // Looping over all files and add it to FormData object
        for (var i = 0; i < files.length; i++) {
            fileData.append(files[i].name, files[i]);
        }

        $.ajax({
            url: virtualPath + 'DigitalLibrary/UploadBulkDocument',
            type: "POST",
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            data: fileData,
            success: function (response) {
                var result = JSON.parse(response);

                if (result.CustumException == null) {

                    var dt = result.data.Table;
                    var dt1 = result.data.Table1;

                    $('#emSuccess').DataTable({
                        "processing": true, // for show progress bar           
                        "destroy": true,
                        "data": dt1,

                        "columns": [
                            { "data": "SN" },
                            { "data": "Category" },
                            { "data": "SubCategory" },
                            { "data": "ProjectCode" },

                            { "data": "Author" }


                        ]
                    });

                    $('#emFailled').DataTable({
                        "processing": true, // for show progress bar           
                        "destroy": true,
                        "data": dt,

                        "columns": [
                            { "data": "SN" },
                            { "data": "Category" },
                            { "data": "SubCategory" },
                            { "data": "ProjectCode" },
                            { "data": "Author" },
                            { "data": "ErrorReason" }


                        ]
                    });
                    $("#emFileUpload").val('');
                    document.getElementById('hReturnMessage').innerHTML = result.ErrorMessage;
                    $('#btnShowModel').click();

                }
                else {


                    $("#emFileUpload").val('');
                    document.getElementById('hReturnMessage').innerHTML = result.CustumException;
                    $('#btnShowModel').click();
                }
            }
            ,
            error: function (error) {

                $("#emFileUpload").val('');
                FailToaster(error);
                //document.getElementById('hReturnMessage').innerHTML = error;
                //$('#btnShowModel').click();
                isSuccess = false;
            }

        });
    }
    else {
        document.getElementById('hReturnMessage').innerHTML = "Please select excel file to upload the data";
        $('#btnShowModel').click();
    }
}