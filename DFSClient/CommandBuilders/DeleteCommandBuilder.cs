using DFSClient.Commands;
using DFSClient.Options;

namespace DFSClient.CommandBuilders
{
    public class DeleteCommandBuilder : ICommandBuilder<DeleteSubOptions>
    {
        public ICommand Build(DeleteSubOptions options)
        {
            return new DeleteCommand { FilePath = options.FilePath[0] };
        }
    }
}
