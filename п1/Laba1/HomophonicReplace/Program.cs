using System;
namespace HomophonicReplace
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter word:");
            var s1 = Console.ReadLine();
            var s2 = string.Empty;
            if (s1 != null)
                foreach (var t in s1)
                {
                    if (t != ' ')
                        if (t == 1071)
                            s2 += (char) (1103);
                        else if (t == 1040)
                            s2 += (char) (1071);
                        else
                            s2 += (char) (t - 1);
                    else
                        s2 += t;
                }

            Console.WriteLine("Replaced word {0}",s2);
            Console.ReadKey(true);
        }
    }
}