using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using NHibernate.Linq;
using UnipiForum.Models;
using UnipiForum.ViewModels;

namespace UnipiForum.Controllers
{
    public class UserTestsController : Controller
    {

        public ActionResult Index()
        {

            using (var context = new unipiforumSQLEntities1())
            {
                var test_id = 1;
                var _tests = context.tests.Find(test_id);



                //take questions list of the text that i am running
                var _questions = context.questions.ToList()?.Where(a=>a.test_id == _tests.test_id).ToList();


                var _questionsOfThisTest = _questions.Select(question => new Question
                {
                    Question_ID = question.question_id,
                    Test_ID =  question.test_id,
                    Question_Difficulty = question.question_difficulty,
                    Question_Text = question.question_text

                }).ToList();

                var _answers = context.answers.ToList().Select(answer => new AnswersRadioButton
                {
                    Answer_ID = answer.answer_id,
                    Is_Correct = answer.is_correct,
                    Answer_Text = answer.answer_text,
                    Question_ID = answer.question_id

                }).ToList();
                



                return View(new DoATest()
                {
                    Test_ID = _tests.test_id,
                    Test_Text = _tests.test_text,
                    Questions = _questionsOfThisTest,
                    Answers = _answers
                });








            }
        }
    }
}