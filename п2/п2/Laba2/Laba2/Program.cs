using System;
using System.Collections.Generic;
using System.Linq;

namespace Laba2
{
    internal class Program
    {
        private static int state = 1;
        private static readonly int[] K = {2,5,4,3,1};
        private static readonly int[] Key = K;
        
        private static int[] Getunkey (IReadOnlyList<int> k)
        {
            var res = new int[k.Count];
            for (var i = 0; i < k.Count; i++)
            {
                var ki = k[i];
                ki--;
                res[ki] = i + 1;
            }
            return res;
        }
        
        private static string Crypt (string msg,bool f)
        {
            var k1 = f ? Key : Getunkey (Key);
            var res = "";
 
            for (var i = 0; i < (1+(msg.Length/k1.Length)); i++)
            {
                if ((i + 1) * k1.Length > msg.Length)
                {
                    var r = i * k1.Length;
                    r = msg.Length - r;
                    return res += msg.Substring(i * k1.Length, r);
                }

                var tmp1 = msg.Substring (i * k1.Length, k1.Length);
                var tmp2 = k1.Select(t => t - 1).Aggregate("", (current, ki) => current + tmp1.Substring(ki, 1));

                res += tmp2;
            }
            return res;
        }
        
        
        public static void Main(string[] args)
        {
            Console.Write("Enter message\t");
            var message = Console.ReadLine();
            Console.WriteLine(Crypt(message, true));
            Console.ReadKey(true);
        }
    }
}