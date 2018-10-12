using Sitecore.Analytics.Data;
using Sitecore.Data.Items;

namespace Sitecore.Feature.ProfileMapper.Abstractions
{
    public interface IProfileMap
    {
        bool IsValid(Item mapItem);

        TrackingField Evaluate(Item mapItem, Item contextItem);
    }
}