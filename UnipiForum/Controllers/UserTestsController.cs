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

            using (var context = new unipiforumSQLEntities2())
            {
                var test_id = 1;
                var _test = context.tests.Find(test_id);


                //take questions list of the text that i am running
                var _questions = context.questions.ToList()?.Where(a => a.test_id == test_id).ToList();

                var questions = context.questions.Include("answers").ToList().Where(s => s.test_id == test_id).ToList();


                var anssadasda = context.answers.Select(ans => new AnswersRadioButton
                {
                    Answer_ID = ans.answer_id,
                    Question_ID = ans.question_id,
                    User_Answer = false,
                    Is_Correct = ans.is_correct
                }).ToList();

                var _questionsOfThisTest = questions.Select(question => new NewQuestions
                {
                    Test_ID = question.test_id,
                    Question_ID = question.question_id,
                    //Answers_List = question.answers.Where(s=>s.question_id==question.question_id).ToList(),
                    Answers_List = context.answers.Select(ans => new AnswersRadioButton
                    {
                        Answer_ID = ans.answer_id,
                        Question_ID = ans.question_id,
                        Answer_Text = ans.answer_text,
                        User_Answer = false,
                        Is_Correct = ans.is_correct
                    }).Where(s => s.Question_ID == question.question_id).ToList(),

                    Question_Difficulty = question.question_difficulty,
                    Question_Text = question.question_text

                }).ToList();

                //var _answers = context.answers.ToList().Select(answer => new AnswersRadioButton
                //{
                //    Answer_ID = answer.answer_id,
                //    User_Answer = false,
                //    Answer_Text = answer.answer_text,
                //    Question_ID = answer.question_id

                //}).ToList();

                return View(new DoATest()
                {
                    Test_ID = _test.test_id,
                    Test_Text = _test.test_text,
                    Questions = _questionsOfThisTest,
                    //Answers = _answers
                });

            }
        }

        [HttpPost]
        public ActionResult Index(int test_id, DoATest form)
        {
            using (var context = new unipiforumSQLEntities2())
            {
                var test = context.tests.Find(test_id);
                var resust = new result();
                double testResults = 0.0;


                var resquest = form.Questions;
                if (resquest.Count != 0)
                {
                    foreach (var quest in resquest)
                    {
                        var trueAns = context.answers.Where(s => s.question_id == quest.Question_ID).ToList();
                        foreach (var ans in quest.Answers_List)
                        {
                            //here add the result to result table

                            var trAns = trueAns.FirstOrDefault(a => a.answer_id == ans.Answer_ID);
                            if (ans.User_Answer)
                            {
                                resust.question_id = quest.Question_ID;
                                resust.question_text = quest.Question_Text;
                                resust.answer_id = ans.Answer_ID;
                                resust.answer_text = ans.Answer_Text;
                                resust.is_the_correct_answer = trAns?.is_correct;
                                resust.user_answer = ans.User_Answer;
                                resust.test_id = test_id;
                                resust.test_text = test.test_text;
                                resust.user_id = context.users.FirstOrDefault(d => d.username == User.Identity.Name).user_id;
                                context.results.Add(resust);
                                context.SaveChanges();
                            }
                        }
                    }
                }

                if (test.test_type == 0)
                {
                    var questOf0Dif = form.Questions.Where(s => s.Question_Difficulty == 0).ToList();
                    if (questOf0Dif.Count != 0)
                    {
                        var numberquestOf0Dif = questOf0Dif.Count;
                        var passedQuest = 0;
                        foreach (var quest in questOf0Dif)
                        {
                            var trueAns = context.answers.Where(s => s.question_id == quest.Question_ID).ToList();
                            var countOfcorrectAns = 0;
                            foreach (var ans in quest.Answers_List)
                            {
                                var trAns = trueAns?.FirstOrDefault(a => a.answer_id == ans.Answer_ID);
                                if (ans.User_Answer == trAns?.is_correct)
                                {
                                    countOfcorrectAns++;
                                }
                            }

                            if (countOfcorrectAns == quest.Answers_List.Count)
                            {
                                //passed this quest
                                passedQuest++;
                            }

                        }

                        testResults = (double)passedQuest / numberquestOf0Dif;
                    }

                    if (testResults >= 0.45)//is passed the 0
                    {
                        testResults = 0.0;
                        var questOf1Dif = form.Questions.Where(s => s.Question_Difficulty == 1).ToList();
                        if (questOf1Dif.Count != 0)
                        {
                            var numberquestOf1Dif = questOf0Dif.Count;
                            var passedQuest = 0;
                            foreach (var quest in questOf1Dif)
                            {
                                var trueAns = context.answers.Where(s => s.question_id == quest.Question_ID).ToList();
                                var countOfcorrectAns = 0;
                                foreach (var ans in quest.Answers_List)
                                {
                                    var trAns = trueAns.FirstOrDefault(a => a.answer_id == ans.Answer_ID);
                                    if (ans.User_Answer == trAns?.is_correct)
                                    {
                                        countOfcorrectAns++;
                                    }
                                }

                                if (countOfcorrectAns == quest.Answers_List.Count)
                                {
                                    //passed this quest
                                    passedQuest++;
                                }
                            }

                            testResults = (double)passedQuest / numberquestOf1Dif;
                        }
                    }
                    else
                    {
                        return RedirectToAction("AssignToGroup", "Group", new{ diffId=0,userId= context.users.FirstOrDefault(d => d.username == User.Identity.Name).user_id});
                    }

                    if (testResults >= 0.45)//Is passed the 1
                    {
                        testResults = 0.0;
                        var questOf2Dif = form.Questions.Where(s => s.Question_Difficulty == 1).ToList();
                        if (questOf2Dif.Count != 0)
                        {
                            var numberquestOf2Dif = questOf0Dif.Count;
                            var passedQuest = 0;
                            foreach (var quest in questOf2Dif)
                            {
                                var trueAns = context.answers.Where(s => s.question_id == quest.Question_ID).ToList();
                                var countOfcorrectAns = 0;
                                foreach (var ans in quest.Answers_List)
                                {
                                    //here add the result to result table

                                    var trAns = trueAns.FirstOrDefault(a => a.answer_id == ans.Answer_ID);
                                    if (ans.User_Answer == trAns?.is_correct)
                                    {
                                        countOfcorrectAns++;
                                    }
                                }

                                if (countOfcorrectAns == quest.Answers_List.Count)
                                {
                                    //passed this quest
                                    passedQuest++;
                                }
                            }

                            testResults = (double)passedQuest / numberquestOf2Dif;
                        }
                    }
                    else
                    {
                        return RedirectToAction("AssignToGroup", "Group", new { diffId = 1, userId = context.users.FirstOrDefault(d => d.username == User.Identity.Name).user_id });
                    }

                    if (testResults >= 0.45)//is passed the 2
                    {
                        testResults = 0.0;
                        var questOf3Dif = form.Questions.Where(s => s.Question_Difficulty == 1).ToList();
                        if (questOf3Dif.Count != 0)
                        {
                            var numberquestOf3Dif = questOf0Dif.Count;
                            var passedQuest = 0;
                            foreach (var quest in questOf3Dif)
                            {
                                var trueAns = context.answers.Where(s => s.question_id == quest.Question_ID).ToList();
                                var countOfcorrectAns = 0;
                                foreach (var ans in quest.Answers_List)
                                {
                                    //here add the result to result table

                                    var trAns = trueAns.FirstOrDefault(a => a.answer_id == ans.Answer_ID);
                                    if (ans.User_Answer == trAns?.is_correct)
                                    {
                                        countOfcorrectAns++;
                                    }
                                }

                                if (countOfcorrectAns == quest.Answers_List.Count)
                                {
                                    //passed this quest
                                    passedQuest++;
                                }
                            }

                            testResults = (double)passedQuest / numberquestOf3Dif;
                        }

                        return RedirectToAction("AssignToGroup", "Group", new { diffId = 3, userId = context.users.FirstOrDefault(d => d.username == User.Identity.Name).user_id });
                    }

                    return RedirectToAction("AssignToGroup", "Group", new { diffId = 2, userId = context.users.FirstOrDefault(d => d.username == User.Identity.Name).user_id });
                }
                else
                {
                    var questions = form.Questions;
                    if (questions.Count != 0)
                    {
                        var numberquestOf0Dif = questions.Count;
                        var passedQuest = 0;
                        foreach (var quest in questions)
                        {
                            var trueAns = context.answers.Where(s => s.question_id == quest.Question_ID).ToList();
                            var countOfcorrectAns = 0;
                            foreach (var ans in quest.Answers_List)
                            {
                                //here add the result to result table

                                var trAns = trueAns.FirstOrDefault(a => a.answer_id == ans.Answer_ID);
                                if (ans.User_Answer == trAns?.is_correct)
                                {
                                    countOfcorrectAns++;
                                }
                            }

                            if (countOfcorrectAns == quest.Answers_List.Count)
                            {
                                //passed this quest
                                passedQuest++;
                            }

                        }

                        testResults = (double)passedQuest / numberquestOf0Dif;
                    }

                    var testdiff = test.test_type-1;
                    if (testResults >= 0.45 && testdiff==3)
                    {
                        // you won
                        
                    }
                    else if (testResults >= 0.45)
                    {
                        return RedirectToAction("AssignToGroup", "Group", new { diffId = testdiff+1, userId = context.users.FirstOrDefault(d => d.username == User.Identity.Name).user_id });
                    }
                    else
                    {
                        return RedirectToAction("AssignToGroup", "Group", new { diffId = testdiff, userId = context.users.FirstOrDefault(d => d.username == User.Identity.Name).user_id });
                    }
                    
                }

                





                return RedirectToAction("Results", "UserProfile");
            }
        }
    }
}