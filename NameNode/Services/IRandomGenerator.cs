using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NameNode.Services
{
    public interface IRandomGenerator
    {
        int Generate(int max);
    }
}
