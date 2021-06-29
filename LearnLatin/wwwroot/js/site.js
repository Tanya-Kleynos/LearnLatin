$('.wordCard').click(function () {
    $(this).toggleClass('flipped');
});

$(document).ready(function () {
    var val = $("input[name*='progress']").val();
    $('#progressPerc').css("width", val + "%").attr("aria-valuenow", val)
});