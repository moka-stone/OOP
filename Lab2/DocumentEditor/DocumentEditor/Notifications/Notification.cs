using System;

namespace DocumentEditor.Notifications
{
    public class Notification
    {
        public string Id { get; }
        public string UserId { get; }
        public string Message { get; }
        public DateTime CreatedAt { get; }
        public bool IsRead { get; set; }
        public string DocumentName { get; }
        public NotificationType Type { get; }

        public Notification(string userId, string message, string documentName, NotificationType type)
        {
            Id = Guid.NewGuid().ToString();
            UserId = userId;
            Message = message;
            DocumentName = documentName;
            Type = type;
            CreatedAt = DateTime.Now;
            IsRead = false;
        }
    }

    public enum NotificationType
    {
        DocumentCreated,
        DocumentEdited,
        DocumentDeleted,
        DocumentViewed
    }
} 