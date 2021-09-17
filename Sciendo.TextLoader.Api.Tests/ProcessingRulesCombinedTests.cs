using NUnit.Framework;
using Sciendo.Test.Loader.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sciendo.TextLoader.Api.Tests
{
    [TestFixture]
    public class ProcessingRulesCombinedTests
    {
        List<IProcessingRule> processingRules;

        [SetUp]
        public void SetUp()
        {
            processingRules= new List<IProcessingRule>(){ 
                new ProcessingRuleContentMessage(), 
                new ProcessingRuleGetMetadata(), 
                new ProcessingRuleLink(), 
                new ProcessingRuleMultimediaContent(), 
                new ProcessingRuleSystemMessage()};
        }
        [Test]
        public void ProcessASystemMessage()
        {
            var item = new Item();
            var source = @"8/12/20 18:30 - Los mensajes y las llamadas están cifrados de extremo a extremo. Nadie fuera de este chat, ni siquiera WhatsApp, puede leerlos ni escucharlos. Toca para obtener más información.";
            foreach (var processingRule in processingRules.OrderBy(r=>r.Order))
            {

                if (!processingRule.Process(item, ref source))
                    break;
            }
            Assert.AreEqual(item.ContentType, ContentType.None);
            Assert.AreEqual(new DateTime(),item.When);
            Assert.IsNull(item.Owner);
            Assert.IsNull(item.Subject);
            Assert.IsNull(item.Link);

        }
        [Test]
        public void ProcessAMultimediaMessage()
        {
            var item = new Item();
            var source = @"10/12/20 12:25 - Gala: <Multimedia omitido>
";
            foreach (var processingRule in processingRules.OrderBy(r => r.Order))
            {

                if (!processingRule.Process(item, ref source))
                    break;
            }
            Assert.AreEqual(item.ContentType, ContentType.MultimediaMessage);
            Assert.AreEqual(new DateTime(2020,12,10,12,25,0), item.When);
            Assert.AreEqual("Gala", item.Owner);
            Assert.IsNull(item.Subject);
            Assert.IsNull(item.Link);

        }
        [Test]
        public void ProcessALinkOnlyMessage()
        {
            var item = new Item();
            var source = @"9/12/20 20:09 - Octavian Duma: https://www.youtube.com/watch?v=yRIMq6rfuK4";
            foreach (var processingRule in processingRules.OrderBy(r => r.Order))
            {

                if (!processingRule.Process(item, ref source))
                    break;
            }
            Assert.AreEqual(item.ContentType, ContentType.Link);
            Assert.AreEqual(new DateTime(2020, 12, 9, 20, 9, 0), item.When);
            Assert.AreEqual("Octavian Duma", item.Owner);
            Assert.IsNull(item.Subject);
            Assert.AreEqual("https://www.youtube.com/watch?v=yRIMq6rfuK4",item.Link);

        }
        [Test]
        public void ProcessAMessage()
        {
            var item = new Item();
            var source = @"8/12/20 19:42 - Octavian Duma: You call that poetic skills? I call it a defective brain to mouth filter";
            foreach (var processingRule in processingRules.OrderBy(r => r.Order))
            {

                if (!processingRule.Process(item, ref source))
                    break;
            }
            Assert.AreEqual(item.ContentType, ContentType.Message);
            Assert.AreEqual(new DateTime(2020, 12, 8, 19, 42, 0), item.When);
            Assert.AreEqual("Octavian Duma", item.Owner);
            Assert.IsNull(item.Link);
            Assert.AreEqual("You call that poetic skills? I call it a defective brain to mouth filter", item.Subject);

        }
        [Test]
        public void ProcessAMessageWithALink()
        {
            var item = new Item();
            var source = @"11/12/20 7:38 - Octavian Duma: No. 20 Artist this year Wargirl while listening to their album #DancingGold I was transposed in a different world somewhere in the future probably in 2069. What was it like, you ask? Awesome: https://www.youtube.com/watch?v=bMM8u5lbGeI&ab_channel=CloudsHillGroup";
            foreach (var processingRule in processingRules.OrderBy(r => r.Order))
            {

                if (!processingRule.Process(item, ref source))
                    break;
            }
            Assert.AreEqual(item.ContentType, ContentType.Link);
            Assert.AreEqual(new DateTime(2020, 12, 11, 7, 38, 0), item.When);
            Assert.AreEqual("Octavian Duma", item.Owner);
            Assert.AreEqual("https://www.youtube.com/watch?v=bMM8u5lbGeI&ab_channel=CloudsHillGroup", item.Link);
            Assert.AreEqual("No. 20 Artist this year Wargirl while listening to their album #DancingGold I was transposed in a different world somewhere in the future probably in 2069. What was it like, you ask? Awesome: ", item.Subject);

        }
    }
}
