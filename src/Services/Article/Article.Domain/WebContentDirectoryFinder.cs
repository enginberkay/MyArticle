using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Content.Domain
{
    public static class WebContentDirectoryFinder
    {
        public static string CalculateContentRootFolder()
        {
            var coreAssemblyDirectoryPath = Path.GetDirectoryName(System.Reflection.Assembly.GetAssembly(typeof(AppConst)).Location);
            if (coreAssemblyDirectoryPath == null)
            {
                throw new Exception("Could not find location of Content.Domain assembly!");
            }

            var directoryInfo = new DirectoryInfo(coreAssemblyDirectoryPath);
            while (!DirectoryContains(directoryInfo.FullName, "MyArticle.sln"))
            {
                if (directoryInfo.Parent == null)
                {
                    throw new Exception("Could not find content root folder!");
                }

                directoryInfo = directoryInfo.Parent;
            }
            directoryInfo = new DirectoryInfo(Path.Combine(directoryInfo.FullName, "Services", "Article"));
            var webMvcFolder = Path.Combine(directoryInfo.FullName, "Article.API");
            if (Directory.Exists(webMvcFolder))
            {
                return webMvcFolder;
            }

            throw new Exception("Could not find root folder of the web project!");
        }

        private static bool DirectoryContains(string directory, string fileName)
        {
            return Directory.GetFiles(directory).Any(filePath => string.Equals(Path.GetFileName(filePath), fileName));
        }
    }
}
