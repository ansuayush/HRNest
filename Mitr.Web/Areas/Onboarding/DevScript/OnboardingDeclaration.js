var iScandidateStatus = false;
var userId = "";
var empId = "";
$(document).ready(function () {
    CommonAjaxMethod(virtualPath + 'OnboardingRequest/BindonboardingCandidateStatus', { CandidateId: CandidateId, inputData: 15 }, 'GET', function (response) {
        var dataJoiningKit = response.data.data.Table;
        var masterEmp = response.data.data.Table1;
        if (masterEmp.length>0) {
            userId = response.data.data.Table1[0].user_id;
            empId = response.data.data.Table1[0].id;
        }
       
        if (dataJoiningKit.length > 0) {
            if (dataJoiningKit[0].Status > 11) {
                iScandidateStatus = true;
                showtable(1)
            }
        }
    });
});

function RedirectToClick(View) {
    window.location.href = virtualPath + 'Onboarding/' + View + '?id=' + CandidateId;
}
