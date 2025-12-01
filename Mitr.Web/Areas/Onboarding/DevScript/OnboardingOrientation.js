$(document).ready(function () {
    BindOrientationOnboard();
});
function BindOrientationOnboard() {
    var mArray = "";
    CommonAjaxMethod(virtualPath + 'OnboardingRequest/OrientationOnboard', { CandidateId: CandidateId, inputData: 10 }, 'GET', function (response) {
        mArray = response.data.data.Table;
        $('#OrientationTable').html('');
        var newtbblData1 = '<table id="tblOnboardUsers" class="table mt-0" >' +
            ' <thead>' +
            ' <tr>' +
            ' <th class="tw-10">S.No</th>' +
            ' <th class="tw-200">Name of Member</th>' +
            ' <th class="tw-100">Date</th>' +
            ' <th class="tw-200">Time</th>' +
            ' <th class="tw-200">Place</th>' +
            ' <th class="tw-200">Purpose</th>' +
            ' <th class="tw-100">Mode</th>' +
            ' <th class="tw-500">Feedback</th>' +
            ' <th class="tw-100">Status</th>' +
            ' </tr>' +
            ' </thead>';
        var html1 = "</table>";
        var tableData = "";
        var serialNo = 0;
        for (let i = 0; i < mArray.length; i++) {
            serialNo = 0;
            var cData = mArray[i].OriDate == null ? "" : mArray[i].OriDate;
            cData = ChangeDateFormatToddMMYYY(cData);
            var Feedback = mArray[i].Feedback == null ? "" : mArray[i].Feedback;   

            var Status = mArray[i].Feedback == null ? "Pending" : "Complete"; 
            var newtbblData = "<tr><td>" + mArray[i].RowNum + "</td><td>" + mArray[i].NameOfMember + "</td><td>" + cData + "</td><td>" + mArray[i].Time + "</td><td>" + mArray[i].Place + "</td><td>" + mArray[i].Purpose + "</td><td >" + mArray[i].Mode + "</td><td >" + Feedback + "</td><td class=\"green-clr\">" + Status + "</td></tr>";
          
            var allstring = newtbblData ;

            tableData += allstring;


        }
        $('#OrientationTable').html(newtbblData1 + tableData + html1);
    });
    HtmlPaging();
}
function HtmlPaging() {
    $('#tblOnboardUsers').after('<div id="divNav" style="text-align:right"></div>');
    var rowsShown = 10;
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
function RedirectToOnboard() {
	window.location.href = virtualPath + 'Onboarding/HRScreenOnboard';
}
