using System.Collections.Generic;
using System.IO;

namespace SolutionCreator.Commands
{
    public class DirectoryCleaner
    {
        private readonly string _solutionDirectory;

        public DirectoryCleaner(string solutionDirectory)
        {
            _solutionDirectory = solutionDirectory;
        }

        public void Clean()
        {
            foreach (var fileInfo in GetFiles(_solutionDirectory, "*.user"))
                fileInfo.Delete();

            foreach (var fileInfo in GetFiles(_solutionDirectory, "*.suo"))
                fileInfo.Delete();

            foreach (var fileInfo in GetFiles(_solutionDirectory, "*.sln"))
                fileInfo.Delete();

            foreach (var directoryInfo in GetDirectories(_solutionDirectory, "_Resharper*"))
                directoryInfo.Delete(true);

            foreach (var directoryInfo in GetDirectories(_solutionDirectory, ".git"))
                directoryInfo.Delete(true);
        }

        private IEnumerable<DirectoryInfo> GetDirectories(string directory, string searchPattern)
        {
            return new DirectoryInfo(directory).GetDirectories(searchPattern);
        }

        private IEnumerable<FileInfo> GetFiles(string directory, string searchPattern)
        {
            return new DirectoryInfo(directory).GetFiles(searchPattern, SearchOption.AllDirectories);
        }
    }
}