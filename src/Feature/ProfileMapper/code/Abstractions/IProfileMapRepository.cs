using Sitecore.Data;
using Sitecore.Data.Items;
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.Feature.ProfileMapper.Abstractions
{
    public interface IProfileMapRepository
    {
        IEnumerable<IGrouping<ID, Item>> GetProfileMapSets();
    }
}
