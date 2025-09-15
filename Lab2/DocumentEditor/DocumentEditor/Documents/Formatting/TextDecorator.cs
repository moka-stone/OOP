namespace DocumentEditor.Documents.Formatting
{
    public abstract class TextDecorator : ITextComponent
    {
        protected ITextComponent component;

        public TextDecorator(ITextComponent component)
        {
            this.component = component;
        }

        public virtual string GetContent()
        {
            return component.GetContent();
        }

        public virtual void SetContent(string content)
        {
            component.SetContent(content);
        }
    }
}