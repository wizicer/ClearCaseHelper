namespace IcerDesign.CCHelper
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Text.RegularExpressions;

    [DebuggerDisplay("Exclude {reRule}")]
    internal class ExcludeRule : FilterRule
    {
        private Regex reRule;

        public ExcludeRule(string rule)
        {
            this.reRule = new Regex(WildcardToRegex(rule), RegexOptions.IgnoreCase);
        }

        public override Rule MatchRule(string input)
        {
            return reRule.IsMatch(input) ? Rule.Exclude : Rule.NotApply;
        }
    }
}