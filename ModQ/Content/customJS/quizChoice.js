﻿$(function() {
    var quit = false;
    $('div.options').on('click',function () {
        if (quit) {
            return;
        }
        var thisObject = $(this);
        thisObject.children().children().children().attr('checked', true);
        $.post("AnswerCheck", thisObject.closest("form").serialize(), function() {

            })
            .success(function(data) {

                if (data == "True") {
                    thisObject.addClass('panel-success');

                } else {
                    thisObject.addClass('panel-danger');

                }
            })
            .fail(function() {
                alert("error");
            })
            .always(function() {
            quit = true;
        });
    });

});