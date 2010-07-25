using System;
using System.IO;

namespace SolutionCreator.Commands
{
    public class ProjectCreator
    {
        private readonly string _nameOfProject;
        private readonly string _solutionLocation;

        public ProjectCreator(string nameOfProject, string solutionLocation)
        {
            _nameOfProject = nameOfProject;
            _solutionLocation = solutionLocation;
        }

        public void UpdateFilesWithName()
        {
            var cSharpFiles = Directory.GetFiles(_solutionLocation, "*.*", SearchOption.AllDirectories);
            foreach (var cSharpFile in cSharpFiles)
            {
                ReplaceContentsOfFile(cSharpFile, "$safeprojectname$", _nameOfProject);
            }
        }

        public void UpdateGlobalAsax()
        {
            var globalClassName = "MvcApplication";

            var global = Directory.GetFiles(_solutionLocation, "*.asax", SearchOption.AllDirectories);
            foreach (var g in global)
            {
                ReplaceContentsOfFile(g, "$languageext$", "cs");
                ReplaceContentsOfFile(g, "$language$", "C#");
                ReplaceContentsOfFile(g, "$globalclassname$", globalClassName);
            }

            var globalCs = Directory.GetFiles(_solutionLocation, "*.asax.cs", SearchOption.AllDirectories);
            foreach (var gc in globalCs)
            {
                ReplaceContentsOfFile(gc, "$globalclassname$", globalClassName);
            }
        }

        public void UpdateProjectFile()
        {
            var projectFiles = Directory.GetFiles(_solutionLocation, "*.csproj", SearchOption.AllDirectories);
            foreach (var proj in projectFiles)
            {
                ReplaceContentsOfFile(proj, "$guid1$", "65623951-21A5-4BBF-BC0D-92B184986122");
                ReplaceContentsOfFile(proj, "$targetframeworkversion$", "3.5");
            }

        }

        public void UpdateAssemblyInfo()
        {
            var cSharpFiles = Directory.GetFiles(_solutionLocation, "AssemblyInfo.cs", SearchOption.AllDirectories);
            foreach (var cSharpFile in cSharpFiles)
            {
                ReplaceContentsOfFile(cSharpFile, "$registeredorganization$", "Company");
                ReplaceContentsOfFile(cSharpFile, "$year$", "Year");
                ReplaceContentsOfFile(cSharpFile, "$registeredorganization$", "Company");
                ReplaceContentsOfFile(cSharpFile, "$guid2$", Guid.NewGuid().ToString());
            }
        }

        public void CreateSolutionFile()
        {
            var contents = "Microsoft Visual Studio Solution File, Format Version 11.00" + Environment.NewLine +
                              "Project(\"{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}\") = \"NewWebsite\", \"MvcApplication.csproj\", \"{65623951-21A5-4BBF-BC0D-92B184986122}\"" + Environment.NewLine +
                              "EndProject" + Environment.NewLine +
                              "Global" + Environment.NewLine +
                              "	GlobalSection(SolutionConfigurationPlatforms) = preSolution" + Environment.NewLine +
                              "		Debug|Any CPU = Debug|Any CPU" + Environment.NewLine +
                              "		Release|Any CPU = Release|Any CPU" + Environment.NewLine +
                              "	EndGlobalSection" + Environment.NewLine +
                              "	GlobalSection(ProjectConfigurationPlatforms) = postSolution" + Environment.NewLine +
                              "		{65623951-21A5-4BBF-BC0D-92B184986122}.Debug|Any CPU.ActiveCfg = Debug|Any CPU" + Environment.NewLine +
                              "		{65623951-21A5-4BBF-BC0D-92B184986122}.Debug|Any CPU.Build.0 = Debug|Any CPU" + Environment.NewLine +
                              "		{65623951-21A5-4BBF-BC0D-92B184986122}.Release|Any CPU.ActiveCfg = Release|Any CPU" + Environment.NewLine +
                              "		{65623951-21A5-4BBF-BC0D-92B184986122}.Release|Any CPU.Build.0 = Release|Any CPU" + Environment.NewLine +
                              "	EndGlobalSection" + Environment.NewLine +
                              "	GlobalSection(SolutionProperties) = preSolution" + Environment.NewLine +
                              "		HideSolutionNode = FALSE" + Environment.NewLine +
                              "	EndGlobalSection" + Environment.NewLine + 
                              "EndGlobal";

            StreamWriter writer = new StreamWriter(Path.Combine(_solutionLocation, "NewProject.sln"));
            writer.Write(contents);
            writer.Close();
            writer.Dispose();
        }

        private void ReplaceContentsOfFile(string file, string searchFor, string replaceWith)
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