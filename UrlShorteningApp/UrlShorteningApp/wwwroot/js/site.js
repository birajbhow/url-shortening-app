// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {

    $('button').click(() => {
        //alert($('#txtUrl').val());
        var requestData = $('#txtUrl').val();
        $.post('https://localhost:44331/', requestData, () => { })
    })
});