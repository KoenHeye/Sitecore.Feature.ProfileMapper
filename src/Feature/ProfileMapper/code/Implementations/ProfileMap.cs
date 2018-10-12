using Sitecore.Analytics.Data;
using Sitecore.Data.Items;
using Sitecore.Feature.ProfileMapper.Models;

namespace Sitecore.Feature.ProfileMapper.Implementations
{
    public abstract class ProfileMap
    {
        public virtual TrackingField GetTrackingField(Item mapItem)
        {
            return new TrackingField(mapItem.Fields[FieldNames.TrackingField]);
        }
    }
}