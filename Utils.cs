using Lab1_AaDS;
using Lab21_AaDS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2_3_AaDS
{
    public static class ParsingUtility
    {
        public static void LoadFlightDataFromString(string str, NotADictionary<string, NotADictionary<string, NotAList<Flight>>> matrix, NotAList<string> cities)
        {
            int start = 0;
            int parsedBlocks = 0;

            string from="";
            string to="";

            int cost = -1;
            int costReturn = -1;


            for (int i=0;i<str.Length;i++)
            {
                if (str[i] == ';')
                {
                    string block = str[start..i];
                    switch (parsedBlocks)
                    {
                        case 0:
                            from = block;
                            break;
                        case 1:
                            to = block;
                            break;
                        case 2:
                            cost = block == "N/A" ? -1 : Convert.ToInt32(block);
                            break;
                        case 3:
                            costReturn = block == "N/A" ? -1 : Convert.ToInt32(block);
                            break;
                        default:
                            break;
                    }
                    parsedBlocks++;
                    start = i + 1;
                }
                else if (str[i] == '\n')
                {
                    if (parsedBlocks != 4)
                        throw new FormatException();

                    if (cost != -1)
                    {
                        var flights = matrix
                            .LazyGet(from, () => new NotADictionary<string, NotAList<Flight>>())
                            .LazyGet(to, () => new NotAList<Flight>());

                        if (flights.Empty)
                            flights.Add(new Flight(to, cost));

                        else if (flights[0].cost > cost)
                            flights[0] = new Flight(to, cost);
                    }
                    if (costReturn != -1)
                    {
                        var flights = matrix
                             .LazyGet(to, () => new NotADictionary<string, NotAList<Flight>>())
                             .LazyGet(from, () => new NotAList<Flight>());

                        if (flights.Empty)
                            flights.Add(new Flight(from, costReturn));

                        else if (flights[0].cost > costReturn)
                            flights[0] = new Flight(from, costReturn);
                    }

                    cities.AddUnique(from);
                    cities.AddUnique(to);

                    from = "";
                    to = "";

                    parsedBlocks = 0;
                    start = i + 1;
                }
            }
        }
    }

    public static class ListUtility
    {
        public static int SumOf<T>(this NotAList<T> list, Func<T, int> value)
        {
            int sum = 0;
            foreach (var el in list)
                sum += value(el);

            return sum;
        }

        public static NotAList<T> Copy<T>(this NotAList<T> original)
        {
            var copy = new NotAList<T>();
            foreach (var el in original)
                copy.Add(el);

            return copy;
        }

        public static NotAList<T> Concat<T>(NotAList<T> left, NotAList<T> right)
        {
            var res = left.Copy();
            foreach (var el in right)
                res.Add(el);

            return res;
        }

        public static void AddUnique<T>(this NotAList<T> list, T val)
        {
            if (list.Contains(val))
                return;

            list.Add(val);
        }
    }
}
