using NameNode.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NameNode.Services
{
    public class TimeProvider : ITimeProvider
    {
        DateTime ITimeProvider.Now { get => DateTime.Now; }
    }
}
