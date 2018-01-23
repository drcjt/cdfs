﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DFSClient.Commands
{
    public interface ICommandDispatcher
    {
        void Dispatch(ICommand command);
    }
}