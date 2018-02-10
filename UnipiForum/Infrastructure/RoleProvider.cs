using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UnipiForum.Models;

namespace UnipiForum.Infrastructure
{
    public class RoleProvider : System.Web.Security.RoleProvider
    {


        public override string[] GetRolesForUser(string username)
        {
            using (var context = new unipiforumSQLEntities2())
            {
                var user = context.users.FirstOrDefault(o=>o.username== username);
                var roles = user.role_users.Select(role_user => role_user.role.name).ToArray();
                return roles;
            }
        }


        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }
        
        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}