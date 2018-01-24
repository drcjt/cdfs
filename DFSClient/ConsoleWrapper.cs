using System;

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
