using System;

namespace Magic{
    internal class Program{
        static void Swap (int a, int b){
            var T = a;
            a = b;
            b = T;
        }

        static void Main(string[] args){          
            var p = true;
            int i, j, k;
            int b, r, m;

            Console.Write("Введите n = ");
            var n = Convert.ToInt32(Console.ReadLine());
            
            var a = new int[n+1,n+1];

            if (n % 2 != 0){ // It's odd
                i = 1;
                j = (n/2) + 1; // there is no Succ function in C#

                for (k = 1; k <= n*n; k++){
                    a[i,j] = k;
                    if ((k % n) == 0) i++;
                    else{
                        i--;
                        j++;
                        if (i == 0) i = n;
                        if (j > n) j = 1;
                    }
                }
            }
            else{ // It's even
                int s;
                if ((n % 4) == 0){
                    k = 1;
                    for (i = 1; i <= n; j++){
                        for (j = 1; j <= n; j++){
                            a[i,j] = k;
                            k++;
                        }
                    }
                    j = 2; m = n / 2;
                    for (i = 1; i <= m; i++){
                        for (k = 1; k <= m / 2; k++){
                            if (j == m + 1) j = 2;
                            else{
                                if (j == m + 2) j = 1;
                                s = (n - i) + 1;
                                b = (n - j) + 1;
                                Swap(a[i,j], a[s,b]);
                                Swap(a[i,b], a[s,j]);
                                j = j + 2;
                            }
                        }
                    }
                }
                else{
                    if (n != 2){
                        k = 1;
                        for (i = 1; i <= n; i++){
                            for (j = 1; j <= n; j++){
                                a[i,j] = k;
                                k++;
                            }

                            r = ((n/2-1))/2; 
                            m = n/2;

                            for (i = 1; i <= m; i++){
                                j = i;

                                for (k = 1; k <= r; k++){
                                    if (j > m) j = 1;
                                    s = (n - i)+1; 
                                    b = (n - j)+1;
                                    Swap(a[i,j], a[s,b]);
                                    Swap(a[i,b], a[s,j]);
                                    j++;
                                }
                            }
                            
                            i = 1; 
                            j = r+1;

                            for ( k = 1; k <= m; k++){
                                if (j > m) j = 1;
                                s = (n - i)+1;
                                Swap(a[i, j], a[s, j]);
                                i++;
                                j++;
                            }
                            
                            i = 1;
                            j = r + 2;
                            for ( k = 1; k <= m; k++){
                                if (j > m) j = 1;
                                b = (n - j)+1;
                                Swap(a[i, j], a[i, b]);
                                i++;
                                j++;
                            }
                        }
                    }
                    else p = false;
                }
            }

            if (p){   
                Console.Write("p = ");
                Console.Write(p);
                Console.WriteLine();

                for (i = 1; i <= n; i++){
                    for (j = 1; j <= n; j++){
                        Console.Write(a[i, j]);
                        Console.Write(" ");
                    }
                    Console.WriteLine();
                }
            }
            else Console.WriteLine("do not exists");
        }
    }
}