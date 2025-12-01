$('.btnaddOtherExpense').on('click', function (e) {
	e.preventDefault();
	e.stopPropagation();
	AddNewRowOtherExpense();
});
$('.btnaddTransport').on('click', function (e) {
	e.preventDefault();
	e.stopPropagation();
	AddNewRowTransport();
	$('.CModeOfTransportDD').bind("change", function () {
		
		var PerKmRate = $(this).closest('tr').find("select.CModeOfTransportDD option:selected").attr("kmrate");
		var TrnasType = $(this).closest('tr').find("select.CModeOfTransportDD option:selected").text();
		if (TrnasType == "Self four wheeler" || TrnasType == "Self two wheeler") {
			$(this).closest('tr').find(".CAmount").attr('readonly', 'readonly');
			$(this).closest('tr').find(".cTrnasKM").val(0).removeAttr('readonly');
			$(this).closest('tr').find(".RateofPerKM").val(PerKmRate).attr('readonly', 'readonly');
		}
		else if (TrnasType == "Hired Taxi" || TrnasType == "Hired two wheeler" || TrnasType =="Hired four Wheeler") {
			$(this).closest('tr').find(".cTrnasKM").val(0).attr('readonly', 'readonly');
			$(this).closest('tr').find(".RateofPerKM").val(0).attr('readonly', 'readonly');
			$(this).closest('tr').find(".CAmount").removeAttr('readonly');
		}


	});
	$('.cTrnasKM').blur(function () {
		
		var PerKmRate = 0, FinalAmount = 0;
		var PerKmRate = $(this).closest('tr').find("select.CModeOfTransportDD option:selected").attr("kmrate");
		var Km = $(this).closest('tr').find(".cTrnasKM").val();
		if (parseInt(Km) > 0) {
			FinalAmount = parseFloat(parseInt(PerKmRate) * parseInt(Km));
		}
		else {
			FirstAmount = parseInt(FinalAmount);
		}

		$(this).closest('tr').find(".CAmount").val(FinalAmount);

		var checkradio = $("#TravelExpense_Expensedistrubute_status").val();
		if (checkradio == "No") {
			CalculateNoDist();
		}
		else {
			CalculateAll();
		}
	});
});

$('.btnAddRowTravelFare').on('click', function (e) {
	e.preventDefault();
	e.stopPropagation();
	AddRowTravelFare();

});
function TravelTransDisable() {
	
	$("#tblTransport TBODY TR").each(function (i) {
		
		var TrnasType = $(this).closest('tr').find("select.CModeOfTransportDD option:selected").text();
		if (TrnasType == "Self four wheeler" || TrnasType == "Self two wheeler") {
			$(this).closest('tr').find(".CAmount").attr('readonly', 'readonly');
			$(this).closest('tr').find(".cTrnasKM").removeAttr('readonly');
			$(this).closest('tr').find(".RateofPerKM").attr('readonly', 'readonly');
		}
		else if (TrnasType == "Hired Taxi" || TrnasType == "Hired two wheeler" || TrnasType == "Hired four Wheeler") {
			$(this).closest('tr').find(".cTrnasKM").attr('readonly', 'readonly');
			$(this).closest('tr').find(".RateofPerKM").attr('readonly', 'readonly');
			$(this).closest('tr').find(".CAmount").removeAttr('readonly');
		}
	});
}
function TravelBPerDiem() {
	var count = 0;
	$("#tblBPerDiem TBODY TR").each(function (i) {

		var PercantageAmount = 0, FinalAmount = 0;
		var percentage = $(this).closest('tr').find("select.BFreemealDD option:selected").attr("percentage");
		var ddltext = $(this).closest('tr').find("select.BFreemealDD option:selected").text();
		var perdiemrate = $(this).closest('tr').find(".spnperdiemrate").html();
		var hdnFirstLast = $(this).closest('tr').find(".hdnFirstLast").val();
		if (ddltext == "leave (100 %)" && count==0) {
		
			var newpercentage = 75;
			var prevpercentage = $(this).closest('tr').prev().find("select.BFreemealDD option:selected").attr("percentage");
			var newperdiemrate = $(this).closest('tr').prev().find(".spnperdiemrate").html();
			var newFirstAmount = parseFloat(newperdiemrate);
			var newPercantageAmount = parseFloat((newFirstAmount * newpercentage) / 100);

			var newprevAmount = parseFloat((newPercantageAmount * prevpercentage) / 100);
			var newFinalAmount = parseFloat(newPercantageAmount) - parseFloat(newprevAmount);
			//$(this).closest('tr').prev().find(".bAmount").val();
			//$(this).closest('tr').prev().find(".bAmount").val(newFinalAmount);
			//$("input[name='[" + (parseInt(LastTRCount) + 1) + "].PerDiem_Amount']").val(0);
			//$("#BPERDIEM_'" + parseInt(count - 1) + "'__Amount").val(newFinalAmount);
			//$("#BPERDIEM_2__Amount").val(newFinalAmount);
			$(this).closest('tr').prev().find(".bAmount").val(0);
			$(this).closest('tr').prev().find(".bAmount").val(newFinalAmount);
			count++;

		}
    
		
		if (parseFloat(hdnFirstLast) > 0) {
			FirstAmount = parseFloat(perdiemrate * parseFloat(hdnFirstLast));
		}
		else {
			FirstAmount = parseFloat(perdiemrate);
		}
		PercantageAmount = parseFloat((FirstAmount * percentage) / 100);
		FinalAmount = parseFloat(FirstAmount - PercantageAmount).toFixed(0);
		$(this).closest('tr').find(".bAmount").val(FinalAmount);
		
	});
}
function AddNewRowTransport() {

	var LastTRCount = parseInt($('#tblTransport').find("tbody tr").length) - 1;

	$('.applyselect').select2("destroy");
	var $tableBody = $('#tblTransport').find("tbody"),
		$trLast = $tableBody.find("tr:last"),
		$trNew = $trLast.clone();
	$trNew.find("td:last").html('<a onclick="DeleteTransport(this)" class="remove"><i class="fas fa-window-close red-clr" aria-hidden="true"></i></a>')


	$trNew.find("label").each(function () {
		$(this).attr({
			'id': function (_, id) { var arr = id.split('_'); return id.replace(arr[1], LastTRCount + 1); },
		});
		$(this).html(parseInt($('#tblTransport').find("tbody tr").length) + 1)
	});

	$trNew.find("select").each(function (i) {
		$(this).attr({
			'id': function (_, name) { return name.replace('_' + LastTRCount + '_', '_' + (parseInt(LastTRCount) + 1) + '_'); },
			'name': function (_, name) { return name.replace('[' + LastTRCount + ']', '[' + (parseInt(LastTRCount) + 1) + ']'); },
		});
		$(this).removeAttr('disabled');

	});
	$trNew.find("input").each(function (i) {
		$(this).attr({
			'id': function (_, name) { return name.replace('_' + LastTRCount + '_', '_' + (parseInt(LastTRCount) + 1) + '_'); },
			'name': function (_, name) { return name.replace('[' + LastTRCount + ']', '[' + (parseInt(LastTRCount) + 1) + ']'); },
		});
		$(this).val('')
		$(this).removeAttr('disabled');
	});
	$trNew.find("textarea").each(function (i) {
		$(this).attr({
			'id': function (_, name) { return name.replace('_' + LastTRCount + '_', '_' + (parseInt(LastTRCount) + 1) + '_'); },
			'name': function (_, name) { return name.replace('[' + LastTRCount + ']', '[' + (parseInt(LastTRCount) + 1) + ']'); },
		});
		$(this).val('')
		$(this).removeAttr('disabled');
	});

	$trNew.find("span").each(function (i) {
		if ($(this).attr("data-valmsg-for")) {
			$(this).attr({
				'data-valmsg-for': function (_, name) { return name.replace('[' + LastTRCount + ']', '[' + (parseInt(LastTRCount) + 1) + ']'); }
			});
		}
		if ($(this).attr("for")) {
			$(this).attr({
				'for': function (_, name) { return name.replace('_' + LastTRCount + '_', '_' + (parseInt(LastTRCount) + 1) + '_'); }
			});
		}
	});
	var dropCount = (parseInt(LastTRCount) + 1);
	$trLast.after($trNew);
	$("#CTRANSPORTATION_" + dropCount + "__TransportId option[value='0']").prop("selected", true);
	var form = $("form");
	form.removeData('validator');
	form.removeData('unobtrusiveValidation');
	$.validator.unobtrusive.parse(form);
	$('[data-toggle="tooltip"]').tooltip();
	$(".applyselect").select2();

}


function DeleteTransport(obj) {
	var count = 0;
	var TotalRowCount = $('#tblTransport').find("tbody tr").length;
	ConfirmMsgBox("Are you sure want to delete", '', function () {

		$(obj).closest('tr').remove();
		$("#tblTransport TBODY TR").each(function (i) {
			$(this).closest("tr").find("label").each(function () {
				$(this).attr({
					'id': function (_, id) { var arr = id.split('_'); return id.replace(arr[1], i); },
				});
				$(this).html(i + 1)
			});


			$(this).closest("tr").find("input").each(function () {
				$(this).attr({
					'id': function (_, id) { return id.replace('_' + (parseInt(i) + 1) + '_', '_' + i + '_'); },
					'name': function (_, name) { return name.replace('[' + (parseInt(i) + 1) + ']', '[' + i + ']'); },
				});

			});



			$(this).closest("tr").find("textarea").each(function () {
				$(this).attr({
					'id': function (_, id) { return id.replace('_' + (parseInt(i) + 1) + '_', '_' + i + '_'); },
					'name': function (_, name) { return name.replace('[' + (parseInt(i) + 1) + ']', '[' + i + ']'); },
				});

			});


			$(this).closest("tr").find("span").each(function () {
				if ($(this).attr("data-valmsg-for")) {
					$(this).attr({
						'data-valmsg-for': function (_, name) { return name.replace('[' + (parseInt(i) + 1) + ']', '[' + i + ']'); },
					});
				}
				if ($(this).attr("for")) {
					$(this).attr({
						'for': function (_, id) { return id.replace('_' + (parseInt(i) + 1) + '_', '_' + i + '_'); },
					});
				}
			});
		});
		var form = $("form");
		form.removeData('validator');
		form.removeData('unobtrusiveValidation');
		$.validator.unobtrusive.parse(form);
		var checkradio = $("#TravelExpense_Expensedistrubute_status").val();
		if (checkradio == "No") {
			CalculateNoDist();
		}
		else {
			CalculateAll();
		}
		
	})

}


function AddNewRowOtherExpense() {
	var LastTRCount = parseInt($('#tblOtherExpense').find("tbody tr").length) - 1;
	$('.applyselect').select2("destroy");
	var $tableBody = $('#tblOtherExpense').find("tbody"),
		$trLast = $tableBody.find("tr:last"),
		$trNew = $trLast.clone();
	$trNew.find("td:last").html('<a onclick="DeleteOtherExpense(this)" class="remove"><i class="fas fa-window-close red-clr" aria-hidden="true"></i></a>')



	$trNew.find("label").each(function () {
		$(this).attr({
			'id': function (_, id) { var arr = id.split('_'); return id.replace(arr[1], LastTRCount + 1); },
		});
		$(this).html(parseInt($('#tblOtherExpense').find("tbody tr").length) + 1)
	});

	$trNew.find("input").each(function (i) {
		$(this).attr({
			'id': function (_, name) { return name.replace('_' + LastTRCount + '_', '_' + (parseInt(LastTRCount) + 1) + '_'); },
			'name': function (_, name) { return name.replace('[' + LastTRCount + ']', '[' + (parseInt(LastTRCount) + 1) + ']'); },
		});
		$(this).val('')
		$(this).removeAttr('disabled');
	});

	$trNew.find("select").each(function (i) {
		$(this).attr({
			'id': function (_, name) { return name.replace('_' + LastTRCount + '_', '_' + (parseInt(LastTRCount) + 1) + '_'); },
			'name': function (_, name) { return name.replace('[' + LastTRCount + ']', '[' + (parseInt(LastTRCount) + 1) + ']'); },
		});
	

	});

	$trNew.find("textarea").each(function (i) {
		$(this).attr({
			'id': function (_, name) { return name.replace('_' + LastTRCount + '_', '_' + (parseInt(LastTRCount) + 1) + '_'); },
			'name': function (_, name) { return name.replace('[' + LastTRCount + ']', '[' + (parseInt(LastTRCount) + 1) + ']'); },
		});
		$(this).val('')
		$(this).removeAttr('disabled');
	});

	$trNew.find("span").each(function (i) {
		if ($(this).attr("data-valmsg-for")) {
			$(this).attr({
				'data-valmsg-for': function (_, name) { return name.replace('[' + LastTRCount + ']', '[' + (parseInt(LastTRCount) + 1) + ']'); }
			});
		}
		if ($(this).attr("for")) {
			$(this).attr({
				'for': function (_, name) { return name.replace('_' + LastTRCount + '_', '_' + (parseInt(LastTRCount) + 1) + '_'); }
			});
		}
	});

	$trLast.after($trNew);
	var form = $("form");
	form.removeData('validator');
	form.removeData('unobtrusiveValidation');
	$.validator.unobtrusive.parse(form);
	$('[data-toggle="tooltip"]').tooltip();
	$(".applyselect").select2();
}


function DeleteOtherExpense(obj) {
	var count = 0;
	var TotalRowCount = $('#tblOtherExpense').find("tbody tr").length;
	ConfirmMsgBox("Are you sure want to delete", '', function () {

		$(obj).closest('tr').remove();
		$("#tblOtherExpense TBODY TR").each(function (i) {
			$(this).closest("tr").find("label").each(function () {
				$(this).attr({
					'id': function (_, id) { var arr = id.split('_'); return id.replace(arr[1], i); },
				});
				$(this).html(i + 1)
			});


			$(this).closest("tr").find("input").each(function () {
				$(this).attr({
					'id': function (_, id) { return id.replace('_' + (parseInt(i) + 1) + '_', '_' + i + '_'); },
					'name': function (_, name) { return name.replace('[' + (parseInt(i) + 1) + ']', '[' + i + ']'); },
				});

			});



			$(this).closest("tr").find("textarea").each(function () {
				$(this).attr({
					'id': function (_, id) { return id.replace('_' + (parseInt(i) + 1) + '_', '_' + i + '_'); },
					'name': function (_, name) { return name.replace('[' + (parseInt(i) + 1) + ']', '[' + i + ']'); },
				});

			});


			$(this).closest("tr").find("span").each(function () {
				if ($(this).attr("data-valmsg-for")) {
					$(this).attr({
						'data-valmsg-for': function (_, name) { return name.replace('[' + (parseInt(i) + 1) + ']', '[' + i + ']'); },
					});
				}
				if ($(this).attr("for")) {
					$(this).attr({
						'for': function (_, id) { return id.replace('_' + (parseInt(i) + 1) + '_', '_' + i + '_'); },
					});
				}
			});
		});
		var form = $("form");
		form.removeData('validator');
		form.removeData('unobtrusiveValidation');
		$.validator.unobtrusive.parse(form);
		var checkradio = $("#TravelExpense_Expensedistrubute_status").val();
		if (checkradio == "No") {
			CalculateNoDist();
		}
		else {
			CalculateAll();
		}

	})
}



function AddRowTravelFare() {
	
	var LastTRCount = parseInt($('#tblTravelFare').find("tbody tr").length) - 1;
	$('.applyselect').select2("destroy");
	var $tableBody = $('#tblTravelFare').find("tbody"),
		$trLast = $tableBody.find("tr:last"),
		$trNew = $trLast.clone();
	$trNew.find("td:last").html('<a onclick="DeletTravelFare(this)" data-toggle="tooltip" data-original-title="Delete Row" class="close"><i class="fas fa-window-close red-clr" aria-hidden="true"></i></a>');


	$trNew.find("label").each(function () {
		$(this).attr({
			'id': function (_, id) { var arr = id.split('_'); return id.replace(arr[1], LastTRCount + 1); },
		});
		$(this).html(parseInt($('#tblTravelFare').find("tbody tr").length) + 1);
	});

	$trNew.find("select").each(function (i) {
		$(this).attr({

			'name': function (_, name) { return name.replace('[' + LastTRCount + ']', '[' + (parseInt(LastTRCount) + 1) + ']'); },
		});
		$(this).removeAttr('readonly');


	});
	$trNew.find("input").each(function (i) {
		$(this).attr({

			'name': function (_, name) { return name.replace('[' + LastTRCount + ']', '[' + (parseInt(LastTRCount) + 1) + ']'); },
		});
		if ($(this).attr('type') != 'hidden') {
			$(this).val('');
			$(this).removeAttr('readonly');
		}



	});

	$trNew.find("span").each(function (i) {
		if ($(this).attr("data-valmsg-for")) {
			$(this).attr({
				'data-valmsg-for': function (_, name) { return name.replace('[' + LastTRCount + ']', '[' + (parseInt(LastTRCount) + 1) + ']'); }
			});
		}
		if ($(this).attr("for")) {
			$(this).attr({
				'for': function (_, name) { return name.replace('_' + LastTRCount + '_', '_' + (parseInt(LastTRCount) + 1) + '_'); }
			});
		}
	});

	$trLast.after($trNew);
	//$("#[2].PerDiem_Amount").attr("readonly", false);
	$("input[name='[" + (parseInt(LastTRCount) + 1)+"].PerDiem_Amount']").val(0);
	var form = $("form");
	form.removeData('validator');
	form.removeData('unobtrusiveValidation');
	$.validator.unobtrusive.parse(form);
	$('[data-toggle="tooltip"]').tooltip();
	$(".applyselect").select2();

	$('.ATravelSource').bind("change", function () {
		DiableEnableATravelfareAmount(this);
	});
	//PageLoadATravelfareAmount();

	
	
}


function DeletTravelFare(obj) {
	var count = 0;
	var TotalRowCount = $('#tblTravelFare').find("tbody tr").length;
	ConfirmMsgBox("Are you sure want to delete", '', function () {

		$(obj).closest('tr').remove();
		$("#tblTravelFare TBODY TR").each(function (i) {
			$(this).closest("tr").find("label").each(function () {
				$(this).attr({
					'id': function (_, id) { var arr = id.split('_'); return id.replace(arr[1], i); },
				});
				$(this).html(i + 1);
			});
			$(this).closest("tr").find("select").each(function () {
				$(this).attr({
					'id': function (_, id) { return id.replace('_' + (parseInt(i) + 1) + '_', '_' + i + '_'); },
					'name': function (_, name) { return name.replace('[' + (parseInt(i) + 1) + ']', '[' + i + ']'); },
				});

			});

			$(this).closest("tr").find("input").each(function () {
				$(this).attr({
					'id': function (_, id) { return id.replace('_' + (parseInt(i) + 1) + '_', '_' + i + '_'); },
					'name': function (_, name) { return name.replace('[' + (parseInt(i) + 1) + ']', '[' + i + ']'); },
				});

			});
			$(this).closest("tr").find("span").each(function () {
				if ($(this).attr("data-valmsg-for")) {
					$(this).attr({
						'data-valmsg-for': function (_, name) { return name.replace('[' + (parseInt(i) + 1) + ']', '[' + i + ']'); },
					});
				}
				if ($(this).attr("for")) {
					$(this).attr({
						'for': function (_, id) { return id.replace('_' + (parseInt(i) + 1) + '_', '_' + i + '_'); },
					});
				}
			});
		});
		var form = $("form");
		form.removeData('validator');
		form.removeData('unobtrusiveValidation');
		$.validator.unobtrusive.parse(form);
	});
}
$('.ATravelSource').bind("change", function () {
	DiableEnableATravelfareAmount(this);
});

function DiableEnableATravelfareAmount(obj) {
	
	var a = $(obj).closest('tr').find("select.ATravelSource option:selected").val();
	if (a == "By Staff") {
		$(obj).closest('tr').find(".AAmount").removeAttr('readonly');
	}
	else {
		$(obj).closest('tr').find(".AAmount").attr('readonly', 'readonly');
	}
}
function PageLoadATravelfareAmount() {

	var a = $("select.ATravelSource option:selected").val();
	if (a == "By Staff") {
		$("select.ATravelSource option:selected").closest('tr').find(".AAmount").removeAttr('readonly');

	}
	else {
		$("select.ATravelSource option:selected").closest('tr').find(".AAmount").attr('readonly', 'readonly');
	}
}


$('.ErdbOther_status').bind("change", function () {
	ShowE();
});
function ShowE() {
	var checkAll = $(".ErdbOther_status:checked").val();
	if (checkAll === "Yes") {
		$(".EDiv").show();
	}
	else {
		$(".EDiv").hide();
	}
}

function TravelBPerDiemChangeByDDl() {
	var count = 1;
    ;
     var TotalRowCount = $('#tblBPerDiem').find("tbody tr").length;
	$("#tblBPerDiem TBODY TR").each(function (i) {
		  ;
		var PercantageAmount = 0, FinalAmount = 0;
		var percentage = $(this).closest('tr').find("select.BFreemealDD option:selected").attr("percentage");
		var ddltext = $(this).closest('tr').find("select.BFreemealDD option:selected").text();
		var perdiemrate = $(this).closest('tr').find(".spnperdiemrate").html();
		var hdnFirstLast = $(this).closest('tr').find(".hdnFirstLast").val();
	    var hdnIndexValue = $(this).closest('tr').find(".hdnIndexValue").val();
		if ( count==1) {
		
			var newpercentage = 75;
            if(ddltext!="Holiday/ Saturday/ Sunday (100 %)" ){
			var prevpercentage = $(this).closest('tr').find("select.BFreemealDD option:selected").attr("percentage");
			var newperdiemrate = $(this).closest('tr').find(".spnperdiemrate").html();
			var newFirstAmount = parseFloat(newperdiemrate);
			var newPercantageAmount = parseFloat((newFirstAmount * newpercentage) / 100);
			var newprevAmount = parseFloat((newPercantageAmount * prevpercentage) / 100);
			var newFinalAmount = parseFloat(newPercantageAmount) - parseFloat(newprevAmount);
			$(this).closest('tr').find(".bAmount").val(0);
			//$(this).closest('tr').find(".bAmount").val(newFinalAmount); code comment by shailendra on 09/04/2024
              $(this).closest('tr').find(".bAmount").val(Math.round(newFinalAmount)); 

			}
           else if(ddltext=="Holiday/ Saturday/ Sunday (100 %)" ){
            $(this).closest('tr').find(".bAmount").val(0);
			var prevpercentage = $(this).closest('tr').next().find("select.BFreemealDD option:selected").attr("percentage");
			var newperdiemrate = $(this).closest('tr').next().find(".spnperdiemrate").html();
			var newFirstAmount = parseFloat(newperdiemrate);
			var newPercantageAmount = parseFloat((newFirstAmount * newpercentage) / 100);
			var newprevAmount = parseFloat((newPercantageAmount * prevpercentage) / 100);
			var newFinalAmount = parseFloat(newPercantageAmount) - parseFloat(newprevAmount);
			$(this).closest('tr').next().find(".bAmount").val(0);
			//$(this).closest('tr').next().find(".bAmount").val(newFinalAmount);
            if(isNaN(parseInt(newFinalAmount))){
                newFinalAmount=0;
               }
            $(this).closest('tr').find(".bAmount").val(Math.round(newFinalAmount)); 
			}
            else if(ddltext !="Holiday/ Saturday/ Sunday (100 %)"){
            $(this).closest('tr').find(".bAmount").val(0);
			var prevpercentage = $(this).closest('tr').next().find("select.BFreemealDD option:selected").attr("percentage");
			var newperdiemrate = $(this).closest('tr').next().find(".spnperdiemrate").html();
			var newFirstAmount = parseFloat(newperdiemrate);
			var newPercantageAmount = parseFloat((newFirstAmount * newpercentage) / 100);
			var newprevAmount = parseFloat((newPercantageAmount * prevpercentage) / 100);
			var newFinalAmount = parseFloat(newPercantageAmount) - parseFloat(newprevAmount);
			$(this).closest('tr').next().find(".bAmount").val(0);
 			//$(this).closest('tr').next().find(".bAmount").val(newFinalAmount);
          $(this).closest('tr').find(".bAmount").val(Math.round(newFinalAmount)); 
			}
            else{
                 
              		$(this).closest('tr').find(".bAmount").val(0);
             }

		}
       else if(  count==parseInt(TotalRowCount)){
           
             var newpercentage = 75;
            if(ddltext!="Holiday/ Saturday/ Sunday (100 %)"){
			var prevpercentage = $(this).closest('tr').find("select.BFreemealDD option:selected").attr("percentage");
			var newperdiemrate = $(this).closest('tr').find(".spnperdiemrate").html();
			var newFirstAmount = parseFloat(newperdiemrate);
			var newPercantageAmount = parseFloat((newFirstAmount * newpercentage) / 100);
			var newprevAmount = parseFloat((newPercantageAmount * prevpercentage) / 100);
			var newFinalAmount = parseFloat(newPercantageAmount) - parseFloat(newprevAmount);
			$(this).closest('tr').find(".bAmount").val(0);
			//$(this).closest('tr').find(".bAmount").val(newFinalAmount);
         
             $(this).closest('tr').find(".bAmount").val(Math.round(newFinalAmount)); 
			}

            else{
                
              $(this).closest('tr').find(".bAmount").val(0);
             var ddlPrevtext = $(this).closest('tr').prev().find("select.BFreemealDD option:selected").text();

             if(ddlPrevtext=="Holiday/ Saturday/ Sunday (100 %)"){
              var prevpercentage = $(this).closest('tr').prev().prev().find("select.BFreemealDD option:selected").attr("percentage");
			var newperdiemrate = $(this).closest('tr').prev().prev().find(".spnperdiemrate").html();
			var newFirstAmount = parseFloat(newperdiemrate);
			var newPercantageAmount = parseFloat((newFirstAmount * newpercentage) / 100);
			var newprevAmount = parseFloat((newPercantageAmount * prevpercentage) / 100);
			var newFinalAmount = parseFloat(newPercantageAmount) - parseFloat(newprevAmount);
			$(this).closest('tr').prev().prev().find(".bAmount").val(0);
			//$(this).closest('tr').prev().prev().find(".bAmount").val(newFinalAmount);
             $(this).closest('tr').prev().prev().find(".bAmount").val(Math.round(newFinalAmount));
         
              
               }
               else{

            
             var prevpercentage = $(this).closest('tr').prev().find("select.BFreemealDD option:selected").attr("percentage");
			var newperdiemrate = $(this).closest('tr').prev().find(".spnperdiemrate").html();
			var newFirstAmount = parseFloat(newperdiemrate);
			var newPercantageAmount = parseFloat((newFirstAmount * newpercentage) / 100);
			var newprevAmount = parseFloat((newPercantageAmount * prevpercentage) / 100);
			var newFinalAmount = parseFloat(newPercantageAmount) - parseFloat(newprevAmount);
			$(this).closest('tr').prev().find(".bAmount").val(0);
			//$(this).closest('tr').prev().find(".bAmount").val(newFinalAmount);
             $(this).closest('tr').prev().find(".bAmount").val(Math.round(newFinalAmount));
       
               }
             }


       }
    else{
             var newpercentage = 75;
           	var ddlPrevtext = $(this).closest('tr').prev().find("select.BFreemealDD option:selected").text();
            var ddlPrevPrevtext = $(this).closest('tr').prev().prev().find("select.BFreemealDD option:selected").text();
             if(ddlPrevtext=="Holiday/ Saturday/ Sunday (100 %)" && ddlPrevPrevtext=="Holiday/ Saturday/ Sunday (100 %)"){
              var prevpercentage = $(this).closest('tr').find("select.BFreemealDD option:selected").attr("percentage");
			var newperdiemrate = $(this).closest('tr').find(".spnperdiemrate").html();
			var newFirstAmount = parseFloat(newperdiemrate);
			var newPercantageAmount = parseFloat((newFirstAmount * newpercentage) / 100);
			var newprevAmount = parseFloat((newPercantageAmount * prevpercentage) / 100);
			var newFinalAmount = parseFloat(newPercantageAmount) - parseFloat(newprevAmount);
			$(this).closest('tr').find(".bAmount").val(0);
			//$(this).closest('tr').find(".bAmount").val(newFinalAmount);
           $(this).closest('tr').find(".bAmount").val(Math.round(newFinalAmount)); 
             }
            else if(ddlPrevtext=="Holiday/ Saturday/ Sunday (100 %)" && ddlPrevPrevtext==""){
             var prevpercentage = $(this).closest('tr').find("select.BFreemealDD option:selected").attr("percentage");
			var newperdiemrate = $(this).closest('tr').find(".spnperdiemrate").html();
			var newFirstAmount = parseFloat(newperdiemrate);
			var newPercantageAmount = parseFloat((newFirstAmount * newpercentage) / 100);
			var newprevAmount = parseFloat((newPercantageAmount * prevpercentage) / 100);
			var newFinalAmount = parseFloat(newPercantageAmount) - parseFloat(newprevAmount);
			$(this).closest('tr').find(".bAmount").val(0);
			//$(this).closest('tr').find(".bAmount").val(newFinalAmount);
            $(this).closest('tr').find(".bAmount").val(Math.round(newFinalAmount)); 
             }
             else {
            // if(ddlPrevtext!=""){
           var prevpercentage = $(this).closest('tr').find("select.BFreemealDD option:selected").attr("percentage");
	       var prevperdiemrate = $(this).closest('tr').find(".spnperdiemrate").html();
		   var newFirstAmount = parseFloat(prevperdiemrate);
           var PrevPercantageAmount = parseFloat((newFirstAmount * prevpercentage) / 100);
		   var PrevFinalAmount = parseFloat(prevperdiemrate) - parseFloat(PrevPercantageAmount);
	       //$(this).closest('tr').find(".bAmount").val(PrevFinalAmount);
             $(this).closest('tr').find(".bAmount").val(Math.round(PrevFinalAmount)); 
           // }
           }
    }
		
	count++;
		
	});
}


$('.BFreemealDD').bind("change", function () {
	 ;
	var PercantageAmount = 0, FinalAmount = 0;
	var percentage = $(this).closest('tr').find("select.BFreemealDD option:selected").attr("percentage");
	var perdiemrate = $(this).closest('tr').find(".spnperdiemrate").html();
	var hdnFirstLast = $(this).closest('tr').find(".hdnFirstLast").val();
	var ddltext = $(this).closest('tr').next().find("select.BFreemealDD option:selected").text();
    var ddlPrevText=$(this).closest('tr').find("select.BFreemealDD option:selected").text();
	if (ddltext == "leave (100 %)") {
		 ;
		var newpercentage = 75;
		var prevpercentage = $(this).closest('tr').find("select.BFreemealDD option:selected").attr("percentage");
		var newperdiemrate = $(this).closest('tr').find(".spnperdiemrate").html();
		var newFirstAmount = parseFloat(newperdiemrate);
		var newPercantageAmount = parseFloat((newFirstAmount * newpercentage) / 100);

		var newprevAmount = parseFloat((newPercantageAmount * prevpercentage) / 100);
		var newFinalAmount = parseFloat(newPercantageAmount) - parseFloat(newprevAmount);
		//$(this).closest('tr').prev().find(".bAmount").val();
		//$(this).closest('tr').prev().find(".bAmount").val(newFinalAmount);
		//$("input[name='[" + (parseInt(LastTRCount) + 1) + "].PerDiem_Amount']").val(0);
		//$("#BPERDIEM_'" + parseInt(count - 1) + "'__Amount").val(newFinalAmount);
		//$("#BPERDIEM_2__Amount").val(newFinalAmount);
		$(this).closest('tr').find(".bAmount").val(0);
		//$(this).closest('tr').find(".bAmount").val(newFinalAmount);
        $(this).closest('tr').find(".bAmount").val(Math.round(newFinalAmount)); 


	}
    else if(hdnFirstLast=="1.1")
     {
       TravelBPerDiemChangeByDDl();

     }
	else {
		
// code comment by shailendra 11/06/2024
//if (parseFloat(hdnFirstLast) > 0) {
			//FirstAmount = parseFloat(perdiemrate * parseFloat(hdnFirstLast));
		//}
		//else {
			//FirstAmount = parseFloat(perdiemrate);
		//}
		//PercantageAmount = parseFloat((FirstAmount * percentage) / 100);
		//FinalAmount = parseFloat(FirstAmount - PercantageAmount).toFixed(0);
		//$(this).closest('tr').find(".bAmount").val(FinalAmount);
     //  $(this).closest('tr').find(".bAmount").val(Math.round(FinalAmount)); 
  TravelBPerDiemChangeByDDl();
	}

	

	var checkradio = $("#TravelExpense_Expensedistrubute_status").val();
	if (checkradio == "No") {
		CalculateNoDist();
	}
	else {
		CalculateAll();
	}
});
$('.CModeOfTransportDD').bind("change", function () {

	
	var PerKmRate = $(this).closest('tr').find("select.CModeOfTransportDD option:selected").attr("kmrate");
	var TrnasType = $(this).closest('tr').find("select.CModeOfTransportDD option:selected").text();
	if (TrnasType == "Self four wheeler" || TrnasType == "Self two wheeler") {
		$(this).closest('tr').find(".CAmount").attr('readonly', 'readonly');
		$(this).closest('tr').find(".RateofPerKM").val(PerKmRate).attr('readonly', 'readonly');
		$(this).closest('tr').find(".cTrnasKM").val(0).removeAttr('readonly');
	}
	else if (TrnasType == "Hired Taxi" || TrnasType == "Hired two wheeler" || TrnasType == "Hired four Wheeler") {
		$(this).closest('tr').find(".cTrnasKM").val(0).attr('readonly', 'readonly');
		$(this).closest('tr').find(".RateofPerKM").val(0).attr('readonly', 'readonly');
		$(this).closest('tr').find(".CAmount").removeAttr('readonly');
	}

	
});
$('.cTrnasKM').blur(function () {

	var PerKmRate = 0, FinalAmount = 0;
	var PerKmRate = $(this).closest('tr').find("select.CModeOfTransportDD option:selected").attr("kmrate");
	var Km = $(this).closest('tr').find(".cTrnasKM").val();
	if (parseInt(Km) > 0) {
		FinalAmount = parseFloat(parseInt(PerKmRate) * parseInt(Km));
	}
	else {
		FirstAmount = parseInt(FinalAmount);
	}
	
	$(this).closest('tr').find(".CAmount").val(FinalAmount);
	var checkradio = $("#TravelExpense_Expensedistrubute_status").val();
	if (checkradio == "No") {
		CalculateNoDist();
	}
	else {
		CalculateAll();
	}

});
function BCalFreeMealAmount() {
	var count = 0;
	$("#tblBPerDiem TBODY TR").each(function (i) {

		var PercantageAmount = 0, FinalAmount = 0;
		var percentage = $(this).find("select.BFreemealDD option:selected").attr("percentage");
		var perdiemrate = $(this).find(".spnperdiemrate").html();
		var hdnFirstLast = $(this).find(".hdnFirstLast").val();
		var ddltext = $(this).closest('tr').find("select.BFreemealDD option:selected").text();
		if (ddltext == "leave (100 %)" && count == 0) {
		
			var newpercentage = 75;
			var prevpercentage = $(this).closest('tr').prev().find("select.BFreemealDD option:selected").attr("percentage");
			var newperdiemrate = $(this).closest('tr').prev().find(".spnperdiemrate").html();
			var newFirstAmount = parseFloat(newperdiemrate);
			var newPercantageAmount = parseFloat((newFirstAmount * newpercentage) / 100);

			var newprevAmount = parseFloat((newPercantageAmount * prevpercentage) / 100);
			var newFinalAmount = parseFloat(newPercantageAmount) - parseFloat(newprevAmount);
			//$(this).closest('tr').prev().find(".bAmount").val();
			//$(this).closest('tr').prev().find(".bAmount").val(newFinalAmount);
			//$("input[name='[" + (parseInt(LastTRCount) + 1) + "].PerDiem_Amount']").val(0);
			//$("#BPERDIEM_'" + parseInt(count - 1) + "'__Amount").val(newFinalAmount);
			//$("#BPERDIEM_2__Amount").val(newFinalAmount);
			$(this).closest('tr').prev().find(".bAmount").val(0);
			//$(this).closest('tr').prev().find(".bAmount").val(newFinalAmount);
             $(this).closest('tr').prev().find(".bAmount").val(Math.round(newFinalAmount)); 
			count++;

		}
		if (parseFloat(hdnFirstLast) > 0) {
			FirstAmount = parseFloat(perdiemrate * parseFloat(hdnFirstLast));
		}
		else {
			FirstAmount = parseFloat(perdiemrate);
		}
		PercantageAmount = parseFloat((FirstAmount * percentage) / 100);
	    FinalAmount = parseFloat(FirstAmount - PercantageAmount).toFixed(0);
	  //  $(this).find(".bAmount").val(FinalAmount);
          $(this).find(".bAmount").val(Math.round(FinalAmount));
	//	$(this).closest('tr').find(".bAmount").val(PercantageAmount.toFixed(0));
	});
	CalculateAll();
}
$('.AAmount').blur(function () {

	CalculateAll();
	var checkradio = $("#TravelExpense_Expensedistrubute_status").val();
	if (checkradio == "No") {
		CalculateNoDist();

	}
	else {
		CalculateAll();
	}

});

$('.bAmount').blur(function () {
	CalculateAll();
	var checkradio = $("#TravelExpense_Expensedistrubute_status").val();
	if (checkradio == "No") {
		CalculateNoDist();
	}
	else {
		CalculateAll();
	}
});

$('.CAmount').blur(function () {
	
	var checkradio = $("#TravelExpense_Expensedistrubute_status").val();
	if (checkradio == "No") {
		CalculateNoDist();
	}
	else {
		CalculateAll();
	}
	
	
});
$('.DAmount').blur(function () {
	
	var checkradio = $("#TravelExpense_Expensedistrubute_status").val();
	if (checkradio == "No") {
		CalculateNoDist();
	}
	else {
		CalculateAll();
	}
	
});
$('.EAmount').blur(function () {
	ECalculatetotal();
	CalCulateTotal();
});
$('.FAmount').blur(function () {

	FCalculatetotal();
	CalCulateTotal();

});
$('.GAmount').blur(function () {

	GCalculatetotal();
	CalCulateTotal();
});
$('.HAmount').blur(function () {
	HCalculatetotal();
	CalCulateTotal();
});
$('#TravelExpense_amount').blur(function () {
	$("#TravelExpense_anyOther_Credit").val($(this).val());
	CalculateAll();
});

function CalculateAll() {
	ACalculatetotal();
	BCalculatetotal();
	CCalculatetotal();
	DCalculatetotal();
	//ECalculatetotal();
	CalCulateTotal();
	CalcualteNetpayable();
}
function ACalculatetotal() {

	var tempCtotal = 0;
	var count = $("#hdfprojectcount").val();
	$(".AAmount").each(function (index) {
		tempCtotal += parseFloat(Number($(this).val()));
	});
	$(".totalA").val(tempCtotal);
	$(".prnttravelfare").each(function (index) {
	
		var fillamount = (parseFloat(tempCtotal) / parseInt(count)).toFixed(2);
		//$(this).find('.dvdtravelfare').val(parseFloat(tempCtotal) / parseInt(count));
		$(this).find('.dvdtravelfare').val(fillamount);
		
	});
}

function ECalculatetotal() {
	var tempCtotal = 0;
	$(".EAmount").each(function (index) {
		tempCtotal += parseFloat(Number($(this).val()));
	});
	$(".totalA").val(tempCtotal);
}
function BCalculatetotal() {
	var count = $("#hdfprojectcount").val();
	var tempCtotal = 0;
	$(".bAmount").each(function (index) {
		tempCtotal += parseFloat(Number($(this).val()));
	});
	$(".totalB").val(tempCtotal);
	$(".prntperdiem").each(function (index) {
		var fillamount = (parseFloat(tempCtotal) / parseInt(count)).toFixed(2);
		$(this).find('.dvdperdiemrate').val(fillamount);
		//$(this).find('.dvdperdiemrate').val(parseFloat(tempCtotal) / parseInt(count));
	});
}

function FCalculatetotal() {

	var count = $("#hdfprojectcount").val();
	var tempCtotal = 0;
	$(".FAmount").each(function (index) {

		tempCtotal += parseFloat(Number($(this).val()));
	});
	$(".totalB").val(tempCtotal);
}
function CCalculatetotal() {

	var count = $("#hdfprojectcount").val();
	var tempCtotal = 0;
	$(".CAmount").each(function (index) {
	
		tempCtotal += parseFloat(Number($(this).val()));
	});
	$(".totalC").val(tempCtotal);
	$(".prnttransportion").each(function (index) {
	
		var fillamount = (parseFloat(tempCtotal) / parseInt(count)).toFixed(2);
		$(this).find('.dvdtransportion').val(fillamount);
	});
}
function GCalculatetotal() {

	var tempCtotal = 0;
	$(".GAmount").each(function (index) {

		tempCtotal += parseFloat(Number($(this).val()));
	});
	$(".totalC").val(tempCtotal);
}
function DCalculatetotal() {

	var tempCtotal = 0;
	var count = $("#hdfprojectcount").val();
	$(".DAmount").each(function (index) {
		tempCtotal += parseFloat(Number($(this).val()));
	});
	$(".totalD").val(tempCtotal);
	$(".prntExpenses").each(function (index) {
		var fillamount = (parseFloat(tempCtotal) / parseInt(count)).toFixed(2);
		$(this).find('.dvdExpenses').val(fillamount);

	});
}

function HCalculatetotal() {
	var tempCtotal = 0;
	$(".HAmount").each(function (index) {
		tempCtotal += parseFloat(Number($(this).val()));
	});
	$(".totalD").val(tempCtotal);
}


function CalCulateTotal() {
	
	var count = $("#hdfprojectcount").val();

	/*  Total cal*/ //TravelFare


	if (document.getElementById('nodistrubute').checked) {
		// $(".CAmount ").attr("readonly", true);
		// $(".DAmount  ").attr("readonly", true);
		//if (count == 1) {
		//	var temptravelone = $("#EXPENSESUMMARY_0__Travelfare").val();
		//	var tempperdiemone = $("#EXPENSESUMMARY_0__Perdiem").val();
		//	var temptrnasone = $("#EXPENSESUMMARY_0__Transportion").val();
		//	var temptotherexpone = $("#EXPENSESUMMARY_0__Otherexpesnse").val();
		//	$("#txttotal_1").val(Number(Number(temptravelone) + Number(tempperdiemone) + Number(temptrnasone) + Number(temptotherexpone)).toFixed(2));
		//}
		//if (count == 2) {
		

			//var temptravelone = $("#EXPENSESUMMARY_0__Travelfare").val();
			//var temptraveltwo = $("#EXPENSESUMMARY_1__Travelfare").val();
			//var tempperdiemone = $("#EXPENSESUMMARY_0__Perdiem").val();
			//var tempperdiemtwo = $("#EXPENSESUMMARY_1__Perdiem").val();
			//var temptrnasone = $("#EXPENSESUMMARY_0__Transportion").val();
			//var temptrnastwo = $("#EXPENSESUMMARY_1__Transportion").val();
			//var temptotherexpone = $("#EXPENSESUMMARY_0__Otherexpesnse").val();
			//var temptotherexptwo = $("#EXPENSESUMMARY_1__Otherexpesnse").val();
			//$("#txttotal_1").val(Number(Number(temptravelone) + Number(tempperdiemone) + Number(temptrnasone) + Number(temptotherexpone)).toFixed(2));
			//$("#txttotal_2").val(Number(Number(temptraveltwo) + Number(tempperdiemtwo) + Number(temptrnastwo) + Number(temptotherexptwo)).toFixed(2));
		//}
		//if (count == 3) {
		//	var temptravelone = $("#EXPENSESUMMARY_0__Travelfare").val();
		//	var temptraveltwo = $("#EXPENSESUMMARY_1__Travelfare").val();
		//	var temptravelthree = $("#EXPENSESUMMARY_2__Travelfare").val();
		//	var tempperdiemone = $("#EXPENSESUMMARY_0__Perdiem").val();
		//	var tempperdiemtwo = $("#EXPENSESUMMARY_1__Perdiem").val();
		//	var tempperdiemthree = $("#EXPENSESUMMARY_2__Perdiem").val();
		//	var temptrnasone = $("#EXPENSESUMMARY_0__Transportion").val();
		//	var temptrnastwo = $("#EXPENSESUMMARY_1__Transportion").val();
		//	var temptrnasthree = $("#EXPENSESUMMARY_2__Transportion").val();
		//	var temptotherexpone = $("#EXPENSESUMMARY_0__Otherexpesnse").val();
		//	var temptotherexptwo = $("#EXPENSESUMMARY_1__Otherexpesnse").val();
		//	var temptotherexpthree = $("#EXPENSESUMMARY_2__Otherexpesnse").val();
		//	$("#txttotal_1").val(Number(Number(temptravelone) + Number(tempperdiemone) + Number(temptrnasone) + Number(temptotherexpone)).toFixed(2));
		//	$("#txttotal_2").val(Number(Number(temptraveltwo) + Number(tempperdiemtwo) + Number(temptrnastwo) + Number(temptotherexptwo)).toFixed(2));
		//	$("#txttotal_3").val(Number(Number(temptravelthree) + Number(tempperdiemthree) + Number(temptrnasthree) + Number(temptotherexpthree)).toFixed(2));
		//}
		var A = Number($(".totalA").val());
		var B = Number($(".totalB").val());
		var C = Number($(".totalC").val());
		var D = Number($(".totalD").val());
		$(".totalFinal").val(A + B + C + D);
	}

	else {
		// $(".CAmount ").attr("readonly", false);
		//  $(".DAmount  ").attr("readonly", false);
		var A = Number($(".totalA").val());
		var B = Number($(".totalB").val());
		var C = Number($(".totalC").val());
		var D = Number($(".totalD").val());
		$(".totalFinal").val(A + B + C + D);
		$(".prntTotal").each(function (index) {
			
			var fillamount = (parseFloat(A + B + C + D) / parseInt(count)).toFixed(2);
			$(this).find('.dvdTotal').val(fillamount);
			//$(this).find('.dvdTotal').val(parseFloat(A + B + C + D) / parseInt(count));
		});





	}
	CalcualteNetpayable();
}

function CalcualteNetpayable() {

	var A = Number($(".totalFinal").val());
	var B = Number($("#TravelExpense_Paidamount").val());
	var C = Number($("#TravelExpense_anyOther_Credit").val());
	// $(".Netpayable").val(A + (B - C ));
	$(".Netpayable").val(A - (B + C));
}