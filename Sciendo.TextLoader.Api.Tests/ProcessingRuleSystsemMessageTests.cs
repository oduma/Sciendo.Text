using NUnit.Framework;
using Sciendo.Test.Loader.Api;
using System;

namespace Sciendo.TextLoader.Api.Tests
{
    [TestFixture]
    public class ProcessingRuleSystsemMessageTests
    {
        [Test]
        public void SystemMessageDetectedOk()
        {
            var rule = new ProcessingRuleSystemMessage();
            var item = new Item();
            var source = @"8/12/20 18:30 - Los mensajes y las llamadas están cifrados de extremo a extremo. Nadie fuera de este chat, ni siquiera WhatsApp, puede leerlos ni escucharlos. Toca para obtener más información.";
            var result = rule.Process(item, ref source);
            Assert.IsFalse(result);
            Assert.AreEqual(item.ContentType, ContentType.None);

        }

        [Test]
        public void SystemMessageNotDetectedOk()
        {
            var rule = new ProcessingRuleSystemMessage();
            var item = new Item();
            var source = @"8/12/20 19:42 - Octavian Duma: You call that poetic skills? I call it a defective brain to mouth filter";
            var result = rule.Process(item, ref source);
            Assert.IsTrue(result);
            Assert.AreEqual(item.ContentType, ContentType.None);

        }
    }
}
