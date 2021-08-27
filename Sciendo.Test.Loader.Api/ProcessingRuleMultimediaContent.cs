using System;
using System.Collections.Generic;
using System.Text;

namespace Sciendo.Test.Loader.Api
{
    public class ProcessingRuleMultimediaContent : IProcessingRule
    {
        private const string multimediaMaker = "multimedia";
        public int Order => 2;

        public bool Process(Item item, ref string input)
        {
            if (input.StartsWith("<") && input.EndsWith(">") && input.ToLower().Contains(multimediaMaker))
            {
                item.ContentType = ContentType.MultimediaMessage;
                return false;
            }
            return true;
        }
    }
}
