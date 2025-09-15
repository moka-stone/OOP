using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using DocumentEditor.Documents.Factory;
using DocumentEditor.Storage;
using DocumentEditor.Notifications;
using System.Linq;

namespace DocumentEditor.Documents
{
    public class DocumentManager
    {
        private readonly Dictionary<string, IDocumentFactory> documentFactories;
        private readonly IStorageImplementor storage;
        private readonly string documentsDirectory = "Documents";
        private readonly string metadataDirectory = "Documents/Metadata";
        private readonly NotificationManager notificationManager;

        public DocumentManager(IStorageImplementor storage = null)
        {
            this.storage = storage ?? new LocalStorage();
            documentFactories = new Dictionary<string, IDocumentFactory>
            {
                { "plain", new PlainTextFactory() },
                { "markdown", new MarkDownFactory() },
                { "rich", new RichTextFactory() }
            };
            notificationManager = NotificationManager.Instance;

            if (!Directory.Exists(documentsDirectory))
            {
                Directory.CreateDirectory(documentsDirectory);
            }
            if (!Directory.Exists(metadataDirectory))
            {
                Directory.CreateDirectory(metadataDirectory);
            }
        }

        public Documents.Document CreateDocument(string fileName, string content, string documentType, string creatorId)
        {
            if (documentFactories.TryGetValue(documentType.ToLower(), out var factory))
            {
                var document = factory.CreateDocument(creatorId);
                document.Content = content;

                string fullFileName = Path.Combine(documentsDirectory, fileName + document.GetFileExtension());
                string metadataFileName = Path.Combine(metadataDirectory, fileName + ".metadata");

                if (File.Exists(fullFileName))
                {
                    throw new IOException($"File {fileName}{document.GetFileExtension()} already exists");
                }

                // Сохраняем метаданные
                var metadata = new DocumentMetadata(creatorId, documentType);
                string metadataJson = JsonSerializer.Serialize(metadata);
                File.WriteAllText(metadataFileName, metadataJson);

                // Сохраняем содержимое
                SaveDocumentToFile(document, fullFileName);

                // Отправляем уведомление о создании документа
                notificationManager.AddNotification(
                    creatorId,
                    $"Document {fileName} has been created",
                    fileName,
                    NotificationType.DocumentCreated
                );

                return document;
            }
            throw new ArgumentException($"Unknown document type: {documentType}");
        }

        public Documents.Document OpenDocument(string fileName, string userId)
        {
            var supportedExtensions = new[] { ".txt", ".md", ".rtf" };
            string foundFile = null;
            string extension = "";

            foreach (var ext in supportedExtensions)
            {
                string fullPath = Path.Combine(documentsDirectory, fileName + ext);
                if (File.Exists(fullPath))
                {
                    foundFile = fullPath;
                    extension = ext;
                    break;
                }
            }

            if (foundFile == null)
            {
                throw new FileNotFoundException($"Document {fileName} not found");
            }

            // Загружаем метаданные
            string metadataPath = Path.Combine(metadataDirectory, fileName + ".metadata");
            if (!File.Exists(metadataPath))
            {
                throw new FileNotFoundException($"Document metadata not found");
            }

            var metadata = JsonSerializer.Deserialize<DocumentMetadata>(File.ReadAllText(metadataPath));
            
            // Создаем соответствующий тип документа
            Documents.Document document = null;
            switch (metadata.DocumentType.ToLower())
            {
                case "plain":
                    document = documentFactories["plain"].CreateDocument(metadata.CreatorId);
                    break;
                case "markdown":
                    document = documentFactories["markdown"].CreateDocument(metadata.CreatorId);
                    break;
                case "rich":
                    document = documentFactories["rich"].CreateDocument(metadata.CreatorId);
                    break;
                default:
                    throw new ArgumentException("Unsupported document type");
            }

            // Загружаем содержимое
            document.Content = File.ReadAllText(foundFile);

            // Отправляем уведомление о просмотре документа создателю
            if (userId != metadata.CreatorId)
            {
                notificationManager.AddNotification(
                    metadata.CreatorId,
                    $"User viewed your document {fileName}",
                    fileName,
                    NotificationType.DocumentViewed
                );
            }

            return document;
        }

        public void SaveDocument(Documents.Document document, string fileName)
        {
            string fullPath = Path.Combine(documentsDirectory, fileName + document.GetFileExtension());
            string metadataPath = Path.Combine(metadataDirectory, fileName + ".metadata");

            if (!File.Exists(metadataPath))
            {
                throw new FileNotFoundException("Document metadata not found");
            }

            // Обновляем метаданные
            var metadata = JsonSerializer.Deserialize<DocumentMetadata>(File.ReadAllText(metadataPath));
            metadata.LastModified = DateTime.Now;
            File.WriteAllText(metadataPath, JsonSerializer.Serialize(metadata));

            // Сохраняем содержимое
            SaveDocumentToFile(document, fullPath);

            // Отправляем уведомление об изменении документа создателю
            if (document.CreatorId != metadata.CreatorId)
            {
                notificationManager.AddNotification(
                    metadata.CreatorId,
                    $"Your document {fileName} has been edited",
                    fileName,
                    NotificationType.DocumentEdited
                );
            }
        }

        private void SaveDocumentToFile(Documents.Document document, string filePath)
        {
            File.WriteAllText(filePath, document.Content);
        }

        public void DeleteDocument(string fileName)
        {
            bool found = false;
            var supportedExtensions = new[] { ".txt", ".md", ".rtf" };
            string creatorId = null;
            
            foreach (var ext in supportedExtensions)
            {
                string fullPath = Path.Combine(documentsDirectory, fileName + ext);
                string metadataPath = Path.Combine(metadataDirectory, fileName + ".metadata");

                if (File.Exists(fullPath))
                {
                    // Получаем creatorId перед удалением
                    if (File.Exists(metadataPath))
                    {
                        var metadata = JsonSerializer.Deserialize<DocumentMetadata>(File.ReadAllText(metadataPath));
                        creatorId = metadata.CreatorId;
                        File.Delete(metadataPath);
                    }

                    File.Delete(fullPath);
                    found = true;

                    // Отправляем уведомление об удалении документа
                    if (creatorId != null)
                    {
                        notificationManager.AddNotification(
                            creatorId,
                            $"Document {fileName} has been deleted",
                            fileName,
                            NotificationType.DocumentDeleted
                        );
                    }

                    Console.WriteLine($"Document {fileName}{ext} and its metadata deleted");
                    break;
                }
            }

            if (!found)
            {
                Console.WriteLine($"Document {fileName} not found");
            }
        }

        public List<(string Name, string Extension, string CreatorId)> GetAllDocuments()
        {
            var documents = new List<(string Name, string Extension, string CreatorId)>();
            if (Directory.Exists(documentsDirectory))
            {
                foreach (var file in Directory.GetFiles(documentsDirectory))
                {
                    string name = Path.GetFileNameWithoutExtension(file);
                    string ext = Path.GetExtension(file);
                    string metadataPath = Path.Combine(metadataDirectory, name + ".metadata");

                    if (File.Exists(metadataPath))
                    {
                        var metadata = JsonSerializer.Deserialize<DocumentMetadata>(File.ReadAllText(metadataPath));
                        documents.Add((name, ext, metadata.CreatorId));
                    }
                }
            }
            return documents;
        }

        public string GetDocumentContent(string fileName)
        {
            var supportedExtensions = new[] { ".txt", ".md", ".rtf" };
            foreach (var ext in supportedExtensions)
            {
                string fullPath = Path.Combine(documentsDirectory, fileName + ext);
                if (File.Exists(fullPath))
                {
                    return File.ReadAllText(fullPath);
                }
            }
            throw new FileNotFoundException($"Document {fileName} not found");
        }

        public List<(string Name, string Extension, string CreatorId)> GetUserDocuments(string userId)
        {
            return GetAllDocuments().Where(doc => doc.CreatorId == userId).ToList();
        }
    }
}
