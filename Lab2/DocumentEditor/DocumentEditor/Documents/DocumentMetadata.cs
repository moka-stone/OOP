using System;
using System.Text.Json.Serialization;

namespace DocumentEditor.Documents
{
    public class DocumentMetadata
    {
        public string CreatorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModified { get; set; }
        public string DocumentType { get; set; }

        public DocumentMetadata(string creatorId, string documentType)
        {
            CreatorId = creatorId;
            DocumentType = documentType;
            CreatedAt = DateTime.Now;
            LastModified = DateTime.Now;
        }

        [JsonConstructor]
        public DocumentMetadata()
        {
        }
    }
} 