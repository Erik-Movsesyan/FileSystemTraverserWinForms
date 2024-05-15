namespace FileSystemTraverser
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var test = new FileSystemVisitor();
            test.TraverseFileSystemEntries(@"C:\Users\Erik_Movsesyan\OneDrive - EPAM\.NET Training\Homework\Advanced-C#-Section\FileSystemTraverser\obj\Debug");

            var tt = 6;
        }
    }
}
