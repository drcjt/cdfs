using DFSClient.Commands;
using DFSClient.Options;

namespace DFSClient.CommandBuilders
{
    public interface ICommandFactory
    {
        ICommand Build(CommonSubOptions options);
    }
}
