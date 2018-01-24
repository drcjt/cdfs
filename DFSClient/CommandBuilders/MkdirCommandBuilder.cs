using DFSClient.Commands;
using DFSClient.Options;

namespace DFSClient.CommandBuilders
{
    public class MkdirCommandBuilder : ICommandBuilder<MkdirSubOptions>
    {
        public ICommand Build(MkdirSubOptions options)
        {
            return new MkdirCommand { DirectoryPath = options.DirectoryPath[0] };
        }
    }
}
