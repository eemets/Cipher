using System;
using System.Linq;

namespace TrisemusSystem
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var alphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ ,.".ToCharArray();
 
            // Пытаемся вычислить размерность таблицы
            Console.WriteLine("Символов в алфавите: " + alphabet.Length);
            int rows = 0, columns;
            bool isValidTable;
            
            do
            {
                Console.Write("Количество колонок в таблице: ");
                isValidTable = int.TryParse(Console.ReadLine(), out columns) && columns > 1;
                if (!isValidTable)
                {
                    Console.WriteLine("Необходимо ввести число больше 1");
                }
                else
                {
                    rows = alphabet.Length / columns;
                    isValidTable &= rows > 1 && rows * columns == alphabet.Length;
                    if (!isValidTable)
                    {
                        Console.WriteLine("Необходимо ввести число колонок таким образом, чтобы число строк таблицы " +
                                          "было больше 1 и таблица могла вмещать в себе все символы алфавита");
                    }
                }
            }
            while (!isValidTable);
             
            // Пытаемся получить ключевое слово
            char[] keyWord;
            bool isValidKeyWord;
            do
            {
                Console.Write("Введите ключевое слово: ");
                keyWord = Console.ReadLine()?.ToUpper().Distinct().ToArray();
                isValidKeyWord = keyWord.Length > 0 && keyWord.Length <= alphabet.Length;
                if (!isValidKeyWord)
                {
                    Console.WriteLine("Ключевое слово не может быть пустой строкой или содержать число уникальных символов больше размера алфавита");
                }
                else
                {
                    isValidKeyWord &= !keyWord.Except(alphabet).Any();
                    if (!isValidKeyWord)
                    {
                        Console.WriteLine("Ключевое слово не может содержать символы, которых нет в алфавите");
                    }
                }
            }
            while (!isValidKeyWord);
             
            // Создаем таблицу
            var table = new char[rows, columns];
             
            // Вписываем в нее ключевое слово
            for (var i = 0; i < keyWord.Length; i++)
            {
                table[i / columns, i % columns] = keyWord[i];
            }
             
            // Исключаем уникальные символы ключевого слова из алфавита
            alphabet = alphabet.Except(keyWord).ToArray();
             
            // Вписываем алфавит
            for (var i = 0; i < alphabet.Length; i++)
            {
                var position = i + keyWord.Length;
                table[position / columns, position % columns] = alphabet[i];
            }
             
            // Получаем сообщение, которое необходимо зашифровать
            string message;
            bool isValidMessage;
            do
            {
                Console.Write("Введите сообщение: ");
                message = Console.ReadLine()?.ToUpper();
                isValidMessage = !string.IsNullOrEmpty(message);
                if (!isValidMessage)
                {
                    Console.WriteLine("Сообщение не может быть пустой строкой");
                }
            }
            while (!isValidMessage);
             
            // Создаем место для будущего зашифрованного сообщения
            var result = new char[message.Length];
             
            // Шифруем сообщение
            for (var k = 0; k < message.Length; k++)
            {
                var symbol = message[k];
                // Пытаемся найти символ в таблице
                for (var i = 0; i < rows; i++)
                {
                    for (var j = 0; j < columns; j++)
                    {
                        if (symbol != table[i, j]) continue;
                        symbol = table[(i + 1) % rows, j]; // Смещаемся циклически на следующую строку таблицы и запоминаем новый символ
                        i = rows; // Завершаем цикл по строкам
                        break; // Завершаем цикл по колонкам
                    }
                }
                // Записываем найденный символ
                result[k] = symbol;
            }
             
            // Выводим зашифрованное сообщение
            Console.WriteLine("Зашифрованное сообщение: " + new string(result));
            Console.ReadKey(true);
        }
    }
}