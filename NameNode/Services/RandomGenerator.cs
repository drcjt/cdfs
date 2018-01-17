using NameNode.Services.Interfaces;
using System;

namespace NameNode.Services
{
    public class RandomGenerator : IRandomGenerator
    {
        private readonly Random _random = new Random();

        public int Generate(int max)
        {
            return _random.Next(max);
        }
    }
}
