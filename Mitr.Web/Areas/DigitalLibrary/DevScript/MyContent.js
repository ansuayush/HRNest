$(document).ready(function ()
{
    if (TabId == '1' || TabId == '3') {
        $('#tab-C').click();
    }
    if (TabId == '2') {
        $('#tab-A').click();
    }
    if (TabId == '4') {
        $('#tab-B').click();
    }
    if (TabId == '5') {
        $('#tab-D').click();
    }
    BindContent();   
    
   // $('#emUploadedBy').val(loggedinUserName);
});
function FillAuther(strArray)
{
    arrayOfauther = [];
    arrayOfautherId = [];
    arrayOfautherId = strArray.split(',');

    CommonAjaxMethod(virtualPath + 'CommonMethod/GetDropdown/',
        {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.Employee,
        manualTableId: 0
        }, 'GET', function (response)
       {
            var autherData = response.data.data.Table;
            for (var i = 0; i < arrayOfautherId.length; i++)
            {              
                var data = autherData.filter(function (itemParent) { return (itemParent.ID == arrayOfautherId[i]); });
                if (data.length > 0) {
                    var authObj = {
                        Id: data[0].ID,
                        Name: data[0].ValueName
                    }
                    arrayOfauther.push(authObj);
                }
            }

            var html = "";
            $("#dvAutherList").html('');
            html += "<ul class='list-style-type:none list-unstyled dltgdgn'>";
            for (var i = 0; i < arrayOfauther.length; i++) {
                html += "<li onclick='RemoveAuther(" + arrayOfauther[i].Id + ")'><span class='select2-selection__choice__remove' role='presentation'>×</span>" + arrayOfauther[i].Name + "</li>";
            }
            html += "</ul>";

            $("#dvAutherList").append(html);
            
       
    });
}
var arrayOfauther = [];
var arrayOfautherId = [];

function SetAuther(ctrl)
{
    var id = $(ctrl).val();
    if (id == null || id == 'Select') {
        var nothing = "";
    }
    else {
        let checkIfExists = arrayOfauther.filter((arrayOfauther) => arrayOfauther.Id === id);

        if (checkIfExists.length == 0) {
            var authObj = {
                Id: id,
                Name: ctrl.options[ctrl.selectedIndex].text
            }
            arrayOfauther.push(authObj);
            arrayOfautherId.push(id);
        }
    }
    var html = "";
    $("#dvAutherList").html('');
    html += "<ul class='list-style-type:none list-unstyled dltgdgn'>";
    for (var i = 0; i < arrayOfauther.length; i++) {
        html += "<li onclick='RemoveAuther(" + arrayOfauther[i].Id + ")'><span class='select2-selection__choice__remove' role='presentation'>×</span>" + arrayOfauther[i].Name + "</li>";
    }
    html += "</ul>";

    $("#dvAutherList").append(html);


}
function RemoveAuther(id) {


    arrayOfauther = arrayOfauther.filter((arrayOfauther) => arrayOfauther.Id != id);

    arrayOfautherId = arrayOfautherId.filter((number) => number != id);
    var html = "";
    $("#dvAutherList").html('');
    html += "<ul class='list-style-type:none list-unstyled dltgdgn'>";
    for (var i = 0; i < arrayOfauther.length; i++) {
        html += "<li><span onclick='RemoveAuther(" + arrayOfauther[i].Id + ")' class='select2-selection__choice__remove' role='presentation'>×</span>" + arrayOfauther[i].Name + "</li>";
    }
    html += "</ul>";

    $("#dvAutherList").append(html);
}
function ShowContent(id)
{
    $("#submitView").hide();
    $("#submitAdd").hide(); 
    $("#DivDocumentListUpdate").hide();    
    $("#DivDocumentListView").show(); 
    CommonAjaxMethod(virtualPath + 'DigitalLibrary/GetContentView', { id: id }, 'GET', function (response)
    {
       
        var dataContent = response.data.data.Table;
        var dataAttachement = response.data.data.Table1;
        $("#hdnContentId").val(id);
        FillAuther(dataContent[0].Author_CoordinatorId);
        commonArray = dataAttachement;
        BindDocumentGrid(commonArray,true);
        FillSubCategory(dataContent);
        $('#btnOpenEditPopup').click();
    });
    return false;
   
}
function EditContent(id)
{
    $("#submitView").show();
    $("#submitAdd").show();
    $("#DivDocumentListUpdate").show();   
    $("#DivDocumentListView").hide(); 
    
    CommonAjaxMethod(virtualPath + 'DigitalLibrary/GetContentView', { id: id }, 'GET', function (response)
    {
        
        var dataContent = response.data.data.Table;     
        
        var dataAttachement = response.data.data.Table1;   
        $("#hdnContentId").val(id);
        if (dataContent[0].Status == 'Draft') {
          
            $('#btnDraft').show();
        }
        else {
            $('#btnDraft').hide();
        }
        FillAuther(dataContent[0].Author_CoordinatorId);
        commonArray = dataAttachement;
        BindDocumentGrid(commonArray,false);
        FillSubCategory(dataContent);
        $('#btnOpenEditPopup').click();
        
        
    });
    return false;
}
function AttachmentTypeChange(ctr) {
    HideErrorMessage(ctr);
    if ($("#emAttachementType").val() == "Video") {
        $('#dvVedioFileName').show();
        $('#dvFileUpload').hide();

    }
    else {
        $('#dvVedioFileName').hide();
        $('#dvFileUpload').show();
    }
}
function UploadDocument(mandateClass)
{
    if (subCategoryNameToUpload == "")
    {
        document.getElementById('hReturnMessage').innerHTML = "Please select subcategorty to upload!";
        $('#btnShowModel').click();      
    }
    else {
        if (checkValidationOnSubmit(mandateClass) == true) {
           
            var categoryText = $('#pdCategory').val();

            var AttachmentTypeObject = document.getElementById("emAttachementType");
            var AttachmentTypeText = AttachmentTypeObject.options[AttachmentTypeObject.selectedIndex].text;

            var fileUpload = $("#emFileUpload").get(0);

            var files = fileUpload.files;
            if ($("#emAttachementType").val() == "Video")
            {
                if ($("#VedioFileName").val() == "")
                {
                    isvalid = false;
                    document.getElementById('hReturnMessage').innerHTML = "Please enter the video file details!";
                    $('#btnShowModel').click();

                }
                else {
                    IdPd = IdPd - 1;
                    var loop = IdPd;

                    var objarrayinner =
                    {
                        Id: loop,
                        ActualFileName: $("#emFileName").val(),
                        NewFileName: $("#emFileName").val(),
                        FileName: $("#emFileName").val(),
                       // UploadedBy: $("#emUploadedBy").val(),
                        AttachmentType: $("#emAttachementType").val(),
                        FileUrl: $("#VedioFileName").val(),
                        FileSize: "0"

                    }

                    commonArray.push(objarrayinner);

                    $('#emDocumentListUpdate').DataTable({
                        "processing": true, // for show progress bar           
                        "destroy": true,
                        "data": commonArray,
                        "paging": false,
                        "info": false,
                        "columns": [
                            {
                                "orderable": false,
                                data: null, render: function (data, type, row) {
                                    if (row.AttachmentType == "Video")
                                        return "<i class='fas fa-video mtr-y-clr fn-md '></i>" + row.FileName;
                                    else if (row.AttachmentType == "Photo")
                                        return "<i class='fas fa-image mtr-y-clr fn-md '></i>" + row.FileName;
                                    else
                                        return "<i class='fas fa-file-pdf mtr-y-clr fn-md '></i>" + row.FileName;
                                }
                            },
                           // { "data": "UploadedBy" },
                            { "data": "AttachmentType" },
                            { "data": "FileSize" },
                            {
                                "orderable": false,
                                data: null, render: function (data, type, row) {

                                    var strReturn = "<a title='Click to Remove' data-toggle='tooltip' data-original-title='Click to DeActivate' class='AIsActive'  onclick='deleteRows(" + row.Id + ")' ><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'  ></i> </a>";


                                    return strReturn;
                                }
                            }


                        ]
                    });

                    $("#emFileUpload").val('');
                    $("#emFileUpload").val(null);
                    $("#emAttachementType").val('0');
                    $('#select2-emAttachementType-container').text('Select');
                    $("#emFileName").val('');
                    $("#VedioFileName").val('');
                }
            }
            else if (files.length > 0) {

                // Create FormData object
                var fileData = new FormData();

                // Looping over all files and add it to FormData object
                for (var i = 0; i < files.length; i++) {
                    fileData.append(files[i].name, files[i]);
                }

                $.ajax({
                    url: virtualPath + 'CommonMethod/UploadDocument?categoryText=' + categoryText + '&type=' + AttachmentTypeText + '&SubCate=' + subCategoryNameToUpload,
                    type: "POST",
                    contentType: false, // Not to set any content header
                    processData: false, // Not to process data
                    data: fileData,
                    success: function (response) {
                        var result = JSON.parse(response);

                        if (result.ErrorMessage == "") {

                            IdPd = IdPd - 1;
                            var loop = IdPd;

                            var objarrayinner =
                            {
                                Id: loop,
                                ActualFileName: result.FileModel.ActualFileName,
                                NewFileName: result.FileModel.NewFileName,
                                FileName: $("#emFileName").val(),
                                //UploadedBy: $("#emUploadedBy").val(),
                                AttachmentType: $("#emAttachementType").val(),
                                FileSize: result.FileModel.FileSize,
                                FileUrl: result.FileModel.FileUrl

                            }

                            commonArray.push(objarrayinner);


                            $('#emDocumentListUpdate').DataTable({
                                "processing": true, // for show progress bar           
                                "destroy": true,
                                "paging": false,
                                "info": false,
                                "data": commonArray,

                                "columns": [


                                    {
                                        "orderable": false,
                                        data: null, render: function (data, type, row) {
                                            if (row.AttachmentType == "Video")
                                                return "<i class='fas fa-video mtr-y-clr fn-md '></i>" + row.FileName;
                                            else if (row.AttachmentType == "Photo")
                                                return "<i class='fas fa-image mtr-y-clr fn-md '></i>" + row.FileName;
                                            else
                                                return "<i class='fas fa-file-pdf mtr-y-clr fn-md '></i>" + row.FileName;
                                        }
                                    },
                                   // { "data": "UploadedBy" },
                                    { "data": "AttachmentType" },
                                    { "data": "FileSize" },
                                    {
                                        "orderable": false,
                                        data: null, render: function (data, type, row) {

                                            var strReturn = "<a title='Click to Remove' data-toggle='tooltip' data-original-title='Click to DeActivate' class='AIsActive'  onclick='deleteRows(" + row.Id + ")' ><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'  ></i> </a>";


                                            return strReturn;
                                        }
                                    }


                                ]
                            });

                            $("#emFileUpload").val('');
                            $("#emFileUpload").val(null);
                            $("#emAttachementType").val('0');
                            $('#select2-emAttachementType-container').text('Select');
                            $("#emFileName").val('');
                            $("#VedioFileName").val('');

                        }
                        else {

                            document.getElementById('hReturnMessage').innerHTML = result.ErrorMessage;
                            $('#btnShowModel').click();
                          
                        }
                    }
                    ,
                    error: function (error) {
                        FailToaster(error);
                        //document.getElementById('hReturnMessage').innerHTML = error;
                        //$('#btnShowModel').click();
                        isSuccess = false;
                       
                    }

                });
            }
            else {

                document.getElementById('hReturnMessage').innerHTML = "Please select file to attach!";
                $('#btnShowModel').click();
               
            }

        }
    }

}
var subCategoryNameToUpload = "";
function FillProjectDetails(ctrl) {
    var pid = $(ctrl).val();
    HideErrorMessage(ctrl);
    CommonAjaxMethod(virtualPath + 'DigitalLibrary/GetProjectDetails', { id: pid }, 'GET', function (response)
    {
        var dt = response.data.data.Table;
        $('#pdProjectName').val(dt[0].ProjectName);
        $('#pdThematicArea').val(dt[0].thematicarea_code);
        $('#pdFundedBy').val(dt[0].FundedBy); 
        
        LoadMasterDropdown('pdTag',
            {
                ParentId: 0,
                masterTableType: 0,
                isMasterTableType: false,
                isManualTable: true,
                manualTable: ManaulTableEnum.TagWithThemetic,
                manualTableId: dt[0].ThemArea_ID
            }, ' ', false);
    });

}
function FillSubCategory(data)
{
    $('#hdnApprovar').val(data[0].ApprovarAuth);
    var categoryText = data[0].Category;
    var Categoryid = data[0].CategoryId;
    var tagName = data[0].TagName;
    var SubCategoryid = data[0].Sub_CategoryId;
    $('#hdnCategoryId').val(Categoryid);
    $('#hdnTagName').val(tagName);
    $('#pdCategory').val(categoryText);
    subCategoryNameToUpload = data[0].SubCategory
    const words = ['pd', 'pld', 'cnts', 'eb', 'tm', 'pbn'];
    const resultEqual = words.filter(function (itemParent) { return (itemParent == tagName); });
    const resultNotEqual = words.filter(function (itemParent) { return (itemParent != tagName); });

    CommonAjaxMethod(virtualPath + 'DigitalLibrary/GetMatrix', { category: tagName }, 'GET', function (response) {


        var d = response.data.data.Table;
        var $ele2 = $('#pdDocument');
        $ele2.empty();

        $ele2.append($('<option/>').val('0').text('Select'));
        $.each(d, function (ii, vall) {
            $ele2.append($('<option/>').val(vall.DocCatCode).text(vall.DocCatCode));
        })
        $('#pdDocument').val(data[0].Document_Category).trigger('change'); 
    });

    if (tagName == 'Choose Your Category')
    {
        $('#FileAttachmentContent').hide();
        $('#mainContentDiv').hide();

    }
    else
    {      
        $('#FileAttachmentContent').show();
        $('#mainContentDiv').show();
    }

    for (var i = 0; i < resultEqual.length; i++) {
        $('.' + resultEqual[i]).show();
    }

    for (var k = 0; k < resultNotEqual.length; k++) {
        $('.' + resultNotEqual[k]).hide();
    }   

     
    $('#pdReqNo').val(data[0].Req_No);   
    $('#pdReqDate').val(ChangeDateFormatToddMMYYY(data[0].Req_Date)); 
    $('#pdReqBy').val(data[0].ReqBy);   
    
    LoadMasterDropdown('pdSubCategory',
        {
            ParentId: Categoryid, masterTableType: DropDownTypeEnum.Category, isMasterTableType: false, isManualTable: false,
            manualTable: 0,
            manualTableId: 0
        }, 'Select', false);
    if (SubCategoryid!= null)
    {
        $('#pdSubCategory').val(SubCategoryid).trigger('change');
    }
  //  $('#pdSubCategory').val(data[0].SubCategory);

    LoadMasterDropdown('pdProjectCode',
        {
            ParentId: 0,
            masterTableType: 0,
            isMasterTableType: false,
            isManualTable: true,
            manualTable: ManaulTableEnum.Project,
            manualTableId: 0
        }, 'Select', false);

    var pID = parseInt(data[0].Project_CodeId);
    if (data[0].Project_CodeId != '0') {
        $('#pdProjectCode').val(pID).trigger('change');
    }

    CommonAjaxMethod(virtualPath + 'DigitalLibrary/GetProjectDetails', { id: data[0].Project_CodeId }, 'GET', function (response)
    {
        var dt = response.data.data.Table;
        if (dt.length > 0) {
            $('#pdProjectName').val(dt[0].ProjectName);
            $('#pdThematicArea').val(dt[0].thematicarea_code);
            $('#pdFundedBy').val(dt[0].FundedBy);

            LoadMasterDropdown('pdTag',
                {
                    ParentId: 0,
                    masterTableType: 0,
                    isMasterTableType: false,
                    isManualTable: true,
                    manualTable: ManaulTableEnum.TagWithThemetic,
                    manualTableId: dt[0].ThemArea_ID
                }, ' ', false);
            if (data[0].Tag_Id.length > 0) {
                let pdTagArray = data[0].Tag_Id.split(',');

                $("#pdTag").select2({
                    multiple: true,
                });
                $('#pdTag').val(pdTagArray).trigger('change');
            }
        }

    });


    LoadMasterDropdown('pdPlaceOfOrigin',
        {
            ParentId: 0,
            masterTableType: 0,
            isMasterTableType: false,
            isManualTable: true,
            manualTable: ManaulTableEnum.MasterLocation,
            manualTableId: 0
        }, 'Select', false);
    if (data[0].PlaceId != null) {
        if (data[0].PlaceId.length > 0) {
            let pdPlaceOfOriginArray = data[0].PlaceId.split(',');

            $("#pdPlaceOfOrigin").select2({
                multiple: true,
            });
            $('#pdPlaceOfOrigin').val(pdPlaceOfOriginArray).trigger('change');
        }
    }

 
    LoadMasterDropdown('pdAuther', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.Employee,
        manualTableId: 0
    }, 'Select', false);

    //let pdpdAuther = data[0].Author_CoordinatorId.split(',');

    //$("#pdAuther").select2({
    //    multiple: true,
    //});
    $('#pdAuther').val('Select').trigger('change'); 


    LoadMasterDropdown('pdProjectLead', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.Employee,
        manualTableId: 0
    }, 'Select', false);
     
        $('#pdProjectLead').val(data[0].ProjectLead).trigger('change'); 



    LoadMasterDropdown('ebProjectLead', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.Employee,
        manualTableId: 0
    }, 'Select', false);

    $('#ebProjectLead').val(data[0].ProjectLead).trigger('change'); 

     $('#pdReportNo').val(data[0].Report_No);
     $('#pdTitle').val(data[0].Title);
     $('#pdSubTitle').val(data[0].Sub_Title);
     $('#pdSummary').val(data[0].Abstract_Summary);
     $('#pdRemarks').val(data[0].Remark);
    
    $('#pldProposalNo').val(data[0].Proposal_No);
    if (data[0].Accepted != null) {
        $('#pldAccepted').val(data[0].Accepted).trigger('change');
    }
     $('#tmCopyRights').val(data[0].Copyright);
     $('#cntsContactNo').val(data[0].Contract_No);
     $('#cntsPartyName').val(data[0].Party_Name);
    $('#cntsEffectiveDate').val(ChangeDateFormatToddMMYYY(data[0].Effective_Date));
    $('#cntsExperiDate').val(ChangeDateFormatToddMMYYY(data[0].Expiry_RenewableDate));
     $('#pbnSource').val(data[0].Source);
   
    
   // $('#hdnLoggedInUser').val(loggedinUserid);
   // $('#pdReqBy').val(loggedinUserName);
    //$('#emUploadedBy').val(loggedinUserName);

}
var commonArray = [];
var tagArray = [];
var IdPd = 0;
function deleteRows(rowId) {

    var result = confirm("Are you sure want to delete this file?");
    if (result)
    {
        var data = commonArray.filter(function (itemParent) { return (itemParent.Id == rowId); });
        var url = data[0].FileUrl;
        var type = data[0].AttachmentType;
        var id = data[0].Id 
        CommonAjaxMethod(virtualPath + 'CommonMethod/DeleteFileOnEdit', { FileUrl: url, AttachmentType: type, Id: id }, 'POST', function (response) {
            commonArray = commonArray.filter(function (itemParent) { return (itemParent.Id!= rowId); });
            BindDocumentGrid(commonArray,false)
        });

    }


}
function BindDocumentGrid(commonAry, from) {
        

    if (from == true) {
        $('#emDocumentListView').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": commonAry,
            "paging": false,
            "info": false,
            "columns": [
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {
                        if (row.AttachmentType == "Video")
                            return "<i class='fas fa-video mtr-y-clr fn-md '></i>" + row.FileName;
                        else if (row.AttachmentType == "Photo")
                            return "<i class='fas fa-image mtr-y-clr fn-md '></i>" + row.FileName;
                        else
                            return "<i class='fas fa-file-pdf mtr-y-clr fn-md '></i>" + row.FileName;
                    }
                },
              //  { "data": "UploadedBy" },
                { "data": "AttachmentType" },
                { "data": "FileSize" }


            ]
        });
    }
    else { 
        $('#emDocumentListUpdate').DataTable({
        "processing": true, // for show progress bar           
        "destroy": true,
        "data": commonAry,
            "paging": false,
            "info": false,
        "columns": [
            {
                "orderable": false,
                data: null, render: function (data, type, row) {
                    if (row.AttachmentType == "Video")
                        return "<i class='fas fa-video mtr-y-clr fn-md '></i>" + row.FileName;
                    else if (row.AttachmentType == "Photo")
                        return "<i class='fas fa-image mtr-y-clr fn-md '></i>" + row.FileName;
                    else
                        return "<i class='fas fa-file-pdf mtr-y-clr fn-md '></i>" + row.FileName;
                }
            },
           // { "data": "UploadedBy" },
            { "data": "AttachmentType" },
            { "data": "FileSize" },
            {
                "orderable": false,
                data: null, render: function (data, type, row) {

                    var strReturn = "<a title='Click to Remove' data-toggle='tooltip' data-original-title='Click to DeActivate' class='AIsActive'  onclick='deleteRows(" + row.Id + ")' ><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'  ></i> </a>";


                    return strReturn;
                }
            }


        ]
    });
    }
}

function BindContent()
{
    CommonAjaxMethod(virtualPath + 'DigitalLibrary/GetMyContent',  null, 'GET', function (response)
    {
       
        var dataPending = response.data.data.Table;

        var dataApproved = response.data.data.Table1;

        var dataResubmitted = response.data.data.Table2;
        
        var dataRejected = response.data.data.Table3;

        $('#tblPendingContent').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": dataPending,

            "columns": [
               
                { "data": "Req_No" },              
                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {

                        var strReturn = ChangeDateFormatToddMMYYY(row.Req_Date);
                        return strReturn;
                    }
                },
                
                
                { "data": "ReqBy" },
                { "data": "Category" },
                { "data": "SubCategory" },
                { "data": "doc_no" },
                { "data": "proj_name" },
                { "data": "Document_Category" },
                { "data": "Title" },
                { "data": "Tags" },
                { "data": "Status" },                
                {
                    "orderable": false,
                    data: null, render: function (data, type, row)
                    {

                        var strReturn = '<a href="" onclick="return EditContent(' + row.Id + ')"><i class="fas fa-edit"></i>View</a>';
                        return strReturn;
                    }
                }


            ]
        });


        $('#tblApprovedContent').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": dataApproved,

            "columns": [

                { "data": "Req_No" },
                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {

                        var strReturn = ChangeDateFormatToddMMYYY(row.Req_Date);
                        return strReturn;
                    }
                },


                { "data": "ReqBy" },
                { "data": "Category" },
                { "data": "SubCategory" },
                { "data": "doc_no" },
                { "data": "proj_name" },
                { "data": "Document_Category" },
                { "data": "Title" },
                { "data": "Tags" },
                { "data": "Status" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {

                        var strReturn = '<a href="" onclick="return ShowContent(' + row.Id +')" ><i class="fas fa-eye"></i>View</a>';
                        return strReturn;
                    }
                }


            ]
        });

        $('#tblResubmittedContent').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": dataResubmitted,

            "columns": [

                { "data": "Req_No" },
                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {

                        var strReturn = ChangeDateFormatToddMMYYY(row.Req_Date);
                        return strReturn;
                    }
                },


                { "data": "ReqBy" },
                { "data": "Category" },
                { "data": "SubCategory" },
                { "data": "doc_no" },
                { "data": "proj_name" },
                { "data": "Document_Category" },
                
                { "data": "Title" },
                { "data": "Tags" },
                { "data": "Status" },
                { "data": "Reason" },
                
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {

                        var strReturn = '<a href="" onclick="return EditContent(' + row.Id + ')"><i class="fas fa-edit"></i>View</a>';
                        return strReturn;
                    }
                }


            ]
        });


        $('#tblRejectedContent').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": dataRejected,

            "columns": [

                { "data": "Req_No" },
                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {

                        var strReturn = ChangeDateFormatToddMMYYY(row.Req_Date);
                        return strReturn;
                    }
                },


                { "data": "ReqBy" },
                { "data": "Category" },
                { "data": "SubCategory" },
                { "data": "doc_no" },
                { "data": "proj_name" },
                { "data": "Document_Category" },
                { "data": "Title" },
                { "data": "Tags" },
                { "data": "Status" },
                { "data": "Reason" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {

                        var strReturn = '<a href="" onclick="return ShowContent(' + row.Id +')" ><i class="fas fa-eye"></i>View</a>';
                        return strReturn;
                    }
                }


            ]
        });
    });

   
}
function SetApprover(ctrl)
{
   
    if (ctrl.value != '')
    {
        var docCategory = ctrl.value;
        var tName=$('#hdnTagName').val();
        CommonApprovalUploadWorkFlow(tName, UserGrade, docCategory, IsPM);
    }
}
var subCategoryNameToUpload = "";
function SetSubCategoryName(ctrl)
{
    if (document.getElementById("pdSubCategory").selectedIndex != -1)
    {
        subCategoryNameToUpload = document.getElementById("pdSubCategory").options[document.getElementById("pdSubCategory").selectedIndex].text;

        HideErrorMessage(ctrl);
    }
}
function SaveData(num) {

    var reqApprovarId;

    var TagName = $('#ddlCategory').val();

    var className = 'Mandatorypld';
    var Categoryid = $('#ddlCategory option[value="' + $('#ddlCategory').val() + '"]').attr("dataEle")

    if (num == 1) {

        if ($('#hdnApprovar').val() == 'S') {
            reqApprovarId = ContentManagerID;
        }
        if ($('#hdnApprovar').val() == 'E') {
            reqApprovarId = EDID;
        }
        if ($('#hdnApprovar').val() == 'P') {
            reqApprovarId = 0;
        }
        if (UserGrade == 'A') {
            reqApprovarId = loggedinUserid;
        }
        var dt = new Date();

        var ContentObj =
        {
            ProjectLead: $('#pdProjectLead').val(),
            Id: $('#hdnContentId').val(),
            Req_No: $('#pdReqNo').val(),
            Req_Date: ChangeDateFormat($('#pdReqDate').val()),
            Req_By: $('#loggedinUserid').val(),
            CategoryId: Categoryid,
            Sub_CategoryId: $('#pdSubCategory').val(),
            Project_CodeId: $('#pdProjectCode').val(),
            PlaceIds: $('#pdPlaceOfOrigin').val(),
            Upload_Date: dt,
            Report_No: $('#pdReportNo').val(),
            Author_Id: arrayOfautherId.join(),
            Title: $('#pdTitle').val(),
            Sub_Title: $('#pdSubTitle').val(),
            Tags: $('#pdTag').val(),
            Abstract_Summary: $('#pdSummary').val(),
            Remark: $('#pdRemarks').val(),
            Document_Category: $('#pdDocument').val(),
            Published: " ",
            Proposal_No: $('#pldProposalNo').val(),
            Accepted: $('#pldAccepted').val(),
            Copyright: $('#tmCopyRights').val(),
            Contract_No: $('#cntsContactNo').val(),
            Party_Name: $('#cntsPartyName').val(),
            Effective_Date: ChangeDateFormat($('#cntsEffectiveDate').val()),
            Expiry_RenewableDate: ChangeDateFormat($('#cntsExperiDate').val()),
            Source: $('#pbnSource').val(),
            Stage_Level: " ",
            Status: " ",
            IPAddress: $('#hdnIP').val(),
            UserId: loggedinUserid,
            DocumentList: [],
            ApproverId: reqApprovarId,
            IsSubmit: num,
            ApprovarAuth:$('#hdnApprovar').val()

        }
        for (var i = 0; i < commonArray.length; i++) {
            var Files =
            {
                ActualFileName: commonArray[i].ActualFileName,
                NewFileName: commonArray[i].NewFileName,
                /*    UploadedBy: $('#hdnLoggedInUser').val(),*/
                AttachmentType: commonArray[i].AttachmentType,
                FileSize: commonArray[i].FileSize,
                FileUrl: commonArray[i].FileUrl,
                FileName: commonArray[i].FileName
            }
            ContentObj.DocumentList.push(Files)
        }


        CommonAjaxMethod(virtualPath + 'DigitalLibrary/SaveDLContent', ContentObj, 'POST', function (response) {

            BindContent();   
            $('#UpdateButtonClose').click();  
        });

    }
    else if (checkValidationOnSubmit(className) == true)
    {
        if ($('#hdnApprovar').val() == 'S')
        {
            reqApprovarId = ContentManagerID;
        }
        if ($('#hdnApprovar').val() == 'E')
        {
            reqApprovarId = EDID;
        }
        if ($('#hdnApprovar').val() == 'P') {
            reqApprovarId = 0;
        }
        if (UserGrade == 'A') {
            reqApprovarId = loggedinUserid;
        }
        var dt = new Date();

        var ContentObj =
        {
            ProjectLead: $('#pdProjectLead').val(),
            Id: $('#hdnContentId').val(),
            Req_No: $('#pdReqNo').val(),
            Req_Date: ChangeDateFormat($('#pdReqDate').val()),
            Req_By: $('#loggedinUserid').val(),
            CategoryId: Categoryid,
            Sub_CategoryId: $('#pdSubCategory').val(),
            Project_CodeId: $('#pdProjectCode').val(),
            PlaceIds: $('#pdPlaceOfOrigin').val(),
            Upload_Date: dt,
            Report_No: $('#pdReportNo').val(),
            Author_Id: arrayOfautherId.join(),
            Title: $('#pdTitle').val(),
            Sub_Title: $('#pdSubTitle').val(),
            Tags: $('#pdTag').val(),
            Abstract_Summary: $('#pdSummary').val(),
            Remark: $('#pdRemarks').val(),
            Document_Category: $('#pdDocument').val(),
            Published: " ",
            Proposal_No: $('#pldProposalNo').val(),
            Accepted: $('#pldAccepted').val(),
            Copyright: $('#tmCopyRights').val(),
            Contract_No: $('#cntsContactNo').val(),
            Party_Name: $('#cntsPartyName').val(),
            Effective_Date: ChangeDateFormat($('#cntsEffectiveDate').val()),
            Expiry_RenewableDate: ChangeDateFormat($('#cntsExperiDate').val()),
            Source: $('#pbnSource').val(),
            Stage_Level: " ",
            Status: " ",
            IPAddress: $('#hdnIP').val(),
            UserId: loggedinUserid,
            DocumentList: [],
            ApproverId: reqApprovarId,
            IsSubmit: num,
            ApprovarAuth:$('#hdnApprovar').val()

        }
        if (commonArray.length > 0) {
            for (var i = 0; i < commonArray.length; i++) {
                var Files =
                {
                    ActualFileName: commonArray[i].ActualFileName,
                    NewFileName: commonArray[i].NewFileName,
                    /*    UploadedBy: $('#hdnLoggedInUser').val(),*/
                    AttachmentType: commonArray[i].AttachmentType,
                    FileSize: commonArray[i].FileSize,
                    FileUrl: commonArray[i].FileUrl,
                    FileName: commonArray[i].FileName
                }
                ContentObj.DocumentList.push(Files)
            }


            CommonAjaxMethod(virtualPath + 'DigitalLibrary/SaveDLContent', ContentObj, 'POST', function (response) {
                BindContent();
                $('#UpdateButtonClose').click();  
            });
        }
        else {
            alert("Please upload atleast one document.");
            return false;
        }
    }

}
function FormClear() {

    //$("#pdTag").select2({
    //    multiple: true,
    //});
    //$('#pdTag').val([1, 3]).trigger('change'); 
    $('#pdProjectName').val('');
    $('#pdThematicArea').val('');
    $('#pdFundedBy').val('');
    arrayOfauther = [];
    arrayOfautherId = [];

    CommonAjaxMethod(virtualPath + 'DigitalLibrary/GetMaxReqNo', null, 'GET', function (response) {
        $('#pdReqNo').val(response.data.data.Table[0].ReqNo);
    });

    if (('#pdSubCategory').length > 0) {
        $('#pdSubCategory').val('0');
        $('#select2-pdSubCategory-container').text('Select');

    }
    if (('#pdProjectCode').length > 0) {
        $('#pdProjectCode').val('0');
        $('#select2-pdProjectCode-container').text('Select');

    }
    if (('#pdPlaceOfOrigin').length > 0) {
        $('#pdPlaceOfOrigin').val([0]).trigger('change');

    }
    if (('#pdReportNo').length > 0) {
        $('#pdReportNo').val('');

    }
    if (('#pdAuther').length > 0) {
        $('#pdAuther').val([0]).trigger('change');


    }
    if (('#pdTitle').length > 0) {
        $('#pdTitle').val('');

    }
    if (('#pdSubTitle').length > 0) {
        $('#pdSubTitle').val('');

    }
    if (('#pdTag').length > 0) {
        $('#pdTag').val([0]).trigger('change');

    }
    if (('#pdSummary').length > 0) {
        $('#pdSummary').val('');

    }
    if (('#pdRemarks').length > 0) {
        $('#pdRemarks').val('');

    }
    if (('#pdDocument').length > 0) {
        $('#pdDocument').val('0');
        $('#select2-pdDocument-container').text('Select');

    }
    if (('#pdPublished').length > 0) {
        $('#pdPublished').val('0');

    }
    if (('#pldProposalNo').length > 0) {
        $('#pldProposalNo').val('');

    }
    if (('#pldAccepted').length > 0) {
        $('#pldAccepted').val('');
        $('#select2-pldAccepted-container').text('Select');

    }
    if (('#tmCopyRights').length > 0) {
        $('#tmCopyRights').val('');

    }
    if (('#cntsContactNo').length > 0) {
        $('#cntsContactNo').val('');

    }
    if (('#cntsPartyName').length > 0) {
        $('#cntsPartyName').val('');

    }
    if (('#cntsEffectiveDate').length > 0) {
        $('#cntsEffectiveDate').val('');

    }
    if (('#cntsExperiDate').length > 0) {
        $('#cntsExperiDate').val('');

    }


    commonArray = [];
    BindDocumentGrid(commonArray);
    $("#emDocumentList").find('tbody').html("");
    $("#emAttachementType").val('0');
    $('#select2-emAttachementType-container').text('Select');
    $("#emFileUpload").val('');
    $("#emFileName").val('');
    $("#VedioFileName").val('');
}