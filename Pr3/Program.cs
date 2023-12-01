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
            var SIZE = s.First().Length;
            /*
            var zeroes = new int[SIZE];
            var ones = new int[SIZE];
            s.ToList().ForEach(line =>
            {
                var index = 0;
                line.ToList().ForEach(c =>
                {
                    if (c == '0')
                    {
                        zeroes[index]++;
                    }
                    else
                    {
                        ones[index]++;
                    }
                    index++;
                });
            });
            var gamma = 0;
            var epsilon = 0;
            Enumerable.Range(0, SIZE).Select(i => ones[i] < zeroes[i]).ToList().ForEach(b =>
            {
                gamma *= 2;
                epsilon *= 2;
                if (b)
                    gamma++;
                else
                    epsilon++;
            });
            */

            var all = (string[])s.Clone();
            var a2ll = (string[])s.Clone();

            for (var i = 0; i < SIZE; i++)
            {
                if (all.Length > 1)
                {
                    var countOfOnes = all.Count(x => x[i] == '1');
                    var shouldBeOne = countOfOnes >= (all.Length - countOfOnes);
                    var criteria = shouldBeOne ? '1' : '0';
                    all = all.Where(x => x[i] == criteria).ToArray();
                }

                if (a2ll.Length > 1)
                {
                    var c2ountOfOnes = a2ll.Count(x => x[i] == '1');
                    var s2houldBeOne = c2ountOfOnes < (a2ll.Length - c2ountOfOnes);
                    var c2riteria = s2houldBeOne ? '1' : '0';
                    a2ll = a2ll.Where(x => x[i] == c2riteria).ToArray();
                }
            }

            var gamma = 0;
            var epsilon = 0;
            Enumerable.Range(0, SIZE).ToList().ForEach(i =>
            {
                gamma *= 2;
                epsilon *= 2;
                if (all.First()[i] == '1')
                    gamma++;
                if (a2ll.First()[i] == '1')
                    epsilon++;
            });
            Console.WriteLine(gamma * epsilon);
            Console.ReadLine();
        }
    }
}
