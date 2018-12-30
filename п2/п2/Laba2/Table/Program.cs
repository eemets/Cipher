using System;
using System.Linq;
using static System.String;

namespace Table{
    internal class Program{
        private static string Encrypt(string text, int[] key){
            var newText = Empty;
            for (var i = 0; i < text.Length-1; i += 1){
                var index = key[i] - 1;
                if (i >= 0 && index >= 0) newText += text[index];
            }
            return newText;
        }
        
        public static void Main(string[] args){
            Console.WriteLine("Enter string\t");
            var text = Console.ReadLine();
            if (text == null) return;
            var key = new int[text.Length];

            for (var i = 0; i < text.Length; i += 1)
                key[i] = i;
            var rnd = new Random();
            key = key.OrderBy(x => rnd.Next()).ToArray();
            
            Console.WriteLine("Coded text\t{0}", Encrypt(text, key));
        }
    }
}