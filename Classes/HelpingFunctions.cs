using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBOT.Classes {
    public class HelpingFunctions {
        public static bool isNumeric(string test) {
            int thisOut;
            return (int.TryParse(test, out thisOut));
        }
        public static int CharCount(string Origstring, string Chars) {
            int Count = 0;

            for (int i = 0; i < Origstring.Length - 1; i++) {
                if (Origstring.Substring(i, 1) == Chars)
                    Count++;
            }

            return Count;
        }
    }
}
