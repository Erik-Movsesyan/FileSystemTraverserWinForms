using System;
using System.Collections;
using System.Collections.Generic;
using FileSystemTraverser.Entities;

namespace FileSystemTraverser.Enumerators
{
    public class FieSystemEntryEnumerator : IEnumerator<FileSystemEntry>
    {
        private readonly FileSystemEntry?[] _collection;
        private int _currentIndex;
        private FileSystemEntry? _currentEntry;

        public FieSystemEntryEnumerator(FileSystemEntry?[] collection)
        {
            _collection = collection;
            _currentIndex = -1;
            _currentEntry = default;
        }

        public bool MoveNext()
        {
            if (++_currentIndex >= _collection.Length)
            {
                return false;
            }

            _currentEntry = _collection[_currentIndex];
            return true;
        }

        public void Reset() { _currentIndex = -1; }

        public FileSystemEntry? Current => _currentEntry;

        object? IEnumerator.Current => Current;

        void IDisposable.Dispose() { }
    }
}
