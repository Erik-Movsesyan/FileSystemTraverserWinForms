using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FileSystemTraverser.Core;
using FileSystemTraverser.Entities;
using FileSystemTraverser.Extensions;

namespace FileSystemTraverser.Tests.FileSystemVisitorTests
{
    public partial class FileSystemVisitorTests
    {
        private readonly string _testDataFolderRelativeToCurrentDomainFolder = $@"{AppDomain.CurrentDomain.BaseDirectory}..\..\..\TestData";
        private FileSystemVisitor _fileSystemVisitor;

        [SetUp]
        public void TestSetUp()
        {
            _fileSystemVisitor = new FileSystemVisitor();
        }

        [Test]
        public async Task Test_FieSystemEntries_UnderlyingField_Populated_Correctly_Files_Only()
        {
            //Arrange
            var testDataFolderFilesOnlyRelativePath = Path.Combine(_testDataFolderRelativeToCurrentDomainFolder, "TestDataFolderFilesOnly");
            var dirInfo = new DirectoryInfo(testDataFolderFilesOnlyRelativePath);

            var expectedFileSystemEntries = dirInfo.GetFileSystemInfos("*", SearchOption.AllDirectories)
                .Select(fileSystemInfo => fileSystemInfo.ToFileSystemEntry(File.Exists(fileSystemInfo.FullName)));

            //Act
            await _fileSystemVisitor.StartFileSystemSearch(testDataFolderFilesOnlyRelativePath, null, CancellationToken.None);
            var actualFileSystemEntries = _fileSystemVisitor.ToList();

            //Assert
            Assert.That(actualFileSystemEntries.SequenceEqual(expectedFileSystemEntries), Is.True);
        }

        [Test]
        public async Task Test_FieSystemEntries_UnderlyingField_Populated_Correctly_Files_And_Folders()
        {
            //Arrange
            var testDataFolderFilesAndDirectoriesRelativePath = Path.Combine(_testDataFolderRelativeToCurrentDomainFolder, "TestDataFolderFilesAndDirectories");
            var testDataFolderFilesAndDirectoriesPath = new DirectoryInfo(testDataFolderFilesAndDirectoriesRelativePath).FullName;
            var expectedFileSystemEntries = new List<FileSystemEntry>
            {
                new("Dir", false, Path.Combine(testDataFolderFilesAndDirectoriesPath, @"Dir")),
                new("TestFile2.txt", true, Path.Combine(testDataFolderFilesAndDirectoriesPath, @"TestFile2.txt"))
            };

            //Act
            await _fileSystemVisitor.StartFileSystemSearch(testDataFolderFilesAndDirectoriesPath, null, CancellationToken.None);
            var actualFileSystemEntries = _fileSystemVisitor.ToList();

            //Assert
            Assert.That(actualFileSystemEntries.SequenceEqual(expectedFileSystemEntries), Is.True);
        }

        [Test]
        public async Task Test_FieSystemEntries_UnderlyingField_Populated_And_Filtered_Correctly_Files_Only()
        {
            //Arrange
            var testDataFolderFilesOnlyRelativePath = Path.Combine(_testDataFolderRelativeToCurrentDomainFolder, "TestDataFolderFilesOnly");
            var dirInfo = new DirectoryInfo(testDataFolderFilesOnlyRelativePath);
            const string filter = ".txt";

            var expectedFileSystemEntries = dirInfo.GetFileSystemInfos($"*{filter}", SearchOption.AllDirectories)
                .Select(fileSystemInfo => fileSystemInfo.ToFileSystemEntry(File.Exists(fileSystemInfo.FullName)));

            //Act
            await _fileSystemVisitor.StartFilteredFileSystemSearch(testDataFolderFilesOnlyRelativePath, filter, null, CancellationToken.None);
            var actualFileSystemEntries = _fileSystemVisitor.ToList();

            //Assert
            Assert.That(actualFileSystemEntries.SequenceEqual(expectedFileSystemEntries), Is.True);
        }

        [Test]
        public async Task Test_FieSystemEntries_UnderlyingField_Populated_And_Filtered_Correctly_Files_And_Folders()
        {
            //Arrange
            var testDataFolderFilesAndDirectoriesRelativePath = Path.Combine(_testDataFolderRelativeToCurrentDomainFolder, "TestDataFolderFilesAndDirectories");
            var testDataFolderFilesAndDirectoriesPath = new DirectoryInfo(testDataFolderFilesAndDirectoriesRelativePath).FullName;
            const string filter = ".txt";

            var expectedFileSystemEntries = new List<FileSystemEntry>
            {
                new("TestFile2.txt", true, Path.Combine(testDataFolderFilesAndDirectoriesPath, @"TestFile2.txt"))
            };

            //Act
            await _fileSystemVisitor.StartFilteredFileSystemSearch(testDataFolderFilesAndDirectoriesPath, filter, null, CancellationToken.None);
            var actualFileSystemEntries = _fileSystemVisitor.ToList();

            //Assert
            Assert.That(actualFileSystemEntries.SequenceEqual(expectedFileSystemEntries), Is.True);
        }

        [Test, TestCaseSource(nameof(YieldEventEventRelatedTestData))]
        public async Task Template_For_YieldEventEventRelatedTestData_TestCaseSource(EventHandler eventHandler, string eventName)
        {
            //Arrange
            var testDataFolderFilesOnlyRelativePath = Path.Combine(_testDataFolderRelativeToCurrentDomainFolder, "TestDataFolderFilesOnly");
            _fileSystemVisitor.FileSystemSearchStarted += eventHandler;

            //Act
            await _fileSystemVisitor.StartFileSystemSearch(testDataFolderFilesOnlyRelativePath, null, CancellationToken.None);

            //Assert
            Assert.Fail($"The '{eventName}' event did not fire since the subscribed handler: " +
                        $"'{eventHandler.Method.Name}' was not invoked");
        }

        private static IEnumerable<TestCaseData> YieldEventEventRelatedTestData()
        {
            yield return new TestCaseData((EventHandler)HandleFileSystemSearchStarted, nameof(_fileSystemVisitor.FileSystemSearchStarted))
                .SetName("Test_FileSystemSearchStared_Event_Fired");
            yield return new TestCaseData((EventHandler)HandleFileSystemSearchCompleted, nameof(_fileSystemVisitor.FileSystemSearchCompleted))
                .SetName("Test_FileSystemSearchCompleted_Event_Fired");
            yield return new TestCaseData((EventHandler)HandleFileFound, nameof(_fileSystemVisitor.FileFound))
                .SetName("Test_FileFound_Event_Fired");
            yield return new TestCaseData((EventHandler)HandleDirectoryFound, nameof(_fileSystemVisitor.DirectoryFound))
                .SetName("Test_DirectoryFound_Event_Fired");
            yield return new TestCaseData((EventHandler)HandleFilteredFileFound, nameof(_fileSystemVisitor.FilteredFileFound))
                .SetName("Test_FilteredFileFound_Event_Fired");
            yield return new TestCaseData((EventHandler)HandleFilteredDirectoryFound, nameof(_fileSystemVisitor.FilteredDirectoryFound))
                .SetName("Test_FilteredDirectoryFound_Event_Fired");
        }
    }
}