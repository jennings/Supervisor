//----------------------------------------------------------------------------------------
// <copyright file="MainForm.cs" company="The Supervisor Project">
//   Copyright 2011 Various Contributors. Licensed under the Apache License, Version 2.0.
// </copyright>
//----------------------------------------------------------------------------------------

namespace Supervisor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using Quartz;
    using Quartz.Impl;

    /// <summary>
    ///     The form which owns the scheduler. Should be replaced
    ///     at some point with something deriving from ServiceBase.
    /// </summary>
    public partial class MainForm : Form
    {
        private IScheduler scheduler;

        public MainForm()
        {
            this.InitializeComponent();

            this.FormClosed += new FormClosedEventHandler(this.StopScheduler);

            var schedulerFactory = new StdSchedulerFactory();
            this.scheduler = schedulerFactory.GetScheduler();
            this.scheduler.Start();

            var jobDetail = JobBuilder.Create<Monitoring.AlwaysErrorMonitor>()
                                      .WithIdentity("AlwaysErrorMonitor")
                                      .Build();

            var trigger = TriggerBuilder.Create()
                                        .WithIdentity("Trigger")
                                        .StartNow()
                                        .WithSimpleSchedule(x => x.WithIntervalInSeconds(5).RepeatForever())
                                        .Build();

            this.scheduler.ScheduleJob(jobDetail, trigger);
        }

        private void StopScheduler(object sender, EventArgs e)
        {
            this.scheduler.Shutdown();
        }
    }
}
