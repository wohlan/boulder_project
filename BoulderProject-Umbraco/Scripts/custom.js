var disabledDays = [1, 2, 3, 4];
var date = new Date();
date.setMinutes(0);
date.setHours(15);
$('#datepicker').datepicker({
    language: 'de',
    timepicker: false,
    dateFormat:"dd.mm.yyyy",
    minHours: 15,
    maxHours: 20,
    startMinute: 0,
    minutesStep: 30,
    startDate: date,
    onRenderCell: function (date, cellType) {
        if (cellType == 'day') {
            var day = date.getDay(),
                isDisabled = disabledDays.indexOf(day) != -1;

            return {
                disabled: isDisabled
            }
        }
    },
    onSelect: function () {
        var data = $('#datepicker').data("datepicker")._prevOnSelectValue;
        $("#date-input").val(data);

    }
});

$(".datepicker--time-row input").change(function () {
    var data = $('#datepicker').data("datepicker")._prevOnSelectValue;
    var time = $("#timeSelect").val();
    data += " " + time;
    $("#date-input").val(data);

});

$("#timeSelect").change(function () {
    var data = $('#datepicker').data("datepicker")._prevOnSelectValue;
    var time = $("#timeSelect").val();
    data += " " + time;
    $("#date-input").val(data);

});
$("#kidsCheckBox").click(function () {
    if ($(this).prop("checked")) {
        $("#calendarGroup").collapse('show');
        $("#calendarGroup").find("input").removeAttr("disabled");

    } else {
        $("#calendarGroup").collapse('hide');
        $("#calendarGroup").find("input").attr("disabled", "disabled");
    }
});




//$("#mapImage").click(function () {
//    window.open("https://www.google.com/maps/place/Boulder-Point/@53.8281901,9.9797165,17z/data=!3m1!4b1!4m5!3m4!1s0x47b22e33490595bd:0xa9055b293eeb1364!8m2!3d53.828187!4d9.9819052");
//});
