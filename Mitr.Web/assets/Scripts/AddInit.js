
$(document).ready(function () {
    ApplySelectAndDate();
});

function ApplySelectAndDate() {
    $('.has-feedback-left').daterangepicker({
        singleDatePicker: true,
        showDropdowns: true,
        calender_style: "picker_4",
        format: "DD/MM/YYYY"
    }, function (start, end, label) {
        console.log(start.toISOString(), end.toISOString(), label);
    });


    $('.has-feedback-datetime').daterangepicker({
        singleDatePicker: true,
        timePicker: true,
        timePicker24Hour: true,
        timePickerIncrement: 10,
        locale: {
            format: 'MM/DD/YYYY H:mm'
        },
        showDropdowns: true,
        calender_style: "picker_4",
        format: 'DD/MM/YYYY H:mm'
    }, function (start, end, label) {
           
        console.log(start.toISOString(), end.toISOString(), label);
        });


    $('.mydatetime').daterangepicker({
        singleDatePicker: true,
        timePicker: true,
        timePicker24Hour: true,
        timePickerIncrement: 10,
        locale: {
            format: 'DD/MM/YYYY H:mm'
        },
        showDropdowns: true,
        calender_style: "picker_4",
        format: 'MM/DD/YYYY H:mm'
    }, function (start, end, label) {

        console.log(start.toISOString(), end.toISOString(), label);
    });

    $(".applyselect").select2();
}