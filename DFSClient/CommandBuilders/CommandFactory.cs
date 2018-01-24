using DFSClient.Commands;
using DFSClient.Options;
using StructureMap;

namespace DFSClient.CommandBuilders
{
    public class CommandFactory : ICommandFactory
    {
        private readonly IContainer _container;
        public CommandFactory(IContainer container)
        {
            _container = container;
        }

        public ICommand Build(CommonSubOptions options)
        {
            var builderType = typeof(ICommandBuilder<>).MakeGenericType(options.GetType());
            dynamic builder = _container.GetInstance(builderType);
            return builder.Build((dynamic)options);
        }
    }
}
