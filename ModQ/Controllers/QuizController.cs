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
        private const String ENGINEERING_TEXT = "Engineering";
        private const String COMPUTING_TEXT = "Computing";
        private const String BUSINESS_TEXT = "Business";
        private const String SCIENCE_TEXT = "Science";

        /// <summary>
        /// To iterate throught the entire database of questions.
        /// Used on startup page and displays the first question in database.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the set of questions from database by faculty.
        /// </summary>
        /// <param name="forward"></param>
        /// <param name="faculty"></param>
        /// <param name="question"></param>
        /// <returns></returns>
        private QuizViewModel GetRequiredQuestionByFaculty(bool forward, string faculty, string question)
        {
         
            var output = new QuizViewModel();
            if (faculty == null)
            {
                return null;
            }
            var queryResult = _db.QuizModels.Select(data => data).Where(row => row.Faculty == faculty);
            var listQueryResult = queryResult.ToList();
            for (int i = 0; i < listQueryResult.Count; i++)
            {
                if (question == null)
                {
                    output = MapQuizModelToQuizViewModel(listQueryResult.ElementAt(i));
                    break;
                }

                if (listQueryResult.ElementAt(i).Question.Equals(question))
                {
                    if (forward && i != listQueryResult.Count - 1)
                    {
                        output = MapQuizModelToQuizViewModel(listQueryResult.ElementAt(i + 1));
                        break;
                    }
                    else if (forward == false && i != 0)
                    {
                        output = MapQuizModelToQuizViewModel(listQueryResult.ElementAt(i - 1));
                        break;
                    }
                    else
                    {
                        output = MapQuizModelToQuizViewModel(listQueryResult.ElementAt(i));
                        break;
                    }
                }
               
            }
           
            return output;
        }

        /// <summary>
        /// Convert to QuizViewModel for display
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Displays the total number of questions under the given section
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, int> GetFacultyAndNumberOfQuestions()
        {
            var result = new Dictionary<string, int>();
            
            int eq = 0, socq = 0, bq = 0, sq = 0;
            eq = _db.QuizModels.Count(quiz => quiz.Faculty.Equals(ENGINEERING_TEXT));
            socq = _db.QuizModels.Count(quiz => quiz.Faculty.Equals(COMPUTING_TEXT));
            bq = _db.QuizModels.Count(quiz => quiz.Faculty.Equals(BUSINESS_TEXT));
            sq = _db.QuizModels.Count(quiz => quiz.Faculty.Equals(SCIENCE_TEXT));

            result.Add(ENGINEERING_TEXT, eq);
            result.Add(COMPUTING_TEXT, socq);
            result.Add(BUSINESS_TEXT, bq);
            result.Add(SCIENCE_TEXT, sq);
            return result;
        }

         /// <summary>
         /// Landing page for the Quiz application.
         /// </summary>
         /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult LoginLanding()
        {
            const int startIndex = 0;
            var model = QuizModelToViewModel(startIndex);
            if (model == null)
            {
                return View("ErrorPage");
            }
            model.HiddenIndex = startIndex;
            model.NumberOfQuestionsByFaculty = GetFacultyAndNumberOfQuestions();
            return View(model);
        }

        /// <summary>
        /// Gets the required quizByFaculty option from database.
        /// This implementation does not use the hiddenIndex attribute.
        /// The hiddenIndex attribute is used to iterate through entire list of questions in the database.
        /// </summary>
        /// <param name="quizByFaculty"></param>
        /// <param name="hiddenIndex"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult QuizByFaculty(String quizByFaculty, int hiddenIndex)
        {
            QuizViewModel model = null;

            model = GetRequiredQuestionByFaculty(false, quizByFaculty, null);
            if (model == null)
            {
                return View("ErrorPage");
            }
            else
            {
                model.NumberOfQuestionsByFaculty = GetFacultyAndNumberOfQuestions();
                return View("LoginLanding", model);
            }
        }

        /// <summary>
        /// Gets the input model details to get the next question
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult NextQuestion(QuizViewModel inputModel)
        {
            int currentIndex = inputModel.HiddenIndex;
            QuizViewModel model = null;
            if (currentIndex < _db.QuizModels.Count() - 1)
            {
                currentIndex += 1;
                model = GetRequiredQuestionByFaculty(true, inputModel.Faculty, inputModel.Question);
                if (model == null)
                {
                    return View("ErrorPage");
                }
                model.HiddenIndex = currentIndex;
            }
            else
            {
                model = GetRequiredQuestionByFaculty(true, inputModel.Faculty, inputModel.Question);
                if (model == null)
                {
                    return View("ErrorPage");
                }
                model.HiddenIndex = currentIndex;
            }
            model.NumberOfQuestionsByFaculty = GetFacultyAndNumberOfQuestions();
            return View("LoginLanding", model);
        }

        /// <summary>
        /// Gets the input model details for the previous question
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
         [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PreviousQuestion(QuizViewModel inputModel)
        {
            int currentIndex = inputModel.HiddenIndex;
            QuizViewModel model = null;
            if (currentIndex > 0)
            {
                currentIndex -= 1;
                model = GetRequiredQuestionByFaculty(false, inputModel.Faculty, inputModel.Question);
                if (model == null)
                {
                    return View("ErrorPage");
                }
                model.HiddenIndex = currentIndex;
            }
            else
            {
                model = GetRequiredQuestionByFaculty(false, inputModel.Faculty,  inputModel.Question);
                if (model == null)
                {
                    return View("ErrorPage");
                }
                model.HiddenIndex = currentIndex;
            }
            model.NumberOfQuestionsByFaculty = GetFacultyAndNumberOfQuestions();
            return View("LoginLanding", model);
        }

        /// <summary>
        /// Ensures the option selected by user matches the correct answer in database
        /// </summary>
        /// <param name="answerGroup"></param>
        /// <param name="inputModel"></param>
        /// <returns></returns>
         public JsonResult AnswerCheck(string answerGroup, QuizViewModel inputModel)
        {
            var model = new AnswerResultViewModel();
            if (inputModel.QuestionId == 0)
            {
                model.CorrectChoice = false;
                return Json(model);
            }
            var fromDb = _db.QuizModels.First(quiz => quiz.ID == inputModel.QuestionId);
            
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