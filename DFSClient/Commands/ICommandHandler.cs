using System;
using System.Collections.Generic;
using System.Text;

namespace DFSClient.Commands
{
    public interface ICommandHandler<in T> where T : ICommand
    {
        void Handle(T command);
    }
}
