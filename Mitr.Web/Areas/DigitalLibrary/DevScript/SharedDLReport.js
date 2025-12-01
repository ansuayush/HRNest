$(document).ready(function () {

    LoadMasterDropdown('RequesterLocation',
        {
            ParentId: 0,
            masterTableType: 0,
            isMasterTableType: false,
            isManualTable: true,
            manualTable: ManaulTableEnum.MasterLocation,
            manualTableId: 0
        }, 'All', false);

    var obj = {
        ParentId: 0,
        masterTableType: DropDownTypeEnum.Category,
        isMasterTableType: false,
        isManualTable: false,
        manualTable: 0,
        manualTableId: 0
    }
    LoadMasterDropdown('ddlCategory', obj, 'All', true);

    LoadMasterDropdown('pdProjectCode',
        {
            ParentId: 0,
            masterTableType: 0,
            isMasterTableType: false,
            isManualTable: true,
            manualTable: ManaulTableEnum.Project,
            manualTableId: 0
        }, 'All', false);

    LoadMasterDropdown('pdPlaceOfOrigin',
        {
            ParentId: 0,
            masterTableType: 0,
            isMasterTableType: false,
            isManualTable: true,
            manualTable: ManaulTableEnum.MasterLocation,
            manualTableId: 0
        }, 'All', false);

    $(function () {
        $('.datepicker').datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: "dd-mm-yy",
            yearRange: "-90:+10"
        });

    });

});
function FillSubCategory(ctrl) {
    var Categoryid = $('#ddlCategory option[value="' + $(ctrl).val() + '"]').attr("dataEle")

    LoadMasterDropdown('pdSubCategory',
        {
            ParentId: Categoryid, masterTableType: DropDownTypeEnum.Category, isMasterTableType: false, isManualTable: false,
            manualTable: 0,
            manualTableId: 0
        }, 'Select', false);


}
function ClearFormControl() {


}
function Export() {
   
        var obj =
        {
            ProjectCode: $('#pdProjectCode').val(),
            Location: $('#pdPlaceOfOrigin').val(),
            FromDate: $('#StartDate').val(),
            ToDate: $('#EndDate').val(),
            Category: $('#ddlCategory').val(),
            SubCategory: $('#pdSubCategory').val(),
            RequesterLocation: $('#RequesterLocation').val(),
            FromExport: 1
        };
        CommonAjaxMethod(virtualPath + 'DigitalLibrary/DLSharedReportInCSV', obj, 'GET', function (response) {
            var link = document.createElement("a");
            link.download = "SharedDLReport.csv";
            link.href = "/Attachments/SampleCSVFile/SharedDLReport.csv";
            link.click();
        });
    

   


}
function Search() {
 
        var obj =
        {
            ProjectCode: $('#pdProjectCode').val(),
            Location: $('#pdPlaceOfOrigin').val(),
            FromDate: $('#StartDate').val(),
            ToDate: $('#EndDate').val(),
            Category: $('#ddlCategory').val(),
            SubCategory: $('#pdSubCategory').val(),
            RequesterLocation: $('#RequesterLocation').val(),
            FromExport: 2
        };
        CommonAjaxMethod(virtualPath + 'DigitalLibrary/DLSharedReportInCSV', obj, 'GET', function (response) {
            $('#table').DataTable({
                "processing": true, // for show progress bar           
                "destroy": true,
                dom: 'Bfrtip',
                "buttons": [
                    'excel'
                ],
                "data": response.data.data.Table,
                "columns": [
                    { "data": "Req_No" },
                    { "data": "DateOfSharing" },
                    { "data": "SharedBy" },
                    { "data": "RequesterLocation" },  
                    { "data": "SharedWith" },
                    { "data": "Category" },
                    { "data": "SubCategory" },
                    { "data": "ProjectCode" },
                    { "data": "ProjectName" },
                    { "data": "ThemeticArea" },
                    { "data": "FundedBy" },
                    { "data": "ContentOrigin" },
                    { "data": "Proposal_No" },

                    { "data": "Report_No" },

                    { "data": "Copyright" },
                    { "data": "ProjectManager" },
                    { "data": "Title" },
                    { "data": "Sub_Title" },

                    { "data": "Tags" },
                    { "data": "Abstract_Summary" },
                    { "data": "Remark" },
                    { "data": "Document_Category" },

                    { "data": "AuthName" },
                    { "data": "Accepted" },
                    { "data": "FileName" },
                    { "data": "FileType" },

                    { "data": "FileSize" },
                    { "data": "Status" } 
                     
                ]
            });
        });
     
}
