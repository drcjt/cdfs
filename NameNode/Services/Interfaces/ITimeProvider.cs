using System;

namespace NameNode.Services.Interfaces
{
    public interface ITimeProvider
    {
        DateTime Now { get; }
    }
}
