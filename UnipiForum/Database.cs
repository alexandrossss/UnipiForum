using System.Security.Cryptography.X509Certificates;
using System.Web;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using UnipiForum.Models;

namespace UnipiForum
{
    public static class Database
    {
        //private const string Sessionkey = "UnipiForum.Database.SessionKey";
        //private static ISessionFactory _sessionFactory;
        //public static ISession Session
        //{
        //    get { return (ISession)HttpContext.Current.Items[Sessionkey]; }
        //}
        //public static void Configure()
        //{
        //    var config = new Configuration();
        //    //configure the connection string

        //    config.Configure();

        //    //add our mappings

        //    //var mapper = new ModelMapper();
        //    //mapper.AddMapping<UserMap>();
        //    //mapper.AddMapping<RoleMap>();
        //    //mapper.AddMapping<TestMap>();
        //    //mapper.AddMapping<QuestionMap>();
        //    //mapper.AddMapping<AnswerMap>();

        //    //config.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());
            
        //    //create session factory
        //    _sessionFactory = config.BuildSessionFactory();

        //}

        //public static void OpenSession()
        //{
        //    HttpContext.Current.Items[Sessionkey] = _sessionFactory.OpenSession();
        //}

        //public static void CloseSession()
        //{
        //    var session = HttpContext.Current.Items[Sessionkey] as ISession;
        //    if (session != null)
        //        session.Close();
        //    HttpContext.Current.Items.Remove(Sessionkey);
        //}


    }

}