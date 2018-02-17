using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using UnipiForum.Areas.Admin.ViewModels;
using UnipiForum.Infrastructure;
using UnipiForum.Models;
using UnipiForum.ViewModels;

namespace UnipiForum.Areas.Admin.Controllers
{
    //[Authorize(Roles = "admin")]
    [SelectedTab("groups")]
    public class GroupsController : Controller
    {
        // GET: Admin/Posts
        public ActionResult AllGroups()// Groups page for admin
        {
            using (var context = new unipiforumSQLEntities2())
            {
                var _groups = context.groups.Include("users");


                return View(new GroupsIndex
                {
                    Groups = _groups.ToList()


                });

            }
        }
        public ActionResult GTG(int group_id)// Go to Group when you pick a group from groups page
        {
            using (var context = new unipiforumSQLEntities2())
            {
                var user = context.users.First(s => s.username == User.Identity.Name);
                var group = context.groups.Include("users").FirstOrDefault(p => p.group_id == group_id);
                var chatMessages = context.chats.Where(s => s.group_id == group_id).OrderBy(t => t.chat_time);
                var chatModel = new List<ChatMessageViewModel>();
                foreach (var c in chatMessages)
                {
                    chatModel.Add(new ChatMessageViewModel()
                    {
                        ChatId = c.id,
                        SenderId = c.user_id,
                        IsCurrentUserMessage = c.user_id == user.user_id,
                        SenderName = user.username,
                        SenderImagePath = $"/Files/user{c.user_id}.jpg",
                        Content = c.chat_text,
                        GroupId = group_id,
                        Timestamp = c.chat_time
                    });
                }

                return View(new GTG
                {
                    Group = group,
                    ChatMessages = chatModel
                });

            }
        }

        // lastTimeChecked: the datetime (could send messageId) of the LAST chat message of the client
        public ActionResult GetChatMessages(int lastNotificationId, int groupId)
        {
            int? userId = 0;
            using (var context = new unipiforumSQLEntities2())
            {
                userId = context.users.FirstOrDefault(u => u.username == User.Identity.Name)?.user_id;
            }

            var hasNewMessages = false;
            var count = 0;
            var messages = new List<ChatMessageViewModel>();
            while (!hasNewMessages)
            {
                messages = GetNewMessages(lastNotificationId, userId.Value, groupId);
                if (messages.Any())
                    hasNewMessages = true;

                count++;
                //Checks the db every 5seconds
                Thread.Sleep(1000);
                //After 30seconds of no messages it returns to the client with no messages.
                if (count > 6)
                    break;


            }

            return Json(messages, JsonRequestBehavior.AllowGet);
        }

        private List<ChatMessageViewModel> GetNewMessages(int lastNotificationId, int currentUserId, int groupId)
        {
            var messages = new List<ChatMessageViewModel>();

            using (var context = new unipiforumSQLEntities2())
            {
                var lastNotification = context.chats.FirstOrDefault(s => s.id == lastNotificationId);
                if (lastNotification == null)
                {
                    lastNotification = new chat()
                    {
                        chat_time = DateTime.UtcNow
                    };
                }
                var lastNotificationTimestamp = lastNotification.chat_time;
                var newChatMessages =
                    context.chats.Where(s => s.group_id == groupId && s.chat_time > lastNotificationTimestamp).ToList();

                foreach (var newMEssage in newChatMessages.Where(s => s.chat_time > lastNotificationTimestamp).ToList())
                {
                    messages.Add(new ChatMessageViewModel()
                    {
                        ChatId = newMEssage.id,
                        Content = newMEssage.chat_text,
                        Timestamp = newMEssage.chat_time,
                        SenderId = newMEssage.user_id,
                        GroupId = newMEssage.group_id,
                        IsCurrentUserMessage = newMEssage.user_id == currentUserId,
                        SenderImagePath = $"/Files/user{newMEssage.user_id}.jpg"
                    });
                }
            }
            return messages;
        }

        public JsonResult SendMessage(ChatMessageViewModel message)
        {
            var success = true;

            using (var context = new unipiforumSQLEntities2())
            {
                var userId = context.users.FirstOrDefault(u => u.username == User.Identity.Name)?.user_id;

                context.chats.Add(new chat()
                {
                    group_id = message.GroupId,
                    user_id = userId.Value,
                    chat_time = DateTime.UtcNow,
                    chat_text = message.Content
                });
                var result = context.SaveChanges();
                if (result < 1)
                    success = false;
            }

            return Json(success, JsonRequestBehavior.AllowGet);
        }
    }
}