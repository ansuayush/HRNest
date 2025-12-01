var DropDownTypeEnum = {

    Category: 1,
    FundedBy: 2,
    ProjectCode: 3,
    ProjectName: 4,
    ThematicArea: 5,
    PlaceOfOrigin: 6,
    ProjectLead: 7,
    Tag: 8,
    Document: 9,
    Author: 10,
    Source: 11,   
    ProcurementVenderType:13,
    ProcurementRelationshipMaster: 14,
    ProcurementGoodAndServicesCategory: 15,
    ProcurementPurpose: 16,
    ProcurementBankDetails: 17,
    SubCategory: 101,
    RiskType : 102,
  

};
 
var ManaulTableEnum = {
   
    MasterThematicArea: 1,
    Employee: 2,
    Project: 3,
    MasterLocation:4,
    TagWithThemetic: 5,
    Donar: 6,
    Year: 7,
    TagWithThematic: 8,
    State: 9,
    ProcurementVenderTypeNo: 10,
    ProcurementVenderTypeYes: 11,
    EmployeeWithoutLWD: 12,
    Vendor:13,
    Designation: 14,
    Department: 63,
};

function HideErrorMessage(ctrl) {
    $('#sp' + ctrl.id).hide(); 
}
function checkValidationOnSubmitWithoutMessage(Mandatory) {

    var x = document.getElementsByClassName(Mandatory);
    x.innerHTML = '';
    var isMandatory = true;
    for (var i = 0; i < x.length; i++) {
        var extactValue = jQuery.trim(x[i].value);
        if ((extactValue == "") || (extactValue == "Select") ) 
        {
            if (extactValue == "Select")
            {
                var ctrlid = 'select2-' + x[i].id + '-container';
                var ctrl = document.getElementById(ctrlid);
                ctrl.classList.add("errorValidation");
            }
            else {
                 
                x[i].classList.add("errorValidation");
            }
           
            isMandatory = false;
        }
        
    }
    if (!isMandatory) {
        FailToaster("Hey, You missed this field! Please check the highlighted fileds.");
        return false;
    }
    return isMandatory;
}
//This function is used to set comma in numbes
function NumberWithComma(inputNo)
{
    if (inputNo == null) {
        return '';
    }
    else {
        return inputNo.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',')
       //return inputNo.toLocaleString('en-US'); 
    }
    
}

function checkValidationOnSubmit(Mandatory) {

    var x = document.getElementsByClassName(Mandatory);
    x.innerHTML = '';
    var isMandatory = true;
    for (var i = 0; i < x.length; i++)
    {
        var extactValue= jQuery.trim(x[i].value);
        if ((extactValue == "") || (extactValue == "Select") || (extactValue == "Choose Your Category"))
        {
            
            if ($('#dv' + x[i].id).length > 0)
            {
                if ($('#dv' + x[i].id).css('display')!= 'none') {

                    $('#sp' + x[i].id).show();
                    x[i].classList.add("errorValidation");
                    isMandatory = false;
                }
                
            }
            else {
                $('#sp' + x[i].id).show();
                x[i].classList.add("errorValidation");
                isMandatory = false;
            }
           
        }
        else
        {
            x[i].classList.remove("errorValidation");  
            $('#sp' + x[i].id).hide();       
        }
    }
    if (!isMandatory)
    {
        //var mobileerrormsg = "Hey, You missed this field! (*)..";
        //var x = document.getElementById("toastmsg");
        //x.innerHTML = '<p class="yellow">' + mobileerrormsg + '!</p>';
        ////var x = document.getElementById("toastmsg");
        //x.className = "show";
        //setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
        return false;
    }
    return isMandatory;
}

function getErrorMessage(request, status) {

    if (request.status === 0)
    {
        return ('We are not able to communicate to  server.\nPlease verify your network connection and ensure  you are connected to internet.');
    } else if (request.status==404) {
        return ('The requested page not found. [404]');
    } else if (request.status == 500) {
        return ('Internal Server Error [500].');
    } else if (status === 'parsererror') {
        return ('Requested JSON parse failed.');
    } else if (status === 'timeout') {
        return ('Time out error.');
    } else if (status === 'abort') {
        return ('Ajax request aborted.');
    } else
    {
        return ('Uncaught Error.\n' + request.responseText);
    }
}
function handleError(request, status, error) {

   // alert(getErrorMessage(request, status));
    var x = document.getElementById("hReturnMessage");

}

function handleErrorResponse(response) {
    if (response.IsSuccessStatusCode == false)
    {
       // alert(response.ErrorMessage);
        
        var x = document.getElementById("hReturnMessage");
    }

}

function BindTarget()
{

    CloseLoadingDialog();
   
    
}

function CommonAjaxMethod(actionMethodUrl, data, methodType, successCallBack, isPost) {
    var isSuccess = false;

    var dataObj;
    if (methodType == "GET") {
        dataObj = data;
    }
    else {
        dataObj = JSON.stringify(data);
    }

    $.ajax({
        async: false,
        type: methodType,
        url: actionMethodUrl,
        data: dataObj,
        contentType: 'application/json',
        success: function (res) {

            var response = JSON.parse(res);
            var d = actionMethodUrl;
            if (response.CustumException == "" || response.CustumException == null || response.CustumException == undefined) {

                if (successCallBack && methodType == "POST" && isPost == true) {
                    if (response.ErrorMessage == "" || response.ErrorMessage == null || response.ErrorMessage == undefined) {
                        var responseObj =
                        {
                            data: response
                        }
                        successCallBack(responseObj);
                        return;
                    }
                    else {
                        if (response.ErrorMessage != 'amendmendamount') {
                            if (response.ErrorMessage != "")
                                SuccessToaster(response.ErrorMessage);
                        }
                        //document.getElementById('hReturnMessage').innerHTML = response.ErrorMessage;
                        //$('#btnShowModel').click();
                        //x.innerHTML = '<p class="green">' + response.ErrorMessage + '!</p>';
                        //x.className = "show";
                        //setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
                    }
                }
                else if (successCallBack && methodType == "GET") {
                    if (response.ErrorMessage == "" || response.ErrorMessage == null || response.ErrorMessage == undefined) {
                        var responseObj =
                        {
                            data: response
                        }
                        successCallBack(responseObj);
                        return;
                    }
                    else {


                        if (response.ErrorMessage != 'amendmendamount') {
                            if (response.ErrorMessage != "")
                                SuccessToaster(response.ErrorMessage);
                        }
                        //document.getElementById('hReturnMessage').innerHTML = response.ErrorMessage;
                        //$('#btnShowModel').click();
                        //x.innerHTML = '<p class="green">' + response.ErrorMessage + '!</p>';
                        //x.className = "show";
                        //setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
                    }
                }
                else {

                    processMethodType(methodType, response.ErrorMessage, response.ValidationInput);

                    if (successCallBack) {
                        successCallBack(response);
                    }
                    //This method should be declared in calling page js to get result.
                    //  FormControlValueClear(true);

                }
            }
            else {
                // var x = document.getElementById("hReturnMessage");
                FailToaster(response.CustumException);

                //document.getElementById('hReturnMessage').innerHTML = response.CustumException;
                //$('#btnShowModel').click();
                //  x.innerHTML = '<p class="green">' + response.CustumException + '!</p>';
                // x.className = "show";
                //setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
            }

        },
        error: function (request, status, error) {
            handleError(request, status, error);
            isSuccess = false;
        }
    });
    return isSuccess;
}




function processMethodType(methodType, customMessage, ValidationInput) {

    if (methodType == "POST") {
        if (customMessage != 'amendmendamount') {
            if (customMessage != "")
                if (ValidationInput == 1) {
                    SuccessToaster(customMessage);
                }
                else {
                    FailToaster(customMessage);
                }
        }

        //alert('Successfully Saved');
        //var x = document.getElementById("hReturnMessage");

        //document.getElementById('hReturnMessage').innerHTML = customMessage;
        //$('#btnShowModel').click();
        // x.innerHTML = '<p class="green">Saved successfully!</p>';
        // x.innerHTML = '<p class="green">' + customMessage+'!</p>';
        // x.className = "show";
        // setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
    }
    else if (methodType == "PUT") {
        SuccessToaster(customMessage);
        //var x = document.getElementById("hReturnMessage");
        //document.getElementById('hReturnMessage').innerHTML = customMessage;
        //$('#btnShowModel').click();
        //x.innerHTML = '<p class="green">' + customMessage + '!</p>';
        // x.className = "show";
        // setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
    }
    else if (methodType == "DELETE") {
        SuccessToaster(customMessage);
        //var x = document.getElementById("hReturnMessage");
        //document.getElementById('hReturnMessage').innerHTML = customMessage;
        //$('#btnShowModel').click();
        // x.innerHTML = '<p class="green">' + customMessage + '!</p>';
        // x.className = "show";
        // setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
    }

}

var specialKeys = new Array();
specialKeys.push(8);  //Backspace
specialKeys.push(9);  //Tab
specialKeys.push(46); //Delete
specialKeys.push(36); //Home
specialKeys.push(35); //End
specialKeys.push(37); //Left
specialKeys.push(39); //Right

function ValidateInput(e)
{
    var k = e.keyCode;
    return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || (k==32)|| (k >= 48 && k <= 57));


}

function GetMonth(monthInitial, returnMonthOrNum) {
    var arrMonth = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
    for (var iCount = 0; iCount < arrMonth.length; iCount++) {
        if (monthInitial == arrMonth[iCount] && returnMonthOrNum == "Num") {
            return iCount;
        }
        if (monthInitial == iCount && returnMonthOrNum == "Month") {
            return arrMonth[iCount];
        }
    }
}
function ChangeDateFormat(MyDate)
{
    var strDate = "";
    if (MyDate == '1900-01-01T00:00:00') {
        MyDate = "";
    }

    if (MyDate != "")
    {
        var myArray = MyDate.split("-");
        if (myArray.length > 2) {
            var mm = "";
            if (myArray[1] == "01")
                mm = "Jan";
            else if (myArray[1] == "02")
                mm = "Feb";
            else if (myArray[1] == "03")
                mm = "Mar";
            else if (myArray[1] == "04")
                mm = "Apr";
            else if (myArray[1] == "05")
                mm = "May";
            else if (myArray[1] == "06")
                mm = "Jun";
            else if (myArray[1] == "07")
                mm = "Jul";
            else if (myArray[1] == "08")
                mm = "Aug";
            else if (myArray[1] == "09")
                mm = "Sep";
            else if (myArray[1] == "10")
                mm = "Oct";
            else if (myArray[1] == "11")
                mm = "Nov";
            else if (myArray[1] == "12")
                mm = "Dec";
            strDate = myArray[0] + '-' + mm + '-' + myArray[2];
        }
    }
 
    return strDate;


}
function ChangeDateFormatToddMMYYY( dt)
{    
    var MyDateString = "";
    if (dt == '1900-01-01T00:00:00') {
        dt = null;
    }

    if (dt == null || dt == "") {
        MyDateString = "";
    }
    else {


        // MyDate.setDate(MyDate.getDate());
        var MyDate = new Date(dt);
        MyDateString = ('0' + MyDate.getDate()).slice(-2) + '-' + ('0' + (MyDate.getMonth() + 1)).slice(-2) + '-' + MyDate.getFullYear();

    }
    return MyDateString;


}
function ChangeDateFormatToddMMYYYWithSlace(dt) {
    var MyDateString = "";
    if (dt == '1900-01-01T00:00:00') {
        MyDate = "";
    }

    if (dt == null || dt == "") {
        MyDateString = "";
    }
    else {


        // MyDate.setDate(MyDate.getDate());
        var MyDate = new Date(dt);
        MyDateString = ('0' + MyDate.getDate()).slice(-2) + '/' + ('0' + (MyDate.getMonth() + 1)).slice(-2) + '/' + MyDate.getFullYear();

    }
    return MyDateString;


}
function getdate(strdate) {
    var strRetrunDate;
    if (strdate == '1900-01-01T00:00:00') {
        strdate = null;
    }
    if (strdate != null) {
        var dateData = new Date(parseInt(strdate.replace("/Date(", "").replace(")/")));
        var dd = dateData.getDate();
        var mm = dateData.getgetMonth() + 1; //January is 0!
        var yyyy = dateData.getFullYear();
        if (dd < 10) { dd = '0' + dd }
        if (mm < 10) { mm = '0' + mm }
        strRetrunDate = mm + '-' + dd + '-' + yyyy;
    }
    return strRetrunDate;
}
function getdatetimewithoutJson(strdate)
{
 ;
    var strRetrunDate = "";
if(strdate!=null)
{
    var dt = new Date(parseInt(Date.parse(strdate)));
    var dd = dt.getDate();
    var mmNo = dt.getMonth() + 1; //January is 0!

         var mm = "";
        if (mmNo == 1)
            mm = "Jan";
        else if (mmNo == 2)
            mm = "Feb";
        else if (mmNo == 3)
            mm = "Mar";
        else if (mmNo == 4)
            mm = "Apr";
        else if (mmNo == 5)
            mm = "May";
        else if (mmNo == 6)
            mm = "Jun";
        else if (mmNo == 7)
            mm = "Jul";
        else if (mmNo == 8)
            mm = "Aug";
        else if (mmNo == 9)
            mm = "Sep";
        else if (mmNo == 10)
            mm = "Oct";
        else if (mmNo == 11)
            mm = "Nov";
        else if (mmNo == 12)
            mm = "Dec";
    var yyyy = dt.getFullYear();
    if (dd < 10) { dd = '0' + dd }
    if (mm < 10) { mm = '0' + mm }


    var hours = dt.getHours();
    var minutes = dt.getMinutes();
    var sec = dt.getSeconds();
    var ampm = hours >= 12 ? 'PM' : 'AM';
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
    hours = hours < 10 ? '0' + hours : hours;
    minutes = minutes < 10 ? '0' + minutes : minutes;
    sec = sec < 10 ? '0' + sec : sec;
    var strTime = hours + ':' + minutes + ':' + sec+ ' ' + ampm;

    // strRetrunDate = yyyy + '-' + mm + '-' + dd + " " + strTime;
   return strRetrunDate = dd + '-' + mm + '-' + yyyy + " " + strTime;
}

else
{
return strRetrunDate 
  
}
}
function getdatetime(strdate)
{

    if (strdate == '1900-01-01T00:00:00') {
        strdate = null;
    }

    var strRetrunDate;
    if (strdate != null) {
        var dateData = new Date(parseInt(strdate.replace("/Date(", "").replace(")/")));
        var dd = dateData.getDate();
        var mm = dateData.getMonth() + 1; //January is 0!
        var yyyy = dateData.getFullYear();
        if (dd < 10) { dd = '0' + dd }
        if (mm < 10) { mm = '0' + mm }


        var hours = dateData.getHours();
        var minutes = dateData.getMinutes();
        var ampm = hours >= 12 ? 'PM' : 'AM';
        hours = hours % 12;
        hours = hours ? hours : 12; // the hour '0' should be '12'
        minutes = minutes < 10 ? '0' + minutes : minutes;
        var strTime = hours + ':' + minutes + ' ' + ampm;

       // strRetrunDate = yyyy + '-' + mm + '-' + dd + " " + strTime;
        strRetrunDate = dd + '-' + mm + '-' + yyyy + " " + strTime;
    }
    return strRetrunDate;
}

function CommonApprovalUploadWorkFlow(cateId, grade,docType,isPM)
{
            var isValid = false;
     
            if (docType == 'Not Critical and not confidential')
            {
                isValid = true;               
                $('#hdnApprovar').val("S");
            }
            else if (docType == 'Critical and not confidential')
            {
                var pld = ['A', 'B', 'C', 'D'];    
                if (cateId == 'pld') {
                    if (pld.filter((n) =>n==grade).length > 0) {
                        isValid = true;                       
                        $('#hdnApprovar').val("E");
                    }
                }
                var cnts = ['A', 'B', 'C', 'D'];
                if (cateId == 'cnts') {
                    if (cnts.filter((n) =>n==grade).length > 0) {
                        isValid = true;
                        $('#hdnApprovar').val("S");
                    }

                }
                var pd = ['A', 'B', 'C', 'D'];

                if (cateId == 'pd') {
                    if (pd.filter((n) =>n==grade).length > 0) {
                        isValid = true;
                        $('#hdnApprovar').val("P");
                    }
                }
                var eb = ['A', 'B', 'C', 'D', 'E'];
                if (cateId == 'eb') {
                    if (eb.filter((n) =>n==grade).length > 0) {
                        isValid = true;
                        $('#hdnApprovar').val("S");
                    }
                }
                var em = ['A', 'B', 'C', 'D', 'E'];
                if (cateId == 'em') {
                    if (em.filter((n) =>n==grade).length > 0) {
                        isValid = true;
                        $('#hdnApprovar').val("S");
                    }
                }
                var ee = ['A', 'B', 'C', 'D', 'E'];
                if (cateId == 'ee') {
                    if (ee.filter((n) =>n==grade).length > 0) {
                        isValid = true;
                        $('#hdnApprovar').val("S");
                    }
                }
                var ems = ['A', 'B', 'C', 'D', 'E'];

                if (cateId == 'ems') {
                    if (ems.filter((n) =>n==grade).length > 0) {
                        isValid = true;
                        $('#hdnApprovar').val("S");
                    }
                }

                var ea = ['A', 'B', 'C', 'D', 'E'];
                if (cateId == 'ea') {
                    if (ea.filter((n) =>n==grade).length > 0) {
                        isValid = true;
                        $('#hdnApprovar').val("S");
                    }
                }

                var pbn = ['A', 'B', 'C', 'D' ,'E'];
                if (cateId == 'pbn') {
                    if (pbn.filter((n) =>n==grade).length > 0) {
                        isValid = true;
                       
                        $('#hdnApprovar').val("P");
                    }
                }

                var tm = ['A', 'B', 'C', 'D'];
                if (cateId == 'tm') {
                    if (tm.filter((n) =>n==grade).length > 0) {
                        isValid = true;
                        $('#hdnApprovar').val("P");
                    }
                }

                var cc = ['A', 'B', 'C', 'D', 'E'];
                if (cateId == 'cc') {
                    if (cc.filter((n) =>n==grade).length > 0) {
                        isValid = true;
                        $('#hdnApprovar').val("S");
                    }
                }
                var ce = ['A', 'B', 'C', 'D', 'E'];

                if (cateId == 'ce') {
                    if (ce.filter((n) =>n==grade).length > 0) {
                        isValid = true;
                        $('#hdnApprovar').val("S");
                    }
                }
                var hr = ['A', 'B', 'C', 'D', 'E'];

                if (cateId=='hr') {
                    if (hr.filter((n) =>n==grade).length > 0) {
                        isValid = true;
                        $('#hdnApprovar').val("S");
                    }
                }
                
                var bdg = ['A', 'B'];

                if (cateId=='bdg') {
                    if (bdg.filter((n) =>n==grade).length > 0) {
                        isValid = true;
                        $('#hdnApprovar').val("S");
                    }
                }

                var brd = ['A', 'B'];
                if (cateId=='brd') {
                    if (brd.filter((n) =>n==grade).length > 0) {
                        isValid = true;
                        $('#hdnApprovar').val("S");
                    }
                }
                var dnr = ['A', 'B', 'C'];
                if (cateId=='dnr') {
                    if (dnr.filter((n) =>n== grade).length > 0) {
                        isValid = true;
                        $('#hdnApprovar').val("S");
                    }
                }
                var bsd = ['A', 'B', 'C', 'D'];

                if (cateId=='bsd') {
                    if (bsd.filter((n) =>n==grade).length > 0) {
                        isValid = true;
                        $('#hdnApprovar').val("S");
                    }
                }
                
               


            }
            else if (docType=='Critical and confidential')
            {               


                var pld = ['A', 'B', 'C', 'D'];

                if (cateId=='pld') {
                    if (pld.filter((n) =>n==grade).length > 0) {
                        isValid = true;
                        
                        $('#hdnApprovar').val("E");
                    }
                }

                var cnts = ['A', 'B']; //PM
                if (cateId=='cnts')
                {
                    if (cnts.filter((n) =>n==grade).length > 0) {
                        isValid = true;
                        $('#hdnApprovar').val("S");
                    }
                    else if (isPM==true) {
                        isValid = true;
                        $('#hdnApprovar').val("S");
                    }
                }

                var pd = ['A', 'B'];

                if (cateId=='pd') {
                    

                    if (pd.filter((n) =>n==grade).length > 0) {
                        isValid = true;
                        $('#hdnApprovar').val("E");
                    }
                    else if (isPM==true) {
                        isValid = true;
                        $('#hdnApprovar').val("E");
                    }
                }

                var eb = ['A', 'B'];

                if (cateId=='eb') {
                    if (eb.filter((n) =>n==grade).length > 0) {
                        isValid = true;
                        $('#hdnApprovar').val("S");
                    }
                }

                var em = ['A', 'B'];
                if (cateId=='em') {
                    if (em.filter((n) =>n==grade).length > 0) {
                        isValid = true;
                        $('#hdnApprovar').val("S");
                    }
                }
                var ee = ['A', 'B'];
                if (cateId=='ee') {
                    if (ee.filter((n) =>n==grade).length > 0) {
                        isValid = true;
                        $('#hdnApprovar').val("S");
                    }
                }
                var ems = ['A', 'B'];

                if (cateId=='ems') {
                    if (ems.filter((n) =>n==grade).length > 0) {
                        isValid = true;
                        $('#hdnApprovar').val("S");
                    }
                }

                var ea = ['A', 'B'];//PM

                if (cateId == 'ea') {
                    if (ea.filter((n) => n == grade).length > 0) {
                        isValid = true;
                        $('#hdnApprovar').val("S");
                    }
                }

                 

                var pbn = ['A', 'B'];//PM

                if (cateId=='pbn') {
                    if (pbn.filter((n) =>n==grade).length > 0)
                    {
                        isValid = true;
                        $('#hdnApprovar').val("E");
                    }
                    else if (isPM==true)
                    {
                        isValid = true;
                        $('#hdnApprovar').val("E");
                    }
                }

                var tm = ['A', 'B'];
                if (cateId == 'tm') {
                    if (tm.filter((n) => n == grade).length > 0) {
                        isValid = true;
                        $('#hdnApprovar').val("S");
                    }
                    else if (isPM == true) {
                        isValid = true;
                        $('#hdnApprovar').val("S");
                    }
                }
                var cc = ['A', 'B', 'C'];

                if (cateId=='cc') {
                    if (cc.filter((n) =>n==grade).length > 0) {
                        isValid = true;
                        $('#hdnApprovar').val("S");
                    }
                }

                var ce = ['A', 'B','C'];
                if (cateId=='ce') {
                    if (ce.filter((n) =>n==grade).length > 0) {
                        isValid = true;
                        $('#hdnApprovar').val("S");
                    }
                }
                var hr = ['A', 'B'];

                if (cateId=='hr') {
                    if (hr.filter((n) =>n==grade).length > 0) {
                        isValid = true;
                        $('#hdnApprovar').val("S");
                    }
                }

                var bdg = ['A', 'B'];

                if (cateId=='bdg') {
                    if (bdg.filter((n) =>n==grade).length > 0) {
                        isValid = true;
                        $('#hdnApprovar').val("S");
                    }
                }
                var brd = ['A', 'B'];

                if (cateId=='brd') {
                    if (brd.filter((n) =>n==grade).length > 0) {
                        isValid = true;
                        $('#hdnApprovar').val("E");
                    }
                }
                var dnr = ['A', 'B'];

                if (cateId=='dnr') {
                    if (dnr.filter((n) =>n==grade).length > 0) {
                        isValid = true;
                        $('#hdnApprovar').val("E");
                    }
                }

                var bsd = ['A', 'B'];

                if (cateId=='bsd') {
                    if (bsd.filter((n) =>n==grade).length > 0) {
                        isValid = true;
                        $('#hdnApprovar').val("E");
                    }
                }


            }          
       

    return isValid;
}

function CommonApprovalDownloadWorkFlow(cateId, grade, docType)
{
        //<option>Critical and confidential</option>
		//<option>Critical and not confidential</option>
	    //<option>Not Critical and not confidential</option>

        //Proposal Document	pld
        //Contracts	cnts
        //Project Document	pd
        //Evaluation Baseline	eb
        //Evaluation Midline	em
        //Evaluation Endline	ee
        //Monitoring, ems
        //Project Evaluation / Assessment	ea
        //Publication	pbn
        //Capacity building modules / Training Documents	tm
        //Communication Collateral 	cc
        //Event related Communication	ce
        //Human Resources	hr
        //Budget	bdg
        //Board	brd
        //Donor	dnr
        //Business Development	bsd

}
/*
Function Name               : ValidatePasteNumeric()
Parameter(s)                : Allows numeric paste only.
Created By / On DateTime    : Jiya / 21 Aug 2019
Modified By / On DateTime   :
Description                 : This function allows only numeric values in the CONTROL.
*/
function ValidatePasteNumeric(isDecimal, oTextBox) {
    var emailErrMsg = '';
    if (isDecimal) {
        regex = /^\d+(\.\d{1,2})?$/;
        emailErrMsg = 'Please enter only numeric/decimal values';
    }
    else {
        regex = /^[\d]+$/;
        emailErrMsg = 'Please enter only numeric values';
    }
    var $this = $(oTextBox);
    setTimeout(function () {
        var val = $this.val();

        if (regex.test(val)) {
            $this.val(val);
        }
        else {
            $this.val('');
            FailToaster(emailErrMsg);
        }

    }, 0);



}

function ClearInvalidText(oTextBox) {
    if (!IsNumeric(oTextBox.value, false)) {
        oTextBox.value = "";
        oTextBox.focus();
    }


}

/*
Used to allow only numeric data to be entered.
This method need to be called on keyPress event of an object in which only numeric data is allowed.
isDecimal : Pass true if number needs to be added in decimal format eg ##.##. Pass false if only numeric data needs to be added.
fieldName: Name of the control that is getting validated.
*/
function AllowNumbersOnly(isDecimal, oTextBox, emailErrMsg) {
    //Here 46 key code is for dot(.) entered.
    //48 - 57 is for numbers between 0 - 9.
    if (isDecimal && event.keyCode == 46)//If decimal number is allowed then allow dot(.) to be entered.
    {
        return true;
    }
    if (event.keyCode >= 48 && event.keyCode <= 57)//Allow only number to be entered.
    {
        return true;
    }
    
    FailToaster(emailErrMsg);

    oTextBox.focus();
    return false;
}
/*
Function Name               : IsNumeric(sText)
Parameter(s)                : sText - String to be checked. Allows numeric paste only.
Created By / On DateTime    : Jiya / 21 Aug 2019
Modified By / On DateTime   :
Description                 : This function checks if the text has all numbers.
*/
function IsNumeric(sText, isDecimal) {
    var ValidChars = "";
    if (isDecimal) {
        ValidChars = "0123456789.";
    }
    else {
        ValidChars = "0123456789";
    }
    var IsNumber = true;
    var Char;

    for (i = 0; i < sText.length && IsNumber == true; i++) {
        Char = sText.charAt(i);
        if (ValidChars.indexOf(Char) == -1) {
            IsNumber = false;
        }
    }
    return IsNumber;
}

 

//Used to validate entered date is not greater than current date.
//dateText		: in the format dd-MMM-yyyy.
//dateFieldName : name of the date field that needs to be validated.
function IsGreaterThanCurrentDate(dText, tDate, message) {
 
    var todaysDate = new Date(tDate);
    var dateText = new Date(dText);
    var arrTodaysDate = new Array();

    var arrFromdaysDate = new Array();

    arrTodaysDate[0] = todaysDate.getDate();
    arrTodaysDate[1] = GetMonth(todaysDate.getMonth(), "Month");
    arrTodaysDate[2] = todaysDate.getFullYear();

    arrFromdaysDate[0] = dateText.getDate();
    arrFromdaysDate[1] = GetMonth(dateText.getMonth(), "Month");
    arrFromdaysDate[2] = dateText.getFullYear();

    daysDiff = GetDateDiffInDays(arrFromdaysDate, arrTodaysDate);
    if (daysDiff < 0 || daysDiff == 0)
    {
        FailToaster(message);   
        //alert("The " + dateFieldName + " Date cannot be greater than Todays Date.");
        return false;
    }
    return true;
}
function IsGreaterThanFromAndToDate(dText, tDate, message) {

    var todaysDate = new Date(tDate);
    var dateText = new Date(dText);
    var arrTodaysDate = new Array();

    var arrFromdaysDate = new Array();

    arrTodaysDate[0] = todaysDate.getDate();
    arrTodaysDate[1] = GetMonth(todaysDate.getMonth(), "Month");
    arrTodaysDate[2] = todaysDate.getFullYear();

    arrFromdaysDate[0] = dateText.getDate();
    arrFromdaysDate[1] = GetMonth(dateText.getMonth(), "Month");
    arrFromdaysDate[2] = dateText.getFullYear();

    daysDiff = GetDateDiffInDays(arrFromdaysDate, arrTodaysDate);
    if (daysDiff < 0 ) {
        FailToaster(message);
        //alert("The " + dateFieldName + " Date cannot be greater than Todays Date.");
        return false;
    }
    return true;
}
//Get the number of day bettween fromDate and toDate passed.
//fromDateText: in the format dd-MMM-yyyy.
//toDateText: in the format dd-MMM-yyyy.
function GetDateDiffInDays(fromDateText, toDateText) {
    var arrFromDate = fromDateText;
    var arrToDate = toDateText;
    var fromDate = null;
    var toDate = null;
    var hrs = 0, min = 0, sec = 0, miliSec = 0;

    fromDate = new Date(arrFromDate[2], GetMonth(arrFromDate[1], "Num"), arrFromDate[0], hrs, min, sec, miliSec);
    toDate = new Date(arrToDate[2], GetMonth(arrToDate[1], "Num"), arrToDate[0], hrs, min, sec, miliSec);

    var difference = toDate.getTime() - fromDate.getTime();
    var daysDiff = Math.floor(difference / 1000 / 60 / 60 / 24);

    return daysDiff; //if fromDate is greater than toDate then a negative value is returned else positive value is returned.
}
//Get the number of milliseconds bettween fromDate, fromHrs, fromMin and toDate, toHrs, toMin passed.
//fromDateText: in the format dd-MMM-yyyy.
//fromHrsText: hours selected e.g. 1.
//fromMinText: minutes selected e.g. 1.
//toDateText: in the format dd-MMM-yyyy.
//toHrsText: hours selected e.g. 1.
//toMinText: minutes selected e.g. 1.
function GetDateDiffInMilliseconds(fromDateText, fromHrsText, fromMinText, toDateText, toHrsText, toMinText) {
    var arrFromDate = fromDateText.split("-");
    var arrToDate = toDateText.split("-");
    var fromDate = null;
    var toDate = null;
    var sec = 0, miliSec = 0;

    fromDate = new Date(arrFromDate[2], GetMonth(arrFromDate[1], "Num"), arrFromDate[0], fromHrsText, fromMinText, sec, miliSec);
    toDate = new Date(arrToDate[2], GetMonth(arrToDate[1], "Num"), arrToDate[0], toHrsText, toMinText, sec, miliSec);

    var difference = toDate.getTime() - fromDate.getTime();
    return difference; //if fromDate is greater than toDate then a negative value is returned else positive value is returned.
}
//Here the first argument is monthInitial i.e. the month in initial 3 letters or number to be passed.
//returnMonthOrNum should be "Month" to return month initial else "Num" if month Index needs to be returned.
function GetMonth(monthInitial, returnMonthOrNum) {
    var arrMonth = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
    for (var iCount = 0; iCount < arrMonth.length; iCount++) {
        if (monthInitial == arrMonth[iCount] && returnMonthOrNum == "Num") {
            return iCount;
        }
        if (monthInitial == iCount && returnMonthOrNum == "Month") {
            return arrMonth[iCount];
        }
    }
}

$('body').on('focus', ".datepicker1", function () {
    $(this).datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: "dd-mm-yy",
        yearRange: "-90:+10"
    });
});

function validate(evt) {
    var theEvent = evt || window.event;

    // Handle paste
    if (theEvent.type === 'paste') {
        key = event.clipboardData.getData('text/plain');
    } else {
        // Handle key press
        var key = theEvent.keyCode || theEvent.which;
        key = String.fromCharCode(key);
    }
    var regex = /[0-9]|\./;
    if (!regex.test(key)) {
        theEvent.returnValue = false;
        if (theEvent.preventDefault) theEvent.preventDefault();
    }
}

function ChangeDateFormatWithSlace(MyDate) {
    var strDate = "";
    if (MyDate == '1900-01-01T00:00:00') {
        MyDate = "";
    }
    if (MyDate != "") {
        var myArray = MyDate.split("/");
        if (myArray.length > 2) {
            var mm = "";
            if (myArray[1] == "01")
                mm = "Jan";
            else if (myArray[1] == "02")
                mm = "Feb";
            else if (myArray[1] == "03")
                mm = "Mar";
            else if (myArray[1] == "04")
                mm = "Apr";
            else if (myArray[1] == "05")
                mm = "May";
            else if (myArray[1] == "06")
                mm = "Jun";
            else if (myArray[1] == "07")
                mm = "Jul";
            else if (myArray[1] == "08")
                mm = "Aug";
            else if (myArray[1] == "09")
                mm = "Sep";
            else if (myArray[1] == "10")
                mm = "Oct";
            else if (myArray[1] == "11")
                mm = "Nov";
            else if (myArray[1] == "12")
                mm = "Dec";
            strDate = myArray[0] + '-' + mm + '-' + myArray[2];
        }
    }

    return strDate;


}

function ChangeDateFormatWithFullMonthName(MyDate) {
    var strDate = "";
    if (MyDate == '1900-01-01T00:00:00') {
        MyDate = "";
    }
    if (MyDate != "") {
        var myArray = MyDate.split("-");
        if (myArray.length > 2) {
            var mm = "";
            if (myArray[1] == "01")
                mm = "January";
            else if (myArray[1] == "02")
                mm = "February";
            else if (myArray[1] == "03")
                mm = "March";
            else if (myArray[1] == "04")
                mm = "April";
            else if (myArray[1] == "05")
                mm = "May";
            else if (myArray[1] == "06")
                mm = "June";
            else if (myArray[1] == "07")
                mm = "July";
            else if (myArray[1] == "08")
                mm = "August";
            else if (myArray[1] == "09")
                mm = "September";
            else if (myArray[1] == "10")
                mm = "October";
            else if (myArray[1] == "11")
                mm = "November";
            else if (myArray[1] == "12")
                mm = "December";
            strDate = myArray[0] + '-' + mm + '-' + myArray[2];
        }
    }

    return strDate;


}

function DatatableScriptsWithColumnSearch(TableID, table) {
    $('#' + TableID + ' thead tr:eq(1) th').each(function () {
        var title = $('#' + TableID + ' thead tr:eq(0) th').eq($(this).index()).text();
        if (!$(this).hasClass('searchhide')) {
            $(this).html('<input type="text" placeholder="Search" class="search form-control"/>');
        }
    });

    table.columns().every(function (index) {
        $('#' + TableID + ' thead tr:eq(1) th:eq(' + index + ') input').on('keyup change', function () {
            table.column($(this).parent().index() + ':visible')
                .search(this.value)
                .draw();
        });
    });

   
}

function initCompleteCallback(TableID) {
    var table = $('#' + TableID).DataTable();

    if (table) {
        // Save search input values for each column
        var searchValues = [];
        table.columns().every(function (index) {
            var searchValue = this.search() || "";
            searchValues.push(searchValue);
        });

        // Restore search input values after table initialization
        table.on('draw', function () {
            table.columns().every(function (index) {
                var column = this;
                var searchValue = searchValues[index];

                // Find the input in the header for this specific column index
                var headerInput = $('#' + TableID + ' thead tr:eq(1) th:eq(' + index + ') input.search');

                // Set the search value in the header input
                if (headerInput.length > 0) {
                    headerInput.val(searchValue);
                }
            });
        });
    }
}

document.addEventListener('DOMContentLoaded', function () {
    var numericInputs = document.querySelectorAll('.numeric-only');

    numericInputs.forEach(function (numericInput) {
        numericInput.addEventListener('keydown', function (event) {
            // Allow: backspace, delete, tab, escape, enter, and arrows
            if (
                event.key === 'Backspace' ||
                event.key === 'Delete' ||
                event.key === 'Tab' ||
                event.key === 'Escape' ||
                event.key === 'Enter' ||
                (event.key === 'ArrowLeft' || event.key === 'Control' || event.key === 'ArrowRight' || event.key === 'ArrowUp' || event.key === 'ArrowDown')
            ) {
                return;
            }
            // Ensure that it is a number and stop the keypress
            if ((event.key < '0' || event.key > '9') && (event.key !== '.')) {
                event.preventDefault();
            }
        });

        numericInput.addEventListener('input', function () {
            // Remove 'e', '-', '+', '.', and other non-numeric characters
            this.value = this.value.replace(/[^0-9]/g, '');
        });

        numericInput.addEventListener('paste', function (event) {
            // Prevent pasting non-numeric values
            var pasteData = (event.clipboardData || window.clipboardData).getData('text');
            if (!/^\d+$/.test(pasteData)) {
                event.preventDefault();
            }
        });
    });
});

function ShowandCloseLoader() {
    ShowLoadingDialog();
  

    setTimeout(function () {


        CloseLoadingDialog();

    }, 1500);
}
function convertTo24HourFormat(time12h) {
    // Create a Date object using the time in 12-hour format
    const [time, modifier] = time12h.split(' '); // Split time and AM/PM

    let [hours, minutes] = time.split(':'); // Split hours and minutes

    // Convert hours to number
    if (hours === '12') {
        hours = '00'; // Handle 12 AM case
    }
    if (modifier === 'PM' && hours !== '12') {
        hours = parseInt(hours, 10) + 12; // Add 12 to PM hours (except for 12 PM)
    }

    // Format the time as HH:MM in 24-hour format
    return hours + ":" + minutes;
}