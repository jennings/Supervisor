//----------------------------------------------------------------------------------------
// <copyright file="MonitorBase.cs" company="The Supervisor Project">
//   Copyright 2011 Various Contributors. Licensed under the Apache License, Version 2.0.
// </copyright>
//----------------------------------------------------------------------------------------

namespace Supervisor.Monitoring
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NLog;
    using Quartz;

    [DisallowConcurrentExecution]
    internal abstract class MonitorBase : IJob
    {
        private Logger log;

        protected MonitorBase()
        {
            this.log = LogManager.GetLogger(this.GetType().ToString());
        }

        public abstract void Execute();

        public void Execute(IJobExecutionContext context)
        {
            this.Execute();
        }

        protected void CriticalAlert(string message, params object[] args)
        {
            this.log.Error(message, args);
        }

        protected void WarningAlert(string message, params object[] args)
        {
            this.log.Warn(message, args);
        }

        protected void InformationalAlert(string message, params object[] args)
        {
            this.log.Info(message, args);
        }
    }
}
