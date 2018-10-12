using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Feature.ProfileMapper.Extensions;

namespace Sitecore.Feature.ProfileMapper.ContentSearch.ComputedFields
{
    public class IsProfileMap : IComputedIndexField
    {
        public string FieldName { get; set; }

        public string ReturnType { get; set; }

        public object ComputeFieldValue(IIndexable indexable)
        {
            var indexableItem = indexable as SitecoreIndexableItem;

            if (indexableItem == null)
                return null;

            var item = indexableItem.Item;

            if (item == null || !item.Paths.IsContentItem || !item.IsProfileMap())
                return null;

            return true;
        }
    }
}