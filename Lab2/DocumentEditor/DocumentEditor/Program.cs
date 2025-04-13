using DocumentEditor;
using DocumentEditor.Documents;
class Program 
{
    static void Main(string[] args) 
    {
        var documentManager = new DocumentManager();

        Console.WriteLine("Введите тип документа (PlainText, MarkDown, RichText):");
        string docType = Console.ReadLine();
        documentManager.CreateDocument(docType);

        var textEditor = new TextEditor(documentManager.GetCurrentDocumentContent());
        textEditor.Run();
        documentManager.SetCurrentDocumentContent(textEditor.TransportContent());
        documentManager.SaveDocument("Textedittest1.txt");

    }
}