
import java.math.BigInteger;
import java.nio.charset.StandardCharsets;

public class Cipher {
    static BigInteger Func1(BigInteger x, BigInteger y, BigInteger z)
    {
        BigInteger value = (x.and(y)).or(x.negate().and(x));
        return value;
    }

    static BigInteger Func2(BigInteger x, BigInteger y, BigInteger z)
    {
        BigInteger value = (x.and(z)).or(z.negate().and(y));
        return value;
    }

    static BigInteger Func3(BigInteger x, BigInteger y, BigInteger z)
    {
        BigInteger value = x.xor(y).xor(z);
        return value;
    }

    static BigInteger Func4(BigInteger x, BigInteger y, BigInteger z)
    {
        BigInteger value = y.xor(z.negate().or(x));
        return value;
    }

    static BigInteger RotateLeft(BigInteger n, int bits)
    {
        return (n.shiftLeft(bits)).or(n.shiftRight(32 - bits));
    }

    static void encrypt(String message)
    {
        byte[] messageInASCII = message.getBytes(StandardCharsets.US_ASCII);
        String newMessage = "";

        for (int i = 0; i < messageInASCII.length; i++)
        {
            newMessage += Integer.toBinaryString((int)messageInASCII[i]);
        }

        int messageLengthInBytes = newMessage.length();

        newMessage += "1";
        while (newMessage.length() % 512 != 448)
        {
            newMessage += "0";
        }
        StringBuilder sb = new StringBuilder("0000000000000000000000000000000000000000000000000000000000000000");
        String temp = Integer.toBinaryString(messageLengthInBytes);

        for (int i = 0, j = sb.length() - 1, k = temp.length() - 1; i < temp.length(); i++) {
            sb.setCharAt(j, temp.charAt(k));
            j--;
            k--;
        }
        String messageLengthInBytes64 = sb.toString();

        newMessage += messageLengthInBytes64.substring(32, 32);
        newMessage += messageLengthInBytes64.substring(0, 32);

        long A = 0x01234567;
        long B = 0x89ABCDEF;
        long C = 0xFEDCBA98;
        long D = 0x76543210;

        BigInteger AA = new BigInteger(Long.toString(A));
        BigInteger BB = new BigInteger(Long.toString(B));
        BigInteger CC = new BigInteger(Long.toString(C));
        BigInteger DD = new BigInteger(Long.toString(D));

        BigInteger Divisor = new BigInteger("4294967296");
        BigInteger[] T =
                {
                        new BigInteger("d76aa478", 16), new BigInteger("e8c7b756", 16), new BigInteger("242070db", 16), new BigInteger("c1bdceee", 16),
                        new BigInteger("f57c0faf", 16), new BigInteger("4787c62a", 16), new BigInteger("a8304613", 16), new BigInteger("fd469501", 16),
                        new BigInteger("698098d8", 16), new BigInteger("8b44f7af", 16), new BigInteger("ffff5bb1", 16), new BigInteger("895cd7be", 16),
                        new BigInteger("6b901122", 16), new BigInteger("fd987193", 16), new BigInteger("a679438e", 16), new BigInteger("49b40821", 16),
                        new BigInteger("f61e2562", 16), new BigInteger("c040b340", 16), new BigInteger("265e5a51", 16), new BigInteger("e9b6c7aa", 16),
                        new BigInteger("d62f105d", 16), new BigInteger("02441453", 16), new BigInteger("d8a1e681", 16), new BigInteger("e7d3fbc8", 16),
                        new BigInteger("21e1cde6", 16), new BigInteger("c33707d6", 16), new BigInteger("f4d50d87", 16), new BigInteger("455a14ed", 16),
                        new BigInteger("a9e3e905", 16), new BigInteger("fcefa3f8", 16), new BigInteger("676f02d9", 16), new BigInteger("8d2a4c8a", 16),
                        new BigInteger("fffa3942", 16), new BigInteger("8771f681", 16), new BigInteger("6d9d6122", 16), new BigInteger("fde5380c", 16),
                        new BigInteger("a4beea44", 16), new BigInteger("4bdecfa9", 16), new BigInteger("f6bb4b60", 16), new BigInteger("bebfbc70", 16),
                        new BigInteger("289b7ec6", 16), new BigInteger("eaa127fa", 16), new BigInteger("d4ef3085", 16), new BigInteger("04881d05", 16),
                        new BigInteger("d9d4d039", 16), new BigInteger("e6db99e5", 16), new BigInteger("1fa27cf8", 16), new BigInteger("c4ac5665", 16),
                        new BigInteger("f4292244", 16), new BigInteger("432aff97", 16), new BigInteger("ab9423a7", 16), new BigInteger("fc93a039", 16),
                        new BigInteger("655b59c3", 16), new BigInteger("8f0ccc92", 16), new BigInteger("ffeff47d", 16), new BigInteger("85845dd1", 16),
                        new BigInteger("6fa87e4f", 16), new BigInteger("fe2ce6e0", 16), new BigInteger("a3014314", 16), new BigInteger("4e0811a1", 16),
                        new BigInteger("f7537e82", 16), new BigInteger("bd3af235", 16), new BigInteger("2ad7d2bb", 16), new BigInteger("eb86d391", 16)
                };

        String block;
        String[] X = new String[16];
        int index;
        int[] K =
                {
                        0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15,
                        1, 6, 11, 0, 5, 10, 15, 4, 9, 14, 3, 8, 13, 2, 7, 12,
                        5, 8, 11, 14, 1, 4, 7, 10, 13, 0, 3, 6, 9, 12, 15, 2,
                        0, 7, 14, 5, 12, 3, 10, 1, 8, 15, 6, 13, 4, 11, 2, 9
                };
        int counter = 0;

        for (int i = 0; i < newMessage.length() / 512; i++)
        {
            block = newMessage.substring(0, 512);
            index = 0;
            for (int j = 0; j < X.length; j++)
            {
                X[j] = newMessage.substring(index, 32);
                index += 32;
            }

            for (int j = 0; j < 4; j++)
            {
                switch (j)
                {
                    case 0:
                        System.out.println("1st round");
                        break;
                    case 1:
                        System.out.println("2nd round");
                        break;
                    case 2:
                        System.out.println("3rd round");
                        break;
                    case 3:
                        System.out.println("4th round");
                        break;
                }
                for (int k = 0; k < 4; k++)
                {
                    switch (j)
                    {
                        case 0:
                            AA = BB.add(RotateLeft(AA.add(Func1(BB, CC, DD)).remainder(Divisor).add(BigInteger.valueOf(Long.parseLong(X[K[counter]]))).remainder(Divisor).add(T[counter]).remainder(Divisor), 7)).remainder(Divisor);
                            counter++;
                            System.out.println("AA = " + String.format("0x{0:x2} ", AA));

                            DD = AA.add(RotateLeft(DD.add(Func1(AA, BB, CC)).remainder(Divisor).add(BigInteger.valueOf(Long.parseLong(X[K[counter]]))).remainder(Divisor).add(T[counter]).remainder(Divisor), 12)).remainder(Divisor);
                            //DD = BigInteger.Remainder(AA + (RotateLeft((BigInteger.Remainder(BigInteger.Remainder(BigInteger.Remainder(DD + Func1(AA, BB, CC), Divisor) + BigInteger.Parse(X[K[counter]]), Divisor) + T[counter], Divisor)), 12)), Divisor);
                            counter++;
                            System.out.println("DD = " + String.format("0x{0:x2} ", DD));

                            CC = DD.add(RotateLeft(CC.add(Func1(DD, AA, BB)).remainder(Divisor).add(BigInteger.valueOf(Long.parseLong(X[K[counter]]))).remainder(Divisor).add(T[counter]).remainder(Divisor), 17)).remainder(Divisor);
                            //CC = BigInteger.Remainder(DD + (RotateLeft((BigInteger.Remainder(BigInteger.Remainder(BigInteger.Remainder(CC + Func1(DD, AA, BB), Divisor) + BigInteger.Parse(X[K[counter]]), Divisor) + T[counter], Divisor)), 17)), Divisor);
                            counter++;
                            System.out.println("CC = " + String.format("0x{0:x2} ", CC));

                            BB = CC.add(RotateLeft(BB.add(Func1(CC, DD, AA)).remainder(Divisor).add(BigInteger.valueOf(Long.parseLong(X[K[counter]]))).remainder(Divisor).add(T[counter]).remainder(Divisor), 22)).remainder(Divisor);
                            //BB = BigInteger.Remainder(CC + (RotateLeft((BigInteger.Remainder(BigInteger.Remainder(BigInteger.Remainder(BB + Func1(CC, DD, AA), Divisor) + BigInteger.Parse(X[K[counter]]), Divisor) + T[counter], Divisor)), 22)), Divisor);
                            counter++;
                            System.out.println("BB = " + String.format("0x{0:x2}\n", BB));
                            break;

                        case 1:
                            AA = BB.add(RotateLeft(AA.add(Func1(BB, CC, DD)).remainder(Divisor).add(BigInteger.valueOf(Long.parseLong(X[K[counter]]))).remainder(Divisor).add(T[counter]).remainder(Divisor), 5)).remainder(Divisor);
                            //AA = BigInteger.Remainder(BB + (RotateLeft((BigInteger.Remainder(BigInteger.Remainder(BigInteger.Remainder(AA + Func1(BB, CC, DD), Divisor) + BigInteger.Parse(X[K[counter]]), Divisor) + T[counter], Divisor)), 5)), Divisor);
                            counter++;
                            System.out.println("AA = " + String.format("0x{0:x2} ", AA));

                            DD = AA.add(RotateLeft(DD.add(Func1(AA, BB, CC)).remainder(Divisor).add(BigInteger.valueOf(Long.parseLong(X[K[counter]]))).remainder(Divisor).add(T[counter]).remainder(Divisor), 9)).remainder(Divisor);
                            //DD = BigInteger.Remainder(AA + (RotateLeft((BigInteger.Remainder(BigInteger.Remainder(BigInteger.Remainder(DD + Func1(AA, BB, CC), Divisor) + BigInteger.Parse(X[K[counter]]), Divisor) + T[counter], Divisor)), 9)), Divisor);
                            counter++;
                            System.out.println("DD = " + String.format("0x{0:x2} ", DD));

                            CC = DD.add(RotateLeft(CC.add(Func1(DD, AA, BB)).remainder(Divisor).add(BigInteger.valueOf(Long.parseLong(X[K[counter]]))).remainder(Divisor).add(T[counter]).remainder(Divisor), 14)).remainder(Divisor);
                            //CC = BigInteger.Remainder(DD + (RotateLeft((BigInteger.Remainder(BigInteger.Remainder(BigInteger.Remainder(CC + Func1(DD, AA, BB), Divisor) + BigInteger.Parse(X[K[counter]]), Divisor) + T[counter], Divisor)), 14)), Divisor);
                            counter++;
                            System.out.println("CC = " + String.format("0x{0:x2} ", CC));

                            BB = CC.add(RotateLeft(BB.add(Func1(CC, DD, AA)).remainder(Divisor).add(BigInteger.valueOf(Long.parseLong(X[K[counter]]))).remainder(Divisor).add(T[counter]).remainder(Divisor), 20)).remainder(Divisor);
                            //BB = BigInteger.Remainder(CC + (RotateLeft((BigInteger.Remainder(BigInteger.Remainder(BigInteger.Remainder(BB + Func1(CC, DD, AA), Divisor) + BigInteger.Parse(X[K[counter]]), Divisor) + T[counter], Divisor)), 20)), Divisor);
                            counter++;
                            System.out.println("BB = " + String.format("0x{0:x2}\n", BB));
                            break;

                        case 2:

                            AA = BB.add(RotateLeft(AA.add(Func1(BB, CC, DD)).remainder(Divisor).add(BigInteger.valueOf(Long.parseLong(X[K[counter]]))).remainder(Divisor).add(T[counter]).remainder(Divisor), 7)).remainder(Divisor);
                            //AA = BigInteger.Remainder(BB + (RotateLeft((BigInteger.Remainder(BigInteger.Remainder(BigInteger.Remainder(AA + Func1(BB, CC, DD), Divisor) + BigInteger.Parse(X[K[counter]]), Divisor) + T[counter], Divisor)), 5)), Divisor);
                            counter++;
                            System.out.println("AA = " + String.format("0x{0:x2} ", AA));

                            DD = AA.add(RotateLeft(DD.add(Func1(AA, BB, CC)).remainder(Divisor).add(BigInteger.valueOf(Long.parseLong(X[K[counter]]))).remainder(Divisor).add(T[counter]).remainder(Divisor), 7)).remainder(Divisor);
                            //DD = BigInteger.Remainder(AA + (RotateLeft((BigInteger.Remainder(BigInteger.Remainder(BigInteger.Remainder(DD + Func1(AA, BB, CC), Divisor) + BigInteger.Parse(X[K[counter]]), Divisor) + T[counter], Divisor)), 9)), Divisor);
                            counter++;
                            System.out.println("DD = " + String.format("0x{0:x2} ", DD));

                            CC = DD.add(RotateLeft(CC.add(Func1(DD, AA, BB)).remainder(Divisor).add(BigInteger.valueOf(Long.parseLong(X[K[counter]]))).remainder(Divisor).add(T[counter]).remainder(Divisor), 7)).remainder(Divisor);
                            //CC = BigInteger.Remainder(DD + (RotateLeft((BigInteger.Remainder(BigInteger.Remainder(BigInteger.Remainder(CC + Func1(DD, AA, BB), Divisor) + BigInteger.Parse(X[K[counter]]), Divisor) + T[counter], Divisor)), 14)), Divisor);
                            counter++;
                            System.out.println("CC = " + String.format("0x{0:x2} ", CC));

                            BB = CC.add(RotateLeft(BB.add(Func1(CC, DD, AA)).remainder(Divisor).add(BigInteger.valueOf(Long.parseLong(X[K[counter]]))).remainder(Divisor).add(T[counter]).remainder(Divisor), 7)).remainder(Divisor);
                            //BB = BigInteger.Remainder(CC + (RotateLeft((BigInteger.Remainder(BigInteger.Remainder(BigInteger.Remainder(BB + Func1(CC, DD, AA), Divisor) + BigInteger.Parse(X[K[counter]]), Divisor) + T[counter], Divisor)), 20)), Divisor);
                            counter++;
                            System.out.println("BB = " + String.format("0x{0:x2}\n", BB));
                            break;

                        case 3:
                            AA = BB.add(RotateLeft(AA.add(Func1(BB, CC, DD)).remainder(Divisor).add(BigInteger.valueOf(Long.parseLong(X[K[counter]]))).remainder(Divisor).add(T[counter]).remainder(Divisor), 6)).remainder(Divisor);
                            //AA = BigInteger.Remainder(BB + (RotateLeft((BigInteger.Remainder(BigInteger.Remainder(BigInteger.Remainder(AA + Func1(BB, CC, DD), Divisor) + BigInteger.Parse(X[K[counter]]), Divisor) + T[counter], Divisor)), 5)), Divisor);
                            counter++;
                            System.out.println("AA = " + String.format("0x{0:x2} ", AA));

                            DD = AA.add(RotateLeft(DD.add(Func1(AA, BB, CC)).remainder(Divisor).add(BigInteger.valueOf(Long.parseLong(X[K[counter]]))).remainder(Divisor).add(T[counter]).remainder(Divisor), 10)).remainder(Divisor);
                            //DD = BigInteger.Remainder(AA + (RotateLeft((BigInteger.Remainder(BigInteger.Remainder(BigInteger.Remainder(DD + Func1(AA, BB, CC), Divisor) + BigInteger.Parse(X[K[counter]]), Divisor) + T[counter], Divisor)), 9)), Divisor);
                            counter++;
                            System.out.println("DD = " + String.format("0x{0:x2} ", DD));

                            CC = DD.add(RotateLeft(CC.add(Func1(DD, AA, BB)).remainder(Divisor).add(BigInteger.valueOf(Long.parseLong(X[K[counter]]))).remainder(Divisor).add(T[counter]).remainder(Divisor), 15)).remainder(Divisor);
                            //CC = BigInteger.Remainder(DD + (RotateLeft((BigInteger.Remainder(BigInteger.Remainder(BigInteger.Remainder(CC + Func1(DD, AA, BB), Divisor) + BigInteger.Parse(X[K[counter]]), Divisor) + T[counter], Divisor)), 14)), Divisor);
                            counter++;
                            System.out.println("CC = " + String.format("0x{0:x2} ", CC));

                            BB = CC.add(RotateLeft(BB.add(Func1(CC, DD, AA)).remainder(Divisor).add(BigInteger.valueOf(Long.parseLong(X[K[counter]]))).remainder(Divisor).add(T[counter]).remainder(Divisor), 21)).remainder(Divisor);
                            //BB = BigInteger.Remainder(CC + (RotateLeft((BigInteger.Remainder(BigInteger.Remainder(BigInteger.Remainder(BB + Func1(CC, DD, AA), Divisor) + BigInteger.Parse(X[K[counter]]), Divisor) + T[counter], Divisor)), 20)), Divisor);
                            counter++;
                            System.out.println("BB = " + String.format("0x{0:x2}\n", BB));
                            break;
                    }
                }
            }

            A = AA.add(BigInteger.valueOf(A)).remainder(Divisor).longValue();
            B = BB.add(BigInteger.valueOf(B)).remainder(Divisor).longValue();
            C = CC.add(BigInteger.valueOf(C)).remainder(Divisor).longValue();
            D = DD.add(BigInteger.valueOf(D)).remainder(Divisor).longValue();

            newMessage = newMessage.substring(512);
        }

        System.out.println(("encrypt = " +
                String.format("%d", A & 0xffff) +
                String.format("%d", A >> 16) +
                String.format("%d", B & 0xffff) +
                String.format("%d", B >> 16) +
                String.format("%d", C & 0xffff) +
                String.format("%d", C >> 16) +
                String.format("%d", D & 0xffff) +
                String.format("%d", D >> 16)));
    }
}
