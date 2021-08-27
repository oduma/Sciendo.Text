using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sciendo.Test.Loader.Api
{
    public class FileLoader : ILoader
    {
        private readonly IReader fileReader;
        private readonly IWriter dataWriter;
        private readonly int writeBatchSize;

        public FileLoader(IReader fileReader, IWriter dataWriter, int writeBatchSize)
        {
            this.fileReader = fileReader;
            this.dataWriter = dataWriter;
            this.writeBatchSize = writeBatchSize;
        }
        public void Load(string source)
        {
            fileReader.Read(source).Batch(writeBatchSize).ProcessBatchesNoReturn(dataWriter.Write);
        }
    }
}
