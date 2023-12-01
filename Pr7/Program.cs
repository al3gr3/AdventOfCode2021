using System;
using System.IO;
using System.Linq;

namespace Pr7
{
    class Program
    {
        static void Main(string[] args)
        {
            var positions = File.ReadAllText("TextFile1.txt").Split(',').Select(x => int.Parse(x)).ToList();

            var min = positions.Min();
            var max = positions.Max();

            
            var result = Enumerable.Range(min, max - min + 1).Select(center => new { Center = center, Sum = positions.Select(x => f(Math.Abs(x - center))).Sum() }).OrderBy(x => x.Sum);

            Console.WriteLine(result.First().Center);
        }

        private static int f(int v)
        {
            return (v * (v + 1)) / 2;
        }
    }
}
