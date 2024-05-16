namespace FileSystemTraverser.Entities
{
    public class FileSystemEntry(string name, bool isFile, string path)
    {
        public string Name { get; } = name;
        public bool IsFile { get; } = isFile;
        public string Path { get; } = path;

        public override bool Equals(object obj)
        {
            return obj is FileSystemEntry entry
                   && Equals(Name, entry.Name) && Equals(IsFile, entry.IsFile) && Equals(Path, entry.Path);
        }

        public static bool operator ==(FileSystemEntry entry1, FileSystemEntry entry2)
        {
            return entry1.Equals(entry2);
        }

        public static bool operator !=(FileSystemEntry entry1, FileSystemEntry entry2)
        {
            return !entry1.Equals(entry2);
        }

        public override int GetHashCode()
        {
            const int hashingBase = 1326136261;
            const int hashingMultiplier = 16777619;

            int hash = hashingBase;
            hash = (hash * hashingMultiplier) ^ (Name != null ? Name.GetHashCode() : 0);
            hash = (hash * hashingMultiplier) ^ (Path != null ? Path.GetHashCode() : 0);
            hash = (hash * hashingMultiplier) ^ IsFile.GetHashCode();
            return hash;
        }
    }
}