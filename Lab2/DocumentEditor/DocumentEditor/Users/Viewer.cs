using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DocumentEditor.Documents;


namespace DocumentEditor.Users
{
    public class Viewer : IUserRole
    {
        public Viewer() { }
        public void UOpenDocument(DocumentManager documentManager, string fileName, string creatorId)
        {
            documentManager.OpenDocument(fileName, creatorId);
        }
        public void UCreateDocument(DocumentManager documentManager, string fileName, string content, string type, string creatorId)
        {
            throw new UnauthorizedAccessException("Viewers cannot create documents");
        }

        public void UEditDocument(DocumentManager documentManager, Document document, string newContent)
        {
            throw new UnauthorizedAccessException("Viewers cannot edit documents");
        }
        public void USaveDocument(DocumentManager documentManager, Document document, string fileName)
        {
            throw new UnauthorizedAccessException("Viewers cannot save documents");
        }

        public void ManagePermissions(User user)
        {
            throw new UnauthorizedAccessException("Viewers cannot manage permissions");
        }
    }
}
