using System;
using static System.String;

namespace Simple
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var massiv = new char[100,100];
            int c, count = 0;
            
            Console.WriteLine("Source word\t");
            var s = Console.ReadLine()?.ToCharArray();
            Console.WriteLine("Enter key\t");
            var b = c = Convert.ToInt32(Console.ReadLine());
            
            for(var i=0; i<b; i++){
                for(var j=0; j<c; j++){
                    if (s != null) massiv[i, j] = s[count];
                    count++;
                }
            }
            
            var result = Empty;
            for(var j=0; j<c; j++){
                for(var i=0; i<b; i++){
                    if(massiv[i, j] >= 'a' && massiv[i, j] <= 'z')
                        result+=(massiv[i, j]);
                }
            }
            Console.WriteLine("Compiled word\t{0}", result);
        }
    }
}