using System;
using System.Collections.Generic;
using System.IO;

namespace SolutionCreator.Commands
{
    public class ProjectCreator : ProjectModifier
    {
        private readonly string _solutionName;
        private readonly string _solutionLocation;

        public ProjectCreator(string solutionName, string solutionLocation) : base(solutionLocation)
        {
            _solutionName = solutionName;
            _solutionLocation = solutionLocation;
        }

        public void UpdateFilesWithName()
        {
            var cSharpFiles = FindFiles("*.*");
            foreach (var cSharpFile in cSharpFiles)
            {
                ReplaceContentsOfFile(cSharpFile, "$safeprojectname$", _solutionName);
            }
        }

        public void UpdateGlobalAsax()
        {
            var globalClassName = "MvcApplication";

            var global = FindFiles("*.asax");
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

        public List<Project> UpdateProjectFile()
        {
            List<Project> updatedFiles = new List<Project>();
            var projectFiles = FindFiles("*.csproj");
            foreach (var proj in projectFiles)
            {
                string guid = Guid.NewGuid().ToString();
                updatedFiles.Add(new Project { Location = proj, Guid = guid });
                ReplaceContentsOfFile(proj, "$guid1$", guid);
                ReplaceContentsOfFile(proj, "$targetframeworkversion$", "3.5");
            }

            return updatedFiles;
        }

        public void UpdateAssemblyInfo()
        {
            var cSharpFiles = FindFiles("AssemblyInfo.cs");
            foreach (var cSharpFile in cSharpFiles)
            {
                ReplaceContentsOfFile(cSharpFile, "$registeredorganization$", "Company");
                ReplaceContentsOfFile(cSharpFile, "$year$", "Year");
                ReplaceContentsOfFile(cSharpFile, "$registeredorganization$", "Company");
                ReplaceContentsOfFile(cSharpFile, "$guid2$", Guid.NewGuid().ToString());
            }
        }

        public void CreateSolutionFile(List<Project> projectFiles)
        {
            var contents = "Microsoft Visual Studio Solution File, Format Version 11.00"; 

            foreach (var proj in projectFiles)
            {
                var projLocation = proj.Location.Replace(_solutionLocation, "");
                contents = contents + Environment.NewLine + 
                           "Project(\"{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}\") = \"NewWebsite\", \"" + projLocation + "\", \"{" + proj.Guid + "}\"" 
                                + Environment.NewLine +
                              "EndProject";
            }

            contents = contents + Environment.NewLine +
                       "Global" + Environment.NewLine +
                       "	GlobalSection(SolutionConfigurationPlatforms) = preSolution" + Environment.NewLine +
                       "		Debug|Any CPU = Debug|Any CPU" + Environment.NewLine +
                       "		Release|Any CPU = Release|Any CPU" + Environment.NewLine +
                       "	EndGlobalSection" + Environment.NewLine +
                       "	GlobalSection(ProjectConfigurationPlatforms) = postSolution";

            foreach (var proj in projectFiles)
            {
                contents = contents + Environment.NewLine + 
                              "		{" + proj.Guid + "}.Debug|Any CPU.ActiveCfg = Debug|Any CPU" + Environment.NewLine +
                              "		{" + proj.Guid + "}.Debug|Any CPU.Build.0 = Debug|Any CPU" + Environment.NewLine +
                              "		{" + proj.Guid + "}.Release|Any CPU.ActiveCfg = Release|Any CPU" + Environment.NewLine +
                              "		{" + proj.Guid + "}.Release|Any CPU.Build.0 = Release|Any CPU";
            }


            contents = contents + Environment.NewLine + 
                              "	EndGlobalSection" + Environment.NewLine +
                              "	GlobalSection(SolutionProperties) = preSolution" + Environment.NewLine +
                              "		HideSolutionNode = FALSE" + Environment.NewLine +
                              "	EndGlobalSection" + Environment.NewLine + 
                              "EndGlobal";

            StreamWriter writer = new StreamWriter(Path.Combine(_solutionLocation, _solutionName + ".sln"));
            writer.Write(contents);
            writer.Close();
            writer.Dispose();
        }
    }
}