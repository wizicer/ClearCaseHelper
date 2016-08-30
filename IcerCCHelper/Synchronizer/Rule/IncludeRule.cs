namespace IcerDesign.CCHelper
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Text.RegularExpressions;

    [DebuggerDisplay("Include {reRule}")]
    internal class IncludeRule : FilterRule
    {
        private Regex reRule;

        public IncludeRule(string rule)
        {
            this.reRule = new Regex(WildcardToRegex(rule), RegexOptions.IgnoreCase);
        }

        public override Rule MatchRule(string input)
        {
            return reRule.IsMatch(input) ? Rule.Include : Rule.NotApply;
        }
    }
}