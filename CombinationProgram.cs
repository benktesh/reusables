using System;
using System.Collections.Generic;
using System.Linq;

namespace Combination
{
    /// <summary>
    /// This class implements a combinaiton logic to get a cross-product of all the columns. 
    /// Such combination can be be useful when we have Ycolumns and each column has X values we want to generates 
    /// a combination of all values from each column.
    /// </summary>
    class CombinationProgram
    {
        /// <summary>
        /// returns combination
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> GetProduct<T>(IEnumerable<IEnumerable<T>> items)
        {
            IEnumerable<IEnumerable<T>> result = new[] { Enumerable.Empty<T>() };
            foreach (var sequence in items)
            {
                result =
                    result.SelectMany((l) => sequence, (l, r) => l.Concat(new[] { r
                    }));
            }
            return result.Select(item => item.ToList()).ToList().Distinct(new Comparer<T>())
                 .Where(p => p != null).ToList();
        }

        /// <summary>
        /// Driver progoram. For test purpose, crate a matrix defined in SizeX and creates combination from that point on. 
        /// </summary>
        static void Main()
        {

            //a symetrical matrix makes navigation easier. 
            int sizeX = 4; //maximum of any fields
            //each list is field
            List<List<string>> data = new List<List<string>>();

            //create data
            int res = 1;
            for (int i = 0; i < sizeX; i++)
            {
                List<string> d = new List<string>();
                for (int j = 0; j < sizeX; j++)
                {
                    d.Add(res.ToString());
                    res = res + 1;
                }
                data.Add(d);
            }
            Print(data);
            
            var p1 = GetProduct(data);
            Print(p1);
            Console.ReadKey();
        }

        /// <summary>
        /// pretty prints
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listOfList"></param>
        public static void Print<T>(IEnumerable<IEnumerable<T>> listOfList)
        {
            int i = 0;
            foreach (var list in listOfList)
            {
                foreach (var it in list)
                {
                    Console.Write(it + " ");
                }
                i++;
                Console.WriteLine($"({i})");
            }
            Console.WriteLine(" ");
        }
    }

    /// <summary>
    /// Equality comparer for list of lists
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Comparer<T> : IEqualityComparer<List<T>>
    {
        public bool Equals(List<T> x, List<T> y)
        {
            if (x == null && y == null)
            {
                return true;
            }

            if (x == null)
            {
                return false;
            }

            if (y == null)
            {
                return false;
            }

            return x.SequenceEqual(y);
        }


        public int GetHashCode(List<T> obj)
        {
            int hashCode = 0;

            for (var index = 0; index < obj.Count; index++)
            {
                hashCode ^= new { Index = index, Item = obj[index] }.GetHashCode();
            }

            return hashCode;
        }
    }
}
