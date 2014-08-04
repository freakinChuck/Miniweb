using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MinismuriWeb
{
    /// <summary>
    /// der Container für den TaskSchueduler
    /// </summary>
    public class TaskSchedulerContainer
    {
        /// <summary>
        /// Initialisiert die statischen Objekte der Klasse 'TaskSchedulerContainer'
        /// </summary>
        static TaskSchedulerContainer()
        {
            Tasks = new List<ScheduledTask>();
        }
        /// <summary>
        /// die maximale Zeit zwischen den Taskabläufen
        /// </summary>
        public static TimeSpan TimeBetweenTaskRun
        {
            get
            {
                return TimeSpan.FromMinutes(int.Parse(ConfigurationManager.AppSettings["AnzahlMinutenZwischenTaskRun"]));
            }
        }
        /// <summary>
        /// die tasks
        /// </summary>
        public static List<ScheduledTask> Tasks { get; set; }

        /// <summary>
        /// gets or sets the last Run of the TaskSchedulerContainer
        /// </summary>
        public static DateTime? LastRun
        {
            get;
            private set;
        }

        /// <summary>
        /// startet die Tasks
        /// </summary>
        public static void RunScheduledActions()
        {
            LastRun = DateTime.Now;
            var allTasks = TaskSchedulerContainer.Tasks.ToList();
            foreach (var task in allTasks)
            {
                task.Run();
            }
        }
    }
}