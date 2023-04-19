using Org.BouncyCastle.Math;

namespace podzial_cienie
{
    internal class Program
    {
        static BigInteger LagrangePoly(BigInteger x)
        {
            return x.Pow(2).Multiply(new BigInteger("7"))
                .Add(x.Multiply(new BigInteger("8"))
                .Add(new BigInteger("11").Mod(new BigInteger("13")))
                );
        }
        public static BigInteger modInverse(BigInteger a, BigInteger n)
        {
            return a.Add(n).ModInverse(n);
        }
        public static BigInteger recreate(BigInteger p, BigInteger[] xi, BigInteger[] mi)
        {
            // odwtorzenie sekretu z m2 - mi[1], m3 - mi[2], m5 - mi[4]
            BigInteger p1 = mi[1].Multiply((xi[2].Multiply(new BigInteger("-1")).Multiply(modInverse(xi[1].Subtract(xi[2]), p)).Multiply((xi[4].Multiply(new BigInteger("-1")).Multiply(modInverse(xi[1].Subtract(xi[4]), p)))))).Mod(p);
            BigInteger p2 = mi[2].Multiply((xi[1].Multiply(new BigInteger("-1")).Multiply(modInverse(xi[2].Subtract(xi[1]), p)).Multiply((xi[4].Multiply(new BigInteger("-1")).Multiply(modInverse(xi[2].Subtract(xi[4]), p)))))).Mod(p);
            BigInteger p3 = mi[4].Multiply((xi[1].Multiply(new BigInteger("-1")).Multiply(modInverse(xi[4].Subtract(xi[1]), p)).Multiply((xi[2].Multiply(new BigInteger("-1")).Multiply(modInverse(xi[4].Subtract(xi[2]), p)))))).Mod(p);

            return (p1.Add(p2).Add(p3)).Mod(p);
        }
        static void Main(string[] args)
        {
            BigInteger M = new BigInteger("11");
            // n - ilosc cieni
            BigInteger n = new BigInteger("5");
            // m - wartosc progowa
            // ilosc stopni wielomianu lagrange'a do wygenerowania = wartosc progowa - 1, dla m = 3, wychodzi 2, dla m = 4 wychodzi 3
            BigInteger m = new BigInteger("3");
            BigInteger p = new BigInteger("13");
            // a1 i a2 sa losowo wybrane
            BigInteger a1 = new BigInteger("8");
            BigInteger a2 = new BigInteger("7");

            BigInteger x1 = new BigInteger("1");
            BigInteger x2 = new BigInteger("2");
            BigInteger x3 = new BigInteger("3");
            BigInteger x4 = new BigInteger("4");
            BigInteger x5 = new BigInteger("5");
            BigInteger m1 = LagrangePoly(x1).Mod(p);
            BigInteger m2 = LagrangePoly(x2).Mod(p);
            BigInteger m3 = LagrangePoly(x3).Mod(p);
            BigInteger m4 = LagrangePoly(x4).Mod(p);
            BigInteger m5 = LagrangePoly(x5).Mod(p);

            BigInteger[] xi = { x1, x2, x3, x4, x5 };
            BigInteger[] mi = { m1, m2, m3, m4, m5 };
            foreach (var foo in mi)
            {
                Console.WriteLine(foo);
            }
            Console.WriteLine(recreate(p, xi, mi));
        }
    }
}