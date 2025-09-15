namespace DocumentEditor.Documents.Factory
{
    public class MarkDownFactory : IDocumentFactory
    {
        public Document CreateDocument()
        {
            return new MarkDownDocument();
        }
    }
} 