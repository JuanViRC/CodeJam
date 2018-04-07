using System;
using System.Collections.Generic;
using System.Text;

namespace CodeJam.Shared
{
    public class ProblemFileDto
    {
        public int TestCasesNumber { get; set; }
        public IEnumerable<string> TestCases { get; set; }
    }
}
