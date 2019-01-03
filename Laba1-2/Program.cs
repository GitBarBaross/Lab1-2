using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba1_2 {
    class Program {
        static void Main(string[] args) {

            string a = "A962CB3129CF6D479D2574246BD15760B929D0F22122D8C77179A33109CAF6B7";
            string b = "A1B8CFBCEB1BA7DC9FB51BE63DDDB3F6061C422F0F238E24EFC05197A20DB218";
            a = Add0(a);
            b = Add0(b);
          
            var a_mass = new ulong[a.Length / 8];
            var b_mass = new ulong[b.Length / 8];
            a_mass = ToArr(a, a_mass);
            b_mass = ToArr(b, b_mass);
            var sum = new ulong[0];
            sum = AddLong(a_mass, b_mass);
            string summ = ToStr(sum);
            Console.WriteLine(summ);
            
           
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
            for (int i = 0; i < b.Length; i++) {
                x = (b[i].ToString("X").PadLeft(8, '0')) + x;
            }
            x = x.TrimStart('0');
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



    }
}


