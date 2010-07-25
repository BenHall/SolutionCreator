using System.IO;

namespace SolutionCreator.Commands
{
    public abstract class ProjectModifier
    {
        private readonly string _solutionLocation;

        protected ProjectModifier(string solutionLocation)
        {
            _solutionLocation = solutionLocation;
        }
        
        protected string[] FindFiles(string searchPattern)
        {
            return Directory.GetFiles(_solutionLocation, searchPattern, SearchOption.AllDirectories);
        }

        protected void ReplaceContentsOfFile(string file, string searchFor, string replaceWith)
        {
            StreamReader reader = new StreamReader(file);

            string contents = reader.ReadToEnd();

            reader.Close();
            reader.Dispose();

            contents = contents.Replace(searchFor, replaceWith);

            StreamWriter writer = new StreamWriter(file);

            writer.Write(contents);

            writer.Close();
            writer.Dispose();
        }
    }
}