using Sitecore.Sites;
using System;

namespace Sitecore.Feature.ProfileMapper.Extensions
{
    public static class SiteExtensions
    {
        public static bool ProfileMappingEnabled(this SiteContext site)
        {
            return site.Properties["profileMappingEnabled"] != null && site.Properties["profileMappingEnabled"].Equals("true", StringComparison.OrdinalIgnoreCase);
        }
    }
}