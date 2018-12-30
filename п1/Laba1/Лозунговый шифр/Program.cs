using System;
using System.Linq;
using System.Text;

namespace Лозунговый_шифр
{
    internal class SloganCipher
    {
        private char[] _table;
        private SloganCipher(string slogan)
        {
            Slogan = slogan ?? throw new ArgumentNullException(nameof(slogan));
            CreateTable();
        }
        private string Slogan
        {
            get;
        }
        private string Cipher(string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;
            var buffer = new StringBuilder(text.Length);
            foreach (var letter in text)
                buffer.Append(IsRussianLetter(letter) ? _table[letter - 'а'] : letter);
            return buffer.ToString();
        }
        private void CreateTable()
        {
            _table = Slogan.Where(IsRussianLetter).Distinct().Concat("абвгдежзиклмнопрстуфхцчшщыьъэюя".Except(Slogan)).ToArray();
        }
        private static bool IsRussianLetter(char character)
        {
            return char.IsLetter(character) && character >= 'а' && character <= 'я';
        }
    
        static void Main()
        {
            Console.Write("Лозунг: ");
            var slogan = Console.ReadLine();
 
            var cipher = new SloganCipher(slogan);
 
            Console.Write("Текст для шифрования: ");
            var text = Console.ReadLine();
 
            var cipheredText = cipher.Cipher(text);
            Console.WriteLine("Зашифрованный текст: {0}", cipheredText);

            Console.ReadKey(true);
        }
    }
}