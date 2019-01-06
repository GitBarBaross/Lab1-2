using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba1_2 {

    public class Program {
        static void Main(string[] args) {

            string a = "D26D92F6422BE72AE738680C5669D64CEED3F48705CB19AB30F84CFCEEE17C5D";
            string b = "861";
            a = Add0(a);
            b = Add0(b);

            var a_mass = new ulong[a.Length / 8];
            var b_mass = new ulong[b.Length / 8];
            a_mass = ToArr(a, a_mass);
            b_mass = ToArr(b, b_mass);

            //sum
            var sum = new ulong[0];
            sum = AddLong(a_mass, b_mass);
            string summ = ToStr(sum);
            Console.WriteLine("Sum= " + summ);
            //sub
            var sub = new ulong[0];
            sub = SubLong(a_mass, b_mass);
            string subs = ToStr(sub);
            bool q;

            if (q = LongCmp(a_mass, b_mass) != -1) {
                Console.WriteLine("Sub= " + subs);
            }
            else { Console.WriteLine("you have negative number" + '\n'); };


            //LongCmp

            int Cmp = LongCmp(a_mass, b_mass);
            if (Cmp == 1) { Console.WriteLine("a > b "); };
            if (Cmp == 0) { Console.WriteLine("a = b "); };
            if (Cmp == -1) { Console.WriteLine("a < b "); };




            //Multi
            var mult = new ulong[0];
            mult = Multi(a_mass, b_mass);
            string multi = ToStr(mult);
            Console.WriteLine("Multiply= " + multi);

            //Div
            var di = new ulong[0];
            di = Div(a_mass, b_mass);
            string div = ToStr(di);
            Console.WriteLine("Division = " + div); 


      



            Console.ReadKey();


        }

        public static string Add0(string a) {
            while (a.Length % 8 != 0) {
                a = "0" + a;
            }

            return a;
        }






        public static ulong[] ToArr(string a, ulong[] a_mass) {

            var a32 = new ulong[a.Length / 8];
            for (int i = 0; i < a.Length; i += 8) {
                a32[i / 8] = Convert.ToUInt64(a.Substring(i, 8), 16);
                a_mass[i / 8] = a32[i / 8];

            }
            Array.Reverse(a32);
            Array.Reverse(a_mass);
            return a_mass;
        }



        public static string ToStr(ulong[] b) {
            string x = null;
            if (b != null) {
                for (int i = 0; i < b.Length; i++) {
                    x = (b[i].ToString("X").PadLeft(8, '0')) + x;
                }
                x = x.TrimStart('0');
            }


            return x;


        }

        public static ulong[] AddLong(ulong[] a, ulong[] b) {
            ulong carry = 0;
            int max = 0;
            if (a.Length > b.Length) { max = a.Length; }
            else { max = b.Length; };
            Array.Resize(ref a, max);
            Array.Resize(ref b, max);
            var ot = new ulong[max + 1];
            for (int i = 0; i < max; i++) {
                ulong p = a[i] + b[i] + carry;
                carry = p >> 32;
                ot[i] = p & 0xffffffff;

            }
            ot[max] = carry;
            return ot;

        }

        public static ulong[] SubLong(ulong[] a, ulong[] b) {
            ulong borrow = 0;
            int max = 0;
            if (a.Length < b.Length) { max = b.Length; }
            else { max = a.Length; };
            Array.Resize(ref a, max);
            Array.Resize(ref b, max);
            var diff = new ulong[max];
            for (int i = 0; i < max; i++) {
                ulong p = a[i] - b[i] - borrow;
                diff[i] = p & 0xffffffff;
                if (b[i] <= a[i]) { borrow = 0; }
                else { borrow = 1; };
            }
            if (borrow == 1) {
                if (a[max - 1] == b[max - 1]) { diff[1] = 0; }
                else { diff = null; };
            }




            return diff;
        }



        public static int LongCmp(ulong[] a, ulong[] b) {
            
            
            ulong borrow = 0;
            ulong borrow1 = 0;
            int max = 0;
            if (a.Length < b.Length) { max = b.Length; }
            else { max = a.Length; };
            Array.Resize(ref a, max + 1);
            Array.Resize(ref b, max + 1);
            var diff = new ulong[max];
            for (int i = 0; i < max; i++) {
                ulong p = a[i] - b[i] - borrow;
                diff[i] = p & 0xffffffff;
                if (b[i] < a[i]) { borrow = 0; }
                else { borrow = 1; };

            }
            if (b[max - 1] <= a[max - 1]) { borrow1 = 0; }
            else { borrow1 = 1; };

            if (borrow == 0) {  return 1 ; };

            if (borrow == 1 && a[max] - b[max] - borrow1 == 0) {  return 0; }




            return -1;
        }

        private static ulong[] LongMulOneDigit(ulong[] a, ulong b) {
            ulong carry = 0;
            ulong[] c = new ulong[a.Length + 1];
            for (int i = 0; i < a.Length; i++) {
                ulong p = a[i] * b + carry;
                carry = p >> 32;
                c[i] = p & 0xffffffff;
            }
            c[a.Length] = carry;
            return c;
        }

        private static ulong[] LongShiftDigitsToHigh(ulong[] a, int b) {
            ulong[] p = new ulong[a.Length + b];
            for (int i = 0; i < a.Length; i++) {
                p[i + b] = a[i];
            }
            return p;

        }

        public static ulong[] Multi(ulong[] a, ulong[] b) {
            int max = 0;
            if (a.Length < b.Length) { max = b.Length; }
            else { max = a.Length; };
            Array.Resize(ref a, max);
            Array.Resize(ref b, max);
            ulong[] c = new ulong[2 * max];
            ulong[] temp = new ulong[a.Length];
            for (int i = 0; i < a.Length; i++) {
                temp = LongMulOneDigit(a, b[i]);
                temp = LongShiftDigitsToHigh(temp, i);
                c = AddLong(c, temp);

            }
            return c;
        }
        
        



            private static int BitLength(ulong[] a) {
            int i = a.Length  - 1;
            int b = 0;
        
             for ( i = a.Length-1; a[i] == 0; i--) {
                 if (i < 0) { return 0; }
             }
             
            var k = a[i];
            while (k>0) {
            b++;
            k= k >> 1;
            }
             
             b = b + 32 * i;
             return b;
         }
         
        



        private static ulong[] LongShiftBitsToHigh(ulong[] a, int b) {
            int p = b / 32;
            int k = b - p * 32;
            ulong x;
            ulong carry = 0;
            ulong[] c = new ulong[a.Length + p + 1];
            for (int i = 0; i < a.Length; i++) {
                x = a[i];
                x = x << k;
                c[i + p] = (x & 0xFFFFFFFF) + carry;
                carry = (x & 0xFFFFFFFF00000000) >> 32;
            }
            c[c.Length - 1] = carry;
            return c;
        }


        public static ulong[] Div(ulong[] a, ulong[] b) {
            var k = BitLength(b);
            var p = a;
            ulong[] ans = new ulong[a.Length];
            ulong[] x = new ulong[a.Length];
            ulong[] y = new ulong[a.Length];
            x[0] = 0x1;

            
            while (LongCmp(p, b) >= 0) {
                var z = BitLength(p);
                y = LongShiftBitsToHigh(b, z - k);
                if (LongCmp(p, y) == -1) {
                    z = z - 1;
                    y = LongShiftBitsToHigh(b, z - k);
                }
                p = SubLong(p, y);
                ans = AddLong(ans, LongShiftBitsToHigh(x, z - k));
            }
            
            
            return ans;

        }
        
        












    } 
}



    





