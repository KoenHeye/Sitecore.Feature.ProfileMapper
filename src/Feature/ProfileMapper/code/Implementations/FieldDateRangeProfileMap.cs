using Sitecore.Analytics.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Feature.ProfileMapper.Abstractions;
using Sitecore.Feature.ProfileMapper.Models;
using System;

namespace Sitecore.Feature.ProfileMapper.Implementations
{
    public class FieldDateRangeProfileMap : ProfileMap, IProfileMap
    {
        public TrackingField Evaluate(Item mapItem, Item contextItem)
        {
            // the page context item should have the field that the field date range profile map is inspecting

            if (contextItem.Fields[mapItem[Templates.FieldDateRangeProfileMap.Fields.ContextItemField]] == null)
                return null;

            var value = ((DateField)contextItem.Fields[mapItem[Templates.FieldDateRangeProfileMap.Fields.ContextItemField]]).DateTime;

            if (value == default(DateTime))
                return null;

            var dateFrom = ((DateField)mapItem.Fields[Templates.FieldDateRangeProfileMap.Fields.DateFrom]).DateTime;
            var dateTo = ((DateField)mapItem.Fields[Templates.FieldDateRangeProfileMap.Fields.DateTo]).DateTime;

            if (dateFrom == default(DateTime) || dateTo == default(DateTime))
                return null;

            var repeatYear = ((CheckboxField)mapItem.Fields[Templates.FieldDateRangeProfileMap.Fields.RepeatEveryYear]).Checked;

            if (repeatYear)
            {
                dateFrom = new DateTime(value.Year, dateFrom.Month, dateFrom.Day, dateFrom.Hour, dateFrom.Minute, dateFrom.Second);
                dateTo = new DateTime(value.Year, dateTo.Month, dateTo.Day, dateTo.Hour, dateTo.Minute, dateTo.Second);
            }

            return EvaluateDates(mapItem, value, dateFrom, dateTo);
        }

        protected virtual TrackingField EvaluateDates(Item mapItem, DateTime value, DateTime dateFrom, DateTime dateTo)
        {
            if ((dateFrom <= value) && (dateTo >= value))
                return GetTrackingField(mapItem);

            return null;
        }

        public bool IsValid(Item mapItem)
        {
            if (string.IsNullOrWhiteSpace(mapItem[Templates.FieldDateRangeProfileMap.Fields.ContextItemField]))
                return false;

            if (string.IsNullOrWhiteSpace(mapItem[Templates.FieldDateRangeProfileMap.Fields.DateFrom]))
                return false;

            if (string.IsNullOrWhiteSpace(mapItem[Templates.FieldDateRangeProfileMap.Fields.DateTo]))
                return false;

            return true;
        }
    }
}