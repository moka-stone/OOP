using DocumentEditor.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// strategy pattern: user divides to viewer, editor and admin with same metods but other realisation

namespace DocumentEditor.Users
{
    public interface IUserRole
    {
        void UOpenDocument(DocumentManager document);
        void UCreateDocument(DocumentManager document);
        void UEditDocument(DocumentManager document, TextEditor text);
        void USaveDocument(DocumentManager document);
        void ManagePermissions(User user);

    }
}
