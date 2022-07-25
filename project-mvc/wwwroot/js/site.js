// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var wideScreen = 320; // for example beyond 640 is considered wide

var toggleBtnGroup = function () {
    var windowWidth = $(window).width();

    if (windowWidth < wideScreen) {
        $('#btn-group-toggle').addClass('btn-group-vertical').removeClass('btn-group');

    } else {
        $('#btn-group-toggle').addClass('btn-group').removeClass('btn-group-vertical');
    }
}

toggleBtnGroup(); // trigger on load

window.addEventListener('resize', toggleBtnGroup); // change on resize
