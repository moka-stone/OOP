namespace DocumentEditor.Documents.Observer
{
    public interface IDocumentSubject
    {
        void Attach(IDocumentObserver observer);
        void Detach(IDocumentObserver observer);
        void Notify(string message);
    }
} 