
//using System;
//using System.Collections.Generic;
//using System.Xml.Schema;
//using NHibernate.Mapping;
//using NHibernate.Mapping.ByCode;
//using NHibernate.Mapping.ByCode.Conformist;
//using NHibernate.SqlCommand;

//namespace UnipiForum.Models
//{
//    public class Test
//    {
//        public virtual int Id { get; set; }
//        public virtual user User { get; set; }
//        public virtual string TestText { get; set; }
//        public virtual int TestType  { get; set; }
//        public virtual bool IsVisibleToOtherAdmins { get; set; }
//        //public virtual IList<Question> Questions { get; set; }

//        //public Test()
//        //{
//        //    Questions = new List<Question>();
//        //}

//    }

//    public class TestMap : ClassMapping<Test>
//    {
//        public TestMap()
//        {
//            Table("Tests");

//            Id(x => x.Id, x => x.Generator(Generators.Identity));

//            ManyToOne(x => x.User, x =>
//            {
//                x.Column("user_id");
//                x.NotNullable(true);
//            });

//            Property(x => x.TestText, x => x.NotNullable(true));
//            Property(x => x.TestType, x => x.NotNullable(true));
//            Property(x => x.IsVisibleToOtherAdmins, x => x.NotNullable(true));


//        }
//    }
//}