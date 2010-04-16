using System;
using System.IO;

namespace FileBuilder
{
    class Build
    {
        private static int _fileCounter = 1;
        private static long _kilobytes = 1;

        static void Main(string[] args)
        {
            const string baseDirectoryPath = ".\\FILES";
            int desiredFileCount = 16;

            if(args.Length > 0)
            {
                desiredFileCount = Convert.ToInt32(args[0]);
            }

            if(args.Length > 1)
            {
                _kilobytes = Convert.ToInt32(args[1]);
            }

            var levels = (int) (Math.Log(desiredFileCount, 2) - 1);
            var rootDirectory = new DirectoryInfo(baseDirectoryPath);

            Console.WriteLine("Building " + desiredFileCount + ", " + _kilobytes + "kB files in " + levels + " levels");
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
                BuildFile(baseDirectory, _fileCounter++);
                BuildFile(baseDirectory, _fileCounter++);
            }
        }

        private static void BuildFile(DirectoryInfo baseDirectory, int fileCount)
        {
            var fileInfo = new FileInfo(Path.Combine(baseDirectory.FullName, fileCount + ".txt"));

            using(var stream = fileInfo.CreateText())
            {
                for (var i = 0; i < _kilobytes * 1024; i++)
                {
                    stream.Write('X');
                }
            }
        }
    }
}
