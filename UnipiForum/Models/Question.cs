using System.Collections.Generic;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace UnipiForum.Models
{
    public class Question
    {
        public virtual int Id { get; set; }
        public virtual string QuestionText { get; set; }
        public virtual Test Test{ get; set; }
        //public virtual IList<Answer> Answers { get; set; }

        //public Question()
        //{
        //    Answers = new List<Answer>();
        //}
    }

    public class QuestionMap : ClassMapping<Question>
    {
        public QuestionMap()
        {
            Table("questions");

            Id(x => x.Id, x => x.Generator(Generators.Identity));

            Property(x => x.QuestionText, x => x.NotNullable(true));

            ManyToOne(x => x.Test, x =>
            {
                x.Column("test_id");
                x.NotNullable(true);
            });
        }
    }
}