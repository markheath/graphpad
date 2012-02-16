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
            Regex regularExpression = new Regex(@"([\-\<\>])");
            
            var lines = graphMarkup.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

            foreach (var line in lines.InterleaveWith("%NEWLINE%"))
            {
                foreach (var token in regularExpression.Split(line))
                {
                    yield return token.Trim();
                }
            }
        }

    }

    static class Extensions
    {
        public static IEnumerable<T> InterleaveWith<T>(this IEnumerable<T> sequence, T separator)
        {
            var enumerator = sequence.GetEnumerator();
            bool finished = !enumerator.MoveNext();
            
            while (!finished)
            {
                yield return enumerator.Current;
                if (enumerator.MoveNext())
                    yield return separator;
                else
                    finished = true;
            }

        }
    }

}
