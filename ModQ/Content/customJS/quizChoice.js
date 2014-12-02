$(function() {
    var quit = false;
    var quizByTypeText = 'faculty';
    var quizByTypeValue = 'Computing';
    $('div.options').on('click',function () {
        if (quit) {
            return;
        }
        var thisObject = $(this);
        thisObject.children().children().children().attr('checked', true);
        var panelDetails = thisObject.children().children();
    //    console.log($(panelDetails).find('span'));
        $.post("AnswerCheck", {question: $("#questionPanel").html(), optionValue: $(panelDetails).find('span').html()}, function() {
                
            })
            .success(function(data) {
                
                $('#answerDescription').html(data.AnswerDescription);
                if (data.CorrectChoice) {
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

    $('#ajaxPrev').on('click', function() {
        $.post("AjaxPreviousQuestion", { question: $("#questionPanel").html(), quizByTypeText: quizByTypeText, quizByTypeValue: quizByTypeValue }, function () {

        })
           .success(function (data) {
               $("#facultyType").html(data.Faculty);
               $("#moduleCode").html(data.Module);
               $("#questionPanel").html(data.Question);
               $("#optionOnePanel").html(data.FirstOption);
               $("#optionTwoPanel").html(data.SecondOption);
               $("#optionThreePanel").html(data.ThirdOption);
               $("#optionFourPanel").html(data.FourthOption);
               $("div.options").removeClass('panel-success');
               $("div.options").removeClass('panel-danger');
               $("div.options").children().children().children().removeAttr('checked');
               $("#answerDescription").html("");
               quit = false;

           });
    });

    $('#ajaxNext').on('click', function() {
        $.post("AjaxNextQuestion", { question: $("#questionPanel").html(), quizByTypeText: quizByTypeText, quizByTypeValue: quizByTypeValue }, function() {

            })
            .success(function (data) {
                $("#facultyType").html(data.Faculty);
                $("#moduleCode").html(data.Module);
            $("#questionPanel").html(data.Question);
            $("#optionOnePanel").html(data.FirstOption);
            $("#optionTwoPanel").html(data.SecondOption);
            $("#optionThreePanel").html(data.ThirdOption);
            $("#optionFourPanel").html(data.FourthOption);
            $("div.options").removeClass('panel-success');
            $("div.options").removeClass('panel-danger');
            $("div.options").children().children().children().removeAttr('checked');
            $("#answerDescription").html("");
            quit = false;
           
        });
    });

    $("a.quizByFacultyLink").on('click', function () {
        quizByTypeText = 'faculty';
        quizByTypeValue = $(this).html();
    });

    $("a.quizByModuleLink").on('click', function () {
        quizByTypeText = 'module';
        quizByTypeValue = $(this).html();
    });

    //$('a.quizByFaculty').on('click', function() {
    //    console.log($(this).html());
    //    $.post("QuizByFaculty", function() {

    //        })
    //        .success(function() {
    //            alert("success");
    //        })
    //        .fail(function() {
    //        alert("quizbyfaculty error");
    //    });
    //});

});