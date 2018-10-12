using Sitecore.Feature.ProfileMapper.Abstractions;
using Sitecore.Analytics.Data;
using Sitecore.Data.Items;
using Sitecore.Feature.ProfileMapper.Models;
using System;
using System.Linq;
using Sitecore.Data.Fields;

namespace Sitecore.Feature.ProfileMapper.Implementations
{
    public class FieldReferencesProfileMap : ProfileMap, IProfileMap
    {
        public TrackingField Evaluate(Item mapItem, Item contextItem)
        {
            // the page context item should have the field that the field reference profile map is inspecting

            if (contextItem.Fields[mapItem[Templates.FieldReferencesProfileMap.Fields.ContextItemField]] == null)
                return null;

            // the page context item should contain the guid of the reference items being targeted by the profile map, and contain all item references if configured to do so

            var contextReferences = contextItem[mapItem[Templates.FieldReferencesProfileMap.Fields.ContextItemField]].Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);            

            if (!contextReferences.Any())
                return null;

            var mappedReferences = mapItem[Templates.FieldReferencesProfileMap.Fields.ReferencesItems].Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            var matchAll = ((CheckboxField)mapItem.Fields[Templates.FieldReferencesProfileMap.Fields.MatchAll]).Checked;

            if (matchAll && mappedReferences.All(x => contextReferences.Any(y => y.Equals(x, StringComparison.OrdinalIgnoreCase))))
                return GetTrackingField(mapItem);

            if (contextReferences.Any(x => mappedReferences.Any(y => y.Equals(x, StringComparison.OrdinalIgnoreCase))))
                return GetTrackingField(mapItem);

            return null;
        }

        public bool IsValid(Item mapItem)
        {
            if (string.IsNullOrWhiteSpace(mapItem[Templates.FieldReferencesProfileMap.Fields.ContextItemField]))
                return false;

            if (string.IsNullOrWhiteSpace(mapItem[Templates.FieldReferencesProfileMap.Fields.ReferencesItems]))
                return false;

            return true;
        }
    }
}