using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sciendo.Test.Loader.Api
{
    public class Item
    {
       
        public Item LoadItem(IEnumerable<IProcessingRule> processingRules, string input)
        {
            foreach(var processingRule in processingRules.OrderBy(r=>r.Order))
            {
                if(!processingRule.Process(this,ref input))
                {
                    return this;
                }
            }
            return this;
        }
        public DateTime When { get; set; }

        public string Owner { get; set; }

        public string Subject { get; set; }

        public string Link { get; set; }

        public ContentType ContentType { get; set; }
    }
}
