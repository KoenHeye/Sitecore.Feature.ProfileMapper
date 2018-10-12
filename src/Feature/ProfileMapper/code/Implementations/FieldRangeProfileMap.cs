using Sitecore.Analytics.Data;
using Sitecore.Data.Items;
using Sitecore.Feature.ProfileMapper.Abstractions;
using Sitecore.Feature.ProfileMapper.Models;

namespace Sitecore.Feature.ProfileMapper.Implementations
{
    public class FieldRangeProfileMap : ProfileMap, IProfileMap
    {
        public TrackingField Evaluate(Item mapItem, Item contextItem)
        {
            // the page context item should have the field that the field reference profile map is inspecting

            if (contextItem.Fields[mapItem[Templates.FieldRangeProfileMap.Fields.ContextItemField]] == null)
                return null;

            // the page context item field value should be an integer and fall within the min and max values of the profile map

            if (!int.TryParse(contextItem[mapItem[Templates.FieldRangeProfileMap.Fields.ContextItemField]], out int value) || value == 0)
                return null;

            var min = int.Parse(mapItem[Templates.FieldRangeProfileMap.Fields.MinValue]);
            var max = int.Parse(mapItem[Templates.FieldRangeProfileMap.Fields.MaxValue]);

            if (value < min || value > max)
                return null;

            return GetTrackingField(mapItem);
        }

        public bool IsValid(Item mapItem)
        {
            if (string.IsNullOrWhiteSpace(mapItem[Templates.FieldRangeProfileMap.Fields.ContextItemField]))
                return false;

            if (string.IsNullOrWhiteSpace(mapItem[Templates.FieldRangeProfileMap.Fields.MinValue]))
                return false;

            if (string.IsNullOrWhiteSpace(mapItem[Templates.FieldRangeProfileMap.Fields.MaxValue]))
                return false;

            return true;
        }
    }
}