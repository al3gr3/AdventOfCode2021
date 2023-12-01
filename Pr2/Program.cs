using System;
using System.IO;
using System.Linq;

namespace Pr2
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = File.ReadAllLines("TextFile1.txt");

            var aim = 0;
            var horizontal = 0;
            var depth = 0;
            s.ToList().ForEach(line =>
            {
                var n = int.Parse(line.Split(' ').Skip(1).First());
                if (line.StartsWith('u'))
                {
                    aim -= n;
                }
                else if (line.StartsWith('d'))
                {
                    aim += n;
                }
                else if (line.StartsWith('f'))
                {
                    //It increases your horizontal position by X units.
                    //It increases your depth by your aim multiplied by X.
                    horizontal += n;
                    depth += (aim * n);
                }
            });
            var gs = s.GroupBy(s => s[0]);
            var forward = gs.Single(g => g.Key == 'f').Select(g => int.Parse(g.Split(' ').Skip(1).First())).Sum();
            var up = gs.Single(g => g.Key == 'u').Select(g => int.Parse(g.Split(' ').Skip(1).First())).Sum();
            var down = gs.Single(g => g.Key == 'd').Select(g => int.Parse(g.Split(' ').Skip(1).First())).Sum();
            Console.WriteLine((up - down) * forward);
            Console.WriteLine(depth * horizontal);
        }
    }
}
