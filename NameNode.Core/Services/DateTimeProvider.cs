using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NameNode.Core.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        DateTime IDateTimeProvider.Now => DateTime.Now;
        DateTime IDateTimeProvider.UtcNow => DateTime.UtcNow;
    }
}
