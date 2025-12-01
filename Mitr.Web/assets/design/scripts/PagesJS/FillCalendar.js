function GetCalenedarDataJson() {
  
  
    var data = $.parseJSON($.ajax({
        url: '/CommonAjax/GetCalenedarDataJson',
        dataType: "json",
        async: false
    }).responseText);

    return data;

}
document.addEventListener('DOMContentLoaded', function () {
 
    var calendarEl = document.getElementById('calendar');
    var calendar = new FullCalendar.Calendar(calendarEl, {
        headerToolbar: {
            left: 'title',
            center: false,
            right: 'prev,next today',
        },
        firstDay: 1,
        editable: true,
        expandRows: true,
        dayMaxEvents: true,
        events: GetCalenedarDataJson(),
        //eventClassNames: function (arg) {
        //    return arg.event.extendedProps.classname
        //},
        eventDidMount: function (info) {
            $(info.el).tooltip({
                title: info.event.extendedProps.description,
                placement: "top",
                trigger: "hover",
                container: "body"
            });

        },
    });
 
    calendar.render();

    if ($("#DivLeaveBalance").html() != undefined) {
      
        var date = calendar.getDate();
        FillLeaveBalance(date.toISOString());

        $('.fc-button').on('click', function (e) {
            e.preventDefault();
            e.stopPropagation();
            var date = calendar.getDate();
            FillLeaveBalance(date.toISOString())

        });
    }
});

function FillLeaveBalance(StartDate) {
    var src = EncryptQueryStringJSON(MenuID + "*" + "/Leave/_LeaveBalance" + "*" + StartDate);

    ShowLoadingDialog();
    $.ajax({
        url: "/Leave/_LeaveBalance",
        type: "Get",
        data: { src: src },
        success: function (data) {
            $("#DivLeaveBalance").empty();
            $("#DivLeaveBalance").html(data);
            CloseLoadingDialog();
        },
        error: function (er) {
            alert(er);
        }
    });
    CloseLoadingDialog();
}