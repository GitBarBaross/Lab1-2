using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba1_2 {
    class Program {
        static void Main(string[] args) {

            string a = "16D4C";
            string b = "16D4CA8";
            a = Add0(a);
            b = Add0(b);

            Console.Write(a + "\n");
            var a_mass = new ulong[a.Length / 8];
            var b_mass = new ulong[b.Length / 8];
            a_mass = ToArr(a, a_mass);
            b_mass = ToArr(b, b_mass);
            string b1 = ToStr(b_mass);
            Console.WriteLine(b_mass);
            Console.WriteLine(b1);
            Console.ReadKey();


        }

        public static string Add0(string a) {
            while (a.Length % 8 != 0) {
                a = "0" + a;
            }

            return a;
        }


        public static string Del0(string h) {

            


            return h;
        }

        public static ulong[] ToArr(string c, ulong[] a) {

            var a32 = new ulong[c.Length / 8];
            for (int i = 0; i < c.Length; i += 8) {
                a32[i / 8] = Convert.ToUInt64(c.Substring(i, 8), 16);
                a[i / 8] = a32[i / 8];
                Array.Reverse(a32);
                Array.Reverse(a);
            }
            return a;
        }



        public static string ToStr(ulong[] b) {
            string x = null;
            for (int i = 0; i < b.Length; i++) {
                x = (b[i].ToString("X").PadLeft(8, '0')) + x;
            }
         
            return x;


        }





    }
}


