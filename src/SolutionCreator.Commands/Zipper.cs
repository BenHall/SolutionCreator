//using System.Text;
//using Ionic.Zip;
//using System.IO;

//namespace SolutionCreator.Commands
//{
//    public class Zipper
//    {
//        public void Extract(string file, string extractTo)
//        {
//            using (var zip = ZipFile.Read(file))
//            {
//               zip.ExtractAll(extractTo,ExtractExistingFileAction.OverwriteSilently);
//            }
//        }

//        public void Zip(string directory, string fileName)
//        {
//            ZipFile file = new ZipFile(Path.GetFileName(fileName));
//            file.AddDirectory(directory);
//            file.Save(fileName);
//        }
//    }
//}
using System.Text;
using System.IO;
using TinySharpZip;

namespace SolutionCreator.Commands
{
    public class Zipper
    {
        public void Extract(string file, string extractTo)
        {
            Stream zipStream = new FileStream(file, FileMode.Open);
            ZipArchive.Extract(zipStream, extractTo);
            zipStream.Close();
        }

        public void Zip(string directory, string fileName)
        {
            Stream outputZipStream = new FileStream(fileName, FileMode.Create);
            ZipArchive.CreateFromDirectory(directory, outputZipStream, false);
            outputZipStream.Close();
        }
    }
}