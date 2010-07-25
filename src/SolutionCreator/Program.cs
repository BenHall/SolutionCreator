using System;
using SolutionCreator.Commands;

namespace SolutionCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Solution Creator - Create a new project based off a template");
            if (args.Length != 3)
            {
                PrintUsage();
                return;
            }

            var templateToExtract = args[0];
            var solutionLocation = args[1];
            var nameOfProject = args[2];

            Zipper template = new Zipper();

            template.Extract(templateToExtract, solutionLocation);

            ProjectCreator creator = new ProjectCreator(nameOfProject, solutionLocation);
            var projectFiles = creator.UpdateProjectFile();
            creator.CreateSolutionFile(projectFiles);
            creator.UpdateGlobalAsax();
            creator.UpdateAssemblyInfo();
            creator.UpdateFilesWithName();

            Console.WriteLine("Created {0} at {1} based on {2}", nameOfProject, solutionLocation, templateToExtract);
        }

        private static void PrintUsage()
        {
            Console.WriteLine("Invalid arguments.");
            Console.WriteLine("Usage:\tSolutionCreator.exe <TemplateToUse> <LocationOfSolution> <NameOfProject>");
        }
    }
}
