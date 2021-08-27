using NUnit.Framework;
using Sciendo.Test.Loader.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sciendo.TextLoader.Api.Tests
{
    [TestFixture]
    public class ProcessingRuleMultimediaContentTests
    {
        [Test]
        public void ProcessingMultimediaContentOk()
        {
            var rule = new ProcessingRuleMultimediaContent();
            var item = new Item();
            var source = @"<Multimedia omitido>";
            var result = rule.Process(item, ref source);
            Assert.IsFalse(result);
            Assert.AreEqual(item.ContentType, ContentType.MultimediaMessage);
            Assert.IsNull(item.Subject);
            Assert.IsNull(item.Link);
            Assert.IsEmpty(source);

        }
        [Test]
        public void ProcessingMultimediaContentNoMultimediaOk()
        {
            var rule = new ProcessingRuleMultimediaContent();
            var item = new Item();
            var source = @"No. 20 Artist this year Wargirl while listening to their album #DancingGold I was transposed in a different world somewhere in the future probably in 2069. What was it like, you ask? Awesome: https://www.youtube.com/watch?v=bMM8u5lbGeI&ab_channel=CloudsHillGroup";
            var result = rule.Process(item, ref source);
            Assert.IsTrue(result);
            Assert.AreEqual(item.ContentType, ContentType.None);
            Assert.IsNull(item.Subject);
            Assert.IsNull(item.Link);
            Assert.AreEqual(@"No. 20 Artist this year Wargirl while listening to their album #DancingGold I was transposed in a different world somewhere in the future probably in 2069. What was it like, you ask? Awesome: https://www.youtube.com/watch?v=bMM8u5lbGeI&ab_channel=CloudsHillGroup", source);

        }
    }
}
