using System;

namespace UnipiForum.ViewModels
{
    public class ChatMessageViewModel
    {
        public int ChatId { get; set; }
        public int GroupId { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public int SenderId { get; set; }
        public string SenderName { get; set; }
        public bool IsCurrentUserMessage { get; set; }
        public string SenderImagePath { get; set; }
    }
}