using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment
{
    public class Program
    {
        public static int total = 0;
        public enum UnitPrice
        {
            A = 50, B = 30, C = 20, D = 15
        }
        public enum Promotion
        {
            Three_A = 130, Two_B = 45 , One_C_One_D = 30
        }
        public enum PromotionOffer
        {
            A = 3, B = 2, C_D_Each = 1
        }
        public static void Promotion_A(int a)
        {
            int rem = a % (int)PromotionOffer.A;
            int quo = a / (int)PromotionOffer.A;

            if (rem == 0)
            {
                var val = quo * (int)Promotion.Three_A;
                total = total + val;
                Console.WriteLine("{0} * A   = {1} ", a, val);
            }
            else
            {
                var val = quo * (int)Promotion.Three_A ;
                var val2 = rem * (int)UnitPrice.A;
                total = total + val + val2;
                Console.WriteLine("{0} * A   = {1}  +  {2}", a, val, val2);
            }
        }
        public static void Promotion_B(int b)
        {
            int rem = b % (int)PromotionOffer.B;
            int quo = b / (int)PromotionOffer.B;

            if (rem == 0)
            {
                var val = quo * (int)Promotion.Two_B;
                total = total + val;
                Console.WriteLine("{0} * B   = {1} ", b, val);
            }
            else
            {
                var val = quo * (int)Promotion.Two_B;
                var val2 = rem * (int)UnitPrice.B;
                total = total + val+ val2;
                Console.WriteLine("{0} * B   = {1}  +  {2}", b, val, val2);
            }
        }
        public static void Promotion_C(int c_count, int d_count)
        {
            if(c_count < d_count)
            {
                Console.WriteLine("{0} * C   = {1}", c_count, "-");
            }
            else
            {
                var val = (c_count - d_count) * (int)UnitPrice.C;
                total = total + val;
                Console.WriteLine("{0} * C   = {1}", c_count, val);
            }
            
        }
        public static void Promotion_D(int d_count, int c_count)
        {
            int common = 0;
            if (d_count > c_count)
            {
                common = c_count % d_count;
                var val = common * (int)Promotion.One_C_One_D;
                total = total + val + (d_count - c_count) * (int)UnitPrice.D;

                Console.WriteLine("{0} * D   = {1}  +  {2}", d_count, val, (d_count - c_count) * (int)UnitPrice.D);
            }
            else
            {
                common = d_count % c_count;
                total = total + common * (int)Promotion.One_C_One_D;

                Console.WriteLine("{0} * D   = {1}", d_count, common * (int)Promotion.One_C_One_D);
            }
        }

        public static int Occurance(char occ, string input)
        {
            int count=0;
            for(int i = 0; i < input.Length; i++)
            {
                    if(input[i]== occ)
                    {
                        count++;
                    }
            }
            return count;
        }

        public static void ActivePromotionFor_SKU(string input)
        {
            Dictionary<string, int> pairs = new Dictionary<string, int>();
            
            List<string> SKU = input.Split(',').ToList();
            
            SKU.ForEach(
                     q =>
                     {
                         if (!pairs.ContainsKey(q))
                         {
                             pairs.TryAdd(q, Occurance(q.ElementAt(0), input));
                         };
                     }
                );

            foreach(var occ in pairs.OrderBy(q=>q.Key))
            {
                if(occ.Key == "A")
                {
                    Promotion_A(occ.Value);
                }
                else if(occ.Key == "B")
                {
                    Promotion_B(occ.Value);
                }
                else if (occ.Key == "C")
                {
                    Promotion_C(occ.Value, 
                                pairs.Where(q => q.Key.Equals("D")).Select(s => s.Value).FirstOrDefault()
                               );
                }
                else if (occ.Key == "D")
                {
                    Promotion_D(occ.Value,
                                pairs.Where(q => q.Key.Equals("C")).Select(s => s.Value).FirstOrDefault()
                                );
                }
                else
                {
                    throw new Exception("Invalid Input");
                }
            }
            Total();
        }

        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Please make the selection of products from: A,B,C,D");
                Console.WriteLine("---------------------------------------------------");
                Console.WriteLine("Example: A,B,C,D");
                string selection = Console.ReadLine();
                Console.WriteLine("---------------------------------------------------");
                ActivePromotionFor_SKU(selection);
                Console.ReadLine();
            }
            catch(Exception e)
            {
                Console.WriteLine("Invalid Input");
            }
        }

        public static void Total()
        {
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("Total:    =    {0}", total);
        }
    }
}
