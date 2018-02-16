using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using UnipiForum.Models;

namespace UnipiForum.Controllers
{
    public class ChatController : Controller
    {
        // lastTimeChecked: the datetime (could send messageId) of the LAST chat message of the client
        public JsonResult GetChatMessages(DateTime lastTimeChecked)
        {
            List<int> groupCodes = new List<int>();
            using (var context = new unipiforumSQLEntities2())
            {
                var groupId = context.users.FirstOrDefault(u => u.username == User.Identity.Name)?.group_id;
                if (groupId == null)
                    groupCodes.AddRange(context.groups.Select(g => g.group_id));
                else
                    groupCodes.Add(groupId.Value);
            }

            var hasNewMessages = false;
            var count = 0;
            var messages = new List<ChatMessageViewModel>();
            while (!hasNewMessages)
            {
                count++;
                //Checks the db every 5seconds
                Thread.Sleep(5000);
                //After 30seconds of no messages it returns to the client with no messages.
                if (count > 30)
                    break;
                messages = GetNewMessages(groupCodes, lastTimeChecked);
                if (messages.Any())
                    hasNewMessages = true;
            }

            return Json(messages, JsonRequestBehavior.DenyGet);
        }

        private List<ChatMessageViewModel> GetNewMessages(List<int> groupIds, DateTime lastTimeChecked)
        {
            var messages = new List<ChatMessageViewModel>();
            if (groupIds == null || !groupIds.Any())
                return messages;
            using (var context = new unipiforumSQLEntities2())
            {
                var chatRooms = context.chats.Where(u => groupIds.Contains(u.group_id))?.ToList();
                var newMessages = chatRooms?.Where(s => s.chat_time > lastTimeChecked);
                foreach (var newMEssage in newMessages)
                {
                    messages.Add(new ChatMessageViewModel()
                    {
                        ChatId = newMEssage.id,
                        Content = newMEssage.chat_text,
                        Timestamp = newMEssage.chat_time,
                        SenderId = newMEssage.user_id,
                        GroupId = newMEssage.group_id
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
                context.chats.Add(new chat()
                {
                    group_id = message.GroupId,
                    user_id = message.SenderId,
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



    public class ChatMessageViewModel
    {
        public int ChatId { get; set; }
        public int GroupId { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public int SenderId { get; set; }
        public string SenderName { get; set; }
    }
}