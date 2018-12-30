import java.util.Random;

/**
 * Created by Admin on 11.11.2017.
 */
public class HOST12 {
    private static int h = 26;
    private static int e;
    private static int es;
    private static int rs;
    private static int q = 47;
    private static int k;
    private static int xp = 7;
    private static int yp = 17;
    private static int xc = 16;
    private static int yc = 16;
    private static int r = 0;
    private static int s = 0;
    private static int d = 10;
    private static int v;
    private static int z1;
    private static int z2;
    private static int xcs = 16;
    private static int ycs = 16;
    static Random random;
    static {
        random = new Random();
    }
    public static void sendMessage(){
        e = h % q;
        while(s==0){
            while(r==0){
                k = random.nextInt(q-2) + 2;
                r = xc % q;
            }
            s = ( r * d + k * e)%q;
        }
    }
    public static void getMessage(){
        es = h % q;
        es = 27;
        v = es % q;
        z1 = ( s * v) % q;
        z2 = ((q-r)*v)%q;
        rs = xcs % q;
    }
    public static void checkECP() {
        System.out.println("Пользователь Алиса генерирует ключи:");
        sendMessage();
        System.out.println("e: " + e);
        System.out.println("d: " + d);
        System.out.println("k: " + k);
        System.out.println("Точка эллиптической кривой C(xc, yc) =  (" + xc + ", " + yc + ")");
        System.out.println("r: " + r);
        System.out.println("\nВычисление хэш-образа сообщения:");
        System.out.println("h(T): " + h);
        sendMessage();
        System.out.println("\nВыработка цифровой подписи:");
        System.out.println("s: " + s);
        System.out.println("\nПользователь Алиса отправляет исходное \nсообщение и цифровую подпись пользователю Бобу");
        getMessage();
        getMessage();
        System.out.println("\nПользователь Боб вычисляет хэш-образ \nпо полученному сообщению:");
        System.out.println("h' = h(T') = " + h);
        System.out.println("e': " + es);
        System.out.println("z1: " + z1);
        System.out.println("z2: " + z2);
        System.out.println("v: " + v);
        System.out.println("Точка эллиптической кривой C'(xc', yc') =  (" + xcs + ", " + ycs + ")");
        System.out.println("r': " + rs);
        if(r == rs) {
            System.out.println("Проверка сертификата прошла успешно");
        }
        else {
            System.out.println("Проверка сертификата провалилась");
        }
    }
}
