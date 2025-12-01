$(document).ready(function ()
{




    $(function () {
        $('.datepicker').datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: "dd-mm-yy",
            yearRange: "-20:+10"
        });

    });

    var obj = {
        ParentId: 0,
        masterTableType: DropDownTypeEnum.SubCategory,
        isMasterTableType: false,
        isManualTable: false,
        manualTable: 0, 
        manualTableId: 0
    }
    LoadMasterDropdown('ddlSubCategory', obj, 'Select', false);

    var obj1 = {
        ParentId: 0,
        masterTableType: DropDownTypeEnum.RiskType,
        isMasterTableType: false,
        isManualTable: false,
        manualTable: 0,
        manualTableId: 0
    }
    LoadMasterDropdown('dldRisk', obj1, 'Select', false);

    LoadMasterDropdown('ddlDoer', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.EmployeeWithoutLWD,
        manualTableId: 0
    }, 'Select', false);

    LoadMasterDropdown('ddlsecDoer', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.EmployeeWithoutLWD,
        manualTableId: 0
    }, 'Select', false);

    LoadMasterDropdown('PC', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.EmployeeWithoutLWD,
        manualTableId: 0
    }, 'Select', false);

    LoadMasterDropdown('PC2', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.EmployeeWithoutLWD,
        manualTableId: 0
    }, 'Select', false);

    LoadMasterDropdown('EscalateTo', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.EmployeeWithoutLWD,
        manualTableId: 0
    }, '', false);



    if (EditId > 0) {
        GetComplianceDocNo(EditId);
        EditComplianceMaster(EditId);
    }
    else {
        GetComplianceDocNo(0);
    }

    
});


function ChangeCategory(ctrl) {

    HideErrorMessage(ctrl);

    var subId = ctrl.value;

    if (subId != 'Select') {
        CommonAjaxMethod(virtualPath + 'ComplianceTransaction/ChangeCategory', { id: subId }, 'GET', function (response) {

            var data = response.data.data.Table;
            $('#Category').val(data[0].CategoryId)
            $('#cate').html(data[0].Category);

        });
    }
    else {
        $('#cate').html('Select');
    }

}

function ChangeRiskType(ctrl) {

    HideErrorMessage(ctrl);

    var subId = ctrl.value;

    if (subId != 'Select') {
        CommonAjaxMethod(virtualPath + 'ComplianceTransaction/ChangeRiskType', { id: subId }, 'GET', function (response) {
            var data = response.data.data.Table;
            $('#description').html(data[0].description);
            $('#vh').show();

        });
    }
    else {
        $('#vh').hide();

    }

}

function PCChange(ctrl) {
    HideErrorMessage(ctrl);
    $('.ra_yes').hide();
    $('#PC').removeClass('Mandatory');
    $("#PC").val('Select').trigger('change');
    $("#PC2").val('Select').trigger('change');
    $('#spPC').hide();
    if ($(ctrl).val() == 'ra_yes') {
        $('#PC').addClass('Mandatory');
    }
    $('.' + $(ctrl).val()).show();
}
function CheckForOneTime(ctrl)
{   
    $('#dueDate').val('');
    if (ctrl.value == "1") {
        $('#divDueDate').show();
        
    }
    else {
        $('#divDueDate').hide();
    }
}

function EditComplianceMaster(id) {

    CommonAjaxMethod(virtualPath + 'ComplianceTransaction/GetComplianceTransById', { id: id }, 'GET', function (response) {
        console.log(response);
        var data = response.data.data.Table;
        var subtask = response.data.data.Table1;
        var conditions = response.data.data.Table2;
        var documents = response.data.data.Table3;
        var JustifyRemarks = response.data.data.Table4;
        var MaxVersion = response.data.data.Table5;
        var complianceMaster = data[0];


        $('#saveCompliance').show();
       
        var button = document.getElementById('saveCompliance');
        button.innerHTML = '<i class="fas fa-save"></i>Update ';

            $('.heading-one').html('Update Compliances');
            $('#versionTxt').css('display', 'block');
            $('#hdnMaxVersionID').val(MaxVersion[0].VersionID);
            $('#start').html(MaxVersion[0].VersionID);
            $('#end').html(MaxVersion[0].VersionID);
            $('.ver_next').addClass('isDisabled');
            $('.ver_prev').removeClass('isDisabled');
            if (MaxVersion[0].VersionID == 1) {
                $('.ver_prev').addClass('isDisabled');
            }


            $('#prevVersion').val(MaxVersion[0].VersionID - 1);
      
            $('#Doc_no').html(formatNumber(complianceMaster.Doc_No));
            $('#Doc_Date').html(ChangeDateFormatToddMMYYY(complianceMaster.Doc_Date));  
            $('#hdnComplianceMasterId').val(complianceMaster.id);
            $('#ddlSubCategory').val(complianceMaster.SubCategary).trigger('change');
            $('#Category').val(complianceMaster.Categary);
            $('#ddlCompType').val(complianceMaster.ComplianceType);
            $("#subsubcate").val(complianceMaster.SubSubCategary);
            $('#dldRisk').val(complianceMaster.RiskType).trigger('change');
            $('#ComplianceName').val(complianceMaster.ComplianceName);
            $('#LeadDay').val(complianceMaster.LeadDay);
            $('#LeadHour').val(complianceMaster.LeadHour);
            $('#LeadMin').val(complianceMaster.LeadMin);
        $('#startDate').val(ChangeDateFormatToddMMYYY(complianceMaster.EffectiveDate));
            $('#isApproval').val('ra_' + (complianceMaster.IsApproval ? 'yes' : 'no')).trigger('change');
            $('#isEscalation').val('er_' + (complianceMaster.IsEscalation ? 'yes' : 'no')).trigger('change');
            $('#ddlDoer').val(complianceMaster.Doer).trigger('change');
            $('#DepartmentId').val(complianceMaster.Department);
            $('#ddlsecDoer').val(complianceMaster.SecDoer == 0 ? 'Select' : complianceMaster.SecDoer).trigger('change');
            $('#PC').val(complianceMaster.ProcessController1 == 0 ? 'Select' : complianceMaster.ProcessController1).trigger('change');
            $('#PC2').val(complianceMaster.ProcessController2 == 0 ? 'Select' : complianceMaster.ProcessController2).trigger('change');
            $('#isChecklist').val('subtask_' + (complianceMaster.IsChecklist ? 'yes' : 'no')).trigger('change');
        $('#ClosureType').val(complianceMaster.ClosureType);
        if (complianceMaster.IsEscalation) {
            $('#EscalateTo').val(complianceMaster.EscalateTo.split(',')).trigger('change');
        }
            $('#EscalationTime').val(complianceMaster.EscalationTime);
            $('#ReminderTime').val(complianceMaster.ReminderTime);

        if (complianceMaster.ComplianceType == 1) {
            $('#ddlFrequency').val('Select').trigger('change');
            $('#ddlFrequency').prop('disabled', true);
            $('#ddlFrequency').removeClass('Mandatory');
            $('#dayofM').removeClass('Mandatory');
            $('#dayofY').removeClass('Mandatory');
        }

        $('#startDate').prop('disabled', true); 
        $('#ddlCompType').prop('disabled', true);
        $('#dueDate').val(ChangeDateFormatToddMMYYY(complianceMaster.DueOn));
        var freq = complianceMaster.Frequency;

        if (freq == 1) {
            $('#ddlFrequency').val('weekly_1').trigger('change');
            $('#ddlFrequency').prop('disabled', true);
            $('#' + complianceMaster.FrequencyCont).prop('checked', true);
            $('input[name="weekly"]').attr('disabled', true);
        }

        if (freq == 2) {
            $('#ddlFrequency').val('months_2').trigger('change');
            $('#ddlFrequency').prop('disabled', true);
            $('#checkAllone').prop('disabled', true);

            var arr = complianceMaster.FrequencyCont.split(',');
            $('#dayofM').val(complianceMaster.DayOF);
            $('#dayofM').prop('disabled', true);

            for (var i = 0; i < arr.length; i++) {
                $('#M_' + arr[i]).prop('checked', true);
            }
            $('.itemCheckboxone').prop('disabled', true);
        }

        if (freq == 3) {
            $('#ddlFrequency').val('yearly_3').trigger('change');
            $('#ddlFrequency').prop('disabled', true);
            var id = complianceMaster.FrequencyCont
            $('#dayofY').val(complianceMaster.DayOF);
            $('#dayofY').prop('disabled', true);
           
            $('#Y_' + id).prop('checked', true); 
            $('input[name="year"]').attr('disabled', true);
        }



        for (var i = 0; i < subtask.length; i++) {
            var id = i + 1;
            if (i == 0) {

                $("#subTaskName-" + id).val(subtask[i].Subtask_Name);
                $("#responseType-" + id).val(subtask[i].SubTaskType + "_" + id).trigger('change');
                if (subtask[i].IsMandatory == true) {
                    $("#mandatory-" + id).prop('checked', true);
                }
            }
            else {
                addSubTaskRow();
                $("#subTaskName-" + id).val(subtask[i].Subtask_Name);
                $("#responseType-" + id).val(subtask[i].SubTaskType + "_" + id).trigger('change');
                if (subtask[i].IsMandatory == true) {
                    $("#mandatory-" + id).prop('checked', true);
                }
            }
        }

        for (var i = 0; i < conditions.length; i++) {
            var id = i + 1;
            if (i == 0) {
                $("#conditionInput-" + id).val(conditions[i].Condition);
            }
            else {
                addConditionRow();
                $("#conditionInput-" + id).val(conditions[i].Condition);

            }
        }

        for (var i = 0; i < documents.length; i++) {
            var id = i + 1;
            if (i == 0) {
                $('#hdnUploadActualFileName_' + id).val(documents[i].ActualFileName);
                $('#hdnUploadNewFileName_' + id).val(documents[i].ActualFileName);
                $('#hdnUploadFileUrl_' + id).val(documents[i].FileURL);

                $('#imageDescription-' + id).val(documents[i].Description);

                if (documents[i].FileURL != "" || documents[i].FileURL != null) {
                    $('#imageDescription-' + id).addClass('Mandatory');
                }

            }
            else {
                addImageRow();
                $('#hdnUploadActualFileName_' + id).val(documents[i].ActualFileName);
                $('#hdnUploadNewFileName_' + id).val(documents[i].ActualFileName);
                $('#hdnUploadFileUrl_' + id).val(documents[i].FileURL);
                $('#imageDescription-' + id).val(documents[i].Description);

                if (documents[i].FileURL != "" || documents[i].FileURL != null) {
                    $('#imageDescription-' + id).addClass('Mandatory');
                }
            }
        }

        if (JustifyRemarks.length == 0) {
            var element = `  <label>Justification in case of updation in compliance<sup>*</sup></label>
                <textarea name="" placeholder="Enter" id="remarks_1" onchange="HideErrorMessage(this)" class="form-control Mandatory h-100"></textarea>
                <span id="spremarks_1" class="text-danger field-validation-error" style="display:none;">Hey, You missed this field!!</span>`;

            $('#Justremarks').append(element);
        }

        else {
            for (var i = 0; i < JustifyRemarks.length; i++) {
                var id = i + 2;

                var element = `<label>Justification in case of updation in compliance<sup>*</sup></label>
                <textarea name="" placeholder="Enter" id="remarks_${id}" class="form-control  h-100"></textarea>`; 
              

                $('#Justremarks').append(element);

             
                    $("#remarks_" + id).val(JustifyRemarks[i].Description);
                    $("#remarks_" + id).prop('disabled', true);

            }

            var element = `  <label>Justification in case of updation in compliance<sup>*</sup></label>
                <textarea name="" placeholder="Enter" id="remarks_1" onchange="HideErrorMessage(this)" class="form-control Mandatory h-100"></textarea>
                <span id="spremarks_1" class="text-danger field-validation-error" style="display:none;">Hey, You missed this field!!</span>`;

            $('#Justremarks').append(element);
        }

        
    });
}

function ChangePrevCompliance() {

    ChangeComplianceMasterVersion(EditId, $('#prevVersion').val());
}

function ChangeNextCompliance() {
    if ($('#nextVersion').val() != $('#hdnMaxVersionID').val()) {
        ChangeComplianceMasterVersion(EditId, $('#nextVersion').val());
    }
    else {
        clearFormControl();
        EditComplianceMaster(EditId);
    }
}


function ChangeComplianceMasterVersion(id , version) {
    CommonAjaxMethod(virtualPath + 'ComplianceTransaction/ChangeComplianceVersion', { id: id, version: version }, 'GET', function (response) {
        console.log(response);
        var data = response.data.data.Table;
        var subtask = response.data.data.Table1;
        var conditions = response.data.data.Table2;
        var documents = response.data.data.Table3;
        var JustifyRemarks = response.data.data.Table4;
        var Version = response.data.data.Table5;
        var complianceMaster = data[0];

        clearFormControl();
        $('#saveCompliance').hide();
        if (Version[0].VersionID != $('#hdnMaxVersionID').val()) {
            $('.ver_next').removeClass('isDisabled');
            $('#nextVersion').val(Version[0].VersionID + 1);


        }
        else {
            $('.ver_next').addClass('isDisabled');
        }
        if (Version[0].VersionID == 1) {
            $('.ver_prev').addClass('isDisabled');
            $('#start').html('1');
        }
        else {
            $('#prevVersion').val(Version[0].VersionID - 1);
            $('.ver_prev').removeClass('isDisabled');
            $('#start').html(Version[0].VersionID);
        }
       
         
        $('.heading-one').html('Update Compliances');
        $('#versionTxt').css('display', 'block');
        
        $('#dueDate').val(ChangeDateFormatToddMMYYY(complianceMaster.DueOn));
        $('#Doc_no').html(formatNumber(complianceMaster.Doc_No));
        $('#Doc_Date').html(ChangeDateFormatToddMMYYY(complianceMaster.Doc_Date));
        $('#hdnComplianceMasterId').val(complianceMaster.id);
        $('#ddlSubCategory').val(complianceMaster.SubCategary).trigger('change');
        $('#Category').val(complianceMaster.Categary);
        $('#ddlCompType').val(complianceMaster.ComplianceType);
        $("#subsubcate").val(complianceMaster.SubSubCategary);
        $('#dldRisk').val(complianceMaster.RiskType).trigger('change');
        $('#ComplianceName').val(complianceMaster.ComplianceName);
        $('#LeadDay').val(complianceMaster.LeadDay);
        $('#LeadHour').val(complianceMaster.LeadHour);
        $('#LeadMin').val(complianceMaster.LeadMin);
        $('#startDate').val(ChangeDateFormatToddMMYYY(complianceMaster.EffectiveDate));
        $('#isApproval').val('ra_' + (complianceMaster.IsApproval ? 'yes' : 'no')).trigger('change');
        $('#isEscalation').val('er_' + (complianceMaster.IsEscalation ? 'yes' : 'no')).trigger('change');
        $('#ddlDoer').val(complianceMaster.Doer).trigger('change');
        $('#DepartmentId').val(complianceMaster.Department);
        $('#ddlsecDoer').val(complianceMaster.SecDoer == 0 ? 'Select' : complianceMaster.SecDoer ).trigger('change');
        $('#PC').val(complianceMaster.ProcessController1 == 0 ? 'Select' : complianceMaster.ProcessController1).trigger('change');
        $('#PC2').val(complianceMaster.ProcessController2 == 0 ? 'Select' : complianceMaster.ProcessController2).trigger('change');
        $('#isChecklist').val('subtask_' + (complianceMaster.IsChecklist ? 'yes' : 'no')).trigger('change');
        $('#ClosureType').val(complianceMaster.ClosureType);
        if (complianceMaster.IsEscalation) {
            $('#EscalateTo').val(complianceMaster.EscalateTo.split(',')).trigger('change');
        }
        $('#EscalationTime').val(complianceMaster.EscalationTime);
        $('#ReminderTime').val(complianceMaster.ReminderTime);

        if (complianceMaster.ComplianceType == 1) {
            $('#ddlFrequency').val('Select').trigger('change');
            $('#ddlFrequency').prop('disabled', true);
            $('#ddlFrequency').removeClass('Mandatory');
            $('#dayofM').removeClass('Mandatory');
            $('#dayofY').removeClass('Mandatory');
        }

        $('#startDate').prop('disabled', true);
        $('#ddlCompType').prop('disabled', true);

        var freq = complianceMaster.Frequency;

        if (freq == 1) {
            $('#ddlFrequency').val('weekly_1').trigger('change');
            $('#ddlFrequency').prop('disabled', true);
            $('#' + complianceMaster.FrequencyCont).prop('checked', true);
            $('input[name="weekly"]').attr('disabled', true);
        }

        if (freq == 2) {
            $('#ddlFrequency').val('months_2').trigger('change');
            $('#ddlFrequency').prop('disabled', true);
            $('#checkAllone').prop('disabled', true);

            var arr = complianceMaster.FrequencyCont.split(',');
            $('#dayofM').val(complianceMaster.DayOF);
            $('#dayofM').prop('disabled', true);

            for (var i = 0; i < arr.length; i++) {
                $('#M_' + arr[i]).prop('checked', true);
            }
            $('.itemCheckboxone').prop('disabled', true);
        }

        if (freq == 3) {
            $('#ddlFrequency').val('yearly_3').trigger('change');
            $('#ddlFrequency').prop('disabled', true);
            var id = complianceMaster.FrequencyCont
            $('#dayofY').val(complianceMaster.DayOF);
            $('#dayofY').prop('disabled', true);

            $('#Y_' + id).prop('checked', true);
            $('input[name="year"]').attr('disabled', true);
        }

      

        for (var i = 0; i < subtask.length; i++) {
            var id = i + 1;
            if (i == 0) {

                $("#subTaskName-" + id).val(subtask[i].Subtask_Name);
                $("#responseType-" + id).val(subtask[i].SubTaskType + "_" + id).trigger('change');
                if (subtask[i].IsMandatory == true) {
                    $("#mandatory-" + id).prop('checked', true);
                }
            }
            else {
                addSubTaskRow();
                $("#subTaskName-" + id).val(subtask[i].Subtask_Name);
                $("#responseType-" + id).val(subtask[i].SubTaskType + "_" + id).trigger('change');
                if (subtask[i].IsMandatory == true) {
                    $("#mandatory-" + id).prop('checked', true);
                }
            }
        }

        for (var i = 0; i < conditions.length; i++) {
            var id = i + 1;
            if (i == 0) {
                $("#conditionInput-" + id).val(conditions[i].Condition);
            }
            else {
                addConditionRow();
                $("#conditionInput-" + id).val(conditions[i].Condition);

            }
        }

        for (var i = 0; i < documents.length; i++) {
            var id = i + 1;
            if (i == 0) {
                $('#hdnUploadActualFileName_' + id).val(documents[i].ActualFileName);
                $('#hdnUploadNewFileName_' + id).val(documents[i].ActualFileName);
                $('#hdnUploadFileUrl_' + id).val(documents[i].FileURL);

                $('#imageDescription-' + id).val(documents[i].Description);

                if (documents[i].FileURL != "" || documents[i].FileURL != null) {
                    $('#imageDescription-' + id).addClass('Mandatory');
                }

            }
            else {
                addImageRow();
                $('#hdnUploadActualFileName_' + id).val(documents[i].ActualFileName);
                $('#hdnUploadNewFileName_' + id).val(documents[i].ActualFileName);
                $('#hdnUploadFileUrl_' + id).val(documents[i].FileURL);
                $('#imageDescription-' + id).val(documents[i].Description);

                if (documents[i].FileURL != "" || documents[i].FileURL != null) {
                    $('#imageDescription-' + id).addClass('Mandatory');
                }
            }
        }

        

       
            for (var i = 0; i < JustifyRemarks.length; i++) {
                var id = i + 2;


                var element = `<label>Justification in case of updation in compliance<sup>*</sup></label>
                <textarea name="" placeholder="Enter" id="remarks_${id}" class="form-control  h-100"></textarea>`;


                $('#Justremarks').append(element);


                $("#remarks_" + id).val(JustifyRemarks[i].Description);
                $("#remarks_" + id).prop('disabled', true);

            }


    });
}



function ChangeDepartment(ctrl) {

    HideErrorMessage(ctrl);

    var subId = ctrl.value;

    if (subId != 'Select') {
        CommonAjaxMethod(virtualPath + 'ComplianceTransaction/GetDepartment', { id: subId }, 'GET', function (response) {
            var data = response.data.data.Table;
            $('#DepartmentId').val(data[0].DepartmentID);
            $('#Department').html(data[0].Department);

        });
    }
}

function ClearFormControl() {
   
    $('#txtSubCate').val('');
    $('#hdnMasterTableId').val('');
    $('#hdnCateId').val('');
    

}



function GetComplianceDocNo(id)
{
    ClearFormControl();
    CommonAjaxMethod(virtualPath + 'ComplianceTransaction/GetComplianceDocNo', { id: id }, 'GET', function (response)
    {
        console.log(response);
        var data = response.data.data.Table;
        var Doc_No = formatNumber(data[0].Doc_No);
        $('#Doc_no').html(Doc_No);
        $('#Doc_Date').html(ChangeDateFormatToddMMYYY(data[0].TaskDate));
        
    });
   
    
}
//function Activate(id)
//{
  
//    var x = confirm("Do you want to change the status of this record?");

//    if (x)
//    { 
//        var obj = {
//            ID: 0,
//            SubCategory: '',
//            TableType: '',
//            Code: '',
//            MasterTableId: id,
//            IsActive: 1,
//            IPAddress: $('#hdnIP').val()?"":" "    
//        }
//        CommonAjaxMethod(virtualPath + 'DigitalLibrary/SaveSubCategory', obj, 'POST', function (response) {
//            BindSubcategory();
//            ClearFormControl();
//        });
//    }
//}


var Week;

function validateRadioButtons() {
    var radioButtons = document.getElementsByName('weekly');
    var isSelected = false;

    for (var i = 0; i < radioButtons.length; i++) {
        if (radioButtons[i].checked) {
            isSelected = true;
            Week = radioButtons[i].id;
            break;
        }
    }

    if (!isSelected) {
        $('#spWeekly').html('Please select atleast a week!!');
        $('#spWeekly').show();
        return false;
    }
    else {
        $('#spWeekly').hide();
    }
    return true;
}


    document.getElementById('checkAllone').addEventListener('change', function() {
        var checkboxes = document.querySelectorAll('.itemCheckboxone');
        checkboxes.forEach(function(checkbox) {
            checkbox.checked = this.checked;
        }, this);
    });


var Month = [];
    function validateMonthForm() {
        var dayOfInput = document.getElementById('dayofM');
        var dayOfValue = parseInt(dayOfInput.value);
        var isDayOfValid = true;

        // Month-Day Mapping
        var monthDayLimits = {
            'M_1': 31, 'M_2': 28, 'M_3': 31, 'M_4': 30,
            'M_5': 31, 'M_6': 30, 'M_7': 31, 'M_8': 31,
            'M_9': 30, 'M_10': 31, 'M_11': 30, 'M_12': 31
        };

        var monthDayName = {
            'M_1': 'Jan', 'M_2': 'Feb', 'M_3': 'Mar', 'M_4': 'Apr',
            'M_5': 'May', 'M_6': 'June', 'M_7': 'Jul', 'M_8': 'Aug',
            'M_9': 'Sept', 'M_10': 'Oct', 'M_11': 'Nov', 'M_12': 'Dec'
        };
        
        // Get selected months
        var checkboxes = document.querySelectorAll('.itemCheckboxone');
        var selectedMonths = [];

        Month = [];
        checkboxes.forEach(function (checkbox) {
            if (checkbox.checked) {
                selectedMonths.push(checkbox.id);
                var id = checkbox.id.split('_')[1];
                Month.push(id);
            }
        });

        var isChecked = selectedMonths.length > 0;


        if (dayOfValue === "" || dayOfValue <= 0) {
            document.getElementById('spdayofM').innerText = 'Invalid no.of Days!!';
            document.getElementById('spdayofM').style.display = 'inline';
            isDayOfValid = false;
        } else if (isDayOfValid) {
            document.getElementById('spdayofM').style.display = 'none';
        }

        if (!isChecked) {
            document.getElementById('spdayofM').innerText = 'Please select atleast one month';
            document.getElementById('spdayofM').style.display = 'inline';
            isDayOfValid = false;
        } else {
            document.getElementById('spdayofM').innerText = 'Invalid no.of Days!!';
            document.getElementById('spdayofM').style.display = 'none';

        }




        // Validate Day of Month according to selected months
        selectedMonths.forEach(function(month) {
            if (dayOfValue > monthDayLimits[month]) {
                isDayOfValid = false;
                document.getElementById('spdayofM').innerText = `Day of should not be greater than ${monthDayLimits[month]} for ${monthDayName[month]}.`;
                document.getElementById('spdayofM').style.display = 'inline';
            }
        });


        // Validate if at least one month is selected
      

        if (checkboxes && isDayOfValid) {

            return true;
        } else {

            return false;
        }
}

function formatNumber(value) {
    // Check if the number is an integer
    if (Number.isInteger(value)) {
        return value.toFixed(1); // Append .0 to integer values
    }
    return value.toString(); // Leave decimal values as they are
}



var Year;
    function validateYearForm() {
        var dayOfInput = document.getElementById('dayofY');
        var dayOfValue = parseInt(dayOfInput.value);
        var isDayOfValid = true;

        // Month-Day Mapping
        var monthDayLimits = {
            'Y_1': 31, 'Y_2': 28, 'Y_3': 31, 'Y_4': 30,
            'Y_5': 31, 'Y_6': 30, 'Y_7': 31, 'Y_8': 31,
            'Y_9': 30, 'Y_10': 31, 'Y_11': 30, 'Y_12': 31
        };

    

        // Get selected month
        var selectedMonth = document.querySelector('input[name="year"]:checked');
        var selectedMonthId = selectedMonth ? selectedMonth.id : null;
        Year = selectedMonth ? selectedMonth.id.split('_')[1] : null;

        // Validate Day of field is not empty or less than or equal to 0
        if (!dayOfValue || dayOfValue <= 0) {
            document.getElementById('spdayofY').innerText = 'Invalid no.of Days!!';
            document.getElementById('spdayofY').style.display = 'inline';
            isDayOfValid = false;
        } else if (isDayOfValid) {
            document.getElementById('spdayofY').style.display = 'none';
        }

        // Validate if a month is selected
        if (!selectedMonth) {
            document.getElementById('spdayofY').innerText = 'Please select atleast one month!!';
            document.getElementById('spdayofY').style.display = 'inline';
        } else {
            document.getElementById('spdayofY').innerText = 'Invalid no.of Days!!';
            document.getElementById('spdayofY').style.display = 'none';
        }

        // Validate Day of Month according to the selected month
        if (selectedMonthId && dayOfValue > monthDayLimits[selectedMonthId]) {
            isDayOfValid = false;
            document.getElementById('spdayofY').innerText = `Day of should not be greater than ${monthDayLimits[selectedMonthId]} for the selected month.`;
            document.getElementById('spdayofY').style.display = 'inline';
        }

       

        if (selectedMonth && isDayOfValid) {
          
            return true;
        } else {

            return false;
        }
    }



var rowIdCounter = 1;

function addSubTaskRow() {
    rowIdCounter++;
    var rowId = "row-" + rowIdCounter;
    var subTaskNameId = "subTaskName-" + rowIdCounter;
    var responseTypeId = "responseType-" + rowIdCounter;
    var mandatoryId = "mandatory-" + rowIdCounter;

    var newRow = `
        <tr id="${rowId}">
            <td><input type="text" id="${subTaskNameId}" value="" class="form-control Mandatory"  onchange="HideErrorMessage(this)"  Mandatory" placeholder="Enter" name="subTaskName[]">
             <span id="sp${subTaskNameId}" class="text-danger field-validation-error" style="display:none;">Hey, You missed this field!!</span>
            </td>
            <td>
                <select class="form-control applyselect Mandatory"  onchange="HideErrorMessage(this)"  id="${responseTypeId}" name="responseType[]">
                    <option >Select</option>
                    <option value="Text_${rowIdCounter}">Text</option>
                    <option value="Numeric_${rowIdCounter}">Numeric</option>
                    <option value="Attachment_${rowIdCounter}">Attachment</option>
                </select>
                <span id="sp${responseTypeId}" class="text-danger field-validation-error" style="display:none;">Hey, You missed this field!!</span>
            </td>
            <td>
                <div class="switch--box">
                    <label class="cswitch m-0">
                        <input class="cswitch--input" id="${mandatoryId}" type="checkbox" name="mandatory[]"/><span class="cswitch--trigger wrapper"></span>
                    </label>
                </div>
            </td>
            <td class="text-center"><i class="fas fa-window-close red-clr" data-toggle="tooltip" title="Remove" aria-hidden="true" onclick="removeSubTaskRow('${rowId}', this)"></i></td>
        </tr>
    `;

    $('#subTaskTableBody').append(newRow);
    $("#" + responseTypeId).select2();  // Apply select2 to the new select element
    $('[data-toggle="tooltip"]').tooltip();    
}

function removeSubTaskRow(rowId, ctrl) {
    $(ctrl).tooltip('dispose');
    $('#' + rowId).remove();
}


// this will remove all tasks at once
// this will remove all tasks at once
function removeAllSubTaskRows() {
    for (var i = 2; i <= rowIdCounter; i++) {
        var rowId = 'row-' + i;
        var rowElement = document.getElementById(rowId);

        if (rowElement) {
            $(rowElement).tooltip('dispose');
            rowElement.remove();
        }
    }
}

var objTaskQuestion = [];
function gatherTableData() {


    for (var i = 1; i <= rowIdCounter; i++) {
        var subTaskNameElement = document.getElementById('subTaskName-' + i);
        var responseTypeElement = document.getElementById('responseType-' + i);
        var mandatoryElement = document.getElementById('mandatory-' + i);

        if (subTaskNameElement && responseTypeElement && mandatoryElement) {

            var obj = {
                SubtaskName: subTaskNameElement.value,
                SubTaskType: responseTypeElement.value.split('_')[0],
                IsMandatory: mandatoryElement.checked
            }
            objTaskQuestion.push(obj);

        }
    }

}

// Function to remove a row from the table
function removeRow(rowId) {
    var row = document.getElementById(rowId);
    row.remove();
}

var conditionRowCounter = 1;

// Function to add a new row
function addConditionRow() {
    conditionRowCounter++;
    var rowId = "conditionRow-" + conditionRowCounter;
    var inputId = "conditionInput-" + conditionRowCounter;

    var newRow = `
        <tr id="${rowId}">
            <td><input type="text" id="${inputId}" class="form-control conditionInput" placeholder="Enter Condition" name="conditions[]"></td>
            <td class="text-center"><i class="fas fa-window-close red-clr" data-toggle="tooltip" title="Remove" aria-hidden="true" onclick="removeConditionRow('${rowId}', this)"></i></td>
        </tr>
    `;

    $('#conditionsTableBody').append(newRow);

    $('[data-toggle="tooltip"]').tooltip();

}

// Function to remove a row

function removeAllConditionRow(rowId, ctrl) {
    for (var i = 1; i <= conditionRowCounter; i++) {
        var rowId = 'conditionRow-' + i;
        var rowElement = document.getElementById(rowId);

        if (rowElement) {
            $(rowElement).tooltip('dispose');
            rowElement.remove();
        }
    }
}

function removeAllSubTaskRowChange() {
    for (var i = 1; i <= rowIdCounter; i++) {
        var rowId = 'row-' + i;
        var rowElement = document.getElementById(rowId);

        if (rowElement) {
            $(rowElement).tooltip('dispose');
            rowElement.remove();
        }
    }
}

function removeAllImageRows() {
    for (var i = 1; i <= imageRowCounter; i++) {
        var rowId = 'imageRow-' + i;
        var rowElement = document.getElementById(rowId);

        if (rowElement) {
            $(rowElement).tooltip('dispose');
            rowElement.remove();
        }
    }
}
function removeConditionRow(rowId, ctrl) {
    $(ctrl).tooltip('dispose');
    $('#' + rowId).remove();
}

// Function to get array of values

var objConditions = [];
function getConditionValues() {

    $('.conditionInput').each(function () {
        var value = $(this).val().trim();
        if (value !== '') {
            var obj = {
                Condition: value,
            }
            objConditions.push(obj);
        }
    });

}

var imageRowCounter = 1;

// Function to add a new row  
function addImageRow(row) {
    imageRowCounter++;
    var rowId = "imageRow-" + imageRowCounter;
    var fileId = "imageFile_" + imageRowCounter;
    var descriptionId = "imageDescription-" + imageRowCounter;

    var newRow = `
        <tr id="${rowId}">
           <td>
                <input type="file" id="${fileId}" class="form-control imageFile" onchange="UploadocumentQuesReport(this.id)"  placeholder="Upload Image" name="imageFiles[]">
                <input type="hidden" id="hdnUploadActualFileName_${imageRowCounter}" />
                <input type="hidden" id="hdnUploadNewFileName_${imageRowCounter}" />
                <input type="hidden" id="hdnUploadFileUrl_${imageRowCounter}" />
                <a id="ancUploadActualFileName_${imageRowCounter}" onclick="DownloadFileQuotation(this)"><i class="fas fa-download float-right" data-toggle="tooltip" title="Download"></i></a>
           </td>
            <td>
                <textarea id="${descriptionId}" class="form-control imageDescription" onchange="HideErrorMessage(this)"  placeholder="Enter Description" name="imageDescriptions[]"></textarea>
                 <span id="sp${descriptionId}" class="text-danger field-validation-error" style="display:none;">Hey, You missed this field!!</span>
            </td>
            <td class="text-center"><i class="fas fa-window-close red-clr" data-toggle="tooltip" title="Remove" aria-hidden="true" onclick="removeImageRow('${rowId}', this)"></i></td>
        </tr>
    `;


    $('#imageTableBody').append(newRow);


    $('[data-toggle="tooltip"]').tooltip();



}

// Function to remove a row
function removeImageRow(rowId, ctrl) {
    $(ctrl).tooltip('dispose');
    $('#' + rowId).remove();

}

// Function to get array of values

var objImageDescrption = [];
function getImageRowValues() {
    var imageRows = [];

    $('.imageFile').each(function (index) {
        var fileId = $(this).attr('id');
        var descriptionId = fileId.replace('imageFile', 'imageDescription');

        var imageFile = $('#hdnUploadFileUrl_' + fileId.split('_')[1]).val().trim();
        var imageDescription = $('#imageDescription-' + fileId.split('_')[1]).val().trim();

        if (imageFile !== '' || imageDescription !== '') {
            var obj = {
                ActualFileName: $('#hdnUploadActualFileName_' + fileId.split('_')[1]).val(),
                NewFileName: $('#hdnUploadNewFileName_' + fileId.split('_')[1]).val(),
                FileURL: $('#hdnUploadFileUrl_' + fileId.split('_')[1]).val(),
                Description: $('#imageDescription-' + fileId.split('_')[1]).val()
            }
            objImageDescrption.push(obj);
        }
    });

    return imageRows;
}

function UploadocumentQuesReport(id) {


    var array = id.split('_');
    var fileUpload = $("#" + id).get(0);

    var files = fileUpload.files;
    if (files.length > 0) {

        // Create FormData object
        var fileData = new FormData();

        // Looping over all files and add it to FormData object
        for (var i = 0; i < files.length; i++) {
            fileData.append(files[i].name, files[i]);
        }

        $.ajax({
            url: virtualPath + 'CommonMethod/UploadComplianceDocument',
            type: "POST",
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            data: fileData,

            success: function (response) {
                var result = JSON.parse(response);

                if (result.ErrorMessage == "") {

                    $('#hdnUploadActualFileName_' + array[1]).val(result.FileModel.ActualFileName);
                    $('#hdnUploadNewFileName_' + array[1]).val(result.FileModel.NewFileName);
                    $('#hdnUploadFileUrl_' + array[1]).val(result.FileModel.FileUrl);
                    $('#imageDescription-' + array[1]).addClass('Mandatory');


                }
                else {


                    FailToaster(result.ErrorMessage);

                }
            },
            error: function (error) {
                FailToaster(error);

                isSuccess = false;
            }

        });
    }
    else {
        $('#hdnUploadActualFileName_' + array[1]).val('');
        $('#hdnUploadNewFileName_' + array[1]).val('');
        $('#hdnUploadFileUrl_' + array[1]).val('');
        $('#imageDescription-' + array[1]).removeClass('Mandatory');
        $('#spimageDescription-' + array[1]).hide();


        return "error";

    }

    return "";


}

function DownloadFileQuotation(ctrl) {
    var id = ctrl.id.split('_');
    var controlNo = id[1];
    var fileURl = $('#hdnUploadFileUrl_' + controlNo).val();
    var fileName = $('#hdnUploadActualFileName_' + controlNo).val();
    if(fileURl.length > 0) {
        if (fileURl != null || fileURl != undefined) {
            var stSplitFileName = fileName.split(".");
            var link = document.createElement("a");
            link.download = stSplitFileName[0];
            link.href = fileURl;
            link.click();
        }
    }
    else {
        FailToaster("Please select file to attach!");;
    }
}


function handleVisibility() {
    var leadHour;
    var leadDay;
    var leadMin;
    var totalSum;
    if ($("#LeadDay").val() == "") {
        $("#LeadDay").val(0);
    }
    if ($("#LeadHour").val() == "") {
        $("#LeadHour").val(0);
    }
    if ($("#LeadMin").val() == "") {
        $("#LeadMin").val(0);
    }
   
    if ($("#ddlFrequency").val() == 'weekly_1') {

        leadDay = parseInt($("#LeadDay").val(), 10);
        leadHour = parseInt($("#LeadHour").val(), 10);
        leadMin = parseInt($("#LeadMin").val(), 10);

        totalSum = leadDay * 24 * 60 + leadHour * 60 + leadMin;

        var totalTime = 7 * 24 * 60;

        if (totalSum > totalTime) {
            return false;
        }

        else {
            return true;
        }

    }
    
    else if ($("#ddlFrequency").val() == 'months_2') {



        leadDay = parseInt($("#LeadDay").val(), 10);
        leadHour = parseInt($("#LeadHour").val(), 10);
        leadMin = parseInt($("#LeadMin").val(), 10);

        totalSum = leadDay * 24 * 60 + leadHour * 60 + leadMin;

        var totalTime = 31 * 24 * 60;

        if (totalSum > totalTime) {
            return false;
        }

        else {
            return true;
        }

    }
    
    else {
        leadDay = parseInt($("#LeadDay").val(), 10);
        leadHour = parseInt($("#LeadHour").val(), 10);
        leadMin = parseInt($("#LeadMin").val(), 10);

        totalSum = leadDay * 24 * 60 + leadHour * 60 + leadMin;

        var totalTime = 366 * 24 * 60;

         if (totalSum > totalTime) {
            
             return false;
        }

         else {
          
             return true;
        }
    }
}


function SaveCompliance() {


    var isValid = true;
    
    if ($('#ddlCompType').val()=="1")
    {
        if ($('#dueDate').val() == "")
        {
            $('#spdueDate').show();            
            isValid = false;
        }

    }
     

    if (checkValidationOnSubmit('Mandatory') == true) {


        var obj = handleVisibility();

        var errormessage = '';

        if (obj == false) {
          
            errormessage = 'The Given Lead day is greater than the Expected time.'; 

            FailToaster(errormessage);
            return;
        }

        objTaskQuestion.length = 0
        objConditions.length = 0;
        objImageDescrption.length = 0;

        gatherTableData();
        getConditionValues();
        getImageRowValues();

        var freq = $('#ddlFrequency').val();

        if (freq != 'Select') {
            var weekId = $('#ddlFrequency').val().split('_')[1];
        }
        else {
            WeekId = 0;
        }
        var isBool;
        if (weekId == 1) {
            isBool = validateRadioButtons();
        }
        else if (weekId == 2) {
            isBool = validateMonthForm();
        }
        else if (weekId == 3) {
            isBool = validateYearForm();
        }
        else {
            isBool = true;
        }

        if (isBool == true) {
            var FrequencyCont;
            var day;
            if (weekId == 1) {
                FrequencyCont = Week;
                day = 0;
            }
            else if (weekId == 2) {
                FrequencyCont = Month.join();
                day = $('#dayofM').val();
            }
            else if (weekId == 3) {
                FrequencyCont = Year;
                day = $('#dayofY').val();
            }
            else {
                FrequencyCont = '';
                day = 0;
            }

            var ComplianceMasterModel = {
                id: $('#hdnComplianceMasterId').val() ? $('#hdnComplianceMasterId').val(): 0,
                Doc_No: $('#Doc_no').html(),          
                DocDate: ChangeDateFormat($('#Doc_Date').html()),         
                SubCategary: $('#ddlSubCategory').val(),     
                Categary: $('#Category').val(),        
                ComplianceType: $('#ddlCompType').val(),  
                SubSubCategary: $("#subsubcate").val(),    
                RiskType: $('#dldRisk').val(),        
                ComplianceName: $('#ComplianceName').val(),    
                Frequency: weekId,       
                DayOF: day,           
                FrequencyCont: FrequencyCont,     
                LeadDay: $('#LeadDay').val(),         
                LeadHour: $('#LeadHour').val(),        
                LeadMin: $('#LeadMin').val(),         
                EffectiveDate: ChangeDateFormat($('#startDate').val()),   
                IsApproval: $('#isApproval').val().split('_')[1] == 'yes'?1: 0,     
                IsEscalation: $('#isEscalation').val().split('_')[1] == 'yes' ? 1 : 0,   
                Doer: $('#ddlDoer').val(),            
                Department: $('#DepartmentId').val(),      
                SecDoer: $('#ddlsecDoer').val(),
                ProcessController1: $('#PC').val(),
                ProcessController2: $('#PC2').val(),
                IsChecklist: $('#isChecklist').val().split('_')[1] == 'yes' ? 1 : 0,    
                ClosureType: $('#ClosureType').val(),     
                EscalateTo: $('#EscalateTo').val().join(),        
                EscalationTime: $('#EscalationTime').val(),  
                ReminderTime: $('#ReminderTime').val(),
                DueDate:ChangeDateFormat($('#dueDate').val()) 
            }

            if (ComplianceMasterModel.IsChecklist == 0) {
                objTaskQuestion = [];
            }

            if (ComplianceMasterModel.id > 0) {
                var objRemark = {
                    Remark: $('#remarks_1').val(),
                }

                var ComplianceModel = {
                    ComplianceMasterModel: ComplianceMasterModel,
                    complianceMasterSubTaskList: objTaskQuestion,
                    ComplianceMasterConditionsList: objConditions,
                    complianceMasterDocumentsList: objImageDescrption,
                    remarkModel: objRemark
                }
            }
            else {
                var ComplianceModel = {
                    ComplianceMasterModel: ComplianceMasterModel,
                    complianceMasterSubTaskList: objTaskQuestion,
                    ComplianceMasterConditionsList: objConditions,
                    complianceMasterDocumentsList: objImageDescrption,
                }
            }
            console.log(ComplianceModel);

            CommonAjaxMethod(virtualPath + 'ComplianceTransaction/SaveComplianceTrans', { objComplianceMaster: ComplianceModel }, 'POST', function (response) {
                if (response.ValidationInput == 1)
                {
                    $('#saveCompliance').hide();
                    ComplianceMaster();

                }

            });

        }

    }
}

function HideComplianceError(id) {
    $('#' + id).hide();
}

function clearFormControl() {
    $('.heading-one').html('Edit Compliances');  // Reset heading to default // Hide version text element

    rowIdCounter = 1;
    imageRowCounter = 1;
    conditionRowCounter = 1;
    // Clear compliance master fields
    $('#hdnComplianceMasterId').val('');
    $('#ddlSubCategory').val('Select').trigger('change');
    $('#Category').val('Select');
    $('#subsubcate').val('');
    $('#dldRisk').val('Select').trigger('change');
    $('#ComplianceName').val('');
    $('#LeadDay').val('');
    $('#LeadHour').val('');
    $('#LeadMin').val('');
    $('#startDate').val('').prop('disabled', false);
    $('#isApproval').val('Select').trigger('change');
    $('#isEscalation').val('Select').trigger('change');
    $('#ddlDoer').val('Select').trigger('change');
    $('#DepartmentId').val('');
    $('#ddlsecDoer').val('Select').trigger('change');
    $('#PC').val('Select').trigger('change');
    $('#PC2').val('Select').trigger('change');
    $('#isChecklist').val('Select').trigger('change');
    $('#ClosureType').val('');
    $('#EscalateTo').val('').trigger('change');
    $('#EscalationTime').val('');
    $('#ReminderTime').val('');

    $('#ddlFrequency').val('Select').prop('disabled', false).trigger('change');
    $('#dayofM').val('').prop('disabled', false);
    $('#dayofY').val('').prop('disabled', false);
    $('input[name="weekly"]').attr('disabled', false).prop('checked', false);
    $('input[name="year"]').attr('disabled', false).prop('checked', false);
    $('#checkAllone').prop('disabled', false);
    $('.itemCheckboxone').prop('disabled', false).prop('checked', false);

  

    var ele = `<tr id="row-1">
        <td><input type="text" id="subTaskName-1" value="" class="form-control Mandatory" placeholder="Enter" onchange="HideErrorMessage(this)" name="subTaskName[]"><span id="spsubTaskName-1" class="text-danger field-validation-error" style="display:none;">Hey, You missed this field!!</span></td>
        <td>
            <select class="form-control applyselect Mandatory" id="responseType-1" onchange="HideErrorMessage(this)" name="responseType[]">
                <option>Select</option>
                <option value="Text_1">Text</option>
                <option value="Numeric_1">Numeric</option>
                <option value="Attachment_1">Attachment</option>
            </select>
            <span id="spresponseType-1" class="text-danger field-validation-error" style="display:none;">Hey, You missed this field!!</span>
        </td>
        <td>
            <div class="switch--box">
                <label class="cswitch m-0">
                    <input class="cswitch--input" type="checkbox" id="mandatory-1" name="mandatory[]"><span class="cswitch--trigger wrapper"></span>
                </label>
            </div>
        </td>
        <td class="text-center">
            <i class="fas fa-window-close" data-toggle="tooltip" title="Remove" aria-hidden="true"></i>
        </td>
    </tr>`;

    $('#subTaskTableBody').html(ele);

    var ele2 = `  <tr id="conditionRow-1">
                            <td><input type="text" id="conditionInput-1" class="form-control conditionInput" placeholder="Enter Condition" name="conditions[]"></td>
                            <td class="text-center"><i class="fas fa-window-close" data-toggle="tooltip" title="Remove" aria-hidden="true"></i></td>
                        </tr>`;




    $('#conditionsTableBody').html(ele2);


    var ele3 = `<tr id="imageRow-1" class="imageRow">
                            <td>
                                <input type="file" id="imageFile_1" class="form-control imageFile" onchange="UploadocumentQuesReport(this.id)" placeholder="Upload Image" name="imageFiles[]"><a id="ancUploadActualFileName_1" onclick="DownloadFileQuotation(this)"><i class="fas fa-download float-right" data-toggle="tooltip" title="Download"></i></a>
                                <input type="hidden" id="hdnUploadActualFileName_1" />
                                <input type="hidden" id="hdnUploadNewFileName_1" />
                                <input type="hidden" id="hdnUploadFileUrl_1" />

                            </td>
                            <td>
                                <textarea id="imageDescription-1" class="form-control imageDescription" onchange="HideErrorMessage(this)" placeholder="Enter Description" name="imageDescriptions[]"></textarea>
                                <span id="spimageDescription-1" class="text-danger field-validation-error" style="display:none;">Hey, You missed this field!!</span>
                            </td>
                            <td class="text-center"><i class="fas fa-window-close" data-toggle="tooltip" title="Remove" aria-hidden="true"></i></td>
                        </tr>`;

    $('#imageTableBody').html(ele3);

    // Clear justification remarks fields
    $('#Justremarks').empty();
   

}

function ClearInvalidNumbers(oTextBox, from) {
    if (from == 1) {
        if (parseInt(oTextBox.value) > 23) {
            oTextBox.value = "";
            oTextBox.focus();
            FailToaster("Invalid hour! Hours must be between 0 and 23.");
        }
    }
    if (from == 2) {
        if (parseInt(oTextBox.value) > 59) {
            oTextBox.value = "";
            oTextBox.focus();
            FailToaster("Invalid minute! Minutes must be between 0 and 59.");
        }
    }

}