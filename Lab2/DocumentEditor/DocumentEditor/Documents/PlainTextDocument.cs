using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentEditor.Documents
{
    public class PlainTextDocument : Document
    {
        public PlainTextDocument(string creatorId) : base(creatorId)
        {
        }

        public override string GetFileExtension()
        {
            return ".txt";
        }
    }
}
