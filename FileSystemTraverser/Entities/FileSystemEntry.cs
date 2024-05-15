namespace FileSystemTraverser.Entities
{
    public class FileSystemEntry(string name, bool isFile, string path)
    {
        public string Name { get; } = name;
        public bool IsFile { get; } = isFile;
        public string Path { get; } = path;
    }
}