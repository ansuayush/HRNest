
$(document).ready(function () {
    var AddButtonName = $(".hdnAddButton").val();
    if (AddButtonName) {
        $(".sbAtext").html(AddButtonName);
    }

});

//$(".AIsActive").on('click', function (e) {
//    UpdateIsActive(this);
//});


function CheckRecordExists(obj) {
    var ObjURL = $(obj).closest("div").find(".hdnRecordExit");
    if ($(obj).val() != "") {
        var dataObject = JSON.stringify({
            'ID': $(ObjURL).attr("tag-id"),
            'Doctype': $(ObjURL).attr("tag-value"),
            'Value': $(obj).val(),

        });
        $.ajax({
            url: "/CommonAjax/CheckRecordExistsJSon",
            type: "Post",
            contentType: 'application/json',
            data: dataObject,
            success: function (data) {
                if (!data.Status) {

                    $(obj).val('');
                    $(obj).closest("div").find('.field-validation-valid').html(data.SuccessMessage).removeClass('color-green').addClass('text-danger')
                }
                else {
                    $(obj).closest("div").find('.field-validation-valid').html(data.SuccessMessage).removeClass('text-danger').addClass('color-green');
                }
            },
            error: function (er) {
                alert(er);
            }
        });
    }
}

function CheckUserDetailsExists(obj) {
    var ObjURL = $(obj).closest("div").find(".hdnRecordExit");
    if ($(obj).val() != "") {
        var dataObject = JSON.stringify({
            'ID': $(ObjURL).attr("tag-id"),
            'Doctype': $(ObjURL).attr("tag-value"),
            'Value': $(obj).val(),

        });
        $.ajax({
            url: "/CommonAjax/CheckUserDetailsExistsJSon",
            type: "Post",
            contentType: 'application/json',
            data: dataObject,
            success: function (data) {
                if (!data.Status) {

                    $(obj).val('');
                    $(obj).closest("div").find('.field-validation-valid').html(data.SuccessMessage).removeClass('color-green').addClass('text-danger')
                    document.getElementById('supMsgUserLoginId').innerHTML = data.SuccessMessage;
                    $('#supMsgUserLoginId').addClass("text-danger");
                }
                else {
                    $(obj).closest("div").find('.field-validation-valid').html(data.SuccessMessage).removeClass('text-danger').addClass('color-green');
                    document.getElementById('supMsgUserLoginId').innerHTML = data.SuccessMessage;
                    //document.getElementById('supMsgUserLoginId').parentNode.firstChild.className = 'color-green';
                    $('#supMsgUserLoginId').addClass("color-green");
                }
            },
            error: function (er) {
                alert(er);
            }
        });
    }
}


//function UpdateIsActive(obj) {
//    var _this = $(obj);
//    var ID = $(_this).closest("tr").find("input:hidden[name=I]").val();
//    var Name = $(_this).closest("tr").find("input:hidden[name=N]").val();
//    var Value = $(_this).attr('OP');
//    var list = $(_this).attr('list');

//    var dataObject = JSON.stringify({
//        'ID': ID,
//        'Value': Value,
//        'Doctype': list
//    });
//    var Msg = '';
//    if (Value == 1) {
//        Msg = 'Are you sure want to Activate ' + Name;
//    }
//    else {
//        Msg = 'Are you sure want to Deactivate ' + Name;
//    }
//    ConfirmMsgBox(Msg, '', function () {
//        $.ajax({
//            url: "/CommonAjax/UpdateColumn_CommonJson",
//            type: "Post",
//            contentType: 'application/json',
//            async: true,
//            data: dataObject,
//            success: function (args) {
//                if (args.Status) {
//                    if (parseInt(Value) == 0) {
//                        $(_this).find('i').attr("class", "fa fa-times-circle crossred");
//                        $(_this).attr("OP", "1");
//                        $(_this).attr("data-original-title", "Click to Activate");

//                        if (list.split('_')[1].toLowerCase() == "isactive") {
//                            $(_this).closest("tr").addClass("trrowred");
//                        } else {
//                            $(_this).addClass("colorred").removeClass("colorgreen");

//                        }
//                    }
//                    else {
//                        $(_this).find('i').attr("class", "fa fa-check-circle checkgreen");
//                        $(_this).attr("OP", "0");
//                        $(_this).attr("data-original-title", "Click to DeActivate");

//                        if (list.split('_')[1].toLowerCase() == "isactive") {
//                            $(_this).closest("tr").removeClass("trrowred");
//                        } else {
//                            $(_this).addClass("colorgreen").removeClass("colorred");

//                        }
//                    }
//                }
//                else {

//                    FailToaster(args.SuccessMessage);
//                }
//            },
//            error: function (er) {
//                console.log(er);
//            }
//        });
//    })
//}



//$('.listpriority').blur(function () {
//    UpdatePriority(this);
//});
//$('.listpriority').focus(function () {
//    if ($(this).val() == '0') { $(this).val('') }
//});


//function UpdatePriority(obj) {
//    var _this = $(obj);
//    if ($(_this).val() == '') { $(_this).val('0') }
//    var ID = $(_this).closest("tr").find("input:hidden[name=I]").val();
//    var list = $(_this).attr('list');
//    var Val = Number($(_this).val());

//    var dataObject = JSON.stringify({
//        'ID': ID,
//        'Value': Val,
//        'Doctype': list
//    });

//    $.ajax({
//        url: "/CommonAjax/UpdateColumn_CommonJson",
//        type: "Post",
//        contentType: 'application/json',
//        async: true,
//        data: dataObject,
//        success: function (data) {
//            if (data.Status) {
//                $(_this).css('border', '2px solid Green');
//                SuccessToaster(data.SuccessMessage);
//            }
//            else {

//                $(_this).css('border', '2px solid red');
//                FailToaster(data.SuccessMessage);
//            }

//        },
//        error: function (er) {
//            console.log(er);
//        }
//    });
//}


function GetDropDownData(selectedID, Value) {
   
    var dataObject = JSON.stringify({
        'ID': selectedID,
        'Doctype': Value,
    });
    var data = $.parseJSON($.ajax({
        url: '/CommonAjax/GetDropDownListJson',
        type: "post",
        data: dataObject,
        contentType: 'application/json',
        async: false
    }).responseText);
    return data;
}



function DelRecord_Common(ID, Doctype) {
    if (ID != 0) {
        var dataObject = JSON.stringify({
            'ID': ID,
            'Doctype': Doctype
        });
        var data = $.parseJSON($.ajax({
            url: '/CommonAjax/DelRecord_CommonJson',
            type: "post",
            data: dataObject,
            contentType: 'application/json',
            async: false
        }).responseText);
        return data;
    }
}
function DelRecord_Prospect(obj) {
    var Response = 0;
    var ID = $(obj).closest('tr').find('.I').val();  
   
    ConfirmMsgBox("Are you sure,you want to delete", '', function () {
        if (ID) {
            var data = DelRecord_Common(ID, 'FR_PROSPECT');
            if (data.Status) {
                Response = 1;
                FailToaster(data.SuccessMessage);
                $(obj).closest('tr').remove();
            }
            else {
                FailToaster(data.SuccessMessage);

            }
        }       
    })
}
function GetGenerateValues(AllIDs, Doctype) {
    if (AllIDs != "") {
        var dataObject = JSON.stringify({
            'AllIDs': AllIDs,
            'Doctype': Doctype
        });
        var data = $.parseJSON($.ajax({
            url: '/CommonAjax/GetGenerateValuesJSon',
            type: "post",
            data: dataObject,
            contentType: 'application/json',
            async: false
        }).responseText);
        return data;
    }
}

function downloadURI(uri, name) {   
    uri = "/Attachments/PDF/" + uri;    
    var link = document.createElement("a");
    // If you don't know the name or want to use
    // the webserver default set name = ''
    link.setAttribute('download', name);
    link.href = uri;
    document.body.appendChild(link);
    link.click();
    link.remove();
}

function exportToExcelSahaj(Exportid,ReportName) {
    var htmls = "";
    var uri = 'data:application/vnd.ms-excel;base64,';
    var template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>';
    var base64 = function (s) {
        return window.btoa(unescape(encodeURIComponent(s)))
    };

    var format = function (s, c) {
        return s.replace(/{(\w+)}/g, function (m, p) {
            return c[p];
        })
    };

    htmls = document.getElementById(Exportid).innerHTML;

    var ctx = {
        worksheet: ReportName,
        table: htmls
    }


    var link = document.createElement("a");
    link.download = ReportName + ".xls";
    link.href = uri + base64(format(template, ctx));
    link.click();
}