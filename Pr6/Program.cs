using System;
using System.IO;
using System.Linq;

namespace Pr6
{
    class Program
    {
        static void Main(string[] args)
        {
            var counts = new decimal[9];
            File.ReadAllText("TextFile1.txt").Split(',').Select(x => int.Parse(x)).ToList().ForEach(x =>
            {
                counts[x]++;
            });

            Enumerable.Range(1, 256).ToList().ForEach(day =>
            {
                var newDayCounts = new decimal[9];
                Enumerable.Range(1, 8).ToList().ForEach(i => newDayCounts[i - 1] = counts[i]);
                newDayCounts[8] = counts[0];
                newDayCounts[6] += counts[0];

                Console.WriteLine($"day {day}, count {newDayCounts.Sum()}");
                counts = (decimal[])newDayCounts.Clone();
            });
            Console.ReadLine();
        }
    }
}
