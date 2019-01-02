using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba1_2 {
    class Program {
        static void Main(string[] args) {

            string a = "16D4CA8";
            string b = "0";
            a = Add0(a);
            b = Add0(b);

            Console.Write(a +  "\n");
            var a_mass = new ulong[a.Length / 8];
            var b_mass = new ulong[b.Length / 8];
            a_mass = ToArr(a, a_mass);
            b_mass = ToArr(b, b_mass);

            Console.Write(a_mass);

            Console.ReadKey();


        }

        public static string Add0(string a) {
           while (a.Length % 8 != 0) {
                a = "0" + a;
            }

            return a;
        }

        public static ulong[] ToArr(string a, ulong [] a_mass) {
            
            var a32 = new ulong[a.Length / 8];
            for (int i=0; i < a.Length; i += 8) {
                a32[i / 8] = Convert.ToUInt64(a.Substring(i, 8), 16);
                Array.Reverse(a32);
            }
            return a32;
        } 



        }





    }


