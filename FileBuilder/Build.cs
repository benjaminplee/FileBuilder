using System;
using System.IO;

namespace FileBuilder
{
    class Build
    {
        private static int fileCounter = 1;

        static void Main(string[] args)
        {
            const string baseDirectoryPath = ".\\FILES";
            int desiredFileCount = 16;

            if(args.Length > 0)
            {
                desiredFileCount = Convert.ToInt32(args[0]);
            }

            var levels = (int) (Math.Log(desiredFileCount, 2) - 1);
            var rootDirectory = new DirectoryInfo(baseDirectoryPath);

            Console.WriteLine("Building " + desiredFileCount + " files in " + levels + " levels");
            Console.WriteLine("Cleaning out root directory: " + rootDirectory.FullName);

            if(rootDirectory.Exists)
            {
                rootDirectory.Delete(true);
            }

            BuildLevel(levels, rootDirectory);
        }

        static void BuildLevel(int currentLevel, DirectoryInfo baseDirectory)
        {
            Console.WriteLine("Building in directory: " + baseDirectory.FullName);

            baseDirectory.Create();

            if(currentLevel > 0)
            {
                currentLevel--;
                BuildLevel(currentLevel, new DirectoryInfo(Path.Combine(baseDirectory.FullName, currentLevel + "A")));
                BuildLevel(currentLevel, new DirectoryInfo(Path.Combine(baseDirectory.FullName, currentLevel + "B")));
            }
            else
            {
                new FileInfo(Path.Combine(baseDirectory.FullName, (fileCounter++) + ".txt")).Create();
                new FileInfo(Path.Combine(baseDirectory.FullName, (fileCounter++) + ".txt")).Create();
            }
        }
    }
}
