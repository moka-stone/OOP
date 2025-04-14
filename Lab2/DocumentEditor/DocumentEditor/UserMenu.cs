using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using DocumentEditor.Documents;
using DocumentEditor.Users;


namespace DocumentEditor
{
    public class UserMenu
    {
        public User currentUser = null;
        public User viewer;
        public User editor;
        public User admin;
        private TextEditor textEditor;
        private DocumentManager docManager;

        public UserMenu() 
        {
            this.docManager = new DocumentManager();
            this.textEditor = new TextEditor("");
            this.viewer = new User("Bob", new Viewer());
            this.editor = new User("Ivan", new Editor());
            this.admin = new User("Makar", new Admin());
        }
        
        public void Run()
        {
            while (true)
            {
                Console.WriteLine("\nChoose a user:");
                Console.WriteLine("1. Alice (Viewer)");
                Console.WriteLine("2. Bob (Editor)");
                Console.WriteLine("3. Charlie (Admin)");
                Console.WriteLine("0. Exit");
                Console.Write("Select an option: ");

                string userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        currentUser = viewer;
                        break;
                    case "2":
                        currentUser = editor;
                        break;
                    case "3":
                        currentUser = admin;
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        continue;
                }

                while (currentUser != null)
                {
                    Console.WriteLine($"\nCurrent User Role: {currentUser.Role}");
                    Console.WriteLine("Choose an action:");
                    Console.WriteLine("1. Open Document");
                    Console.WriteLine("2. Create Document");
                    Console.WriteLine("3. Edit Document");
                    Console.WriteLine("4. Save Document");
                    Console.WriteLine("5. Manage Permissions (Admin only)");
                    Console.WriteLine("6. Manage Settings (Admin or Editor)");      
                    Console.WriteLine("0. Switch User");
                    Console.Write("Select an option: ");

                    string actionInput = Console.ReadLine();
                    switch (actionInput)
                    {
                        case "1":
                            currentUser.OpenDocument(docManager);
                            break;
                        case "2":
                            currentUser.CreateDocument(docManager);
                            break;
                        case "3":                        
                            currentUser.EditDocument(docManager,textEditor);                         
                            break;
                        case "4":
                            currentUser.SaveDocument(docManager);
                            break;
                        case "5":
                            if (currentUser is Admin)
                            {
                                
                                Console.WriteLine("Managing permissions...");
                            }
                            else
                            {
                                Console.WriteLine("This action is only for Admin.");
                            }
                            break;
                        case "6":
                            if (currentUser is Admin || currentUser is Editor)
                            {
                                //EditorSettings settings = new EditorSettings();
                                //currentUser.ManageSettings(settings); // Логика управления настройками
                                Console.WriteLine("Managing settings..");
                            }
                            else
                            {
                                Console.WriteLine("This action is only for Admin and Editor.");
                            }
                            break;
                        case "0":
                            currentUser = null; 
                            break;
                        default:
                            Console.WriteLine("Invalid option. Try again.");
                            break;
                    }
                }

            }
        }
        
    }
}
