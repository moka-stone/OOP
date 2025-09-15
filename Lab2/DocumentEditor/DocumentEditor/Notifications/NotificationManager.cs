using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;

namespace DocumentEditor.Notifications
{
    public class NotificationManager
    {
        private static NotificationManager instance;
        private readonly List<Notification> notifications;
        private readonly string notificationsFilePath = "notifications.json";

        private NotificationManager()
        {
            notifications = new List<Notification>();
            LoadNotifications();
        }

        public static NotificationManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NotificationManager();
                }
                return instance;
            }
        }

        public void AddNotification(string userId, string message, string documentName, NotificationType type)
        {
            var notification = new Notification(userId, message, documentName, type);
            notifications.Add(notification);
            SaveNotifications();
        }

        public List<Notification> GetUserNotifications(string userId)
        {
            return notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToList();
        }

        public List<Notification> GetUnreadNotifications(string userId)
        {
            return notifications
                .Where(n => n.UserId == userId && !n.IsRead)
                .OrderByDescending(n => n.CreatedAt)
                .ToList();
        }

        public void MarkAsRead(string notificationId)
        {
            var notification = notifications.FirstOrDefault(n => n.Id == notificationId);
            if (notification != null)
            {
                notification.IsRead = true;
                SaveNotifications();
            }
        }

        public void MarkAllAsRead(string userId)
        {
            var userNotifications = notifications.Where(n => n.UserId == userId && !n.IsRead);
            foreach (var notification in userNotifications)
            {
                notification.IsRead = true;
            }
            SaveNotifications();
        }

        private void LoadNotifications()
        {
            if (File.Exists(notificationsFilePath))
            {
                try
                {
                    var jsonString = File.ReadAllText(notificationsFilePath);
                    var loadedNotifications = JsonSerializer.Deserialize<List<Notification>>(jsonString);
                    notifications.Clear();
                    notifications.AddRange(loadedNotifications);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading notifications: {ex.Message}");
                }
            }
        }

        private void SaveNotifications()
        {
            try
            {
                var jsonString = JsonSerializer.Serialize(notifications);
                File.WriteAllText(notificationsFilePath, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving notifications: {ex.Message}");
            }
        }

        public void DeleteOldNotifications(int daysOld = 30)
        {
            var cutoffDate = DateTime.Now.AddDays(-daysOld);
            notifications.RemoveAll(n => n.CreatedAt < cutoffDate);
            SaveNotifications();
        }
    }
} 