using System;
using System.IO;
using SolutionCreator.Commands;

namespace SolutionCreator.Export
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Solution Exporter - Create a new re-usable template");
            if (args.Length != 3)
            {
                PrintUsage();
                return;
            }

            Console.WriteLine("Point it at a directory and have a template ready to use...");

            string from = args[0];
            string to = args[1];
            string zipName = args[2];

            Process(from, to, zipName);

            Console.WriteLine("Created {0} at {1} based on {2}", zipName, to, from);
        }

        private static void Process(string from, string to, string zipName)
        {
            DirectoryCopier copier = new DirectoryCopier();
            var solutionDirectory = Path.Combine(to, "temp");
            copier.Copy(new DirectoryInfo(from), new DirectoryInfo(solutionDirectory));

            var directoryCleaner = new DirectoryCleaner(solutionDirectory);
            directoryCleaner.Clean();

            string solutionName = new DirectoryInfo(from).GetFiles("*.sln", SearchOption.AllDirectories)[0].Name.Replace(".sln", "");
            ProjectExtracter extracter = new ProjectExtracter(solutionDirectory, solutionName);
            extracter.ReplaceValuesWithPlaceholders();
            extracter.UpdateAssemblyInfo();
            extracter.UpdateGlobalAsax();
            extracter.UpdateProjectFile();

            Zipper template = new Zipper();

            template.Zip(solutionDirectory, Path.Combine(to, zipName));
        }

        private static void PrintUsage()
        {
            Console.WriteLine("Invalid arguments.");
            Console.WriteLine("Usage:\tSolutionCreator.Export.exe <TemplateDirectoryToExport> <PathToZipFile> <NameOfZip>");
        }
    }
}
