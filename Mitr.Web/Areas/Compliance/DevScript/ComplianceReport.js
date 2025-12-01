$(document).ready(function () {
    LoadMasterDropdown('ddlFinanceYear', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: 60,
        manualTableId: 0
    }, 'Select', false);

    LoadMasterDropdown('ddlCategorys', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: 11112,
        manualTableId: 0
    }, 'Select', false);

    LoadMasterDropdown('ddlSubCategorys', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: 11113,
        manualTableId: 0
    }, 'Select', false);

    // When Finance Year is selected, trigger the report binding
    $('#ddlFinanceYear').change(function () {
        var financeYear = $('#ddlFinanceYear').val();
        var category = $('#ddlCategorys').val(); // This can be kept optional
        var subCategory = $('#ddlSubCategorys').val(); // This can be kept optional
        if (financeYear) {
            BindComplianceReport(financeYear, category, subCategory);
        }
    });


    // If Category or Sub-Category changes, call the BindComplianceReport with the selected values
    $('#ddlCategorys, #ddlSubCategorys').change(function () {
        var financeYear = $('#ddlFinanceYear').val();
        var category = $('#ddlCategorys').val();
        var subCategory = $('#ddlSubCategorys').val();
        if (financeYear) {
            BindComplianceReport(financeYear, category, subCategory);
        }
    });
});

function ClearData(value) {
    if (value === 0) {
        $('#ddlCategorys').val('').trigger('change');
        $('#ddlSubCategorys').val('').trigger('change');
    }
}

// LoadData function
function LoadData(ctrl) {
    ClearData(0);
    if (ctrl.value != 0) {
        BindComplianceReport(ctrl.value);
        var subCategoryNameToUpload = ctrl.options[ctrl.selectedIndex].text;
        console.log("Selected SubCategory Name:", subCategoryNameToUpload);
        var dataspl = subCategoryNameToUpload.split('-');
        let d2 = dataspl[1];
        let num = Number(d2);
        let d1 = num - 1;
        console.log("Month Labels:", d1, num);
        $("#m1").text('Apr-' + d1);
        $("#m2").text('May-' + d1);
        $("#m3").text('Jun-' + d1);
        $("#m4").text('Jul-' + d1);
        $("#m5").text('Aug-' + d1);
        $("#m6").text('Sep-' + d1);
        $("#m7").text('Oct-' + d1);
        $("#m8").text('Nov-' + d1);
        $("#m9").text('Dec-' + d1);
        $("#m10").text('Jan-' + num);
        $("#m11").text('Feb-' + num);
        $("#m12").text('Mar-' + num);

        $("#m1t").text('Apr-' + d1);
        $("#m2t").text('May-' + d1);
        $("#m3t").text('Jun-' + d1);
        $("#m4t").text('Jul-' + d1);
        $("#m5t").text('Aug-' + d1);
        $("#m6t").text('Sep-' + d1);
        $("#m7t").text('Oct-' + d1);
        $("#m8t").text('Nov-' + d1);
        $("#m9t").text('Dec-' + d1);
        $("#m10t").text('Jan-' + num);
        $("#m11t").text('Feb-' + num);
        $("#m12t").text('Mar-' + num);
    }
}

function BindComplianceReport(financeYear = null, category = null, subCategory = null) {
    var selectedFinanceYear = financeYear || $('#ddlFinanceYear').val();
    var selectedCategory = category || $('#ddlCategorys').val();
    var selectedSubCategory = subCategory || $('#ddlSubCategorys').val();
    var tableId = '#tblComplianceData';
    CommonAjaxMethod(virtualPath + 'ComplianceTransaction/GetComplianceReport',
        { yid: selectedFinanceYear, Cid: selectedCategory || 0, Sid: selectedSubCategory || 0 },
        'GET', function (response) {

            console.log(response);
            var table = $('#tblComplianceData').DataTable({
                "processing": true,
                "destroy": true,
                "info": false,
                "lengthChange": false,
                "bFilter": false,
                "data": response.data.data.Table,
                "stateSave": true, // Enable state saving
                "columns": [
                    {
                        "orderable": true,
                        "data": null,
                        "render": function (data, type, row) {
                            return '<label style="text-align: right; display: inline-block;padding-top: 6px; width: 100%;">' + row.RowNumber + '</label>';
                        }
                    },
                    { "data": "Category" },
                    { "data": "SubCategory" },
                    { "data": "Compliance" },
                    { "data": "FrequencyDesc" },
                    { "data": "Primary_User" },
                    {
                        "orderable": true,
                        data: null, render: function (data, type, row) {
                            debugger;
                            var dueDate1 = row.Duedate_1;
                            var formattedDate = ChangeDateFormatToddMMYYY(dueDate1);
                            if (formattedDate === 'aN-aN-NaN') {
                                formattedDate = '';
                            }
                            return "<label class='lgt-mtr-o-bg'>" + formattedDate + "</label>";
                        }
                    },
                    {
                        "orderable": true,
                        data: null, render: function (data, type, row) {
                            return "<label>" + ChangeDateFormatToddMMYYY(row.ActualDate_1) + "</label>";
                        }
                    },
                    {
                        "orderable": false,
                        data: null, render: function (data, type, row) {

                            if (row.FileURL_1 != null) {
                                return '<label   style="display:none;" id="hdnFileURL_1_' + row.Id + '" >' + row.FileURL_1 + '</label >' +
                                    ' <label  style="display:none;" id="hdnActualFileName_1_' + row.Id + '">' + row.ActualFileName_1 + '</label>' +
                                    '<a id="ancActualFileName_1_' + row.Id + '" href="#" onclick="DownloadLegacyData(this)">Download <i class="fas fa-download float-right" data-toggle="tooltip" title="Download"></i></a>' +
                                    ' <label  style="display:none;" id="hdnNewFileName_1_' + row.Id + '">' + row.NewFileName_1 + '</label>';
                            }
                            else {
                                return "";
                            }


                        }
                    },

                    // 2nd Row start

                    {
                        "orderable": true,
                        data: null, render: function (data, type, row) {
                            debugger;
                            var dueDate = row.Duedate_2;
                            var formattedDate = ChangeDateFormatToddMMYYY(dueDate);
                            if (formattedDate === 'aN-aN-NaN') {
                                formattedDate = '';
                            }
                            return "<label class='lgt-mtr-o-bg'>" + formattedDate + "</label>";
                        }
                    },

                    {
                        "orderable": true,
                        data: null, render: function (data, type, row) {
                            return "<label>" + ChangeDateFormatToddMMYYY(row.ActualDate_2) + "</label>";
                        }
                    },
                    {
                        "orderable": false,
                        data: null, render: function (data, type, row) {

                            if (row.FileURL_2 != null) {
                                return '<label   style="display:none;" id="hdnFileURL_2_' + row.Id + '" >' + row.FileURL_2 + '</label >' +
                                    ' <label  style="display:none;" id="hdnActualFileName_2_' + row.Id + '">' + row.ActualFileName_2 + '</label>' +
                                    '<a id="ancActualFileName_2_' + row.Id + '" href="#" onclick="DownloadLegacyData(this)">Download <i class="fas fa-download float-right" data-toggle="tooltip" title="Download"></i></a>' +
                                    ' <label  style="display:none;" id="hdnNewFileName_2_' + row.Id + '">' + row.NewFileName_2 + '</label>';
                            }
                            else {
                                return "";
                            }


                        }
                    },
                    //3row start
                    {
                        "orderable": true,
                        data: null, render: function (data, type, row) {
                            debugger;
                            var dueDate = row.Duedate_3;
                            var formattedDate = ChangeDateFormatToddMMYYY(dueDate);
                            if (formattedDate === 'aN-aN-NaN') {
                                formattedDate = '';
                            }
                            return "<label class='lgt-mtr-o-bg'>" + formattedDate + "</label>";
                        }
                    },

                    {
                        "orderable": true,
                        data: null, render: function (data, type, row) {
                            return "<label>" + ChangeDateFormatToddMMYYY(row.ActualDate_3) + "</label>";
                        }
                    },
                    {
                        "orderable": false,
                        data: null, render: function (data, type, row) {

                            if (row.FileURL_3 != null) {
                                return '<label   style="display:none;" id="hdnFileURL_3_' + row.Id + '" >' + row.FileURL_3 + '</label >' +
                                    ' <label  style="display:none;" id="hdnActualFileName_3_' + row.Id + '">' + row.ActualFileName_3 + '</label>' +
                                    '<a id="ancActualFileName_3_' + row.Id + '" href="#" onclick="DownloadLegacyData(this)">Download <i class="fas fa-download float-right" data-toggle="tooltip" title="Download"></i></a>' +
                                    ' <label  style="display:none;" id="hdnNewFileName_3_' + row.Id + '">' + row.NewFileName_3 + '</label>';
                            }
                            else {
                                return "";
                            }


                        }
                    },

                    //4th Row start

                    {
                        "orderable": true,
                        data: null, render: function (data, type, row) {
                            debugger;
                            var dueDate = row.Duedate_4;
                            var formattedDate = ChangeDateFormatToddMMYYY(dueDate);
                            if (formattedDate === 'aN-aN-NaN') {
                                formattedDate = '';
                            }
                            return "<label class='lgt-mtr-o-bg'>" + formattedDate + "</label>";
                        }
                    },

                    {
                        "orderable": true,
                        data: null, render: function (data, type, row) {
                            return "<label>" + ChangeDateFormatToddMMYYY(row.ActualDate_4) + "</label>";
                        }
                    },
                    {
                        "orderable": false,
                        data: null, render: function (data, type, row) {

                            if (row.FileURL_4 != null) {
                                return '<label   style="display:none;" id="hdnFileURL_4_' + row.Id + '" >' + row.FileURL_4 + '</label >' +
                                    ' <label  style="display:none;" id="hdnActualFileName_4_' + row.Id + '">' + row.ActualFileName_4 + '</label>' +
                                    '<a id="ancActualFileName_4_' + row.Id + '" href="#" onclick="DownloadLegacyData(this)">Download <i class="fas fa-download float-right" data-toggle="tooltip" title="Download"></i></a>' +
                                    ' <label  style="display:none;" id="hdnNewFileName_4_' + row.Id + '">' + row.NewFileName_4 + '</label>';
                            }
                            else {
                                return "";
                            }


                        }
                    },


                    //5th Row start

                    {
                        "orderable": true,
                        data: null, render: function (data, type, row) {
                            debugger;
                            var dueDate = row.Duedate_5;
                            var formattedDate = ChangeDateFormatToddMMYYY(dueDate);
                            if (formattedDate === 'aN-aN-NaN') {
                                formattedDate = '';
                            }
                            return "<label class='lgt-mtr-o-bg'>" + formattedDate + "</label>";
                        }
                    },

                    {
                        "orderable": true,
                        data: null, render: function (data, type, row) {
                            return "<label>" + ChangeDateFormatToddMMYYY(row.ActualDate_5) + "</label>";
                        }
                    },
                    {
                        "orderable": false,
                        data: null, render: function (data, type, row) {

                            if (row.FileURL_5 != null) {
                                return '<label   style="display:none;" id="hdnFileURL_5_' + row.Id + '" >' + row.FileURL_5 + '</label >' +
                                    ' <label  style="display:none;" id="hdnActualFileName_5_' + row.Id + '">' + row.ActualFileName_5 + '</label>' +
                                    '<a id="ancActualFileName_5_' + row.Id + '" href="#" onclick="DownloadLegacyData(this)">Download <i class="fas fa-download float-right" data-toggle="tooltip" title="Download"></i></a>' +
                                    ' <label  style="display:none;" id="hdnNewFileName_5_' + row.Id + '">' + row.NewFileName_5 + '</label>';
                            }
                            else {
                                return "";
                            }


                        }
                    },


                    //6th Row start

                    {
                        "orderable": true,
                        data: null, render: function (data, type, row) {
                            debugger;
                            var dueDate = row.Duedate_6;
                            var formattedDate = ChangeDateFormatToddMMYYY(dueDate);
                            if (formattedDate === 'aN-aN-NaN') {
                                formattedDate = '';
                            }
                            return "<label class='lgt-mtr-o-bg'>" + formattedDate + "</label>";
                        }
                    },

                    {
                        "orderable": true,
                        data: null, render: function (data, type, row) {
                            return "<label>" + ChangeDateFormatToddMMYYY(row.ActualDate_6) + "</label>";
                        }
                    },
                    {
                        "orderable": false,
                        data: null, render: function (data, type, row) {

                            if (row.FileURL_6 != null) {
                                return '<label   style="display:none;" id="hdnFileURL_6_' + row.Id + '" >' + row.FileURL_6 + '</label >' +
                                    ' <label  style="display:none;" id="hdnActualFileName_6_' + row.Id + '">' + row.ActualFileName_6 + '</label>' +
                                    '<a id="ancActualFileName_6_' + row.Id + '" href="#" onclick="DownloadLegacyData(this)">Download <i class="fas fa-download float-right" data-toggle="tooltip" title="Download"></i></a>' +
                                    ' <label  style="display:none;" id="hdnNewFileName_6_' + row.Id + '">' + row.NewFileName_6 + '</label>';
                            }
                            else {
                                return "";
                            }


                        }
                    },

                    //7th Row start

                    {
                        "orderable": true,
                        data: null, render: function (data, type, row) {
                            debugger;
                            var dueDate = row.Duedate_7;
                            var formattedDate = ChangeDateFormatToddMMYYY(dueDate);
                            if (formattedDate === 'aN-aN-NaN') {
                                formattedDate = '';
                            }
                            return "<label class='lgt-mtr-o-bg'>" + formattedDate + "</label>";
                        }
                    },

                    {
                        "orderable": true,
                        data: null, render: function (data, type, row) {
                            return "<label>" + ChangeDateFormatToddMMYYY(row.ActualDate_7) + "</label>";
                        }
                    },
                    {
                        "orderable": false,
                        data: null, render: function (data, type, row) {

                            if (row.FileURL_7 != null) {
                                return '<label   style="display:none;" id="hdnFileURL_7_' + row.Id + '" >' + row.FileURL_7 + '</label >' +
                                    ' <label  style="display:none;" id="hdnActualFileName_7_' + row.Id + '">' + row.ActualFileName_7 + '</label>' +
                                    '<a id="ancActualFileName_7_' + row.Id + '" href="#" onclick="DownloadLegacyData(this)">Download <i class="fas fa-download float-right" data-toggle="tooltip" title="Download"></i></a>' +
                                    ' <label  style="display:none;" id="hdnNewFileName_7_' + row.Id + '">' + row.NewFileName_7 + '</label>';
                            }
                            else {
                                return "";
                            }


                        }
                    },



                    //8th Row start

                    {
                        "orderable": true,
                        data: null, render: function (data, type, row) {
                            debugger;
                            var dueDate = row.Duedate_8;
                            var formattedDate = ChangeDateFormatToddMMYYY(dueDate);
                            if (formattedDate === 'aN-aN-NaN') {
                                formattedDate = '';
                            }
                            return "<label class='lgt-mtr-o-bg'>" + formattedDate + "</label>";
                        }
                    },

                    {
                        "orderable": true,
                        data: null, render: function (data, type, row) {
                            return "<label>" + ChangeDateFormatToddMMYYY(row.ActualDate_8) + "</label>";
                        }
                    },
                    {
                        "orderable": false,
                        data: null, render: function (data, type, row) {

                            if (row.FileURL_8 != null) {
                                return '<label   style="display:none;" id="hdnFileURL_8_' + row.Id + '" >' + row.FileURL_8 + '</label >' +
                                    ' <label  style="display:none;" id="hdnActualFileName_8_' + row.Id + '">' + row.ActualFileName_8 + '</label>' +
                                    '<a id="ancActualFileName_8_' + row.Id + '" href="#" onclick="DownloadLegacyData(this)">Download <i class="fas fa-download float-right" data-toggle="tooltip" title="Download"></i></a>' +
                                    ' <label  style="display:none;" id="hdnNewFileName_8_' + row.Id + '">' + row.NewFileName_8 + '</label>';
                            }
                            else {
                                return "";
                            }


                        }
                    },

                    //9th Row start

                    {
                        "orderable": true,
                        data: null, render: function (data, type, row) {
                            debugger;
                            var dueDate = row.Duedate_9;
                            var formattedDate = ChangeDateFormatToddMMYYY(dueDate);
                            if (formattedDate === 'aN-aN-NaN') {
                                formattedDate = '';
                            }
                            return "<label class='lgt-mtr-o-bg'>" + formattedDate + "</label>";
                        }
                    },

                    {
                        "orderable": true,
                        data: null, render: function (data, type, row) {
                            return "<label>" + ChangeDateFormatToddMMYYY(row.ActualDate_9) + "</label>";
                        }
                    },
                    {
                        "orderable": false,
                        data: null, render: function (data, type, row) {

                            if (row.FileURL_9 != null) {
                                return '<label   style="display:none;" id="hdnFileURL_9_' + row.Id + '" >' + row.FileURL_9 + '</label >' +
                                    ' <label  style="display:none;" id="hdnActualFileName_9_' + row.Id + '">' + row.ActualFileName_9 + '</label>' +
                                    '<a id="ancActualFileName_9_' + row.Id + '" href="#" onclick="DownloadLegacyData(this)">Download <i class="fas fa-download float-right" data-toggle="tooltip" title="Download"></i></a>' +
                                    ' <label  style="display:none;" id="hdnNewFileName_9_' + row.Id + '">' + row.NewFileName_9 + '</label>';
                            }
                            else {
                                return "";
                            }


                        }
                    },

                    //10th Row start

                    {
                        "orderable": true,
                        data: null, render: function (data, type, row) {
                            debugger;
                            var dueDate = row.Duedate_10;
                            var formattedDate = ChangeDateFormatToddMMYYY(dueDate);
                            if (formattedDate === 'aN-aN-NaN') {
                                formattedDate = '';
                            }
                            return "<label class='lgt-mtr-o-bg'>" + formattedDate + "</label>";
                        }
                    },
                    {
                        "orderable": true,
                        data: null, render: function (data, type, row) {
                            return "<label>" + ChangeDateFormatToddMMYYY(row.ActualDate_10) + "</label>";
                        }
                    },
                    {
                        "orderable": false,
                        data: null, render: function (data, type, row) {

                            if (row.FileURL_10 != null) {
                                return '<label   style="display:none;" id="hdnFileURL_10_' + row.Id + '" >' + row.FileURL_10 + '</label >' +
                                    ' <label  style="display:none;" id="hdnActualFileName_10_' + row.Id + '">' + row.ActualFileName_10 + '</label>' +
                                    '<a id="ancActualFileName_10_' + row.Id + '" href="#" onclick="DownloadLegacyData(this)">Download <i class="fas fa-download float-right" data-toggle="tooltip" title="Download"></i></a>' +
                                    ' <label  style="display:none;" id="hdnNewFileName_10_' + row.Id + '">' + row.NewFileName_10 + '</label>';
                            }
                            else {
                                return "";
                            }


                        }
                    },

                    //11th Row start

                    {
                        "orderable": true,
                        data: null, render: function (data, type, row) {
                            debugger;
                            var dueDate = row.Duedate_11;
                            var formattedDate = ChangeDateFormatToddMMYYY(dueDate);
                            if (formattedDate === 'aN-aN-NaN') {
                                formattedDate = '';
                            }
                            return "<label class='lgt-mtr-o-bg'>" + formattedDate + "</label>";
                        }
                    },
                    {
                        "orderable": true,
                        data: null, render: function (data, type, row) {
                            return "<label>" + ChangeDateFormatToddMMYYY(row.ActualDate_11) + "</label>";
                        }
                    },
                    {
                        "orderable": false,
                        data: null, render: function (data, type, row) {

                            if (row.FileURL_11 != null) {
                                return '<label   style="display:none;" id="hdnFileURL_11_' + row.Id + '" >' + row.FileURL_11 + '</label >' +
                                    ' <label  style="display:none;" id="hdnActualFileName_11_' + row.Id + '">' + row.ActualFileName_11 + '</label>' +
                                    '<a id="ancActualFileName_11_' + row.Id + '" href="#" onclick="DownloadLegacyData(this)">Download <i class="fas fa-download float-right" data-toggle="tooltip" title="Download"></i></a>' +
                                    ' <label  style="display:none;" id="hdnNewFileName_11_' + row.Id + '">' + row.NewFileName_11 + '</label>';
                            }
                            else {
                                return "";
                            }


                        },

                    },  //12th Row start

                    {
                        "orderable": true,
                        data: null, render: function (data, type, row) {
                            debugger;
                            var dueDate = row.Duedate_12;
                            var formattedDate = ChangeDateFormatToddMMYYY(dueDate);
                            if (formattedDate === 'aN-aN-NaN') {
                                formattedDate = '';
                            }
                            return "<label class='lgt-mtr-o-bg'>" + formattedDate + "</label>";
                        }
                    },

                    {
                        "orderable": true,
                        data: null, render: function (data, type, row) {
                            return "<label>" + ChangeDateFormatToddMMYYY(row.ActualDate_12) + "</label>";
                        }
                    },
                    {
                        "orderable": false,
                        data: null, render: function (data, type, row) {

                            if (row.FileURL_12 != null) {
                                return '<label   style="display:none;" id="hdnFileURL_12_' + row.Id + '" >' + row.FileURL_12 + '</label >' +
                                    ' <label  style="display:none;" id="hdnActualFileName_12_' + row.Id + '">' + row.ActualFileName_12 + '</label>' +
                                    '<a id="ancActualFileName_12_' + row.Id + '" href="#" onclick="DownloadLegacyData(this)">Download <i class="fas fa-download float-right" data-toggle="tooltip" title="Download"></i></a>' +
                                    ' <label  style="display:none;" id="hdnNewFileName_12_' + row.Id + '">' + row.NewFileName_12 + '</label>';
                            }
                            else {
                                return "";
                            }


                        }
                    },
                ],
                "initComplete": function () {
                    initCompleteCallback(tableId.substring(1));
                }
            });
            table.destroy();
            $('[data-toggle="tooltip"]').tooltip();
            table.on('draw.dt', function () {
                $('[data-toggle="tooltip"]').tooltip();
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