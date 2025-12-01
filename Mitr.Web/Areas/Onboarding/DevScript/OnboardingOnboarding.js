

$(document).ready(function () {
    BindOnboardUserOnboardRequest();
});


function BindOnboardUserOnboardRequest() {
    CommonAjaxMethod(virtualPath + 'OnboardingRequest/BindUserOnboard', { Id: CandidateId, inputData: 7 }, 'GET', function (response) {
        var mArray = response.data.data.Table;
        var statusRecord = response.data.data.Table1;
        if (statusRecord[0].Status == "14") {
            candodate_Status = true;
        }
        if (statusRecord[0].Status == "15") {
            candodate_Status = true;
        }
        if (statusRecord[0].Status == "16") {
            candodate_Status = true;
        }
        $('#tblUserOnboard').html('');
        var newtbblData1 = '<table id="tblOnboardUsers" class="fold-table table mt-0 " >' +
            ' <thead>' +
            ' <tr>' +
            ' <th class="tw-10">S.No</th>' +
            ' <th class="tw-150">Candidate Name</th>' +
            ' <th class="tw-100 hide-th">Phone Number</th>' +
            ' <th class="tw-150 hide-th">Email ID</th>' +
            ' <th class="tw-120 ">Offer Issue Date</th>' +
            ' <th class="tw-120 ">Offer Accepted Date</th>' +
            ' <th class="tw-150 ">Status</th>' +
            ' <th class="tw-100 ">Action</th>' +
            ' <th class="tw-100 ">Map Emp</th>' +
            ' </tr>' +
            ' </thead>';
        var html1 = "</table>";
        var tableData = "";
        var serialNo = 0;
        for (let i = 0; i < mArray.length; i++) {
            serialNo = 0;
            var OfferAcceptedDate = ChangeDateFormatToddMMYYYWithSlace(mArray[i].OfferAcceptedDate);// == null ? "" : mArray[i].OfferAcceptedDate;
            var newtbblData = "";
            if (mArray[i].IsMitrEmployee == false) {
                newtbblData = "<tr><td>" + mArray[i].RowNum + "</td><td>" + mArray[i].Candidate + "</td><td>" + mArray[i].PhoneNumber + "</td><td>" + mArray[i].EmailID + "</td><td>" + mArray[i].IssuesDOFLetter + "</td><td>" + OfferAcceptedDate + "</td><td>" + mArray[i].Status + "</td><td><a  onclick=\"ViewApplication(" + mArray[i].REC_AppID + ");\"><i class=\"fas fa-eye\"></i>View</a><td><a  onclick=\"SubmitMasterEmployee(" + mArray[i].REC_AppID + ");\"><i class=\"fas fa-eye\"></i>View</a></td></tr>";
            }
            else {
                newtbblData = "<tr><td>" + mArray[i].RowNum + "</td><td>" + mArray[i].Candidate + "</td><td>" + mArray[i].PhoneNumber + "</td><td>" + mArray[i].EmailID + "</td><td>" + mArray[i].IssuesDOFLetter + "</td><td>" + OfferAcceptedDate + "</td><td>" + mArray[i].Status + "</td><td><a  onclick=\"ViewApplication(" + mArray[i].REC_AppID + ");\"><i class=\"fas fa-eye\"></i>View</a><td><a  ><i class=\"fas fa-eye\" style=\"cursor:default!important\"></i>Mapped</a></td></tr>";
            }
            var allstring = newtbblData;
            tableData += allstring;
        }
        $('#tblUserOnboard').html(newtbblData1 + tableData + html1);
    });
    HtmlPaging();
}

function HtmlPaging() {
    $('#tblOnboardUsers').after('<div id="divNav" style="text-align:right"></div>');
    var rowsShown = 15;
    var rowsTotal = $('#tblOnboardUsers tbody tr').length;
    var numPages = rowsTotal / rowsShown;
    for (i = 0; i < numPages; i++) {
        var pageNum = i + 1;
        $('#divNav').append('<a class="e-link e-numericitem e-spacing e-currentitem e-active" href="#" rel="' + i + '">' + pageNum + '</a> ');
    }
    $('#tblOnboardUsers tbody tr').hide();
    $('#tblOnboardUsers tbody tr').slice(0, rowsShown).show();
    $('#divNav a:first').addClass('active');
    $('#divNav a').bind('click', function () {
        $('#divNav a').removeClass('active');
        $(this).addClass('active');
        var currPage = $(this).attr('rel');
        var startItem = currPage * rowsShown;
        var endItem = startItem + rowsShown;
        $('#tblOnboardUsers tbody tr').css('opacity', '0.0').hide().slice(startItem, endItem).
            css('display', 'table-row').animate({ opacity: 1 }, 300);
    });
}
function RedirectToClick(View) {
    window.location.href = virtualPath + 'Onboarding/' + View + '?id=' + CandidateId;
}

