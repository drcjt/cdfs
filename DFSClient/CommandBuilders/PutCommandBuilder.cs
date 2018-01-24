using DFSClient.Commands;
using DFSClient.Options;

namespace DFSClient.CommandBuilders
{
    public class PutCommandBuilder : ICommandBuilder<PutSubOptions>
    {
        public ICommand Build(PutSubOptions options)
        {
            return new PutCommand { SrcFile = options.PutValues[0], FilePath = options.PutValues[1] };
        }
    }
}
