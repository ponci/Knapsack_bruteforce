using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knapsack_bruteforce
{
    public class Attributes
    {
        int greater_weight;
        /// <summary>
        ///  greater weight
        /// </summary>
        public int GreaterWeight
        {
            get { return greater_weight; }
            set { greater_weight = value; }
        }

        int greater_price;
        /// <summary>
        /// greater price
        /// </summary>
        public int GreaterPrice
        {
            get { return greater_price; }
            set { greater_price = value; }
        }

        int seq;
        /// <summary>
        /// list index number of the answer
        /// </summary>
        public int Seq
        {
            get { return seq; }
            set { seq = value; }
        }

        int listSize;
        /// <summary>
        /// list size
        /// </summary>
        public int ListSize
        {
            get { return listSize; }
            set { listSize = value; }
        }


        /// <summary>
        /// Quantity of products
        /// </summary>
        public static int productsQuantity;

        /// <summary>
        /// Bag Size
        /// </summary>
        public static int Size;

        /// <summary>
        /// Vector of weights who was readed from the archive
        /// </summary>
        public static int[] Weight;

        /// <summary>
        /// Vector of prices who was readed from the archive
        /// </summary>
        public static int[] Price;

        public void printValues()
        {
            Console.WriteLine("Greater Weight: " + GreaterWeight + "  GreaterPrice: " + GreaterPrice + "  List Sequence: " + seq + "  List Size: " + ListSize);

        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Resolution of Knapsack problem using brute force\n\n");

            Calcs calc = new Calcs();
            Program prog = new Program();

            // Create new stopwatch.
            Stopwatch stopwatch = new Stopwatch();

            // Begin timing.
            stopwatch.Start();

            //Read Archive
            prog.ReadArc();

            //Calculate the answer
            calc.calculate();

            // Stop timing.
            stopwatch.Stop();

            // Write result.
            Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);

            Console.ReadKey();
        }
        private void ReadArc()
        {
            StreamReader arc = new StreamReader("archive.txt");

            //read the number of products
            Attributes.productsQuantity = int.Parse(arc.ReadLine());

            //read the bag Size
            Attributes.Size = int.Parse(arc.ReadLine());

            Attributes.Weight = new int[Attributes.productsQuantity];
            Attributes.Price = new int[Attributes.productsQuantity];

            for (int i = 0; i < Attributes.productsQuantity; i++)
            {
                string[] value = arc.ReadLine().Split(' ');
                Attributes.Weight[i] = int.Parse(value[0]);
                Attributes.Price[i] = int.Parse(value[1]);
            }
        }

    }

    public class Calcs : Attributes
    {
        public void calculate()
        {
            List<int[]> listVec = calcCombinations();
            ListSize = listVec.Count;

            for (int i = 0; i < listVec.Count; i++)
            {
                int weightTotal = 0; int priceTotal = 0;
                for (int o = 0; o < listVec[i].Length; o++)
                {
                    if (listVec[i][o] != 0)
                    {
                        weightTotal += Weight[o];
                        priceTotal += Price[o];
                    }

                }

                if (priceTotal > GreaterPrice && weightTotal <= Size)
                {
                    GreaterWeight = weightTotal;
                    GreaterPrice = priceTotal;
                    Seq = i;
                }

            }

            printValues();
        }
        private List<int[]> calcCombinations2(int maxValue, int n, int[] vector, List<int[]> listVec)
        {
            n = n + 1;
            if (n >= maxValue)
            {
                return listVec;
            }
            else
            {
                int ZeroCount = 0;
                for (int i = 0; i < vector.Count(); i++)
                {
                    if (vector[i] == 0)
                        ZeroCount++;
                }
                if (ZeroCount >= 2)
                {
                    vector[n] = 1;
                    listVec.Add((int[])vector.Clone());
                    listVec = calcCombinations3(n, maxValue, (int[])vector.Clone(), listVec);
                }
                return calcCombinations2(maxValue, n, vector, listVec);
            }
        }
        private List<int[]> calcCombinations3(int maxValue, int n, int[] vector, List<int[]> listVec)
        {
            n = n - 1;
            if (n <= maxValue + 1)
            {
                return listVec;
            }
            else
            {
                int ZeroCount = 0;
                for (int i = 0; i < vector.Count(); i++)
                {
                    if (vector[i] == 0)
                        ZeroCount++;
                }
                if (ZeroCount >= 2)
                {
                    vector[n] = 1;
                    listVec.Add((int[])vector.Clone());
                }
                return calcCombinations3(maxValue, n, vector, listVec);
            }
        }
        private List<int[]> calcCombinations()
        {
            int maxVal = Attributes.productsQuantity;
            List<int[]> listVec = new List<int[]>();
            int val = ((int)Math.Pow(2, maxVal)) - 3;
            for (int p = -1; p < val; p++)
            {


                int[] vector = new int[maxVal];
                for (int l = 0; l < maxVal; l++)
                {
                    vector[l] = 0;
                }
                calcCombinations2(maxVal, p, vector, listVec);


            }

            int[] vector2 = new int[maxVal];
            for (int l = 0; l < maxVal; l++)
            {
                vector2[l] = 1;
            }
            listVec.Add(vector2);
            return listVec;
        }
    }
}
