import javafx.util.Pair;

import java.util.ArrayList;
import java.util.Random;

public class Helper {
    static Random rnd = new Random();

    public static int GetRsaExponent(int begVal, int eulerFunc)
    {
        for (int num = begVal; num < eulerFunc; num += 2)
        {
            //if (HaveNoCommonDividers(num, eulerFunc))
            if (eulerFunc % num != 0)
                return num;
        }
        return -1;
    }

    //private static bool HaveNoCommonDividers(int num, int eulerFunc)
    //{
    //    for (int i = 3; i <= num; i += 2)
    //    {
    //        if (eulerFunc % i == 0)
    //            return false;
    //    }
    //    return true;
    //}

    public static Pair<Integer, Integer> GetRsaRandomPrimes()
    {
        ArrayList<Integer> primes = GeneratePrimes();
        int rndIndex = rnd.nextInt(primes.size() - 1);

        return new Pair<>(primes.get(rndIndex), primes.get(rndIndex + 1));
    }

    private static ArrayList<Integer> GeneratePrimes()
    {
        int boundary = 30;
        ArrayList<Integer> primes = new ArrayList<>();
        primes.add(2);

        for (int i = 3; i < boundary; i++)
        {
            if (IsPrime(i))
                primes.add(i);
            i++;
        }

        return primes;
    }

    private static boolean IsPrime(int number)
    {
        if ((number & 1) == 0)
        {
            return (number == 2);
        }

        int limit = (int)Math.sqrt(number);

        for (int i = 3; i <= limit; i += 2)
        {
            if ((number % i) == 0)
            {
                return false;
            }
        }
        return true;
    }

     static byte GetRandomBit()
    {
        return (byte)rnd.nextInt(2);
    }
}
