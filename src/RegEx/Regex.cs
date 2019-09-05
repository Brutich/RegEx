using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Dynamo.Graph.Nodes;
using CSRegex = System.Text.RegularExpressions.Regex;
using CSRegexOptions = System.Text.RegularExpressions.RegexOptions;


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

        private Regex(String pattern, CSRegexOptions options)
        {
            Pattern = new CSRegex(pattern, options);
        }

        /// <summary>
        ///     Regular expression by pattern.
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="options"></param>
        [NodeCategory("Create")]
        public static Regex ByPattern(string pattern, Option[] options)
        {
            if (pattern == null)
                return null;

            if (options == null | !options.Any())
                return new Regex(pattern);

            CSRegexOptions regexOptions = options.First().RegexOption;
            options = options.Skip(1).ToArray();

            foreach (var opt in options)
                regexOptions = regexOptions | opt.RegexOption;

            return new Regex(pattern, regexOptions);
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


    /// <summary>
    /// Option for regular expression.
    /// </summary>
    public class Option
    {
        internal CSRegexOptions RegexOption { get; set; }

        private Option(CSRegexOptions option)
        {
            RegexOption = option;
        }


        /// <summary>
        /// Create regular expression option by name.
        /// </summary>
        /// <param name="optionName"></param>
        /// <returns></returns>
        [NodeCategory("Create")]
        public static Option ByName(String optionName)
        {
            switch (optionName)
            {
                case "IgnoreCase":
                    return new Option(CSRegexOptions.IgnoreCase);
                case "Singleline":
                    return new Option(CSRegexOptions.Singleline);
                case "Multiline":
                    return new Option(CSRegexOptions.Multiline);
                case "Compiled":
                    return new Option(CSRegexOptions.Compiled);
                case "ExplicitCapture":
                    return new Option(CSRegexOptions.ExplicitCapture);
                case "IgnorePatternWhitespace":
                    return new Option(CSRegexOptions.IgnorePatternWhitespace);
                case "CultureInvariant":
                    return new Option(CSRegexOptions.CultureInvariant);
                case "ECMAScript":
                    return new Option(CSRegexOptions.ECMAScript);
                case "RightToLeft":
                    return new Option(CSRegexOptions.RightToLeft);
                case "None":
                    return new Option(CSRegexOptions.None);
                default:
                    throw new FormatException("Name is incorrect.");
            }
        }
    }
}
