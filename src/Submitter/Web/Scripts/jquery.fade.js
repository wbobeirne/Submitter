//Provided by hv-designs.co.uk

$(function () {
    // OPACITY OF BUTTON SET TO 0%
    $(".footer a img").css("opacity", "0.0");

    // ON MOUSE OVER
    $(".footer a img").hover(function () {

        // SET OPACITY TO 100%
        $(this).stop().animate({
            opacity: 1.0
            //-ms-filter:"progid:DXImageTransform.Microsoft.Alpha(opacity=100)";
            //filter:alpha(opacity=100);
        }, "fast");
    },

    // ON MOUSE OUT
function () {

    // SET OPACITY BACK TO 0%
    $(this).stop().animate({
        opacity: 0.0
    }, "fast");
});
});