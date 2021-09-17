using NUnit.Framework;
using Sciendo.Test.Loader.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sciendo.TextLoader.Api.Tests
{
    [TestFixture]
    public class ProcessingRuleContentMessageTests
    {
        [Test]
        public void ProcessingRuleMessageContentOk()
        {
            var rule = new ProcessingRuleContentMessage();
            var item = new Item();
            var source = @"You call that poetic skills? I call it a defective brain to mouth filter";
            var result = rule.Process(item, ref source);
            Assert.IsFalse(result);
            Assert.AreEqual(item.ContentType, ContentType.Message);
            Assert.AreEqual(@"You call that poetic skills? I call it a defective brain to mouth filter",item.Subject);
            Assert.IsNull(item.Link);
            Assert.IsEmpty(source);
        }
        [Test]
        public void ProcessingRuleMessageContentAfterLinkOk()
        {
            var rule = new ProcessingRuleContentMessage();
            var item = new Item();
            item.ContentType = ContentType.Link;
            var source = @"You call that poetic skills? I call it a defective brain to mouth filter";
            var result = rule.Process(item, ref source);
            Assert.IsFalse(result);
            Assert.AreEqual(item.ContentType, ContentType.Link);
            Assert.AreEqual(@"You call that poetic skills? I call it a defective brain to mouth filter", item.Subject);
            Assert.IsNull(item.Link);
            Assert.IsEmpty(source);
        }
    }
}
