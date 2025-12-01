$(document).ready(function ()
{
  //testing for checking
    $(function () {
        $('.datepicker').datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: "dd-mm-yy",
            yearRange: "-90:+10"
        });

    });

    LoadMasterDropdown('ddlLocation',
        {
            ParentId: 0,
            masterTableType: 0,
            isMasterTableType: false,
            isManualTable: true,
            manualTable: ManaulTableEnum.MasterLocation,
            manualTableId: 0
        }, 'Select', false);

    LoadMasterDropdown('ddlMember', {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.EmployeeWithoutLWD,
        manualTableId: 0
    }, 'Select', false);

    BindProcureCommittee();

    if ($('#hdnMasterTableId').val() == "0" || $('#hdnMasterTableId').val() == "")
    {
        CommonAjaxMethod(virtualPath + 'ProcureCommittee/GetMaxReqNo', null, 'GET', function (response) {
            $('#ReqNo').val(response.data.data.Table[0].ReqNo);
        });
        var dt = new Date();
        var newDate = ChangeDateFormatToddMMYYY(dt);
        $('#ReqDate').val(newDate);
       
    } 
    
    
});

var commonArray = [];
var IdPd = 0;


var LocationcommonArray = [];
var LocationIdPd = 0;

function ClearOnAddMember() {
    LoadMasterDropdown('ddlMember',
        {
            ParentId: 0,
            masterTableType: 0,
            isMasterTableType: false,
            isManualTable: true,
            manualTable: ManaulTableEnum.EmployeeWithoutLWD,
            manualTableId: 0
        }, 'Select', false);
    $('#tbStartDate').val('');
    $('#tbEndDate').val('');
}
function ClearOnAddLocation() {
    LoadMasterDropdown('ddlLocation',
        {
            ParentId: 0,
            masterTableType: 0,
            isMasterTableType: false,
            isManualTable: true,
            manualTable: ManaulTableEnum.MasterLocation,
            manualTableId: 0
        }, 'Select', false);
    $('#txtRemark').val('');
    
}
 
function AddLocation() {
    if (checkValidationOnSubmit('Mandatory2') == true)
    {
        var ValidData = LocationcommonArray.filter(function (itemParent) { return (itemParent.LocationId == $("#ddlLocation").val()); });
        if (ValidData.length > 0)
        {
            alert("Location already added.")
        }
        else {
            var ctrl = document.getElementById("ddlLocation");
            LocationIdPd = LocationIdPd + 1;
            var loop = LocationIdPd;
            var objarrayinner =
            {
                ProcureCommitteeId: loop,
                LocationName: ctrl.options[ctrl.selectedIndex].text,
                LocationId: $("#ddlLocation").val(),               
                Remark:  $("#txtRemark").val()

            }
            LocationcommonArray.push(objarrayinner);

            var newtbblData = "<tr><td>" + objarrayinner.LocationName + "</td><td>" + objarrayinner.Remark + "</td><td><a title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='DeleteMSMEArray(this," + objarrayinner.ProcureCommitteeId + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr>";

            $("#tbllocation").find('tbody').append(newtbblData);

           
            ClearOnAddLocation();

            $("#spddlLocation1").hide();
        }
    }
}
function DeleteMSMEArray(obj, id) {

    ConfirmMsgBox("Are you sure want to delete", '', function () {

        $(obj).closest('tr').remove();
        LocationcommonArray = LocationcommonArray.filter(function (itemParent) { return (itemParent.ProcureCommitteeId != id); });

    })
}
function AddMember()
{    
    if (checkValidationOnSubmit('Mandatory1') == true)
    {
       var ValidData = commonArray.filter(function (itemParent) { return (itemParent.CommitteeMember == $("#ddlMember").val()); });
        if (ValidData.length > 0) {
            alert("Member already added.")
        }
        else
        {
            var ctrl = document.getElementById("ddlMember");
            IdPd = IdPd + 1;
            var loop = IdPd;
            var objarrayinner =
            {
                ProcureCommitteeId: loop,
                Member: ctrl.options[ctrl.selectedIndex].text,
                CommitteeMember: $("#ddlMember").val(),
                StartDate: $("#tbStartDate").val(),
                EndDate: $("#tbEndDate").val(),
                TenureStartDate: ChangeDateFormat($("#tbStartDate").val()),
                TenureEndDate: ChangeDateFormat($("#tbEndDate").val())

            }
            commonArray.push(objarrayinner);

            $('#tblProjects').DataTable({
                "processing": true, // for show progress bar           
                "destroy": true,
                "data": commonArray,
                "paging": false,
                "info": false,
                "columns": [
                    { "data": "Member" },
                    { "data": "StartDate" },
                    { "data": "EndDate" },
                    {
                        "orderable": false,
                        data: null, render: function (data, type, row) {

                            var strReturn = "<a title='Click to Remove' data-toggle='tooltip' data-original-title='Click to DeActivate' class='AIsActive'  onclick='deleteRows(" + row.ProcureCommitteeId + ")' ><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'  ></i> </a>";


                            return strReturn;
                        }
                    }


                ]
            });

            ClearOnAddMember();
        }
    }
}
function BindLocationGrid(locationArray) {

    $("#tbllocation tr").filter(function (index) {
        return index >= 2;
    }).remove();
    for (var i = 0; i < locationArray.length; i++)
    {
        var newtbblData = "<tr><td>" + locationArray[i].LocationName + "</td><td>" + locationArray[i].Remark + "</td><td><a title='Click to Remove' data-toggle='tooltip' data-original-title='Click to Remove' onclick='DeleteMSMEArray(this," + locationArray[i].ProcureCommitteeId + ")'><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'></i> </a></td></tr>";

        $("#tbllocation").find('tbody').append(newtbblData);
    }
     
    
}
function BindDocumentGrid(commonArray)
{
    $('#tblProjects').DataTable({
        "processing": true, // for show progress bar           
        "destroy": true,
        "data": commonArray,
        "paging": false,
        "info": false,
        "columns": [
            { "data": "Member" },
            { "data": "StartDate" },
            { "data": "EndDate" },
            {
                "orderable": false,
                data: null, render: function (data, type, row) {

                    var strReturn = "<a title='Click to Remove' data-toggle='tooltip' data-original-title='Click to DeActivate' class='AIsActive'  onclick='deleteRows(" + row.ProcureCommitteeId + ")' ><i class='fas fa-window-close m-0 red-clr' aria-hidden='true'  ></i> </a>";


                    return strReturn;
                }
            }


        ]
    });
}
function deleteRows(rowId) {

    commonArray = commonArray.filter(function (itemParent) { return (itemParent.ProcureCommitteeId != rowId); });
    BindDocumentGrid(commonArray);


}
function deleteRowsLocation(rowId) {

    LocationcommonArray = LocationcommonArray.filter(function (itemParent) { return (itemParent.ProcureCommitteeId != rowId); });
    BindLocationGrid(commonArray);


}

function SaveProcureCommittee()
{
    var isValidData = true;
    var CommitteeDataList = [];
    if (LocationcommonArray.length == 0)
    {
        $('#spddlLocation1').show();
        isValidData = false;
    }
    else {
        $('#spddlLocation1').hide();
    }
    if (checkValidationOnSubmit('Mandatory') == true && isValidData==true)
    {
        for (var i = 0; i < commonArray.length; i++)
        {
            for (var j = 0; j < LocationcommonArray.length; j++)
            {
                var objData = {
                    MinProcureValue: $('#minProcure').val(),
                    MaxProcureValue: $('#maxProcure').val(),
                    LocationId: LocationcommonArray[j].LocationId,
                    MemberId: commonArray[i].CommitteeMember

                }
                CommitteeDataList.push(objData);
            }
              
        }
        const stringRepresentation = JSON.stringify(LocationcommonArray, null, 2);
        var ProcureCommitteeModel =
        {
            ID: $('#hdnMasterTableId').val(),                  
            EffectiveDate: ChangeDateFormat($('#EffectiveDate').val()),          
            MinProcureValue: $('#minProcure').val(),
            MaxProcureValue: $('#maxProcure').val(),
            UserId: loggedinUserid,
            IPAddress: $('#hdnIP').val() ? "" : " "   ,
            ProcureCommitteeMemberList: commonArray,
            ProcureCommitteeDataList: CommitteeDataList,
           // ProcureCommitteeLocationList: LocationcommonArray,
            LocationData: stringRepresentation
        }
        CommonAjaxMethod(virtualPath + 'ProcureCommittee/SaveProcureCommittee', ProcureCommitteeModel, 'POST', function (response)
        {
            BindProcureCommittee();
            ClearFormControl();       
            $('#cm').modal('hide')
        });
    }
}
function ClearFormControl()
{
    $('#btnSave').show();
    LoadMasterDropdown('ddlLocation',
        {
            ParentId: 0,
            masterTableType: 0,
            isMasterTableType: false,
            isManualTable: true,
            manualTable: ManaulTableEnum.MasterLocation,
            manualTableId: 0
        }, 'Select', false);
    
    LoadMasterDropdown('ddlMember',
        {
        ParentId: 0,
        masterTableType: 0,
        isMasterTableType: false,
        isManualTable: true,
        manualTable: ManaulTableEnum.EmployeeWithoutLWD,
        manualTableId: 0
    }, 'Select', false);
     
        $('#EffectiveDate').val('');
        $('#minProcure').val('');
        $('#maxProcure').val('');
        $('#tbStartDate').val('');
        $('#tbEndDate').val(''); 
        $('#hdnMasterTableId').val(''); 

    commonArray = [];
    locationArray = [];    
    LocationcommonArray = [];
    BindLocationGrid(LocationcommonArray);
    BindDocumentGrid(commonArray);
    CommonAjaxMethod(virtualPath + 'ProcureCommittee/GetMaxReqNo', null, 'GET', function (response) {
        $('#ReqNo').val(response.data.data.Table[0].ReqNo);
    });
    var dt = new Date();
    var newDate = ChangeDateFormatToddMMYYY(dt);
    $('#ReqDate').val(newDate);
    
}

function BindProcureCommittee()
{
    CommonAjaxMethod(virtualPath + 'ProcureCommittee/BindProcureCommittee', null, 'GET', function (response) {

        $('#tblLive').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": response.data.data.Table,
            "paging": false,
            "info": false,
            "columns": [
                { "data": "RowNum" },
                { "data": "Places" },


                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {
                        return "<label>" + getdatetimewithoutJson(row.EffectiveDate) + "</label>";
                    }
                },
                { "data": "MinProcureValue" },
                { "data": "MaxProcureValue" },

                { "data": "NoOFMembers" },
                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {
                        return "<label>" + getdatetimewithoutJson(row.createdat) + "</label>";
                    }
                },

                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {
                        return "<label>" + getdatetimewithoutJson(row.modifiedat) + "</label>";
                    }
                },


                { "data": "Status" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {
                        var strReturn = "";
                        //Click to DeActivate                     
                        //Click to Activate                      
                        if (row.Status == "Active") {
                            strReturn = "<a title='Click to DeActivate' data-toggle='tooltip' data-original-title='Click to DeActivate' class='AIsActive'  onclick='Activate(" + row.ID + ")' ><i class='fa fa-check-circle checkgreen' aria-hidden='true'  ></i> </a><a title='Edit' data-toggle='modal' data-target='#cm'  onclick='EditSubCate(" + row.ID + ",2)' ><i class='fas fa-edit checkgreen' aria-hidden='true' ></i> </a> ";
                        }
                        if (row.Status == "Deactive") {
                            strReturn = "<a title='Click to Activate' data-toggle='tooltip' data-original-title='Click to Activate' class='AIsActive' onclick='Activate(" + row.ID + ")' ><i class='fa fa-times-circle crossred' aria-hidden='true'  ></i> </a><a title='Edit' data-toggle='modal' data-target='#cm'  onclick='EditSubCate(" + row.ID + ",2)' ><i class='fas fa-edit checkgreen' aria-hidden='true' ></i> </a> ";
                        }

                        return strReturn;
                    }
                }

            ]
        });
        $('#tblLegacy').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": response.data.data.Table1,
            "paging": false,
            "info": false,
            "columns": [
                { "data": "RowNum" },
                { "data": "Places" },
              

                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {
                        return "<label>" + getdatetimewithoutJson(row.EffectiveDate) + "</label>";
                    }
                },
                { "data": "MinProcureValue" },
                { "data": "MaxProcureValue" },

                { "data": "NoOFMembers" },
                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {
                        return "<label>" + getdatetimewithoutJson(row.createdat) + "</label>";
                    }
                },

                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {
                        return "<label>" + getdatetimewithoutJson(row.modifiedat) + "</label>";
                    }
                },

             
                { "data": "Status" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {
                        var strReturn = "";
                        //Click to DeActivate                     
                        //Click to Activate                      
                     
                            strReturn = "<a title='View' data-toggle='modal' data-target='#cm'  onclick='EditSubCate(" + row.ID + ",1)' ><i class='fas fa-edit checkgreen' aria-hidden='true' ></i> </a> ";
                       

                        return strReturn;
                    }
                }

            ]
        });
    });
   
}

function EditSubCate(id,type)
{
    ClearFormControl();
    if (type == 1) {

        $('#btnSave').hide();
        
    }
    else {
        $('#btnSave').show();
       
    }
    CommonAjaxMethod(virtualPath + 'ProcureCommittee/GetProcureCommittee', { id: id }, 'GET', function (response)
    {  
     
        var data = response.data.data.Table;
        var data2 = response.data.data.Table1;
        var data3 = response.data.data.Table2;
       // $('#txtCate').show();
        $('#hdnMasterTableId').val(id); 
        $('#EffectiveDate').val(ChangeDateFormatToddMMYYY(data[0].EffectiveDate));   
        $('#minProcure').val(data[0].MinProcureValue); 
        $('#maxProcure').val(data[0].MaxProcureValue);         
        $('#ReqNo').val(data[0].Doc_No);
        $('#ReqDate').val(ChangeDateFormatToddMMYYY(data[0].Doc_CreationDate));
      
        commonArray = [];
        for (var i = 0; i < data2.length; i++)
        {
            IdPd = data2[0].MaxId;
            var objarrayinner =
            {
                ProcureCommitteeId: data2[i].ProcureCommitteeId,
                Member: data2[i].Member,
                CommitteeMember: data2[i].CommitteeMember,               
                StartDate: ChangeDateFormatToddMMYYY(data2[i].TenureStartDate),
                EndDate: ChangeDateFormatToddMMYYY(data2[i].TenureEndDate),
                TenureStartDate: data2[i].TenureStartDate,
                TenureEndDate: data2[i].TenureEndDate

            }
            commonArray.push(objarrayinner);
        }
        LocationcommonArray = [];
        for (var i = 0; i < data3.length; i++) {
            LocationIdPd = data3[0].MaxId;
            var objarrayinner =
            {                
                ProcureCommitteeId: data3[i].ProcureCommitteeId,
                LocationName: data3[i].location_name,
                LocationId: data3[i].LocationId,
                Remark: data3[i].Remark
            }
            LocationcommonArray.push(objarrayinner);
        }
        BindDocumentGrid(commonArray);
        BindLocationGrid(LocationcommonArray);
       
    });
   
    
}
function Activate(id)
{
    ConfirmMsgBox("Are you sure want to deactivate this record", '', function () {


        var ProcureCommitteeModel = {
            ID: id,
            IsDelete: 1,
            Locations: '',
            MinProcureValue: '',
            MaxProcureValue: '',
            UserId: loggedinUserid,
            IPAddress: $('#hdnIP').val() ? "" : " ",
            ProcureCommitteeMemberList: []
        }

        CommonAjaxMethod(virtualPath + 'ProcureCommittee/SaveProcureCommittee', ProcureCommitteeModel, 'POST', function (response) {
            BindProcureCommittee();
            ClearFormControl();
        });

    })
    

    
}


