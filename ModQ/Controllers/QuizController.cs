using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModQ.Models;

namespace ModQ.Controllers
{
    [Authorize]
    public class QuizController : Controller
    {
        private readonly QuizDBContext _db = new QuizDBContext();

        private QuizViewModel QuizModelToViewModel(int index)
        {
            var idList = new List<int>();
            var output = new QuizViewModel();
            if (index == 0)
            {
                QuizModel eachQuiz = _db.QuizModels.First();
                output = MapQuizModelToQuizViewModel(eachQuiz);
            }
            else
            {
                var queryResult = _db.QuizModels.Select(data => data);
                idList.AddRange(queryResult.Select(row => row.ID));
                if (index <= idList.Count - 1) { 
                    int requiredId = idList.ElementAt(index);

                    var requiredRow = _db.QuizModels.First(quiz => quiz.ID == requiredId);

                    output = MapQuizModelToQuizViewModel(requiredRow);
                }
            }
          
            return output;
        }

        private QuizViewModel MapQuizModelToQuizViewModel(QuizModel input)
        {
            var output = new QuizViewModel
            {
                Faculty = input.Faculty,
                Module = input.Module,
                Question = input.Question,
                FirstOption = input.FirstOption,
                SecondOption = input.SecondOption,
                ThirdOption = input.ThirdOption,
                FourthOption = input.FourthOption,
                QuestionId = input.ID
            };
            return output;
        }

        private Dictionary<string, int> GetFacultyAndNumberOfQuestions()
        {
            var result = new Dictionary<string, int>();
            
            int eq = 0, socq = 0, bq = 0, sq = 0;
            eq = _db.QuizModels.Count(quiz => quiz.Faculty.Equals("Engineering"));
            socq = _db.QuizModels.Count(quiz => quiz.Faculty.Equals("Computing"));
            bq = _db.QuizModels.Count(quiz => quiz.Faculty.Equals("Business"));
            sq = _db.QuizModels.Count(quiz => quiz.Faculty.Equals("Science"));
           
            result.Add("Engineering", eq);
            result.Add("Computing", socq);
            result.Add("Business", bq);
            result.Add("Science", sq);
            return result;
        }

            // GET: Quiz
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult LoginLanding()
        {
            const int startIndex = 0;
            var model = QuizModelToViewModel(startIndex);
            model.HiddenIndex = startIndex;
            model.NumberOfQuestionsByFaculty = GetFacultyAndNumberOfQuestions();
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult NextQuestion(QuizViewModel inputModel)
        {
            int currentIndex = inputModel.HiddenIndex;
            QuizViewModel model = null;
            if (currentIndex < _db.QuizModels.Count() - 1)
            {
                currentIndex += 1;
                model = QuizModelToViewModel(currentIndex);
                model.HiddenIndex = currentIndex;
            }
            else
            {
                model = QuizModelToViewModel(currentIndex);
                model.HiddenIndex = currentIndex;
            }
            model.NumberOfQuestionsByFaculty = GetFacultyAndNumberOfQuestions();
            return View("LoginLanding", model);
        }

         [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PreviousQuestion(QuizViewModel inputModel)
        {
            int currentIndex = inputModel.HiddenIndex;
            QuizViewModel model = null;
            if (currentIndex > 0)
            {
                currentIndex -= 1;
                model = QuizModelToViewModel(currentIndex);
                model.HiddenIndex = currentIndex;
            }
            else
            {
                model = QuizModelToViewModel(currentIndex);
                model.HiddenIndex = currentIndex;
            }
            model.NumberOfQuestionsByFaculty = GetFacultyAndNumberOfQuestions();
            return View("LoginLanding", model);
        }

         public JsonResult AnswerCheck(string answerGroup, QuizViewModel inputModel)
        {
            var fromDb = _db.QuizModels.First(quiz => quiz.ID == inputModel.QuestionId);
            var model = new AnswerResultViewModel();
             model.AnswerDescription = fromDb.AnswerDetails;
             if (answerGroup == inputModel.OptionOne.ToString())
             {
                 if (fromDb.Answer == fromDb.FirstOption)
                 {
                     // correct
                     model.CorrectChoice = true;
                 }
                 else
                 {
                     //incorrect
                     model.CorrectChoice = false;
                 }
             }
             if (answerGroup == inputModel.OptionTwo.ToString())
             {
                 if (fromDb.Answer == fromDb.SecondOption)
                 {
                     // correct
                     model.CorrectChoice = true;
                 }
                 else
                 {
                     //incorrect
                     model.CorrectChoice = false;
                 }
             }
             if (answerGroup == inputModel.OptionThree.ToString())
             {
                 if (fromDb.Answer == fromDb.ThirdOption)
                 {
                     // correct
                     model.CorrectChoice = true;
                 }
                 else
                 {
                     //incorrect
                     model.CorrectChoice = false;
                 }
             }
             if (answerGroup == inputModel.OptionFour.ToString())
             {
                 if (fromDb.Answer == fromDb.FourthOption)
                 {
                     // correct
                    model.CorrectChoice = true;
                 }
                 else
                 {
                     //incorrect
                     model.CorrectChoice = false;
                 }
             }
             return Json(model);
        }
    }
}