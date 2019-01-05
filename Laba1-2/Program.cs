using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba1_2 {
    class Program {
        static void Main(string[] args) {

            string a = "9BCE528E8861A4BF2DE3F456AF796AA98A61FA41801B8EF0AAAE70C8F2789A20";
            string b = "24A7D07FF51A35F2D09D075CBD3851D7C4B858E2D3C20543893D7985EEC2FEF0";
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

            if (subs != null) {
                Console.WriteLine("Sub= " + subs);}
            else { Console.WriteLine("you have negative number" +'\n' + "b > a"); };


            //LongCmp
            var Cmp = new ulong[0];
            Cmp = LongCmp(a_mass, b_mass);


            //Multi
            var mult = new ulong[0];
            mult = Multi(a_mass, b_mass);
            string multi = ToStr(mult);
            Console.WriteLine("Multiply= " + multi);




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
            if (a.Length > b.Length) {  max = a.Length; } 
            else {  max = b.Length; };
            Array.Resize(ref a, max);
            Array.Resize(ref b, max);
            var ot = new ulong[max + 1];
            for (int i=0; i<max; i++) {
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
            for (int i=0; i<max; i++) {
                ulong p = a[i] - b[i] - borrow;
                diff[i] = p & 0xffffffff;
                if (b[i] <= a[i]) { borrow = 0; }
                else { borrow = 1;  };
            }
            if (borrow == 1) {
                if (a[max-1] == b[max-1]) { diff[1] = 0; }
                else { diff = null; };
            }
           
            


            return diff;
        }



        public static ulong[] LongCmp(ulong[] a, ulong[] b) {
            ulong borrow = 0;
            ulong borrow1 = 0;
            int max = 0;
            if (a.Length < b.Length) { max = b.Length; }
            else { max = a.Length; };
            Array.Resize(ref a, max+1);
            Array.Resize(ref b, max+1);
            var diff = new ulong[max];
            for (int i = 0; i < max; i++) {
                ulong p = a[i] - b[i] - borrow;
                diff[i] = p & 0xffffffff;
                if (b[i] < a[i]) { borrow = 0; }
                else { borrow = 1; };

            }
            if (b[max-1] <= a[max-1]) { borrow1 = 0; }
            else { borrow1 = 1; };

            if (borrow == 0) { Console.WriteLine("a > b "); };
            
            if (borrow ==1 && a[max] - b[max] - borrow1 == 0 ) { Console.WriteLine("a = b"); }
            



            return null;
        }

        private static ulong[] LongMulOneDigit(ulong[] a, ulong b) {
            ulong carry = 0;
            ulong[] c = new ulong[a.Length + 1];
            for (int i=0; i< a.Length; i++) {
                ulong p = a[i] * b + carry;
                carry = p >> 32;
                c[i] = p & 0xffffffff;
            }
            c[a.Length] = carry;
            return c;
        }

        private static ulong[] LongShiftDigitsToHigh(ulong[] a, int b) {
            ulong[] p = new ulong[a.Length + b];
            for (int i=0; i < a.Length; i++) {
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
            for (int i=0; i < a.Length; i++) {
                 temp = LongMulOneDigit(a, b[i]);
                 LongShiftDigitsToHigh(temp, i);
                 c = AddLong(c, temp);
            
            }
            return c;
        }
    }



}


