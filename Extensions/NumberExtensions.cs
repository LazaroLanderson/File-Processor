using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Processor.Extensions
{
    public static class NumberExtensions
    {

        public static int CalculateM35(this int value)
        {
            if (value < 0)
            {
                return 0;
            }

            int sum = 0;

            for (int i = 1; i < value; i++)
            {
                if (i % 3 == 0 || i % 5 == 0)
                {
                    sum += i;
                }
            }

            return sum;

        }


        public static string ToReadableTime(this int seconds)
        {
            int hours = seconds / 3600;
            int remainder = seconds % 3600;
            int minutes = remainder / 60;
            int remainingSeconds = remainder % 60;

            return $"{hours:D2}:{minutes:D2}:{remainingSeconds:D2}";
        }


    }
}
