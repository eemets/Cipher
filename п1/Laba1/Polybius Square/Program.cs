using System;

namespace Polybius_Square{
    internal static class Program{
        private static void Main(){
            const string randomLine = "ЙЦУКЕНГШЩЗХФЪЫВАПРОЛДЖЭЧСМИТЬБЮ";
            var rand = new Random();
            
            Console.WriteLine("Введите сообщение:");
            var line = Console.ReadLine()?.ToUpper().Replace(" ", "");
            
            if (line != null)
            {
                var d = (int)Math.Ceiling(Math.Sqrt(line.Length));
                if (d % 2 != 1)
                    d++;
                Console.WriteLine("Квадрат: " + d.ToString());
                
                var quad = new int[d, d];
                for (var j = 0; j < d; j++){ 
                    for (var i = 0; i < d; i++){
                        quad[i, j] = d*(((i + 1) + (j + 1) - 1 + (d / 2)) % d)+(((i+1) + 2*(j+1) - 2) % d) + 1;
                        Console.Write(quad[i, j].ToString() + "\t");
                    }
                    Console.WriteLine();
                }
                
                Console.WriteLine("Шифрование сообщения:");
                var cryptedString = "";
                for (var j = 0; j < d; j++){ 
                    for (var i = 0; i < d; i++){ 
                        if ((quad[i, j] - 1) < line.Length){
                            Console.Write(line[quad[i, j] - 1] + "\t");
                            cryptedString += line[quad[i, j] - 1];                    }
                        else{
                            var randomChar = randomLine[rand.Next(0, randomLine.Length - 1)];
                            Console.Write(randomChar + "\t");
                            cryptedString += randomChar;
                        }                
                    }
                    Console.WriteLine();
                }
                
                Console.WriteLine("\nЗашифрованная строка:");
                Console.WriteLine(cryptedString);
            }
            Console.ReadKey();
        }    
    }    
}