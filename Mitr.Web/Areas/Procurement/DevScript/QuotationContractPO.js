$(document).ready(function () {
    CommonAjaxMethod(virtualPath + 'ProcurementRequest/GetQuotationEntry', { Id: RequestId }
        , 'GET', function (response) {
            var data9 = response.data.data.Table8;

            
            if (data9[0].Status == 24 || data9[0].Status == 25 || data9[0].Status == 27 || data9[0].Status == 28) {
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
    if ($('#ddlPOCA').val()!="Select") {
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
        ContractType: 'Purchase Order (PO)',
        Procure_Request_Id: RequestId,
        QuotationNo: $('#txtQuotationNo').val(),
        Shiping: $('#txtShiping').val(),
        SpecialConditions: $('#txtSpecialCond').val(),
        DeliveryAddress: $('#txtDeliveryAddress').val(),
        AmendmentReason: $('#txtAmendmentReason').val(),
         
        QuotationDate: ChangeDateFormat($('#txtQuotationDate').val()),
        QuotationAgreementDate: ChangeDateFormat($('#txtQuotationAgreementDate').val()),
        QuotationSignDate: ChangeDateFormat($('#txtQuotationSignDate').val()),
    }

    if (checkValidationOnSubmit('UploadDocSign') == true) {
        CommonAjaxMethod(virtualPath + 'ProcurementRequest/SetPOContractNumber', obj, 'POST', function (response) {
            GetQuotationDetails();
         
            var printContents = document.getElementById('PrintDiv').innerHTML;
            var originalContents = document.body.innerHTML;
             document.body.innerHTML = printContents; 
             // code comment by shailendra 20/06/2024
            //  window.print();
            // document.body.innerHTML = originalContents;
            // window.location.reload(true);
        }, true);

    }
}

function TermConditionPopup() {
    CommonAjaxMethod(virtualPath + 'ProcurementRequest/GetQuotationEntry', { Id: RequestId }
        , 'GET', function (response) {
            var data = response.data.data.Table10;
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
    CommonAjaxMethod(virtualPath + 'ProcurementRequest/GetQuotationEntry', { Id: RequestId }
        , 'GET', function (response) {
            var data = response.data.data.Table10;
            var dataPayment = response.data.data.Table2;
      
            var dataColumn = response.data.data.Table12;
            var columnD = dataColumn[0].ColumnNames;

            var dataQuotation = response.data.data.Table13;

            var tmpHTML = "";
            var theadStart = "<thead class=\"bg-primary text-white\">";
            var tHeadClose = "</thead>";
            var res = columnD.split(",");
            tmpHTML += theadStart;
            for (var j = 0; j < res.length; j++)
            {
                 

                if (res[j].indexOf('Description') !== -1)
                {
                    tmpHTML += "<th>" + res[j] + "</th>";

                }
                else
                {
                    tmpHTML += '<th class=\"ClassColumnWidth\" >' + res[j] + '</th>';

                }


                
            
            }

            tmpHTML += tHeadClose;


            $("#tblQutation").html(tmpHTML);

            $('#tblQutation').DataTable({
                "processing": true, // for show progress bar           
                "destroy": true,
                "data": dataQuotation,
                "paging": false,
                "info": false,
                "ordering": false,
                "columns": Returnobj(columnD)
            });

           


          //  $("#spAmendmentReasonPayment").hide(); 
            $("#trAmendDate").hide(); 
            $("#trRevisedDate").hide(); 
            $("#AmendmentReason").hide();
            $("#strAmendment").hide();  
            var isAmend = data[0].IsAmend;
            if (isAmend == 1) {
                
              
                $("#trAmendDate").show();
                $("#spAmendmentDate").text(ChangeDateFormatToddMMYYY(data[0].AmendmentDate));

                $("#trRevisedDate").show();
                $("#spRevisedDate").text(ChangeDateFormatToddMMYYY(data[0].RevisedEndDate));

                // $("#spAmendmentReasonPayment").show(); 
                $("#spAmendmentReason").text(data[0].AmendmentReason)
                $("#AmendmentReason").show();
                $("#strAmendment").show();
                $("#spAmendmentNo").text(data[0].AmendmentNo);
                $("#tblPaymentTermPrintAmend").show();
                $("#tblPaymentTermPrint").hide();
                $('#tblPaymentTermPrintAmend').DataTable({
                    "processing": true, // for show progress bar           
                    "destroy": true,
                    "data": dataPayment,
                    "paging": false,
                    "info": false,
                    "ordering": false,
                    "columns": [


                        {
                            "orderable": false,

                            data: null, render: function (data, type, row) {

                                return '<label class="text-right">' + row.PaymentTerms + '</label>';
                            }
                        },

                        {
                            "orderable": false,

                            data: null, render: function (data, type, row) {

                                return '<label class="text-right">' + NumberWithComma(row.Amount) + '</label>';
                            }
                        },

                        {
                            "orderable": false,

                            data: null, render: function (data, type, row) {

                                return '<label class="text-right">' + ChangeDateFormatToddMMYYY(row.DueOn) + '</label>';
                            }
                        },
                        {
                            "orderable": false,

                            data: null, render: function (data, type, row) {

                                return '<label class="text-right">' + ChangeDateFormatToddMMYYY(row.PaidDate) + '</label>';
                            }
                        }

                    ]
                });
         

            }
            else {
                $("#tblPaymentTermPrintAmend").hide();
                $('#tblPaymentTermPrint').DataTable({
                    "processing": true, // for show progress bar           
                    "destroy": true,
                    "data": dataPayment,
                    "paging": false,
                    "info": false,
                    "ordering": false,
                    "columns": [


                        {
                            "orderable": false,

                            data: null, render: function (data, type, row) {

                                return '<label class="text-right">' + row.PaymentTerms + '</label>';
                            }
                        },

                        {
                            "orderable": false,

                            data: null, render: function (data, type, row) {

                                return '<label class="text-right">' + NumberWithComma(row.Amount) + '</label>';
                            }
                        },

                        {
                            "orderable": false,

                            data: null, render: function (data, type, row) {

                                return '<label class="text-right">' + ChangeDateFormatToddMMYYY(row.DueOn) + '</label>';
                            }
                        }

                    ]
                });
            }
            $("#spAutorizedName").text(data[0].AuthName);
            $("#spAutorizedEmail").text(data[0].AuthEmail);
            $("#spSignedVendorName").text(data[0].VendorName);
            $("#spSignedAuthName").text(data[0].VPOCName);
            $("#spSignedDisignation").text(data[0].VDesignation);
       
            $("#spSignedDate").text($('#txtQuotationSignDate').val());
            $("#spVendorPOC").text(data[0].VPOCName);
            $("#spVendorEmail").text(data[0].VEmail);
            $("#spVendorPhoneNumber").text(data[0].VMobileNo);
            $("#spSessionYear").text(data[0].SessionYear);
            $("#spProjectCode").text(data[0].ProjectCode);
            $("#spVendorName").text(data[0].VendorName);
            $("#spCompanyAddress").text(data[0].CompanyAddress);
            $("#spVendorAddress").text(data[0].VendorAddress);
            $("#spVendorPan").text(data[0].PANNo);
            $("#spVendorMSME").text(data[0].MSMENo);
        
            $("#spQuotationStartDate").text(ChangeDateFormatToddMMYYY(data[0].EstimatedStartDate));
            $("#spQuotationEndDate").text(ChangeDateFormatToddMMYYY(data[0].EstimatedEndDate));


            $("#spPORequester").text(data[0].POCName);
            $("#spPOPhoneNumber").text(data[0].PhoneNo);
            $("#spPOEmailId").text(data[0].email);
            $("#spReqNo").text(data[0].Req_No);
            
            $("#spPODate").text($('#txtQuotationAgreementDate').val());

            if (isAmend == 1) {
                $("#divTermCondition").html(' All other terms and conditions of the purchase order referenced above remain unchanged and continue to remain in full force and effect.');
                
            }
            else {
                $("#divTermCondition").html(data[0].HtmlBody);
            }
            var sp = data[0].SpecialConditions;
            if (sp.length > 0) {
                sp = sp.replace(/\n/g, "<br>");
                $("#spSpecialCondition").html(sp);
            }
             

            $("#spCostCode").text(data[0].ProjectCode);


            $("#spQuotationNo").text($('#txtQuotationNo').val());
            $("#spQuotationDate").text($('#txtQuotationDate').val());
            $("#spShippingVia").text($('#txtShiping').val());
             
            $("#spPONumber").text(data[0].PONumber);
            $("#DeliveryAddress").text($('#txtDeliveryAddress').val()) ;
           
                $('#txtAmendmentReason').text(data[0].AmendmentReason);

            if (data[0].AuthId == "144") {
                $("#authSign1").show();

            }
            if (data[0].AuthId == "176") {
                $("#authSign2").show();
            }



        });
}

function Returnobj(selectColumnToGrid) {

    var jObj = [

    ];
    var res = selectColumnToGrid.split(",");

    for (var j = 0; j < res.length; j++) {
        // tmpHTML += "<th>" + res[j] + "</th>";
         
        if (res[j].indexOf('Description') !== -1)
        {
            var newObj = { "data": res[j], "name": res[j], "className":"ClassColumnWidth" };
            jObj.push(newObj);
        }
        else {
            var newObj = { "data": res[j], "name": res[j], "width": "auto" };
            jObj.push(newObj);
        }
       
    }

    return jObj;
}