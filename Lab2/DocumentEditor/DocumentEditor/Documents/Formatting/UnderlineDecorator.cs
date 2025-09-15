namespace DocumentEditor.Documents.Formatting
{
    public class UnderlineDecorator : TextDecorator
    {
        public UnderlineDecorator(ITextComponent component) : base(component) { }

        public override string GetContent()
        {
            return $"~{base.GetContent()}~";
        }
    }
}