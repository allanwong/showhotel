using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PettiInn.Utilities
{
    public static class DT
    {
        public static SortedDictionary<DateTime, DateTime> MonthsFragments(this DateTime endDT, int countOfMonths)
        {
            var dts = new SortedDictionary<DateTime, DateTime>();
            var nowEnd = endDT;

            var nowStart = new DateTime(nowEnd.Year, nowEnd.Month, 1);

            var previousEnd = nowStart;
            for (int i = 1; i < 13; i++)
            {
                var end = previousEnd.AddSeconds(-1);
                var start = new DateTime(end.Year, end.Month, 1);
                dts.Add(start, end);

                previousEnd = start;
            }

            dts.Add(nowStart, nowEnd);

            return dts;
        }

        public static DateTime? MergeTime(this DateTime? dt, string time, bool toTheEndOfTheDayWhenTimeIsNull = false)
        {
            var result = dt;

            if (dt != null)
            {
                result = dt.Value.Date;

                if (!string.IsNullOrWhiteSpace(time))
                {
                    var dtStr = string.Concat(result.Value.ToString("yyyy-MM-dd"), " ", time);
                    result = DateTime.Parse(dtStr);
                }
                else if (toTheEndOfTheDayWhenTimeIsNull)
                {
                    result = result.Value.AddDays(1).AddSeconds(-1);
                }
            }
            else if (!string.IsNullOrWhiteSpace(time))
            {
                result = DateTime.Now.Date;
                var dtStr = string.Concat(result.Value.ToString("yyyy-MM-dd"), " ", time);
                result = DateTime.Parse(dtStr);
            }

            return result;
        }

        public static DateTime MergeTime(this DateTime dt, string time, bool toTheEndOfTheDayWhenTimeIsNull = false)
        {
            var result = dt.Date;

            if (!string.IsNullOrWhiteSpace(time))
            {
                var dtStr = string.Concat(result.ToString("yyyy-MM-dd"), " ", time);
                result = DateTime.Parse(dtStr);
            }
            else if (toTheEndOfTheDayWhenTimeIsNull)
            {
                result = result.AddDays(1).AddSeconds(-1);
            }

            return result;
        }

        public static DateTime? MergeTime(this DateTime? dt, DateTime? time, bool toTheEndOfTheDayWhenTimeIsNull = false)
        {
            var timeStr = default(string);

            if (time != null)
            {
                timeStr = time.Value.ToString("HH:mm:ss");
            }

            var result = dt.MergeTime(timeStr, toTheEndOfTheDayWhenTimeIsNull);

            return result;
        }

        public static DateTime MergeTime(this DateTime dt, DateTime? time, bool toTheEndOfTheDayWhenTimeIsNull = false)
        {
            var timeStr = default(string);

            if (time != null)
            {
                timeStr = time.Value.ToString("HH:mm:ss");
            }

            var result = dt.MergeTime(timeStr, toTheEndOfTheDayWhenTimeIsNull);

            return result;
        }
    }
}
