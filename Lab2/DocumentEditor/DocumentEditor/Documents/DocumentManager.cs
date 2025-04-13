using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DocumentEditor.Documents
{
    public class DocumentManager
    {
        private Document? currentDocument;
        public string GetCurrentDocumentContent()
        {
            if (currentDocument != null)
            {
                return currentDocument.Content;
            }
            else { throw new Exception("No document selected"); }
                
        }
        public void SetCurrentDocumentContent(string content) 
        {
            this.currentDocument.Content = content;
        }

        public void CreateDocument(string type)
        {
            currentDocument = type switch
            {
                "PlainText" => new PlainTextDocument(),
                "MarkDown" => new MarkDownDocument(),
                "RichText" => new RichTextDocument(),
                _ => throw new ArgumentException("Unsupported document type")
            };          

        }
        
        public void OpenDocument(string filePath) 
        {
            string extension = Path.GetExtension(filePath).ToLower();
            switch (extension) 
            {
                case ".txt":
                    OpenTxtFile(filePath);
                    break;
                case ".json":
                    OpenJsonFile(filePath);
                    break;
                case ".xml":
                    OpenXmlFile(filePath);
                    break;
                default: throw new Exception("Unsupported file extension");
            }
        }

        private void OpenTxtFile(string filePath) 
        {
            string content = File.ReadAllText(filePath);
            currentDocument = new PlainTextDocument {Content = content};
            
        }
        private void OpenJsonFile(string filePath)
        {
            string json = File.ReadAllText(filePath);
            currentDocument = JsonSerializer.Deserialize<Document>(json);
        }
        private void OpenXmlFile(string filePath)
        {
            throw new Exception("Unsupported yet"); // Only for structure, don't want it 
        }

        public void SaveDocument(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLower();
            switch (extension)
            {
                case ".txt":
                    File.WriteAllText(filePath, currentDocument.Content);
                    break;
                case ".json":
                    string json = JsonSerializer.Serialize(currentDocument);
                    File.WriteAllText(filePath, json);
                    break;
                case ".xml":
                    // Логика для сериализации в XML
                    break;
                default:
                    throw new ArgumentException("Unsupported format");
            }
        }
        public void DeleteDocument(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                currentDocument = null; // Очистить текущий документ
                Console.WriteLine($"Document '{filePath}' has been deleted.");
            }
            else
            {
                Console.WriteLine($"File '{filePath}' does not exist.");
            }
        }
    }
}
