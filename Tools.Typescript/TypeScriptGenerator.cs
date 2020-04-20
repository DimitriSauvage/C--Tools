using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using CsToTs;
using Tools.Application.DTOs;
using Tools.Domain.Abstractions;
using Tools.Helpers;

namespace Tools.Typescript
{
    public class TypeScriptGenerator
    {
        /// <summary>
        /// Generate the typescript
        /// </summary>
        /// <param name="assemblies">Assemblies in which search</param>
        public void GenerateTypeScriptModels([NotNull] IEnumerable<Assembly> assemblies)
        {
            var file = this.GetGeneratedFileInfo();
            if (file == null) throw new FileNotFoundException("The specified file name is invalid");
            var types = this.GetAllTypesToWrite(assemblies);

            var options = new TypeScriptOptions()
            {
                UseDateForDateTime = true,
                UseInterfaceForClasses = (type) => true
            };

            var generatedTypeScript = Generator.GenerateTypeScript(options, types);

            if (file?.Directory != null && !file.Directory.Exists) file.Directory.Create();

            File.WriteAllText(file.FullName, generatedTypeScript, Encoding.UTF8);
        }

        #region Private methods

        /// <summary>
        /// Get all types to write
        /// </summary>
        /// <param name="assemblies">Assemblies in which search</param>
        /// <returns></returns>
        private IEnumerable<Type> GetAllTypesToWrite([NotNull] IEnumerable<Assembly> assemblies)
        {
            //Types to get
            IEnumerable<Type> types = new List<Type>()
            {
                typeof(BaseDTO),
                typeof(EnumDTO<>),
                typeof(Enum),
                typeof(Entity),
                typeof(EntityWithId),
                typeof(EntityWithId<>),
                typeof(EntityWithTracking),
                typeof(EntityWithCompositeId),
                typeof(IEntity),
                typeof(IEntityWithId),
                typeof(IEntityWithId<>),
                typeof(IEntityWithTracking),
                typeof(IEntityWithCompositeId)
            };

            var result = new List<Type>();

            var foundedTypes = assemblies
                .SelectMany(x => x.GetTypes())
                .Where(x => !x.IsInterface)
                .ToList();

            //Get types to write
            foundedTypes.ForEach(type =>
            {
                if (types.Any(typeToFound => type.GetInterfaces().Any(x => x.GUID.Equals(typeToFound.GUID)) ||
                                             type.GetAllBaseTypes().Any(x => x.GUID.Equals(typeToFound.GUID))))
                {
                    result.Add(type);
                }
            });

            return result;
        }

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