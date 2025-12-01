


function GetOTPPartial() {

    var Category = "";
    $.ajax({
        url: "/Account/_OTPPartial",
        type: "Get",
        success: function (data) {
            $(".CommmonLayoutModalDiv").html(data);
            $(".spnCommmonLayout").html("Enter OTP");
            $('.CommmonLayoutModal').modal({
                backdrop: 'static',
                keyboard: false
            });
        },
        error: function (er) {

        }
    });
}

function ActionOTPPartial(obj) {
    if (obj.Status) {
        $(".CommmonLayoutModal").modal('hide');
    }
    else {
        FailToaster(obj.SuccessMessage);
    }

}
function GetSessionValueJson(SessionName) {
    $.ajax({
        url: "/CommonAjax/GetSessionValueJson",
        type: "Get",
        async: true,
        data: { SessionName: SessionName },
        success: function (data) {
            if (SessionName === "OTP") {
                if (!data.Status) {
                    GetOTPPartial();
                }
            }
            else {
                if (data.Status) {
                    FailToaster(data.SuccessMessage);
                }
            }
         
        },
        error: function (er) {

        }
    });
}