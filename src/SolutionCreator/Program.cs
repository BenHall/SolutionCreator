using System;
using SolutionCreator.Commands;

namespace SolutionCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SolutionCreator");
            if (args.Length != 3)
            {
                PrintUsage();
                return;
            }

            var templateToExtract = args[0];
            var extractTo = args[1];
            var nameOfProject = args[2];

            Zipper template = new Zipper();

            template.Extract(templateToExtract, extractTo);

            ProjectCreator creator = new ProjectCreator(nameOfProject, extractTo);
            var projectFiles = creator.UpdateProjectFile();
            creator.CreateSolutionFile(projectFiles);
            creator.UpdateGlobalAsax();
            creator.UpdateAssemblyInfo();
            creator.UpdateFilesWithName();

            Console.WriteLine("Created...");
            Console.ReadLine();
        }

        private static void PrintUsage()
        {
            Console.WriteLine("Invalid arguments.");
            Console.WriteLine("Usage:\tSolutionCreator.exe <TemplateToUse> <LocationOfSolution> <NameOfProject>");
        }
    }
}
