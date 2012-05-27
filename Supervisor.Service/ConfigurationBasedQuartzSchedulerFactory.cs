//----------------------------------------------------------------------------------------
// <copyright file="ConfigurationBasedQuartzSchedulerFactory.cs" company="The Supervisor Project">
//   Copyright 2011 Various Contributors. Licensed under the Apache License, Version 2.0.
// </copyright>
//----------------------------------------------------------------------------------------

namespace Supervisor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Quartz;
    using Quartz.Impl;

    internal class ConfigurationBasedQuartzSchedulerFactory
    {
        private IEnumerable<Configuration.MonitorConfiguration> monitorConfiguration;

        public ConfigurationBasedQuartzSchedulerFactory(IEnumerable<Configuration.MonitorConfiguration> monitors)
        {
            this.monitorConfiguration = monitors;
        }

        public IScheduler GetScheduler()
        {
            var stdSchedulerFactory = new StdSchedulerFactory();
            var scheduler = stdSchedulerFactory.GetScheduler();

            foreach (var monitor in this.monitorConfiguration)
            {
                var jobDetail = JobBuilder.Create(monitor.Type)
                                          .WithIdentity(monitor.Id.ToString())
                                          .Build();

                var trigger = TriggerBuilder.Create()
                                            .WithIdentity(monitor.Id.ToString())
                                            .StartNow()
                                            .WithSimpleSchedule(x => x.WithIntervalInSeconds((int)monitor.CheckInterval)
                                                                      .RepeatForever())
                                            .Build();

                scheduler.ScheduleJob(jobDetail, trigger);
            }

            return scheduler;
        }
    }
}
