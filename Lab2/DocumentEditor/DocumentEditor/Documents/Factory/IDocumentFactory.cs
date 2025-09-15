namespace DocumentEditor.Documents.Factory
{
    public interface IDocumentFactory
    {
        Document CreateDocument(string creatorId);
    }
} 