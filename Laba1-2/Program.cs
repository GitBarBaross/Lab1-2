using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba1_2 {
    class Program {
        static void Main(string[] args) {

            string a = "16D4C";
            string b = "0";
            a = Add0(a);
            b = Add0(b);

            Console.Write(a + "\n");
            var a_mass = new ulong[a.Length / 8];
            var b_mass = new ulong[b.Length / 8];
            a_mass = ToArr(a, a_mass);
            b_mass = ToArr(b, b_mass);
            string a1 = ToStr(a_mass);
            Console.WriteLine(a_mass);
            Console.WriteLine(a1);
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
                Array.Reverse(a32);
                Array.Reverse(a_mass);
            }
            return a_mass;
        }



        public static string ToStr(ulong[] a_mass) {
            string x = null;
            for (int i = 0; i < a_mass.Length; i++) {
                x = (a_mass[i].ToString("X").PadLeft(8, '0')) + x;
            }
         
            return x;


        }





    }
}


