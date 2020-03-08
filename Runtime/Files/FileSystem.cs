using System;
using System.Collections.Generic;
using System.IO;

namespace JasonStorey
{
    public interface FileSystem
    {
        string CurrentDirectory { get; }
        string HomeDirectory { get; }
        string TempDirectory { get; }
        string GetText(string filePath);
        byte[] GetBytes(string filePath);
        bool FileExists(string filePath);
        bool DirectoryExists(string filePath);
        bool CanAccess(string filePath);
        bool Delete(string filePath);
        bool IsValid(string filePath);
        bool IsDirectoryAccessible(string directory);
        IEnumerable<string> GetFilesIn(string directory);
        IEnumerable<string> GetSubDirectoriesIn(string directory);
        DateTime? GetLastUpdatedTime(string filePath);
        void Create(string filePath);
        void Create(string filePath, string content);
        void Create(string filePath, byte[] content);

        void Replace(string filePath, string content);
        void Replace(string filePath, byte[] content);

        FileStream ReadStream(string path);
        FileStream WriteStream(string path);

        void OpenFolder(string directory);
        void LaunchFile(string filePath);
        void LaunchFile(string filePath, string arguments);
        void Move(string fromPath, string toPath);

        void ChangeExtension(string path, string newExtension);
        void RenameFile(string path, string newName);
        void MoveTo(string path, string directory);
        void RenameDirectory(string dirPath, string newName);
        string CreateDirectoryPath(string path, string folder);
        string CreateFilePath(string path, string file);
        string CreateFilePath(string path, string file, string extension);

        string GetDirectoryName(string path);
    }
}