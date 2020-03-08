using System;
using System.IO;

namespace JasonStorey
{
    public class FileWatcher
    {
        private readonly string _filePath;

        private string _fileName;

        private readonly FileSystemWatcher _watcher;

        public FileWatcher(string filePath, string extension = "*.json")
        {
            _fileName = Path.GetFileName(filePath);
            _filePath = filePath;
            var directory = Path.GetDirectoryName(filePath);
            _watcher = new FileSystemWatcher(directory, extension) {EnableRaisingEvents = true};
            _watcher.Changed += WatcherOnChanged;
            _watcher.Created += WatcherOnCreated;
        }

        public event EventHandler<string> Changed;

        private void WatcherOnCreated(object sender, FileSystemEventArgs e)
        {
            if (e.FullPath == _filePath) OnChanged(_filePath);
        }

        private void WatcherOnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.FullPath == _filePath) OnChanged(_filePath);
        }

        protected virtual void OnChanged(string e)
        {
            Changed?.Invoke(this, e);
        }
    }
}