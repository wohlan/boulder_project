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
        toggleTimes($('#datepicker').data("datepicker").selectedDates[0].toString().split(" ")[0]);
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

function toggleTimes(day) {
    var className = "." + day;
    $("#timeSelect option").attr("disabled", "disabled");
    $(className).removeAttr("disabled");
    $("#timeSelect").val("");
}

//$("#kidsCheckBox").click(function () {
//    if ($(this).prop("checked")) {
//        $("#calendarGroup").collapse('show');
//        $("#calendarGroup").find("input").removeAttr("disabled");

//    } else {
//        $("#calendarGroup").collapse('hide');
//        $("#calendarGroup").find("input").attr("disabled", "disabled");
//    }
//});
