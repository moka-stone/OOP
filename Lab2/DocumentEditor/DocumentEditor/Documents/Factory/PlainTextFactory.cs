namespace DocumentEditor.Documents.Factory
{
    public class PlainTextFactory : IDocumentFactory
    {
        public Document CreateDocument(string creatorId)
        {
            return new PlainTextDocument(creatorId);
        }
    }
} 