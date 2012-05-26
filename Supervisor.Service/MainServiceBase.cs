//----------------------------------------------------------------------------------------
// <copyright file="MainServiceBase.cs" company="The Supervisor Project">
//   Copyright 2011 Various Contributors. Licensed under the Apache License, Version 2.0.
// </copyright>
//----------------------------------------------------------------------------------------

namespace Supervisor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceProcess;
    using System.Text;
    using Quartz;

    internal class MainServiceBase : ServiceBase
    {
        private IScheduler scheduler;

        public MainServiceBase(IScheduler scheduler)
        {
            this.scheduler = scheduler;
        }

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
            this.scheduler.Start();
        }

        protected override void OnStop()
        {
            base.OnStop();
            this.scheduler.Shutdown();
        }
    }
}
