import javafx.util.Pair;

import java.util.Random;

public class FeigeFiatShamirAlgorithm {
    public static Random rnd = new Random();

    public static int[] GetFFSIntermediaryValues()
    {
        int p = 11;
        int q = 13;

        int n = p * q;

        //public key
        int v = 16;

        //private key
        int s = 9;

        return new int[] {n, v, s, p, q};
    }

    static Pair<Integer, Integer> GetFFSValuesRAndZ(int n, int p)
    {
        int r = 21;//rnd.Next(1, n - 1);
        double z = Math.pow(r, 2) % p;
        System.out.println(String.format("r = %d,\nz = %.4f", r, z));

        return new Pair<>(r, (int)z);
    }

    public static int GetABitDependedValue(byte bit, int r, int s, int p) throws Exception {
        switch (bit)
        {
            case 0:
                System.out.println("Возвращает r\n");
                return r;
            case 1:
                System.out.println("Возвращает y = (r * s) % p\n");
                return (r * s) % p;
            default:
                throw new Exception();
        }
    }

    public static boolean AuthentificationIsValid(byte bit, int z, int r, int p, int v, int bitADependedValue) throws Exception {
        switch (bit)
        {
            case 0:
                return z == Math.pow(r, 2) % p;
            case 1:
                return z == (Math.pow(bitADependedValue, 2) * v) % p;
            default:
                throw new Exception();
        }
    }
}
