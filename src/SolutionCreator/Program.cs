using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolutionCreator.Commands;

namespace SolutionCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting creator...");
            ExtractTemplate template = new ExtractTemplate();
            var extractTo = @"C:\temp\solutioncreator\templateextract";
            var templateToExtract = @"D:\SourceControl\SolutionCreator\example\MvcWebApplicationProjectTemplatev2.0.cs.zip";

            template.Extract(templateToExtract, extractTo);

            ProjectCreator creator = new ProjectCreator("NewWebsite", extractTo);
            creator.CreateSolutionFile();
            creator.UpdateProjectFile();
            creator.UpdateGlobalAsax();
            creator.UpdateAssemblyInfo();
            creator.UpdateFilesWithName();

            Console.WriteLine("Created...");
            Console.ReadLine();
        }
    }
}
