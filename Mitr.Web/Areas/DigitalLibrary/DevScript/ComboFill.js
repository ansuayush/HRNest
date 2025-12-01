

function LoadMasterDropdown(element, obj, slectText, basedonValueCode, selectedValue) {
    CommonAjaxMethod(virtualPath + 'CommonMethod/GetDropdown/', obj, 'GET', function (response) {
        var dropdownMasterData = response.data.data.Table;
        //render catagory
        BindDropDown(element, selectedValue, dropdownMasterData, slectText, basedonValueCode);
    });
}

function LoadMasterDropdownValueCode(element, obj, slectText, basedonValueCode, selectedValue) {
    CommonAjaxMethod(virtualPath + 'CommonMethod/GetDropdown/', obj, 'GET', function (response) {
        var dropdownMasterData = response.data.data.Table;
        //render catagory
        BindDropDownWithValueCode(element, selectedValue, dropdownMasterData, slectText, basedonValueCode);
    });
}
function BindDropDown(element, selectedValue, dropdownMasterData, slectText, basedonValueCode) {

    var $ele = $('#' + element);
    $ele.empty();
    if (slectText != "") {
        $ele.append($('<option/>').val(slectText).text(slectText));
    }

    $.each(dropdownMasterData, function (ii, vall) {
        if (basedonValueCode == true) {
            $ele.append($('<option dataEle=' + vall.ID + ' />').val(vall.ValueCode).text(vall.ValueName));

        }
        else
            $ele.append($('<option/>').val(vall.ID).text(vall.ValueName));
    })
    var ddlselectValue = selectedValue == undefined ? "" : selectedValue;
    if (ddlselectValue != "")
        $ele.val(selectedValue);
}


function BindDropDownWithValueCode(element, selectedValue, dropdownMasterData, slectText, basedonValueCode) {

    var $ele = $('#' + element);
    $ele.empty();
    if (slectText != "") {
        $ele.append($('<option/>').val(slectText).text(slectText));
    }
    $.each(dropdownMasterData, function (ii, vall) {
        if (basedonValueCode == true) {
            $ele.append($('<option dataEle=' + vall.ValueCode + ' />').val(vall.ID).text(vall.ValueName));

        }
        else
            $ele.append($('<option/>').val(vall.ID).text(vall.ValueName));
    })
    var ddlselectValue = selectedValue == undefined ? "" : selectedValue;
    if (ddlselectValue != "")
        $ele.val(selectedValue);
}

function LoadUlLi(element, obj, type) {
    CommonAjaxMethod(virtualPath + 'CommonMethod/GetDropdown/', obj, 'GET', function (response) {
        var dropdownMasterData = response.data.data.Table;
        //render catagory
        BindUlLi(element, dropdownMasterData, type);
    });
}


function BindUlLi(element, data, type) {
    var $ele = $('#' + element);
    $ele.empty();

    var groupName = "";
    if (type == 2) {
        groupName = "Thematic";
    }
    else if (type == 3) {
        groupName = "MasterLocation";
    }
    else if (type == 4) {
        groupName = "Donar";
    }
    else if (type == 5) {
        groupName = "TagWithThematic";
    }
    else if (type == 6) {
        groupName = "Year";
    }

    $.each(data, function (ii, vall) {
        $ele.append($('<li> <input onchange="AddFilters(' + vall.ID + ',' + type + ')" type="checkbox" id="pc' + groupName + vall.ID + '" name="' + groupName + '"> <label for="pc' + groupName + vall.ID + '">' + vall.ValueName + ' </label></li>'));
    })
}

function LoadUlLiProject(element, obj, type, projCode) {
    CommonAjaxMethod(virtualPath + 'CommonMethod/GetDropdown/', obj, 'GET', function (response) {
        var dropdownMasterData = response.data.data.Table;
        //render catagory
        BindUlLiProject(element, dropdownMasterData, type, projCode);
    });
}
function BindUlLiProject(element, data, type, projCode) {
    var $ele = $('#' + element);
    $ele.empty();
    var groupName = "Project";
    $.each(data, function (ii, vall) {
        if (vall.ID == projCode) {
            $ele.append($('<li> <input checked onchange="AddFilters(' + vall.ID + ',' + type + ')" type="checkbox" id="pc' + groupName + vall.ID + '" name="' + groupName + '"> <label for="pc' + groupName + vall.ID + '">' + vall.ValueCode + ' </label></li>'));

        }
        else {
            $ele.append($('<li> <input  onchange="AddFilters(' + vall.ID + ',' + type + ')" type="checkbox" id="pc' + groupName + vall.ID + '" name="' + groupName + '"> <label for="pc' + groupName + vall.ID + '">' + vall.ValueCode + ' </label></li>'));

        }

    })
}

// Add by Brajesh
function LoadMasterDropdownValueCodeWithOutDefaultValue(element, obj, slectText, basedonValueCode, selectedValue) {
    CommonAjaxMethod(virtualPath + 'CommonMethod/GetDropdown/', obj, 'GET', function (response) {
        var dropdownMasterData = response.data.data.Table;
        //render catagory
        BindDropDownWithValueCodeAndWithOutDefaultValue(element, selectedValue, dropdownMasterData, slectText, basedonValueCode);
    });
}
function BindDropDownWithValueCodeAndWithOutDefaultValue(element, selectedValue, dropdownMasterData, slectText, basedonValueCode) {

    var $ele = $('#' + element);
    $ele.empty();
    //if (slectText != "") {
    //    $ele.append($('<option/>').val(slectText).text(slectText));
    //}
    $.each(dropdownMasterData, function (ii, vall) {
        if (basedonValueCode == true) {
            $ele.append($('<option dataEle=' + vall.ValueCode + ' />').val(vall.ID).text(vall.ValueName));

        }
        else
            $ele.append($('<option/>').val(vall.ID).text(vall.ValueName));
    })
    var ddlselectValue = selectedValue == undefined ? "" : selectedValue;
    if (ddlselectValue != "")
        $ele.val(selectedValue);
}
