using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Pr12
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("TextFile1.txt");
            var caves = new List<Cave>();
            lines.ToList().ForEach(line =>
            {
                var conn = line.Split('-');

                var left = caves.FirstOrDefault(x => x.Name == conn[0]);
                if (left == null)
                {
                    left = new Cave
                    {
                        Name = conn[0],
                        Caves = new List<Cave>()
                    };
                    caves.Add(left);
                }

                var right = caves.FirstOrDefault(x => x.Name == conn[1]);
                if (right == null)
                {
                    right = new Cave
                    {
                        Name = conn[1],
                        Caves = new List<Cave>()
                    };
                    caves.Add(right);
                }
                left.Caves.Add(right);
                right.Caves.Add(left);
            });

            var start = caves.First(x => x.Name == "start");
            var firstWave = new[]
            {
                new Path
                {
                    Current = start,
                    PreviousPath = new [] { start }.ToList()
                }
            }.ToList();

            var isChanged = true;
            while(isChanged)
            {
                isChanged = false;
                var secondWave = new List<Path>();
                firstWave.ForEach(path =>
                {
                    if (path.Current.Name == "end")
                    {
                        secondWave.Add(path);
                        return;
                    }

                    path.Current.Caves.ForEach(goingTo =>
                    {
                        if (goingTo.Name == "start")
                            return;

                        if (path.PreviousPath.Contains(goingTo) && (path.SingleSmallCaveVisitedTwice == goingTo || path.SingleSmallCaveVisitedTwice != null))
                        {
                            return;
                        }

                        var newPath = path.PreviousPath.ToList(); // it clones
                        
                        Cave newSingleSmallCave = null;
                        
                        if (!goingTo.IsLarge)
                        {
                            if (newPath.Contains(goingTo))
                                newSingleSmallCave = goingTo;
                            else
                                newPath.Add(goingTo);
                        }

                        isChanged = true;

                        secondWave.Add(new Path
                        {
                            Current = goingTo,
                            PreviousPath = newPath,
                            SingleSmallCaveVisitedTwice = path.SingleSmallCaveVisitedTwice ?? newSingleSmallCave,
                        });
                    });
                });
                if (isChanged)
                    firstWave = secondWave;
            }
            Console.WriteLine(firstWave.Count(x => x.Current.Name == "end"));
            Console.ReadLine();
        }
    }

    class Cave
    {
        internal string Name;
        internal bool IsLarge => Name == Name.ToUpper();
        public List<Cave> Caves { get; internal set; }
    }

    class Path
    {
        internal List<Cave> PreviousPath;
        internal Cave Current;
        internal Cave SingleSmallCaveVisitedTwice;
        public override string ToString()
        {
            return $"{Current.Name} {string.Join('|', PreviousPath.Select(x => x.Name).ToArray())}";
        }
    }
}
