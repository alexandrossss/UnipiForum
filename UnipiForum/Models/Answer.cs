using System.Collections.Generic;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace UnipiForum.Models
{
    public class Answer
    {
        public virtual int Id { get; set; }
        public virtual string AnswerText { get; set; }
        //public virtual string Name { get; set; }
        public virtual bool IsCorrect{ get; set; }
        public virtual Question Question { get; set; }
    
        //public virtual IList<Question> Questions { get; set; }
        //public Answer()
        //{
        //    Questions = new List<Question>();
        //}

}
    public class AnswerMap : ClassMapping<Answer>
    {
        public AnswerMap()
        {
            Table("answers");

            Id(x => x.Id, x => x.Generator(Generators.Identity));

            Property(x => x.AnswerText, x => x.NotNullable(true));
            Property(x => x.IsCorrect, x => x.NotNullable(true));

            ManyToOne(x => x.Question, x =>
            {
                x.Column("question_id");
                x.NotNullable(true);
            });
        }
    }
}