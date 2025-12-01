$(document).ready(function () {


    FillDropdown();


});

function FillDropdown()
{
    LoadMasterDropdown('ddlVendor',
        {
            ParentId: 0,
            masterTableType: 0,
            isMasterTableType: false,
            isManualTable: true,
            manualTable: 14,
            manualTableId: 0
        }, 'All', false);
    LoadMasterDropdown('ddlVendorType',
        {
            ParentId: 0,
            masterTableType: 0,
            isMasterTableType: false,
            isManualTable: true,
            manualTable: 15,
            manualTableId: 0
        }, 'All', false);

    

    var obj3 = {
        ParentId: 0,
        masterTableType: DropDownTypeEnum.ProcurementRelationshipMaster,
        isMasterTableType: false,
        isManualTable: false,
        manualTable: 0,
        manualTableId: 0
    }
    LoadMasterDropdown('ddlRelationshipC3', obj3, 'All', false);

    var obj4 = {
        ParentId: 0,
        masterTableType: DropDownTypeEnum.ProcurementGoodAndServicesCategory,
        isMasterTableType: false,
        isManualTable: false,
        manualTable: 0,
        manualTableId: 0
    }
    LoadMasterDropdown('ddlCategory', obj4, 'All', false);

    LoadMasterDropdown('ddlArea',
        {
            ParentId: 0,
            masterTableType: 0,
            isMasterTableType: false,
            isManualTable: true,
            manualTable: ManaulTableEnum.MasterLocation,
            manualTableId: 0
        }, 'All', false);
}

function SearchData()
{    

    var SearchObject =
    {       
        VendorId: $('#ddlVendor').val(),
        VendorType: $('#ddlVendorType').val(),         
        C3Relation: $('#ddlRelationshipC3').val().join(),
        CategoryGoodsNService: $('#ddlCategory').val().join(),
        Area: $('#ddlArea').val().join()
    }
    CommonAjaxMethod(virtualPath + 'ProcureVendorRegis/VendorSearch', SearchObject, 'POST', function (response) {

        var dt = response.data.data.Table;

        $('#tableVendor').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": dt,
            "paging": true,
            "info": false,
            "order": [],

            "columns": [

                { "data": "Req_No" },
                
                { "data": "VendorName" },
                { "data": "IsEmpaneled" },
                { "data": "Rating" },
                { "data": "History" } ,
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {
                        var strReturn = '<a href="VendorRegistrationSearch?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';
                        return strReturn;
                    }
                }


            ]
        });

         
    }, true);
}

function ClearFilter() {
    FillDropdown();
}