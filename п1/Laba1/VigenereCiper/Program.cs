using System;

namespace VigenereCiper
{
    internal static class Program
    {
        private static readonly char[] Characters = { 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И',
                                                      'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 
                                                      'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ь', 'Ы', 'Ъ',
                                                      'Э', 'Ю', 'Я'};
            /*{'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 
                'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 
                'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};*/
        
        private static readonly int N  = Characters.Length;
        
        private static string Encode(string input, string keyword)
        {
            input = input.ToUpper();
            keyword = keyword.ToUpper();
 
            var result = "";
            var keywordIndex = 0;
 
            foreach (var symbol in input)
            {
                var c = (Array.IndexOf(Characters, symbol) +
                         Array.IndexOf(Characters, keyword[keywordIndex])) % N;
 
                result += Characters[c];
 
                keywordIndex++;
 
                if ((keywordIndex + 1) == keyword.Length)
                    keywordIndex = 0;
            }
            return result;
        }
        
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter word to encode:");
            var inputEncode = Console.ReadLine();
            Console.WriteLine("Enter keyword:");
            var key = Console.ReadLine();
            Console.WriteLine("Зашифрованное слово имеет вид {0}",Encode(inputEncode, key));
            Console.ReadKey(true);

        }
    }
}