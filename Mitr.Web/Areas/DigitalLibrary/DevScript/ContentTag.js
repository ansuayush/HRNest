$(document).ready(function () {

    

    var obj = {
        ParentId: 0,
        masterTableType: DropDownTypeEnum.Category,
        isMasterTableType: false,
        isManualTable: false,
        manualTable: 0,
        manualTableId: 0
    }
    LoadMasterDropdown('ddlCategory', obj, 'Choose Your Category', true);
    $(function () {
        $('.datepicker').datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: "dd-mm-yy",
            yearRange: "-90:+10"
        });

    });

});

 
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
var subCategoryNameToUpload = "";
function SetSubCategoryName(ctrl) {
    subCategoryNameToUpload = ctrl.options[ctrl.selectedIndex].text;

    HideErrorMessage(ctrl);
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
    for (var i = 0; i < arrayOfauther.length; i++)
    {
        html += "<li onclick='RemoveAuther(" + arrayOfauther[i].Id + ")'><span class='select2-selection__choice__remove' role='presentation'>×</span>" + arrayOfauther[i].Name + "</li>";
    }
    html += "</ul>";
   
    $("#dvAutherList").append(html);

   
}
function RemoveAuther(id)
{  
    

    arrayOfauther = arrayOfauther.filter((arrayOfauther) => arrayOfauther.Id!= id);
   
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
var commonArray = [];
var tagArray = [];
var IdPd = 0;
function FillSubCategory(ctrl) {


    var categoryText = ctrl.options[ctrl.selectedIndex].text;
    var Categoryid = $('#ddlCategory option[value="' + $(ctrl).val() + '"]').attr("dataEle")
    var tagName = $(ctrl).val();

    arrayOfauther = [];
    arrayOfautherId = [];
    var html = "";
    $("#dvAutherList").html('');
    html += "<ul class='list-style-type:none'>";
    for (var i = 0; i < arrayOfauther.length; i++) {
        html += "<li onclick='RemoveAuther(" + arrayOfauther[i].Id + ")'><span class='select2-selection__choice__remove' role='presentation'>×</span>   " + arrayOfauther[i].Name + "</li>";
    }
    html += "</ul>";

    $("#dvAutherList").append(html);

    CommonAjaxMethod(virtualPath + 'DigitalLibrary/GetMatrix', { category: tagName }, 'GET', function (response)
    {      

        var d = response.data.data.Table;
        var $ele2 = $('#pdDocument');
        $ele2.empty();
       
        $ele2.append($('<option/>').val('0').text('Select'));
        $.each(d, function (ii, vall) {  
            $ele2.append($('<option/>').val(vall.DocCatCode).text(vall.DocCatCode));
        })
        $('#pdDocument').val('0').trigger('change'); 
    });

    $('#pdCategory').val(categoryText);

    const words = ['pd', 'pld', 'cnts', 'eb', 'tm', 'pbn'];
    const resultEqual = words.filter(function (itemParent) { return (itemParent == tagName); });
    const resultNotEqual = words.filter(function (itemParent) { return (itemParent != tagName); });
    if (tagName == 'Choose Your Category') {
        $('#FileAttachmentContent').hide();
        $('#mainContentDiv').hide();

    }
    else {
        FormClear();
        $('#FileAttachmentContent').show();
        $('#mainContentDiv').show();
    }

    for (var i = 0; i < resultEqual.length; i++) {
        $('.' + resultEqual[i]).show();
    }

    for (var k = 0; k < resultNotEqual.length; k++) {
        $('.' + resultNotEqual[k]).hide();
    }

    commonArray = [];
    IdPd = 0;


    CommonAjaxMethod(virtualPath + 'DigitalLibrary/GetMaxReqNo', null, 'GET', function (response) {
        $('#pdReqNo').val(response.data.data.Table[0].ReqNo);
    });



    var dt = new Date();
    var newDate = ChangeDateFormatToddMMYYY(dt);
    $('#pdReqDate').val(newDate);


    LoadMasterDropdown('pdSubCategory',
        {
            ParentId: Categoryid, masterTableType: DropDownTypeEnum.Category, isMasterTableType: false, isManualTable: false,
            manualTable: 0,
            manualTableId: 0
        }, 'Select', false);



    LoadMasterDropdown('pdProjectCode',
        {
            ParentId: 0,
            masterTableType: 0,
            isMasterTableType: false,
            isManualTable: true,
            manualTable: ManaulTableEnum.Project,
            manualTableId: 0
        }, 'Select', false);

    LoadMasterDropdown('pdPlaceOfOrigin',
        {
            ParentId: 0,
            masterTableType: 0,
            isMasterTableType: false,
            isManualTable: true,
            manualTable: ManaulTableEnum.MasterLocation,
            manualTableId: 0
        }, 'Select', false);


    LoadMasterDropdown('pdAuther', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.Employee,
        manualTableId: 0
    }, 'Select', false);


    LoadMasterDropdown('pdProjectLead', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.Employee,
        manualTableId: 0
    }, 'Select', false);
   
    LoadMasterDropdown('ebProjectLead', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.Employee,
        manualTableId: 0
    }, 'Select', false);

    $('#hdnLoggedInUser').val(loggedinUserid);
    $('#pdReqBy').val(loggedinUserName);
   // $('#emUploadedBy').val(loggedinUserName);

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
    var isValidUpload = false;
    isValidUpload = CommonApprovalUploadWorkFlow(document.getElementById("ddlCategory").value, UserGrade, document.getElementById("pdDocument").value, IsPM)
    if (UserGrade == 'A') {
        isValidUpload = true;
    }
    if (isValidUpload == true)
    {
        if (subCategoryNameToUpload == "") {
            document.getElementById('hReturnMessage').innerHTML = "Please select subcategorty to upload!";
            $('#btnShowModel').click();
        }
        else {
            if (checkValidationOnSubmit(mandateClass) == true) {

                var CateObject = document.getElementById("ddlCategory");
                var categoryText = CateObject.options[CateObject.selectedIndex].text;

                var AttachmentTypeObject = document.getElementById("emAttachementType");
                var AttachmentTypeText = AttachmentTypeObject.options[AttachmentTypeObject.selectedIndex].text;

                var fileUpload = $("#emFileUpload").get(0);

                var files = fileUpload.files;
                if ($("#emAttachementType").val() == "Video") {
                    if ($("#VedioFileName").val() == "") {
                        isvalid = false;
                        document.getElementById('hReturnMessage').innerHTML = "Please enter the video file details!";
                        $('#btnShowModel').click();

                    }
                    else {
                        IdPd = IdPd + 1;
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

                        $('#emDocumentList').DataTable({
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
                             /*   { "data": "UploadedBy" },*/
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

                                IdPd = IdPd + 1;
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


                                $('#emDocumentList').DataTable({
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
                                      /*  { "data": "UploadedBy" },*/
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
    else {
        document.getElementById('hReturnMessage').innerHTML = "You do not have permission to upload document for this category!";
        $('#btnShowModel').click();
    }
}
function deleteRows(rowId) {

    var result = confirm("Are you sure want to delete this file?");
    if (result) {
        var data = commonArray.filter(function (itemParent) { return (itemParent.Id == rowId); });
        var url = data[0].FileUrl;
        var type = data[0].AttachmentType;
        if (type == "Video") {

            document.getElementById('hReturnMessage').innerHTML = "File has been deleted!";
            $('#btnShowModel').click();


            commonArray = commonArray.filter(function (itemParent) { return (itemParent.Id != rowId); });
            BindDocumentGrid(commonArray)
        }
        else {
            CommonAjaxMethod(virtualPath + 'CommonMethod/DeleteFile', { FileUrl: url }, 'POST', function (response) {
                commonArray = commonArray.filter(function (itemParent) { return (itemParent.Id != rowId); });
                BindDocumentGrid(commonArray)
            });
        }

    }


}

function BindDocumentGrid(commonArray) {
    $('#emDocumentList').DataTable({
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
           /* { "data": "UploadedBy" },*/
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

function RemoveText(ctrl) {
    tagArray = tagArray.filter(function (itemParent) { return (itemParent != ctrl.title); });
}

function bindTags() {
    $("#treeBox").dxDropDownBox({
        value: [],
        valueExpr: "SubjectMaster_ID",
        placeholder: "-",
        displayExpr: "Name",
        showClearButton: true,
        placeholder: "Select subjects...",
        dataSource: response.dataCollection,
        contentTemplate: function (e) {
            var value = e.component.option("value"),
                $dataGrid = $("<div>").dxDataGrid({
                    dataSource: e.component.option("dataSource"),
                    columns: [{ dataField: "Name", caption: "Subject(s)" }],
                    hoverStateEnabled: true,
                    paging: { enabled: false },
                    filterRow: { visible: true },
                    scrolling: { mode: "infinite" },
                    height: 325,
                    selection: {
                        mode: "multiple",
                        showCheckBoxesMode: "always",
                        allowSelectAll: false
                    },
                    selectedRowKeys: value,
                    selectByClick: true,

                    onSelectionChanged: function (selectedItems) {
                        var keys = selectedItems.currentSelectedRowKeys;
                        var keys2 = selectedItems.currentDeselectedRowKeys;
                        e.component.option("SubjectMaster_ID", keys);
                        if (fromclass != true) {
                            if (keys.length == 1) {
                                //addEmptyRow(true, keys[0].SubjectMaster_ID);
                            }
                            if (keys2.length == 1) {
                                // addEmptyRow(false, keys2[0].SubjectMaster_ID);
                            }
                        }
                        fromclass = false;

                    }

                });

            dataGrid = $dataGrid.dxDataGrid("instance");

            e.component.on("valueChanged", function (args) {
                var value = args.value;
                dataGrid.selectRows(value, false);

            });

            return $dataGrid;
        }
    });
}
function SetApprover(ctrl) {

    if (ctrl.value != '') {
        var docCategory = ctrl.value;       
        CommonApprovalUploadWorkFlow(document.getElementById("ddlCategory").value, UserGrade, docCategory, IsPM);
    }
}
function SaveData(num) {
 
    var reqApprovarId;
    
    var TagName = $('#ddlCategory').val();
  
    var className = 'Mandatorypld';
    var Categoryid = $('#ddlCategory option[value="' + $('#ddlCategory').val() + '"]').attr("dataEle")

    if (num == 1)
    {
       
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

            FormClear();
        });

    }
    else if (checkValidationOnSubmit(className) == true)
    {
        if ($('#hdnApprovar').val()=='S') {
            reqApprovarId = ContentManagerID;
        }
        if ($('#hdnApprovar').val()=='E') {
            reqApprovarId = EDID;
        }
        if ($('#hdnApprovar').val()=='P') {
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

                FormClear();
            });
        }
        else {
            alert("Please upload atleast one document.");
            return false;
        }
    }

}

