/*
    $(document).ready(function() {
        $('.load').hide();
    });    
    */
/*
$(document).ready(function ($) {
    var Body = $('body');
    Body.addClass('preloader-site');   
});
$(window).load(function () {
    $('.preloader-wrapper').fadeOut();
    $('body').removeClass('preloader-site');   
});
*/

$(document).ready(function () {
    $(".navbar-toggle").click(function () {
        $(".menu").toggleClass("menu-open");
    })
});
