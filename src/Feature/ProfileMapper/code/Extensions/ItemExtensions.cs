using Sitecore.Data.Items;
using Sitecore.Feature.ProfileMapper.Models;
using System.Linq;

namespace Sitecore.Feature.ProfileMapper.Extensions
{
    public static class ItemExtensions
    {
        public static bool IsProfileMap(this Item item)
        {
            return item.Template.BaseTemplates.Any(x => x.ID == Templates.ProfileMap.ID);
        }

        public static bool HasTrackingField(this Item item)
        {
            return !string.IsNullOrWhiteSpace(item[FieldNames.TrackingField]);
        }
    }
}