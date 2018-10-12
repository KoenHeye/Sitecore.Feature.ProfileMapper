using Sitecore.Feature.ProfileMapper.Extensions;
using Sitecore.Pipelines.GetContentEditorWarnings;

namespace Sitecore.Feature.ProfileMapper.Pipelines.GetContentEditorWarnings
{
    public class ProfileMapTrackingFieldNotSet
    {
        public void Process(GetContentEditorWarningsArgs args)
        {
            var item = args.Item;

            if (item == null)
                return;

            if (!item.IsProfileMap() || item.HasTrackingField())
                return;
            
            GetContentEditorWarningsArgs.ContentEditorWarning key = args.Add();

            key.Key = "Sitecore.Pipelines.GetContentEditorWarnings.ProfileMapTrackingFieldNotSet";
            key.IsExclusive = false;
            key.Title = "No tracking field set on profile map.";
            key.Text = $"Before publishing a profile map you should associate it with a profile card or set custom profile key values.";
            key.HideFields = false;
        }
    }
}