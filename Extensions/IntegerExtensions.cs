using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public static class IntegerExtensions
    {
        /// <summary>
        /// Kilobytes
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int KB(this int value)
        {
            return value * 1024;
        }

        /// <summary>
        /// Megabytes
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int MB(this int value)
        {
            return value.KB() * 1024;
        }

        /// <summary>
        /// Gigabytes
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GB(this int value)
        {
            return value.MB() * 1024;
        }

        /// <summary>
        /// Terabytes
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long TB(this int value)
        {
            return (long)value.GB() * (long)1024;
        }
        public static int? GetNullableInt32(this IDataRecord dr, int ordinal)
        {
            int? nullInt = null;
            return dr.IsDBNull(ordinal) ? nullInt : dr.GetInt32(ordinal);
        }

        public static int? GetNullableInt32(this IDataRecord dr, string fieldname)
        {
            int ordinal = dr.GetOrdinal(fieldname);
            return dr.GetNullableInt32(ordinal);
        }

        public static bool? GetNullableBoolean(this IDataRecord dr, int ordinal)
        {
            bool? nullBool = null;
            return dr.IsDBNull(ordinal) ? nullBool : dr.GetBoolean(ordinal);
        }

        public static bool? GetNullableBoolean(this IDataRecord dr, string fieldname)
        {
            int ordinal = dr.GetOrdinal(fieldname);
            return dr.GetNullableBoolean(ordinal);
        }
    }
}
