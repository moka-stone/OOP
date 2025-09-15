using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentEditor.Documents
{
    public class RichTextDocument : Document
    {
        public RichTextDocument(string creatorId) : base(creatorId)
        {
        }

        public override string GetFileExtension()
        {
            return ".rtf";
        }
    }
}
