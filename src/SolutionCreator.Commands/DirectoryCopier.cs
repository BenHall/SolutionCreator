﻿using System.IO;

namespace SolutionCreator.Commands
{
    public class DirectoryCopier
    {
        public void Copy(DirectoryInfo source, DirectoryInfo target)
        {
            foreach (DirectoryInfo dir in source.GetDirectories())
            {
                if(!dir.Name.StartsWith(".") && !dir.Name.StartsWith("_"))
                    Copy(dir, target.CreateSubdirectory(dir.Name));
            }
            foreach (FileInfo file in source.GetFiles())
                file.CopyTo(Path.Combine(target.FullName, file.Name), true);
        }
    }
}