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
        public void UOpenDocument(DocumentManager document)
        {
            Console.WriteLine("Write filepath");
            string filepath = Console.ReadLine();
            document.OpenDocument(filepath);
        }
        public void UCreateDocument(DocumentManager document)
        {
            Console.WriteLine("Write type of document(PlainText,MarkDown,RichText)");
            string type = Console.ReadLine();
            document.CreateDocument(type);
        }

        public void UEditDocument(DocumentManager document, TextEditor text)
        {
            document.SetCurrentDocumentContent(text.Run());
        }
        public void USaveDocument(DocumentManager document)
        {
            Console.WriteLine("Write name.(txt,json,xml)");
            string name = Console.ReadLine();
            document.SaveDocument(name);
        }

        public void ManagePermissions(User user)
        {
            Console.WriteLine("Managing user permissions.");         
        }
    }
}
