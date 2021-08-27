using NUnit.Framework;
using Sciendo.Test.Loader.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sciendo.TextLoader.Api.Tests
{
    [TestFixture]
    public class ProcessingRuleLinkTests
    {
        [Test]
        public void ProcessingRuleLinkOnlyLinkOk()
        {
            var rule = new ProcessingRuleLink();
            var item = new Item();
            var source = @"https://www.youtube.com/watch?v=yRIMq6rfuK4";
            var result = rule.Process(item, ref source);
            Assert.IsFalse(result);
            Assert.AreEqual(item.ContentType, ContentType.Link);
            Assert.IsNull(item.Subject);
            Assert.AreEqual(@"https://www.youtube.com/watch?v=yRIMq6rfuK4", item.Link);
            Assert.IsEmpty(source);
        }

        [Test]
        public void ProcessingRuleLinkOnlyLinkWithMessageOk()
        {
            var rule = new ProcessingRuleLink();
            var item = new Item();
            var source = @"No. 20 Artist this year Wargirl while listening to their album #DancingGold I was transposed in a different world somewhere in the future probably in 2069. What was it like, you ask? Awesome: https://www.youtube.com/watch?v=bMM8u5lbGeI&ab_channel=CloudsHillGroup";
            var result = rule.Process(item, ref source);
            Assert.IsTrue(result);
            Assert.AreEqual(item.ContentType, ContentType.Link);
            Assert.IsNull(item.Subject);
            Assert.AreEqual(@"https://www.youtube.com/watch?v=bMM8u5lbGeI&ab_channel=CloudsHillGroup", item.Link);
            Assert.AreEqual("No. 20 Artist this year Wargirl while listening to their album #DancingGold I was transposed in a different world somewhere in the future probably in 2069. What was it like, you ask? Awesome: ", source);

        }

        [Test]
        public void ProcessingRuleLinkOnlyLinkWithMessageBeforeAndAfterLinkOk()
        {
            var rule = new ProcessingRuleLink();
            var item = new Item();
            var source = @"No. 20 Artist this year Wargirl https://www.youtube.com/watch?v=bMM8u5lbGeI&ab_channel=CloudsHillGroup while listening to their album #DancingGold I was transposed in a different world somewhere in the future probably in 2069. What was it like, you ask? Awesome: https://www.youtube.com/watch?v=bMM8u5lbGeI&ab_channel=CloudsHillGroup";
            var result = rule.Process(item, ref source);
            Assert.IsTrue(result);
            Assert.AreEqual(item.ContentType, ContentType.Link);
            Assert.IsNull(item.Subject);
            Assert.AreEqual(@"https://www.youtube.com/watch?v=bMM8u5lbGeI&ab_channel=CloudsHillGroup", item.Link);
            Assert.AreEqual("No. 20 Artist this year Wargirl  while listening to their album #DancingGold I was transposed in a different world somewhere in the future probably in 2069. What was it like, you ask? Awesome: ", source);

        }
        [Test]
        public void ProcessingRuleLinkNoLinkOk()
        {
            var rule = new ProcessingRuleLink();
            var item = new Item();
            var source = @"8/12/20 19:42 - Octavian Duma: You call that poetic skills? I call it a defective brain to mouth filter";
            var result = rule.Process(item, ref source);
            Assert.IsTrue(result);
            Assert.AreEqual(item.ContentType, ContentType.None);
            Assert.IsNull(item.Subject);
            Assert.IsNull(item.Link);
            Assert.AreEqual(@"8/12/20 19:42 - Octavian Duma: You call that poetic skills? I call it a defective brain to mouth filter", source);

        }
    }
}
