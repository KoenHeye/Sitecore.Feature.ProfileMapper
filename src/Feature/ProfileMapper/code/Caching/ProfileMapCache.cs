using Sitecore.Caching;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.Feature.ProfileMapper.Caching
{
    public class ProfileMapCache : CustomCache
    {
        protected ProfileMapCache(long maxSize) : base(Settings.GetSetting("Feature.ProfileMapper.Repository.ProfileMapCache.Name"), maxSize)
        {

        }

        public static readonly ICache Instance = new ProfileMapCache(StringUtil.ParseSizeString(Settings.GetSetting("Feature.ProfileMapper.Repository.ProfileMapCache.MaxSize"))).InnerCache;

        public static void Set(IEnumerable<IGrouping<ID, Item>> profileMapSets)
        {
            if (Settings.GetBoolSetting("Feature.ProfileMapper.Repository.ProfileMapCache.Enabled", true))
            {
                Instance.Add(GenerateCacheKey(), profileMapSets);
            }
        }

        public static IEnumerable<IGrouping<ID, Item>> GetSets()
        {
            if (Settings.GetBoolSetting("Feature.ProfileMapper.Repository.ProfileMapCache.Enabled", true))
            {
                return Instance.GetValue(GenerateCacheKey()) as IEnumerable<IGrouping<ID, Item>>;
            }

            return null;
        }

        private static string GenerateCacheKey()
        {
            return $"{Settings.GetSetting("Feature.ProfileMapper.Repository.ProfileMapCache.SetsKey")}.{Context.Site.Name}";
        }
    }
}