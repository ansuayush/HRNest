$(document).ready(function ()
{  
   
    $(function () {
        $('.datepicker').datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: "dd-mm-yy",
            yearRange: "-90:+10"
        });

    });

  
    VendorBinding();
    ProjectBinding();
     
});

function VendorBinding()
{
    var obj = {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: 14,
        manualTableId: 0
    }
    CommonAjaxMethod(virtualPath + 'CommonMethod/GetDropdown/', obj, 'GET', function (response)
    {
        var dropdownMasterData = response.data.data.Table;
        //render catagory
        var $ele = $('#ddlVendor');
        $ele.empty();

        $.each(dropdownMasterData, function (ii, vall) {             
                $ele.append($('<option/>').val(vall.ID).text(vall.ValueName));
        })
        $('.multiple-checkboxesV').multiselect({
            includeSelectAllOption: true,
            nSelectedText: 'selected',
            nonSelectedText: 'Select',
            enableFiltering: true,
            includeFilterClearBtn: false,
            enableCaseInsensitiveFiltering: false

        });
    });
}

function ProjectBinding() {
    var obj = {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.Project,
        manualTableId: 0
    }

    CommonAjaxMethod(virtualPath + 'CommonMethod/GetDropdown/', obj, 'GET', function (response) {
        var dropdownMasterData = response.data.data.Table;
        //render catagory
        var $ele = $('#ddlProjectCode');
        $ele.empty();

        $.each(dropdownMasterData, function (ii, vall) {
            $ele.append($('<option/>').val(vall.ID).text(vall.ValueName));
        })
        $('.multiple-checkboxesP').multiselect({
            includeSelectAllOption: true,
            nSelectedText: 'selected',
            nonSelectedText: 'Select',
            enableFiltering: true,
            includeFilterClearBtn: false,
            enableCaseInsensitiveFiltering: false

        });
    });
}
//function SaveSample() {

//    var model = {
//        Id: "12",
//        FirstName: "Santosh"
//    }
//    const jsonString = JSON.stringify(model);

//    let GenericModeldata =
//    {
//        ScreenID: "C100",
//        Operation: "add",
//        ModelData: jsonString,
//        Rows: {
//            Data: [{
//                RowIndex: 0,
//                KeyName: "CommAssingmentSubmission_Id",
//                ValueData: "23"
//            }, {
//                RowIndex: 0,
//                KeyName: "CommAssignmentEvaluation_Id",
//                ValueData: "25"
//            }, {
//                RowIndex: 0,
//                KeyName: "MarkObtained",
//                ValueData: "test"
//            }, {
//                RowIndex: 0,
//                KeyName: "Remarks",
//                ValueData: "26"
//            }]
//        }
//    };


//    CommonAjaxMethod(virtualPath + 'Generic/PerformOperation', GenericModeldata, 'POST', function (response) {


//    });

//}
//function ProcurementReport() {
//    var model = {
//        ProjectCode: $('#ddlProjectCode').val().join(),
//        NameofVendor: $('#ddlVendor').val().join(),
//        Status: $('#ddlStatus').val().join(),
//        ToDate: $('#tbToDate').val() === "" ? null : ChangeDateFormat($('#tbToDate').val()),
//        FromDate: $('#tbFromDate').val() === "" ? null : ChangeDateFormat($('#tbFromDate').val()),
//    };

//    const jsonString = JSON.stringify(model);
//    var ScreenID = "Pro_Report_1";

//    $('#tblReport').DataTable({
//        "processing": true,   // Show processing indicator
//        "serverSide": true,   // Enable server-side processing
//        "destroy": true,      // Allow reinitialization
//        "ajax": {
//            "url": virtualPath + 'Generic/GetRecordsPaging',
//            "type": "POST",    // Use POST or GET based on your API needs
//            "contentType": "application/json",
//            dataSrc: function (json) {
//                // Access and return data.data.Table[0]
//                return JSON.parse(json).data[0].Table;
//            },
//            "data": function (d) {
//                // Extend DataTables' default parameters with your model
//                return JSON.stringify({
//                    draw: d.draw,
//                    start: d.start,
//                    length: d.length,
//                    search: d.search.value,
//                    order: d.order,
//                    columns: d.columns,
//                    modelData: jsonString,
//                    screenId: ScreenID
//                });
//            }
//        },
//        "columns": [
//            { "data": "RowNum" },
//            { "data": "AgreementNumber" },
//            { "data": "VendorName" },
//            { "data": "VendorMobile" },
//            { "data": "EstimatedStartDate" },
//            { "data": "EstimatedEndDate" },
//            { "data": "ProjectNumber" },
//            { "data": "POC" },
//            { "data": "ContractFixedRecommendAmount" },
//            { "data": "ContractReumRecommendAmount" },
//            { "data": "PaidFixedRecommendAmount" },
//            { "data": "PaidReumRecommendAmount" },
//            { "data": "BalanceFixedRecommendAmount" },
//            { "data": "BalanceReumRecommendAmount" },
//            { "data": "Status" },
//            { "data": "Rating" },
//            { "data": "IsEmpaneled" },
//            {
//                "orderable": false,
//                "data": null,
//                "render": function (data, type, row) {
//                    if (row.FileUrl) {
//                        return '<label style="display:none;" id="hdnFileURL_1_' + row.Id + '">' + row.FileUrl + '</label>' +
//                            '<label style="display:none;" id="hdnActualFileName_1_' + row.Id + '">' + row.ActualFileName + '</label>' +
//                            '<a id="ancActualFileName_1_' + row.Id + '" href="#" onclick="DownloadLegacyData(this)">Download <i class="fas fa-download float-right" data-toggle="tooltip" title="Download"></i></a>' +
//                            '<label style="display:none;" id="hdnNewFileName_1_' + row.Id + '">' + row.NewFileName + '</label>';
//                    }
//                    return "";
//                }
//            }
//        ]
//    });
//}

function ProcurementReport() {
    var model =
    {
        ProjectCode: $('#ddlProjectCode').val().join(), 
        NameofVendor: $('#ddlVendor').val().join(), 
        Status: $('#ddlStatus').val().join(), 
        ToDate: $('#tbToDate').val() == "" ? null : ChangeDateFormat($('#tbToDate').val()),
        FromDate: $('#tbFromDate').val() == "" ? null : ChangeDateFormat($('#tbFromDate').val()),
    }
    const jsonString = JSON.stringify(model);
    var ScreenID = "Pro_Report_1";

    CommonAjaxMethod(virtualPath + 'Generic/GetRecords', { modelData: jsonString, screenId: ScreenID }, 'GET', function (response) {

        

        $('#tblReport').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": response.data.data.Table,
            "stateSave": true, // Enable state saving
            "columns": [
                 
                { "data": "RowNum" },

                { "data": "AgreementNumber" },

                { "data": "VendorName" },

                { "data": "VendorNumber" },     

                { "data": "EstimatedStartDate" },

                { "data": "EstimatedEndDate" },

                { "data": "ProjectNumber" },    
                { "data": "POC" },


                { "data": "ContractFixedRecommendAmount" ,"className": "text-right" },
                { "data": "ContractReumRecommendAmount", "className": "text-right" },
                { "data": "PaidFixedRecommendAmount", "className": "text-right" },
                { "data": "PaidReumRecommendAmount", "className": "text-right" },
                { "data": "BalanceFixedRecommendAmount", "className": "text-right" },
                { "data": "BalanceReumRecommendAmount", "className": "text-right" },
                { "data": "Status" },
                { "data": "Rating", "className": "text-right" },
                { "data": "IsEmpaneled" }, 
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {

                        if (row.FileUrl != null)
                        {
                            return '<label   style="display:none;" id="hdnFileURL_1_' + row.Id + '" >' + row.FileUrl + '</label >' +
                                ' <label  style="display:none;" id="hdnActualFileName_1_' + row.Id + '">' + row.ActualFileName + '</label>' +
                                '<a id="ancActualFileName_1_' + row.Id + '" href="#" onclick="DownloadLegacyData(this)">Download <i class="fas fa-download float-right" data-toggle="tooltip" title="Download"></i></a>' +
                                ' <label  style="display:none;" id="hdnNewFileName_1_' + row.Id + '">' + row.NewFileName + '</label>';
                        }
                        else
                        {
                            return "";
                        }


                    }
                
                },
            ] 
        });
    });


}
function DownloadLegacyData(ctrl) {
    var id = ctrl.id.split('_');
    var controlNo = id[2];
    var srNo = id[1];

    var fileURl = $('#hdnFileURL_' + srNo + '_' + controlNo).text();
    var fileName = $('#hdnActualFileName_' + srNo + '_' + controlNo).text();

    if (fileURl != null || fileURl != undefined) {
        var fpath = fileName.replace(/\\/g, '/');
        var fname = fpath.substring(fpath.lastIndexOf('/') + 1, fpath.lastIndexOf('.'));
        var link = document.createElement("a");
        link.download = fname;
        link.href = fileURl;
        link.click();
    }
}
function Export() {

    var model =
    {
        ProjectCode: $('#ddlProjectCode').val().join(),
        NameofVendor: $('#ddlVendor').val().join(),
        Status: $('#ddlStatus').val().join(),
        ToDate: $('#tbToDate').val() == "" ? null : ChangeDateFormat($('#tbToDate').val()),
        FromDate: $('#tbFromDate').val() == "" ? null : ChangeDateFormat($('#tbFromDate').val()),
    }
    const jsonString = JSON.stringify(model);
    var ScreenID = "Pro_Report_2";

    var link = document.createElement("a"); 
    link.target = "_blank"
    link.download = "Report";
    link.href = "/ProcurementRequest/ExportProcurementReport?modelData=" + jsonString + "&screenId=" + ScreenID;
    link.click();


}