using DocumentEditor.Documents;

namespace DocumentEditor.Users
{
    public class Editor : IUserRole
    {
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
            // Редактор не может управлять правами пользователей
            throw new System.UnauthorizedAccessException("Editors cannot manage permissions");
        }
    }
}
