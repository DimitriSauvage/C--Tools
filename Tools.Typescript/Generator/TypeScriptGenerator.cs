using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using CsToTs;

namespace Tools.Typescript.Generator
{
    public class TypeScriptGenerator
    {
        /// <summary>
        /// Generate the typescript
        /// </summary>
        /// <param name="types">Types to transform</param>
        /// <param name="fileName">File name</param>
        public void GenerateTypeScriptModels([NotNull] IEnumerable<Type> types, string fileName = "")
        {
            //Get file name
            var file = fileName == null ? this.GetGeneratedFileInfo() : new FileInfo(fileName);
            if (file == null) throw new FileNotFoundException("The specified file name is invalid");


            //Generate typescript
            var options = new TypeScriptOptions()
            {
                UseDateForDateTime = true,
                UseInterfaceForClasses = (type) => false
            };
            var generatedTypeScript = CsToTs.Generator.GenerateTypeScript(options, types);

            //Write file
            if (file?.Directory != null && !file.Directory.Exists) file.Directory.Create();
            File.WriteAllText(file.FullName, generatedTypeScript, Encoding.UTF8);
        }

        #region Private methods

        /// <summary>
        /// Get the generated file path
        /// </summary>
        /// <returns></returns>
        private FileInfo GetGeneratedFileInfo()
        {
            return new FileInfo($"{this.GetGeneratedFileDirectory().FullName}/{this.GetFileName()}");
        }

        /// <summary>
        /// Get the generated file directory
        /// </summary>
        /// <returns>Generated file directory</returns>
        private DirectoryInfo GetGeneratedFileDirectory()
        {
            DirectoryInfo directory = null; //Directory where the file will be generated

            //Message to enter the directory path
            Console.WriteLine("Please, enter the path of the generated file directory :");

            var isPathValid = false; // If the path is valid
            do
            {
                var generatedFilePath = Console.ReadLine();
                if (generatedFilePath == null)
                {
                    Console.WriteLine("Please enter a valid path !");
                    continue;
                }

                directory = new DirectoryInfo(generatedFilePath);
                isPathValid = true;
            } while (!isPathValid);

            return directory;
        }

        /// <summary>
        /// Get the file name
        /// </summary>
        /// <returns>File name</returns>
        private string GetFileName()
        {
            Console.WriteLine("Please, enter the name of the generated file (Without the .ts) :");
            var result = "generated.ts";
            var tempResult = Console.ReadLine();

            if (tempResult != null && tempResult.Replace(".ts", "").Trim() != "")
                result = tempResult;

            return result;
        }

        #endregion
    }
}