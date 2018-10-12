using Sitecore.Configuration;
using Sitecore.ContentSearch;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Feature.ProfileMapper.Abstractions;
using Sitecore.Feature.ProfileMapper.Caching;
using Sitecore.Feature.ProfileMapper.ContentSearch;
using Sitecore.Feature.ProfileMapper.Extensions;
using Sitecore.Feature.ProfileMapper.Models;
using Sitecore.Sites;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.Feature.ProfileMapper.Repository
{
    public class DefaultProfileMapRepository : IProfileMapRepository
    {
        protected virtual IEnumerable<Item> GetProfileMaps()
        {
            if (Settings.GetBoolSetting("Feature.ProfileMapper.ContentSearch.Enabled", false))
            {
                return GetProfileMapsFromIndex();
            }

            return GetProfileMapsFromApi();
        }

        protected virtual IEnumerable<Item> GetProfileMapsFromApi()
        {
            var rootItemId = Settings.GetSetting("Feature.ProfileMapper.Repository.RootItem", "{306F0D82-93ED-43D5-9563-896DEC7753F2}");

            var rootItem = GetSiteProfileMapRoot(Context.Site);

            if (rootItem == null)
                yield break;

            foreach (var site in rootItem.Children.Where(x => x.TemplateID == Templates.ProfileMapSite.ID))
                foreach (var set in site.Children.Where(y => y.TemplateID == Templates.ProfileMapSet.ID))
                    foreach (var map in set.Children.Where(x => x.IsProfileMap()))
                        yield return map;            
        }

        protected virtual List<Item> GetProfileMapsFromIndex()
        {
            using (var context = ContentSearchManager.GetIndex(Settings.GetSetting("Feature.ProfileMapper.Repository.IndexName")).CreateSearchContext())
            {
                var rootItem = GetSiteProfileMapRoot(Context.Site);

                if (rootItem == null)
                    return new List<Item>();

                var query = context.GetQueryable<ProfileMapSearchResultItem>()
                    .Where(x => x["_latestversion"].Equals("1") && x.IsProfileMap && x.Paths.Contains(rootItem.ID));

                var maps = new List<Item>();

                foreach (var result in query.ToList())
                {
                    var item = result.GetItem();

                    if (item != null)
                        maps.Add(item);
                }

                return maps;
            }
        }

        public virtual IEnumerable<IGrouping<ID, Item>> GetProfileMapSets()
        {
            var cache = ProfileMapCache.GetSets();

            if (cache != null && cache.Any())
                return cache;

            var maps = GetProfileMaps();

            if (!maps.Any())
                return Enumerable.Empty<IGrouping<ID, Item>>();

            var sets = maps.GroupBy(x => x.ParentID);

            ProfileMapCache.Set(sets);

            return sets;
        }

        protected virtual Item GetSiteProfileMapRoot(SiteContext site)
        {
            var rootItemId = Settings.GetSetting("Feature.ProfileMapper.Repository.ProfileMapRoot", "/sitecore/content/Profile Mapper");

            var rootItem = ID.IsID(rootItemId) ? Context.Database.GetItem(ID.Parse(rootItemId)) : Context.Database.GetItem(rootItemId);

            if (rootItem == null)
                return null;

            return rootItem.Children.FirstOrDefault(x => x.TemplateID == Templates.ProfileMapSite.ID && x[Templates.ProfileMapSite.Fields.SiteName].Trim().Equals(site.Name.Trim(), StringComparison.OrdinalIgnoreCase));
        }
    }
}