namespace IcerDesign.CCHelper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static class RuleHelper
    {
        public static IEnumerable<FilterRule> GenRules(string input)
        {
            var list = new List<FilterRule>();
            var lines = input.Trim()
                .Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(_ => _.Trim());

            foreach (var line in lines)
            {
                if (line.StartsWith("!"))
                {
                    yield return new ExcludeRule(line.Substring(1).Replace(@"\!", "!"));
                }
                else
                {
                    yield return new IncludeRule(line.Replace(@"\!", "!"));
                }
            }
        }

        internal static bool MatchExclude(this FilterRule[] rules, string input, string root)
        {
            if (!root.EndsWith("\\")) root += "\\";
            var path = input.Substring(root.Length);
            //var flag = FilterRule.Rule.Include;
            var flag = true;
            foreach (var rule in rules)
            {
                if (rule.MatchRule(path) == FilterRule.Rule.Include)
                {
                    flag = true;
                }
                else if (rule.MatchRule(path) == FilterRule.Rule.Exclude)
                {
                    flag = false;
                }
            }

            return !flag;
        }
    }
}