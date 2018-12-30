import javafx.util.Pair;

import java.io.Console;
import java.math.BigInteger;
import java.util.Random;

public class ShnorrAlgorithm {

        private static Random rnd = new Random();

        public static int[] GetShnorrValues()
        {
            int p = 31;
            int q = 11;

            int x = 8;

            int g = 3;

            int y = 4;
            System.out.println("y: " + y);

            return new int[] { p, q, x, g, y};
        }

        public static Pair<Integer, Integer> GetShnorrValuesKandR(int p, int q, int g)
        {
            int k = 12;//rnd.Next(1, q - 1);
            System.out.println(("k: " + k));
            BigInteger r = BigInteger.valueOf(BigInteger.valueOf(g).pow(k).intValue() % p);
            System.out.println("r: " + r);

            return new Pair<>(r.intValue(), k);
        }

        public static int GetShnorrValueE()
        {
            final int t = 8;
            int e = 4;//rnd.Next(0, (int)Math.Pow(2, t) - 1);
            System.out.println("e: " + e);

            return e;
        }

        public static int GetShnorrValueS(int k, int x, int e, int q)
        {
            int s = (k + x * e) % q;
            System.out.println("s: " + s);

            return s;
        }

        public static boolean AuthenticationIsValid(int r, int p, int g, int s, int y, int e)
        {
            BigInteger a = BigInteger.valueOf(g).pow(s);
            BigInteger b = BigInteger.valueOf(y).pow(e);
            BigInteger result = BigInteger.valueOf(a.multiply(b).intValue() % p);

            return r == result.intValue();
        }

}
