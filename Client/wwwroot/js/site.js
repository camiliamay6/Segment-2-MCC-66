// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.getElementById("click-button").addEventListener("click", function () {
    var element1 = document.getElementById("column-1");
    var element2 = document.getElementById("column-2-row-1");
    var element3 = document.getElementById("column-2-row-2");

    var style1 = getComputedStyle(element1);
    var style2 = getComputedStyle(element2);
    var style3 = getComputedStyle(element3);

    var bg_color_1 = style1.backgroundColor;
    var txt_color_1 = style1.color;
    var bg_color_2 = style2.backgroundColor;
    var txt_color_2 = style2.color;
    var bg_color_3 = style3.backgroundColor;
    var txt_color_3 = style3.color;

    element1.style.transition = "200ms linear";
    element1.style.color = bg_color_1;
    element1.style.backgroundColor = txt_color_1;

    element2.style.transition = "200ms linear";
    element2.style.transitionDelay = "100ms";
    element2.style.backgroundColor = txt_color_2;
    element2.style.color = bg_color_2;

    element3.style.transition = "200ms linear";
    element3.style.transitionDelay = "200ms";
    element3.style.backgroundColor = txt_color_3;
    element3.style.color = bg_color_3;
    
})

function ChangeColumn1() {
    var element = document.getElementById("column-1");
    var style = getComputedStyle(element);
    var bg_color = style.backgroundColor;
    var txt_color = style.color;
    element.style.transition = "200ms linear";
    element.style.color = bg_color;
    element.style.backgroundColor = txt_color;
}