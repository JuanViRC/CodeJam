using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeJam.Year2018.Qualification_Round.Saving_The_Universe_Again
{
    public class Solution
    {

        private readonly StringReader input;
        private readonly StringWriter output;

        public int TestNumber { get; set; }
        public int HacksNumber { get; set; }
        public int RobotPower { get; set; }
        public StringBuilder RobotCommands { get; set; }
        public int Shield { get; set; }

        private class TestData
        {
            public int Shield { get; set; }
            public string RobotCommands { get; set; }
        }

        private class ImpossibleException : Exception { }

        private IEnumerable<TestData> GetProblemTests()
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

        public Solution(StringReader input, StringWriter output)
        {
            this.input = input;
            this.output = output;            
        }

        public void Start()
        {
            input.ReadLine(); // Line with number of test cases

            TestNumber = 0;

            var expression = new Regex(@"(?<Shield>\d)+\s(?<RobotCommands>[SC]+)");
            string line;

            while(!string.IsNullOrEmpty(line = input.ReadLine()))
            {
                var match = expression.Match(line);
                var test = new TestData
                {
                    Shield = int.Parse(match.Groups["middle"].Value),
                    RobotCommands = match.Groups["RobotCommands"].Value
                };

                TestNumber++;

                Solve(test);
            }
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
                    HacksNumber = -1;
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
                if (RobotCommands[i] == 'S' && RobotCommands[i-1] == 'C')
                {
                    RobotCommands[i] = 'C';
                    RobotCommands[i-1] = 'S';
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
            int currentShield = Shield;
            int currectRobotPower = RobotPower;

            for (var i = 0; i < RobotCommands.Length; i++)
            {
                if (RobotCommands[i] == 'C') currectRobotPower *= 2;

                else if (RobotCommands[i] == 'S') currentShield -= currectRobotPower;

                if (currentShield < 0) return false;
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
            var solution = HacksNumber < 0 ? "IMPOSSIBLE" : HacksNumber.ToString();
            output.WriteLine($"Case #{TestNumber}: {solution}");
        }

    }
}
