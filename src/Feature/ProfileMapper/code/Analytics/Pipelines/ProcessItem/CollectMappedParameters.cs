using Sitecore.Analytics.Pipelines.ProcessItem;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Feature.ProfileMapper.Extensions;
using Sitecore.Feature.ProfileMapper.Models;
using Sitecore.Feature.ProfileMapper.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.Feature.ProfileMapper.Analytics.Pipelines.ProcessItem
{
    public class CollectMappedParameters : ProcessItemProcessor
    {
        public override void Process(ProcessItemArgs args)
        {
            if (!Context.Site.ProfileMappingEnabled())
                return;

            foreach (var profileMapSet in GetProfileMapsSets())
            {
                var set = Context.Database.GetItem(profileMapSet.Key);

                if (set == null)
                    continue;

                var enabled = ((CheckboxField)set.Fields[Templates.ProfileMapSet.Fields.Enabled]).Checked;

                if (!enabled)
                    continue;

                var matchSingle = ((CheckboxField)set.Fields[Templates.ProfileMapSet.Fields.MatchSingle]).Checked;

                foreach (var profileMap in profileMapSet)
                {
                    var trackingField = new ProfileMapItem(profileMap).EvaluateProfileMap(args.Item);

                    if (trackingField != null)
                    {
                        args.TrackingParameters.Add(trackingField);

                        if (matchSingle)
                            break;
                    }
                }
            }
        }

        public virtual IEnumerable<IGrouping<ID, Item>> GetProfileMapsSets()
        {
            var sets = new DefaultProfileMapRepository().GetProfileMapSets();

            return sets; 
        }
    }
}