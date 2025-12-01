$(document).ready(function ()
{
    BindScreen();
  LoadMasterDropdown('ddllavel', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.Employee,
        manualTableId: 0
    }, 'Select', false);

  let HOD=$("#hodid").val();
  $('#ddllavel').val(HOD).trigger('change'); 
     LoadMasterDropdown('ddllaveltwo', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.Employee,
        manualTableId: 0
    }, 'Select', false);
 let L2=$("#L2").val();
  $('#ddllaveltwo').val(L2).trigger('change'); 
  LoadMasterDropdown('ddllavelthree', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.Employee,
        manualTableId: 0
    }, 'Select', false);
 let L3=$("#L3").val();
  $('#ddllavelthree').val(L3).trigger('change'); 
  LoadMasterDropdown('ddllavelfour', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.Employee,
        manualTableId: 0
    }, 'Select', false);
 let L4=$("#L4").val();
  $('#ddllavelfour').val(L4).trigger('change'); 
  LoadMasterDropdown('ddllavelfive', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.Employee,
        manualTableId: 0
    }, 'Select', false);
 let L5=$("#L5").val();
  $('#ddllavelfive').val(L5).trigger('change'); 
});

function BindScreen()
{

    var id=$("#hdfid").val();
     var model = {
        Id: id
     
      }
     const jsonString = JSON.stringify(model);
      var ScreenID = "HRAction";
    CommonAjaxMethod(virtualPath + 'Generic/GetRecords', { modelData: jsonString, screenId: ScreenID }, 'GET', function (response) {
        debugger;
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

       $("#NPAP").text(data[0].noticeperiod);
       $("#EPNP").text(data[0].relievingday);
       $("#ERDate").text(data[0].relievingdate);
       $("#RDateP").text(data[0].RelievingdatePolicy);

       $("#spanReasonNP").text(data[0].reasonNP.substring(0, 50));
       $("#spanFullReasonNP").text(data[0].reasonNP);

       $("#hodid").val(data[0].HOD);
    
       $("#L2").val(data[0].L2);
       $("#L3").val(data[0].L3);
       $("#L4").val(data[0].L4);
       $("#L5").val(data[0].L5);
         if (data[0].statusflag == 1 && data[0].ResolutionStatus==1 ){
        $("#btnR").hide();
        $("#btnF").hide();
        $("#btnA").hide();
        $("#divR").show();
        $("#spanRReason").text(dataR[0].Reason.substring(0, 50));
        $("#spanFullRReason").text(dataR[0].Reason);
       $('#hdnUploadActualFileName').val(dataR[0].ActualFileName);
       $('#hdnUploadNewFileName').val(dataR[0].NewFileName);
       $('#hdnUploadFileUrl').val(dataR[0].FileUrl);

       }
       else if (data[0].statusflag == 2 || data[0].statusflag == 3 || data[0].statusflag == 4 || data[0].statusflag == 5 || data[0].statusflag == 6 ){
       $("#btnR").hide();
        $("#btnF").hide();
         $("#btnA").hide();
         $("#divR").hide();
       }
       else if ( data[0].statusflag == 7) {
           $("#btnR").hide();
           $("#btnF").hide();
           $("#btnA").hide();
           $("#divR").hide();
           $("#divlevelComment").show();
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
           
       }

      else{
        $("#btnR").show();
        $("#btnF").show();
        $("#btnA").show();
       }

    });
   
}



function SaveProcessflow()
{

     $("#txtReason").removeClass("Mandatory");
    if (checkValidationOnSubmit('Mandatory') == true) {
     ShowLoadingDialog();
       var model = {
            Id: $("#hdfid").val(),
            LevelOne: $('#ddllavel').val(),            
            LevelTwo: $('#ddllaveltwo').val(), 
            LevelThree: $('#ddllavelthree').val(),  
            LevelFour: $('#ddllavelfour').val(), 
            LevelFive: $('#ddllavelfive').val() 
     }
    const jsonString = JSON.stringify(model);
     
     let GenericModeldata =
    {
        ScreenID: "HRAction",
        Operation: "ADD",
        ModelData: jsonString,
       
    };    

      
  CommonAjaxMethod(virtualPath + 'Generic/PerformOperation', GenericModeldata, 'POST', function (response) {
  BindScreen();
   $('#fts').modal('hide')
       $("#btnR").hide();
        $("#btnF").hide();
        $("#btnA").hide();
        $("#divR").show();
         CloseLoadingDialog();
    });
  }

    
}
   

function SaveRecord()
{
      //$("#txtReason").addClass("Mandatory");
         if (checkValidationOnSubmit('Mandatory')) {
              ShowLoadingDialog();
       var model = {
            ReqID: $("#hdfid").val(),
            Reason: $('#txtReason').val(),            
           ActualFileName:$('#hdnUploadActualFileName').val(),
           NewFileName: $('#hdnUploadNewFileName').val(),
           FileUrl:  $('#hdnUploadFileUrl').val()  ,
         
     }
    const jsonString = JSON.stringify(model);
     
     let GenericModeldata =
    {
        ScreenID: "HRDocument",
        Operation: "ADD",
        ModelData: jsonString,
       
    };    

      
  CommonAjaxMethod(virtualPath + 'Generic/PerformOperation', GenericModeldata, 'POST', function (response) {
  BindScreen();
   $('#rfc').modal('hide')
  CloseLoadingDialog();
    });
  } 
 else {
    CloseLoadingDialog();
  }   
}

function UploadDocument() 
{
 
        var fileUpload = $("#txtAttachment").get(0);
        var files = fileUpload.files;
        if (files.length > 0) {
            var fileData = new FormData();
            fileData.append(files[0].name, files[0]);
            $.ajax({
                url: virtualPath + 'CommonMethod/UploadExitDocument',
                type: "POST",
                contentType: false,
                processData: false,
                data: fileData,
                success: function (response) {
                    var result = JSON.parse(response);
                    if (result.ErrorMessage === "") {
                        $('#hdnUploadActualFileName').val(result.FileModel.ActualFileName);
                        $('#hdnUploadNewFileName').val(result.FileModel.NewFileName);
                        $('#hdnUploadFileUrl').val(result.FileModel.FileUrl);
                         $('#txtAttachmentfile').val(result.FileModel.ActualFileName);
 
                    } 
                },
                
            });
         
    }
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