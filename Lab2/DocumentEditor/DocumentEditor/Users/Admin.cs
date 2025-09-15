using DocumentEditor.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentEditor.Users
{
    public class Admin : IUserRole
    {
        public Admin() { }
        
        public void UOpenDocument(DocumentManager documentManager, string fileName, string creatorId)
        {
            documentManager.OpenDocument(fileName, creatorId);
        }

        public void UCreateDocument(DocumentManager documentManager, string fileName, string content, string type, string creatorId)
        {
            documentManager.CreateDocument(fileName, content, type, creatorId);
        }

        public void UEditDocument(DocumentManager documentManager, Document document, string newContent)
        {
            document.Content = newContent;
        }

        public void USaveDocument(DocumentManager documentManager, Document document, string fileName)
        {
            documentManager.SaveDocument(document, fileName);
        }

        public void ManagePermissions(User user)
        {
            // Администратор может управлять правами пользователей
            // Реализация будет добавлена позже
        }
    }
}
