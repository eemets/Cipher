using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADFGVX{
    public class Adfgvx{
        private readonly string _key;
        public Adfgvx(string key){
            _key = key;
        }
        public string DecryptMessage(string cipherText){
            var splitCipher = cipherText.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            var transformedCipher = TransformCipher(splitCipher);
            var returnText = ReverseStepOne(transformedCipher);
            return returnText.Replace("SPACE", " ");
        }
        private string ReverseStepOne(string transformedCipher){
            var sb = new StringBuilder();
            for (var i = 1; i < transformedCipher.Length; i += 2)
                sb.Append(GetPlainText(transformedCipher[i - 1], transformedCipher[i]));
            return sb.ToString();
        }
        private static string GetPlainText(char c, char c1){
            var sb = new StringBuilder();
            var sb2 = new StringBuilder();
            sb2.Append(c);
            sb2.Append(c1);
            switch (sb2.ToString()){
                case "AA":
                    sb.Append("F");
                    break;
                case "AD":
                    sb.Append("U");
                    break;
                case "AF":
                    sb.Append("W");
                    break;
                case "AG":
                    sb.Append("5");
                    break;
                case "AV":
                    sb.Append("Q");
                    break;
                case "AX":
                    sb.Append("G");
                    break;
                case "DA":
                    sb.Append("M");
                    break;
                case "DD":
                    sb.Append("E");
                    break;
                case "DF":
                    sb.Append("V");
                    break;
                case "DG":
                    sb.Append("R");
                    break;
                case "DV":
                    sb.Append("H");
                    break;
                case "DX":
                    sb.Append("0");
                    break;
                case "FA":
                    sb.Append("1");
                    break;
                case "FD":
                    sb.Append("N");
                    break;
                case "FF":
                    sb.Append("D");
                    break;
                case "FG":
                    sb.Append("I");
                    break;
                case "FV":
                    sb.Append("6");
                    break;
                case "FX":
                    sb.Append("7");
                    break;
                case "GA":
                    sb.Append("2");
                    break;
                case "GD":
                    sb.Append("S");
                    break;
                case "GF":
                    sb.Append("J");
                    break;
                case "GG":
                    sb.Append("C");
                    break;
                case "GV":
                    sb.Append("X");
                    break;
                case "GX":
                    sb.Append("8");
                    break;
                case "VA":
                    sb.Append("T");
                    break;
                case "VD":
                    sb.Append("K");
                    break;
                case "VF":
                    sb.Append("#");
                    break;
                case "VG":
                    sb.Append("O");
                    break;
                case "VV":
                    sb.Append("B");
                    break;
                case "VX":
                    sb.Append("Y");
                    break;
                case "XA":
                    sb.Append("L");
                    break;
                case "XD":
                    sb.Append("Z");
                    break;
                case "XF":
                    sb.Append("4");
                    break;
                case "XG":
                    sb.Append("9");
                    break;
                case "XV":
                    sb.Append("P");
                    break;
                case "XX":
                    sb.Append("A");
                    break;
            }
            return sb.ToString();
        }
        private string TransformCipher(string[] splitCipher){
            var alphabeticalString = _key.ToList();
            alphabeticalString.Sort();

            var columnTransposition = _key.ToDictionary(c => c, c => new List<char>());
            var counter = 0;
            foreach (var c in alphabeticalString){
                columnTransposition[c].AddRange(splitCipher[counter].ToList());
                counter++;
            }
            var sb = new StringBuilder();
            var longestLength = GetLongestLength(splitCipher);
            for (var i = 0; i < longestLength; i++){
                foreach (var c in _key){
                    if (columnTransposition[c].Count == 0) continue;
                    sb.Append(columnTransposition[c].FirstOrDefault());
                    columnTransposition[c].RemoveAt(0);
                }
            }
            return sb.ToString();
        }
        private static int GetLongestLength(string[] splitCipher){
            var largestLength = int.MinValue;
            foreach (var each in splitCipher){
                if (each.Length > largestLength)
                    largestLength = each.Length;
            }
            return largestLength;
        }
        public string EncryptMessage(string message){
            message = message.Replace(" ", "SPACE");
            var stepOne = StepOne(message.ToLower());

            var returnText = OutputText(stepOne);
            return returnText;
        }
        public string CleanMessage(string message)
        {
            var sb = new StringBuilder();
            foreach (var c in message){
                switch (c){
                    case '\r':
                    case '\n':
                    case '\t':
                    case ' ':
                        continue;
                    default:
                        sb.Append(c);
                        break;
                }
            }
            return sb.ToString().ToLower();
        }
        private string OutputText(string message){
            var counter = 0;
            var columnTransposition = _key.ToDictionary(c => c, c => new List<char>());
            foreach (var each in message){
                columnTransposition[_key[counter]].Add(each);
                counter++;
                if (counter >= _key.Length)
                    counter = 0;
            }

            var sb = new StringBuilder();
            var alphabeticalKey = _key.ToList();
            alphabeticalKey.Sort();
            foreach (var c in alphabeticalKey){
                foreach (var each in columnTransposition[c])
                    sb.Append(each);
                sb.Append(' ');
            }
            return sb.ToString();
        }
        private static string StepOne(string cleanedMessage)
        {
            var sb = new StringBuilder();
            foreach (var c in cleanedMessage){
                switch (c)
                {
                    case 'a':
                        sb.Append("XX");
                        break;
                    case 'b':
                        sb.Append("VV");
                        break;
                    case 'c':
                        sb.Append("GG");
                        break;
                    case 'd':
                        sb.Append("FF");
                        break;
                    case 'e':
                        sb.Append("DD");
                        break;
                    case 'f':
                        sb.Append("AA");
                        break;
                    case 'g':
                        sb.Append("AX");
                        break;
                    case 'h':
                        sb.Append("DV");
                        break;
                    case 'i':
                        sb.Append("FG");
                        break;
                    case 'j':
                        sb.Append("GF");
                        break;
                    case 'k':
                        sb.Append("VD");
                        break;
                    case 'l':
                        sb.Append("XA");
                        break;
                    case 'm':
                        sb.Append("DA");
                        break;
                    case 'n':
                        sb.Append("FD");
                        break;
                    case 'o':
                        sb.Append("VG");
                        break;
                    case 'p':
                        sb.Append("XV");
                        break;
                    case 'q':
                        sb.Append("AV");
                        break;
                    case 'r':
                        sb.Append("DG");
                        break;
                    case 's':
                        sb.Append("GD");
                        break;
                    case 't':
                        sb.Append("VA");
                        break;
                    case 'u':
                        sb.Append("AD");
                        break;
                    case 'v':
                        sb.Append("DF");
                        break;
                    case 'w':
                        sb.Append("AF");
                        break;
                    case 'x':
                        sb.Append("GV");
                        break;
                    case 'y':
                        sb.Append("VX");
                        break;
                    case 'z':
                        sb.Append("XD");
                        break;
                    case '0':
                        sb.Append("DX");
                        break;
                    case '1':
                        sb.Append("FA");
                        break;
                    case '2':
                        sb.Append("GA");
                        break;
                    case '3':
                        sb.Append("VF");
                        break;
                    case '4':
                        sb.Append("XF");
                        break;
                    case '5':
                        sb.Append("AG");
                        break;
                    case '6':
                        sb.Append("FV");
                        break;
                    case '7':
                        sb.Append("FX");
                        break;
                    case '8':
                        sb.Append("GX");
                        break;
                    case '9':
                        sb.Append("XG");
                        break;
                }
            }
            return sb.ToString();
        }
    }
    public class mainProg
    {
        public static void Main(string[] Args)
        {
            Console.WriteLine("Enter key\t");
            var code = new Adfgvx(Console.ReadLine());
            Console.WriteLine("Enter message\t");
            var message = Console.ReadLine();
            Console.Write("Cleaned text\t{0}\n", code.CleanMessage(message));
            var encrypt = code.EncryptMessage(message);
            Console.Write("Coded text\t{0}\n", encrypt);
            Console.Write("Decrypted text\t{0}", code.DecryptMessage(encrypt));
        }
    }
}