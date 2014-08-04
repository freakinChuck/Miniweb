using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MinismuriWeb
{
    /// <summary>
    /// This class symbolises a Scheduled Task
    /// </summary>
    /// <remarks>
    ///     The Actions get executed like that:
    ///     1) InitializeRun
    ///     2) CheckForExecution
    ///     3) Execute
    ///     4) OnSucess OR OnError
    ///     5) OnEnd
    /// </remarks>
    public abstract class ScheduledTask
    {
        private DateTime? lastRun;
        private TaskRunStatus lastRunStatus;

        /// <summary>
        /// protected Constructor to prepare for singleton
        /// </summary>
        protected ScheduledTask()
        {
            lastRunStatus = TaskRunStatus.Suspended;
        }

        /// <summary>
        /// the minutes to wait between task runs (slightly depends on ContainerRunTime)
        /// </summary>
        /// <remarks>DEFAULT: 0 or Web.Config 'MinutesToWaitBetwenTaskRun_{0}', where {0} is Typename</remarks>
        protected virtual int MinutesToWaitBetwenTaskRun
        {
            get
            {
                int configInt = 0;
                string configString = ConfigurationManager.AppSettings[string.Format("MinutesToWaitBetwenTaskRun_{0}", this.GetType().Name)];
                int.TryParse(configString, out configInt);
                return configInt;
            }
        }

        /// <summary>
        /// runs the task
        /// </summary>
        public void Run(bool force = false)
        {
            this.InitializeRun();
            
            if (!lastRun.HasValue || lastRun.Value.AddMinutes(this.MinutesToWaitBetwenTaskRun) < DateTime.Now || force)
            {
                if (this.CheckForExecution())
                {
                    lastRun = DateTime.Now;
                    bool sucess = true;
                    try
                    {
                        this.Execute();
                        lastRunStatus = TaskRunStatus.Successful;
                        this.OnSucess();
                    }
                    catch (Exception e)
                    {
                        sucess = false;
                        bool handled;
                        lastRunStatus = TaskRunStatus.Failed;
                        this.OnError(e, out handled);
                        if (!handled)
                        {

                            throw;
                        }
                    }
                    this.OnEnd(sucess);
                }
                else
                {
                    lastRunStatus = TaskRunStatus.Suspended;
                }
            }
            else
            {

            }
        }
        /// <summary>
        /// initializes a task Run
        /// </summary>
        protected virtual void InitializeRun()
        {
        }
        /// <summary>
        /// of overridden, checks if the method needs to be executed
        /// </summary>
        /// <returns>true by default, false if the task gets prevented from execution</returns>
        protected virtual bool CheckForExecution()
        {
            return true;
        }
        /// <summary>
        /// executes the Task
        /// </summary>
        protected abstract void Execute();
        /// <summary>
        /// gets executed on Error
        /// </summary>
        /// <param name="e">the Exception</param>
        /// <param name="exceptionHandled">handled flag, if false, the exception gets rethrown</param>
        protected abstract void OnError(Exception e, out bool exceptionHandled);
        /// <summary>
        /// gets executed on sucess
        /// </summary>
        protected virtual void OnSucess()
        {

        }
        /// <summary>
        /// gets executed, when the task ends
        /// </summary>
        /// <param name="sucess">true, if sucess, false if error</param>
        protected virtual void OnEnd(bool sucess)
        {

        }

        /// <summary>
        /// der Status des letzten TaskRuns
        /// </summary>
        public TaskRunStatus LastRunStatus
        {
            get { return lastRunStatus; }
        }
        /// <summary>
        /// der zeitstempel des letzten TaskRuns
        /// </summary>
        public DateTime? LastRunTimestamp
        {
            get { return lastRun; }
        }
        /// <summary>
        /// das Datum, ab wann der nächste taskRun ausgeführt wird
        /// </summary>
        public DateTime? NextRunAb
        {
            get { return lastRun.HasValue ? lastRun.Value.AddMinutes(MinutesToWaitBetwenTaskRun) as DateTime? : null; }
        }

    }
}