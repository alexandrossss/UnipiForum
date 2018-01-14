using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using NHibernate.Linq;
using UnipiForum;
using UnipiForum.Models;


namespace UnipiForum
{
    public static class Auth
    {
        private const string UserKey = "UnipiForum.Auth.UserKey";

        public static User User
        {
            get
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                    return null;

                var user = HttpContext.Current.Items[UserKey] as User;
                if (user == null)
                {
                    user = Database.Session.Query<User>().FirstOrDefault(u => u.Username == HttpContext.Current.User.Identity.Name);

                    if (user == null)
                        return null;
                    HttpContext.Current.Items[UserKey] = user;
                }
                return user;
            }
        }
    }
}