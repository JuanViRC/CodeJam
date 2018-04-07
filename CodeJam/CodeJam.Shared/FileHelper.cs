using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodeJam.Shared
{
    public class FileHelper
    {
        public static IEnumerable<string> ReadFile(string filePath)
        {
            return File.ReadAllLines(filePath);
        }

        public static ProblemFileDto GetProblemTests(string filePath)
        {
            var fileLines = ReadFile(filePath);

            return new ProblemFileDto
            {
                TestCasesNumber = int.Parse(fileLines.First()),
                TestCases = fileLines.Skip(1)
            };
        }

    }
}
