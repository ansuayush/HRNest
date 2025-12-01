$(document).ready(function () {
    //BindOnboardingStatus();
    BindHRScreenOnboard();

});

function BindHRScreenOnboard() {
    var mArray = "";
    CommonAjaxMethod(virtualPath + 'OnboardingRequest/BindHRScreenOnboard', null, 'GET', function (response) {
        mArray = response.data.data.Table;
        var cArray = response.data.data.Table1;
        $('#hrTable').html('');
        var newtbblData1 = '<table id="tblOnboardUsers" class="fold-table table mt-0 " >' +
            ' <thead>' +
            ' <tr>' +
            ' <th class="tw-15 b-0 bg-n"></th>' +
            ' <th class="tw-10">S.No</th>' +
            ' <th class="tw-200">Job Title</th>' +
            ' <th class="tw-100 hide-th">No. of Candidate</th>' +
            ' <th class="tw-200 hide-th">Project</th>' +
            ' <th class="tw-150 ">Location</th>' +
            ' <th style="display: none">REC_ReqID</th>' +
            ' </tr>' +
            ' </thead>';
        var html1 = "</table>";
        var tableData = "";
        var serialNo = 0;
        for (let i = 0; i < mArray.length; i++) {
            serialNo = 0;
            var newtbblData = "<tr class=\"view\" ><td></td><td>" + mArray[i].RowNum + "</td><td>" + mArray[i].JobTitle + "</td><td>" + mArray[i].NumberOfCandidate + "</td><td>" + mArray[i].Project + "</td><td>" + mArray[i].Location + "</td><td style=\"display: none\">" + mArray[i].REC_ReqID + "</td></tr>";
            var childData = cArray.filter(function (itemParent) { return (itemParent.REC_ReqID == mArray[i].REC_ReqID); });
            var childStringData = '';
            var childStringDataHeader = '<tr class="fold"> <td colspan="6"> <div class="fold-content m-scrl mb-lst"> <table id="tblChildOnboardUsers' + i + '"  class="table mt-0"> <thead><tr><td class="tw-10">S.No.</td><td>Candidate Name</td><td>Phone Number</td><td>Email ID</td><td>Offer Issue Date</td><td class="tw-200">Status</td></tr></thead><tbody>';
            for (var j = 0; j < childData.length; j++) {
                serialNo = j + 1;
                var cData = childData[j].IssuesDOFLetter == null ? "" : childData[j].IssuesDOFLetter;
                //cData = ChangeDateFormatToddMMYYY(cData);
                childStringData += '<tr><td>' + serialNo + '</td><td><a href="#" onclick="RedirectToDestination(' + childData[j].ID + ',' + childData[j].StatusCode + ');" >' + childData[j].Candidate + '</a></td><td>' + childData[j].PhoneNumber + '</td><td>' + childData[j].EmailID + '</td><td>' + cData + '</td><td><strong >' + childData[j].Status + '</strong></td></tr>';
            }

            var childStringDataFooter = '</tbody></table></div></td></tr>';
            var allstring = newtbblData + childStringDataHeader + childStringData + childStringDataFooter;

            tableData += allstring;
            

        }
        $('#hrTable').html(newtbblData1 + tableData + html1);
    });

    HtmlPaging();
    for (let i = 0; i < mArray.length; i++) {
        HtmlChildPaging(i);
    }
}
function HtmlChildPaging(gId) {
    $('#tblChildOnboardUsers' + gId + '').after('<div id="divChildNav' + gId + '" style="text-align:right"></div>');
    var rowsShown = 5;
    var rowsTotal = $('#tblChildOnboardUsers' + gId + ' tbody tr').length;
    var numPages = rowsTotal / rowsShown;
    for (i = 0; i < numPages; i++) {
        var pageNum = i + 1;
        $('#divChildNav' + gId + '').append('<a href="#" rel="' + i + '">' + pageNum + '</a> ');
    }
    $('#tblChildOnboardUsers' + gId + ' tbody tr').hide();
    $('#tblChildOnboardUsers' + gId + ' tbody tr').slice(0, rowsShown).show();
    $('#divChildNav' + gId + ' a:first').addClass('active');
    $('#divChildNav' + gId + ' a').bind('click', function () {
        $('#divChildNav' + gId + ' a').removeClass('active');
        $(this).addClass('active');
        var currPage = $(this).attr('rel');
        var startItem = currPage * rowsShown;
        var endItem = startItem + rowsShown;
        $('#tblChildOnboardUsers' + gId + ' tbody tr').css('opacity', '0.0').hide().slice(startItem, endItem).
            css('display', 'table-row').animate({ opacity: 1 }, 300);
    });
}
function HtmlPaging() {
    $('#tblOnboardUsers').after('<div id="divNav" style="text-align:right"></div>');
    var rowsShown = 10;
    var rowsTotal = $('#tblOnboardUsers tbody tr.view').length;
    var numPages = rowsTotal / rowsShown;
    for (i = 0; i < numPages; i++) {
        var pageNum = i + 1;
        $('#divNav').append('<a class="e-link e-numericitem e-spacing e-currentitem e-active" href="#" rel="' + i + '">' + pageNum + '</a> ');
    }
    $('#tblOnboardUsers tbody tr.view').hide();
    $('#tblOnboardUsers tbody tr.view').slice(0, rowsShown).show();
    $('#divNav a:first').addClass('active');
    $('#divNav a').bind('click', function () {
        $('#divNav a').removeClass('active');
        $(this).addClass('active');
        var currPage = $(this).attr('rel');
        var startItem = currPage * rowsShown;
        var endItem = startItem + rowsShown;
        $('#tblOnboardUsers tbody tr.view').css('opacity', '0.0').hide().slice(startItem, endItem).
            css('display', 'table-row').animate({ opacity: 1 }, 300);
    });
}

function RedirectToDestination(id, sourceCode) {
    $('#hdnCanId').val('');
    if (sourceCode == '1') {
        $('#hdnCanId').val(id);
        $("#btnMulti").click();
    }
    else if (sourceCode == '2') {
        $('#hdnCanId').val(id);
        RedirectToOffer();
    }
    else if (sourceCode == '3') {
        $('#hdnCanId').val(id);
        RedirectToJoiningkit();
    }
    else if (sourceCode == '4') {
        $('#hdnCanId').val(id);
        RedirectToPreRegistration();
    }
    else if (sourceCode == '5') {
        $('#hdnCanId').val(id);
        RedirectToRegistration();
    }
    //else if (sourceCode == '5') {
    //    $('#hdnCanId').val(id);
    //    RedirectToDeclaration();
    //}
    else if (sourceCode == '8') {
        $('#hdnCanId').val(id);
        RedirectToOffer();
    }
    else if (sourceCode == '14') {
        $('#hdnCanId').val(id);
        RedirectToOffer();
    }
    else if (sourceCode == '15') {
        $('#hdnCanId').val(id);
        RedirectToOffer();
    }
    else if (sourceCode == '16') {
        $('#hdnCanId').val(id);
        RedirectToJoiningkit();
    }
    else if (sourceCode == '10') {
        $('#hdnCanId').val(id);
        RedirectToOrientationSchedule();
    }
    else if (sourceCode == '11') {
        $('#hdnCanId').val(id);
        RedirectToOffer();
    }
    else if (sourceCode == '12') {
        $('#hdnCanId').val(id);
        RedirectToAttachment();
    }
    else if (sourceCode == '13') {
        $('#hdnCanId').val(id);
        RedirectToUserOnboarding();
    }
    else if (sourceCode == '9') {
        $('#hdnCanId').val(id);
        RedirectToUserOnboarding();
    }
    return false;
}
function RedirectToUserOnboarding() {
    var id = $('#hdnCanId').val();
    window.location.href = virtualPath + 'Onboarding/UserOnboarding?id=' + id;
}
function RedirectToOffer() {
    var id = $('#hdnCanId').val();
    window.location.href = virtualPath + 'Onboarding/Offer?id=' + id;
}
function RedirectToJoiningkit() {
    var id = $('#hdnCanId').val();
    window.location.href = virtualPath + 'Onboarding/Joiningkit?id=' + id;
}
function RedirectToPreRegistration() {
    var id = $('#hdnCanId').val();
    window.location.href = virtualPath + 'Onboarding/PreRegistration?id=' + id;
}
function RedirectToRegistration() {
    var id = $('#hdnCanId').val();
    window.location.href = virtualPath + 'Onboarding/Registration?id=' + id;
}
function RedirectToAttachment() {
    var id = $('#hdnCanId').val();
    window.location.href = virtualPath + 'Onboarding/Attachment?id=' + id;
}
function RedirectToDeclaration() {
    var id = $('#hdnCanId').val();
    window.location.href = virtualPath + 'Onboarding/UserOnboarding?id=' + id;
}
function RedirectToOrientationSchedule() {
    var id = $('#hdnCanId').val();
    window.location.href = virtualPath + 'Onboarding/OrientationSchedule?id=' + id;
}
function ChangeStatus() {
    var id = $('#hdnCanId').val();
    var Consultant = {
        CandidateId: id
    }
    var obj = {

        OnboardingConsultantData: Consultant
    }
    CommonAjaxMethod(virtualPath + 'OnboardingRequest/SaveConsultant', obj, 'POST', function (response) {
        window.location.reload();

    });

}

function GetOfferLetter(ctrl) {
    alert(ctrl);
}