using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Pr7
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = 0;
            var d = new Dictionary<char, int>
            {
                {')', 3 },
                {']', 57 },
                {'}', 1197 },
                { '>', 25137 },
            };
            File.ReadAllLines("TextFile1.txt").ToList().ForEach(line =>
            {
                var firstIllegal = illegal(line);
                if (firstIllegal.HasValue)
                    result += d[firstIllegal.Value];
            });

            var scores = File.ReadAllLines("TextFile1.txt")
                .Select(line => autocomplete(line))
                .Where(x => x.HasValue)
                .Select(x => x.Value)
                .OrderBy(x => x)
                .ToList();
            var result2 = scores[scores.Count / 2];

            Console.WriteLine(result2);
            Console.ReadLine();
        }

        private static char? illegal(string line)
        {
            var pairs = new[]
            {
                new Pair
                {
                    First = '(',
                    Second = ')',
                    V = 1,
                },
                new Pair
                {
                    First = '[',
                    Second = ']',
                    V = 2,
                },
                new Pair
                {
                    First = '<',
                    Second = '>',
                    V = 4,
                },
                new Pair
                {
                    First = '{',
                    Second = '}',
                    V = 3,
                },
            };
            var stack = new List<char>();

            foreach(var c in line)
            {
                var opening = pairs.FirstOrDefault(x => x.First == c);
                if (opening != null)
                    stack.Add(c);
                else
                {
                    var closing = pairs.FirstOrDefault(x => x.Second == c);
                    if (closing != null)
                    {
                        if (stack.Count > 0 && stack.Last() == closing.First)
                            stack.RemoveAt(stack.Count - 1);
                        else
                            return c;
                    }
                }
            }
            return null;
        }

        private static decimal? autocomplete(string line)
        {
            var pairs = new[]
            {
                new Pair
                {
                    First = '(',
                    Second = ')',
                    V = 1,
                },
                new Pair
                {
                    First = '[',
                    Second = ']',
                    V = 2,
                },
                new Pair
                {
                    First = '<',
                    Second = '>',
                    V = 4,
                },
                new Pair
                {
                    First = '{',
                    Second = '}',
                    V = 3,
                },
            };
            var stack = new List<char>();

            foreach (var c in line)
            {
                var opening = pairs.FirstOrDefault(x => x.First == c);
                if (opening != null)
                    stack.Add(c);
                else
                {
                    var closing = pairs.FirstOrDefault(x => x.Second == c);
                    if (closing != null)
                    {
                        if (stack.Count > 0 && stack.Last() == closing.First)
                            stack.RemoveAt(stack.Count - 1);
                        else
                            return null;
                    }
                }
            }

            stack.Reverse();
            decimal sumInit = 0;
            return stack.Select(c => pairs.First(p => p.First == c).V).Aggregate(sumInit, (s, n) => s = s * 5 + n);
        }
    }
}
