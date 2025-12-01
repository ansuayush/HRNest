$(document).ready(function () {

    LoadMasterDropdown('ddlSkills',
        {
            ParentId: 0,
            masterTableType: 0,
            isMasterTableType: false,
            isManualTable: true,
            manualTable: 25,
            manualTableId: 0
        }, 'Select', false);
    LoadMasterDropdown('ddlApplicableTo',
        {
            ParentId: 0,
            masterTableType: 0,
            isMasterTableType: false,
            isManualTable: true,
            manualTable: 53,
            manualTableId: 0
        }, 'Select', false);
    LoadMasterDropdownValueCodeWithOutDefaultValue('ddlApplicableDesignations',
        {
            ParentId: 0,
            masterTableType: 0,
            isMasterTableType: false,
            isManualTable: true,
            manualTable: 24,
            manualTableId: 0
        }, 'Select', false);

    $('#ddlApplicableTo').on('change', function () {
        $('#divApplicabledesignation').css('display', 'none');
        if ($(this).val() === '162') {
            $('#divApplicabledesignation').css('display', 'block');

        }
        //else {
        //    $("#divApplicabledesignation").attr('value', ' ');
        //    //$('#divApplicabledesignation').val('');
        //}
    });

    $(function () {
        $("#bond").click(function () {
            if ($(this).is(":checked")) {
                $("#AddPassport").show();
            } else {
                $("#AddPassport").hide();
            }
        });
    });

    //$('#ddlApplicableDesignations').on('change', function () {
    //    var selMulti = $.map($('#ddlApplicableDesignations option:selected'), function (el, i) {
    //        return $(el).val();
    //    });
    //    $("#hdnSelectDesinations").val(selMulti.join(","));


    //});

    //$('#ddlSkills').on('change', function () {
    //    var selMulti = $.map($('#ddlSkills option:selected'), function (el, i) {
    //        return $(el).val();
    //    });
    //    $("#hdnSelectSkills").val(selMulti.join(","));


    //});
    if (TraingTypeID != null || '') { EditTrainingTypeById(); }

    

});
function BindApplicableDesignations() {
    LoadMasterDropdown('ddlApplicableDesignations',
        {
            ParentId: 0,
            masterTableType: 0,
            isMasterTableType: false,
            isManualTable: true,
            manualTable: 24,
            manualTableId: 0
        }, 'Select', false);

}

//function SetDropDownForApplicableTo() {
//    $('#ddlApplicableTo').on('change', function () {
//        $('#divApplicabledesignation').css('display', 'none');
//        if ($(this).val() === 'Specify') {
//            $('#divApplicabledesignation').css('display', 'block');
//            BindApplicableDesignations();
//        }
//    });
//}
var IsActiveState = 0;

function SaveTrainingType() {

    if (checkValidationOnSubmit('Mandatory') == true) {
        var isValid = true;

        if ($('#ddlApplicableTo').val() == '161') {
            var applicableDesignationId = $('#divApplicabledesignation').val('');
        }
        else {
            if ($('#ddlApplicableDesignations').val() == "") {
                isValid = false;
                $('#spddlApplicableDesignations').show();
            }
            var applicableDesignationId = $('#ddlApplicableDesignations').val().join()
        }
        //if($('#fileUpload').val() == "")
        //{
        //    isValid = false;
        //    $('#spfileUpload').show();
        //}
        if (isValid) {
            var obj = {
                Id: TraingTypeID,
                TrainingTypeName: $('#txtTrainingTypeName').val(),
                TrainingDesc: $('#txtTrainingDesc').val(),
                ApplicableToId: $('#ddlApplicableTo').val(),
                ApplicableDesignationId: applicableDesignationId,
                SkillId: $('#ddlSkills').val().join(),
                SupervisorAssessmentReq: $('#complaint').is(":checked"),
                DeclarationReqforAttendies: $('#employee').is(":checked"),
                BondReq: $('#bond').is(":checked"),
                ActualFileName: $('#hdnfileScopeActualName').val(),
                NewFileName: $('#hdnfileScopeNewName').val(),
                AttachmentPath: $('#hdnfileScopeFileUrl').val(),
                IsActive: IsActiveState
            }
            CommonAjaxMethod(virtualPath + 'CapacityRequest/SaveTrainingType', obj, 'POST', function (response) {
                var url = "/Capacity/TrainingType";
                window.location.href = url;
            });
        }
    }
}


function ClearFormControl() {
    $('#txtTrainingTypeName').val('Enter')
    $('#txtTrainingDesc').val('Enter')
    $('#ddlApplicableTo').Text('Select'),
        $('#ddlSkills').Text('Select')
    $('#divApplicabledesignation').Text('Select')
    $('#hdnfileScopeActualName').val('');
    $('#hdnfileScopeNewName').val('');
    $('#hdnfileScopeFileUrl').val('');
    //$('#hdnSelectDesinations').val('');
    //$('#hdnSelectSkills').val('');
}
function EditTrainingTypeById() {
    if (TraingTypeID == "") {
        return false;
    }

    CommonAjaxMethod(virtualPath + 'CapacityRequest/GetTrainingType', { id: TraingTypeID }, 'GET', function (response) {

        var data = response.data.data.Table;
        IsActiveState = data[0].IsActive;
        $('#txtTrainingTypeName').val(data[0].TrainingTypeName);
        $('#txtTrainingDesc').val(data[0].TrainingDesc);
        $('#ddlApplicableTo').val(data[0].ApplicableToId).trigger("change");
        //$('#ddlSkills').val(data[0].SkillId).trigger("change");
        $('#hdnfileScopeActualName').val(data[0].ActualFileName);
        $('#hdnfileScopeNewName').val(data[0].NewFileName),
            $('#hdnfileScopeFileUrl').val(data[0].AttachmentPath)
        if (data[0].AttachmentPath != null) {
            var path = data[0].AttachmentPath.substring(1, data[0].AttachmentPath.length);
            $('#linkAttachement').attr("href", virtualPath + path);
        }

        $('#lblAttachement').text(data[0].ActualFileName);
        if (data[0].ActualFileName != null && divDeleteAttach != '') {
            document.getElementById('divDeleteAttach').style.display = "block";
        }
        else {
            document.getElementById('divDeleteAttach').style.display = "none";
        }
        if (data[0].BondReq == true) {
            $('#bond').attr('checked', true);
            $("#AddPassport").show();
        }
        else {
            $('#bond').attr('checked', false);
            $("#AddPassport").hide();
        }
        if (data[0].SupervisorAssessmentReq == false) {
            //$("#complaint").prop('checked', "off");
            $('#complaint').attr('checked', false);
        }
        else {
            $('#complaint').attr('checked', true);
        }
        if (data[0].DeclarationReqforAttendies == false) {
            $('#employee').attr('checked', false);
        }
        else {
            $('#employee').attr('checked', true);
        }
        if (data[0].ApplicableDesignationId != null && data[0].ApplicableDesignationId != '') {
            let pdTagArray = data[0].ApplicableDesignationId.split(',');
            $("#ddlApplicableDesignations").select2({
                multiple: true,
            });
            $('#ddlApplicableDesignations').val(pdTagArray).trigger('change');
        }
        if (data[0].SkillId != null && data[0].SkillId != '') {
            let pdTagArray1 = data[0].SkillId.split(',');
            $("#ddlSkills").select2({
                multiple: true,
            });
            $('#ddlSkills').val(pdTagArray1).trigger('change');
        }
    });
}

function UploadFileScope() {
    var fileUpload = $("#fileUpload").get(0);

    var files = fileUpload.files;
    if (files.length > 0) {

        // Create FormData object
        var fileData = new FormData();

        // Looping over all files and add it to FormData object
        for (var i = 0; i < files.length; i++) {
            fileData.append(files[i].name, files[i]);
        }

        $.ajax({
            url: virtualPath + 'CommonMethod/UploadCapacityDocument',
            type: "POST",
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            data: fileData,

            success: function (response) {
                var result = JSON.parse(response);

                if (result.ErrorMessage == "") {
                    $('#hdnfileScopeActualName').val(result.FileModel.ActualFileName);
                    $('#hdnfileScopeNewName').val(result.FileModel.NewFileName);
                    $('#hdnfileScopeFileUrl').val(result.FileModel.FileUrl);
                }
                else {

                    FailToaster(result.ErrorMessage);

                    //document.getElementById('hReturnMessage').innerHTML = result.ErrorMessage;
                    //$('#btnShowModel').click();
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
        FailToaster("Please select file to attach!");
        //document.getElementById('hReturnMessage').innerHTML = "Please select file to attach!";
        //$('#btnShowModel').click();
    }
}

function DeleteAttachment() {
    var url = $("#hdnfileScopeFileUrl").val();
    if (url != '') {
        var result = confirm("Are you sure want to delete this attach file?");
        if (result) {
            CommonAjaxMethod(virtualPath + 'CommonMethod/DeleteFile', { FileUrl: url }, 'POST', function (response) {
                $('#hdnfileScopeActualName').val('');
                $('#hdnfileScopeNewName').val('');
                $('#hdnfileScopeFileUrl').val('');
                var obj = {
                    Id: TraingTypeID,
                    TrainingTypeName: $('#txtTrainingTypeName').val(),
                    TrainingDesc: $('#txtTrainingDesc').val(),
                    ApplicableToId: $('#ddlApplicableTo').val(),
                    ApplicableDesignationId: $('#ddlApplicableDesignations').val().join(),
                    SkillId: $('#ddlSkills').val().join(),
                    SupervisorAssessmentReq: $('#complaint').is(":checked"),
                    DeclarationReqforAttendies: $('#employee').is(":checked"),
                    BondReq: $('#bond').is(":checked"),
                    ActualFileName: '',
                    NewFileName: '',
                    AttachmentPath: ''
                }
                //CommonAjaxMethod(virtualPath + 'CapacityRequest/SaveTrainingType', obj, 'POST', function (response) {
                //    window.location.reload();
                //});
                $('#fileUpload').val('');
                document.getElementById('lblAttachement').innerText = '';
            });
        }
    }
    else {
        FailToaster("File is not uploaded");
    }

}
function DownloadFile() {
    var fileURl = $('#hdnfileScopeFileUrl').val();
    var fileName = $('#hdnfileScopeActualName').val();
    if (fileURl != '' && fileURl != null) {
        var stSplitFileName = fileName.split(".");
        var link = document.createElement("a");
        link.download = stSplitFileName[0];
        link.href = fileURl;
        link.click();
    }
    else {
        alert('Please attach file.');
    }

}

function CheckTrainingTypeDetailsExists(obj) {
    var ObjURL = $(obj).closest("div").find(".hdnRecordExit");
    if ($(obj).val() != "") {
        var dataObject = JSON.stringify({
            'ID': loggedinUserid,
            'Doctype': $(ObjURL).attr("tag-value"),
            'Value': $(obj).val(),

        });
        $.ajax({
            url: "/CommonAjax/CapacityTrainingTypeRecordExist",
            type: "Post",
            contentType: 'application/json',
            data: dataObject,
            success: function (data) {
                if (!data.Status) {

                    $(obj).val('');
                    $(obj).closest("div").find('.field-validation-valid').html(data.SuccessMessage).removeClass('color-green').addClass('text-danger')
                    document.getElementById('lblAddTrainingNameConfirmation').innerHTML = data.SuccessMessage;
                    $('#sptxtTrainingTypeName').addClass("text-danger");
                }
                else {
                    $(obj).closest("div").find('.field-validation-valid').html(data.SuccessMessage).removeClass('text-danger').addClass('color-green');
                    document.getElementById('lblAddTrainingNameConfirmation').innerHTML = data.SuccessMessage;
                    $('#sptxtTrainingTypeName').addClass("color-green");
                }
            },
            error: function (er) {
                alert(er);
            }
        });
    }
}
