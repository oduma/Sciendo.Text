using System;
using System.Collections.Generic;
using System.Text;

namespace Sciendo.Test.Loader.Api
{
    public interface IWriter
    {
        IList<int> Write(IEnumerable<Item> batch);
    }
}
