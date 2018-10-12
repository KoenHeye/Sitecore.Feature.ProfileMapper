using Sitecore.Analytics.Data;
using Sitecore.Data.Items;
using Sitecore.Feature.ProfileMapper.Abstractions;
using Sitecore.Feature.ProfileMapper.Models;
using Sitecore.Rules;

namespace Sitecore.Feature.ProfileMapper.Implementations
{
    public class RulesProfileMap : ProfileMap, IProfileMap
    {
        public TrackingField Evaluate(Item mapItem, Item contextItem)
        {
            var context = new RuleContext
            {
                Item = contextItem
            };

            foreach (Rule<RuleContext> rule in RuleFactory.GetRules<RuleContext>(new[] { mapItem }, Templates.RulesProfileMap.Fields.Rules.ToString()).Rules)
            {
                if (rule.Condition != null)
                {
                    var stack = new RuleStack();

                    rule.Condition.Evaluate(context, stack);

                    if (context.IsAborted)
                        continue;

                    if ((stack.Count != 0) && ((bool)stack.Pop()))
                        return GetTrackingField(mapItem);
                }
            }

            return null;
        }

        public bool IsValid(Item mapItem)
        {
            return !string.IsNullOrWhiteSpace(mapItem[Templates.RulesProfileMap.Fields.Rules]);
        }
    }
}