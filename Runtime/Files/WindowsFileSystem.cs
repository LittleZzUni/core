using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace JasonStorey
{
    public class WindowsFileSystem : FileSystem
    {
        public string GetText(string filepath)
        {
            if (string.IsNullOrWhiteSpace(filepath)) throw new ArgumentNullException(nameof(filepath));
            if (DirectoryExists(filepath)) throw new NotAFile(filepath);
            try
            {
                return File.ReadAllText(filepath);
            }
            catch (UnauthorizedAccessException ua)
            {
                throw new UnauthorizedAccess(filepath, ua);
            }
            catch (FileNotFoundException nf)
            {
                throw new FileNotFound(filepath, nf);
            }
            catch (Exception ex)
            {
                throw new CannotReadFile(filepath, ex);
            }
        }

        public byte[] GetBytes(string filepath)
        {
            if (string.IsNullOrWhiteSpace(filepath)) throw new ArgumentNullException(nameof(filepath));
            try
            {
                return File.ReadAllBytes(filepath);
            }
            catch (UnauthorizedAccessException ua)
            {
                throw new UnauthorizedAccess(filepath, ua);
            }
            catch (FileNotFoundException nf)
            {
                throw new FileNotFound(filepath, nf);
            }
            catch (Exception ex)
            {
                throw new CannotReadFile(filepath, ex);
            }
        }

        public bool FileExists(string path)
        {
            try
            {
                return File.Exists(path);
            }
            catch (UnauthorizedAccessException)
            {
                return true;
            }
        }

        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public bool IsValid(string filePath)
        {
            try
            {
                var fi = new FileInfo(filePath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool IsDirectoryAccessible(string directory)
        {
            if (string.IsNullOrWhiteSpace(directory)) return false;
            if (FileExists(directory)) return false;
            try
            {
                var di = new DirectoryInfo(directory);
                var files = GetFilesIn(directory);
                return DirectoryExists(directory);
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<string> GetFilesIn(string directory)
        {
            if (string.IsNullOrWhiteSpace(directory)) throw new ArgumentNullException(nameof(directory));
            if (FileExists(directory)) throw new NotADirectory(directory);
            if (!DirectoryExists(directory)) throw new DirectoryNotFound(directory);

            try
            {
                return Directory.GetFiles(directory);
            }
            catch (UnauthorizedAccessException ua)
            {
                throw new UnauthorizedAccess(directory, ua);
            }
            catch (Exception ex)
            {
                throw new Unknown(directory, ex);
            }
        }

        public IEnumerable<string> GetSubDirectoriesIn(string directory)
        {
            if (string.IsNullOrWhiteSpace(directory)) throw new ArgumentNullException(nameof(directory));
            if (FileExists(directory)) throw new NotADirectory(directory);
            if (!DirectoryExists(directory)) throw new DirectoryNotFound(directory);


            try
            {
                return Directory.GetDirectories(directory);
            }
            catch (UnauthorizedAccessException ua)
            {
                throw new UnauthorizedAccess(directory, ua);
            }
            catch (Exception ex)
            {
                throw new Unknown(directory, ex);
            }
        }

        public void Create(string filepath)
        {
            if (string.IsNullOrWhiteSpace(filepath)) throw new ArgumentNullException(nameof(filepath));
            if (FileExists(filepath)) throw new FileAlreadyExists(filepath);
            if (DirectoryExists(filepath)) throw new NotAFile(filepath);
            try
            {
                File.Create(filepath);
            }
            catch (UnauthorizedAccessException ua)
            {
                throw new UnauthorizedAccess(filepath, ua);
            }
            catch (Exception ex)
            {
                throw new CannotCreateFile(filepath, ex);
            }
        }

        public void Create(string filepath, string content)
        {
            if (string.IsNullOrWhiteSpace(filepath)) throw new ArgumentNullException(nameof(filepath));
            if (FileExists(filepath)) throw new FileAlreadyExists(filepath);
            try
            {
                File.WriteAllText(filepath, content);
            }
            catch (UnauthorizedAccessException ua)
            {
                throw new UnauthorizedAccess(filepath, ua);
            }
            catch (Exception ex)
            {
                throw new CannotCreateFile(filepath, ex);
            }
        }

        public void Create(string filepath, byte[] content)
        {
            if (string.IsNullOrWhiteSpace(filepath)) throw new ArgumentNullException(nameof(filepath));
            if (FileExists(filepath)) throw new FileAlreadyExists(filepath);
            try
            {
                File.WriteAllBytes(filepath, content);
            }
            catch (UnauthorizedAccessException ua)
            {
                throw new UnauthorizedAccess(filepath, ua);
            }
            catch (Exception ex)
            {
                throw new CannotCreateFile(filepath, ex);
            }
        }

        public void Replace(string filePath, string content)
        {
            if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentNullException(nameof(filePath));
            if (!FileExists(filePath)) throw new FileDoesNotExist(filePath);
            try
            {
                File.WriteAllText(filePath, content);
            }
            catch (UnauthorizedAccessException ua)
            {
                throw new UnauthorizedAccess(filePath, ua);
            }
            catch (Exception ex)
            {
                throw new CannotCreateFile(filePath, ex);
            }
        }

        public void Replace(string filePath, byte[] content)
        {
            if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentNullException(nameof(filePath));
            if (!FileExists(filePath)) throw new FileDoesNotExist(filePath);
            try
            {
                File.WriteAllBytes(filePath, content);
            }
            catch (UnauthorizedAccessException ua)
            {
                throw new UnauthorizedAccess(filePath, ua);
            }
            catch (Exception ex)
            {
                throw new CannotCreateFile(filePath, ex);
            }
        }

        public FileStream ReadStream(string path)
        {
            return new FileStream(path, FileMode.Open, FileAccess.Read);
        }

        public FileStream WriteStream(string path)
        {
            return new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
        }

        public void OpenFolder(string directory)
        {
            Process.Start(directory);
        }

        public void LaunchFile(string filePath)
        {
            Process.Start(filePath);
        }

        public void LaunchFile(string filePath, string arguments)
        {
            Process.Start(filePath, arguments);
        }

        public void Move(string fromPath, string toPath)
        {
            if (string.IsNullOrWhiteSpace(toPath)) throw new ArgumentNullException(nameof(toPath));
            if (string.IsNullOrWhiteSpace(fromPath)) throw new ArgumentNullException(nameof(fromPath));
            try
            {
                File.Move(fromPath, toPath);
            }
            catch (UnauthorizedAccessException ua)
            {
                throw new UnauthorizedAccess(toPath, ua);
            }
            catch (Exception ex)
            {
                throw new CannotMoveFile(fromPath, toPath, ex);
            }
        }

        public void ChangeExtension(string path, string newExtension)
        {
            var dir = Path.GetDirectoryName(path);
            var name = Path.GetFileNameWithoutExtension(path);

            Move(path, dir + @"\" + name + CleanExtension(newExtension));
        }

        public void RenameFile(string path, string newName)
        {
            var dir = Path.GetDirectoryName(path);
            var ext = Path.GetExtension(path);
            var newPath = CreateFilePath(dir, newName, ext);
            Move(path, newPath);
        }

        public DateTime? GetLastUpdatedTime(string filePath)
        {
            if (!File.Exists(filePath)) return null;

            var modification = File.GetLastWriteTime(filePath);
            return modification;
        }

        public void MoveTo(string path, string directory)
        {
            var filename = Path.GetFileName(path);
            var newPath = CreateFilePath(directory, filename);
            Move(path, newPath);
        }

        public bool Delete(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    return false;
                File.Delete(filePath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CanAccess(string file)
        {
            try
            {
                using (var stream = File.OpenRead(file))
                {
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public string CurrentDirectory => Environment.CurrentDirectory;
        public string HomeDirectory => Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
        public string TempDirectory => Path.GetTempPath();

        public string CreateFilePath(string path, string file, string extension)
        {
            file = RemoveExtension(file);
            return CreateFilePath(path, file + CleanExtension(extension));
        }

        public string GetDirectoryName(string path)
        {
            return Path.GetFileName(path);
        }

        public string CreateFilePath(string path, string file)
        {
            if (!path.EndsWith(@"\")) path += @"\";
            return Path.Combine(path, file);
        }

        public string CreateDirectoryPath(string path, string folder)
        {
            return Path.Combine(path, folder);
        }

        public void RenameDirectory(string dirPath, string newName)
        {
            var parent = Directory.GetParent(dirPath).FullName;
            var newPath = CreateDirectoryPath(parent, newName);
            Directory.Move(dirPath, newPath);
        }

        public void ChangeExtension(string directory, string file, string newExtension)
        {
            if (file.Contains("."))
            {
                ChangeExtension(CreateFilePath(directory, file), newExtension);
            }
            else
            {
                var oldPath = CreateFilePath(directory, file, FindExtension(directory, file));
                var newPath = CreateFilePath(directory, file, newExtension);
                Move(oldPath, newPath);
            }
        }

        private string FindExtension(string directory, string fileName)
        {
            if (fileName.Contains(".")) return fileName.Split('.')[1];
            var filesMatchingName = GetFilesIn(directory).Where(x => Path.GetFileName(x).Contains(fileName)).ToList();
            if (filesMatchingName.Count == 1) return Path.GetExtension(filesMatchingName[0]);

            return string.Empty;
        }

        private string CleanExtension(string extension)
        {
            if (extension.StartsWith("*")) extension = extension.Substring(1, extension.Length - 1);
            if (!extension.StartsWith(".")) extension = "." + extension;
            return extension.ToLower();
        }

        public void RenameFile(string directory, string fileName, string newName)
        {
            var ext = FindExtension(directory, fileName);
            var newPath = CreateFilePath(directory, newName, ext);
            var oldPath = CreateFilePath(directory, fileName, ext);
            Move(oldPath, newPath);
        }

        private string RemoveExtension(string file)
        {
            if (!file.Contains(".")) return file;
            return file.Split('.')[0];
        }
    }
}