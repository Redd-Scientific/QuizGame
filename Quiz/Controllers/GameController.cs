using Quiz.Logging;
using Quiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Quiz.Controllers
{
    public class GameController : Controller
    {
        //
        // GET: /Game/

        QuizContext db = new QuizContext();
        int currentUser, currentQuestion;

        public ActionResult StartGame()
        {
            MembershipUser user = Membership.GetUser(User.Identity.Name);
            currentUser = (int)user.ProviderUserKey;
            Session["currentUser"] = currentUser;
            return View();
        }

        public ActionResult DisplayCategory()
        {
            if (Session["currentUser"] != null)
            {
                int uid = (int)Session["currentUser"];
                var get_all_questions_answered_query = from q in db.UserQuestions
                                                       where q.UserId == uid
                                                       select q;

                var get_next_question_not_answered = from q in db.Questions
                                                     where !(get_all_questions_answered_query).Any(quest => quest.QuestionId == q.QuestionId)
                                                     select new QuestionDTO()
                                                     {
                                                         QuestionId = q.QuestionId,
                                                         QuestionText = q.QuestionText,
                                                         AnswerA = q.AnswerA,
                                                         AnswerB = q.AnswerB,
                                                         AnswerC = q.AnswerC,
                                                         AnswerD = q.AnswerD,
                                                         CategoryId = q.Category.CategoryId,
                                                         Category = q.Category
                                                     };

                QuestionDTO question = get_next_question_not_answered.FirstOrDefault();

                if (question == null)
                {
                    var get_incorrectly_answered_questions = from q in get_all_questions_answered_query
                                                             where q.correct == false
                                                             select q;
                    question = (from q in db.Questions
                                where (get_incorrectly_answered_questions).Any(quest => quest.QuestionId == q.QuestionId)
                                select new QuestionDTO()
                                {
                                    QuestionId = q.QuestionId,
                                    QuestionText = q.QuestionText,
                                    AnswerA = q.AnswerA,
                                    AnswerB = q.AnswerB,
                                    AnswerC = q.AnswerC,
                                    AnswerD = q.AnswerD,
                                    CategoryId = q.Category.CategoryId,
                                    Category = q.Category
                                }).FirstOrDefault();

                    if (question == null)
                    {
                        return RedirectToAction("GameOver");
                    }
                }

                Session["currentQuestion"] = question.QuestionId;
                return View(question);
            }
            else
            {
                return RedirectToAction("StartGame");
            }
        }

        public ActionResult GameOver()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SubmitBet(int questionId, int betAmt)
        {
            HttpResponseMessage res;
            int uid = (int)Session["currentUser"];
            new LogEvent("questionId | SubmitBet " + questionId).Raise();
            new LogEvent("betAmt | SubmitBet " + betAmt).Raise();
            new LogEvent("uid | SubmitBet " + uid).Raise();
            if (questionId != null && betAmt != null && uid != null)
            {
                new LogEvent("In If | SubmitBet ").Raise();
                UserProfile up = (from u in db.UserProfiles
                                  where u.UserId == uid
                                  select u).FirstOrDefault();
                //UserProfile up = db.UserProfiles.Find(uid);
                new LogEvent("up | SubmitBet " + up.UserName).Raise();
                //Check if the question is already in UserQuestion table
                UserQuestions uq = (from u in db.UserQuestions
                                    where u.UserId == uid && u.QuestionId == questionId
                                    select u).FirstOrDefault();
                new LogEvent("uq | SubmitBet " + uq).Raise();
                //If not, create the question
                if (uq == null)
                {
                    new LogEvent("Creating uq | SubmitBet ").Raise();
                    uq = new UserQuestions { UserId = uid, QuestionId = questionId, betAmount = betAmt };
                    uq.answered = 0;
                    uq.correct = false;
                    uq.question = db.Questions.Find(uq.QuestionId);
                    uq.question.CategoryId = uq.question.Category.CategoryId;
                    uq.user = up;
                    db.UserQuestions.Add(uq);
                    if (uq.question == null)
                    {
                        uq.question = db.Questions.Find(uq.QuestionId);
                    }
                    new LogEvent("Created uq | SubmitBet " + uq.QuestionId).Raise();
                }
                if (uq.question == null)
                {
                    uq.question = db.Questions.Find(uq.QuestionId);
                }
                
                //check if the User and Category combo is already in the table
                 var query = from u in db.UserCategories
                          where u.UserId == uid && u.CategoryId == uq.question.CategoryId
                          select u;
                 UserCategories uc = query.FirstOrDefault();
                 //if not, add the user and category combo into the table
                 if (uc == null)
                 {
                     uc = new UserCategories { UserId = uid, totalQuestionsAnswered = 0 };
                     uc.category = uq.question.Category;
                     uc.CategoryId = uq.question.CategoryId;
                     uc.user = up;
                     uc.totalQuestionsAnswered = 0;
                     db.UserCategories.Add(uc);
                    // db.SaveChanges();
                 }
                
                 //increment the answered questions in the in the user category combo
                 uc.totalQuestionsAnswered += 1;
                db.SaveChanges();
                res = new HttpResponseMessage();
                res.StatusCode = HttpStatusCode.Accepted;
                new LogEvent("Working | SubmitBet").Raise();
            }
            else
            {
                res = new HttpResponseMessage();
                res.StatusCode = HttpStatusCode.ExpectationFailed;
                new LogEvent("Failure | SubmitBet").Raise();
            }
            //return RedirectToAction("ShowQuestion", "Game");
            new LogEvent("coming to an end | SubmitBet").Raise();
            return Json(new { redirectToUrl = Url.Action("ShowQuestion", "Game") });
            //return Json(new { redirectToUrl = "http://quiz-6.apphb.com/Game/ShowQuestion" });
        }

        public ActionResult ShowQuestion()
        {
            int qid = (int)Session["currentQuestion"];
            QuestionDTO question = (from q in db.Questions
                                    where q.QuestionId == qid
                                    select new QuestionDTO()
                                    {
                                        QuestionId = q.QuestionId,
                                        QuestionText = q.QuestionText,
                                        AnswerA = q.AnswerA,
                                        AnswerB = q.AnswerB,
                                        AnswerC = q.AnswerC,
                                        AnswerD = q.AnswerD,
                                        CategoryId = q.Category.CategoryId,
                                        Category = q.Category
                                    }).FirstOrDefault();
            return View(question);
        }

        [HttpPost]
        public JsonResult SubmitAnswer(int answer)
        {
            int qid = (int)Session["currentQuestion"];
            int correctAnswer = (from q in db.Questions
                                 where q.QuestionId == qid
                                 select q.Correct).FirstOrDefault();

            int uid = (int)Session["currentUser"];

            UserQuestions uq = (from u in db.UserQuestions
                                where u.UserId == uid && u.QuestionId == qid
                                select u).FirstOrDefault();

            uq.answered += 1;

            if (correctAnswer == answer)
            {
                uq.correct = true;
                db.SaveChanges();
                return Json(new { redirectToUrl = Url.Action("AnswerCorrect", "Game") });
                //return Json(new { redirectToUrl = "http://quiz-6.apphb.com/Game/AnswerCorrect" });
            }
            else
            {
                uq.correct = false;
                db.SaveChanges();
                return Json(new { redirectToUrl = Url.Action("AnswerWrong", "Game") });
                //return Json(new { redirectToUrl = "http://quiz-6.apphb.com/Game/AnswerWrong" });
            }
        }

        public ActionResult AnswerCorrect()
        {
            return View();
        }

        public ActionResult AnswerWrong()
        {
            return View();
        }

        public ActionResult RestartGame()
        {
            List<UserQuestions> uq = db.UserQuestions.ToList();
            foreach (UserQuestions u in uq)
            {
                db.UserQuestions.Remove(u);
            }
            List<UserCategories> uc = db.UserCategories.ToList();
            foreach (UserCategories u in uc)
            {
                db.UserCategories.Remove(u);
            }
            db.SaveChanges();
            return RedirectToAction("DisplayCategory"); 
        }

    }
}
