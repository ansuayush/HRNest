$(document).ready(function () {

    BindScreen();

  


});

function BindScreen() {
    

    var id = $("#hdfid").val();
    var model = {
        Id: id

    }
    const jsonString = JSON.stringify(model);
    var ScreenID = "HRAction";
    CommonAjaxMethod(virtualPath + 'Generic/GetRecords', { modelData: jsonString, screenId: ScreenID }, 'GET', function (response) {
   debugger
        var data = response.data.data.Table;
        var dataR = response.data.data.Table1;
   
        $("#labelreq").text(data[0].ReqNo);
        $("#labelreqdate").text(data[0].ReqDate);
        $("#labelreqby").text(data[0].Reqby);
        $("#labelloc").text(data[0].Location);
        $("#labeldept").text(data[0].Department);
        $("#labeldesg").text(data[0].Designation);
        $("#pReason").text(data[0].ReasonforResignation);
        $("#spanComment").text(data[0].comment.substring(0, 10));
        $("#spanFullComment").text(data[0].comment);

        
        if (data[0].noticeperiodserve == "1") {

            $("#NPAP").text(data[0].relievingday);

            $("#RDateP").text(data[0].RelievingdatePolicy);

            $("#divEPRD").hide();

            $("#divEPNP").hide();
            $("#divnComReason").hide();
            $("#divnCommem").hide();
            $("#divlevelComment").show();
            

        }

        else {

            $("#NPAP").text(data[0].noticeperiod);

            $("#EPNP").text(data[0].relievingday);

            $("#ERDate").text(data[0].relievingdate);

            $("#RDateP").text(data[0].RelievingdatePolicy);
            $("#divnComReason").show();
            $("#divnCommem").hide();
            $("#divlevelComment").show();
            $("#divRDAP").hide();
            

        }

        $('#hdfLevelId').val(data[0].PendingLevel);
        $("#spanReasonNP").text(data[0].reasonNP.substring(0, 50));
        $("#spanFullReasonNP").text(data[0].reasonNP);

        $("#hodid").val(data[0].HOD);

 

        var table = $('#TabellevelComment').DataTable({
            "processing": true,
            "destroy": true,
            "info": false,
            "lengthChange": false,
            "bFilter": false,
            "data": response.data.data.Table2,
            "columns": [

                { "data": "DocType" },
                { "data": "emp_name" },
                { "data": "SuggestedRelievingDate" },
                { "data": "Reason" },
                { "data": "Level" }



            ]
            ,

        });


     
        if (data[0].ResolutionStatus == 1) {
            $("#btnR").hide();
            $("#btnF").hide();
            $("#divR").show();
            $("#spanRReason").text(dataR[0].Reason.substring(0, 50));
            $("#spanFullRReason").text(dataR[0].Reason);
            $("#spanRReason").text(dataR[0].Reason.substring(0, 50));
            $("#spanFullRReason").text(dataR[0].Reason);
            $('#hdnUploadActualFileName').val(dataR[0].ActualFileName);
            $('#hdnUploadNewFileName').val(dataR[0].NewFileName);
            $('#hdnUploadFileUrl').val(dataR[0].FileUrl);
            $("#Pfile").text(dataR[0].ActualFileName.split('.')[0]);
            $("#divDecline").hide();
            $("#divCommentRes").hide();
          

        }
   


    });

}






function DownloadFileRFP() {
    var fileURl = $('#hdnUploadFileUrl').val();
    var fileName = $('#hdnUploadActualFileName').val();
    if (fileURl != null || fileURl != undefined) {
        var stSplitFileName = fileName.split(".");
        var link = document.createElement("a");
        link.download = stSplitFileName[0];
        link.href = fileURl;
        link.click();
    }
}