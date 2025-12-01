$(document).ready(function () {



    $(function () {
        $('.datepicker').datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: "dd-mm-yy",
            yearRange: "-90:+10",
            showOn: "button",
            buttonImage: "https://jqueryui.com/resources/demos/datepicker/images/calendar.gif",
            buttonImageOnly: true,
            buttonText: "Select date"
        });

    });


    var obj = {
        ParentId: 0,
        masterTableType: DropDownTypeEnum.SubCategory,
        isMasterTableType: false,
        isManualTable: false,
        manualTable: 0,
        manualTableId: 0
    }
    LoadMasterDropdown('ddlSubCategory', obj, 'Select', false);

    LoadMasterDropdown('ddlFinanceYear',
        {
            ParentId: 0,
            masterTableType: 0,
            isMasterTableType: false,
            isManualTable: true,
            manualTable: 60,
            manualTableId: 0
        }, 'Select', false);

    



    

});


function LoadData(ctrl) {
    ClearData(0);
    if (ctrl.value != 0) {
        BindLegacy(ctrl.value);        
        var subCategoryNameToUpload = ctrl.options[ctrl.selectedIndex].text;
        var dataspl = subCategoryNameToUpload.split('-');
        let d2 = dataspl[1];

        let num = Number(d2);
        let d1 = num - 1;
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
var isDel;
function confirmmsgDelete(id) {

    isDel = {
        id: id
    }


    $("#cancel").modal('show');

}
function ChangeDelete() {

    var ComplianceModel = {
        Id: isDel.id,
        ActionType: 3,
    }

    CommonAjaxMethod(virtualPath + 'ComplianceTransaction/SaveComplianceLegacyMaster', { objComplianceMaster: ComplianceModel }, 'POST', function (response) {
        if (response.ValidationInput == 1) {
            HrefCompMaster();
        }

    });
    $("#cancel").hide();
    // 100 milliseconds delay
}

function BindLegacy(yearId) {
    var tableId = '#tblLegacyBindData';
    CommonAjaxMethod(virtualPath + 'ComplianceTransaction/GetComplianceLegacyMaster', { yid: yearId }
        , 'GET', function (response) {

            var table = $('#tblLegacyBindData').DataTable({
                "processing": true, // for show progress bar           
                "destroy": true,
                "data": response.data.data.Table,
                "stateSave": true, // Enable state saving
                "columns": [
                    {
                        "orderable": false,
                        "data": null,
                        "render": function (data, type, row) {

                            var strReturn = '<div class="text-center" ><a  href="#"  onclick="EditComplianceLegacyMaster(' + row.id + ')"  > <i class="fas fa-edit green-clr" data-toggle="tooltip" title="" data-original-title="Update"></i></a><span class="divline">|</span><a onclick="confirmmsgDelete(' + row.id + ')"  ><i class="fas fa-window-close red-clr" data-toggle="tooltip" title="" data-original-title="Cancel"></i></a><span class="divline"></div>';


                            return strReturn;
                        }
                    },
                    { "data": "RowNumber" },




                    { "data": "CategoryName" },

                    { "data": "SubCategoryName" },

                    { "data": "ComplianceName" },
                    { "data": "Details" },
                    { "data": "emp_name" },


                    {
                        "orderable": true,
                        data: null, render: function (data, type, row) {
                            return "<label clss='lgt-mtr-o-bg'>" + ChangeDateFormatToddMMYYY(row.Duedate_1) + "</label>";
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



                    {
                        "orderable": true,
                        data: null, render: function (data, type, row) {
                            return "<label>" + ChangeDateFormatToddMMYYY(row.Duedate_2) + "</label>";
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
                            return "<label>" + ChangeDateFormatToddMMYYY(row.Duedate_3) + "</label>";
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
                            return "<label>" + ChangeDateFormatToddMMYYY(row.Duedate_4) + "</label>";
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
                            return "<label>" + ChangeDateFormatToddMMYYY(row.Duedate_5) + "</label>";
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
                            return "<label>" + ChangeDateFormatToddMMYYY(row.Duedate_6) + "</label>";
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
                            return "<label>" + ChangeDateFormatToddMMYYY(row.Duedate_7) + "</label>";
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
                            return "<label>" + ChangeDateFormatToddMMYYY(row.Duedate_8) + "</label>";
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
                            return "<label>" + ChangeDateFormatToddMMYYY(row.Duedate_9) + "</label>";
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
                            return "<label>" + ChangeDateFormatToddMMYYY(row.Duedate_10) + "</label>";
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
                            return "<label>" + ChangeDateFormatToddMMYYY(row.Duedate_11) + "</label>";
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
                            return "<label>" + ChangeDateFormatToddMMYYY(row.Duedate_12) + "</label>";
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
                    initCompleteCallback(tableId.substring(1)); // Remove the leading # from tableId
                }
            });

            table.destroy();

            //// Initialize tooltips for the initial set of rows
            //$('[data-toggle="tooltip"]').tooltip();

            //// Re-initialize tooltips every time the table is redrawn
            //table.on('draw.dt', function () {
            //    $('[data-toggle="tooltip"]').tooltip();
            //});

            //DatatableScriptsWithColumnSearch(tableId.substring(1), table);
        });

}
function ChangeCategory(ctrl) {

    HideErrorMessage(ctrl);

    var subId = ctrl.value;

    if (subId != 'Select') {
        CommonAjaxMethod(virtualPath + 'ComplianceTransaction/ChangeCategory', { id: subId }, 'GET', function (response) {

            var data = response.data.data.Table;
            $('#Category').val(data[0].CategoryId)
            $('#cate').html(data[0].Category);
            var data1 = response.data.data.Table1;

            var $ele = $('#ddlCompliance');
            $ele.empty();
            $ele.append($('<option/>').val("Select").text("Select"));

            $.each(data1, function (ii, vall) {
                $ele.append($('<option/>').val(vall.Id).text(vall.ComplianceName));
            })

            //LoadMasterDropdown('ddlCompliance',
    //    {
    //        ParentId: 0,
    //        masterTableType: 0,
    //        isMasterTableType: false,
    //        isManualTable: true,
    //        manualTable: 61,
    //        manualTableId: 0
    //    }, 'Select', false);



        });
    }
    else {
        $('#cate').html('Select');
    }

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
function UploadocumentLegacyDoc(ctrl, no) {
    var fileUpload = $("#Attach" + no).get(0);

    var files = fileUpload.files;
    if (files.length > 0) {

        // Create FormData object
        var fileData = new FormData();

        // Looping over all files and add it to FormData object
        for (var i = 0; i < files.length; i++) {
            fileData.append(files[i].name, files[i]);
        }

        $.ajax({
            url: virtualPath + 'CommonMethod/UploadComplianceLegacy',
            type: "POST",
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            data: fileData,

            success: function (response) {
                var result = JSON.parse(response);

                if (result.ErrorMessage == "") {
                    $('#hdnUploadActualFileName' + no).val(result.FileModel.ActualFileName);
                    $('#hdnUploadNewFileName' + no).val(result.FileModel.NewFileName);
                    $('#hdnUploadFileUrl' + no).val(result.FileModel.FileUrl);

                }
                else {


                    FailToaster(result.ErrorMessage);
                    //document.getElementById('hReturnMessage').innerHTML = result.ErrorMessage;
                    //$('#btnShowModel').click();
                }
            }
            ,
            error: function (error) {
                FailToaster(error);
                //document.getElementById('hReturnMessage').innerHTML = error;
                //$('#btnShowModel').click();
                isSuccess = false;
            }

        });
    }
    else {
        FailToaster("Please select file to attach!");
        //document.getElementById('hReturnMessage').innerHTML = "Please select file to attach!";
        //$('#btnShowModel').click();
    }
}
function RedirectToLegacy()
{
    
}
function SaveLegacy() {
    var AType = 1;
    if ($('#hdnLegacyId').val()!='0') {
        AType = 2;
    }
    if (checkValidationOnSubmit('Mandatory') == true) {
        const ComplianceLegacyModel = {
            Id: $('#hdnLegacyId').val(),
            FYId: $('#ddlFinanceYear').val(),
            Category: $('#Category').val(),
            SubCategory: $('#ddlSubCategory').val(),
            Compliance: $('#ddlCompliance').val(),
            Details: $('#txtDetails').val(),
            Doer: $('#ddlDoer').val(),

            DueDate1: $('#txtDueDate1').val() == "" ? null : ChangeDateFormat($('#txtDueDate1').val()),
            ActualDate1: $('#txtActualDate1').val() == "" ? null : ChangeDateFormat($('#txtActualDate1').val()),
            ActualFileName1: $('#hdnUploadActualFileName1').val(),
            NewFileName1: $('#hdnUploadNewFileName1').val(),
            FileUrl1: $('#hdnUploadFileUrl1').val(),

            DueDate2: $('#txtDueDate2').val() == "" ? null : ChangeDateFormat($('#txtDueDate2').val()),
            ActualDate2: $('#txtActualDate2').val() == "" ? null : ChangeDateFormat($('#txtActualDate2').val()),
            ActualFileName2: $('#hdnUploadActualFileName2').val(),
            NewFileName2: $('#hdnUploadNewFileName2').val(),
            FileUrl2: $('#hdnUploadFileUrl2').val(),


            DueDate3: $('#txtDueDate3').val() == "" ? null : ChangeDateFormat($('#txtDueDate3').val()),
            ActualDate3: $('#txtActualDate3').val() == "" ? null : ChangeDateFormat($('#txtActualDate3').val()),
            ActualFileName3: $('#hdnUploadActualFileName3').val(),
            NewFileName3: $('#hdnUploadNewFileName3').val(),
            FileUrl3: $('#hdnUploadFileUrl3').val(),

            DueDate4: $('#txtDueDate4').val() == "" ? null : ChangeDateFormat($('#txtDueDate4').val()),
            ActualDate4: $('#txtActualDate4').val() == "" ? null : ChangeDateFormat($('#txtActualDate4').val()),
            ActualFileName4: $('#hdnUploadActualFileName4').val(),
            NewFileName4: $('#hdnUploadNewFileName4').val(),
            FileUrl4: $('#hdnUploadFileUrl4').val(),

            DueDate5: $('#txtDueDate5').val() == "" ? null : ChangeDateFormat($('#txtDueDate5').val()),
            ActualDate5: $('#txtActualDate5').val() == "" ? null : ChangeDateFormat($('#txtActualDate5').val()),
            ActualFileName5: $('#hdnUploadActualFileName5').val(),
            NewFileName5: $('#hdnUploadNewFileName5').val(),
            FileUrl5: $('#hdnUploadFileUrl5').val(),

            DueDate6: $('#txtDueDate6').val() == "" ? null : ChangeDateFormat($('#txtDueDate6').val()),
            ActualDate6: $('#txtActualDate6').val() == "" ? null : ChangeDateFormat($('#txtActualDate6').val()),
            ActualFileName6: $('#hdnUploadActualFileName6').val(),
            NewFileName6: $('#hdnUploadNewFileName6').val(),
            FileUrl6: $('#hdnUploadFileUrl6').val(),

            DueDate7: $('#txtDueDate7').val() == "" ? null : ChangeDateFormat($('#txtDueDate7').val()),
            ActualDate7: $('#txtActualDate7').val() == "" ? null : ChangeDateFormat($('#txtActualDate7').val()),
            ActualFileName7: $('#hdnUploadActualFileName7').val(),
            NewFileName7: $('#hdnUploadNewFileName7').val(),
            FileUrl7: $('#hdnUploadFileUrl7').val(),


            DueDate8: $('#txtDueDate8').val() == "" ? null : ChangeDateFormat($('#txtDueDate8').val()),
            ActualDate8: $('#txtActualDate8').val() == "" ? null : ChangeDateFormat($('#txtActualDate8').val()),
            ActualFileName8: $('#hdnUploadActualFileName8').val(),
            NewFileName8: $('#hdnUploadNewFileName8').val(),
            FileUrl8: $('#hdnUploadFileUrl8').val(),

            DueDate9: $('#txtDueDate9').val() == "" ? null : ChangeDateFormat($('#txtDueDate9').val()),
            ActualDate9: $('#txtActualDate9').val() == "" ? null : ChangeDateFormat($('#txtActualDate9').val()),
            ActualFileName9: $('#hdnUploadActualFileName9').val(),
            NewFileName9: $('#hdnUploadNewFileName9').val(),
            FileUrl9: $('#hdnUploadFileUrl9').val(),

            DueDate10: $('#txtDueDate10').val() == "" ? null : ChangeDateFormat($('#txtDueDate10').val()),
            ActualDate10: $('#txtActualDate10').val() == "" ? null : ChangeDateFormat($('#txtActualDate10').val()),
            ActualFileName10: $('#hdnUploadActualFileName10').val(),
            NewFileName10: $('#hdnUploadNewFileName10').val(),
            FileUrl10: $('#hdnUploadFileUrl10').val(),

            DueDate11: $('#txtDueDate11').val() == "" ? null : ChangeDateFormat($('#txtDueDate11').val()),
            ActualDate11: $('#txtActualDate11').val() == "" ? null : ChangeDateFormat($('#txtActualDate11').val()),
            ActualFileName11: $('#hdnUploadActualFileName11').val(),
            NewFileName11: $('#hdnUploadNewFileName11').val(),
            FileUrl11: $('#hdnUploadFileUrl11').val(),

            DueDate12: $('#txtDueDate12').val() == "" ? null : ChangeDateFormat($('#txtDueDate12').val()),
            ActualDate12: $('#txtActualDate12').val() == "" ? null : ChangeDateFormat($('#txtActualDate12').val()),
            ActualFileName12: $('#hdnUploadActualFileName12').val(),
            NewFileName12: $('#hdnUploadNewFileName12').val(),
            FileUrl12: $('#hdnUploadFileUrl12').val(),
            ActionType: AType,
        };

        CommonAjaxMethod(virtualPath + 'ComplianceTransaction/SaveComplianceLegacyMaster', ComplianceLegacyModel, 'POST', function (response) {
            BindLegacy($('#ddlFinanceYear').val());
            ClearData(0);
        });
    }
}


function EditComplianceLegacyMaster(id) {

    CommonAjaxMethod(virtualPath + 'ComplianceTransaction/GetComplianceLegacyMasterById', { id: id }, 'GET', function (response) {
        console.log(response);
        var d = response.data.data.Table;
       
         
            $('#ddlFinanceYear').val(d[0].FYId).trigger('change');
         
        $('#hdnLegacyId').val(d[0].id);
        $('#ddlSubCategory').val(d[0].SubCategory).trigger('change');;
        // $('#Category').val();       
        $('#ddlCompliance').val(d[0].Compliance).trigger('change');;
        $('#txtDetails').val(d[0].Details);
        $('#ddlDoer').val(d[0].Doer);

        $('#txtDueDate1').val(ChangeDateFormatToddMMYYY(d[0].Duedate_1));
        $('#txtActualDate1').val(ChangeDateFormatToddMMYYY(d[0].ActualDate_1));
        $('#hdnUploadActualFileName1').val(d[0].ActualFileName_1);
        $('#hdnUploadNewFileName1').val(d[0].NewFileName_1);
        $('#hdnUploadFileUrl1').val(d[0].FileURL_1);

        $('#txtDueDate2').val(ChangeDateFormatToddMMYYY(d[0].Duedate_2));
        $('#txtActualDate2').val(ChangeDateFormatToddMMYYY(d[0].ActualDate_2));
        $('#hdnUploadActualFileName2').val(d[0].ActualFileName_2);
        $('#hdnUploadNewFileName2').val(d[0].NewFileName_2);
        $('#hdnUploadFileUrl2').val(d[0].FileURL_2);


        $('#txtDueDate3').val(ChangeDateFormatToddMMYYY(d[0].Duedate_3));
        $('#txtActualDate3').val(ChangeDateFormatToddMMYYY(d[0].ActualDate_3));
        $('#hdnUploadActualFileName3').val(d[0].ActualFileName_3);
        $('#hdnUploadNewFileName3').val(d[0].NewFileName_3);
        $('#hdnUploadFileUrl3').val(d[0].FileURL_3);

        $('#txtDueDate4').val(ChangeDateFormatToddMMYYY(d[0].Duedate_4));
        $('#txtActualDate4').val(ChangeDateFormatToddMMYYY(d[0].ActualDate_4));
        $('#hdnUploadActualFileName4').val(d[0].ActualFileName_4);
        $('#hdnUploadNewFileName4').val(d[0].NewFileName_4);
        $('#hdnUploadFileUrl4').val(d[0].FileURL_4);

        $('#txtDueDate5').val(ChangeDateFormatToddMMYYY(d[0].Duedate_5));
        $('#txtActualDate5').val(ChangeDateFormatToddMMYYY(d[0].ActualDate_5));
        $('#hdnUploadActualFileName5').val(d[0].ActualFileName_5);
        $('#hdnUploadNewFileName5').val(d[0].NewFileName_5);
        $('#hdnUploadFileUrl5').val(d[0].FileURL_5);

        $('#txtDueDate6').val(ChangeDateFormatToddMMYYY(d[0].Duedate_6));
        $('#txtActualDate6').val(ChangeDateFormatToddMMYYY(d[0].ActualDate_6));
        $('#hdnUploadActualFileName6').val(d[0].ActualFileName_6);
        $('#hdnUploadNewFileName6').val(d[0].NewFileName_6);
        $('#hdnUploadFileUrl6').val(d[0].FileURL_6);

        $('#txtDueDate7').val(ChangeDateFormatToddMMYYY(d[0].Duedate_7));
        $('#txtActualDate7').val(ChangeDateFormatToddMMYYY(d[0].ActualDate_7));
        $('#hdnUploadActualFileName7').val(d[0].ActualFileName_7);
        $('#hdnUploadNewFileName7').val(d[0].NewFileName_7);
        $('#hdnUploadFileUrl7').val(d[0].FileURL_7);


        $('#txtDueDate8').val(ChangeDateFormatToddMMYYY(d[0].Duedate_8));
        $('#txtActualDate8').val(ChangeDateFormatToddMMYYY(d[0].ActualDate_8));
        $('#hdnUploadActualFileName8').val(d[0].ActualFileName_8);
        $('#hdnUploadNewFileName8').val(d[0].NewFileName_8);
        $('#hdnUploadFileUrl8').val(d[0].FileURL_8);

        $('#txtDueDate9').val(ChangeDateFormatToddMMYYY(d[0].Duedate_9));
        $('#txtActualDate9').val(ChangeDateFormatToddMMYYY(d[0].ActualDate_9));
        $('#hdnUploadActualFileName9').val(d[0].ActualFileName_9);
        $('#hdnUploadNewFileName9').val(d[0].NewFileName_9);
        $('#hdnUploadFileUrl9').val(d[0].FileURL_9);

        $('#txtDueDate10').val(ChangeDateFormatToddMMYYY(d[0].Duedate_10));
        $('#txtActualDate10').val(ChangeDateFormatToddMMYYY(d[0].ActualDate_10));
        $('#hdnUploadActualFileName10').val(d[0].ActualFileName_10);
        $('#hdnUploadNewFileName10').val(d[0].NewFileName_10);
        $('#hdnUploadFileUrl10').val(d[0].FileURL_10);

        $('#txtDueDate11').val(ChangeDateFormatToddMMYYY(d[0].Duedate_11));
        $('#txtActualDate11').val(ChangeDateFormatToddMMYYY(d[0].ActualDate_11));
        $('#hdnUploadActualFileName11').val(d[0].ActualFileName_11);
        $('#hdnUploadNewFileName11').val(d[0].NewFileName_11);
        $('#hdnUploadFileUrl11').val(d[0].FileURL_11);

        $('#txtDueDate12').val(ChangeDateFormatToddMMYYY(d[0].Duedate_12));
        $('#txtActualDate12').val(ChangeDateFormatToddMMYYY(d[0].ActualDate_12));
        $('#hdnUploadActualFileName12').val(d[0].ActualFileName_12);
        $('#hdnUploadNewFileName12').val(d[0].NewFileName_12);
        $('#hdnUploadFileUrl12').val(d[0].FileURL_12);


    });
}

function ClearData(from) {

   
    if (from == 1) {
        $('#ddlFinanceYear').val(0).trigger('change');
    }

    $('#Category').val('0')
    $('#cate').html('Select');

    $('#hdnLegacyId').val(0);
    $('#ddlSubCategory').val(0).trigger('change');
    // $('#Category').val();       
    $('#ddlCompliance').val(0).trigger('change');
    $('#txtDetails').val('');
    $('#ddlDoer').val('');

    $('#txtDueDate1').val('');
    $('#txtActualDate1').val('');
    $('#hdnUploadActualFileName1').val('');
    $('#hdnUploadNewFileName1').val('');
    $('#hdnUploadFileUrl1').val('');
    for (var i = 0; i < 12; i++) {
        var idValue = i + 1;
        $('#Attach' + idValue).val('');
    }    

    $('#txtDueDate2').val('');
    $('#txtActualDate2').val('');
    $('#hdnUploadActualFileName2').val('');
    $('#hdnUploadNewFileName2').val('');
    $('#hdnUploadFileUrl2').val('');


    $('#txtDueDate3').val('');
    $('#txtActualDate3').val('');
    $('#hdnUploadActualFileName3').val('');
    $('#hdnUploadNewFileName3').val('');
    $('#hdnUploadFileUrl3').val('');

    $('#txtDueDate4').val('');
    $('#txtActualDate4').val('');
    $('#hdnUploadActualFileName4').val('');
    $('#hdnUploadNewFileName4').val('');
    $('#hdnUploadFileUrl4').val('');

    $('#txtDueDate5').val('');
    $('#txtActualDate5').val('');
    $('#hdnUploadActualFileName5').val('');
    $('#hdnUploadNewFileName5').val('');
    $('#hdnUploadFileUrl5').val('');

    $('#txtDueDate6').val('');
    $('#txtActualDate6').val('');
    $('#hdnUploadActualFileName6').val('');
    $('#hdnUploadNewFileName6').val('');
    $('#hdnUploadFileUrl6').val('');

    $('#txtDueDate7').val('');
    $('#txtActualDate7').val('');
    $('#hdnUploadActualFileName7').val('');
    $('#hdnUploadNewFileName7').val('');
    $('#hdnUploadFileUrl7').val('');


    $('#txtDueDate8').val('');
    $('#txtActualDate8').val('');
    $('#hdnUploadActualFileName8').val('');
    $('#hdnUploadNewFileName8').val('');
    $('#hdnUploadFileUrl8').val('');

    $('#txtDueDate9').val('');
    $('#txtActualDate9').val('');
    $('#hdnUploadActualFileName9').val('');
    $('#hdnUploadNewFileName9').val('');
    $('#hdnUploadFileUrl9').val('');

    $('#txtDueDate10').val('');
    $('#txtActualDate10').val('');
    $('#hdnUploadActualFileName10').val('');
    $('#hdnUploadNewFileName10').val('');
    $('#hdnUploadFileUrl10').val('');

    $('#txtDueDate11').val('');
    $('#txtActualDate11').val('');
    $('#hdnUploadActualFileName11').val('');
    $('#hdnUploadNewFileName11').val('');
    $('#hdnUploadFileUrl11').val('');

    $('#txtDueDate12').val('');
    $('#txtActualDate12').val('');
    $('#hdnUploadActualFileName12').val('');
    $('#hdnUploadNewFileName12').val('');
    $('#hdnUploadFileUrl12').val('');



}