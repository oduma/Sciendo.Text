using System;
using System.Collections.Generic;
using System.Text;

namespace Sciendo.Test.Loader.Api
{
    public class ProcessingRuleGetMetadata : IProcessingRule
    {
        public int Order => 1;

        public bool Process(Item item, ref string input)
        {
            var inputParts = input.Split('-');
            var dateTimeParts = inputParts[0].Split(' ');
            if (dateTimeParts.Length < 2)
                return false;
            var dateParts = dateTimeParts[0].Split('/');
            var timeParts = dateTimeParts[1].Split(':');
            try
            {
                var dateTime = new DateTime(2000 + Convert.ToInt32(dateParts[2]), Convert.ToInt32(dateParts[1]),
                    Convert.ToInt32(dateParts[0]), Convert.ToInt32(timeParts[0]), Convert.ToInt32(timeParts[1]), 0);
                item.When = dateTime;
            }
            catch
            {
                return false;
            }
            var splitForTheOwner = inputParts[1].Split(':');
            if (splitForTheOwner.Length < 2)
            {
                return false;
            }
            item.Owner = splitForTheOwner[0].Trim();

            input = input.Substring(input.IndexOf(item.Owner) + item.Owner.Length + 2, input.Length - input.IndexOf(item.Owner) - item.Owner.Length - 2);
            return true;

        }
    }
}
