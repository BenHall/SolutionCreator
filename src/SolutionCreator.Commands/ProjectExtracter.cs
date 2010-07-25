using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SolutionCreator.Commands
{
    public class ProjectExtracter : ProjectModifier
    {
        public ProjectExtracter(string solutionLocation) : base(solutionLocation)
        {
        }
        
        public void UpdateGlobalAsax()
        {
        }

        public List<Project> UpdateProjectFile()
        {
            foreach (var findFile in FindFiles("*.csproj"))
            {
                ReplaceContentsOfFile(findFile, new Regex("<RootNamespace>(.*?)</RootNamespace>"), "<RootNamespace>$safeprojectname$</RootNamespace>");
                ReplaceContentsOfFile(findFile, new Regex("<AssemblyName>(.*?)</AssemblyName>"), "<AssemblyName>$safeprojectname$</AssemblyName>");
                ReplaceContentsOfFile(findFile, new Regex("<TargetFrameworkVersion>(.*?)</TargetFrameworkVersion>"), "<TargetFrameworkVersion>v$targetframeworkversion$</TargetFrameworkVersion>");
            }

            return null;
        }

        public void UpdateAssemblyInfo()
        {

        }

        public void ReplaceValuesWithPlaceholders()
        {

        }
    }
}