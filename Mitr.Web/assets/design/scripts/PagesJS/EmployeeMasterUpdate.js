function Salary_Validate() {

var hdfactype="";
var CBankId=true;
var CBankType=true;
var CACName=true;
var CACNo=true;
var CBranchAddress=true;
var CIFSCCode=true;
var CSWiftCode=true;
var COtherDetails=true;
	var ret = false;
	var chk = false;
	var BankId = $("#SalaryAccount_BankID").find("option:selected").val();
	var BankType = $("#SalaryAccount_AccountType").find("option:selected").val();
	var ACName = $('#SalaryAccount_AccountName').val();
	var ACNo = $('#SalaryAccount_AccountNo').val();
	var BranchAddress = $('#SalaryAccount_BranchAddress').val();
	var IFSCCode = $('#SalaryAccount_IFSCCode').val();
	var SWiftCode = $('#SalaryAccount_SwiftCode').val();
	var OtherDetails = $('#SalaryAccount_OtherDetails').val();
    var src = $('#hdfsrc').val();

     var hBankId = $('#hdfBankId').val();
      var hAccountType = $('#hdfAccountType').val();
       if(hAccountType=="Saving"){
          hdfactype=1;
      }
    else{
        hdfactype=0;
    }
      var hAccountName = $('#hdfAccountName').val();
      var hAccountNo = $('#hdfAccountNo').val();
      var hIFSCCode = $('#hdfIFSCCode').val();
      var hBranchAddress= $('#hdfBranchAddress').val();
      var hSwiftCode = $('#hdfSwiftCode').val();
      var hOtherDetails = $('#hdfOtherDetails').val();

  // var chk=CheckValue();
	//if (ACName == "") {
		//$('#msgacname').text("Hey! You missed this field.");
	//}
	//else {
	//	$('#msgacname').text("");
		//ret = true;
	//}
CBankId=ValidateCompare(BankId,hBankId);
CBankType=ValidateCompare(BankType,hdfactype);
CACName=ValidateCompare(ACName,hAccountName);
CBranchAddress=ValidateCompare(BranchAddress,hBranchAddress);
CIFSCCode=ValidateCompare(IFSCCode,hIFSCCode);
CSWiftCode=ValidateCompare(SWiftCode,hSwiftCode);
COtherDetails=ValidateCompare(OtherDetails,hOtherDetails);
CACNo=ValidateCompareNumber(ACNo,hAccountNo);
      
	var obj = {
		BankID: BankId, AccountType: BankType, AccountName: ACName, AccountNo: ACNo, BranchAddress: BranchAddress, IFSCCode: IFSCCode, SwiftCode: SWiftCode, OtherDetails: OtherDetails, Doctype: "Salary",src:src
	}

	if (CBankId == false && CBankType==false && CACName==false && CBranchAddress==false && CIFSCCode==false && CSWiftCode==false && COtherDetails==false && CACNo==false) {
			 FailToaster('Bank Account Details for Salary and Benefits are blank !');
					location.reload();
	
	}
else{
if(BankId =="" && BankType =="1" && ACName =="" && ACNo =="" && BranchAddress =="" && IFSCCode =="" && SWiftCode =="" && OtherDetails ==""){

FailToaster('Can not save.');
location.reload();
}
else{

$.ajax({
			url: "/Account/updateSallaryDetailsEmp",
			type: "Get",
			async: true,
			data: obj,
			success: function (data) {
				 ;
				ShowLoadingDialog();
				if (data.ID > 0) {
					 ;
					SuccessToaster('Bank Details Updated successfully');
					location.reload();

				}
				else {
					FailToaster('Can not save.')
		             location.reload();
				}
			},
			error: function (er) {
				alert(er);
			}
		});


}
}
     
	

}

function ValidateCompare(value1,value2){
var Status=true;
if(value1==value2){

Status=false;
   }
return Status;

}

function ValidateCompareNumber(value1,value2){
var Status=true;
if(parseInt(value1)==parseInt(value2)){

Status=false;
   }
return Status;

}

function Salary_Reimbursement() {

	  ShowLoadingDialog();
      var hdfactype="";
      var CBankId=true;
      var CBankType=true;
      var CACName=true;
      var CACNo=true;
      var CBranchAddress=true;
      var CIFSCCode=true;
      var CSWiftCode=true;
      var COtherDetails=true;
	var ret = false;
	var BankId = $("#ReimbursementAccount_BankID").find("option:selected").val();
	var BankType = $("#ReimbursementAccount_AccountType").find("option:selected").val();
	var ACName = $('#ReimbursementAccount_AccountName').val();
	var ACNo = $('#ReimbursementAccount_AccountNo').val();
	var BranchAddress = $('#ReimbursementAccount_BranchAddress').val();
	var IFSCCode = $('#ReimbursementAccount_IFSCCode').val();
	var SWiftCode = $('#ReimbursementAccount_SwiftCode').val();
	var OtherDetails = $('#ReimbursementAccount_OtherDetails').val();
    var src = $('#hdfsrc').val();

      var hBankId = $('#hdfBankIdR').val();
      var hAccountType = $('#hdfAccountTypeR').val();
       if(hAccountType=="Saving"){
          hdfactype=1;
      }
    else{
        hdfactype=0;
    }
      var hAccountName = $('#hdfAccountNameR').val();
      var hAccountNo = $('#hdfAccountNoR').val();
      var hIFSCCode = $('#hdfIFSCCodeR').val();
      var hBranchAddress= $('#hdfBranchAddressR').val();
      var hSwiftCode = $('#hdfSwiftCodeR').val();
      var hOtherDetails = $('#hdfOtherDetailsR').val();
      CBankId=ValidateCompare(BankId,hBankId);
      CBankType=ValidateCompare(BankType,hdfactype);
      CACName=ValidateCompare(ACName,hAccountName);
      CBranchAddress=ValidateCompare(BranchAddress,hBranchAddress);
      CIFSCCode=ValidateCompare(IFSCCode,hIFSCCode);
      CSWiftCode=ValidateCompare(SWiftCode,hSwiftCode);
      COtherDetails=ValidateCompare(OtherDetails,hOtherDetails);

	var obj = {
		BankID: BankId, AccountType: BankType, AccountName: ACName, AccountNo: ACNo, BranchAddress: BranchAddress, IFSCCode: IFSCCode, SwiftCode: SWiftCode, OtherDetails: OtherDetails, Doctype: "Reimbursement",src:src
	}
	if (CBankId == false && CBankType==false && CACName==false && CBranchAddress==false && CIFSCCode==false && CSWiftCode==false && COtherDetails==false) {
		FailToaster('Bank Account Details for Offical Reimbursement are blank !');
					location.reload();
	}
else{
 if(BankId =="" && BankType =="1" && ACName =="" && ACNo =="" && BranchAddress =="" && IFSCCode =="" && SWiftCode =="" && OtherDetails ==""){

FailToaster('Can not save.');
location.reload();
}
else{


$.ajax({
			url: "/Account/updateSallaryDetailsEmp",
			type: "Get",
			async: true,
			data: obj,
			success: function (data) {

				if (data.ID > 0) {
					SuccessToaster('Bank Details Updated successfully');
					location.reload();

				}
				else {
					FailToaster('Can not save.');
				}
			},
			error: function (er) {
				alert(er);
			}
		});
   }
}
  


}
function Personal_Update() {

	var gender = $("#gender").find("option:selected").text();
	var marital_status = $("#marital_status").find("option:selected").text();
	var SpouseName = $('#SpouseName').val();
	var PartnerName = $('#PartnerName').val();
	var NomineeName = $('#NomineeName').val();
	var NomineeRelation = $('#NomineeRelation').val();
	var children = $('#children').val();
    var src = $('#hdfsrc').val();
   // check hidden value
      var Cmarital_status=true;
      var CSpouseName=true;
      var CPartnerName=true;
      var CNomineeName=true;
      var CNomineeRelation=true;
      var Cchildren=true;
    var hdfmarital_status = $('#hdfmarital_status').val();
    var hdfSpouseName  = $('#hdfSpouseName').val();
	var hdfPartnerName = $("#hdfPartnerName").val();
	var hdfNomineeName = $("#hdfNomineeName").val();
	var hdfNomineeRelation = $("#hdfNomineeRelation").val();
	var hdfchildren = $('#hdfchildren').val();
     Cmarital_status=ValidateCompare(marital_status,hdfmarital_status);
     CSpouseName=ValidateCompare(SpouseName,hdfSpouseName);
     CPartnerName=ValidateCompare(PartnerName,hdfPartnerName);
    CNomineeName=ValidateCompare(NomineeName,hdfNomineeName);
    CNomineeRelation=ValidateCompare(NomineeRelation,hdfNomineeRelation);
      Cchildren=ValidateCompare(children,hdfchildren);

	var obj = {
		gender: gender, marital_status: marital_status, SpouseName: SpouseName, PartnerName: PartnerName, NomineeName: NomineeName, NomineeRelation: NomineeRelation, children: children, Doctype: "PersonalDetails",src:src
	}
if (Cmarital_status == false && CSpouseName==false && CPartnerName==false && CNomineeName==false && CNomineeRelation==false && Cchildren==false ) {
		FailToaster('Personal Details are blank !');
					location.reload();
	}
else{
	$.ajax({
		url: "/Account/updatePersonalDetailsDetailsEmp",
		type: "Get",
		async: true,
		data: obj,
		success: function (data) {

			if (data.ID > 0) {
				SuccessToaster('PersonalDetails  updated successfully');
				location.reload();

			}
			else {
				FailToaster('Can not save.')
			}
		},
		error: function (er) {
			alert(er);
		}
	});
}

}
function UpdateLocal_Update() {

	var lane1 = $('#LocalAddress_lane1').val();
	var zip_code = $('#LocalAddress_zip_code').val();
	var CountryID = $("#LocalAddress_CountryID").find("option:selected").val();
if (typeof CountryID === "undefined") {
  CountryID=0;
}
	var StateId = $("#LocalAddress_StateID").find("option:selected").val();
if (typeof StateId === "undefined") {
  StateId=0;
}
	var CityID = $("#LocalAddress_CityID").find("option:selected").val();
if (typeof CityID === "undefined") {
  CityID=0;
}
	var lane2 = $('#LocalAddress_lane2').val();
    var src = $('#hdfsrc').val();

// check hidden value
      var Clane=true;
      var Czipcode=true;
      var CCountryID=true;
      var CStateId=true;
      var CCityID=true;
       var Clane2=true;
    var hlane1 = $('#hdflane1l').val();
    var hzip_code = $('#hdfzip_codel').val();
	var hCountryID = $("#hdfCountryIDl").val();
	var hStateId = $("#hdfStateIDl").val();
	var hCityID = $("#hdfCityIDl").val();
	var hlane2 = $('#hdflane2l').val();
     Clane=ValidateCompare(lane1,hlane1);
     Czipcode=ValidateCompare(zip_code,hzip_code);
     CCountryID=ValidateCompare(CountryID,hCountryID);
    CStateId=ValidateCompare(StateId,hStateId);
    CCityID=ValidateCompare(CityID,hCityID);
      Clane2=ValidateCompare(lane2,hlane2);
	var obj = {
		lane1: lane1, zip_code: zip_code, CountryID: CountryID, StateId: StateId, CityID: CityID, lane2: lane2, Doctype: "Local",src:src
	}
	if (Clane == false && Czipcode==false && CCountryID==false && CStateId==false && CCityID==false && Clane2==false ) {
		FailToaster('Local address are blank !');
					location.reload();
	}
else{
	$.ajax({
		url: "/Account/updateLocalDetailsDetailsEmp",
		type: "Get",
		async: true,
		data: obj,
		success: function (data) {

			if (data.ID > 0) {
				SuccessToaster('Local address updated successfully');
				location.reload();

			}
			else {
				FailToaster('Can not save.')
			}
		},
		error: function (er) {
			alert(er);
		}
	});
}


}
function UpdatePermanent_Update() {

	var lane1 = $('#PermanentAddress_lane1').val();
	var zip_code = $('#PermanentAddress_zip_code').val();
	var CountryID = $("#PermanentAddress_CountryID").find("option:selected").val();
if (typeof CountryID === "undefined") {
  CountryID=0;
}
	var StateId = $("#PermanentAddress_StateID").find("option:selected").val();
if (typeof StateId === "undefined") {
  StateId=0;
}
	var CityID = $("#PermanentAddress_CityID").find("option:selected").val();
if (typeof CityID === "undefined") {
  CityID=0;
}
	var lane2 = $('#PermanentAddress_lane2').val();
    var src = $('#hdfsrc').val();

	var obj = {
		lane1: lane1, zip_code: zip_code, CountryID: CountryID, StateId: StateId, CityID: CityID, lane2: lane2, Doctype: "Permanent",src:src
	}
// check hidden value
      var Clane=true;
      var Czipcode=true;
      var CCountryID=true;
      var CStateId=true;
      var CCityID=true;
       var Clane2=true;
    var hlane1 = $('#hdflane1').val();
    var hzip_code = $('#hdfzip_code').val();
	var hCountryID = $("#hdfCountryID").val();
	var hStateId = $("#hdfStateID").val();
	var hCityID = $("#hdfCityID").val();
	var hlane2 = $('#hdflane2').val();
     Clane=ValidateCompare(lane1,hlane1);
     Czipcode=ValidateCompare(zip_code,hzip_code);
     CCountryID=ValidateCompare(CountryID,hCountryID);
    CStateId=ValidateCompare(StateId,hStateId);
    CCityID=ValidateCompare(CityID,hCityID);
      Clane2=ValidateCompare(lane2,hlane2);
   	if (Clane == false && Czipcode==false && CCountryID==false && CStateId==false && CCityID==false && Clane2==false ) {
		FailToaster('Permanent address are blank !');
					location.reload();
	}
else{
	$.ajax({
		url: "/Account/updateLocalDetailsDetailsEmp",
		type: "Get",
		async: true,
		data: obj,
		success: function (data) {

			if (data.ID > 0) {
				SuccessToaster('Permanent address updated successfully');
				location.reload();

			}
			else {
				FailToaster('Can not save.')
			}
		},
		error: function (er) {
			alert(er);
		}
	});
}


}
function UpdateMedical_Update() {

	var AnyMedicalCondition = $('#AnyMedicalCondition').val();
	var PhysicianName = $('#PhysicianName').val();
	var SpecialAbility = $("#SpecialAbility").find("option:selected").val();
	var BloodGroup = $("#BloodGroup").find("option:selected").val();
	var PhysicianNumber = $('#PhysicianNumber').val();
	var PhysicianAlternate_No = $('#PhysicianAlternate_No').val();
	var emergContact_Name = $('#emergContact_Name').val();
	var emergContact_no = $('#emergContact_no').val();
	var emergContact_Relation = $('#emergContact_Relation').val();
     var src = $('#hdfsrc').val();
	var obj = {
		emergContact_Relation: emergContact_Relation, emergContact_no: emergContact_no, emergContact_Name: emergContact_Name, AnyMedicalCondition: AnyMedicalCondition, PhysicianName: PhysicianName, SpecialAbility: SpecialAbility, BloodGroup: BloodGroup, PhysicianNumber: PhysicianNumber, PhysicianAlternate_No: PhysicianAlternate_No, Doctype: "Medical",src:src
	}
// check hidden value
       var chkSpecialAbility="";
      var CSpecialAbility=true;
      var CPhysicianName=true;
      var CPhysicianAlternate_No=true;
      var CemergContact_Name=true;
      var CemergContact_no=true;
       var CemergContact_Relation=true;
    var hdfSpecialAbility = $('#hdfSpecialAbility').val();
    var hdfPhysicianName = $('#hdfPhysicianName').val();
	var hdfPhysicianAlternate_No = $("#hdfPhysicianAlternate_No").val();
	var hdfemergContact_Name = $("#hdfemergContact_Name").val();
	var hdfemergContact_no = $("#hdfemergContact_no").val();
	var hdfemergContact_Relation = $('#hdfemergContact_Relation').val();
if(hdfSpecialAbility=="No"){
chkSpecialAbility="0";
}
else{
chkSpecialAbility="1";
}
     CSpecialAbility=ValidateCompare(SpecialAbility,chkSpecialAbility);
     CPhysicianName=ValidateCompare(PhysicianName,hdfPhysicianName);
     CPhysicianAlternate_No=ValidateCompare(PhysicianNumber,hdfPhysicianAlternate_No);
    CemergContact_Name=ValidateCompare(emergContact_Name,hdfemergContact_Name);
    CemergContact_Relation=ValidateCompare(emergContact_Relation,hdfemergContact_Relation);
       	if (CSpecialAbility == false && CPhysicianName==false && CPhysicianAlternate_No==false && CemergContact_Name==false && CemergContact_Relation==false  ) {
		FailToaster('Medical are blank !');
					location.reload();
	}
else{
	$.ajax({
		url: "/Account/updateMedicalDetailsDetailsEmp",
		type: "Get",
		async: true,
		data: obj,
		success: function (data) {

			if (data.ID > 0) {
				SuccessToaster('Medical updated successfully');
				location.reload();

			}
			else {
				FailToaster('Can not save.')
			}
		},
		error: function (er) {
			alert(er);
		}
	});
}

}
function UpdateQuali_Update() {
	var qualifications = new Array();
    var src = $('#hdfsrc').val();
	$("#TableQualificationList TBODY TR").each(function (i) {
		var Qualification = {};
		Qualification.QID = $(this).closest('tr').find(".hdnID").val();
		Qualification.Course = $(this).closest('tr').find(".qual").val();
		Qualification.University = $(this).closest('tr').find(".University").val();
		Qualification.Location = $(this).closest('tr').find(".Location").val();
		Qualification.Year = $(this).closest('tr').find(".Year").val();
        Qualification.src =src;
		qualifications.push(Qualification);

	});
	$.ajax({
		type: "POST",
		url: "/Account/updateQualificationDetailsDetailsEmp",
		data: JSON.stringify(qualifications),
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		success: function (data) {
			if (data.ID > 0) {
				SuccessToaster('Educational and Professional Qualifications updated successfully');
				location.reload();

			}
			else {
				FailToaster('Can not save.')
			}
		},
		error: function (er) {
			alert(er);
		}
	});
}
function UpdateFlyer_Update() {
	var flyer = new Array();
    var src = $('#hdfsrc').val();
	$("#TableAirlinePreferencesList TBODY TR").each(function (i) {

		var Flyer = {};
		Flyer.ID = $(this).closest('tr').find(".hdnID").val();
		Flyer.AirlineName = $(this).closest('tr').find(".AirlineName").val();
		Flyer.FlyerNumber = $(this).closest('tr').find(".FlyerNumber").val();
        Flyer.src = src;
		flyer.push(Flyer);

	});
	$.ajax({
		type: "POST",
		url: "/Account/updateFlyerDetailsDetailsEmp",
		data: JSON.stringify(flyer),
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		success: function (data) {

			if (data.ID > 0) {
				SuccessToaster('Travel Preference updated successfully');
				location.reload();

			}
			else {
				FailToaster('Can not save.')
			}
		},
		error: function (er) {
			alert(er);
		}
	});
}
function UpdatePIO_Update() {
	var PIO = $('#EMPAttachmentsList_7__No').val();
	var PIOName = $('#EMPAttachmentsList_7__Name').val();
	$.ajax({
		type: "Get",
		url: "/Account/updatePIODetailsDetailsEmp",
		data: { PIO: PIO, PIOName: PIOName },
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		success: function (data) {

			if (data.ID > 0) {
				SuccessToaster('Travel Preference updated successfully');

			}
			else {
				FailToaster('Can not save.')
			}
		},
		error: function (er) {
			alert(er);
		}
	});
}
function UpdateOPF_Update() {
	
	var OPF = $('#EMPAttachmentsList_6__No').val();

	$.ajax({
		type: "Get",
		url: "/Account/updateOPFDetailsDetailsEmp",
		data: { OPF: OPF },
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		success: function (data) {

			if (data.ID > 0) {
				SuccessToaster('Travel Preference updated successfully');

			}
			else {
				FailToaster('Can not save.')
			}
		},
		error: function (er) {
			alert(er);
		}
	});
}
function Updateprofessional_Update() {
	var Ammount = $('#EMPAttachmentsList_9__No').val();
	var Remarks = $('#EMPAttachmentsList_9__Remarks').val();
	$.ajax({
		type: "Get",
		url: "/Account/updateProfessinalDetailsDetailsEmp",
		data: { Ammount: Ammount, Remarks: Remarks },
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		success: function (data) {

			if (data.ID > 0) {
				SuccessToaster('Travel Preference updated successfully');

			}
			else {
				FailToaster('Can not save.')
			}
		},
		error: function (er) {
			alert(er);
		}
	});
}

function UpdateVoter_Update() {
	var VoterId = $('#EMPAttachmentsList_2__No').val();
    var src = $('#hdfsrc').val();
	$.ajax({
		type: "Get",
		url: "/Account/updateVotersDetailsEmp",
		data: { VoterId: VoterId,src:src },
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		success: function (data) {

			if (data.ID > 0) {
				SuccessToaster('Voter Id updated successfully');
				location.reload();

			}
			else {
				FailToaster('Can not save.')
			}
		},
		error: function (er) {
			alert(er);
		}
	});
}


function Updatepassport_Update() {

	var PassportNo = $('#EMPAttachmentsList_3__No').val();
	var PassportName = $('#EMPAttachmentsList_3__Name').val();
	var PlaceOfissue = $('#EMPAttachmentsList_3__PlaceOfIssue').val();
	var ExpDate = $('#EMPAttachmentsList_3__ExpiryDate').val();
    var src = $('#hdfsrc').val();
// check hidden value
      var CPassportNo=true;
      var CPassportName=true;
      var CPlaceOfissue=true;
      var CExpDate=true;
     
    var hdfEMPAttachmentsListNo = $('#hdfEMPAttachmentsListNo').val();
    var hdfEMPAttachmentsListName = $('#hdfEMPAttachmentsListName').val();
	var hdfEMPAttachmentsListPlaceOfIssue = $("#hdfEMPAttachmentsListPlaceOfIssue").val();
	var hdfEMPAttachmentsListExpiryDate = $("#hdfEMPAttachmentsListExpiryDate").val();

     CPassportNo=ValidateCompare(PassportNo,hdfEMPAttachmentsListNo);
     CPassportName=ValidateCompare(PassportName,hdfEMPAttachmentsListName);
     CPlaceOfissue=ValidateCompare(PlaceOfissue,hdfEMPAttachmentsListPlaceOfIssue);
     CExpDate=ValidateCompare(ExpDate,hdfEMPAttachmentsListExpiryDate);
   	if (CPassportNo == false && CPassportName==false && CPlaceOfissue==false && CExpDate==false  ) {
		FailToaster('Passport are blank !');
					location.reload();
	}
else{
	$.ajax({
		type: "Get",
		url: "/Account/updatePassportDetailsEmp",
		data: { PassportNo: PassportNo, PassportName: PassportName, PlaceOfissue: PlaceOfissue, ExpDate: ExpDate,src:src },
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		success: function (data) {

			if (data.ID > 0) {
				SuccessToaster('Passport updated successfully');
				location.reload();

			}
			else {
				FailToaster('Can not save.')
			}
		},
		error: function (er) {
			alert(er);
		}
	});
}
}
function UpdateDin_Update() {
	var DinNo = $('#EMPAttachmentsList_4__No').val();
	var DinName = $('#EMPAttachmentsList_4__Name').val();
    var src = $('#hdfsrc').val();
// check hidden value
      var CDinNo=true;
      var CDinName=true;
     
    var hdfEMPAttachmentsListDin = $('#hdfEMPAttachmentsListDin').val();
    var hdfEMPAttachmentsListNameOfDin = $('#hdfEMPAttachmentsListNameOfDin').val();

     CDinNo=ValidateCompare(DinNo,hdfEMPAttachmentsListDin);
     CDinName=ValidateCompare(DinName,hdfEMPAttachmentsListNameOfDin);
     	if (CDinNo == false && CDinName==false   ) {
		FailToaster('Director Identification Number are blank !');
					location.reload();
	}
else{
	$.ajax({
		type: "Get",
		url: "/Account/updateDinDetailsEmp",
		data: { DinNo: DinNo, DinName: DinName,src:src },
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		success: function (data) {

			if (data.ID > 0) {
				SuccessToaster('Director Identification Number updated successfully');
				location.reload();

			}
			else {
				FailToaster('Can not save.')
			}
		},
		error: function (er) {
			alert(er);
		}
	});
}
}

function UpdateDl_Update() {
	var DlNo = $('#EMPAttachmentsList_8__No').val();
	var DlName = $('#EMPAttachmentsList_8__Name').val();
	var IssueDate = $('#EMPAttachmentsList_8__IssueDate').val();
	var ExpDate = $('#EMPAttachmentsList_8__ExpiryDate').val();
	var PlaceOfIssue = $('#EMPAttachmentsList_8__PlaceOfIssue').val();
	var Remarks = $('#EMPAttachmentsList_8__Remarks').val();
    var src = $('#hdfsrc').val();
    var src = $('#hdfsrc').val();
// check hidden value
      var CDlNo=true;
      var CDlName=true;
      var CIssueDate=true;
      var CExpDate=true;
     var CPlaceOfIssue=true;
      var CRemarks=true;
     
    var hdfEMPAttachmentsListDlno = $('#hdfEMPAttachmentsListDlno').val();
    var hdfEMPAttachmentsListDlName = $('#hdfEMPAttachmentsListDlName').val();
    var hdfEMPAttachmentsListDlIssueDate = $('#hdfEMPAttachmentsListDlIssueDate').val();
    var hdfEMPAttachmentsListDlExpiryDate = $('#hdfEMPAttachmentsListDlExpiryDate').val();
    var hdfEMPAttachmentsListDlPlaceOfIssue = $('#hdfEMPAttachmentsListDlPlaceOfIssue').val();
    var hdfEMPAttachmentsListDlRemarks = $('#hdfEMPAttachmentsListDlRemarks').val();

     CDlNo=ValidateCompare(DlNo,hdfEMPAttachmentsListDlno);
     CDlName=ValidateCompare(DlName,hdfEMPAttachmentsListDlName);
    CIssueDate=ValidateCompare(IssueDate,hdfEMPAttachmentsListDlIssueDate);
     CExpDate=ValidateCompare(ExpDate,hdfEMPAttachmentsListDlExpiryDate);
    CPlaceOfIssue=ValidateCompare(PlaceOfIssue,hdfEMPAttachmentsListDlPlaceOfIssue);
     CRemarks=ValidateCompare(Remarks,hdfEMPAttachmentsListDlRemarks);
     	if (CDlNo == false && CDlName==false && CIssueDate == false && CExpDate==false && CPlaceOfIssue == false && CRemarks==false   ) {
		FailToaster('Driving License are blank !');
					location.reload();
	}
else{
	$.ajax({
		type: "Get",
		url: "/Account/updateDlDetailsEmp",
		data: { DlNo: DlNo, DlName: DlName, IssueDate: IssueDate, ExpDate: ExpDate, PlaceOfIssue: PlaceOfIssue, Remarks: Remarks,src:src },
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		success: function (data) {

			if (data.ID > 0) {
				SuccessToaster('Driving License updated successfully');
				location.reload();

			}
			else {
				FailToaster('Can not save.')
			}
		},
		error: function (er) {
			alert(er);
		}
	});
}
}
function UpdateVoterFile_Update() {
	
	 var fileUpload = $("#EMPAttachmentsList_2__Upload").get(0);
	 var files = fileUpload.files;
     var src = $('#hdfsrc').val();
	 var data = new FormData();
	for (var i = 0; i < files.length; i++) {
		data.append(files[i].name, files[i]);
  
	}
	$.ajax({
		
		type: "POST",
        url: '/Account/updateVoterDetailsEmp?src=' + src,
		contentType: false,
		processData: false,
		//data: {data:data,src:src},
		data: data,
		success: function (data) {
			SuccessToaster('Voter Id updated successfully');
			location.reload();
		},
		error: function (er) {
			alert(er);
		}
	});
}
function UpdatePassportFile_Update() {
	var fileUpload = $("#EMPAttachmentsList_3__Upload").get(0);
	var files = fileUpload.files;
    var src = $('#hdfsrc').val();
	var data = new FormData();
	for (var i = 0; i < files.length; i++) {
		data.append(files[i].name, files[i]);
	}
	$.ajax({
		url: '/Account/updatePassportDetailsEmp?src=' + src,
		type: "POST",
		contentType: false,
		processData: false,
		data: data,
		success: function (data) {
			SuccessToaster('Passport file updated successfully');
			location.reload();
		},
		error: function (er) {
			alert(er);
		}
	});
}
function UpdateDlFile_Update() {
	
	var fileUpload = $("#EMPAttachmentsList_6__Upload").get(0);
	var files = fileUpload.files;
    var src = $('#hdfsrc').val();
	var data = new FormData();
	for (var i = 0; i < files.length; i++) {
		data.append(files[i].name, files[i]);
	}
	$.ajax({
		url: '/Account/updateDlDetailsEmp?src=' + src,
		type: "POST",
		contentType: false,
		processData: false,
		data: data,
		success: function (data) {
			SuccessToaster('Driving License file updated successfully');
			location.reload();
		},
		error: function (er) {
			alert(er);
		}
	});
}
function UpdateDinFile_Update() {
  
    var fileUpload = $("#EMPAttachmentsList_6__Upload").get(0);
    var files = fileUpload.files;
    var src = $('#hdfsrc').val();
    var data = new FormData();
    for (var i = 0; i < files.length; i++) {
        data.append(files[i].name, files[i]);
    }
    $.ajax({
        url: '/Account/updateDinDetailsEmp?src=' + src,
        type: "POST",
        contentType: false,
        processData: false,
        data: data,
        success: function (data) {
			SuccessToaster('Director Identification Number file updated successfully');
			location.reload();
        },
        error: function (er) {
            alert(er);
        }
    });
}

function UpdateMeal_Update() {


	 var MealId = $("#MealPreferenceID").find("option:selected").val();
	 var SeatId = $("#SeatPreferencesID").find("option:selected").val();
     var src = $('#hdfsrc').val();
     // check hidden value
      var CSeatPreferencesID =true;
      var CMealPreferenceID=true;
    var hdfSeatPreferencesID = $('#hdfSeatPreferencesID').val();
    var hdfMealPreferenceID = $('#hdfMealPreferenceID').val();
    CMealPreferenceID  =ValidateCompare(MealId,hdfMealPreferenceID);
    CSeatPreferencesID =ValidateCompare(SeatId,hdfSeatPreferencesID);
 	var obj = {
		MealPreferenceID: MealId, SeatPreferencesID: SeatId, Doctype: "MealandSeatPreference",src:src
	}
   	if (CSeatPreferencesID == false && CMealPreferenceID==false   ) {
		FailToaster('Meal and Seat Preference are blank !');
					location.reload();
	}
else{
	$.ajax({
		url: "/Account/updateMealDetailsDetailsEmp",
		type: "Get",
		async: true,
		data: obj,
		success: function (data) {

			if (data.ID > 0) {
				SuccessToaster('Meal and Seat Preference" updated successfully');
				location.reload();

			}
			else {
				FailToaster('Can not save.')
			}
		},
		error: function (er) {
			alert(er);
		}
	});

}
}


function UpdateAdharFile_Update() {

	var fileUpload = $("#EMPAttachmentsList_1__Upload").get(0);
	var files = fileUpload.files;
    var src = $('#hdfsrc').val();
	var data = new FormData();
	for (var i = 0; i < files.length; i++) {
		data.append(files[i].name, files[i]);
	}
	$.ajax({
		url: '/Account/updateAdharDetailsEmp?src=' + src,
		type: "POST",
		contentType: false,
		processData: false,
		data: data,
		success: function (data) {
			SuccessToaster(data);
			location.reload();
		},
		error: function (er) {
			alert(er);
		}
	});
}
function UpdatePanFile_Update() {

	var fileUpload = $("#EMPAttachmentsList_0__Upload").get(0);
	var files = fileUpload.files;
    var src = $('#hdfsrc').val();
	var data = new FormData();
	for (var i = 0; i < files.length; i++) {
		data.append(files[i].name, files[i]);
	}
	$.ajax({
		url: '/Account/updatePanDetailsEmp?src=' + src,
		type: "POST",
		contentType: false,
		processData: false,
		data: data,
		success: function (data) {

			SuccessToaster(data);
			location.reload();
		},
		error: function (er) {
			alert(er);
		}
	});
}
