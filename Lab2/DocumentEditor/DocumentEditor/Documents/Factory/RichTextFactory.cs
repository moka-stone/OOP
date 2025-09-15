namespace DocumentEditor.Documents.Factory
{
    public class RichTextFactory : IDocumentFactory
    {
        public Document CreateDocument()
        {
            return new RichTextDocument();
        }
    }
} 