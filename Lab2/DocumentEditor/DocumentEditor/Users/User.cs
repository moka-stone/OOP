using System;
using System.Text.Json.Serialization;
using DocumentEditor.Documents;

namespace DocumentEditor.Users
{
    public class User
    {
        public string Id { get; }
        public string Name { get; }
        public bool IsAdmin { get; set; }

        public User(string name, bool isAdmin = false)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            IsAdmin = isAdmin;
        }

        public bool CanEditDocument(Documents.Document document)
        {
            return IsAdmin || document.CreatorId == Id;
        }

        public bool CanViewDocument(Documents.Document document)
        {
            return true; // Все пользователи могут просматривать документы
        }

        public bool CanDeleteDocument(Documents.Document document)
        {
            return IsAdmin || document.CreatorId == Id;
        }

        public void OpenDocument(DocumentManager documentManager, string fileName)
        {
            documentManager.OpenDocument(fileName, Id);
        }

        public void CreateDocument(DocumentManager documentManager, string fileName, string content, string type)
        {
            documentManager.CreateDocument(fileName, content, type, Id);
        }

        public void EditDocument(Document document, string newContent)
        {
            if (CanEditDocument(document))
            {
                document.Content = newContent;
            }
            else
            {
                throw new UnauthorizedAccessException("You don't have permission to edit this document");
            }
        }

        public void SaveDocument(DocumentManager documentManager, Document document, string fileName)
        {
            if (CanEditDocument(document))
            {
                documentManager.SaveDocument(document, fileName);
            }
            else
            {
                throw new UnauthorizedAccessException("You don't have permission to save this document");
            }
        }
    }
}
