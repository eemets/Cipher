import javafx.util.Pair;

public class Main {
    public static void main(String[] args) {
        try {
//            //RSA
            //A
            System.out.println("A generates 'n', 'e', 'd' values:");
            Pair<Pair<Integer, Integer>,Pair<Integer, Integer>> keys = RSAAlgorithm.GetRSAKeys();
            Pair<Integer, Integer> publicKey = keys.getKey();
            Pair<Integer, Integer> privateKey = keys.getValue();

            //B
            System.out.println("\nB generates 'k' and calculates 'r':");
            Pair<Integer, Integer> Bvalues = RSAAlgorithm.GetRSAvaluesRandK(publicKey);
            int r = Bvalues.getKey();
            int k = Bvalues.getValue();

            //A
            System.out.println("\nA counts 'k2':");
            int k2 = RSAAlgorithm.GetRSAvalueK2(r, privateKey);

            //B
            System.out.println("\nB checks if 'k' = 'k2':");

            boolean RSAauthResult = RSAAlgorithm.RSAAuthenticationIsValid(k, k2);

            System.out.println(RSAauthResult);


            //Schorr
//            System.out.println("1. Пользователь Алиса генерирует \nзначение публичного ключа y:");
//            int[] shnorrValues = ShnorrAlgorithm.GetShnorrValues();
//            int p = shnorrValues[0];
//            int q = shnorrValues[1];
//            int x = shnorrValues[2];
//            int g = shnorrValues[3];
//            int y = shnorrValues[4];
//
//            System.out.println("\n2. Пользователь Алиса генерирует \nчисло k и вычисляет число r:");
//            Pair<Integer, Integer> Avalues = ShnorrAlgorithm.GetShnorrValuesKandR(p, q, g);
//            int r = Avalues.getKey();
//            int k = Avalues.getValue();
//
//            System.out.println("\nПользователь Боб \nвыбирает случайное число e:");
//            int e = ShnorrAlgorithm.GetShnorrValueE();
//
//            System.out.println("\nПользователь Алиса вычисляет s:");
//            int s = ShnorrAlgorithm.GetShnorrValueS(k, x, e, q);
//
//            System.out.println("\nПользователь Боб проверяет \nистинность выражения r = (g^s * y^e):");
//            boolean shnorrResult = ShnorrAlgorithm.AuthenticationIsValid(r, p, g, s, y, e);
//
//            System.out.println(shnorrResult);

            //Feige-Fiat-Shamir
//            System.out.println("1. Посредник генерирует число n, \nпубличный ключ v, приватный ключ s:");
//            int[] interValues = FeigeFiatShamirAlgorithm.GetFFSIntermediaryValues();
//            int n = interValues[0];
//            int v = interValues[1];
//            int s = interValues[2];
//            int p = interValues[3];
//            int q = interValues[4];
//            System.out.println(String.format("n = %d,\nv = %d,\ns = %d", n, v, s));
//
//            System.out.println("\n2. Пользователь Алиса получает приватный ключ s.");
//
//            System.out.println("\nПользователь Алиса выбирает случайное число r \nи вычисляет значение переменной z:");
//            Pair<Integer, Integer> Avalues = FeigeFiatShamirAlgorithm.GetFFSValuesRAndZ(n, p);
//            int r = 21;//Avalues.getKey();
//            int z = Avalues.getValue();
//
//            System.out.println("\nПользователь Боб отправляет случайное \nзначение бита пользователю Алисе:\n");
//            byte bit = Helper.GetRandomBit();
//            System.out.println("b = " + bit + "\n");
//
//            System.out.println("Если b = 0 - пользователь Алиса отправляет \nпользователю Бобу r, иначе y:\n");
//            int bitADependedValue = FeigeFiatShamirAlgorithm.GetABitDependedValue(bit, r, s, p);
//
//            System.out.println("Пользователь Боб проверяет успешность аутентификации:");
//            boolean ffsValid = false;
//            ffsValid = FeigeFiatShamirAlgorithm.AuthentificationIsValid(bit, z, r, p, v, bitADependedValue);
//            System.out.println(true);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
