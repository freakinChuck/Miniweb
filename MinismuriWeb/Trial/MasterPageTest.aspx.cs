using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MinismuriWeb.Trial
{
    public partial class MasterPageTest : System.Web.UI.Page
    {
        private const string CACHE_KEY = "ScheduledTaskCache";
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.Caching.Cache cache = (System.Web.Caching.Cache)Application[CACHE_KEY];

            //string txt = string.Empty;
            //foreach (var item in cache)
            //{
            //    txt += (GetCacheUtcExpiryDateTime(((System.Collections.DictionaryEntry)item).Key.ToString()).ToString() + ";" + ((System.Collections.DictionaryEntry)item).Key + ";" + ((System.Collections.DictionaryEntry)item).Value + "\n");
            //}
            //testLiteral.Text = txt.Replace("\n", "<br/>");
        }
        private DateTime GetCacheUtcExpiryDateTime(string cacheKey)
        {
            object cacheEntry = Application[CACHE_KEY].GetType().GetMethod("Get", BindingFlags.Instance | BindingFlags.NonPublic).Invoke((System.Web.Caching.Cache)Application[CACHE_KEY], new object[] { cacheKey, 1 });
            PropertyInfo utcExpiresProperty = cacheEntry.GetType().GetProperty("UtcExpires", BindingFlags.NonPublic | BindingFlags.Instance);
            DateTime utcExpiresValue = (DateTime)utcExpiresProperty.GetValue(cacheEntry, null);

            return utcExpiresValue;
        }

    }
}