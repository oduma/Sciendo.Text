using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sciendo.Test.Loader.Api
{
    public static class LoaderFactory
    {
        public static ILoader GetLoader(string source, IReader fileReader, IWriter dataWriter, int writeBatchSize)
        {
            if (Directory.Exists(source))
                return new FolderLoader(new FileLoader(fileReader, dataWriter,writeBatchSize));
            if (File.Exists(source))
                return new FileLoader(fileReader, dataWriter, writeBatchSize);
            throw new Exception("Invalid source");
        }
    }
}
