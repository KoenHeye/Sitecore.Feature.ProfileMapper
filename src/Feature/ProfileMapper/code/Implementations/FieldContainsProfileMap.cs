using Sitecore.Analytics.Data;
using Sitecore.Data.Items;
using Sitecore.Feature.ProfileMapper.Abstractions;
using Sitecore.Feature.ProfileMapper.Models;
using System;

namespace Sitecore.Feature.ProfileMapper.Implementations
{
    public class FieldContainsProfileMap : ProfileMap, IProfileMap
    {
        public TrackingField Evaluate(Item mapItem, Item contextItem)
        {
            // the page context item should have the field that the field contains profile map is inspecting

            if (contextItem.Fields[mapItem[Templates.FieldContainsProfileMap.Fields.ContextItemField]] == null)
                return null;

            // the page context item field value should contain the string to match (case insensitively)

            foreach (var stringToMatch in mapItem[Templates.FieldContainsProfileMap.Fields.StringsToMatch].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (contextItem[mapItem[Templates.FieldContainsProfileMap.Fields.ContextItemField]].IndexOf(stringToMatch.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    return GetTrackingField(mapItem);
            }

            return null;
        }

        public bool IsValid(Item mapItem)
        {
            if (string.IsNullOrWhiteSpace(mapItem[Templates.FieldContainsProfileMap.Fields.ContextItemField]))
                return false;

            if (string.IsNullOrWhiteSpace(mapItem[Templates.FieldContainsProfileMap.Fields.StringsToMatch]))
                return false;

            return true;
        }
    }
}