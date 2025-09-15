namespace DocumentEditor.Documents.Formatting
{
    public class TextComponent : ITextComponent
    {
        private string content;

        public TextComponent(string content)
        {
            this.content = content;
        }

        public string GetContent()
        {
            return content;
        }

        public void SetContent(string content)
        {
            this.content = content;
        }
    }
}