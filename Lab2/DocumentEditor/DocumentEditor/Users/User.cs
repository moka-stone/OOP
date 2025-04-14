using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentEditor.Documents;

namespace DocumentEditor.Users
{
    public class User
    {
        public string Name { get; set; }
        public IUserRole Role { get; set; }

        public User(string name, IUserRole role)
        {
            Name = name;
            Role = role;
        }

        public void OpenDocument(DocumentManager document)
        {
            Role.UOpenDocument(document);
        }
        public void CreateDocument(DocumentManager document)
        {
            Role.UCreateDocument(document);
        }

        public void EditDocument(DocumentManager document, TextEditor text)
        {
            Role.UEditDocument(document,text);
        }
        public void SaveDocument(DocumentManager document)
        {
            Role.USaveDocument(document);
        }

        public void ManagePermissions(User user)
        {
            Role.ManagePermissions(user);
        }
    }
}
