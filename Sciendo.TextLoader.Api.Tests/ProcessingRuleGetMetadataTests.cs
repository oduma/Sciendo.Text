using NUnit.Framework;
using Sciendo.Test.Loader.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sciendo.TextLoader.Api.Tests
{
    [TestFixture]
    public class ProcessingRuleGetMetadataTests
    {
        [Test]
        public void ProcessingRuleGetMedataOk()
        {
            var rule = new ProcessingRuleGetMetadata();
            var item = new Item();
            var source = @"8/12/20 19:42 - Octavian Duma: You call that poetic skills? I call it a defective brain to mouth filter";
            var result = rule.Process(item, ref source);
            Assert.IsTrue(result);
            Assert.AreEqual(item.ContentType, ContentType.None);
            Assert.AreEqual(new DateTime(2020, 12, 8, 19, 42, 0), item.When);
            Assert.AreEqual("Octavian Duma", item.Owner);
            Assert.AreEqual(@"You call that poetic skills? I call it a defective brain to mouth filter", source);
        }

        [Test]
        public void ProcessingRuleGetMetadataNotOk()
        {
            var rule = new ProcessingRuleGetMetadata();
            var item = new Item();
            var source = @"8/12/20 18:30 - Los mensajes y las llamadas están cifrados de extremo a extremo. Nadie fuera de este chat, ni siquiera WhatsApp, puede leerlos ni escucharlos. Toca para obtener más información.";
            var result = rule.Process(item, ref source);
            Assert.IsFalse(result);
            Assert.AreEqual(item.ContentType, ContentType.None);
            Assert.IsNull(item.Owner);
            Assert.AreEqual(@"8/12/20 18:30 - Los mensajes y las llamadas están cifrados de extremo a extremo. Nadie fuera de este chat, ni siquiera WhatsApp, puede leerlos ni escucharlos. Toca para obtener más información.", source);

        }
    }
}
