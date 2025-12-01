$(document).ready(function () {
    alert('Hi');
   
    //if (TabId == '1' || TabId == '3') {
    //    $('#tab-C').click();
    //}
    //if (TabId == '2') {
    //    $('#tab-A').click();
    //}
    //if (TabId == '4') {
    //    $('#tab-B').click();
    //}
    //if (TabId == '5') {
    //    $('#tab-D').click();
    //}
    BindContent();

    // $('#emUploadedBy').val(loggedinUserName);
});

function BindContent() {
   
    CommonAjaxMethod(virtualPath + 'EmployeeSalary/GetBonusPaymentEntryList', null, 'GET', function (response) {

        var dataPending = response.data.data.Table;     
       
        $('#tblBonusPaymentEntry').DataTable({
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
                { "data": "EmployeeCode" },
                { "data": "EmployeeName" },
                { "data": "Category" },
                { "data": "Designation" },
                { "data": "Location" },
                { "data": "Dateofjoining" },
                { "data": "Entitlement" },
                { "data": "Paid" },
                { "data": "Balance" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {

                        var strReturn = '<a href="#" onclick="ShowContent(' + row.Id + ')"><i class="fas fa-edit"></i>View</a>';
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
                { "data": "Title" },
                { "data": "Tags" },
                { "data": "Status" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {

                        var strReturn = '<a href="#" onclick="ShowContent(' + row.Id + ')" ><i class="fas fa-eye"></i>View</a>';
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
                { "data": "Title" },
                { "data": "Tags" },
                { "data": "Status" },
                { "data": "Reason" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {

                        var strReturn = '<a href="#" onclick="ShowContent(' + row.Id + ')" ><i class="fas fa-eye"></i>View</a>';
                        return strReturn;
                    }
                }


            ]
        });


    });


}