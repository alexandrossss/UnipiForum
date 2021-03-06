﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NHibernate.Linq;
using UnipiForum.Areas.Admin.ViewModels;
using UnipiForum.Infrastructure;
using UnipiForum.Models;

namespace UnipiForum.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [SelectedTab("users")]
    public class UsersController : Controller
    {
        // GET: Admin/Users
        public ActionResult Index()
        {
            using (var context = new unipiforumSQLEntities2())
            {
                var _users = context.users.Include(t => t.role_users.Select(ru=>ru.role)).ToList();


                return View(new UsersIndex
                {
                    Users = _users.ToList()
                    //Database.Session.Query<User>().ToList()

                });

            }
        }

        public ActionResult New()
        {
            using (var context = new unipiforumSQLEntities2())
            {
                var _roles = context.roles;
                return View(new UsersNew
                {
                    Roles = _roles.Select(role => new RoleCheckbox
                    {
                        Id = role.role_id,
                        IsChecked = false,
                        Name = role.name
                    }).ToList()

                });
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult New(UsersNew form)
        {
            var user = new user();


            using (var context = new unipiforumSQLEntities2())
            {
                var _users = context.users.ToList();

                if (_users.Any(u => u.user_university_id == form.University_ID))

                    ModelState.AddModelError("University_ID", "University ID must be unique");

                if (_users.Any(u => u.username == form.Username))

                    ModelState.AddModelError("Username", "Username alredy exists");

                if (!ModelState.IsValid)
                    return View(form);

                user.user_university_id = form.University_ID;
                user.email = form.Email;
                user.username = form.Username;
                user.password_hash = form.Password;

                var userDatabaseRoles = context.role_users.ToList();
                var rolesToBeAdded = new List<int>();
                foreach (var role in form.Roles)
                {
                    //If the role was not selected by the user
                    if (role.IsChecked)
                    {
                        
                        
                            rolesToBeAdded.Add(role.Id);
                        
                    }
                }

                foreach (var roleToAdd in rolesToBeAdded)
                {
                    context.role_users.Add(new role_users()
                    {
                        role_id = roleToAdd,
                        user_id = user.user_id
                    });
                }

                //user.SetPassword(form.Password);
                context.users.Add(user);
                context.SaveChanges();
                return RedirectToAction("index");
            }
        }

        public ActionResult Edit(int user_id)
        {
            using (var context = new unipiforumSQLEntities2())
            {
                var user = context.users.Find(user_id);
                //var user = Database.Session.Load<User>(id);

                if (user == null)
                    return HttpNotFound();



                var _roles = context.roles.ToList().Select(role => new RoleCheckbox
                {
                    Id = role.role_id,
                    IsChecked = user.role_users.Select(s => s.role_id).Contains(role.role_id),
                    Name = role.name
                }).ToList();

                return View(new UsersEdit()
                {
                    University_ID = user.user_university_id,
                    Username = user.username,
                    Email = user.email,
                    GroupID = user.group_id,
                    Passed_Text = user.passed_text,
                    Roles = _roles
                });
            }
        }

        

        [HttpPost]
        public ActionResult Edit(int user_id, UsersEdit form)
        {
            using (var context = new unipiforumSQLEntities2())
            {
                //var user = context.users.Find(user_id);
                var user = context.users.Include(t => t.role_users).SingleOrDefault(u => u.user_id == user_id);
                if (user == null)
                    return HttpNotFound();

                if (context.users.ToList().Any(u => u.username == form.Username && u.user_id != user_id))
                    ModelState.AddModelError("Username", "Username must be unique");

                if (!ModelState.IsValid)
                    return View(form);

                var userDatabaseRoles = context.role_users.ToList()?.Where(s => s.user_id == user_id).ToList();
                var rolesToBeDeleted = new List<int>();
                var rolesToBeAdded = new List<int>();
                foreach (var role in form.Roles)
                {
                    //If the role was not selected by the user
                    if (!role.IsChecked)
                    {
                        //but it exists in the database, we must delete it
                        if (userDatabaseRoles.FirstOrDefault(s => s.role_id == role.Id) != null)
                        {
                            rolesToBeDeleted.Add(role.Id);
                        }
                        //if it is not present in the database, we do nothing
                    }
                    else //if the role was selected by the user
                    {
                        //if the role does not exist in the database, we must add it
                        if (userDatabaseRoles.FirstOrDefault(s => s.role_id == role.Id) == null)
                        {
                            rolesToBeAdded.Add(role.Id);
                        }
                    }
                }
                foreach (var roleToAdd in rolesToBeAdded)
                {
                    context.role_users.Add(new role_users()
                    {
                        role_id = roleToAdd,
                        user_id = user_id
                    });
                }
                foreach (var roleForDeletion in userDatabaseRoles.Where(r =>r.role_id !=null && rolesToBeDeleted.Contains(r.role_id.Value)))
                {
                    context.role_users.Remove(roleForDeletion);
                }
                user.username = form.Username;
                user.email = form.Email;
                user.passed_text = form.Passed_Text;

                context.SaveChanges();

                return RedirectToAction("index");
            }
        }

        public ActionResult ResetPassword(int user_id)
        {
            using (var context = new unipiforumSQLEntities2())
            {
                var user = context.users.Find(user_id);
                if (user == null)
                    return HttpNotFound();

                return View(new UsersResetPassword
                {
                    Username = user.username
                });
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ResetPassword(int user_id, UsersResetPassword form)
        {
            using (var context = new unipiforumSQLEntities2())
            {
                var user = context.users.Find(user_id);
                if (user == null)
                    return HttpNotFound();

                form.Username = user.username;

                if (!ModelState.IsValid)
                    return View(form);

                user.password_hash = form.Password;
                //user.SetPassword(form.Password);

                context.SaveChanges();


                return RedirectToAction("index");

            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int user_id)
        {
            using (var context = new unipiforumSQLEntities2())
            {

                var user = context.users.Find(user_id);
                var roleusers = context.role_users.Where(p => p.user_id == user_id).ToList();

                if (user == null)
                    return HttpNotFound();
                foreach (var rl in roleusers)
                {
                    context.role_users.Remove(rl);
                }
                
                context.users.Remove(user);                //Database.Session.Delete(user);
                context.SaveChanges();
                return RedirectToAction("index");
            }
        }

        //[HttpPost, ValidateAntiForgeryToken]
        public ActionResult Results(int user_id)
        {
            using (var context = new unipiforumSQLEntities2())
            {
                var username = context.users.FirstOrDefault(p => p.user_id == user_id)?.username;
                return View(new ResultsViewModel()
                {
                    Username =username,
                    Results = context.results.Where(g => g.user_id == user_id).ToList()
                });

            }
        }

        public ActionResult UserGroup(int user_id)// Go to Group from a User
        {
            using (var context = new unipiforumSQLEntities2())
            {
                var user = context.users.Find(user_id);
                var urgroup = context.groups.Include("users").FirstOrDefault(p => p.group_id == user.group_id);


                return View(new UsGroup
                {
                    Group = urgroup


                });

            }
        }

       
    }
}