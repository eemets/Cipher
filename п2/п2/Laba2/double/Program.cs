using System;
using System.Collections.Generic;
using System.Linq;
 
namespace TestConsole{   
    internal class CharNum{
        private char _ch;
        private int _numberInWord;
        
        public char Ch{
            get => _ch;
            set{
                if (_ch == value)
                    return;
                _ch = value;
            }
        }
        public int NumberInWord{
            get => _numberInWord;
            set{
                if (_numberInWord == value)
                    return;
                _numberInWord = value;
            }
        }
    }

    static class Program
    {
        private static int GetNumberInThealphabet(char s){
            const string str = @"АаБбВвГгДдЕеЁёЖжЗзИиЙйКкЛлМмНнОоПпРрСсТтУуФфХхЦцЧчШшЩщЪъЫыЬьЭэЮюЯя";
            var number = str.IndexOf(s) / 2;
            return number;
        }
 
        private static List<CharNum> FillListKey(char[] chars){
            var listKey = new List<CharNum>(chars.Length);
            listKey.AddRange(chars.Select(t => new CharNum(){
                Ch = t,
                NumberInWord = GetNumberInThealphabet(t)
            }));

            return listKey;
        }
 
        private static void ShowKey(List<CharNum> listCharNum, string message){
            Console.WriteLine(message);
 
            foreach (var i in listCharNum)
                Console.Write(i.Ch + " ");
            
            Console.WriteLine();
 
            foreach (var i in listCharNum)
                Console.Write(i.NumberInWord + " ");
            
            Console.WriteLine();
            Console.WriteLine();
        }
        private static List<CharNum> FillingSerialsNumber(List<CharNum> listCharNum){
            var count = 0;
            var result = listCharNum.OrderBy(a => a.NumberInWord);
 
            foreach (var i in result)
                i.NumberInWord = count++;
            
            return listCharNum;
        }

        private static void ShowMatrix(char[,] matrix, string message)
        {
            Console.WriteLine(message);
            for(var i = 0; i < matrix.GetLength(0); i++){
                for(var j = 0; j < matrix.GetLength(1); j++){
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        private static void Main()
        {  
            // Первый ключ, количество столбцов
            const string firstKey = "Сканер";
            // Второй ключ, количество строк
            const string secondKey = "Надо";
            // Предложение которое шифруем
            const string stringUser = "Системный пароль изменен";
 
            // Матрица в которой производим шифрование
            var matrix = new char[secondKey.Length, firstKey.Length];
 
            // Счетчик символов в строке
            var countSymbols = 0;
 
            // Переводим строки в массивы типа char
            var charsFirstKey = firstKey.ToCharArray();
            var charsSecondKey = secondKey.ToCharArray();
            var charStringUser = stringUser.ToCharArray();
 
            // Создаем списки в которых будут храниться символы и порядковы номера символов

            // Заполняем символами из ключей
            var listCharNumFirst = FillListKey(charsFirstKey);
            var listCharNumSecond = FillListKey(charsSecondKey);
 
            // Заполняем порядковыми номерами
            listCharNumFirst = FillingSerialsNumber(listCharNumFirst);
            listCharNumSecond = FillingSerialsNumber(listCharNumSecond);
 
            ShowKey(listCharNumFirst, "Первый ключ: ");
            ShowKey(listCharNumSecond, "Второй ключ: ");
           
            // Заполнение матрицы строкой пользователя
            for (var i = 0; i < listCharNumSecond.Count; i++){
                for(var j = 0; j < listCharNumFirst.Count; j++){
                    matrix[i, j] = charStringUser[countSymbols++];
                }
            }
 
            ShowMatrix(matrix, "Первоначальное значение: ");
          
            countSymbols = 0;
            // Заполнение матрицы с учетом шифрования. 
            // Переставляем столбцы по порядку следования в первом ключе. 
            // Затем переставляем строки по порядку следования во втором ключа. 
            foreach (var t in listCharNumSecond){
                foreach (var t1 in listCharNumFirst){
                    matrix[t.NumberInWord,
                        t1.NumberInWord] = charStringUser[countSymbols++];
                }
            }
            ShowMatrix(matrix, "Зашифрованное значение: ");
            Console.ReadKey();
        }
    }
}