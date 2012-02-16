using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GraphPad.Logic
{
    class Tokenizer
    {
        // helpful article http://en.csharp-online.net/CSharp_Regular_Expression_Recipes%E2%80%94A_Better_Tokenizer

        public static IEnumerable<string> Tokenize(string graphMarkup)
        {
            Regex regularExpression = new Regex(@"([\-\<\>\r\n])");
            return (regularExpression.Split(graphMarkup).
                Select(x => x.Trim()));
        }

    }

}
