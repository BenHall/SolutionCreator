using Ionic.Zip;
using System.IO;

namespace SolutionCreator.Commands
{
    public class ExtractTemplate
    {
        public void Extract(string file, string extractTo)
        {
            using (var zip = ZipFile.Read(file))
            {
               zip.ExtractAll(extractTo,ExtractExistingFileAction.OverwriteSilently);
            }
        }
    }
}
