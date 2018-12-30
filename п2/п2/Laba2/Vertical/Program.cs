using System;
using System.Diagnostics;
using System.Linq;
 
namespace Vertical{
    internal class Vertical{
        public static void Main(string[] args){
            Console.Write("Enter source word\t");
            var text = Console.ReadLine();
            
            int k = text.Length;
            string tmp = String.Empty;
            string[,] array = new string[k/2, k/2];
            int q = 0;
            for (int i = 0; i < k/2; i++)
            for (int j = 0; j < k/2; j++)
            {
                if (q < text.Length)
 
                    array[i, j] = Convert.ToString(text[q++]);
                else if (q <= text.Length && j == 0)
                    break;
                else array[i, j] = "-";
            }
 
            for (int i = 0; i < k/2; i++)
            {
                for (int j = 0; j < k/2; j++)
                    tmp += array[i, j];
                tmp +="\n";
            }
 
 
            var result = string.Empty;
            Console.WriteLine("Enter key\t");
            var key = Console.ReadLine();
            for (var i = 0; i < k/2; i++)
                for (var j = 0; j < k/2; j++)
                    if (i == (Convert.ToInt64(key[j])))
                        for (var t = 0; t < 7; t++){
                            result += array[t, j];
                            Debug.Write(array[t, j]);
            }
            Console.WriteLine(result);
            
            Console.ReadKey(true);
        }
    }
}