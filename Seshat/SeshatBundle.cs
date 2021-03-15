using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Seshat
{
    /// <summary>
    /// A mod bundle, typically representing the entirety of the mod, including
    /// assemblies and mod content.
    /// </summary>
    public class SeshatBundle
    {
        private string _base;

        public SeshatBundle(string basePath)
        {
            this._base = basePath;
        }

        // Filesystem operation abstractions just in case we need to implement
        // .zip file support later on.
        public bool FileExists(string path)
            => File.Exists(Absolute(path));

        public Stream GetFile(string path)
        {
            return File.OpenRead(Absolute(path));
        }

        public string[] GetFiles(string path)
        {
            return new DirectoryInfo(Absolute(path)).GetFiles()
                .Select(file => file.Name)
                .ToArray();
        }
        
        public string[] GetDirectories(string path)
        {
            return new DirectoryInfo(Absolute(path)).GetDirectories()
                .Select(dir => dir.Name)
                .ToArray();
        }

        /// <summary>
        /// Crawls a directory in the bundle. <c>callback</c> is called with the
        /// relative path of the file from the <c>basePath</c>.
        /// </summary>
        public void Crawl(string root, Action<string> callback)
            => CrawlInternal(root, string.Empty, callback);

        private void CrawlInternal(string root, string path, Action<string> callback)
        {
            string fullPath = Path.Combine(root, path);

            foreach (string file in GetFiles(fullPath))
                callback(file);

            foreach (string dir in GetDirectories(fullPath))
                CrawlInternal(root, Path.Combine(path, dir), callback);
        }

        private string Absolute(string path)
            => Path.Combine(_base, path);
    }
}
