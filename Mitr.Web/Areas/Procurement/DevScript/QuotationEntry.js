$(document).ready(function ()
{
    BindProcureRequest();    
});

function BindProcureRequest() {
    CommonAjaxMethod(virtualPath + 'ProcurementRequest/BindQuatationEntry', null, 'GET', function (response) {
        var dataPending = response.data.data.Table;
        var dataArroved = response.data.data.Table1;
        var dataResubmitted = response.data.data.Table2;        
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
                    data: null, render: function (data, type, row)
                    {
                        var strReturn = "";
                        if (row.ContractType == 'Consultant')
                        {
                            if (row.DataFrom == 1)
                            {
                                strReturn = '<a href="QuotationEntryPendingConsultantRFP?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }
                            else if (row.DataFrom == 2)
                            {
                                strReturn = '<a href="QuotationEntryPendingConsultant?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';
                            }
                        }
                        else if (row.ContractType == 'Sub-grant')
                        {
                            if (row.DataFrom == 1)
                            {
                                strReturn = '<a href="QuotationEntryPendingSubgrantRFP?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';
                            }
                            else if (row.DataFrom == 2)
                            {
                                strReturn = '<a href="QuotationEntryPendingSubgrant?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }
                        }
                        else
                        {
                            if (row.DataFrom == 1)
                            {
                                strReturn = '<a href="QuotationEntryPendingRFP?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }
                            else if (row.DataFrom == 2)
                            {
                                strReturn = '<a href="QuotationEntryPending?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }

                          }

                        return strReturn;
                    }
                }


            ]
        });
        
        
        $('#tblProcess').DataTable({
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
                        var strReturn = "";
                        if (row.ContractType == 'Consultant') {
                            if (row.DataFrom == 1) {
                                strReturn = '<a href="QuotationEntryPendingConsultantRFP?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }
                            else if (row.DataFrom == 2) {
                                strReturn = '<a href="QuotationEntryPendingConsultant?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';
                            }
                        }
                        else if (row.ContractType == 'Sub-grant') {
                            if (row.DataFrom == 1) {
                                strReturn = '<a href="QuotationEntryPendingSubgrantRFP?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';
                            }
                            else if (row.DataFrom == 2) {
                                strReturn = '<a href="QuotationEntryPendingSubgrant?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }
                        }
                        else {
                            if (row.DataFrom == 1) {
                                strReturn = '<a href="QuotationEntryPendingRFP?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }
                            else if (row.DataFrom == 2) {
                                strReturn = '<a href="QuotationEntryPending?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }

                        }

                        return strReturn;
                    }
                }


            ]
        });


        $('#tblClose').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": dataResubmitted,
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
                        var strReturn = "";
                        if (row.ContractType == 'Consultant')
                        {
                            if (row.DataFrom == 1) {
                                strReturn = '<a href="QuotationEntryPendingConsultantRFP?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }
                            else if (row.DataFrom == 2)
                            {
                                strReturn = '<a href="QuotationEntryPendingConsultant?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';
                            }
                        }
                        else if (row.ContractType == 'Sub-grant') {
                            if (row.DataFrom == 1) {
                                strReturn = '<a href="QuotationEntryPendingSubgrantRFP?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';
                            }
                            else if (row.DataFrom == 2) {
                                strReturn = '<a href="QuotationEntryPendingSubgrant?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }
                        }
                        else {
                            if (row.DataFrom == 1) {
                                strReturn = '<a href="QuotationEntryPendingRFP?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }
                            else if (row.DataFrom == 2) {
                                strReturn = '<a href="QuotationEntryPending?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }

                        }

                        return strReturn;
                    }
                }


            ]
        });


      

    });
}