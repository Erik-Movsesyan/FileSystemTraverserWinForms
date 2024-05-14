namespace FileSystemTraverser
{
    internal class Program
    {
        static void Main(string[] args)
        {
            new FileSystemVisitor().TraverseFileSystemEntries(@"C:\Users\Erik_Movsesyan\OneDrive - EPAM\.NET Training");
            var tt = 5;
        }
    }
}
