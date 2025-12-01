var PageIndex = 0;
var TotalNumberOfPages = 0;
var FilterType =
{

    Project: 1,
    Thematic: 2,
    MasterLocation: 3,
    Donar: 4,
    TagWithThematic: 5,   
    Year:6
    
};
var subcatId = 0;
$(document).ready(function ()
{
    
    LoadMasterDropdown('shareEmployee', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.Employee,
        manualTableId: 0
    }, 'Select', false);
    $('[data-toggle="tooltip"]').tooltip();
  //  var FilterSearchArray = [];
     PageIndex = 0;
     TotalNumberOfPages = 0;    

     
   // FilterSearchArray = JSON.parse(localStorage.getItem("SearchArray"));
    var searchData = "";
    var catId = "Choose Your Category";
    subcatId = "Select";
    var projCode = "Select";
    //if (FilterSearchArray.length > 0)
    //{
    //    searchData = FilterSearchArray[0].SearchArea;
    //    catId = FilterSearchArray[0].CategoryId;
    //    subcatId = FilterSearchArray[0].SubcategoryId;
    //    projCode = FilterSearchArray[0].ProjectCode;
    //}

    var obj = {
        ParentId: 0,
        masterTableType: DropDownTypeEnum.Category,
        isMasterTableType: false,
        isManualTable: false,
        manualTable: 0,
        manualTableId: 0
    }

    $('#txtSearchBox').val(searchData);

    if (catId != 'Choose Your Category')
    {
        LoadMasterDropdown('ddlCategory', obj, 'Choose Your Category', false, catId);
       

        if (subcatId != 'Select')
        {
            LoadMasterDropdown('ddlSubCategory',
                {
                    ParentId: catId, masterTableType: DropDownTypeEnum.Category, isMasterTableType: false, isManualTable: false,
                    manualTable: 0,
                    manualTableId: 0
                }, 'Select', false, subcatId);
        }
        else
        {
            LoadMasterDropdown('ddlSubCategory',
                {
                    ParentId: catId, masterTableType: DropDownTypeEnum.Category, isMasterTableType: false, isManualTable: false,
                    manualTable: 0,
                    manualTableId: 0
                }, 'Select', false);
        }
    }
    else
        LoadMasterDropdown('ddlCategory', obj, 'Choose Your Category', false);
    var objProject = {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.Project,
        manualTableId: 0
    }
    if (projCode != 'Select') {
        var pObj = {
            Id: projCode
        }
        ProjectArray.push(pObj);
        LoadUlLiProject('ulProjectCode', objProject, FilterType.Project, projCode);
    }
    else {
        LoadUlLiProject('ulProjectCode', objProject, FilterType.Project);
    }
   

  

    var objulThematic = {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.MasterThematicArea,
        manualTableId: 0
    }   

    LoadUlLi('ulThematic', objulThematic, FilterType.Thematic);

    var ulPlace = {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.MasterLocation,
        manualTableId: 0
    }   
    LoadUlLi('ulPlace', ulPlace, FilterType.MasterLocation);

    var ulDonar = {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.Donar,
        manualTableId: 0
    }
    LoadUlLi('ulDonar', ulDonar, FilterType.Donar);


    var ulYear = {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.Year,
        manualTableId: 0
    }
    LoadUlLi('ulYear', ulYear, FilterType.Year);

    var ulTagwithThematic = {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.TagWithThematic,
        manualTableId: 0
    }
    LoadUlLi('ulTagwithThematic', ulTagwithThematic, FilterType.TagWithThematic);
    BindContent(false);
    
});

function FillSubCategory(ctrl) {


    LoadMasterDropdown('ddlSubCategory',
        {
            ParentId: $(ctrl).val(), masterTableType: DropDownTypeEnum.Category, isMasterTableType: false, isManualTable: false,
            manualTable: 0,
            manualTableId: 0
        }, 'Select', false);



}
function SaveSharedContent()
{   
    var obj =
    {
        Id: $('#hdnContentId').val(),
        SharedUserId: ($('#shareEmployee').val()).join()
         
    }
    CommonAjaxMethod(virtualPath + 'DigitalLibrary/SaveSharedContent', obj, 'POST', function (response)
    {
        $('#shareEmployee').val([0]).trigger('change');
    });

}

function GoPrev()
{
    if (PageIndex > 0)//Check if the pageIndex is not the first page.
    {
        PageIndex -= 1;//Decrement the pageindex by 1;
        BindContent(true);
    }
    else {
        document.getElementById('hReturnMessage').innerHTML = "You are on the first page."
        $('#btnShowModel').click();
    }
       
}
function GoPage(pNo)
{
    PageIndex = (pNo - 1);
    BindContent(true);
   
}
function GoNext()
{
    if ((PageIndex + 1) < TotalNumberOfPages)//Check if the page index is not the last page.
    {
        PageIndex += 1;//Increment the pageindex by 1;
        BindContent(true);
    }
    else {
        document.getElementById('hReturnMessage').innerHTML = "You are on the last page."
        $('#btnShowModel').click();
    }
        

  
}
function SearchData()
{
    BindContent(false);
}
var ProjectArray = [];
var TagArray = [];
var ThemeticArray = [];
var YearArray = [];
var DonarArray = [];
var LocationArray = [];

function AddFilters(id, type)
{    
    if (type == FilterType.Year) {
        var dataYear = YearArray.filter(function (itemParent) { return (itemParent.Id == id); });
        if (dataYear.length > 0)
        {
            YearArray = YearArray.filter(function (itemParent) { return (itemParent.Id != id); });

        }
        else {
            var dataTagArrayObj = {
                Id: id
            }
            YearArray.push(dataTagArrayObj);
        }
    }

    if (type == FilterType.TagWithThematic) {
        var dataTagArray = TagArray.filter(function (itemParent) { return (itemParent.Id == id); });
        if (dataTagArray.length > 0) {
            TagArray = TagArray.filter(function (itemParent) { return (itemParent.Id != id); });
        }
        else {
            var dataTagArrayObj = {
                Id: id
            }
            TagArray.push(dataTagArrayObj);
        }
    }

    if (type == FilterType.Donar) {
        var dataDonar = DonarArray.filter(function (itemParent) { return (itemParent.Id == id); });
        if (dataDonar.length > 0) {
            DonarArray = DonarArray.filter(function (itemParent) { return (itemParent.Id != id); });
        }
        else {
            var donarObj = {
                Id: id
            }
            DonarArray.push(donarObj);
        }
    }

    if (type == FilterType.MasterLocation) {
        var dataloObj = LocationArray.filter(function (itemParent) { return (itemParent.Id == id); });
        if (dataloObj.length > 0) {
            LocationArray = LocationArray.filter(function (itemParent) { return (itemParent.Id != id); });
        }
        else {
            var loObj = {
                Id: id
            }
            LocationArray.push(loObj);
        }
    }

    if (type == FilterType.Thematic) {
        var dataThemeticArray = ThemeticArray.filter(function (itemParent) { return (itemParent.Id == id); });
        if (dataThemeticArray.length > 0) {
            ThemeticArray = ThemeticArray.filter(function (itemParent) { return (itemParent.Id != id); });
        }
        else {
            var thObj = {
                Id: id
            }
            ThemeticArray.push(thObj);
        }
    }
    if (type == FilterType.Project)
    {
        var dataProject = ProjectArray.filter(function (itemParent) { return (itemParent.Id == id); });
        if (dataProject.length > 0) {
            ProjectArray = ProjectArray.filter(function (itemParent) { return (itemParent.Id != id); });
        }
        else {
            var pObj = {
                 Id: id
            }
            ProjectArray.push(pObj);
        }
    }
   
     BindContent(false);
}

function ShowDownload(docId)
{
    CommonAjaxMethod(virtualPath + 'DigitalLibrary/GetAtatchemntById', { Id: docId }, 'GET', function (response)
    {
        var data = response.data.data.Table;
        document.getElementById("hProjectName").innerHTML = data[0].proj_name;        
        var tblDocument = data.filter(function (itemParent) { return (itemParent.AttachmentType == "Document"); });
        var tblPhoto = data.filter(function (itemParent) { return (itemParent.AttachmentType == "Photo"); });
        var tblVideo = data.filter(function (itemParent) { return (itemParent.AttachmentType == "Video"); });        

        $('#tblDocument').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": tblDocument,

            "columns": [
                {
                    "orderable": false,
                    data: null, render: function (data, type, row)
                    {
                        return "<i class='fas fa-file-pdf mtr-y-clr fn-md '></i>" + row.FileName;
                    }
                },
                { "data": "emp_name" },               
                { "data": "FileSize" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {
                        var url = baseURL + row.FileUrl;
                        var strReturn = '<button   onclick="DownloadFile(' + row.ID +')"  target="_blank" class="green-clr"><i class="fas fa-download"></i>Download</button>';
                        return strReturn;
                    }
                }


            ]
        });

        $('#tblPhoto').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": tblPhoto,

            "columns": [
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {
                        return "<i class='fas fa-image mtr-y-clr fn-md '></i>" + row.FileName;
                    }
                },
                { "data": "emp_name" },
                { "data": "FileSize" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {
                        var url = baseURL + row.FileUrl;
                        var strReturn = '<button   onclick="DownloadFile(' + row.ID +')"  target="_blank" class="green-clr"><i class="fas fa-download"></i>Download</button>';
                        return strReturn;
                    }
                }


            ]
        });

        $('#tblVideo').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": tblVideo,

            "columns": [
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {
                        return "<i class='fas fa-video mtr-y-clr fn-md '></i>" + row.FileName;
                    }
                },
                { "data": "emp_name" },
                { "data": "FileSize" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {

                        var strReturn = '<a href="' + row.FileUrl +'" target="_blank" class="green-clr"><i class="fas fa-download"></i>Download</a><span class="divline">|</span><a href="#"><i class="fas fa-eye"></i>View</a>';
                        return strReturn;
                    }
                }


            ]
        });

        $('#btnOpenPopup').click();
    });
}
function DownloadFile(id)
{
    $.ajax({
        url: virtualPath + 'DigitalLibrary/DownloadFile?Id='+id,
        type: "GET",   
        contentType: 'application/json',
        success: function (response)
        {
         
            var obj = JSON.parse(response);           
            var d = obj.data.Table[0];
            var stSplitFileName = d.ActualFileName.split(".");

            var link = document.createElement("a");
            link.download = stSplitFileName[0];
            link.href = d.FileUrl;
            link.click();
        }
        ,
        error: function (error) {       
            FailToaster(error);
           

            isSuccess = false;
        }

    });
}
function RequestForDownload(id, ManagerID)
{
    var RequestApprovalArray = [];
    var obj =
      {
        Id: id,
        ApprovarId: ManagerID,
        RequesterId: loggedinUserid,
        IsRequester: true,
        IsApproved: 0,
        IPAddress: $('#hdnIP').val(),
        UserGrade: UserGrade

    }
    RequestApprovalArray.push(obj);

    CommonAjaxMethod(virtualPath + 'DigitalLibrary/DLContentApproval', RequestApprovalArray, 'POST', function (response)
    {        
        BindContent(true)
    });
}
function ShareContent(cId) {
    $('#hdnContentId').val(cId);
    $('#btnSharePopup').click();
}
function BindContent(isPageIndexChanged)
{
    var SearchObject =
    {
        SearchText: $('#txtSearchBox').val(),
        Category: $('#ddlCategory').val(),
        Subcategory: $('#ddlSubCategory').val(),
        PageSize: 10,
        PageNumber: PageIndex + 1,
        ProjectList: ProjectArray,
        TagSearchList: TagArray,
        YearSearchList: YearArray,
        ThematicSearchList: ThemeticArray,
        DonarSearchList: DonarArray,
        LocationSearchList: LocationArray
    }
    CommonAjaxMethod(virtualPath + 'DigitalLibrary/GetSharedContent', SearchObject, 'POST', function (response) {

        var dt = response.data.data.Table;
        var RoleId = 0;
        var crtiTag = "";
        var totalRows = 0;
        var pageSize = 0.0;
        if (dt.length > 0) {
            totalRows = dt[0].TotalRow;//  dt.length
            RoleId = dt[0].RoleId;
        }
        if (!isPageIndexChanged)//If pageindex is not changed and search is performed then reset the page index to first page.
            PageIndex = 0;

        pageSize = totalRows / (10 * 1.0);
        TotalNumberOfPages = parseInt(pageSize);
        TotalNumberOfPages = pageSize > TotalNumberOfPages ? (TotalNumberOfPages + 1) : TotalNumberOfPages;//Set the number of pages.
        if (TotalNumberOfPages == 0)
            TotalNumberOfPages = 1;//Since if a record exists then page count needs to be one.
        document.getElementById("DivPageInfo").innerHTML = "";
        if (dt.length > 0) {
            document.getElementById("ulPaging").innerHTML = "";


            var paginfor = 'Showing 1 – ' + TotalNumberOfPages + ' of ' + totalRows + ' results ';

            if (jQuery.trim($('#txtSearchBox').val()) != "") {
                paginfor = paginfor + ' for "' + $('#txtSearchBox').val() + '"';
            }
            $('#DivPageInfo').append(paginfor);


            var paginLI = '<li><a onclick="GoPrev()" href="#">Previous</a></li>';
            $('#ulPaging').append(paginLI);
            for (var i = 0; i < TotalNumberOfPages; i++) {
                paginLI = '<li><a onclick="GoPage(' + (i + 1) + ')" href="#">' + (i + 1) + '</a></li>';
                $('#ulPaging').append(paginLI);
            }
            paginLI = '<li><a  onclick="GoNext()" href="#">Next</a></li>';
            $('#ulPaging').append(paginLI);
        }
        document.getElementById("MainUL").innerHTML = "";



        for (var i = 0; i < dt.length; i++) {
            var downloadprintshare = "";


            if (dt[i].IsApproved == 1) {
                downloadprintshare = '<i class="fas fa-share"></i>Shared by ' + dt[i].SharedBy + '<span class="divline">|</span><a href="#" onclick="ShowDownload(' + dt[i].ID + ')" data-toggle="modal" data-target="#dwn"><i class="fas fa-eye"></i>View</a><span class="divline">|</span><a href="#" onclick="ShareContent(' + dt[i].ID + ')" ><i class="fas fa-share"></i>Share</a>';
            }
            else if (dt[i].IsApproved == 3) {
                downloadprintshare = '<i class="fas fa-share"></i>Shared by ' + dt[i].SharedBy + '<span class="divline">|</span><a href="#"  onclick="RequestForDownload(' + dt[i].ID + ',' + dt[i].ManagerID + ')"  class="red-clr"><i class="fas fa-download"></i>Request for Download</a>';
            }
            else if (dt[i].IsApproved == 0) {
                downloadprintshare = ' <i class="fas fa-share"></i>Shared by ' + dt[i].SharedBy + '<span class="divline">|</span><i class="fas fa-clock"></i>Request pending for Download';

            }
            else if (dt[i].IsApproved == 2) {
                downloadprintshare = '<i class="fas fa-share"></i>Shared by ' + dt[i].SharedBy + '<span class="divline">|</span><i class="fas fa-clock"></i>Rejected';

            }
            if (UserGrade == 'A') {
                downloadprintshare = '<i class="fas fa-share"></i>Shared by ' + dt[i].SharedBy + '<span class="divline">|</span><a href="#" onclick="ShowDownload(' + dt[i].ID + ')" data-toggle="modal" data-target="#dwn"><i class="fas fa-eye"></i>View</a><span class="divline">|</span><a href="#" onclick="ShareContent(' + dt[i].ID + ')" ><i class="fas fa-share"></i>Share</a>';
            }
            crtiTag = "";
            if (dt[i].Document_Category == 'Critical and confidential') {
                crtiTag = '<table class="table ccbg">' +
                    '<tbody>' +
                    '<tr>' +
                    '<td><strong>' + dt[i].proj_name + '</strong></td>' +
                    '<td class="text-right"><img src="../assets/design/images/critical-and-confidential-icon.png" data-toggle="tooltip" data-original-title="Critical and Confidential" class="dlimpicon" alt="critical and confidential icon"></td>' +
                    '</tr>' +
                    '</tbody>' +
                    '</table>';
            }
            else if (dt[i].Document_Category == 'Critical and not confidential') {
                crtiTag = '<table class="table cncbg">' +
                    '<tbody>' +
                    '<tr>' +
                    '<td><strong>' + dt[i].proj_name + '</strong></td>' +
                    '<td class="text-right"><img src="../assets/design/images/critical-and-not-confidential-icon.png" data-toggle="tooltip"   data-original-title="Critical and Not Confidential" class="dlimpicon" alt="Critical and Not Confidential icon"></td>' +
                    '</tr>' +
                    '</tbody>' +
                    '</table>';
            }
            else if (dt[i].Document_Category == 'Not Critical and not confidential') {
                downloadprintshare = '<i class="fas fa-share"></i>Shared by ' + dt[i].SharedBy + '<span class="divline">|</span>  <a href="#" onclick="ShowDownload(' + dt[i].ID + ')" data-toggle="modal" data-target="#dwn"><i class="fas fa-eye"></i>View</a><span class="divline">|</span><a href="#" onclick="ShareContent(' + dt[i].ID + ')" ><i class="fas fa-share"></i>Share</a>';

                crtiTag = '<table class="table ncncbg">' +
                    '<tbody>' +
                    '<tr>' +
                    '<td><strong>' + dt[i].proj_name + '</strong></td>' +
                    '<td class="text-right"><img src="../assets/design/images/not-critical-and-not-confidential-icon.png" data-toggle="tooltip" data-original-title="Not Critical and Not Confidential" class="dlimpicon" alt="Not Critical and Not Confidential icon"></td>' +
                    '</tr>' +
                    '</tbody>' +
                    '</table>';

            }



            var li =
                '<li class="rcshrlist">' + crtiTag +

                '<div class="rcshrcontent">' +
                '<strong>' + dt[i].Title + '</strong>' +
                '<span class="d-block">2022</span>' +
                '<p>' + dt[i].Abstract_Summary + '</p>' +
                '<ul class="list-unstyled rcshrcontentlist">' +
                '<li><strong>Category</strong><br>' + dt[i].Category + '</li>' +
                '<li><strong>Sub Category</strong><br>' + dt[i].SubCategory + '</li>' +
                '<li><strong>Location</strong><br>' + dt[i].Places + '</li>' +
                '<li><strong>Donor</strong><br>' + dt[i].donor_name + '</li>' +
                '<li><strong>Thematic Area</strong><br>' + dt[i].thematicarea_code + '</li>' +

                '<li><strong>Author</strong><br>' + dt[i].AuthName + ' <strong class="mtr-o-clr" data-toggle="tooltip" data-original-title="' + dt[i].AuthName + '">...</strong></li>' +

                '</ul>' +
                '<div class="text-right fn-bold">' + downloadprintshare +
                '</div>' +
                '</div>' +
                '</li>';
            $('#MainUL').append(li);
            $('[data-toggle="tooltip"]').tooltip();
        }

    }, true);
}


