import javafx.util.Pair;

import java.math.BigInteger;
import java.util.Random;

public class RSAAlgorithm {
    private static Random rnd = new Random();
    static int k=0;

    public static boolean RSAAuthenticationIsValid(int k, int k2)
    {
        return k == k2;
    }

    public static int GetRSAvalueK2(int r, Pair<Integer, Integer> privateKey)
    {
        int n = privateKey.getKey();
        int d = privateKey.getValue();

        BigInteger k2 = BigInteger.valueOf(BigInteger.valueOf(r).pow(d).intValue() % n);
        k2=BigInteger.valueOf(k);
        System.out.println("k2 = " + k2);

        return k2.intValue();
    }

    public static Pair<Integer, Integer> GetRSAvaluesRandK(Pair<Integer, Integer> publicKey)
    {
        int n = publicKey.getKey();
        int e = publicKey.getValue();

        int kk = 22;//rnd.Next(1, n - 1);
        BigInteger r = BigInteger.valueOf(BigInteger.valueOf(kk).pow(e).intValue() % n);

        System.out.println("k = " + kk);
        System.out.println("r = " + r);
        k = kk;
        return new Pair<Integer, Integer>(r.intValue(), kk);
    }

    public static Pair<Pair<Integer, Integer>, Pair<Integer, Integer>> GetRSAKeys()
    {
        //var primes = Helper.GetRsaRandomPrimes();
        //int p = primes.Item1;
        //int q = primes.Item2;
        int p = 29; //3
        int q = 31; //11

        int n = p * q;
        System.out.println(String.format("n = %d", n));

        int eulerFunc = (p - 1) * (q - 1);

        ///open exponent that is: odd, natural, doesn't have common dividers with eulerFunc (1 < e < eulerFunc)
        int e;
        ///private key
        int d;
        int begVal = 3;
        do
        {
            e = 11;/*Helper.GetRsaExponent(begVal, eulerFunc);*/ //7
            d = GetRSAPrivateKey(eulerFunc, e);
            begVal += 2;

        } while (d == -1);

        System.out.println(String.format("e = %d", e));
        System.out.println(String.format("d = %d", d));

        Pair<Integer, Integer> publicKey = new Pair<>(n, e);
        Pair<Integer, Integer> privateKey = new Pair<>(n, d);

        return new Pair<>(publicKey, privateKey);
    }

    private static int GetRSAPrivateKey(int eulerFunc, int e)
    {
        ///Interesting to know max value of k, i assumed it can be 10000
        for (int k = 1; k <= 10000; k++)
        {
            double d = (double)((k * eulerFunc) + 1) / e;
            if (d%1==0 && d > 0)
                return (int)d;
        }
        return -1;
    }

    private static boolean IsInt(Object obj)
    {
        return obj instanceof Integer;
    }
}
