namespace DocumentEditor.Documents.Formatting
{
    public class BoldDecorator : TextDecorator
    {
        public BoldDecorator(ITextComponent component) : base(component) { }

        public override string GetContent()
        {
            return $"**{base.GetContent()}**";
        }
    }
}