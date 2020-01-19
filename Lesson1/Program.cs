using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using PL.Sorting;

using static PL.Console.Out;

namespace Lesson1
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            int[] a = new int[10];
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = rnd.Next(1, 100);
                Console.WriteLine($"A[{i}] = {a[i]}");
            }
            Console.WriteLine(a[0]);
        }
    }
}





















// while (true)
//            {
//                string mac = Console.ReadLine();

//Console.WriteLine(
//        Regex.IsMatch(mac, @"((([0-9a-f]{2})|([0-9A-F]{2})|(([A-F])([a-f])))([:]|$)){6}$") ? "верен" : "не верен"
//                        );
//            }