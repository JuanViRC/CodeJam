using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeJam.Year2018.Qualification_Round.Saving_The_Universe_Again
{

    public class Program
    {

        static void Main(string[] args)
        {
            var probleQR1 = new Solution(Console.In, Console.Out);
            probleQR1.Start();            
        }
    }

    public class Solution
    {
        private readonly TextReader input;
        private readonly TextWriter output;

        private readonly Regex testCaseRegex = new Regex(@"(?<Shield>\d+)\s(?<RobotCommands>[SC]+)");

        public ulong TestNumber { get; set; }
        public ulong HacksNumber { get; set; }
        public ulong RobotPower { get; set; }
        public StringBuilder RobotCommands { get; set; }
        public ulong Shield { get; set; }

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
                var line = input.ReadLine();
                var test = ConstructTestData(line);

                Solve(test);
            }
        }

        private TestData ConstructTestData(string stringData)
        {
            var match = testCaseRegex.Match(stringData);
            return new TestData
            {
                Shield = ulong.Parse(match.Groups["Shield"].Value),
                RobotCommands = match.Groups["RobotCommands"].Value
            };
        }

        private void Solve(TestData test)
        {
            InitTestData(test);

            while (true)
            {
                try
                {
                    if (CanWeSurvive()) break;

                    HackRobot();
                }
                catch (ImpossibleException)
                {
                    HacksNumber = ulong.MaxValue;
                    break;
                }
            }

            PrintSolution();
        }


        private void HackRobot()
        {
            if (RobotCommands.Length < 2) throw new ImpossibleException();
            if (!CanRobotBeHacked()) throw new ImpossibleException();

            for (var i = 1; i < RobotCommands.Length; i++)
            {
                if (RobotCommands[i] == 'S' && RobotCommands[i - 1] == 'C')
                {
                    RobotCommands[i] = 'C';
                    RobotCommands[i - 1] = 'S';
                    HacksNumber++;
                    break;
                }
            }
        }

        private bool CanRobotBeHacked()
        {
            bool found_C = false;
            bool found_S = false;
            bool areAll_S_AtStart = true;

            char? lastChar = null;

            if (RobotCommands.Length < 2) return false;

            for (var i = 0; i < RobotCommands.Length; i++)
            {
                if (RobotCommands[i] == 'C') found_C = true;
                else if (RobotCommands[i] == 'S') found_S = true;

                if (lastChar.HasValue && lastChar.Value == 'C' && RobotCommands[i] == 'S') areAll_S_AtStart = false;

                lastChar = RobotCommands[i];
            }

            return found_C && found_S && !areAll_S_AtStart;
        }

        private bool CanWeSurvive()
        {
            ulong currentShield = Shield;
            ulong currectRobotPower = RobotPower;

            for (var i = 0; i < RobotCommands.Length; i++)
            {
                if (RobotCommands[i] == 'C') currectRobotPower *= 2;

                else if (RobotCommands[i] == 'S') currentShield -= currectRobotPower;

                if (currentShield == ulong.MaxValue) return false;
            }

            return true;
        }

        private void InitTestData(TestData test)
        {
            HacksNumber = 0;
            RobotPower = 1;
            Shield = test.Shield;
            RobotCommands = new StringBuilder(test.RobotCommands);
        }

        private void PrintSolution()
        {
            var solution = HacksNumber == ulong.MaxValue ? "IMPOSSIBLE" : HacksNumber.ToString();
            output.WriteLine($"Case #{TestNumber}: {solution}");
        }

    }

    public class TestData
    {
        public ulong Shield { get; set; }
        public string RobotCommands { get; set; }

        public static IEnumerable<TestData> GetProblemTests()
        {
            var tests = new List<TestData>();
            tests.Add(new TestData
            {
                Shield = 1,
                RobotCommands = "CS"
            });
            tests.Add(new TestData
            {
                Shield = 2,
                RobotCommands = "CS"
            });
            tests.Add(new TestData
            {
                Shield = 1,
                RobotCommands = "SS"
            });
            tests.Add(new TestData
            {
                Shield = 6,
                RobotCommands = "SCCSSC"
            });
            tests.Add(new TestData
            {
                Shield = 2,
                RobotCommands = "CC"
            });
            tests.Add(new TestData
            {
                Shield = 3,
                RobotCommands = "CSCSS"
            });

            return tests;
        }
    }

    public class ImpossibleException : Exception { }
}
