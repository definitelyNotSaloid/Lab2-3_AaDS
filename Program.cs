using Lab1_AaDS;
using Lab21_AaDS;
using System;
using System.IO;
using System.Text;

namespace Lab2_3_AaDS
{
    

    class Program
    {
        public static string inputPath = "../../../input/data.txt";     // well... this path is only valid when running from ide. 
                                                                        // but its not like im going pack and publish it, so whatever
        static void Main(string[] args)
        {
            NotADictionary<string, NotADictionary<string, NotAList<Flight>>> matrix;
            using (var file = File.OpenRead(inputPath))
            {
                byte[] buff = new byte[file.Length];
                file.Read(buff);
    
                matrix = Pathfinder.GenPathMatrix(Encoding.Default.GetString(buff));
            }
            string source = Console.ReadLine();
            string dest = Console.ReadLine();

            Console.Write($"{source}");
            foreach (var flight in matrix[source][dest])
                Console.Write($" => {flight.destination} ({flight.cost})");
            Console.WriteLine();
            Console.WriteLine($"total cost = {matrix[source][dest].SumOf(el => el.cost)}");

        }
    }
}
