using NameNode.Services.Interfaces;
using System;

namespace NameNode.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        DateTime IDateTimeProvider.Now => DateTime.Now;
        DateTime IDateTimeProvider.UtcNow => DateTime.UtcNow;
    }
}
