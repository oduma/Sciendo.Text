using System;
using System.Collections.Generic;
using System.Text;

namespace Sciendo.Test.Loader.Api
{
    public interface IProcessingRule
    {
        int Order { get;}

        bool Process(Item item, ref string input);
    }
}
