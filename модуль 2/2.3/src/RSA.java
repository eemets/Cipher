import java.util.Random;

public class RSA {
    static int[] primeNumbers = {2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47};
    static Random random;
    static int f;
    static int p;
    static int q;
    static int n;
    static int e;
    static int d;
    static int h;
    static int s;

    static {
        random = new Random(System.currentTimeMillis());
        p = getPrimeByIndex(random.nextInt(3 + (primeNumbers.length - 3)));
        do {
            q = getPrimeByIndex(3 + (random.nextInt(primeNumbers.length - 3)));
        }
        while (p == q);
        n = p * q;
        f = (p - 1) * (q - 1);
        for (int i = 0; ; i++) {
            if (f % getPrimeByIndex(i) != 0) {
                e = getPrimeByIndex(i);
                break;
            }
        }

        for (int i = 1; ; i++) {
            if ((i * e) % f == 1) {
                d = i;
                break;
            }
        }
    }
    private static int getPrimeByIndex(int index) {
        return primeNumbers[index];
    }
    private static void setHash() {
        h = 12;
    }
    private static int getHash(int h, int d) {
        int degree = h;
        for (int i = 0; i < d - 1; i++) {
            h = (int) h * degree;
            h = h % n;
        }

        return h;
    }

    public static void checkECP() {
        setHash();
        s = getHash(h, d);
        System.out.println("Пользователь Алиса генерирует ключи:");
        System.out.println("e: " + e);
        System.out.println("d: " + d);
        System.out.println("n: " + n);
        System.out.println("\nВычисление хэш-образа сообщения:");
        System.out.println("h(T): " + h);
        System.out.println("\nВыработка цифровой подписи:");
        System.out.println("s: " + s);
        System.out.println("\nПользователь Алиса отправляет исходное \nсообщение и цифровую подпись пользователю Бобу");
        System.out.println("\nПользователь Боб вычисляет хэш-образ \nпо полученному сообщению:");
        System.out.println("h' = h(T') = " + h);
        System.out.println("\nВычисление хэш-образа из цифровой подписи h:");
        System.out.println("h: " + getHash(s, e));
        System.out.println("\nСравнение h' и h: ");
        System.out.println("h' = h (" + h + " = " + getHash(s, e) + ") ?\n");
        if(h == getHash(s, e)) {
            System.out.println("Проверка сертификата прошла успешно");
        }
        else {
            System.out.println("Проверка сертификата провалилась");
        }
    }
}
