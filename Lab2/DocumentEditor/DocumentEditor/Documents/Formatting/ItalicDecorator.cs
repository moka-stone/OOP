namespace DocumentEditor.Documents.Formatting
{
    public class ItalicDecorator : TextDecorator
    {
        public ItalicDecorator(ITextComponent component) : base(component) { }

        public override string GetContent()
        {
            return $"_{base.GetContent()}_";
        }
    }
}