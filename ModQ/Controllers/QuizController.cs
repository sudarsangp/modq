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
        private QuizDBContext db = new QuizDBContext();

        private QuizViewModel QuizModelToViewModel(int index)
        {
            QuizModel eachQuiz;
            var idList = new List<int>();
            var output = new QuizViewModel();
            if (index == 0)
            {
                eachQuiz = db.QuizModels.First();
                output.Faculty = eachQuiz.Faculty;
                output.Module = eachQuiz.Module;
                output.Question = eachQuiz.Question;
                output.FirstOption = eachQuiz.FirstOption;
                output.SecondOption = eachQuiz.SecondOption;
                output.ThirdOption = eachQuiz.ThirdOption;
                output.FourthOption = eachQuiz.FourthOption;
                output.QuestionId = eachQuiz.ID;
            }
            else
            {
                var queryResult = db.QuizModels.SqlQuery("Select * from QuizModels");
                foreach (var row in queryResult)
                {
                    idList.Add(row.ID);
                }
                if (index <= idList.Count - 1) { 
                    int requiredId = idList.ElementAt(index);
           
                    var requiredRow = db.QuizModels.SqlQuery("Select * from QuizModels Where ID = " + requiredId);
                    output.Faculty = requiredRow.ElementAt(0).Faculty;
                    output.Module = requiredRow.ElementAt(0).Module;
                    output.Question = requiredRow.ElementAt(0).Question;
                    output.FirstOption = requiredRow.ElementAt(0).FirstOption;
                    output.SecondOption = requiredRow.ElementAt(0).SecondOption;
                    output.ThirdOption = requiredRow.ElementAt(0).ThirdOption;
                    output.FourthOption = requiredRow.ElementAt(0).FourthOption;
                    output.QuestionId = requiredRow.ElementAt(0).ID;
                }
            }
          
            return output;
        }

        // GET: Quiz
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult LoginLanding()
        {
            const int startIndex = 0;
            var model = QuizModelToViewModel(startIndex);
            model.HiddenIndex = startIndex;
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult NextQuestion(QuizViewModel inputModel)
        {
            int currentIndex = inputModel.HiddenIndex;
            QuizViewModel model = null;
            if (currentIndex < db.QuizModels.Count() - 1)
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
            return View("LoginLanding", model);
        }

         public JsonResult AnswerCheck(string answerGroup, QuizViewModel inputModel)
        {
            var fromDb = db.QuizModels.SqlQuery("Select * from QuizModels Where ID = " + inputModel.QuestionId);
            var model = new AnswerResultViewModel();
             model.AnswerDescription = fromDb.ElementAt(0).AnswerDetails;
             if (answerGroup == inputModel.OptionOne.ToString())
             {
                 if (fromDb.ElementAt(0).Answer == fromDb.ElementAt(0).FirstOption)
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
                 if (fromDb.ElementAt(0).Answer == fromDb.ElementAt(0).SecondOption)
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
                 if (fromDb.ElementAt(0).Answer == fromDb.ElementAt(0).ThirdOption)
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
                 if (fromDb.ElementAt(0).Answer == fromDb.ElementAt(0).FourthOption)
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