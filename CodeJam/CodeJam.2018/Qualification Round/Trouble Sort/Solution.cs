using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                var numbersInList = uint.Parse(input.ReadLine());
                var numbersList = input.ReadLine();
                var test = ConstructTestData(numbersInList, numbersList);

                Solve(test);
            }
        }

        private uint[] ConstructTestData(uint count, string stringNumberList)
        {
            return stringNumberList.Split(' ').Select(n => uint.Parse(n)).ToArray();
        }

        private void Solve(uint[] numbersList)
        {
            while (true)
            {
                if (!OrderList(numbersList))
                {
                    break;
                }
            }

            var errorPosition = FindErrorInSortedList(numbersList);

            PrintSolution(errorPosition);
        }

        private int FindErrorInSortedList(uint[] numbersList)
        {
            for (var i = 0; i < numbersList.Length - 1; i++)
            {
                if (numbersList[i] > numbersList[i + 1]) return i;
            }

            return -1;
        }

        private bool OrderList(uint[] numbersList)
        {
            bool wasListSorted = false;
            var endPosition = numbersList.Length - 2;
            for (var i = 0; i < endPosition; i++)
            {
                wasListSorted = wasListSorted || SwapNumbers(i, i + 2, numbersList);
            }
            return wasListSorted;
        }

        private bool SwapNumbers(int startPosition, int endPosition, uint[] numbersList)
        {
            if (numbersList[startPosition] <= numbersList[endPosition]) return false;
            var temp = numbersList[startPosition];
            numbersList[startPosition] = numbersList[endPosition];
            numbersList[endPosition] = temp;
            return true;
        }

        private void PrintSolution(int errorPosition)
        {
            var solution = errorPosition == -1 ? "OK" : errorPosition.ToString() ;
            output.WriteLine($"Case #{TestNumber}: {solution}");
        }

    }

    public class ImpossibleException : Exception { }
}
