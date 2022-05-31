using Lab1_AaDS;
using Lab21_AaDS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2_3_AaDS
{
    public class Flight
    {
        public readonly string destination;
        public readonly int cost;

        public Flight(string dest, int cost)
        {
            this.destination = dest;
            this.cost = cost;
        }
    }


    public static class Pathfinder
    {
        public static NotADictionary<string, NotADictionary<string, NotAList<Flight>>> GenPathMatrix(string listOfFlights)
        {
            var matrix = new NotADictionary<
                string,
                NotADictionary<
                    string,
                    NotAList<
                    Flight>>>();

            var cities = new NotAList<string>();


            ParsingUtility.LoadFlightDataFromString(listOfFlights, matrix, cities);


            Func<NotADictionary<string, NotAList<Flight>>> defaultInit1 = () => new NotADictionary<string, NotAList<Flight>>();
            Func<NotAList<Flight>> defaultInit2 = () => new NotAList<Flight>();

            foreach (string transitCity in cities)
            {
                foreach (string from in cities)
                {
                    foreach (string to in cities)
                    {
                        int costOld =
                            matrix
                            .LazyGet(from, defaultInit1)
                            .LazyGet(to, defaultInit2)
                            .SumOf((el) => el.cost);
                        if (costOld == 0)
                            costOld = Int32.MaxValue;

                        var subPath1 =
                            matrix
                            .LazyGet(from, defaultInit1)
                            .LazyGet(transitCity, defaultInit2);

                        var subPath2 =
                            matrix
                            .LazyGet(transitCity, defaultInit1)
                            .LazyGet(to, defaultInit2);

                        int costNew1 = subPath1.SumOf(el => el.cost);
                        int costNew2 = subPath2.SumOf(el => el.cost);
                        int costNew;

                        if (costNew1 == 0 || costNew2 == 0)
                            costNew = Int32.MaxValue;
                        else
                            costNew = costNew1 + costNew2;

                        if (costNew < costOld)
                            matrix[from][to] = ListUtility.Concat(subPath1, subPath2);
                    }
                }
            }

            return matrix;
        }
    }
}
