using System;
using System.Linq;
using DocumentEditor.Documents;
using DocumentEditor.Users;
using DocumentEditor.Notifications;

namespace DocumentEditor
{
    public class UserMenu
    {
        private readonly UserManager userManager;
        private readonly DocumentManager documentManager;
        private TextEditor textEditor;
        private readonly NotificationManager notificationManager;
        private User currentUser;
        private const string ADMIN_CODE = "0000";
        private Documents.Document currentDocument;

        public UserMenu()
        {
            userManager = UserManager.Instance;
            documentManager = new DocumentManager();
            notificationManager = NotificationManager.Instance;
            textEditor = new TextEditor(string.Empty);
        }

        public void ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();
                ShowUnreadNotificationsCount();
                Console.WriteLine("=== Document Editor ===");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register");
                Console.WriteLine("3. Exit");
                Console.Write("Choose option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Login();
                        if (currentUser != null)
                        {
                            ShowUserMenu();
                        }
                        break;
                    case "2":
                        Register();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }

        private void Login()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            var users = userManager.GetAllUsers();
            currentUser = users.FirstOrDefault(u => u.Name == username);
            if (currentUser == null)
            {
                Console.WriteLine("User not found");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        private void Register()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter admin code (or press Enter to skip): ");
            string adminCode = Console.ReadLine();
            currentUser = userManager.CreateUser(username, adminCode);
            Console.WriteLine("User registered successfully");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            ShowUserMenu();
        }

        private void ShowUserMenu()
        {
            while (true)
            {
                Console.Clear();
                ShowUnreadNotificationsCount();
                Console.WriteLine($"=== Welcome, {currentUser.Name} ===");
                Console.WriteLine("1. Create Document");
                Console.WriteLine("2. Open Document");
                Console.WriteLine("3. List All Documents");
                Console.WriteLine("4. View Notifications");
                Console.WriteLine("5. Enter Admin Code");
                Console.WriteLine("6. Logout");
                Console.Write("Choose option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        CreateDocument();
                        break;
                    case "2":
                        OpenDocument();
                        break;
                    case "3":
                        ListAllDocuments();
                        break;
                    case "4":
                        ShowNotifications();
                        break;
                    case "5":
                        EnterAdminCode();
                        break;
                    case "6":
                        currentUser = null;
                        currentDocument = null;
                        return;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }

        private void ShowUnreadNotificationsCount()
        {
            if (currentUser != null)
            {
                var unreadCount = notificationManager.GetUnreadNotifications(currentUser.Id).Count;
                if (unreadCount > 0)
                {
                    Console.WriteLine($"You have {unreadCount} unread notifications!");
                }
            }
        }

        private void ShowNotifications()
        {
            var notifications = notificationManager.GetUserNotifications(currentUser.Id);
            if (!notifications.Any())
            {
                Console.WriteLine("No notifications");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Notifications ===");
                foreach (var notification in notifications)
                {
                    Console.WriteLine($"[{(notification.IsRead ? " " : "*")}] {notification.CreatedAt:g} - {notification.Message}");
                }

                Console.WriteLine("\n1. Mark all as read");
                Console.WriteLine("2. Return to menu");
                Console.Write("Choose option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        notificationManager.MarkAllAsRead(currentUser.Id);
                        notifications = notificationManager.GetUserNotifications(currentUser.Id);
                        break;
                    case "2":
                        return;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }

        private void EnterAdminCode()
        {
            if (currentUser.IsAdmin)
            {
                Console.WriteLine("You are already an admin");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            Console.Write("Enter admin code: ");
            string code = Console.ReadLine();
            if (code == ADMIN_CODE)
            {
                currentUser.IsAdmin = true;
                Console.WriteLine("Admin rights granted");
            }
            else
            {
                Console.WriteLine("Invalid admin code");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void CreateDocument()
        {
            Console.Write("Enter document name: ");
            string name = Console.ReadLine();
            Console.WriteLine("\nSelect document type:");
            Console.WriteLine("1. Plain Text");
            Console.WriteLine("2. Rich Text");
            Console.WriteLine("3. Markdown");
            Console.Write("Choose type: ");
            
            string typeChoice = Console.ReadLine();
            string type = typeChoice switch
            {
                "1" => "plain",
                "2" => "rich",
                "3" => "markdown",
                _ => "plain"
            };

            Console.WriteLine("\nEnter content (press Enter twice to finish):");
            var content = new System.Text.StringBuilder();
            string line;
            bool previousLineEmpty = false;

            while (true)
            {
                line = Console.ReadLine();
                if (string.IsNullOrEmpty(line) && previousLineEmpty)
                    break;
                
                previousLineEmpty = string.IsNullOrEmpty(line);
                content.AppendLine(line);
            }

            try
            {
                currentDocument = documentManager.CreateDocument(name, content.ToString(), type, currentUser.Id);
                Console.WriteLine("Document created successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating document: {ex.Message}");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void OpenDocument()
        {
            Console.Write("Enter document name: ");
            string name = Console.ReadLine();
            try
            {
                currentDocument = documentManager.OpenDocument(name, currentUser.Id);
                ShowDocumentMenu();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error opening document: {ex.Message}");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        private void ShowDocumentMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== Document: {currentDocument.GetType().Name.Replace("Document", "")} ===");
                Console.WriteLine($"Content:\n{currentDocument.Content}");
                Console.WriteLine($"Your role: {(currentUser.CanEditDocument(currentDocument) ? "Editor" : "Viewer")}");
                Console.WriteLine("\n1. Edit Document");
                Console.WriteLine("2. Save Document");
                Console.WriteLine("3. Delete Document");
                Console.WriteLine("4. Return to Main Menu");
                Console.Write("Choose option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        if (currentUser.CanEditDocument(currentDocument))
                        {
                            textEditor = new TextEditor(currentDocument.Content);
                            string newContent = textEditor.Run();
                            if (!string.IsNullOrEmpty(newContent))
                            {
                                currentDocument.Content = newContent;
                                Console.WriteLine("Document updated");
                            }
                        }
                        else
                        {
                            Console.WriteLine("You don't have permission to edit this document");
                        }
                        break;
                    case "2":
                        if (currentUser.CanEditDocument(currentDocument))
                        {
                            Console.Write("Enter file name to save: ");
                            string fileName = Console.ReadLine();
                            try
                            {
                                documentManager.SaveDocument(currentDocument, fileName);
                                Console.WriteLine("Document saved successfully");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error saving document: {ex.Message}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("You don't have permission to save this document");
                        }
                        break;
                    case "3":
                        if (currentUser.CanEditDocument(currentDocument))
                        {
                            Console.Write("Enter file name to delete: ");
                            string fileName = Console.ReadLine();
                            documentManager.DeleteDocument(fileName);
                            return;
                        }
                        else
                        {
                            Console.WriteLine("You don't have permission to delete this document");
                        }
                        break;
                    case "4":
                        currentDocument = null;
                        return;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        private void ListAllDocuments()
        {
            var documents = documentManager.GetAllDocuments();
            Console.WriteLine("\nAll Documents:");
            foreach (var (name, ext, creatorId) in documents)
            {
                var creator = userManager.GetUserById(creatorId)?.Name ?? "Unknown";
                Console.WriteLine($"Name: {name}, Type: {ext}, Creator: {creator}");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
