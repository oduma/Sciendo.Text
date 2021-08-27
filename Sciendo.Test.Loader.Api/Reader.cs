using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sciendo.Test.Loader.Api
{
    public class Reader : IReader
    {
        private readonly IEnumerable<IProcessingRule> progessingRules;

        public Reader(IEnumerable<IProcessingRule> progessingRules )
        {
            this.progessingRules = progessingRules;
        }
        public IEnumerable<Item> Read(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException(nameof(fileName));
            if (!File.Exists(fileName)) throw new FileNotFoundException("File Not Found", fileName);
            var fileLines = File.ReadAllLines(fileName);

            StringBuilder itemSource = new StringBuilder();
            foreach(var fileLine in fileLines)
            {
                if(LineStarstWithDate(fileLine))
                {
                    if(itemSource!=null)
                    {
                        var item = new Item();
                        var source = itemSource.ToString();
                        itemSource = null;
                        yield return item.LoadItem(progessingRules, source);
                    }
                    else
                    {
                        itemSource.Append(fileLine);
                    }
                }
                else
                {
                    itemSource.Append(fileLine);
                }
            }
            if(itemSource!=null)
            {
                var item = new Item();
                var source = itemSource.ToString();
                itemSource = null;
                yield return item.LoadItem(progessingRules, source);
            }
        }

        private bool LineStarstWithDate(string fileLine)
        {
            if (string.IsNullOrEmpty(fileLine))
                return false;
            if (fileLine.Length < 8)
                return false;
            var possibleDate = fileLine.Substring(0, 8);
            var possibleDateParts = possibleDate.Split('/');
            if (possibleDateParts.Length != 3)
                return false;
            try
            {
                var date = new DateTime(2000 + Convert.ToInt32(possibleDateParts[2]), Convert.ToInt32(possibleDateParts[1]), Convert.ToInt32(possibleDateParts[0]));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
