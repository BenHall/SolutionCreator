using System;
using System.IO;
using SolutionCreator.Commands;

namespace SolutionCreator.Export
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SolutionCreator exporter");
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

            Console.WriteLine("Created...");
            Console.ReadLine();
        }

        private static void Process(string from, string to, string zipName)
        {
            DirectoryCopier copier = new DirectoryCopier();
            copier.Copy(new DirectoryInfo(from), new DirectoryInfo(to));

            var directoryCleaner = new DirectoryCleaner(to);
            directoryCleaner.Clean();

            string solutionName = new DirectoryInfo(from).GetFiles("*.sln")[0].Name.Replace(".sln", "");
            ProjectExtracter extracter = new ProjectExtracter(to, solutionName);
            extracter.ReplaceValuesWithPlaceholders();
            extracter.UpdateAssemblyInfo();
            extracter.UpdateGlobalAsax();
            extracter.UpdateProjectFile();

            Zipper template = new Zipper();

            template.Zip(to, Path.Combine(to, zipName));
        }

        private static void PrintUsage()
        {
            Console.WriteLine("Invalid arguments.");
            Console.WriteLine("Usage:\tSolutionCreator.Export.exe <TemplateDirectoryToExport> <PathToZipFile> <NameOfZip>");
        }
    }
}
