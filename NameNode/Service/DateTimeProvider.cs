using NameNode.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameNode.Service
{
    public class DateTimeProvider : IDateTimeProvider
    {
        DateTime IDateTimeProvider.Now => DateTime.Now;
        DateTime IDateTimeProvider.UtcNow => DateTime.UtcNow;
    }
}
