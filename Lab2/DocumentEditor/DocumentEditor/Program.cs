using DocumentEditor;
using DocumentEditor.Documents;
class Program 
{
    static void Main(string[] args) 
    {
        var documentManager = new DocumentManager();

        documentManager.OpenDocument("Textedittest1.txt");

        var textEditor = new TextEditor(documentManager.GetCurrentDocumentContent());      
        documentManager.SetCurrentDocumentContent(textEditor.Run());
        documentManager.SaveDocument("Textedittest1.txt");

    }
}