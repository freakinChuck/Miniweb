using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using System.Web.SessionState;

namespace MinismuriWeb
{
    public class Global : System.Web.HttpApplication
    {
        private const string TASK_CACHE_ENTRY_KEY = "ScheduledTaskCacheEntry";
        private const string STAYALIVE_CACHE_ENTRY_KEY = "StayAliveCacheEntry";
        private const string CACHE_KEY = "ScheduledTaskCache";

        protected void Application_Start(object sender, EventArgs e)
        {
            MinismuriWeb.Storage.StatisticStorage.MapPath = HttpContext.Current.Server.MapPath;
            MinismuriWeb.Storage.EventStorage.MapPath = HttpContext.Current.Server.MapPath;

            try
            {
                Application[CACHE_KEY] = HttpContext.Current.Cache;
                RegisterTaskCacheEntry();
                RegisterStayAliveCacheEntry();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request["NoStat"]))
            {
                var threadStart = new ParameterizedThreadStart(RegisterStatistics);
                var thread = new Thread(threadStart);
                thread.Start(new
                {
                    Browser = HttpContext.Current.Request.Browser.Browser,
                    BrowserVersion = HttpContext.Current.Request.Browser.Version,
                });   
            }
        }

        #region Scheduled Task Methods

        private void RegisterStayAliveCacheEntry()
        {

            TimeSpan removeTime = TimeSpan.FromMinutes(int.Parse(ConfigurationManager.AppSettings["AnzahlMinutenFuerStayAliveTask"]));
            Cache cache = (Cache)Application[CACHE_KEY];
            if (cache[STAYALIVE_CACHE_ENTRY_KEY] != null)
            {
                cache.Remove(STAYALIVE_CACHE_ENTRY_KEY);
            }
            cache.Add(STAYALIVE_CACHE_ENTRY_KEY, STAYALIVE_CACHE_ENTRY_KEY, null,
                      DateTime.MaxValue, removeTime, CacheItemPriority.Normal,
                      new CacheItemRemovedCallback(StayAliveCacheItemRemoved));

        }

        private void StayAliveCacheItemRemoved(string key, object value, CacheItemRemovedReason reason)
        {
            PreventIISIdle();
            RegisterStayAliveCacheEntry();
        }
        private void PreventIISIdle()
        {

            try
            {
                string url = ConfigurationManager.AppSettings["RootUrl"];
                url = string.Format("{0}{1}NoStat=Yes", url, url.Contains("?") ? "&" : "?");
                WebRequest request = WebRequest.Create(url);
                var response = request.GetResponse();
            }
            catch (WebException exc)
            {
                if (exc.Status != WebExceptionStatus.ConnectFailure)
                {
                }
            }
            catch (Exception e)
            {
            }
        }

        private void RegisterTaskCacheEntry()
        {
            Cache cache = (Cache)Application[CACHE_KEY];
            if (cache[TASK_CACHE_ENTRY_KEY] != null)
            {
                cache.Remove(TASK_CACHE_ENTRY_KEY);
            }
            cache.Add(TASK_CACHE_ENTRY_KEY, TASK_CACHE_ENTRY_KEY, null,
                      DateTime.MaxValue, TaskSchedulerContainer.TimeBetweenTaskRun, CacheItemPriority.Normal,
                      new CacheItemRemovedCallback(TaskCacheItemRemoved));


        }
        private void TaskCacheItemRemoved(string key, object value, CacheItemRemovedReason reason)
        {
            TaskSchedulerContainer.RunScheduledActions();
            RegisterTaskCacheEntry();
        }




        #endregion

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