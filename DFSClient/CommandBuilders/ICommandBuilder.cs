using DFSClient.Commands;
using DFSClient.Options;

namespace DFSClient.CommandBuilders
{
    interface ICommandBuilder<in T> where T : CommonSubOptions
    {
        ICommand Build(T options);
    }
}
