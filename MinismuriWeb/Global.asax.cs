using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace MinismuriWeb
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            MinismuriWeb.Storage.StatisticStorage.MapPath = HttpContext.Current.Server.MapPath;
            MinismuriWeb.Storage.EventStorage.MapPath = HttpContext.Current.Server.MapPath;
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            var threadStart = new ParameterizedThreadStart(RegisterStatistics);
            var thread = new Thread(threadStart);
            thread.Start(new
            {
                Browser = HttpContext.Current.Request.Browser.Browser,
                BrowserVersion = HttpContext.Current.Request.Browser.Version,
            });
        }

        private static object statisticLocker = new { locker = true, name = "statistic" };

        private void RegisterStatistics(object data)
        {
            dynamic dynData = data;
            string browser = string.Format("{0}", dynData.Browser, dynData.BrowserVersion);
            string browserVersion = string.Format("{1}", dynData.Browser, dynData.BrowserVersion);
            lock (statisticLocker)
            {
                var statStorage = Storage.StatisticStorage.LoadStorage();
                statStorage.DataSet.HitCount.AddHitCountRow(Time.Now, browser, browserVersion);
                statStorage.Save();
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}