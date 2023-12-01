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
            var s = File.ReadAllLines("TextFile1.txt");
            
            var initial = s.First();
            var firstWave = new Dictionary<string, decimal>();
            for (var i = 0; i < initial.Length - 1; i++)
            {
                var key = "" + initial[i] + initial[i + 1];

                if (!firstWave.ContainsKey(key))
                    firstWave[key] = 0;

                firstWave[key] += 1;
            }

            var templates = s.Skip(2).Select(line =>
            {
                var components = line.Split(" -> ");
                return new Rule
                {
                    From = components.First(),
                    To = components.Skip(1).First(),
                };
            }).ToList();
            templates.ForEach(template =>
            {
                var left = "" + template.From[0] + template.To;
                var right = template.To + template.From[1];

                template.Left = templates.First(x => x.From == left);
                template.Right = templates.First(x => x.From == right);
            });

            Enumerable.Range(0, 40).ToList().ForEach(day =>
            {
                var secondWave = new Dictionary<string, decimal>();
                firstWave.Keys.Cast<string>().ToList().ForEach(key =>
                {
                    var template = templates.First(x => x.From == key);
                    if (!secondWave.ContainsKey(template.Left.From))
                        secondWave[template.Left.From] = 0;

                    if (!secondWave.ContainsKey(template.Right.From))
                        secondWave[template.Right.From] = 0;

                    secondWave[template.Left.From] += firstWave[key];
                    secondWave[template.Right.From] += firstWave[key];
                });
                firstWave = secondWave;
            });

            var lettersCount = new Dictionary<char, decimal>();
            firstWave.Keys.Cast<string>().ToList().ForEach(key =>
            {
                if (!lettersCount.ContainsKey(key[0]))
                    lettersCount[key[0]] = 0;
                if (!lettersCount.ContainsKey(key[1]))
                    lettersCount[key[1]] = 0;

                lettersCount[key[0]] += firstWave[key];
                lettersCount[key[1]] += firstWave[key];
            });
            lettersCount[initial[0]] += 1;
            lettersCount[initial[initial.Length - 1]] += 1;

            var counts = lettersCount.Values.Cast<decimal>().Select(x => x / 2).ToList();
            var result = counts.Max() - counts.Min();
            Console.ReadLine();

        }

    internal class Rule
    {
        public string From { get; set; }
        public string To { get; set; }
            public Rule Left { get; internal set; }
            public Rule Right { get; internal set; }
        }
}
}
