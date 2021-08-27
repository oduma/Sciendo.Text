using System;
using System.Collections.Generic;
using System.Text;

namespace Sciendo.Test.Loader.Api
{
    public class ProcessingRuleContentMessage : IProcessingRule
    {
        public int Order => 4;

        public bool Process(Item item, ref string input)
        {
            item.Subject = input;
            if (item.ContentType == ContentType.None)
                item.ContentType = ContentType.Message;
            return false;
        }
    }
}
