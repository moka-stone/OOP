using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using DocumentEditor.Documents.Observer;

namespace DocumentEditor.Documents
{
    public abstract class Document : IDocumentSubject
    {
        private string content;
        private readonly List<IDocumentObserver> observers = new List<IDocumentObserver>();

        public string Content
        {
            get => content;
            set
            {
                content = value;
                LastModified = DateTime.Now;
                Notify($"Document content updated");
            }
        }

        public string CreatorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModified { get; set; }

        protected Document(string creatorId)
        {
            CreatorId = creatorId;
            CreatedAt = DateTime.Now;
            LastModified = DateTime.Now;
            observers = new List<IDocumentObserver>();
        }

        public void Attach(IDocumentObserver observer)
        {
            observers.Add(observer);
        }

        public void Detach(IDocumentObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify(string message)
        {
            foreach (var observer in observers)
            {
                observer.Update(message);
            }
        }

        public abstract string GetFileExtension();
    }
}
