using System;
namespace Caesar
{
    static class Program
    {
        private static void Main(string[] args)
        {
            const int n = 1;
 
            Console.WriteLine("Введите слово,которое нужно зашифровать:");
            var s = Console.ReadLine();//храню слово, которое будем шифровать
 
            Console.WriteLine("Введите ключ:");
            var key = Convert.ToInt32(Console.ReadLine());
 
            var s1 = "";//храню результат шифрования
            const string alfphabet = "абвгґдеєжзиіїйклмнопрстуфхцчщьюя"; //украинский алфавит
            var m = alfphabet.Length; //количество знаков в алфавите

            if (s != null)
            {
                foreach (var t in s)
                {
                    for (var j = 0; j < alfphabet.Length; j++) //цикл сравнения каждой бкувы с алфавитом
                    {
                        if (t != alfphabet[j]) continue;
                        
                        var temp = j * n + key; //номер буквы+сдвиг в темп

                        while (temp >= m) //чтобы темп не уходил за рамки алфавита
                            temp -= m;

                        s1 = s1 + alfphabet[temp]; //заношу зашифрованную букву в переменную для ее хранения
                    }
                }
                Console.WriteLine("Зашифрованное слово:" + s1);
            }
            else Console.WriteLine("String is null");
            Console.ReadKey(true);
        }
    }
}