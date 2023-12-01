using System;
using System.IO;
using System.Linq;

namespace Pr1
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = File.ReadAllLines("TextFile1.txt").Select((x, i) => new { Number = int.Parse(x), Index = i }).ToArray();

            var result = s.Skip(1).Count(x => x.Number > s[x.Index - 1].Number);
            var result2 = s.Skip(3).Count(x => x.Number > s[x.Index - 3].Number);
            Console.WriteLine(result2);
            Console.ReadLine();
        }
    }
}
