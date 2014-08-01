using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinismuriWeb
{
    public static class Time
    {
        public static DateTime Now
        {
            get 
            {
                try
                {
                    var zone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
                    return DateTime.UtcNow + zone.BaseUtcOffset;
                }
                catch
                {
                    return DateTime.UtcNow;
                }
            }
        }
    }
}