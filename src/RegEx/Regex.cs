using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Dynamo.Graph.Nodes;
using CSRegex = System.Text.RegularExpressions.Regex;


namespace RegularExpressions
{
    /// <summary>
    /// Represents an regular expression.
    /// </summary>
    public class Regex
    {
        private CSRegex Pattern { get; set; }
        
        private Regex(String pattern)
        {
            Pattern = new CSRegex(pattern);
        }

        /// <summary>
        ///     Regular expression by pattern.
        /// </summary>
        /// <param name="pattern"></param>
        [NodeCategory("Create")]
        public static Regex ByPattern(String pattern)
        {
            return new Regex(pattern);
        }


        /// <summary>
        ///     Indicates whether the regular expression finds a match in the input string.
        /// </summary>
        /// <param name="regex"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsMatch(Regex regex, String input)
        {
            return regex.Pattern.IsMatch(input);
        }


        /// <summary>
        ///     Searches an input string for all occurrences of a regular expression and returns all the matches.
        /// </summary>
        /// <param name="regex"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string[] Matches(Regex regex, String input)
        {
            var matches = new List<string>();

            foreach (System.Text.RegularExpressions.Match match in regex.Pattern.Matches(input))
                matches.Add(match.Value);

            return matches.ToArray();
        }


        /// <summary>
        ///     In a specified input string, replaces strings that match a regular expression pattern with a specified replacement string.
        /// </summary>
        /// <param name="regex"></param>
        /// <param name="input"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        public static String Replace(Regex regex, String input, String replacement)
        {
            return regex.Pattern.Replace(input, replacement);
        }


        /// <summary>
        ///     Splits an input string into an array of substrings at the positions defined by a regular expression match.
        /// </summary>
        /// <param name="regex"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string[] Split(Regex regex, String input)
        {
            return regex.Pattern.Split(input);
        }
    }
}
