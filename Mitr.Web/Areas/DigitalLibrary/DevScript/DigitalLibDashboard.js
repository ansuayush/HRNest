
$(document).ready(function () {
    Dashboard();
     PageIndex = 0;
     TotalNumberOfPages = 0;    
    var obj = {
        ParentId: 0,
        masterTableType: DropDownTypeEnum.Category,
        isMasterTableType: false,
        isManualTable: false,
        manualTable: 0,
        manualTableId: 0
    }
    LoadMasterDropdown('ddlCategory', obj, 'Choose Your Category', false);

    LoadMasterDropdown('ProjectCode',
        {
            ParentId: 0,
            masterTableType: 0,
            isMasterTableType: false,
            isManualTable: true,
            manualTable: ManaulTableEnum.Project,
            manualTableId: 0
        }, 'Select', false);

    
  
    if (IsHOD != 'Y') {
              
        $('#divMyContent').show();
        document.getElementById("divMyContent").classList.remove("col-md-6");
        document.getElementById("divMyContent").classList.add("col-md-12");
    }
    else {
        $('#divMyContent').show();
        $('#dvMyTeamContent').show();
    }

});

function Dashboard()
{
    
    CommonAjaxMethod(virtualPath + 'DigitalLibrary/GetSharedContentCount', null, 'GET', function (response) {
        var d = response.data.data.Table;  
        $('#mySharedContent').text(d[0].TotalSharedContent);

    });
    CommonAjaxMethod(virtualPath + 'DigitalLibrary/Dashboard',  null, 'GET', function (response)
    {

        var d = response.data.data.Table;                                  
        $('#divUserTotalDoc').text(d[0].UserNoOfDocumentUploaded);
        $('#divUserTotalApproved').text(d[0].UserNoOfDocumentApproved);
        $('#divUserTotalPending').text(d[0].UserNoOfDocumentPending);
        $('#divUserTotalResub').text(d[0].UserNoOfDocumentResubmitted);
        $('#divUserTotalMove').text(d[0].UserNoOfDocumentRejected);
        $('#divUserAprTotalDoc').text(d[0].UserAprNoOfDocument);
        $('#divUserAprTotalApproved').text(d[0].UserAprNoOfDocumentApproved);
        $('#divUserAprTotalPending').text(d[0].UserAprNoOfDocumentPending);
        $('#divUserAprTotalNotAprroved').text(d[0].UserAprNoOfDocumentNotApproved);
        $('#divManagerTotalDoc').text(d[0].ManagerNoOfDocumentUploaded);
        $('#divManagerTotalApprved').text(d[0].ManagerNoOfDocumentApproved);
        $('#divManagerTotalPending').text(d[0].ManagerNoOfDocumentPending);
        $('#divManagerTotalRessubmit').text(d[0].ManagerNoOfDocumentResubmitted);
        $('#divManagerTotalMove').text(d[0].ManagerNoOfDocumentRejected);
        $('#divManagerAprTotalDoc').text(d[0].ManagerAprNoOfDocument);
        $('#divManagerAprTotalApproved').text(d[0].ManagerAprNoOfDocumentApproved);
        $('#divManagerAprTotalPending').text(d[0].ManagerAprNoOfDocumentPending);
        $('#divManagerAprTotalNotAprroved').text(d[0].ManagerAprNoOfDocumentNotApproved);


    });
}
function FillSubCategory(ctrl) {


    LoadMasterDropdown('ddlSubCategory',
        {
            ParentId: $(ctrl).val(), masterTableType: DropDownTypeEnum.Category, isMasterTableType: false, isManualTable: false,
            manualTable: 0,
            manualTableId: 0
        }, 'Select', false);



}

function FillFilterOption()
{
    var FilterSearchArray = [];
    if ($('#txtSearchBox').val() == "" && $('#ddlCategory').val() == "Choose Your Category" && ($('#ddlSubCategory').val() == "Select" || $('#ddlSubCategory').val() == null)) {

        document.getElementById('hReturnMessage').innerHTML = "One filter option must be selected."
        $('#btnShowModel').click();
        return false;
    }
    else
    {
        var obj =
        {
            SearchArea: $('#txtSearchBox').val(),
            CategoryId: $('#ddlCategory').val(),
            SubcategoryId: $('#ddlSubCategory').val(),
            ProjectCode: $('#ProjectCode').val()
        }
        FilterSearchArray.push(obj);
       // $('#btnSearchRedirect').click();    
        localStorage.setItem('SearchArray', JSON.stringify(FilterSearchArray) );
        return true;
    }
}