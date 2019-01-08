using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba1_2 {

    public class Program {
        static void Main(string[] args) {

            string a = "2E151CB";
            string b = "10C2";
            string n = "23413";
            a = Add0(a);
            b = Add0(b);

            var a_mass = new ulong[a.Length / 8];
            var b_mass = new ulong[b.Length / 8];
            a_mass = ToArr(a, a_mass);
            b_mass = ToArr(b, b_mass);

            /* //sum
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
                 if (q = LongCmp(a_mass, b_mass) == 0) { Console.WriteLine("Sub= 0"); }
                 else { Console.WriteLine("Sub= " + subs); };
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

             //Gorner
             var go = new ulong[0];
             go = Gorner(a_mass, b_mass);
             string gor = ToStr(go);
             Console.WriteLine("Gorner = " + gor);


             //GCD
             var GC = new ulong[0];
             GC = gcd(a_mass, b_mass);
             string GCD = ToStr(GC);
             Console.WriteLine("GCD = " + GCD);


             //Barrett
             var ba = new ulong[0];
             ba = BarrettReduction(a_mass, b_mass);
             string bar = ToStr(ba);
             Console.WriteLine("Barret = " + bar);*/

            //GornerBarrett
            n = Add0(n);
            var N = new ulong[n.Length / 8];
            N = ToArr(n, N);
            var GB = new ulong[0];
            GB = GornerBarrett(a_mass, b_mass, N);
            string gb = ToStr(GB);
            Console.WriteLine("otvet = " + gb);











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

        public static ulong[] Del0(ulong[] a) {
            int i = a.Length - 1;
            while (a[i] == 0) {
                i--;
            }
            ulong[] b = new ulong[i + 1];
            Array.Copy(a, b, i + 1);
            return b;
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




        static int LongCmp(ulong[] a, ulong[] b) {
            int max = 0;
            if (a.Length < b.Length) { max = b.Length; }
            else { max = a.Length; };
            Array.Resize(ref a, max);
            Array.Resize(ref b, max);
            for (int i = a.Length - 1; i > -1; i--) {
                if (a[i] < b[i]) { return -1; }
                if (a[i] > b[i]) { return 1; }
            }

            return 0;
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

        private static ulong[] ShiftBitsToLow(ulong[] a, int b) {
            int t = b / 32;
            int n = b - t * 32;
            ulong[] c = new ulong[a.Length - t];
            ulong p, q = 0;
            for (int i = t; i < a.Length - 1; i++) {
                p = a[i];
                q = a[i + 1];
                p = p >> n;
                q = q << (64 - n);
                q = q >> (64 - n);
                c[i - t] = p | (q << 32 - n);
            }
            c[a.Length - t - 1] = a[a.Length - 1] >> n;
            return c;
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
            int i = a.Length - 1;
            int b = 0;
            for (i = a.Length - 1; a[i] == 0; i--) {
                if (i < 0) { return 0; };
            }
            var k = a[i];
            while (k > 0) {
                b++;
                k = k >> 1;
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



        public static ulong[] Gorner(ulong[] a, ulong[] b) {
            string strb = ToStr(b);
            ulong[] c = new ulong[1];
            c[0] = 0x1;
            ulong[][] p = new ulong[16][];
            p[0] = new ulong[1] { 1 };
            p[1] = a;
            for (int i = 2; i < 16; i++) {
                p[i] = Multi(p[i - 1], a);
                p[i] = Del0(p[i]);
            }
            for (int i = 0; i < strb.Length; i++) {
                c = Multi(c, p[Convert.ToInt32(strb[i].ToString(), 16)]);
                if (i != strb.Length - 1) {
                    for (int k = 1; k <= 4; k++) {
                        c = Multi(c, c);
                        c = Del0(c);
                    }
                }
            }
            return c;
        }







        ////////////////////////////////////////SECOND LAB///////////////////////////////////////////
        ///


        public static ulong[] gcd(ulong[] a, ulong[] b) {
            ulong[] Min, Max;
            var aa = a;
            var bb = b;
            int min;
            if (a.Length < b.Length) { min = a.Length; }
            else { min = b.Length; };
            ulong[] p = new ulong[min];
            p[0] = 0x1;
            string tt = "2";
            tt = Add0(tt);
            ulong[] x = new ulong[tt.Length / 8];
            ulong[] t = ToArr(tt, x);


            while (((aa[0] & 1) == 0) && ((bb[0] & 1) == 0)) {
                aa = Div(aa, t);
                bb = Div(bb, t);
                p = Multi(p, t);
            }
            while ((aa[0] & 1) == 0) {
                aa = Div(aa, t);
            }
            while (bb[0] != 0) {
                while ((bb[0] & 1) == 0) {
                    bb = Div(bb, t);
                }
                var CMP = LongCmp(aa, bb);
                if (CMP >= 0) {
                    Min = bb;
                    Max = aa;
                }
                else {
                    Min = aa;
                    Max = bb;
                }
                aa = Min;
                bb = (SubLong(Max, Min));
            }
            p = Multi(p, aa);
            return p;
        }


        public static ulong[] U(ulong[] n) {
            ulong[] u = new ulong[1];
            var k = 2 * BitLength(n);
            var b = new ulong[] { 0x01 };
            var up = LongShiftBitsToHigh(b, k);
            u = Div(up, n);
            return u;
        }


        public static ulong[] BarrettReduction(ulong[] x, ulong[] n, ulong[] u) {
           // var u = U(n);     
            var p = BitLength(n);
            var q = ShiftBitsToLow(x, p - 1);
            q = Multi(q, u);
            q = ShiftBitsToLow(q, p + 1);

            var r = SubLong(x, Multi(q, n));
            if (LongCmp(r, n) >= 0) {
                r = SubLong(r, n);
            }
            return r;
        }

        public static ulong[] Mod(ulong[] a, ulong[] b) {
            var p = BitLength(b);
            var ans = a;
            ulong[] x = new ulong[a.Length];
            ulong[] q = new ulong[a.Length];
            ulong[] y = new ulong[a.Length];            
            x[0] = 0x1;



            while (LongCmp(ans, b) >= 0) {
                var t = BitLength(ans);
                y = LongShiftBitsToHigh(b, t - p);
                if (LongCmp(ans, y) == -1) {
                    t = t - 1;
                    y = LongShiftBitsToHigh(b, t - p);
                }
                ans = SubLong(ans, y);
                q = AddLong(q, LongShiftBitsToHigh(x, t - p));
            }



            return ans;
        }

        public static ulong[] ModPower(ulong[] a, ulong[] b, ulong[] mod) {
            string strb = ToStr (b);
            ulong[] c = new ulong[1];
            c[0] = 0x1;
            ulong[][] p = new ulong[16][];
            p[0] = new ulong[1] { 1 };
            p[1] = a;
            for (int i = 2; i < 16; i++) {
                p[i] = Multi(p[i - 1], a);
                p[i] = Del0(p[i]);
            }

            for (int i = 0; i < strb.Length; i++) {
                c = Multi(c, p[Convert.ToInt32(strb[i].ToString(), 16)]);
                c = Mod(c, mod);
                if (i != strb.Length - 1) {
                    for (int k = 1; k <= 4; k++) {
                        c = Multi(c, c);
                        c = Mod(c, mod);
                        c = Del0(c);
                    }
                }
            }
            return c;
        }



        public static string ToStringBit(ulong[] a) {
            string result = "";
            ulong word;
            for (int i = 0; i < a.Length - 1; i++) {
                word = a[i];
                for (int j = 0; j != 64; j++, word >>= 1) {
                    ulong bit = word & 1;
                    result += bit.ToString();
                }
            }

            word = a[a.Length - 1];
            for (; word != 0; word >>= 1) {
                ulong bit = word & 1;
                result += bit.ToString();
            }
            var q = new string(result.ToCharArray().Reverse().ToArray());
            return q.TrimStart('0');
        }



        public static ulong[] GornerBarrett(ulong[] a, ulong[] b, ulong[] n) {
            string strb = ToStringBit(b);
            var u = U(n);
           
            ulong[] c = new ulong[1];
            c[0] = 0x1;

            for (int i = 0; i < strb.Length-1; i++) {
                if (strb[i] == '1') {
                    c = BarrettReduction(Multi(c, a), n, u);
                    c = Del0(c);
                }

                
                a = BarrettReduction(Multi(a, a), n, u);
                a = Del0(a);
                
            }
            return c;
        }




    }
}














































