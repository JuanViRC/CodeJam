using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeJam.Year2018.Qualification_Round.Trouble_Sort
{

    public class Program
    {
        static void Main(string[] args)
        {
            var probleQR2 = new Solution(Console.In, Console.Out);
            probleQR2.Start();
        }
    }

    public class Solution
    {
        private readonly TextReader input;
        private readonly TextWriter output;

        private readonly Regex testCaseRegex = new Regex(@"(?<Shield>\d+)\s(?<RobotCommands>[SC]+)");

        public ulong TestNumber { get; set; }

        public Solution(TextReader input, TextWriter output)
        {
            this.input = input;
            this.output = output;
        }

        public void Start()
        {
            ulong testCasesNumber = ulong.Parse(input.ReadLine());

            for (TestNumber = 1; TestNumber <= testCasesNumber; TestNumber++)
            {
                //var line = input.ReadLine();
                //var test = ConstructTestData(line);

                //Solve(test);
            }
        }

        private void Solve()
        {

            PrintSolution();
        }

        private void PrintSolution()
        {
            var solution = "";
            output.WriteLine($"Case #{TestNumber}: {solution}");
        }

    }
    
    public class ImpossibleException : Exception { }
}
