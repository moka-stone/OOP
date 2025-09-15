namespace DocumentEditor.Storage
{
    public interface IStorageImplementor
    {
        void Save(string path, string content);
        string Load(string path);
        void Delete(string path);
        bool Exists(string path);
    }
} 