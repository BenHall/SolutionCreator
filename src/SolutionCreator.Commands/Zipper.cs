using ICSharpCode.SharpZipLib.Zip;

namespace SolutionCreator.Commands
{
    public class Zipper
    {
        public void Extract(string file, string extractTo)
        {
            FastZip zip = new FastZip();
            zip.ExtractZip(file, extractTo, FastZip.Overwrite.Always, name => true, "", "", false);
        }

        public void Zip(string directory, string fileName)
        {
            FastZip zip = new FastZip();
            zip.CreateZip(fileName, directory, true, "");
        }
    }
}