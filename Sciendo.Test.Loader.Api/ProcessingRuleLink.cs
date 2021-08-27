using System;
using System.Collections.Generic;
using System.Text;

namespace Sciendo.Test.Loader.Api
{
    public class ProcessingRuleLink : IProcessingRule
    {
        public int Order => 3;

        public bool Process(Item item, ref string input)
        {
            if (input.Length == 0)
                return false;
            var startOfLink = input.IndexOf("http");

            if (startOfLink == -1)
                return true;
            var possibleLink = string.Empty;
            if(input.IndexOf(" ")==-1)
            {
                possibleLink = input;
            }
            else
            {
                var endOfLink = input.IndexOf(" ", startOfLink);
                if (endOfLink == -1)
                    endOfLink = input.Length;
                possibleLink = input.Substring(startOfLink, endOfLink - startOfLink).Trim();
            }
            if (possibleLink.StartsWith("http"))
            {
                item.Link = possibleLink;
                input = input.Replace(item.Link, "");
                item.ContentType = ContentType.Link;
            }
            if (string.IsNullOrEmpty(input))
                return false;
            return true;

        }
    }
}
