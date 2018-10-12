using Sitecore.Analytics.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Feature.ProfileMapper.Abstractions;
using Sitecore.Feature.ProfileMapper.Models;

namespace Sitecore.Feature.ProfileMapper.Implementations
{
    public class IsDescendantOfProfileMap : ProfileMap, IProfileMap
    {
        public TrackingField Evaluate(Item mapItem, Item contextItem)
        {
            var ancestorItem = ((ReferenceField)mapItem.Fields[Templates.IsDescendantOfProfileMap.Fields.Ancestor]).TargetItem;

            if (ancestorItem == null)
                return null;

            if (!contextItem.Paths.IsDescendantOf(ancestorItem))
                return null;

            return GetTrackingField(mapItem);
        }

        public bool IsValid(Item mapItem)
        {
            var field = (ReferenceField)mapItem.Fields[Templates.IsDescendantOfProfileMap.Fields.Ancestor];

            return field != null && field.TargetItem != null;
        }
    }
}