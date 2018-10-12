using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;

namespace Sitecore.Feature.ProfileMapper.ContentSearch
{
    public class ProfileMapSearchResultItem : SearchResultItem
    {
        [IndexField("isprofilemap")]
        public bool IsProfileMap { get; set; }
    }
}