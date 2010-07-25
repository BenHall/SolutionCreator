using System;
using SolutionCreator.Commands;

namespace SolutionCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting creator...");
            Console.Write("Template: ");
            var templateToExtract = Console.ReadLine();
            Console.Write("Name of project: ");
            var nameOfProject = Console.ReadLine();

            ExtractTemplate template = new ExtractTemplate();
            var extractTo = @"C:\temp\solutioncreator\templateextract\";

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
    }
}
