$(function() {
    var quit = false;
    $('div.options').on('click', function() {
            if (quit) {
                return;
            }

            var thisObject = $(this);
        console.log(thisObject.children().children().children());
            thisObject.children().children().children().attr('checked', true);
            $.post("AnswerCheck", { question: $('#questionPanel').html(), optionValue: thisObject.find('span').html() }, function() {

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
    
    var quizByTypeText = "faculty", quizByTypeValue = "Engineering";
    $('.quizByFacultyLink').on('click', function () {
        quizByTypeText = "faculty";
        quizByTypeValue = $(this).html();

        $.post("AjaxNextQuestion", { question: $('#questionPanel').html(), quizByTypeText: quizByTypeText, quizByTypeValue: quizByTypeValue }, function () {
            $('div.options').children().children().children().attr('checked', false);
            $('div.options').removeClass('panel-success');
            $('div.options').removeClass('panel-danger');
            $('#answerDescription').html("");
            quit = false;
        })
            .success(function (data) {
                $('#facultyType').html(data.Faculty);
                $('#moduleCode').html(data.Module);
                $('#questionPanel').html(data.Question);
                $('#optionOnePanel').html(data.FirstOption);
                $('#optionTwoPanel').html(data.SecondOption);
                $('#optionThreePanel').html(data.ThirdOption);
                $('#optionFourPanel').html(data.FourthOption);
            })
            .fail(function () {
                alert('error');
            });

    });
    $('.quizByModuleLink').on('click', function() {
        quizByTypeText = "module";
        quizByTypeValue = $(this).html();

        $.post("AjaxPreviousQuestion", { question: $('#questionPanel').html(), quizByTypeText: quizByTypeText, quizByTypeValue: quizByTypeValue }, function () {
            $('div.options').children().children().children().attr('checked', false);
            $('div.options').removeClass('panel-success');
            $('div.options').removeClass('panel-danger');
            $('#answerDescription').html("");
            quit = false;
        })
          .success(function (data) {
              $('#facultyType').html(data.Faculty);
              $('#moduleCode').html(data.Module);
              $('#questionPanel').html(data.Question);
              $('#optionOnePanel').html(data.FirstOption);
              $('#optionTwoPanel').html(data.SecondOption);
              $('#optionThreePanel').html(data.ThirdOption);
              $('#optionFourPanel').html(data.FourthOption);
          })
          .fail(function () {
              alert('error');
          });

    });

    $('#ajaxNext').on('click', function () {
        
        $.post("AjaxNextQuestion", { question: $('#questionPanel').html(), quizByTypeText: quizByTypeText, quizByTypeValue: quizByTypeValue }, function () {
            $('div.options').children().children().children().attr('checked', false);
            $('div.options').removeClass('panel-success');
            $('div.options').removeClass('panel-danger');
            $('#answerDescription').html("");
            quit = false;
            })
            .success(function (data) {
                $('#facultyType').html(data.Faculty);
                $('#moduleCode').html(data.Module);
                $('#questionPanel').html(data.Question);
                $('#optionOnePanel').html(data.FirstOption);
                $('#optionTwoPanel').html(data.SecondOption);
                $('#optionThreePanel').html(data.ThirdOption);
                $('#optionFourPanel').html(data.FourthOption);
            })
            .fail(function() {
                alert('error');
            });


    });
    
    $('#ajaxPrev').on('click', function () {

        $.post("AjaxPreviousQuestion", { question: $('#questionPanel').html(), quizByTypeText: quizByTypeText, quizByTypeValue: quizByTypeValue }, function () {
            $('div.options').children().children().children().attr('checked', false);
            $('div.options').removeClass('panel-success');
            $('div.options').removeClass('panel-danger');
            $('#answerDescription').html("");
            quit = false;
        })
            .success(function (data) {
                $('#facultyType').html(data.Faculty);
                $('#moduleCode').html(data.Module);
                $('#questionPanel').html(data.Question);
                $('#optionOnePanel').html(data.FirstOption);
                $('#optionTwoPanel').html(data.SecondOption);
                $('#optionThreePanel').html(data.ThirdOption);
                $('#optionFourPanel').html(data.FourthOption);
            })
            .fail(function () {
                alert('error');
            });


    });

});