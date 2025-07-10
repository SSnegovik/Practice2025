using System;
using System.IO;
using System.Linq;


public class DirectorySizeCommand(string directoryPath) : ICommand
{

    public string DirectoryPath = directoryPath;
    public long Size;

    public void Execute()
    {
        Size = 0;
        foreach (var file in Directory.GetFiles(DirectoryPath))
        {
            Size += new FileInfo(file).Length;
        }
    }

    public class FindFilesCommand(string directoryPath, string searchPattern) : ICommand
    {

        public string DirectoryPath = directoryPath;
        public string SearchPattern = searchPattern;

        public void Execute()
        {
            var file = Directory.GetFiles(DirectoryPath, SearchPattern);
        }
    }
}

