function AddExp() {
	var LastTRCount = parseInt($('#tblExp').find("tbody tr").length) - 1;
	var $tableBody = $('#tblExp').find("tbody"),
		$trLast = $tableBody.find("tr:last"),
		$trNew = $trLast.clone();
	$trNew.find("td:last").html('<a onclick="DeleteExpRow(this)" class="remove" data-toggle="tooltip" data-original-title="Remove"><i class="fas fa-window-close red-clr" aria-hidden="true"></i></a>');


	$trNew.find("div").each(function () {
		$(this).attr({
			'id': function (_, id) { var arr = id.split('_'); return id.replace(arr[1], LastTRCount + 1); }
		});

	});

	$trNew.find("label").each(function () {
		$(this).attr({
			'id': function (_, id) { var arr = id.split('_'); return id.replace(arr[1], LastTRCount + 1); },
		});
		$(this).html(parseInt($('#tblExp').find("tbody tr").length) + 1);
	});

	$trNew.find("input").each(function (i) {
		$(this).attr({
			'id': function (_, name) { return name.replace('_' + LastTRCount + '_', '_' + (parseInt(LastTRCount) + 1) + '_'); },
			'name': function (_, name) { return name.replace('[' + LastTRCount + ']', '[' + (parseInt(LastTRCount) + 1) + ']'); },
		});
		$(this).val('');
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
	var form = $("#ApplicationFrom").closest("form");
	form.removeData('validator');
	form.removeData('unobtrusiveValidation');
	$.validator.unobtrusive.parse(form);
	$('[data-toggle="tooltip"]').tooltip();
}

function DeleteExpRow(obj) {
	var count = 0;
	var TotalRowCount = $('#tblExp').find("tbody tr").length;
	ConfirmMsgBox("Are you sure want to delete", '', function () {

		$(obj).closest('tr').remove();
		$("#tblExp TBODY TR").each(function (i) {
			$(this).closest("tr").find("label").each(function () {
				$(this).attr({
					'id': function (_, id) { var arr = id.split('_'); return id.replace(arr[1], i); },
				});
				$(this).html(i + 1)
			});

			$(this).closest("tr").find("div").each(function () {
				$(this).attr({
					'id': function (_, id) { var arr = id.split('_'); return id.replace(arr[1], i); },
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
		var form = $("#ApplicationFrom").closest("form");
		form.removeData('validator');
		form.removeData('unobtrusiveValidation');
		$.validator.unobtrusive.parse(form);

	})
}

function AddEdu() {
	var LastTRCount = parseInt($('#tblEdu').find("tbody tr").length) - 1;
	var $tableBody = $('#tblEdu').find("tbody"),
		$trLast = $tableBody.find("tr:last"),
		$trNew = $trLast.clone();
	$trNew.find("td:last").html('<a onclick="DeleteEduRow(this)" class="remove" data-toggle="tooltip" data-original-title="Remove"><i class="fas fa-window-close red-clr" aria-hidden="true"></i></a>');


	$trNew.find("div").each(function () {
		$(this).attr({
			'id': function (_, id) { var arr = id.split('_'); return id.replace(arr[1], LastTRCount + 1); }
		});

	});

	$trNew.find("label").each(function () {
		$(this).attr({
			'id': function (_, id) { var arr = id.split('_'); return id.replace(arr[1], LastTRCount + 1); },
		});
		$(this).html(parseInt($('#tblEdu').find("tbody tr").length) + 1);
	});

	$trNew.find("input").each(function (i) {
		$(this).attr({
			'id': function (_, name) { return name.replace('_' + LastTRCount + '_', '_' + (parseInt(LastTRCount) + 1) + '_'); },
			'name': function (_, name) { return name.replace('[' + LastTRCount + ']', '[' + (parseInt(LastTRCount) + 1) + ']'); },
		});
		$(this).val('');	
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
	var form = $("#ApplicationFrom").closest("form");
	form.removeData('validator');
	form.removeData('unobtrusiveValidation');
	$.validator.unobtrusive.parse(form);
	$('[data-toggle="tooltip"]').tooltip();

}

function DeleteEduRow(obj) {
	var count = 0;
	var TotalRowCount = $('#tblEdu').find("tbody tr").length;
	ConfirmMsgBox("Are you sure want to delete", '', function () {

		$(obj).closest('tr').remove();
		$("#tblEdu TBODY TR").each(function (i) {
			$(this).closest("tr").find("label").each(function () {
				$(this).attr({
					'id': function (_, id) { var arr = id.split('_'); return id.replace(arr[1], i); },
				});
				$(this).html(i + 1)
			});

			$(this).closest("tr").find("div").each(function () {
				$(this).attr({
					'id': function (_, id) { var arr = id.split('_'); return id.replace(arr[1], i); },
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
		var form = $("#ApplicationFrom").closest("form");
		form.removeData('validator');
		form.removeData('unobtrusiveValidation');
		$.validator.unobtrusive.parse(form);

	})
}

function AddRef() {
	var LastTRCount = parseInt($('#tblRef').find("tbody tr").length) - 1;
	var $tableBody = $('#tblRef').find("tbody"),
		$trLast = $tableBody.find("tr:last"),
		$trNew = $trLast.clone();
	$trNew.find("td:last").html('<a onclick="DeleteRefRow(this)" class="remove" data-toggle="tooltip" data-original-title="Remove"><i class="fas fa-window-close red-clr" aria-hidden="true"></i></a>');


	$trNew.find("div").each(function () {
		$(this).attr({
			'id': function (_, id) { var arr = id.split('_'); return id.replace(arr[1], LastTRCount + 1); }
		});

	});

	$trNew.find("label").each(function () {
		$(this).attr({
			'id': function (_, id) { var arr = id.split('_'); return id.replace(arr[1], LastTRCount + 1); },
		});
		$(this).html(parseInt($('#tblRef').find("tbody tr").length) + 1);
	});

	$trNew.find("input").each(function (i) {
		$(this).attr({
			'id': function (_, name) { return name.replace('_' + LastTRCount + '_', '_' + (parseInt(LastTRCount) + 1) + '_'); },
			'name': function (_, name) { return name.replace('[' + LastTRCount + ']', '[' + (parseInt(LastTRCount) + 1) + ']'); },
		});
		$(this).val('');
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
	var form = $("#ApplicationFrom").closest("form");
	form.removeData('validator');
	form.removeData('unobtrusiveValidation');
	$.validator.unobtrusive.parse(form);
	$('[data-toggle="tooltip"]').tooltip();

}


function DeleteRefRow(obj) {
	var count = 0;
	var TotalRowCount = $('#tblRef').find("tbody tr").length;
	ConfirmMsgBox("Are you sure want to delete", '', function () {

		$(obj).closest('tr').remove();
		$("#tblRef TBODY TR").each(function (i) {
			$(this).closest("tr").find("label").each(function () {
				$(this).attr({
					'id': function (_, id) { var arr = id.split('_'); return id.replace(arr[1], i); },
				});
				$(this).html(i + 1)
			});

			$(this).closest("tr").find("div").each(function () {
				$(this).attr({
					'id': function (_, id) { var arr = id.split('_'); return id.replace(arr[1], i); },
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
		var form = $("#ApplicationFrom").closest("form");
		form.removeData('validator');
		form.removeData('unobtrusiveValidation');
		$.validator.unobtrusive.parse(form);

	})
}


$("#ddBreakReason").change(function () { 
	if ($(this).find(":selected").val() == "Yes") {
		$("#divBreak").show();

		$("#BreakReason").rules("add", {
			required: true,
			messages: {
				required: "Hey! You Missed This Field"
			}
		});

	}
	else {
		$('#BreakReason').val('');
		$("#divBreak").hide();
		$("#BreakReason").rules("remove");
	}
});

$(".ddQuestionNo").change(function () { 
	var tag = $(this).attr("tag");
	if ($(this).find(":selected").val() == "Yes") {
		$(this).closest('li').find(".myContent").hide();
		$(this).closest('li').find("#divQuest_" + tag).show();

		$(this).closest('li').find(".myinput_" + tag).each(function () {
			var my = $(this);
			$(my).rules("add", {
				required: false,
				messages: {
					required: "Hey you Missed this!"
				}
			});

		});

	}
	else {
		$(this).closest('li').find(".myContent").hide();

		$(this).closest('li').find(".myinput_" + tag).each(function () {
			var my = $(this);
			$(my).rules("remove");
		});
	}
	
});


