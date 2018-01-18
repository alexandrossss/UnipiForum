using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using FluentMigrator;
using FluentMigrator.Builders;

namespace UnipiForum.Migrations
{
    [Migration(2)]
    public class _002_TestsAndQuestinosAndAnswers : Migration
    {
        public override void Up()
        {
            Create.Table("Tests")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("user_id").AsInt32().ForeignKey("users", "id")
                .WithColumn("testtext").AsCustom("TEXT")
                .WithColumn("testtype").AsInt32()
                .WithColumn("isvisibletootheradmins").AsBoolean();

            Create.Table("Questions")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("questiontext").AsCustom("TEXT")
                .WithColumn("Test_id").AsInt32().ForeignKey("Tests", "id");

            Create.Table("Answers")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("answertext").AsCustom("TEXT")
                .WithColumn("iscorrect").AsBoolean()
                .WithColumn("Question_id").AsInt32().ForeignKey("Questions", "id");


            //Create.Table("test_questions")
            //    .WithColumn("test_id").AsInt32().ForeignKey("tests", "id").OnDelete(Rule.Cascade)
            //    .WithColumn("post_id").AsInt32().ForeignKey("questions", "id").OnDelete(Rule.Cascade);

            //Create.Table("question_answers")
            //    .WithColumn("questions_id").AsInt32().ForeignKey("questions", "id").OnDelete(Rule.Cascade)
            //    .WithColumn("answers_id").AsInt32().ForeignKey("answers", "id").OnDelete(Rule.Cascade);

        }

        public override void Down()
        {
            //Delete.Table("question_answers");
            //Delete.Table("test_questions");
            Delete.Table("Questions");
            Delete.Table("Tests");
            Delete.Table("Answers");
        }
    }
}