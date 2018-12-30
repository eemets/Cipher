using System;

namespace Gamma{
    internal static class Xor{
        private const string Alf = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя 0123456789";
        private static int _k, _x, _z;
        private static string _res;

        public static string Encryption(string source, string key){
            _res = string.Empty;

            while (key.Length < source.Length){
                key += key;
                if (key.Length > source.Length) key = key.Remove(source.Length);
            }

            for (var i = 0; i < source.Length; i++){
                for (var id = 0; id < Alf.Length; id++){
                    if (key[i] == Alf[id]) _k = id;
                    if (source[i] == Alf[id]) _x = id;
                    _z = (_x + _k) % Alf.Length;
                }

                _res += Alf[_z];
            }
            return _res;
        }

        public static string Decryption(string source, string key){
            _res = string.Empty;

            while (key.Length < source.Length){
                key += key;
                if (key.Length > source.Length) key = key.Remove(source.Length);
            }

            for (var i = 0; i < source.Length; i++){
                for (var id = 0; id < Alf.Length; id++){
                    if (key[i] == Alf[id]) _k = id;
                    if (source[i] == Alf[id]) _x = id;
                    _z = ((source[i] - key[i]) + Alf.Length) % Alf.Length;
                }
                _res += Alf[_z];
            }
            return _res;
        }
    }

    internal static class Main1{
        public static void Main(string[] args){
            Console.Write("Enter text\t");
            var text = Console.ReadLine();
            Console.Write("Enter key(str)\t");
            var key = Console.ReadLine();

            var crypt = Xor.Encryption(text, key);
            Console.WriteLine("Crypt\t{0}",crypt);
        }
    }
}