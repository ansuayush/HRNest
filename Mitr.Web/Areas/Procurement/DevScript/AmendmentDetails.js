 
$(document).ready(function () {
    BindAmendmentData();
});
function BindAmendmentData() {
    CommonAjaxMethod(virtualPath + 'ProcurementRequest/BindProjectDetails', { id: RequestId, IsBindLine: 9 }
        , 'GET', function (response) {
            var data1 = response.data.data.Table;

            if (data1[0].NatureOfAmemdment != "") {
                $("#btnAmendSubmut").hide();
                $("#AmendmentDetails").show();

                //  $('#AmendReqNo').text(data1[0].AmendReq_No);
                $('#txtAmendReason').val(data1[0].Remark);
                var strAmendRe = data1[0].NatureOfAmemdment.split(',');
                $("#ddlAmend").select2({
                    multiple: true,
                });
                $('#ddlAmend').val(strAmendRe).trigger('change');

                LoadMasterDropdownValueCode('ddlAmendmentVersion',
                    {
                        ParentId: RequestId,
                        masterTableType: 0,
                        isMasterTableType: false,
                        isManualTable: true,
                        manualTable: 33,
                        manualTableId: 0
                    }, 'Select', true, RequestId);
            }
            else if (data1[0].IsAmendment == 'Y')
            {
                $("#btnAmendSubmut").hide();
                $("#AmendmentDetails").show();

                //  $('#AmendReqNo').text(data1[0].AmendReq_No);
                $('#txtAmendReason').val(data1[0].Remark);
                var strAmendRe = '';
                $("#ddlAmend").select2({
                    multiple: true,
                });
                $('#ddlAmend').val(strAmendRe).trigger('change');

                LoadMasterDropdownValueCode('ddlAmendmentVersion',
                    {
                        ParentId: RequestId,
                        masterTableType: 0,
                        isMasterTableType: false,
                        isManualTable: true,
                        manualTable: 33,
                        manualTableId: 0
                    }, 'Select', true, RequestId);
            }
            else
            {
                $("#AmendmentDetails").hide();
            }
            

        });
}

