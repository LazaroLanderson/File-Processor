using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Processor.Models
{
    public class Secret
    {

        public string Value { get; set; } = string.Empty;
        public string Encrypted { get; set; } = string.Empty;
        public int LongestSubstring { get; set; }
        public int DuplicateCount { get; set; }
        public bool AlmostPalindrome { get; set; }

    }
}
