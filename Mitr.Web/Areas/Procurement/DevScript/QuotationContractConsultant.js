$(document).ready(function () {    
    CommonAjaxMethod(virtualPath + 'ProcurementRequest/GetQuotationEntryConsultant', { Id: RequestId }
        , 'GET', function (response)
        {
           // var data9 = response.data.data.Table8;
            var data9 = response.data.data.Table7;
       
            if (data9[0].Status == 24 || data9[0].Status == 25 || data9[0].Status == 27 || data9[0].Status == 28)
            {
                $("#TermConditionPopup").show();               
            }
            var data = response.data.data.Table10;
            var isAmend = data[0].IsAmend;
            if (isAmend == 1) {
                $("#divQuotationAmendment").show();
            }

    });
});

function UpdatePOC() {
    if ($('#ddlPOCA').val() != "Select") {
        var obj =
        {

            Procure_Request_Id: RequestId,
            POC: $('#ddlPOCA').val(),
        }
        CommonAjaxMethod(virtualPath + 'ProcurementRequest/SaveQuotationPOC', obj, 'POST', function (response) {

        });
    }
}
function PrintContent() {
    var obj =
    {
        ContractType: 'Consultant',
        Procure_Request_Id: RequestId 
    }
    if (checkValidationOnSubmit('UploadDocSign') == true)
    {
        CommonAjaxMethod(virtualPath + 'ProcurementRequest/SetPOContractNumber', obj, 'POST', function (response) {

            GetQuotationDetails();
            var data = response.data.data.Table;
            var printContents = "";
            if (data[0].IsAmend ==1) {
                  printContents = response.data.HtmlView;// document.getElementById('PrintDiv').in
            }
            else {
                  printContents = document.getElementById('PrintDiv').innerHTML;
            }
           
            var originalContents = document.body.innerHTML;
            document.body.innerHTML = printContents;
             // code comment by shailendra 20/06/2024
            // window.print();
            // document.body.innerHTML = originalContents;
            //  window.location.reload(true);
        }, true)
        

    }
}
//function PrintContent() {
//    if (checkValidationOnSubmit('UploadDocSign') == true)  
//        GetQuotationDetails();
//        var DocumentContainer = document.getElementById('PrintDiv');
//        var WindowObject = window.open("http://www.domainname.ext/path.png", "PrintWindow",
//            "width=750,height=650,top=50,left=50,toolbars=no,scrollbars=yes,status=no,resizable=yes");
//        WindowObject.document.write();
//        WindowObject.document.write('<link rel="stylesheet" type="text/css" href="https://fonts.googleapis.com/css?family=Roboto:300,400,700&display=swap">')
//    WindowObject.document.write('<link rel="stylesheet" type="text/css" href="http://mitr2.sahajapps.com/assets/design/css/style.css?v=1.6">')

//    WindowObject.document.write('<link rel="stylesheet" type="text/css" href="http://mitr2.sahajapps.com/assets/design/css/stellarnav.css?v=1.6">')
//    WindowObject.document.write('<link rel="stylesheet" type="text/css" href="http://mitr2.sahajapps.com/assets/design/css/icons.css?v=1.6">')
//    WindowObject.document.write('<link rel="stylesheet" type="text/css" href="http://mitr2.sahajapps.com/assets/design/css/bootstrap.min.css?v=1.6">')
//    WindowObject.document.write('<link rel="stylesheet" type="text/css" href="http://mitr2.sahajapps.com/assets/design/css/select2.min.css?v=1.6">')
//    WindowObject.document.write('<link rel="stylesheet" type="text/css" href="http://mitr2.sahajapps.com/assets/design/css/animate.css?v=1.6">')
//    WindowObject.document.write('<link rel="stylesheet" type="text/css" href="http://mitr2.sahajapps.com/assets/design/css/custom.css?v=1.6">')
//    WindowObject.document.write('<link rel="stylesheet" type="text/css" href="http://mitr2.sahajapps.com/assets/design/css/responsive.css?v=1.6">')



//        WindowObject.document.writeln(DocumentContainer.innerHTML);
//        WindowObject.document.close();
//        WindowObject.focus();
//        WindowObject.print();
//        WindowObject.close();
//    }
//}
function TermConditionPopup() {
    CommonAjaxMethod(virtualPath + 'ProcurementRequest/GetQuotationEntryConsultant', { Id: RequestId }
        , 'GET', function (response) {
            var data = response.data.data.Table9;
            CKEDITOR.instances['txtTemplate'].setData(data[0].HtmlBody);

        });
}
function SaveTermCondition() {
    var desc = CKEDITOR.instances['txtTemplate'].getData();
    if (desc != '') {
        var obj =
        {
            ID: 0,
            Body: CKEDITOR.instances['txtTemplate'].getData(),
            ContractType: 'Purchase Order (PO)',
            ProcureId: RequestId

        }
        CommonAjaxMethod(virtualPath + 'ProcurApprovalAuthority/SaveTemplateMaster', obj, 'POST', function (response) {
            $('#termCon').modal('hide');
        });
    }
    else {
        $('#sptxtTemplate').show();

    }
}

function GetQuotationDetails() {
    CommonAjaxMethod(virtualPath + 'ProcurementRequest/GetQuotationEntryConsultant', { Id: RequestId }
        , 'GET', function (response) {                   
            var data  = response.data.data.Table9;
            $("#spSessionYear").text(data[0].SessionYear);
            $("#spProjectCode").text(data[0].ProjectCode);
            $("#spDay").text(data[0].Day);
            $("#spMonth").text(data[0].Month);
            $("#spYear").text(data[0].Year);
            $("#spVendorName").text(data[0].VendorName);
            $("#spCompanyAddress").text(data[0].CompanyAddress);
            
            $("#spVendorAddress").text(data[0].VendorAddress);
            $("#spPanNumber").text(data[0].PANNo);
            $("#spGstNumber").text(data[0].GST);
            $("#spMsebNumber").text(data[0].MSMENo);
            $("#spEstimateStartDate").text(data[0].EstimatedStartDate);
            $("#spEstimateEndDate").text(data[0].EstimatedEndDate);
            $("#spRecommedAmountNumber").text(data[0].RecommendAmount);
            $("#spRecommedAmountWord").text(data[0].RecommendAmountWord);
            $("#spPOC").text(data[0].POCName);
            $("#spTodayDate").text(data[0].TodayDate);
            $("#divContentHtml").html(data[0].HtmlBody);
            $("#spPONumber").text(data[0].PONumber);
            $("#spReqNo").text(data[0].Req_No);
          
            
            if (data[0].AuthId == "144")
            {
                $("#authSign1").show();
                
             }
            if (data[0].AuthId == "176")
            {
                $("#authSign2").show();
             }
             
            
    });
}