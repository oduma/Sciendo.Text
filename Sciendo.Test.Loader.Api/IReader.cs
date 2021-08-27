using System;
using System.Collections.Generic;
using System.Text;

namespace Sciendo.Test.Loader.Api
{
    public interface IReader
    {
        IEnumerable<Item> Read(string fileName);
    }
}
