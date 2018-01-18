using System.Data;
using FluentMigrator;

namespace UnipiForum.Migrations
{
    [Migration(1)]
    public class _001_UsersAndRolesAndTestsAndQuestinosAndAnswers : Migration
    {
        public override void Up()
        {
            Create.Table("users")
                .WithColumn("id").AsInt32().Identity().PrimaryKey()
                .WithColumn("userId").AsString(128)
                .WithColumn("username").AsString(128)
                .WithColumn("email").AsCustom("VARCHAR(256)")
                .WithColumn("password_hash").AsString(128);

            Create.Table("roles")
                .WithColumn("id").AsInt32().Identity().PrimaryKey()
                .WithColumn("name").AsString(128);

            Create.Table("tests")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("user_id").AsInt32().ForeignKey("users", "id")
                .WithColumn("testtext").AsCustom("VARCHAR(256)")
                .WithColumn("testtype").AsInt32()
                .WithColumn("isvisibletootheradmins").AsBoolean();

            Create.Table("questions")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("questiontext").AsCustom("VARCHAR(256)")
                .WithColumn("Test_id").AsInt32().ForeignKey("Tests", "id");

            Create.Table("answers")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("answertext").AsCustom("VARCHAR(256)")
                .WithColumn("iscorrect").AsBoolean()
                .WithColumn("Question_id").AsInt32().ForeignKey("Questions", "id");

            Create.Table("role_users")
                .WithColumn("user_id").AsInt32().ForeignKey("users", "id").OnDelete(Rule.Cascade)
                .WithColumn("role_id").AsInt32().ForeignKey("roles", "id").OnDelete(Rule.Cascade);

        }

        public override void Down()
        {
            Delete.Table("role_users");
            Delete.Table("roles");
            Delete.Table("users");
            Delete.Table("Questions");
            Delete.Table("Tests");
            Delete.Table("Answers");
        }
    }
}