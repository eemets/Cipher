using System;
using System.Collections.Generic;
using System.Text;

namespace Playfair
{
   public abstract class PlayFairSettings
   {
      private HashSet<char> HsAlphabet { get; }
      protected Dictionary<char, char> Replaces { get; }
 
      protected abstract IEnumerable<char> AlphabetChars { get; }
 
      public abstract int Columns { get; }
      public abstract int Rows { get; }
      public abstract char Replacer { get; }
      public IEnumerable<char> Alphabet
      {
         get
         {
            var chars = AlphabetChars;
            foreach (var c in chars)
               yield return c;
         }
      }

       protected PlayFairSettings()
      {
         HsAlphabet = new HashSet<char>();
         Replaces = new Dictionary<char, char>();
 
         // Переносим символы алфавита в хэштаблицу, чтобы 
         // убрать повторяющиеся значения и быстро определять
         // принадлежит символ алфавиту или нет
          if (AlphabetChars == null) return;
          foreach (var c in AlphabetChars)
              HsAlphabet.Add(c);
      }
 
      //Возвращает символ после всех преобразований
      //Если символ не принадлежит алфавиту, будет поставлен Replacer.
      public char GetChar(char Char)
      {
         Char = char.ToLower(Char);
         Char = Replaces.ContainsKey(Char) ? Replaces[Char] : Char;
         return HsAlphabet.Contains(Char) ? Char : Replacer;
      }
   }
   public class PlayFairRu56 : PlayFairSettings
   {
      public PlayFairRu56()
      {
         Replaces.Add('ё', 'е');
         Replaces.Add('й', 'и');
         Replaces.Add('ъ', 'ь');
      }
 
      protected override IEnumerable<char> AlphabetChars => "абвгдежзиклмнопрстуфхцчшщьыэюя".ToCharArray();
       public override char Replacer => 'х';
       public override int Columns => 5;
       public override int Rows => 6;
   }
   public sealed class PlayFair
{
    private struct TablePosition
    {
        public int Row;
        public int Column;
 
        public TablePosition(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }
    }

    private readonly char[,] _matrix;
    private readonly Dictionary<char, TablePosition> _positions; // Позиции символов в матрице

    private PlayFairSettings Settings { get; set; }
    private string Key { get; set; }
 
    public PlayFair(PlayFairSettings settings, string key)
    {
        Settings = settings;
        Key = key;
 
        // Формирование матрицы
        _positions = new Dictionary<char, TablePosition>();
        var items = MatrixItems().GetEnumerator();
        _matrix = new char[settings.Rows, settings.Columns];
        for (var r = 0; r < settings.Rows; r++)
        {
            for (var c = 0; c < settings.Columns; c++)
            {
                if (items.MoveNext())
                {
                    _matrix[r, c] = items.Current;
                    _positions.Add(items.Current, new TablePosition(r, c));
                }
                else throw new ArgumentException("Алфавит слишком маленький");
            }
        }
    }

    private IEnumerable<char> MatrixItems()
    {
        var used = new HashSet<char>();
 
        // Сначала пишем символы ключа
        foreach (var c in Key)
        {
            var rc = Settings.GetChar(c);
            if (used.Contains(rc)) continue;
            used.Add(rc);
            yield return rc;
        }
 
        // Теперь оставшиеся символы алфавита
        foreach (var c in Settings.Alphabet)
        {
            if (used.Contains(c)) continue;
            used.Add(c);
            yield return c;
        }
    }

    
    //Разбиаение текста на символы по биграммам.
    //eplacer выкидывается
    public IEnumerable<char> Bigrams(string text)
    {
        var prev = '\0'; // Храним перывй символ биграммы
        var even = false; // если второй символ биграммы
        foreach (var c in text)
        {
            // Преобразуем символ из текста
            // он может стать Replacer'ом, если, например, это пробел
            var rc = Settings.GetChar(c);

            if (!even) // Если это первый символ биграммы
            {
                // запоминаем и ищем второй
                prev = rc;
                even = true;
            }
            else
            {
                // Это второй символ биграммы
                if (prev == rc) // и он такойже как и первый
                {
                    if (prev == Settings.Replacer) continue;
                    // то вернем биграмму с Replacer в конце
                    yield return prev;
                    yield return Settings.Replacer;

                    // и будем считать, что уже нашли первый символ в следующей биграмме
                    prev = rc;
                }
                else
                {
                    // Ну, а если они разные, то вернем оба и будем искать дальше
                    yield return prev;
                    yield return rc;
                    even = false;
                }

            }
        }

        // Если мы ищем второй символ, а строка закончилась, 
        // при этом первый символ не Replacer
        if (!even || prev == Settings.Replacer) yield break;
        yield return prev;
        yield return Settings.Replacer;
    }

    public string Crypt(string text, bool modeCrypt = true)
        {
            var shift = modeCrypt ? 1 : -1;
            var sb = new StringBuilder();
            // Разбиваем на биграммы
            var chars = Bigrams(text).GetEnumerator();
            while (chars.MoveNext())
            {
                // Получаем координаты символов биграммы в таблице
                // При расшифровке, если шифротекст неверен 
                // т.е. имеет нечетную длину или неизвестные алфавиту символы
                // вылетит исключение (поэтому лучше раздить на два метода: шифровки и дешифровки
                // и перебрасывать исключение). Для шифровки такого не будет
                var p1 = _positions[chars.Current];
                chars.MoveNext();
                var p2 = _positions[chars.Current];
     
                // Если они на одной строке - переводим вправо
                // Если в одной колнке - вниз
                var error = 0;
                if (p1.Column == p2.Column)
                {
                    p1.Column = Mod(p1.Column + shift, Settings.Columns);
                    p2.Column = Mod(p2.Column + shift, Settings.Columns);
                    error++;
                }
                else if (p1.Row == p2.Row)
                {
                    p1.Row = Mod(p1.Row + shift, Settings.Rows);
                    p2.Row = Mod(p2.Row + shift, Settings.Rows);
                    error++;
                }
     
                if (error == 2)
                    throw new ArgumentException("Неверные биграммы");
     
                sb.Append(_matrix[p1.Row, p2.Column]);
                sb.Append(_matrix[p2.Row, p1.Column]);
            }
            return sb.ToString();
        }
     
        private static int Mod(int x, int m)
        {
            // остаток от деления для отрицательных чисел
            // для -1 вернет (m-1)
            return (x % m + m) % m;
        }
     
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("Ключ: ");
            sb.AppendLine(Key);
            for (var r = 0; r < Settings.Rows; r++)
            {
                for (var c = 0; c < Settings.Columns; c++)
                {
                    sb.Append(_matrix[r, c]);
                    sb.Append(' ');
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }

    internal static class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var ps = new PlayFairRu56();
                var pf = new PlayFair(ps, "Шифр Плейфера");
                Console.WriteLine(pf);
 
                var text = "Текст который нужно зашифровать шифром Плейферах";
                Console.WriteLine("Исходный текст\r\n" + text);
 
                var i = 0;
                foreach (var c in pf.Bigrams(text))
                {
                    Console.Write(c); 
 
                    i++;
                    if (i % 2 == 0) Console.Write(' ');
                    if (i % 10 == 0) Console.WriteLine();
                }
 
                text = pf.Crypt(text, true);
                Console.WriteLine("\r\nЗашифрованный текст\r\n" + text);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}