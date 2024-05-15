using System;
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

            var entries = dirInfo.GetFileSystemInfos();
            var tempArray = new FileSystemEntry[entries.Length];

            for (int i = 0; i < entries.Length; i++)
            {
                var entry = entries[i];
                var isFile = File.Exists(entry.FullName);
                if (!isFile)
                {
                    TraverseFileSystemEntries(entry.FullName);
                }

                tempArray[i] = entry.ToFileSystemEntry(isFile);
            }

            var tempArrayExistingFileSystemEntries = _fileSystemEntries;
            _fileSystemEntries = new FileSystemEntry[tempArray.Length + tempArrayExistingFileSystemEntries.Length];

            Array.Copy(tempArrayExistingFileSystemEntries, _fileSystemEntries, tempArrayExistingFileSystemEntries.Length);
            Array.Copy(tempArray, 0, _fileSystemEntries, tempArrayExistingFileSystemEntries.Length, tempArray.Length);
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
