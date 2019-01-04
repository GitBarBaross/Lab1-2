using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba1_2 {
    class Program {
        static void Main(string[] args) {

            string a = "29DF8974FB1571D55BE1379E242B825293844F5E1573AC521722D835989AC308CBB6F53508DC42062CEEAE16089F46E8265059F791F87775680B8087D4B8D54F8557B3472DC6BF47AC80FA667549A9F82CB1199EC99E0EF92E40C1FA8ADE5CAD76C35A9E93AF0F306C48700868FF2C6ED24961CD4667BC6AA2D8430E874A9CDB";
            string b = "6C5C2905F1AE8C95C5A201B9F3561AA747899511C2A45CA6BBA3BF2F056514CD0009851D87A9A978E1D88EEE8C38D2806495B8F3C01D97DF718CB92247B3D4DAA4383AF7BDA40043BE87886B65F275D50C7886D65B4500E54AD7E2CDDA8BCF2A1DB38F38D884E78ACA7912DF485CBC4BFACCC29346C2D8ECD558F0E7C4149BC4";
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
            Console.WriteLine("Summ= " + summ);
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
    }
}


