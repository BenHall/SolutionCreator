using System;
using System.IO;
using SolutionCreator.Commands;

namespace SolutionCreator.Export
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Point it at a directory and have a template ready to use...");

            string solutionName = "NewProject";
            string from = @"C:\temp\solutioncreator\templateextract\";
            string to = @"C:\temp\solutioncreator\temptozip\";

            DirectoryCopier copier = new DirectoryCopier();
            copier.Copy(new DirectoryInfo(from), new DirectoryInfo(to));

            var directoryCleaner = new DirectoryCleaner(to);
            directoryCleaner.Clean();

            ProjectExtracter extracter = new ProjectExtracter(to, solutionName);
            extracter.ReplaceValuesWithPlaceholders();
            
            Console.WriteLine("Created...");
            Console.ReadLine();
        }
    }
}
