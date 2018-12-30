using System;
using System.Linq;

namespace Gamma2{
    internal static class Program{
        private const string alphabet = "qwertyasdfghjklzxcvbnm 0123456789";

        private static string Crypt(string key, string text){
            var alphaindex = new int[alphabet.Length];
            for(var i = 0;i< alphabet.Length;i++)//расставляем индексы для букв в алфавите
                alphaindex[i] = i;
            
            var newtext = string.Empty;//выходящий текст
            var ikey = 1 + key.Sum(t => alphaindex[alphabet.IndexOf(alphabet, t)]);//Значение для перевода словесного ключа в число
            var random = new Random();
            var lol = new int[text.Length];//массив случайных значений по длине текста
            for (var i = 0; i < text.Length; i++)
                lol[i] = random.Next(ikey);
            for (var i = 0;i<text.Length;i++)
                newtext += alphabet[(alphaindex[alphabet.IndexOf(alphabet, text[i])] ^ lol[i])%alphabet.Length];
            return newtext;
        }
        
        public static void Main(string[] args){
            Console.Write("Enter key(str)\t");
            var key = (Console.ReadLine());
            
            Console.Write("Enter text\t");
            var text = Console.ReadLine();
            
            Console.WriteLine("Coded text\t{0}",Crypt(key, text));
        }
    }
}