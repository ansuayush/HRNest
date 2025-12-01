$(document).ready(function ()
{
    
    var obj = {
        ParentId: 0,
        masterTableType: DropDownTypeEnum.Category,
        isMasterTableType: false,
        isManualTable: false,
        manualTable: 0,
        manualTableId: 0
    }
    LoadMasterDropdown('ddlCategory', obj, 'Choose Your Category', true);
    $(function () {
        $('.datepicker').datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: "dd-mm-yy",
            yearRange: "-90:+10"
        });

    });
    
});
 
function ClearFormControl() {
     

}
function Export()
{
    if ($('#StartDate').val() != '' && $('#EndDate').val() != '')
    {       
        
        var link = document.createElement("a");
        link.target = "_blank"
        link.download = "Test";
        link.href = "/DigitalLibrary/EmployeesExcelToExport?fromDate=" + $('#StartDate').val() + "&todate=" + $('#EndDate').val() + "&category=" + $('#ddlCategory').val() + "&documentType=" + $('#ddlDocCategory').val();
        link.click();
        
    }
    else {
        if ($('#StartDate').val() == '') {
            $('#spStartDate').show();
        }
        else {
            $('#spStartDate').hide();
        }
        if ($('#EndDate').val() == '') {
            $('#spEndDate').show();
        } else {
            $('#spEndDate').hide();
        }



    }

 
}
function Search()
{   
    if ($('#StartDate').val() != '' && $('#EndDate').val() != '')
    {
        var obj =
        {
            FromDate: $('#StartDate').val(),
            ToDate: $('#EndDate').val(),
            category: $('#ddlCategory').val(),
            documentType: $('#ddlDocCategory').val()
        };
        CommonAjaxMethod(virtualPath + 'DigitalLibrary/ContentReport', obj, 'GET', function (response) {

            $('#table').DataTable({
                "processing": true, // for show progress bar           
                "destroy": true,
                dom: 'Bfrtip',
                "buttons": [
                    'excel'
                ],
                "data": response.data.data.Table,
                "columns": [
                    { "data": "Date" },
                    { "data": "Sender" },
                    { "data": "Receiver" },
                    { "data": "Type of Document(Attachment Type)" },
                    { "data": "Category" },
                    { "data": "SubCategory" },
                    { "data": "Documents Sensitivity" }

                ]
            });

        });
    }
    else
    {
        if ($('#StartDate').val() == '') {
            $('#spStartDate').show();
        }
        else {
            $('#spStartDate').hide();
        }
        if ($('#EndDate').val() == '') {
            $('#spEndDate').show();
        } else {
            $('#spEndDate').hide();
        }
            
       

    }

}
 
 
let myExcelXML = (function () {
    let Workbook, WorkbookStart = '<?xml version="1.0"?><ss:Workbook  xmlns="urn:schemas-microsoft-com:office:spreadsheet" xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet" xmlns:html="http://www.w3.org/TR/REC-html40">';
    const WorkbookEnd = '</ss:Workbook>';
    let fs, SheetName = 'Shared Content',
        styleID = 1, columnWidth = 80,
        fileName = "SharedContenReport", uri, link;

    class myExcelXML {
        constructor(o) {
           // let respArray = JSON.parse(o) in case of dataset;
            let respArray =  o; //this is for table
            let finalDataArray = [];

            for (let i = 0; i < respArray.length; i++) {
                finalDataArray.push(flatten(respArray[i]));
            }

            let s = JSON.stringify(finalDataArray);
            fs = s.replace(/&/gi, '&amp;');
        }

        downLoad() {
            const Worksheet = myXMLWorkSheet(SheetName, fs);

            WorkbookStart += myXMLStyles(styleID);

            Workbook = WorkbookStart + Worksheet + WorkbookEnd;

            uri = 'data:text/xls;charset=utf-8,' + encodeURIComponent(Workbook);
            link = document.createElement("a");
            link.href = uri;
            link.style = "visibility:hidden";
            link.download = fileName + ".xls";

            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
        }

        get fileName() {
            return fileName;
        }

        set fileName(n) {
            fileName = n;
        }

        get SheetName() {
            return SheetName;
        }

        set SheetName(n) {
            SheetName = n;
        }

        get styleID() {
            return styleID;
        }

        set styleID(n) {
            styleID = n;
        }
    }

    const myXMLStyles = function (id) {
        let Styles = '<ss:Styles><ss:Style ss:ID="' + id + '"><ss:Font ss:Bold="1"/></ss:Style></ss:Styles>';

        return Styles;
    }

    const myXMLWorkSheet = function (name, o) {
        const Table = myXMLTable(o);
        let WorksheetStart = '<ss:Worksheet ss:Name="' + name + '">';
        const WorksheetEnd = '</ss:Worksheet>';

        return WorksheetStart + Table + WorksheetEnd;
    }

    const myXMLTable = function (o) {
        let TableStart = '<ss:Table>';
        const TableEnd = '</ss:Table>';

        const tableData = JSON.parse(o);

        if (tableData.length > 0) {
            const columnHeader = Object.keys(tableData[0]);
            let rowData;
            for (let i = 0; i < columnHeader.length; i++) {
                TableStart += myXMLColumn(columnWidth);

            }
            for (let j = 0; j < tableData.length; j++) {
                rowData += myXMLRow(tableData[j], columnHeader);
            }
            TableStart += myXMLHead(1, columnHeader);
            TableStart += rowData;
        }

        return TableStart + TableEnd;
    }

    const myXMLColumn = function (w) {
        return '<ss:Column ss:AutoFitWidth="0" ss:Width="' + w + '"/>';
    }


    const myXMLHead = function (id, h) {
        let HeadStart = '<ss:Row ss:StyleID="' + id + '">';
        const HeadEnd = '</ss:Row>';

        for (let i = 0; i < h.length; i++) {
            const Cell = myXMLCell(h[i]);
            HeadStart += Cell;
        }

        return HeadStart + HeadEnd;
    }

    const myXMLRow = function (r, h) {
        let RowStart = '<ss:Row>';
        const RowEnd = '</ss:Row>';
        for (let i = 0; i < h.length; i++) {
            const Cell = myXMLCell(r[h[i]]);
            RowStart += Cell;
        }

        return RowStart + RowEnd;
    }

    const myXMLCell = function (n) {
        let CellStart = '<ss:Cell>';
        const CellEnd = '</ss:Cell>';

        const Data = myXMLData(n);
        CellStart += Data;

        return CellStart + CellEnd;
    }

    const myXMLData = function (d) {
        let DataStart = '<ss:Data ss:Type="String">';
        const DataEnd = '</ss:Data>';

        return DataStart + d + DataEnd;
    }

    const flatten = function (obj) {
        var obj1 = JSON.parse(JSON.stringify(obj));
        const obj2 = JSON.parse(JSON.stringify(obj));
        if (typeof obj === 'object') {
            for (var k1 in obj2) {
                if (obj2.hasOwnProperty(k1)) {
                    if (typeof obj2[k1] === 'object' && obj2[k1] !== null) {
                        delete obj1[k1]
                        for (var k2 in obj2[k1]) {
                            if (obj2[k1].hasOwnProperty(k2)) {
                                obj1[k1 + '-' + k2] = obj2[k1][k2];
                            }
                        }
                    }
                }
            }
            var hasObject = false;
            for (var key in obj1) {
                if (obj1.hasOwnProperty(key)) {
                    if (typeof obj1[key] === 'object' && obj1[key] !== null) {
                        hasObject = true;
                    }
                }
            }
            if (hasObject) {
                return flatten(obj1);
            } else {
                return obj1;
            }
        } else {
            return obj1;
        }
    }

    return myExcelXML;
})();