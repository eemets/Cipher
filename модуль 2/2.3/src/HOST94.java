import java.math.BigInteger;
import java.util.Random;

public class HOST94 {
    private static int p = 79;
    private static int q = 13;
    private static int a;
    private static int x;
    private static int y;
    private static int h = 21;
    private static int w;
    private static int k;
    private static int s = 0;
    private static int ws = 0;
    private static int v;
    private static int z1;
    private static int z2;
    private static BigInteger u;
    static Random random;

    static{
        random = new Random();
        a = random.nextInt(p - 3) + 2;
        while(getHash(a, q, p)%p!=1){
            a = random.nextInt(p - 3) + 2;
        }
        x = random.nextInt(q - 2) + 2;
        y = getHash(a, x, p);
    }
    private static int getHash(int h, int d, int p) {
        int degree = h;
        for (int i = 0; i < d - 1; i++) {
            h = (int) h * degree;
            h = h % p;
        }

        return h;
    }
    public static void sendMessage(){
        while(s == 0){
            while(ws == 0){
                k = random.nextInt(q - 2) + 2;
                w = getHash(a, k, p);
                ws = w % q;
            }
            s = (x*ws + k*h)%q;
        }
    }
    public static void getMessage(){
        v = getHash(h, q-2, q);
        z1 = (s * v) % q;
        z2 = ((q - ws)*v)%q;
        u = BigInteger.valueOf((long)Math.pow(a, z1));
        u = u.multiply(BigInteger.valueOf((long)Math.pow(y, z2)));
        u = u.mod(BigInteger.valueOf(p));
        u = u.mod(BigInteger.valueOf(q));
    }
    public static void checkECP() {
        System.out.println("Пользователь Алиса генерирует ключи:");
        System.out.println("p: " + p);
        System.out.println("q: " + q);
        System.out.println("a: " + a);
        System.out.println("x: " + x);
        System.out.println("y: " + y);

        System.out.println("k: " + k);
        System.out.println("w: " + w);
        System.out.println("w': " + ws);
        System.out.println("\nВычисление хэш-образа сообщения:");
        System.out.println("h(T): " + h);
        sendMessage();
        System.out.println("\nВыработка цифровой подписи:");
        System.out.println("s: " + s);
        System.out.println("\nПользователь Алиса отправляет исходное \nсообщение и цифровую подпись пользователю Бобу");
        getMessage();
        System.out.println("\nПользователь Боб вычисляет хэш-образ \nпо полученному сообщению:");
        System.out.println("h' = h(T') = " + h);
        System.out.println("v: " + v);
        System.out.println("u: " + u);
        if(u.intValue() == ws) {
            System.out.println("Проверка сертификата прошла успешно");
        }
        else {
            System.out.println("Проверка сертификата провалилась");
        }
    }
}
