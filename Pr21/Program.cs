using System;
using System.Collections.Generic;
using System.Linq;

namespace Pr21
{
    class Program
    {
        static void Main(string[] args)
        {
            var realities = Enumerable.Range(0, 31).Select(x => new List<Reality>()).ToArray();
            //realities[0].Add(new Reality
            //{
            //    Count = 1,
            //    FirstPos = 4,
            //    FirstSum = 0,
            //    SecondPos = 8,
            //    SecondSum = 0,
            //    IsTurnOfFirst = true,
            //});
            realities[0].Add(new Reality
            {
                Count = 1,
                FirstPos = 9,
                FirstSum = 0,
                SecondPos = 10,
                SecondSum = 0,
                IsTurnOfFirst = true,
            });

            for (var level = 0; level <= 20; level++)
            {
                var indexIntoReality = 0;
                while (indexIntoReality < realities[level].Count)
                {
                    var currentReality = realities[level][indexIntoReality++];
                    if (currentReality.SecondSum >= 21)
                        continue;
                    for (var dice1 = 1; dice1 <= 3; dice1++)
                    for (var dice2 = 1; dice2 <= 3; dice2++)
                    for (var dice3 = 1; dice3 <= 3; dice3++)
                    {
                        var newPos = currentReality.IsTurnOfFirst
                            ? div(currentReality.FirstPos + dice1 + dice2 + dice3, 10)
                            : div(currentReality.SecondPos + dice1 + dice2 + dice3, 10);
                        
                        var newReality = currentReality.IsTurnOfFirst
                            ? new Reality
                            {
                                FirstPos = newPos,
                                FirstSum = currentReality.FirstSum + newPos,
                                SecondPos = currentReality.SecondPos,
                                SecondSum = currentReality.SecondSum,
                                Count = currentReality.Count,
                                IsTurnOfFirst = !currentReality.IsTurnOfFirst,
                            }
                            : new Reality
                            {
                                FirstPos = currentReality.FirstPos,
                                FirstSum = currentReality.FirstSum,
                                SecondPos = newPos,
                                SecondSum = currentReality.SecondSum + newPos,
                                Count = currentReality.Count,
                                IsTurnOfFirst = !currentReality.IsTurnOfFirst,
                            };

                        var existing = realities[newReality.FirstSum].FirstOrDefault(x => x.IsEqual(newReality));
                        if (existing == null)
                            realities[newReality.FirstSum].Add(newReality);
                        else
                            existing.Count += newReality.Count;
                    }
                }
            }
            var levels = Enumerable.Range(21, 10).SelectMany(x => realities[x]);

            var firstWinRealities = levels
                .Where(x => !x.IsTurnOfFirst && x.SecondSum < 21)
                .Sum(x => x.Count);
            var secondWinRealities = Enumerable.Range(0, 21)
                .SelectMany(x => realities[x])
                .Where(x => x.IsTurnOfFirst && x.SecondSum >= 21)
                .Sum(x => x.Count);

            var result = Math.Max(firstWinRealities, secondWinRealities);
        }

        private static void grey()
        {
            var firstPos = 9;
            var secondPos = 10;
            //var firstPos = 4;
            //var secondPos = 8;

            var firstSum = 0;
            var secondSum = 0;
            var dice = 1;
            while (true)
            {
                firstPos += (div(dice++, 100) + div(dice++, 100) + div(dice++, 100));
                firstPos = div(firstPos, 10);
                firstSum += firstPos;
                if (firstSum >= 1000)
                    break;


                secondPos += (div(dice++, 100) + div(dice++, 100) + div(dice++, 100));
                secondPos = div(secondPos, 10);
                secondSum += secondPos;
                if (secondSum >= 1000)
                    break;
            }
            var result = (dice - 1) * Math.Min(secondSum, firstSum);
            Console.WriteLine("Hello World!");
        }

        class Reality
        {
            internal decimal Count;
            internal int FirstPos;
            internal int SecondPos;
            internal int FirstSum;
            internal int SecondSum;

            internal bool IsTurnOfFirst;

            internal bool IsEqual(Reality r) =>
                IsTurnOfFirst == r.IsTurnOfFirst &&
                FirstPos == r.FirstPos &&
                SecondPos == r.SecondPos &&
                FirstSum == r.FirstSum &&
                SecondSum == r.SecondSum; 
        }

        private static int div(int v1, int v2)
        {
            var result = v1 % v2;
            if (result == 0)
                result = v2;
            return result;
        }
    }
}
