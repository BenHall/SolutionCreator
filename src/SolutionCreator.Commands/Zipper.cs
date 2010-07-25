using Ionic.Zip;
using System.IO;

namespace SolutionCreator.Commands
{
    public class Zipper
    {
        public void Extract(string file, string extractTo)
        {
            using (var zip = ZipFile.Read(file))
            {
               zip.ExtractAll(extractTo,ExtractExistingFileAction.OverwriteSilently);
            }
        }

        public void Zip(string directory, string fileName)
        {
            ZipFile file = new ZipFile(Path.GetFileName(fileName));
            file.AddDirectory(directory);
            file.Save(fileName);
        }
    }
}
