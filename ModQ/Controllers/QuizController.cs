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
         
            QuizViewModel output = null;
            if (faculty == null)
            {
                return null;
            }
            var queryResult = _db.QuizModels.Select(data => data).Where(row => row.Faculty == faculty);
            var listQueryResult = queryResult.ToList();
            int position = 0;
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
            if (output == null && listQueryResult.Count > 0)
            {
                output = MapQuizModelToQuizViewModel(listQueryResult.ElementAt(position));
            }
            return output;
        }

        private QuizViewModel GetRequiredQuestionByModule(bool forward, string module, string question)
        {

            QuizViewModel output = null;
            if (module == null)
            {
                return null;
            }
            var queryResult = _db.QuizModels.Select(data => data).Where(row => row.Module == module);
            var listQueryResult = queryResult.ToList();
            int position = 0;
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
            if (output == null && listQueryResult.Count > 0)
            {
                output = MapQuizModelToQuizViewModel(listQueryResult.ElementAt(position));
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
            model.NumberOfQuestionsByFaculty = GetFacultyAndNumberOfQuestions();
            return View(model);
        }

        /// <summary>
        /// Ensures the option selected by user matches the correct answer in database
        /// </summary>
        /// <param name="question"></param>
        /// <param name="optionValue"></param>
        /// <returns></returns>
         public JsonResult AnswerCheck(string question, string optionValue)
        {
            var model = new AnswerResultViewModel();
           
            var fromDb = _db.QuizModels.First(quiz => quiz.Question == question);
            model.AnswerDescription = fromDb.AnswerDetails;

            model.CorrectChoice = fromDb.Answer.Equals(optionValue);
            return Json(model);
        }

        /// <summary>
        /// Gets the next question for display
        /// </summary>
        /// <param name="question"></param>
        /// <param name="quizByTypeText"></param>
        /// <param name="quizByTypeValue"></param>
        /// <returns></returns>
         public JsonResult AjaxNextQuestion(string question, string quizByTypeText, string quizByTypeValue)
         {
             QuizViewModel output = null;
             if (quizByTypeText.Equals("faculty"))
             {
                 output = GetRequiredQuestionByFaculty(true, quizByTypeValue, question);
             }
             if (quizByTypeText.Equals("module"))
             {
                 output = GetRequiredQuestionByModule(true, quizByTypeValue, question);
             }
            return Json(output);
        }

        /// <summary>
        /// gets the previous question for display
        /// </summary>
        /// <param name="question"></param>
        /// <param name="quizByTypeText"></param>
        /// <param name="quizByTypeValue"></param>
        /// <returns></returns>
         public JsonResult AjaxPreviousQuestion(string question, string quizByTypeText, string quizByTypeValue)
        {
            QuizViewModel output = null;
            if (quizByTypeText.Equals("faculty"))
            {
                output = GetRequiredQuestionByFaculty(false, quizByTypeValue, question);
            }
            if (quizByTypeText.Equals("module"))
            {
                output = GetRequiredQuestionByModule(false, quizByTypeValue, question);
            }
            return Json(output);
        }
    }
}