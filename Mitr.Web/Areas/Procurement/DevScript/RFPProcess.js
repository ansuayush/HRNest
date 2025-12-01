$(document).ready(function ()
{
    BindProcureRequest();    
});

function BindProcureRequest() {
    CommonAjaxMethod(virtualPath + 'ProcurementRequest/BindRFPProcess', null, 'GET', function (response) {
        var dataPending = response.data.data.Table;
        var dataArroved = response.data.data.Table1;       
        var datalive = response.data.data.Table2;
        var dataRejected = response.data.data.Table3;      
        $('#tblPending').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": dataPending,
            "paging": true,
            "info": false,
            "order": [],

            "columns": [



                { "data": "RowNum" },


                { "data": "Req_No" },
                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {

                        var strReturn = ChangeDateFormatToddMMYYY(row.Req_Date);
                        return strReturn;
                    }
                },
                { "data": "Req_ByName" },
                { "data": "POCName" },
                { "data": "ProjectCode" },
                { "data": "ContractType" },
                { "data": "RequiredAmount", "className": "text-right" },
                { "data": "Status" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {

                        var strReturn = '<a href="RFPProcessPending?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';
                        return strReturn;
                    }
                }


            ]
        });

        $('#tblUnderprocess').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": dataArroved,
            "paging": true,
            "info": false,
            "order": [],

            "columns": [



                { "data": "RowNum" },


                { "data": "Req_No" },
                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {

                        var strReturn = ChangeDateFormatToddMMYYY(row.Req_Date);
                        return strReturn;
                    }
                },
                { "data": "Req_ByName" },
                { "data": "POCName" },
                { "data": "ProjectCode" },
                { "data": "ContractType" },
                { "data": "RequiredAmount", "className": "text-right" },
                { "data": "Status" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {
                        var strReturn = '';
                        if (row.ActualStatus == '11')
                        {
                            strReturn = '<a href="ModuleAdminPendingProcessed?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';


                        }
                        else {
                            strReturn = '<a href="RFPProcessPending?id=' + row.Id + ' "><i class="fas fa-eye"></i>View</a>';

                        }
                    
                        return strReturn;

                        
                    }
                }

                   
            ]
        });

      
        $('#tbllive').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": datalive,
            "paging": true,
            "info": false,
            "order": [],

            "columns": [



                { "data": "RowNum" },


                { "data": "Req_No" },
                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {

                        var strReturn = ChangeDateFormatToddMMYYY(row.Req_Date);
                        return strReturn;
                    }
                },
                { "data": "Req_ByName" },
                { "data": "POCName" },
                { "data": "ProjectCode" },
                { "data": "ContractType" },
                { "data": "RequiredAmount", "className": "text-right" },
                { "data": "Status" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {

                        var strReturn = '<a href="RPFProcesslive?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';
                        return strReturn;
                    }
                }


            ]
        });
        $('#tblRejected').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": dataRejected,
            "paging": true,
            "info": false,
            "order": [],

            "columns": [



                { "data": "RowNum" },


                { "data": "Req_No" },
                {
                    "orderable": true,
                    data: null, render: function (data, type, row) {

                        var strReturn = ChangeDateFormatToddMMYYY(row.Req_Date);
                        return strReturn;
                    }
                },
                { "data": "Req_ByName" },
                { "data": "POCName" },
                { "data": "ProjectCode" },
                { "data": "ContractType" },
                { "data": "RequiredAmount", "className": "text-right" },
                { "data": "Status" },
                {
                    "orderable": false,
                    data: null, render: function (data, type, row) {

                        var strReturn = '<a href="RFPProcessPending?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';
                        return strReturn;
                    }
                }


            ]
        });

    });
}