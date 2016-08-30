namespace IcerDesign.CCHelper
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;

    internal abstract class FilterRule
    {
        public enum Rule
        {
            NotApply,
            Exclude,
            Include,
        }

        public static string WildcardToRegex(string pattern)
        {
            return "^" + Regex.Escape(pattern)
            .Replace(@"\*", ".*")
            .Replace(@"\?", ".")
            + "$";
        }

        public abstract Rule MatchRule(string input);
    }
}