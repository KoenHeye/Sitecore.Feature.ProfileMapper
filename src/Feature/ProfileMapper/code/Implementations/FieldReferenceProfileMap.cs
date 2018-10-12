using Sitecore.Feature.ProfileMapper.Abstractions;
using Sitecore.Analytics.Data;
using Sitecore.Data.Items;
using Sitecore.Feature.ProfileMapper.Models;

namespace Sitecore.Feature.ProfileMapper.Implementations
{
    public class FieldReferenceProfileMap : ProfileMap, IProfileMap
    {
        public TrackingField Evaluate(Item mapItem, Item contextItem)
        {
            // the page context item should have the field that the field reference profile map is inspecting

            if (contextItem.Fields[mapItem[Templates.FieldReferenceProfileMap.Fields.ContextItemField]] == null)
                return null;

            // the page context item should contain the guid of the reference item being targeted by the profile map

            if (contextItem[mapItem[Templates.FieldReferenceProfileMap.Fields.ContextItemField]].IndexOf(mapItem[Templates.FieldReferenceProfileMap.Fields.ReferencesItem]) < 0)
                return null;

            return GetTrackingField(mapItem);
        }

        public bool IsValid(Item mapItem)
        {
            if (string.IsNullOrWhiteSpace(mapItem[Templates.FieldReferenceProfileMap.Fields.ContextItemField]))
                return false;

            if (string.IsNullOrWhiteSpace(mapItem[Templates.FieldReferenceProfileMap.Fields.ReferencesItem]))
                return false;

            return true;
        }
    }
}