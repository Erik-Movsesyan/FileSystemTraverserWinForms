using System.Collections.Generic;
using System.IO;

namespace FileSystemTraverser
{
    public class FileSystemVisitor
    {
        private FileSystemEntry[] _fileSystemEntries = [];
        private List<FileSystemEntry> collectionOfEntries = [];

        public void TraverseFileSystemEntries(string folderPath)
        {
            var dirInfo = new DirectoryInfo(folderPath);

            var entries = dirInfo.EnumerateFileSystemInfos();
                
            foreach (var item in entries)
            {
                if (!File.Exists(item.FullName))
                {
                    TraverseFileSystemEntries(item.FullName);
                }

                collectionOfEntries.Add(item.ToFileSystemEntry());
            }
        }
    }

    public class FileSystemEntry(string name, bool isFile, string path)
    {
        public string Name { get; } = name;
        public bool IsFile { get; } = isFile;
        public string Path { get; } = path;
    }

    //public class People : IEnumerable
    //{
    //    private Person[] _people;
    //    public People(Person[] pArray)
    //    {
    //        _people = new Person[pArray.Length];

    //        for (int i = 0; i < pArray.Length; i++)
    //        {
    //            _people[i] = pArray[i];
    //        }
    //    }

    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        return (IEnumerator)GetEnumerator();
    //    }

    //    public PeopleEnum GetEnumerator()
    //    {
    //        return new PeopleEnum(_people);
    //    }
    //}

    //public class PeopleEnum : IEnumerator<Person>
    //{
    //    public Person[] _people;

    //    // Enumerators are positioned before the first element
    //    // until the first MoveNext() call.
    //    int position = -1;

    //    public PeopleEnum(Person[] list)
    //    {
    //        _people = list;
    //    }

    //    public bool MoveNext()
    //    {
    //        position++;
    //        return (position < _people.Length);
    //    }

    //    public void Reset()
    //    {
    //        position = -1;
    //    }

    //    object IEnumerator.Current
    //    {
    //        get
    //        {
    //            return Current;
    //        }
    //    }

    //    public Person Current
    //    {
    //        get
    //        {
    //            try
    //            {
    //                return _people[position];
    //            }
    //            catch (IndexOutOfRangeException)
    //            {
    //                throw new InvalidOperationException();
    //            }
    //        }
    //    }

    //    public void Dispose() { }
    //}

}
