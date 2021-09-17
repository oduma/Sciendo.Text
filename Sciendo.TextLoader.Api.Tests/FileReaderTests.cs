using NUnit.Framework;
using Sciendo.Test.Loader.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sciendo.TextLoader.Api.Tests
{
    [TestFixture]
    public class FileReaderTests
    {
        [Test]
        public void ReadOneLineMessagesFromFileOk()
        {
            var fileReader = new Reader(new List<IProcessingRule>(){
                new ProcessingRuleContentMessage(),
                new ProcessingRuleGetMetadata(),
                new ProcessingRuleLink(),
                new ProcessingRuleMultimediaContent(),
                new ProcessingRuleSystemMessage()});
            var actual = fileReader.Read("OneLineMessagesFile.txt").ToList();
            Assert.AreEqual(15, actual.Count);
            Assert.AreEqual(9, actual.Where(i => i.Owner == "Octavian Duma").Count());
            Assert.AreEqual(4, actual.Where(i => i.ContentType == ContentType.Link).Count());
            Assert.AreEqual(4, actual.Where(i => i.ContentType == ContentType.MultimediaMessage).Count());
        }

        [Test]
        public void ReadMultiLinesMessagesFromFileOk()
        {
            var fileReader = new Reader(new List<IProcessingRule>(){
                new ProcessingRuleContentMessage(),
                new ProcessingRuleGetMetadata(),
                new ProcessingRuleLink(),
                new ProcessingRuleMultimediaContent(),
                new ProcessingRuleSystemMessage()});
            var actual = fileReader.Read("MultiLineMessagesFile.txt").ToList();
            Assert.AreEqual(3, actual.Count);
            Assert.AreEqual(2, actual.Where(i => i.Owner == "Octavian Duma").Count());
            Assert.AreEqual(1, actual.Where(i => i.ContentType == ContentType.Link).Count());
            Assert.AreEqual(2, actual.Where(i => i.ContentType == ContentType.Message).Count());
        }

    }
}
