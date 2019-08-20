var disabledDays = [1, 2, 3, 4];
var date = new Date();
date.setMinutes(0);
date.setHours(15);
$('#datepicker').datepicker({
    language: 'de',
    timepicker: true,
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
    $("#date-input").val(data);

});