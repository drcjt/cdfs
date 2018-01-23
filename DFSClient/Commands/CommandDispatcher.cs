using StructureMap;
using System;
using System.Collections.Generic;
using System.Text;

namespace DFSClient.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IContainer _container;
        public CommandDispatcher(IContainer container)
        {
            _container = container;
        }

        public void Dispatch(ICommand command)
        {
            var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
            dynamic handler = _container.GetInstance(handlerType);
            handler.Handle((dynamic)command);
        }
    }
}
