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
        public void UOpenDocument(DocumentManager document)
        {
            Console.WriteLine("Write filepath");
            string filepath = Console.ReadLine();
            document.OpenDocument(filepath);
        }
        public void UCreateDocument(DocumentManager document)
        {
            Console.WriteLine("Viewer can't create.");           
        }

        public void UEditDocument(DocumentManager document,TextEditor text)
        {
            Console.WriteLine("Viewer cannot edit documents.");
        }
        public void USaveDocument(DocumentManager document)
        {
            Console.WriteLine("Viewer cannot save documents.");
        }

        public void ManagePermissions(User user)
        {
            Console.WriteLine("Viewer cannot manage permissions.");
        }
    }
}
