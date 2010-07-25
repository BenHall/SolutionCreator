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
            string[] filesToDelete = new string[] {"*.user", "*.sln", "*.suo"};
            string[] directoriesToDelete = new string[] { "_Resharper*", ".git" };
            foreach (var pattern in filesToDelete)
            {
                foreach (var fileInfo in GetFiles(_solutionDirectory, pattern))
                    fileInfo.Delete();
            }

            foreach (var pattern in directoriesToDelete)
            {
                foreach (var directoryInfo in GetDirectories(_solutionDirectory, pattern))
                    directoryInfo.Delete(true);
            }
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