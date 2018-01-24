using DFSClient.Commands;
using DFSClient.Options;

namespace DFSClient.CommandBuilders
{
    public class ListingCommandBuilder : ICommandBuilder<ListingSubOptions>
    {
        public ICommand Build(ListingSubOptions options)
        {
            return new ListingCommand { FilePath = options.FilePath.Count > 0 ? options.FilePath[0] : "" };
        }
    }
}
