namespace DocumentEditor.Documents.Formatting
{
    public interface ITextComponent
    {
        string GetContent();
        void SetContent(string content);
    }
}