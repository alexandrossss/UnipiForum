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

        public static user User
        {
            get
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                    return null;

                var user = HttpContext.Current.Items[UserKey] as user;
                if (user == null)
                {
                    
                    using (var context = new unipiforumEntities4())
                    {
                        user = context.users.FirstOrDefault(u => u.username == HttpContext.Current.User.Identity.Name);

                        if (user == null)
                            return null;
                        HttpContext.Current.Items[UserKey] = user;
                    }
                    
                }

                return user;
            }
        }
    }
}