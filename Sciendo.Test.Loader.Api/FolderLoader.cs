using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sciendo.Test.Loader.Api
{
    public class FolderLoader : ILoader
    {
        private readonly ILoader fileLoader;

        public FolderLoader (ILoader fileLoader)
        {
            this.fileLoader = fileLoader;
        }
        public void Load(string source)
        {
            foreach(string fileName in Directory.EnumerateFiles(source,"*.txt",SearchOption.AllDirectories))
            {
                try
                {
                    fileLoader.Load(fileName);
                }
                catch(Exception ex)
                {
                    //log ex but continue 
                }
            }
        }
    }
}
