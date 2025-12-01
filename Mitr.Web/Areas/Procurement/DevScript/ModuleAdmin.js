$(document).ready(function ()
{
    BindProcureRequest();    
});

function BindProcureRequest() {
    CommonAjaxMethod(virtualPath + 'ProcurementRequest/BindRFPModuleAdmin', null, 'GET', function (response) {
        var dataPending = response.data.data.Table;
        var dataArroved = response.data.data.Table1;
        var dataRejected = response.data.data.Table2;        
        var  dataLive = response.data.data.Table3;      
        
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
                        var strReturn = "";
                        if (row.ContractType == 'Consultant') {
                            if (row.DataFrom == 1) {
                                strReturn = '<a href="ModuleAdminQuotationConsultantRFP?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }
                            else if (row.DataFrom == 2) {
                                strReturn = '<a href="ModuleAdminQuotationConsultant?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';
                            }
                        }
                        else if (row.ContractType == 'Sub-grant') {
                            if (row.DataFrom == 1) {
                                strReturn = '<a href="ModuleAdminQuotationSubgrantRFP?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';
                            }
                            else if (row.DataFrom == 2) {
                                strReturn = '<a href="ModuleAdminQuotationSubgrant?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }
                        }
                        else {
                            if (row.DataFrom == 1) {
                                strReturn = '<a href="ModuleAdminQuotationRFP?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }
                            else if (row.DataFrom == 2) {
                                strReturn = '<a href="ModuleAdminQuotation?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }

                        }

                        return strReturn;
                    }
                }


            ]
        });


        $('#tblUnderprocessed').DataTable({
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
                                strReturn = '<a href="ModuleAdminQuotationConsultantRFP?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }
                            else if (row.DataFrom == 2) {
                                strReturn = '<a href="ModuleAdminQuotationConsultant?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';
                            }
                        }
                        else if (row.ContractType == 'Sub-grant') {
                            if (row.DataFrom == 1) {
                                strReturn = '<a href="ModuleAdminQuotationSubgrantRFP?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';
                            }
                            else if (row.DataFrom == 2) {
                                strReturn = '<a href="ModuleAdminQuotationSubgrant?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }
                        }
                        else {
                            if (row.DataFrom == 1) {
                                strReturn = '<a href="ModuleAdminQuotationRFP?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }
                            else if (row.DataFrom == 2) {
                                strReturn = '<a href="ModuleAdminQuotation?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }

                        }

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
                        var strReturn = "";
                        if (row.ContractType == 'Consultant') {
                            if (row.DataFrom == 1) {
                                strReturn = '<a href="ModuleAdminQuotationConsultantRFP?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }
                            else if (row.DataFrom == 2) {
                                strReturn = '<a href="ModuleAdminQuotationConsultant?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';
                            }
                        }
                        else if (row.ContractType == 'Sub-grant') {
                            if (row.DataFrom == 1) {
                                strReturn = '<a href="ModuleAdminQuotationSubgrantRFP?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';
                            }
                            else if (row.DataFrom == 2) {
                                strReturn = '<a href="ModuleAdminQuotationSubgrant?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }
                        }
                        else {
                            if (row.DataFrom == 1) {
                                strReturn = '<a href="ModuleAdminQuotationRFP?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }
                            else if (row.DataFrom == 2) {
                                strReturn = '<a href="ModuleAdminQuotation?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }

                        }

                        return strReturn;
                    }
                }


            ]
        });


        $('#tbllive').DataTable({
            "processing": true, // for show progress bar           
            "destroy": true,
            "data": dataLive,
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
                                strReturn = '<a href="ModuleAdminQuotationConsultantRFP?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }
                            else if (row.DataFrom == 2) {
                                strReturn = '<a href="ModuleAdminQuotationConsultant?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';
                            }
                        }
                        else if (row.ContractType == 'Sub-grant') {
                            if (row.DataFrom == 1) {
                                strReturn = '<a href="ModuleAdminQuotationSubgrantRFP?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';
                            }
                            else if (row.DataFrom == 2) {
                                strReturn = '<a href="ModuleAdminQuotationSubgrant?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }
                        }
                        else {
                            if (row.DataFrom == 1) {
                                strReturn = '<a href="ModuleAdminQuotationRFP?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }
                            else if (row.DataFrom == 2) {
                                strReturn = '<a href="ModuleAdminQuotation?id=' + row.Id + '" ><i class="fas fa-eye"></i>View</a>';

                            }

                        }

                        return strReturn;
                    }
                }


            ]
        });

    });
}