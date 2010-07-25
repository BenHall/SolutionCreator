using System.Text.RegularExpressions;

namespace SolutionCreator.Commands
{
    public class ProjectExtracter : ProjectModifier
    {
        private readonly string _solutionName;

        public ProjectExtracter(string solutionLocation, string solutionName) : base(solutionLocation)
        {
            _solutionName = solutionName;
        }

        public void UpdateGlobalAsax()
        {
            var global = FindFiles("*.asax");
            foreach (var g in global)
            {
                ReplaceContentsOfFile(g, new Regex("Codebehind=\"Global.asax.(.*?)\""), "Codebehind=\"Global.asax.$languageext$\"");
                ReplaceContentsOfFile(g, new Regex("Inherits=\"(.*?).(.*?)\""), "Inherits=\"$safeprojectname$.$globalclassname$\"");
                ReplaceContentsOfFile(g, new Regex("Language=\"(.*?)\""), "Language=\"$language$\"");
            }

            
        }

        public void UpdateProjectFile()
        {
            foreach (var file in FindFiles("*.csproj"))
            {
                ReplaceContentsOfFile(file, new Regex("<RootNamespace>(.*?)</RootNamespace>"), "<RootNamespace>$safeprojectname$</RootNamespace>");
                ReplaceContentsOfFile(file, new Regex("<AssemblyName>(.*?)</AssemblyName>"), "<AssemblyName>$safeprojectname$</AssemblyName>");
                ReplaceContentsOfFile(file, new Regex("<TargetFrameworkVersion>(.*?)</TargetFrameworkVersion>"), "<TargetFrameworkVersion>v$targetframeworkversion$</TargetFrameworkVersion>");
                ReplaceContentsOfFile(file, new Regex("<ProjectGuid>(.*?)</ProjectGuid>"), "<ProjectGuid>{$guid1$}</ProjectGuid>");
            }
        }

        public void UpdateAssemblyInfo()
        {
            foreach (var file in FindFiles("AssemblyInfo.cs"))
            {
                ReplaceContentsOfFile(file, CreateEscapedRegexForAssembly("AssemblyCompany"), "[assembly: AssemblyCompany(\"$registeredorganization$\")]");
                ReplaceContentsOfFile(file, CreateEscapedRegexForAssembly("AssemblyProduct"), "[assembly: AssemblyProduct(\"$safeprojectname$\")]");
                ReplaceContentsOfFile(file, CreateEscapedRegexForAssembly("AssemblyCopyright"), "[assembly: AssemblyCopyright(\"Copyright © $registeredorganization$ $year$\")]");
                ReplaceContentsOfFile(file, CreateEscapedRegexForAssembly("Guid"), "[assembly: Guid(\"$guid2$\")]");                
            }
        }

        private Regex CreateEscapedRegexForAssembly(string value)
        {
            //new Regex()

            string s = string.Format(@"\[assembly: {0}\(""(.*?)""\)\]", value);
            return new Regex(s);

            //var escapeStart = Regex.Escape("[assembly:");
            //var middle = " {0}(\"{0}\")";
            //var escapeEnd = Regex.Escape("]");
            //return new Regex(escapeStart + string.Format(middle, value) + escapeEnd);
        }

        public void ReplaceValuesWithPlaceholders()
        {
            foreach (var file in FindFiles("*.cs"))
            {
                ReplaceContentsOfFile(file, _solutionName, "$safeprojectname$");
            }
        }
    }
}