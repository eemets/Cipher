using System;
using System.Linq;

namespace table90
{
	public class DoTran
	{
		public readonly string Msg;
		public readonly string Key1;
		public readonly string Key2;
		public string Enc1;
		public string Enc2 { get; set; }
		public string Decy2;
		public readonly int[] Ord1;
		public readonly int[] Ord2;
		public readonly int L1;
		public readonly int l2;

		public DoTran(string s, string k1, string k2)
		{
			Msg = s;
            			Key1 = k1;
            			Key2 = k2;
			L1 = k1.Length;
            			l2 = k2.Length;
            			Ord1 = new int[L1];
            			Ord2 = new int[l2];
		}

		public void genKey(string s, int[] p)
		{
			int k = 1;
			bool[] v = new bool[s.Length];
			for (int i = 0;i < s.Length;i++)
			{
				v[i] = false;
			}

			string t = s;
			s.OrderBy(x => s.GetEnumerator());
			foreach (var t1 in s)
			{
				for (var j = 0;j < s.Length;j++)
				{
					if (t1 == t[j] && v[j] == false)
					{
						p[j] = k;
					}
						
					v[j] = true;
					k++;}
			}
			Console.Write("\n");
		}
		public string encrypt(string s, int[] ord, int k)
		{
			string str = String.Empty;
			int c = 1;
			int f = 0;
			int l = s.Length / k;
			char p;
			for (int i = 0;i < k;i++)
			{
				for (int j = 0;j < k;j++)
				{
					if (c == ord[j])
					{
						f = j;
					}
						   
					j = k;
					c++;
				}
				for (int j = 0;j < l + 1;j++)
				{
					if (j * k + f < s.Length)
					{
						p = s[j * k + f];
					}
					else
					{
						p = 'x';
					}

					str = str + p;
				}
			}
			Console.Write(str);
			Console.Write("\n");
			return str;
		}
		public void printMat(string s, int[] ord, int l)
		{
			int z = s.Length / l;
			z = (z + 1) * l;
			for (int i = 0;i < l;i++)
			{
				Console.Write("--");
			}

			Console.Write("\n");
			for (int i = 0;i < l;i++)
			{
				Console.Write(ord[i]);
				Console.Write(" ");
			}

			Console.Write("\n");
			for (int i = 0;i < l;i++)
			{
				Console.Write("--");
			}

			Console.Write("\n");
			for (int i = 0;i < z;i++)
			{
				if (i < s.Length)
				{
					Console.Write(s[i]);
				}
				else
				{
					Console.Write("x");
				}
				if ((i + 1) % l == 0)
				{
					Console.Write("\n");
				}
				else
				{
					Console.Write(" ");
				}
			}
			for (int i = 0;i < l;i++)
			{
				Console.Write("--");
			}
			Console.Write("\n");
		}
	}

	public static class GlobalMembers
	{
		static void Main()
		{
			Console.Write("Enter The Message : ");
			Console.Write("\n");
			var s = Console.ReadLine();
			Console.Write("Enter The First Key : ");
			Console.Write("\n");
			var k1 = Console.ReadLine();
			Console.Write("Enter The Second Key : ");
			Console.Write("\n");
			var k2 = Console.ReadLine();
			DoTran d = new DoTran(s,k1,k2);

			Console.Write("\nGenerating First Key : ");
			d.genKey(d.Key1,d.Ord1);
			d.printMat(d.Msg,d.Ord1,d.L1);
			Console.Write("Message After Single Transposition : ");
			Console.Write("\n");
			d.Enc1 = d.encrypt(d.Msg,d.Ord1,d.L1);
			Console.Write("\nGenerating Second Key : ");
			d.genKey(d.Key2,d.Ord2);
			d.printMat(d.Enc1,d.Ord2,d.l2);

			Console.Write("Message After Double Transposition : ");
			Console.Write("\n");
			d.Enc2 = d.encrypt(d.Enc1,d.Ord2,d.l2);

		}
	}
}