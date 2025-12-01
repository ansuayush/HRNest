function ShowRequestForComOFfNonMitr() {
    var Month = $("#Month").val();
    var Empids = $("#ddlEmp").val();
    ShowLoadingDialog();
    $.ajax({
        url: "/Leave/_RequestForCompensatoryOffNonMitr",
        type: "Post",
        data: {
            src: EncryptQueryStringJSON(MenuID + "*" + "/Leave/_RequestForCompensatoryOffNonMitr" + "*" + Month + "*" + Empids)
},
success: function (data) {
    $("#DivComOFF").html(data);
    $("#RequestCompOffModal").modal('show');
    ApplyScriptsRequestForCompOff();
    CloseLoadingDialog();
    $("#hfCompoffPopUPOpen").val('1');

}
			});
        }

function ApplyScriptsRequestForCompOff() {

    BindAllCheckBox();
    //var form = $("form")
    //    .removeData("validator")
    //    .removeData("unobtrusiveValidation");
    //$.validator.unobtrusive.parse(form);

    $(".btnCompensatory").click(function () {
        return Myvalidate();
    });

}


function BindAllCheckBox() {
    chkAll();
    SinglecheckBind();
}
function SinglecheckBind() {

    $(".aaprovedhours").hide();
    $("#tableApprove input[type=checkbox]").change(function () {
        if ($(this).is(":checked")) {
            $(this).parents("tr:eq(0)").find(".aaprovedhours").show();
           // $(this).parents("tr:eq(0)").find(".aaprovedhours").val($(this).parents("tr:eq(0)").find(".appliedhours").val());
            $(this).parents("tr:eq(0)").find(".aaprovedhours").val('0');
        }
        else {
            $(this).parents("tr:eq(0)").find(".aaprovedhours").hide()
        }
    });
}

function chkAll() {
    $("#checkAll").change(function () {
        if ($(this).is(":checked")) {
            $(".chksingle").each(function () {
                $(this).prop('checked', true);
                $(this).parents("tr:eq(0)").find(".aaprovedhours").show();
               // $(this).parents("tr:eq(0)").find(".aaprovedhours").val($(this).parents("tr:eq(0)").find(".appliedhours").val());
                $(this).parents("tr:eq(0)").find(".aaprovedhours").val('0');
            });
        }
        else {
            $(".chksingle").each(function () {
                $(this).prop('checked', false);
                $(this).parents("tr:eq(0)").find(".aaprovedhours").hide()
            });
        }
    });
}



function Myvalidate() {

    var checked_checkboxes = $("#tableApprove input[type=checkbox]:checked");
    if (checked_checkboxes.length === 0) {
        swal('please Select atleast one checkbox', '');
        return false;

    }

    return true;
}

function OnSuccessRequest(objsss) {
    if (objsss.Status) {
        $("#RequestCompOffModal").modal('hide');
		SuccessToaster(objsss.SuccessMessage);
		//if ($('#IsHaveComOff') != undefined) {
		//	$('#IsHaveComOff').val('0');
		//}
    }
    else {
        FailToaster(objsss.SuccessMessage);
    }

}
