using System;
using System.Collections.Generic;
using System.Text;

namespace DFSClient
{
    public class ConsoleWrapper : IConsole
    {
        public void WriteLine(object value)
        {
            Console.WriteLine(value);
        }
    }
}
