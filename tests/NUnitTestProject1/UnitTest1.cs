using Lab1_AaDS;
using Lab2_3_AaDS;
using NUnit.Framework;
using System.Collections.Generic;

namespace NUnitTestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestBasics()
        {
            var expected = new List<string>()
            {
                "beta",
                "lambda",
                "delta",
                "omega"
            };

            string test = "alpha;omega;1000000;N/A;\n" +
                "alpha;beta;10;4;\n" +
                "beta;gamma;33;21;\n" +
                "alpha;gamma;23;2;\n" +
                "gamma;delta;2;2;\n" +
                "delta;lambda;2;1;\n" +
                "lambda;beta;1000;1000;\n" +
                "beta;omega;1000;1000;\n" +
                "lambda;beta;1;1;\n" +
                "gamma;omega;111;111;\n" +
                "delta;omega;1;1;\n" +
                "lambda;omega;100;100;";

            var matrix = Pathfinder.GenPathMatrix(test);
            var path = matrix["alpha"]["omega"];

            var areDifferent = false;
            int index = 0;
            foreach (var node in path)
            {
                if (node.destination != expected[index])
                    areDifferent = true;

                index++;
            }

            Assert.False(areDifferent);
        }

        [Test]
        public void TestNoPath()
        {
            string test = "alpha;omega;N/A;11111;\n" +
                "alpha;beta;10;4;\n" +
                "beta;gamma;33;21;\n" +
                "alpha;gamma;23;2;\n" +
                "gamma;delta;2;2;\n" +
                "delta;lambda;2;1;\n" +
                "lambda;beta;1000;1000;\n" +
                "beta;omega;N/A;1000;\n" +
                "lambda;beta;1;1;\n" +
                "gamma;omega;N/A;111;\n" +
                "delta;omega;N/A;1;\n" +
                "lambda;omega;N/A;100;";

            var matrix = Pathfinder.GenPathMatrix(test);
            var path = matrix["alpha"]["omega"];

            Assert.True(path.Empty);
        }
    }
}