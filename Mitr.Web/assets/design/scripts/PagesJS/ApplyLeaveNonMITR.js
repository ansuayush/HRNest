$(document).ready(function () {
    //$('[data-toggle="tooltip"]').tooltip();
    //$(".applyselect").select2();
  
        ShowLoadingDialog();
        $("#btnSearch").click();  
});

$(document).ready(function () {
    $('.btnNext').click(function () {
      
        $('.nav-tabs .active').parent().next('li').find('a').trigger('click');
        $('.nav-tabs .active').parent().prev('li').find('a').addClass("prvactive");
    });

    $('.btnPrevious').click(function () {
        $('.nav-tabs .active').parent().prev('li').find('a').trigger('click');
    });
});
function ClickTab(obj)
{
    $("#" + obj).click();
}
function List(Tab)
{
    $("#hfTab").val(Tab);
    $("#btnSearch").click();
}
function CheckLeaveHoursNew(obj,Balance)
{
 ;

   var LeaveBal=parseInt(Balance)
    var hours = parseInt($(obj).val());   

// add code comment bu pooja 02092024
    if ($(obj).val() != "")
    {
       // if (Balance < hours) {
          //  $(obj).val(LeaveBal);
      //  }
      //  else
       // {
      //      $(obj).val(LeaveBal);
       // }
    }

    
}


function CheckLeaveHours(Newobj,Balance)
{

 ;
   var LeaveBal=parseInt(Balance)
    var hours = $(Newobj).val();   
   // if ($(obj).val() != "")
   // {
      //  if (parseInt($(obj).val()) > 8) {
      //      $(obj).val('');
      //  }
      //  else
      //  {
      //      $(obj).val(hours);
       // }
  //  }

   if (parseInt($(Newobj).val()) > 8) {
           $(Newobj).val('');
       }
        else
       {
 ;
          var FinalDaysHour = 0;
          $(Newobj).closest("tr").find(".hrinp").each(function () {
        
             if ($(this).val() == "") {
                 FinalDaysHour +=0;
                 }
                else{
                  FinalDaysHour +=parseInt($(this).val());
              }
              
          });
         // if(FinalDaysHour>LeaveBal){
          //     $(Newobj).val('');
     //  }


        //$(obj).val(hours);
     }


    
}

function CalculateLeaveHours(count) {

    var FinalDaysHour = 0;
    $(".dayshours_" + count).each(function (index) {
        FinalDaysHour += parseInt(Number($(this).val()));
    });

    if (FinalDaysHour > 24) {
        swal('Hey! Looks like you are working more than 24 hours in day!', '');
        $(".dayshours_" + count).val("0");
        $(".spnFinalHour_" + count).html("0");
    }
    else {
        $(".spnFinalHour_" + count).html(FinalDaysHour);
    }
}

function BindTarget(args) {
    if ($("#hfTab").val() == "LEAVE")
    {
        $("#TargetDiv").html(args);
    }
    else if ($("#hfTab").val() == "DAILYLOG") {
        $("#TargetDivLogEntry").html(args);
        $(".dpselect").select2();
    }
    else if ($("#hfTab").val() == "MONTHLYLOG") {
        $("#TargetDivMonthlyLog").html(args);
        $(".btnrequestforCO").click(function (e) {
          
            ShowRequestForComOFfNonMitr();
        });
        $(".dpselect").select2();
    }
    else if ($("#hfTab").val() == "SUBMITTED") {
        $("#TargetDivSubmitted").html(args);      
        $(".dpselect").select2();
    }
   
    $('[data-toggle="tooltip"]').tooltip();   
    //var form = $("#_SaveDailyLogForm").closest("form");
    //form.removeData('validator');
    //form.removeData('unobtrusiveValidation');
    //$.validator.unobtrusive.parse(form);

    //var IsSubmitted = $("#IsSubmitted").val();   
    //$("#btnVerifyAndSave").click(function () {
    //    if (ValidateDays()) {
    //        ValidateVerfiyAndSave();
    //    }
    //});
    //if (IsSubmitted == "False") {
    //    GetHoursSummary();
    //    $('.hideaftersub').show();
    //} else {
    //    $('.hideaftersub').hide();
    //}
    //$(".Anchrrecall").click(function (e) {
    //    e.preventDefault();
    //    fnRecall();
    //});

    CloseLoadingDialog();
}
function OnLeaveSuccess(obj)
{
    //FailToaster("Leave Applied successfully");
    //$("#tab-B").click();

	if (obj.Status) {
        FailToaster("Leave updated successfully");
        $("#tab-B").click();
    }
    else {
        CloseLoadingDialog();
        FailToaster(obj.SuccessMessage);
    }

}

function OnAttachmentSuccess(obj) {   
    if (obj.Status) {
        CloseLoadingDialog();
        $("#" + obj.AdditionalMessage).val("1");

       // $("#147#1" + obj.AdditionalMessage).val("1");
        $("#Modalcllll").modal("hide");
    }
    else {
        CloseLoadingDialog();
        FailToaster(obj.SuccessMessage);
    }

}

function AfterOnSuccess(obj) {

    if (obj.Status) {
        if (obj.StatusCode == 0) {
            SuccessToaster(obj.SuccessMessage);
        }
        if (obj.OtherID == "0") {
            $("#tab-A").click();
        }
        else
        {
            $("#tab-C").click();
        }

      
    }
    else {
        if (obj.StatusCode == 1) {
            swal("Hey! Please verify entries on", obj.SuccessMessage);
        }
        else {
            FailToaster(obj.SuccessMessage);
        }
        CloseLoadingDialog();
    }    
}
function BindTargetAfterSubmit(ogb) {
    if (ogb.Status) {
       // window.location = ogb.RedirectURL;
        FailToaster(ogb.SuccessMessage);
        if (ogb.OtherID == "0") {
            $("#tab-B").click();
        }
        else {
            $("#tab-C").click();
        }
        
    }
    else {
        FailToaster(ogb.SuccessMessage);
        CloseLoadingDialog();
    }
}

function DeleteAttachment(ID) {
    ConfirmMsgBox('Do you really want to delete Attachment', '', function () {
        $.ajax({
            url: "/CommonAjax/DeleteAttachmentJSON",
            type: "Post",
            async: true,
            data: { AttachmentID: ID },
            success: function (data) {
                if (data) {                 
                    var Empid = $("#Empid").val();
                    var Leaveid = $("#Leaveid").val();
                    var id = $("#id").val();
                    AttachmentNonMitr(id, Empid, Leaveid)
                }
                else {
                    FailToaster('Can not delete');
                }
            },
            error: function (er) {
                alert(er);
            }
        });
    });
}




$('#ddlEmp').change(function () {
    var ids = $("#ddlEmp").val();
    $("#Empids").val(ids);
});
$('#ddlLeave').change(function () {
    var ids = $("#ddlLeave").val();
    $("#LeaveTypeids").val(ids);
});

$(".btnOk").on('click', function (e) {
    e.preventDefault();
    e.stopPropagation();
    AddLeaveApplyTable();
});

$('.btnAddRow').on('click', function (e) {
    e.preventDefault();
    e.stopPropagation();
    AddNewRow();
});
$('.deleterow').on('click', function (e) {
    e.preventDefault();
    e.stopPropagation();
    DeleteRow(this);
});

$(".btnApplyLeave").click(function (e) {
    e.preventDefault();
    e.stopPropagation();
    return Validate();
});


$('.txtDate').bind("change", function () {
    if ($(this).val()) {
        HighlightDateCalender();
    }

});


$('.ddleaveType').bind("change", function () {
    ShowLoadingDialog();
    ShowLeaveAttachment();
});


function HighlightDateCalender() {
   
    $('[class*="fc-daygrid-day"]').removeClass('highlighteddate')
    $("#tblApplyLeave TBODY TR").each(function (i) {
        var textvalue = $(this).find("td:eq(1) input").val();
        if (textvalue) {
            var mydate = new Date(textvalue)
            var dateString = moment(mydate).format('YYYY-MM-DD');            
            $('[class*="fc-daygrid-day"]').filter(function () {
                return $(this).attr('data-date') === dateString;
            }).addClass('highlighteddate');
        }
    });
}

function calculatehours() {
    var Total = 0
    $(".txthours").each(function () {
        var current = Number($(this).val());
        Total += parseFloat(current);
        if (current > 8) {
            $.notify($(this), "Leave Hours Can't Be Greater than 8 Hours", "error");
            $(this).focus();
            $(this).val('');
        }
    });
    $("#spnTotalHour").html(Total.toFixed(0));
}



function Validate() {
    
    var AllowedCLSL = GetConfigValueJSON("AllowedCLSL");
    var result = false;
    var show = 0, AttachCount = 0;
    var LeaveTypeID = $("#LeaveTypeID option:selected").val();
    $('.ddleaveType').each(function (index) {
        if ($(this).find("option:selected").val() == 1) {
            show += 1;
        }
    });
    $('.uploadCertificate').each(function (index) {
        if ($(this).val() != "") {
            AttachCount++;
        }
    });

    if (LeaveTypeID == "1") {
        show = show - 1;
    }
    if (show > AllowedCLSL && AttachCount == 0) {

        ConfirmMsgBox("", 'Please remember to submit Medical/ Fitness certificate for this leave request.', function () {
            $("#btnSubmit").click();
        })
    }
    else {
        $("#btnSubmit").click();
    }
    return result;

}

function ShowLeaveAttachment() {
    CloseLoadingDialog();
    var AllowedCLSL = GetConfigValueJSON("AllowedCLSL");
    var show =-1;
    $('.ddleaveType').each(function (index) {
        if ($(this).find("option:selected").val() == 1) {
            show += 1;
        }
    });

    if (show > AllowedCLSL) {
        $(".divCLSL").show();
    }
    else {
        $(".divCLSL").hide();
    }
    ShowExpectedDeliveryDate();
}

function ShowExpectedDeliveryDate() {
    CloseLoadingDialog();
    var show = 0;
    $('.ddleaveType').each(function (index) {
        if ($(this).find("option:selected").val() == 5) {
            show = 1;
            $(".lblEDD").html('Expected Delivery Date');
        }
        if ($(this).find("option:selected").val() == 7) {
            show = 1;
            $(".lblEDD").html('Expected Child birth Date');
        }
    });
    if (show == 1) {
        $(".divEDD").show();
    }
    else {
        $(".divEDD").hide();
    }
}

function AddNewRow() {

    var LastTRCount = parseInt($('#tblApplyLeave').find("tbody tr").length) - 1;
    $('.applyselect').select2("destroy");
    var $tableBody = $('#tblApplyLeave').find("tbody"),
        $trLast = $tableBody.find("tr:last"),
        $trNew = $trLast.clone();
    $trNew.find("td:last").html('<a class="deleterow"><span class="close">X</span></a>');

    $trNew.find("label").each(function () {
        $(this).attr({
            'id': function (_, id) { var arr = id.split('_'); return id.replace(arr[1], LastTRCount + 1); },
        });
        $(this).html(parseInt($('#tblApplyLeave').find("tbody tr").length) + 1);
    });
    $trNew.find("input").each(function (i) {
        $(this).attr({
            'id': function (_, name) { return name.replace('_' + LastTRCount + '_', '_' + (parseInt(LastTRCount) + 1) + '_'); },
            'name': function (_, name) { return name.replace('[' + LastTRCount + ']', '[' + (parseInt(LastTRCount) + 1) + ']'); },
        });
        $(this).val('');
    });
    $trNew.find("select").each(function (i) {
        $(this).attr({
            'id': function (_, name) { return name.replace('_' + LastTRCount + '_', '_' + (parseInt(LastTRCount) + 1) + '_'); },
            'name': function (_, name) { return name.replace('[' + LastTRCount + ']', '[' + (parseInt(LastTRCount) + 1) + ']'); },
        });
    });

    $trNew.find("span").each(function (i) {
        if ($(this).attr("data-valmsg-for")) {
            $(this).attr({
                'data-valmsg-for': function (_, name) { return name.replace('[' + LastTRCount + ']', '[' + (parseInt(LastTRCount) + 1) + ']'); }
            });
        }
        if ($(this).attr("for")) {
            $(this).attr({
                'for': function (_, name) { return name.replace('_' + LastTRCount + '_', '_' + (parseInt(LastTRCount) + 1) + '_'); }
            });
        }
    });

    $trLast.after($trNew);
    $(".applyselect").select2();
    var form = $("#LeaveRequestForm").closest("form");
    form.removeData('validator');
    form.removeData('unobtrusiveValidation');
    $.validator.unobtrusive.parse(form);
    $('[data-toggle="tooltip"]').tooltip();
    $('.deleterow').on('click', function (e) {
        e.preventDefault();
        e.stopPropagation();
        DeleteRow(this);
    });
    calculatehours();
    $('.txthours').bind("change", function () {
        calculatehours();
    });
    $('.txtDate').bind("change", function () {
        if ($(this).val()) {
            HighlightDateCalender();
        }

    });
    ShowLeaveAttachment();

}


function DeleteRow(obj) {
    var count = 0;
    var TotalRowCount = $('#tblApplyLeave').find("tbody tr").length;
    ConfirmMsgBox("Are you sure want to delete this row", '', function () {
        $(obj).closest('tr').remove();
        $("#tblApplyLeave TBODY TR").each(function (i) {
            $(this).closest("tr").find("label").each(function () {
                $(this).attr({
                    'id': function (_, id) { var arr = id.split('_'); return id.replace(arr[1], i); },
                });
                $(this).html(i + 1)
            });

            $(this).closest("tr").find("input").each(function () {
                $(this).attr({
                    'id': function (_, id) { return id.replace('_' + (parseInt(i) + 1) + '_', '_' + i + '_'); },
                    'name': function (_, name) { return name.replace('[' + (parseInt(i) + 1) + ']', '[' + i + ']'); },
                });

            });

            $(this).closest("tr").find("select").each(function () {
                $(this).attr({
                    'id': function (_, id) { return id.replace('_' + (parseInt(i) + 1) + '_', '_' + i + '_'); },
                    'name': function (_, name) { return name.replace('[' + (parseInt(i) + 1) + ']', '[' + i + ']'); },
                });
            });
            $(this).closest("tr").find("span").each(function () {
                if ($(this).attr("data-valmsg-for")) {
                    $(this).attr({
                        'data-valmsg-for': function (_, name) { return name.replace('[' + (parseInt(i) + 1) + ']', '[' + i + ']'); },
                    });
                }
                if ($(this).attr("for")) {
                    $(this).attr({
                        'for': function (_, id) { return id.replace('_' + (parseInt(i) + 1) + '_', '_' + i + '_'); },
                    });
                }
            });
        });
        var form = $("#LeaveRequestForm").closest("form");
        form.removeData('validator');
        form.removeData('unobtrusiveValidation');
        $.validator.unobtrusive.parse(form);
        calculatehours();
        ShowLeaveAttachment();
    })
}

$('#EndDate').change(function () {
	AutomateProcess();
})  


$('#StartDate').change(function () {
	AutomateProcess();
})  


$('#LeaveTypeID').change(function () {
	AutomateProcess();
})  

function AutomateProcess() {
	var E = $('#EndDate').val();
	var S = $('#StartDate').val();
	var DD = $('#LeaveTypeID').find("option:selected").val();
	if (E != "" && S != "" && DD != "") {
		$(".btnOk").click();
	}
}
function SubmitMonthlyLog()
{    
    if ($("#hfCompoffPopUPOpen").val() == "0") {
        ConfirmMsgBox('Comp -off action is pending, Continue to submit?', '', function () {
            $("#btnsubmitMonthlyLog").click();
        });
    }
    else
    {
        ConfirmMsgBox('Are you sure?', '', function () {
            $("#btnsubmitMonthlyLog").click();
        });
      
    }
}

//$("#EndDate").on('onblur', function (e) {
//	alert('')
//});