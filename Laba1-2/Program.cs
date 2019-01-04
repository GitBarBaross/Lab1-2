using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba1_2 {
    class Program {
        static void Main(string[] args) {

            string a = "58D42712A22E24D976B8322D2861283A5B0D603E7BA6E4E384645432C0BC70E7";
            string b = "5E672E3509AA1256B1A78FA3FAAFD1BF47F1E62E7743DD5F67A12A2E5E200BFE";
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
            else { Console.WriteLine("you have negative number"); };
            
            
           
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
                if (b[i] < a[i]) { borrow = 0; }
                else { borrow = 1;  };
            }
            if (borrow == 1) {
                
                 diff = null;
            }
           
            


            return diff;
        }

    }
}


