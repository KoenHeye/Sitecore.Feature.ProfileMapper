using Sitecore.Analytics.Data;
using Sitecore.Data.Items;
using Sitecore.Feature.ProfileMapper.Abstractions;
using Sitecore.Reflection;

namespace Sitecore.Feature.ProfileMapper.Models
{
    public class ProfileMapItem : CustomItem
    {
        public ProfileMapItem(Item innerItem) : base(innerItem)
        {

        }

        public virtual TrackingField EvaluateProfileMap(Item contextItem)
        {
            if (!ProfileMapItemIsValid())
                return null;

            var profileMap = ToProfileMap();

            if (profileMap == null || !profileMap.IsValid(InnerItem))
                return null;

            return profileMap.Evaluate(InnerItem, contextItem);
        }

        /// <summary>
        /// Things that all profile maps regardless of type should have
        /// </summary>
        /// <returns></returns>
        protected virtual bool ProfileMapItemIsValid()
        {
            if (string.IsNullOrWhiteSpace(InnerItem[FieldNames.TrackingField]) 
                || string.IsNullOrWhiteSpace(InnerItem[Templates.ProfileMap.Fields.ProfileMapAssembly]) 
                || string.IsNullOrWhiteSpace(InnerItem[Templates.ProfileMap.Fields.ProfileMapClass]))
                return false;

            return true;
        }

        protected virtual IProfileMap ToProfileMap()
        {
            return ReflectionUtil.CreateObject(
                InnerItem[Templates.ProfileMap.Fields.ProfileMapAssembly], 
                InnerItem[Templates.ProfileMap.Fields.ProfileMapClass], 
                new object[] { }) as IProfileMap;
        }
    }
}