using System.Diagnostics;
using System.Text;
using System.IO;
using TinySharpZip;

namespace SolutionCreator.Commands
{
    public class Zipper
    {
        public void Extract(string file, string extractTo)
        {
            string argument = @"x " + file + " -o"+extractTo;
            Process.Start(@"C:\Program Files\7-Zip\7z.exe", argument);
            //Stream zipStream = new FileStream(file, FileMode.Open);
            //ZipArchive.Extract(zipStream, extractTo);
            //zipStream.Close();
        }

        public void Zip(string directory, string fileName)
        {
            Stream outputZipStream = new FileStream(fileName, FileMode.Create);
            ZipArchive.CreateFromDirectory(directory, outputZipStream, false);
            outputZipStream.Close();
        }
    }
}