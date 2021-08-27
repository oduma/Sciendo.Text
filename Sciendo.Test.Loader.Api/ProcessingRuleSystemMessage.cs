using System;
using System.Collections.Generic;
using System.Text;

namespace Sciendo.Test.Loader.Api
{
    public class ProcessingRuleSystemMessage : IProcessingRule
    {
        public int Order => 0;

        public bool Process(Item item, ref string input)
        {
            var inputParts = input.Split('-');
            if (inputParts.Length < 2)
                return false;
            if (inputParts[1].Split(':').Length < 2)
                return false;
            return true;
        }
    }
}
