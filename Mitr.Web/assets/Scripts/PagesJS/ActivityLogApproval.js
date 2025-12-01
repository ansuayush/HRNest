

// bind all OT CO pop UP

// All OT CO Main Function




function BinCOOTPOpupDetails() {
    $('.bindUnApprovedCO').on('click', function (e) {
        e.preventDefault();
        e.stopPropagation();
        var E = $(this).closest("tr").find("input:hidden[name=E]").val();
        var N = $(this).closest("tr").find("input:hidden[name=N]").val();
        var D = $(this).closest("tr").find("input:hidden[name=D]").val();
        GetCompensatoryOffApprovalList(E, N, D);
    });
    $('.bindUnApprovedOT').on('click', function (e) {
        e.preventDefault();
        e.stopPropagation();
        var E = $(this).closest("tr").find("input:hidden[name=E]").val();
        var N = $(this).closest("tr").find("input:hidden[name=N]").val();
        var D = $(this).closest("tr").find("input:hidden[name=D]").val();
        GetApproveOTList(E, N, D);
    });
}

function GetCompensatoryOffApprovalList(EMPID, EMPNAme, Date) {
    $("#spanOTCOEMPName").html("Compensatory Off " + EMPNAme);
    $.ajax({
        url: "/Activity/_CompensatoryOffApproval",
        type: "Post",
        data: {
            src: EncryptQueryStringJSON(MenuID + "*" + "/Activity/_CompensatoryOffApproval"), EMPID: EMPID, Date: Date, Approve: "0"
        },
        success: function (data) {
            $("#DivOTCO").empty();
            $("#DivOTCO").html(data);
            $("#ModalOTCO").modal('show');
            ApplyCOOTScripts();
        },
        error: function (er) {
            alert(er);

        }
    });
}
function GetApproveOTList(EMPID, EMPNAme, Date) {
    $("#spanOTCOEMPName").html("Approve OT " + EMPNAme);
    
    $.ajax({
        url: "/Activity/_ApproveOT",
        type: "Post",
        data: {
            src: EncryptQueryStringJSON(MenuID + "*" + "/Activity/_ApproveOT"), EMPID: EMPID, Date: Date, Approve: "0"
        },
        success: function (data) {
            $("#DivOTCO").empty();
            $("#DivOTCO").html(data);
            $("#ModalOTCO").modal('show');
            ApplyCOOTScripts();
        },
        error: function (er) {
            alert(er);

        }
    });
}

function OnSuccessAction(objsss) {
    if (objsss.Status) {
        $("#ModalOTCO").modal('hide');
        SuccessToaster(objsss.SuccessMessage);
    }
    else {
        FailToaster(objsss.SuccessMessage);
    }

}

function ApplyCOOTScripts() {
    DatatableScriptsWithColumnSearch("tableApprove");
    $(".btnCompensatory").click(function () {
        return Myvalidate();
    });
    BindAllCheckBox();
    var form = $("form")
        .removeData("validator")
        .removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse(form);
}



// Supporting fucntion for OT CO

function Myvalidate() {

    var checked_checkboxes = $("#tableApprove input[type=checkbox]:checked");
    if (checked_checkboxes.length === 0) {
        swal('please Select atleast one checkbox', '');
        return false;
    }
    return true;
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
            $(this).parents("tr:eq(0)").find(".aaprovedhours").val($(this).parents("tr:eq(0)").find(".appliedhours").val());
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
                $(this).parents("tr:eq(0)").find(".aaprovedhours").val($(this).parents("tr:eq(0)").find(".appliedhours").val());
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



