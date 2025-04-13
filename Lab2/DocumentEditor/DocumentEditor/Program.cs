using DocumentEditor.Documents;
class Program 
{
    static void Main(string[] args) 
    {
        DocumentManager documentManager = new DocumentManager();
        string docname = Console.ReadLine();
        documentManager.CreateDocument(docname);
        string filename = Console.ReadLine();
        documentManager.T1234("1234\n5555");
        documentManager.ShowContent();
        documentManager.SaveDocument(filename);


    }
}