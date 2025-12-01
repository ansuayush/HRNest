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

    LoadMasterDropdown('ddlForward', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.Employee,
        manualTableId: 0
    }, 'Select', false);

   // $('#emUploadedBy').val(loggedinUserName);

    $('#selectall').click(function () {
        $('.selectedId').prop('checked', this.checked);
    });

    $('.selectedId').change(function () {
        var check = ($('.selectedId').filter(":checked").length == $('.selectedId').length);
        $('#selectall').prop("checked", check);
    });
});
function FillAuther(strArray) {
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
        }, 'GET', function (response) {
            var autherData = response.data.data.Table;
            for (var i = 0; i < arrayOfautherId.length; i++) {
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
            html += "<ul class='list-style-type:none'>";
            for (var i = 0; i < arrayOfauther.length; i++) {
                html += "<li onclick='RemoveAuther(" + arrayOfauther[i].Id + ")'><span  role='presentation'>×</span>   " + arrayOfauther[i].Name + "</li>";
            }
            html += "</ul>";

            $("#dvAutherList").append(html);


        });
}
var arrayOfauther = [];
var arrayOfautherId = [];
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
        $('#txtFwReason').val('');   
        $('#txtResubmitReason').val('');
        $('#txtRejectReason').val('');        
        $('#Div1').hide();
        $('#Div2').hide();
        $('#Div3').hide();        
        $('#ddlForward').val(0); 
       
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
                        UploadedBy: $("#emUploadedBy").val(),
                        AttachmentType: $("#emAttachementType").val(),
                        FileUrl: $("#VedioFileName").val(),
                        FileSize: "0"

                    }

                    commonArray.push(objarrayinner);

                    $('#emDocumentList').DataTable({
                        "processing": true, // for show progress bar           
                        "destroy": true,
                        "data": commonArray,
                        "bPaginate": false,
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
                            { "data": "UploadedBy" },
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
                                UploadedBy: $("#emUploadedBy").val(),
                                AttachmentType: $("#emAttachementType").val(),
                                FileSize: result.FileModel.FileSize,
                                FileUrl: result.FileModel.FileUrl

                            }

                            commonArray.push(objarrayinner);


                            $('#emDocumentList').DataTable({
                                "processing": true, // for show progress bar           
                                "destroy": true,
                                "data": commonArray,
                                "bPaginate": false,
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
                                    { "data": "UploadedBy" },
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

                        document.getElementById('hReturnMessage').innerHTML = error;
                        $('#btnShowModel').click();
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
    CommonAjaxMethod(virtualPath + 'DigitalLibrary/GetProjectDetails', { id: pid }, 'GET', function (response) {
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
     
    var categoryText = data[0].Category;
    var Categoryid = data[0].CategoryId;
    var tagName = data[0].TagName;
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
    
    
     
    $('#pdSubCategory').val(data[0].SubCategory);

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
    $('#pdProjectCode').val(pID).trigger('change'); 

    CommonAjaxMethod(virtualPath + 'DigitalLibrary/GetProjectDetails', { id: data[0].Project_CodeId }, 'GET', function (response)
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

        let pdTagArray = data[0].Tag_Id.split(',');

        $("#pdTag").select2({
            multiple: true,
        });
        $('#pdTag').val(pdTagArray).trigger('change'); 


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

       let pdPlaceOfOriginArray = data[0].PlaceId.split(',');

        $("#pdPlaceOfOrigin").select2({
            multiple: true,
        });
       $('#pdPlaceOfOrigin').val(pdPlaceOfOriginArray).trigger('change'); 

 
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

     $('#pdReportNo').val(data[0].Report_No);
     $('#pdTitle').val(data[0].Title);
     $('#pdSubTitle').val(data[0].Sub_Title);
     $('#pdSummary').val(data[0].Abstract_Summary);
     $('#pdRemarks').val(data[0].Remark);
    
     $('#pldProposalNo').val(data[0].Proposal_No);
    
    $('#pldAccepted').val(data[0].Accepted).trigger('change'); 
     $('#tmCopyRights').val(data[0].Copyright);
     $('#cntsContactNo').val(data[0].Contract_No);
     $('#cntsPartyName').val(data[0].Party_Name);
    $('#cntsEffectiveDate').val(ChangeDateFormatToddMMYYY(data[0].Effective_Date));
    $('#cntsExperiDate').val(ChangeDateFormatToddMMYYY(data[0].Expiry_RenewableDate));
     $('#pbnSource').val(data[0].Source);
     $('#pdDocument').val(data[0].Document_Category).trigger('change'); 
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
            commonArray = commonArray.filter(function (itemParent) { return (itemParent.Id != rowId); });
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
                { "data": "UploadedBy" },
                { "data": "AttachmentType" },
                { "data": "FileSize" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {
                        var url = baseURL + row.FileUrl;
                        var strReturn = '<button   onclick="DownloadFile(' + row.Id + ')"  target="_blank" class="green-clr"><i class="fas fa-download"></i>Download</button>';
                        return strReturn;
                    }
                }


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
            { "data": "UploadedBy" },
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
function DownloadFile(id) {
    $.ajax({
        url: virtualPath + 'DigitalLibrary/DownloadFile?Id=' + id,
        type: "GET",
        contentType: 'application/json',
        success: function (response) {
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

            document.getElementById('hReturnMessage').innerHTML = error;
            $('#btnShowModel').click();
            isSuccess = false;
        }

    });
}
function BindContent()
{
    CommonAjaxMethod(virtualPath + 'DigitalLibrary/GetMyTeamContent',  null, 'GET', function (response)
    {
       
        var dataPending = response.data.data.Table;

        var dataApproved = response.data.data.Table1;

        var dataResubmitted = response.data.data.Table2;
        
        var dataRejected = response.data.data.Table3;
        var dataForwared = response.data.data.Table4;

        $('#tblPendingContent').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": dataPending,

            "columns": [

               

                    {
                        "orderable": false,
                    data: null, render: function (data, type, row)
                    {

                        var strReturn = '<input type="checkbox" id="chk-' + row.Id + '" class="selectedId sltchk" name="select" onchange="valueChanged()"><label for= "chk-' + row.Id +'" class= "m-0" ></label>';
                            
                    return strReturn;
                    }
                },
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

                        var strReturn = '<a href="" onclick=" return EditContent(' + row.Id + ')"><i class="fas fa-edit"></i>View</a>';
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

                        var strReturn = '<a href="" onclick=" return ShowContent(' + row.Id +')" ><i class="fas fa-eye"></i>View</a>';
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

                        var strReturn = '<a href="" onclick="return ShowContent(' + row.Id + ')"><i class="fas fa-edit"></i>View</a>';
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

        $('#tblForwarededContent').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": dataForwared,

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

                        var strReturn = '<a href="" onclick="return ShowContent(' + row.Id + ')" ><i class="fas fa-eye"></i>View</a>';
                        return strReturn;
                    }
                }


            ]
        });
    });

   
}

function SaveData(isAprroved)
{
    var contentArrayToApproval = [];
    var ContentObj;     
     
    var isValid = true;
    if ($('#hdnType').val() == 'Div1')
    {
        
        var extactValueReason = jQuery.trim($('#txtFwReason').val());
        if (extactValueReason == "")
        {

            $('#sptxtFwReason').show();
            //$('#sptxtFwReason').classList.add("errorValidation");           
            isValid = false;
        }
        else
        {
            //$('#sptxtFwReason').classList.remove("errorValidation");
            $('#sptxtFwReason').hide();
            isValid = true;
        }
        var extactddlForward = jQuery.trim($('#ddlForward').val());
        if (extactddlForward == "Select")
        {

            $('#spddlForward').show();
           // $('#spddlForward').classList.add("errorValidation");
            isValid = false;

        }
        else {
           // $('#spddlForward').classList.remove("errorValidation");
            $('#spddlForward').hide();
            isValid = true;
        }
        if (isValid)
        {
            ContentObj =
            {
                Id: $('#hdnContentId').val(),
                Reason: $('#txtFwReason').val(),
                ContentStatus: 4,
                ForwaredTo: $('#ddlForward').val(),
                UserGrade: UserGrade

            }
            contentArrayToApproval.push(ContentObj);
            CommonAjaxMethod(virtualPath + 'DigitalLibrary/UpdateDLContent', contentArrayToApproval, 'POST', function (response)
            {
                var fwdata = response.data.Table;
                $('#lblFwEmail').text(fwdata[0].ForwaredEmail);
                $('#lblFwCC').text(fwdata[0].CC);
                $('#lblFwName').text(fwdata[0].ForwaredTo);
                $('#lblFwbyName').text(fwdata[0].FwByName);
                $('#lblFwCate').text(fwdata[0].Category);
                $('#lblFwProject').text(fwdata[0].proj_name);
                $('#lblFwDate').text(fwdata[0].AprrovalDate);
                $('#lblFwReason').text(fwdata[0].Reason);                
                $('#lblFwDocumentCategory').text(fwdata[0].Document_Category); 
                $('#lblFwCateMessage').text(fwdata[0].Category); 
                $('#UpdateButtonClose').click();
            });
            
        }

    }
    else if ($('#hdnType').val() == 'Div2')
    {
        
        var extactValueSubmitReason = jQuery.trim($('#txtResubmitReason').val());
        if (extactValueSubmitReason == "") {

            $('#sptxtResubmitReason').show();
           // $('#sptxtResubmitReason').classList.add("errorValidation");
            isValid = false;
        }
        else {
            //$('#sptxtResubmitReason').classList.remove("errorValidation");
            $('#sptxtResubmitReason').hide();
            isValid = true;
        }

        if (isValid) {
            ContentObj =
            {
                Id: $('#hdnContentId').val(),
                Reason: $('#txtResubmitReason').val(),
                ContentStatus: 2,
                UserGrade: UserGrade


            }
            contentArrayToApproval.push(ContentObj);
            CommonAjaxMethod(virtualPath + 'DigitalLibrary/UpdateDLContent', contentArrayToApproval, 'POST', function (response) {
          

                var fwdata = response.data.Table;
                $('#lblRBReqBy').text(fwdata[0].email);
                $('#lblRBCC').text(fwdata[0].CC);
                $('#lblRBReqByName').text(fwdata[0].ReqBy);
                $('#lblRBApprovarName').text(fwdata[0].FwByName);
                $('#lblRBCategory').text(fwdata[0].Category);
                $('#lblRBProjectName').text(fwdata[0].proj_name);
                $('#lblRBLocation').text(fwdata[0].Places);
                $('#lblRBDate').text(fwdata[0].AprrovalDate);
                $('#lblRBReason').text(fwdata[0].Reason);                
                $('#lblRBDocumentCategory').text(fwdata[0].Document_Category);
                $('#lblRBCategoryMessage').text(fwdata[0].Category); 


                $('#UpdateButtonClose').click();
            });
        }

    }
    if ($('#hdnType').val() == 'Div3')
    {

        var extactValueRejectReason = jQuery.trim($('#txtRejectReason').val());
        if (extactValueRejectReason == "") {

            $('#sptxtRejectReason').show();
            //$('#sptxtRejectReason').classList.add("errorValidation");
            isValid = false;
        }
        else {
           // $('#sptxtRejectReason').classList.remove("errorValidation");
            $('#sptxtRejectReason').hide();
            isValid = true;

        }
        ContentObj =
        {
            Id: $('#hdnContentId').val(),
            Reason: $('#txtRejectReason').val(),
            ContentStatus: 3,
            UserGrade: UserGrade

            
        }    
        if (isValid)
        {
            contentArrayToApproval.push(ContentObj);
            CommonAjaxMethod(virtualPath + 'DigitalLibrary/UpdateDLContent', contentArrayToApproval, 'POST', function (response) {
                                
                var fwdata = response.data.Table;
                $('#lblRJReqBy').text(fwdata[0].email);
                $('#lblRJCC').text(fwdata[0].CC);
                $('#lblRJReqByName').text(fwdata[0].ReqBy);
                $('#lblRJApprovarName').text(fwdata[0].FwByName);
                $('#lblRJCategory').text(fwdata[0].Category);
                $('#lblRJProjectName').text(fwdata[0].proj_name);
                $('#lblRJLocation').text(fwdata[0].Places);
                $('#lblRJDate').text(fwdata[0].AprrovalDate);
                $('#lblRJReason').text(fwdata[0].Reason);
                $('#lblRJDocumentCategory').text(fwdata[0].Document_Category);
                $('#lblRJCategoryMessage').text(fwdata[0].Category); 

                $('#UpdateButtonClose').click();
            });
        }

    }
    if (isAprroved)
    {        
        ContentObj =
        {
            Id: $('#hdnContentId').val(),           
            ContentStatus: 1,
            UserGrade: UserGrade

        }    
        contentArrayToApproval.push(ContentObj);
        CommonAjaxMethod(virtualPath + 'DigitalLibrary/UpdateDLContent', contentArrayToApproval, 'POST', function (response) {

            var fwdata = response.data.Table;
            $('#lblAprReqByEmail').text(fwdata[0].email);
            $('#lblAprCC').text(fwdata[0].CC);
            $('#lblAprReqBy').text(fwdata[0].ReqBy);
            $('#lblAprAprName').text(fwdata[0].FwByName);
            $('#lblAprCategory').text(fwdata[0].Category);
            $('#lblAprProjectName').text(fwdata[0].proj_name);
            $('#lblAprLocation').text(fwdata[0].Places);            
            $('#lblAprDate').text(fwdata[0].AprrovalDate);        
            $('#lblAprDocumentCategory').text(fwdata[0].Document_Category); 
            $('#lblAprCategoryMessage').text(fwdata[0].Category); 
            $('#UpdateButtonClose').click();
        });
    }
     
    BindContent();
}


var divs = ["Div1", "Div2", "Div3", "Div4"];
var visibleDivId = null;
function divVisibility(divId)
{

    $('#hdnType').val(divId);
    if (visibleDivId === divId)
    {
        visibleDivId = null;
    } else {
        visibleDivId = divId;
    }
    hideNonVisibleDivs();
}
function hideNonVisibleDivs() {
    var i, divId, div;
    for (i = 0; i < divs.length; i++) {
        divId = divs[i];
        div = document.getElementById(divId);
        if (visibleDivId === divId) {
            div.style.display = "block";
        } else {
            div.style.display = "none";
        }
    }
}
function valueChanged()
{
    if ($('.sltchk').is(":checked"))
        $(".hide").show();
    else
        $(".hide").hide();
};
function ApproveSelectContent()
{
    var chk = $('.sltchk').is(":checked");

    var contentIdArray = [];
    var checkedArr = $("#tblPendingContent").find("input[type=checkbox]:checked").map(function ()
    {
        return this.id;
    }).get();

    for (var i = 0; i < checkedArr.length; i++)
    {
        if (checkedArr[i].includes('chk'))
        {
            var ids = checkedArr[i].split('-');
           var objIDs= {
               Id: ids[1],
               ContentStatus: 1,
               UserGrade: UserGrade

            }
            contentIdArray.push(objIDs);
        }
    }
    CommonAjaxMethod(virtualPath + 'DigitalLibrary/UpdateDLContent', contentIdArray, 'POST', function (response)
    {

        BindContent();
       
    });

}