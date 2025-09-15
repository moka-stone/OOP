using DocumentEditor.Documents;

namespace DocumentEditor.Users
{
    public interface IUserRole
    {
        void UOpenDocument(DocumentManager documentManager, string fileName, string creatorId);
        void UCreateDocument(DocumentManager documentManager, string fileName, string content, string type, string creatorId);
        void UEditDocument(DocumentManager documentManager, Document document, string newContent);
        void USaveDocument(DocumentManager documentManager, Document document, string fileName);
        void ManagePermissions(User user);
    }
}
