//----------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="The Supervisor Project">
//   Copyright 2011 Various Contributors. Licensed under the Apache License, Version 2.0.
// </copyright>
//----------------------------------------------------------------------------------------

namespace Supervisor
{
    using System;
    using System.IO;
    using System.Windows.Forms;
    using NLog;
    using NLog.Config;
    using NLog.Targets;
    using NLog.Targets.Wrappers;
    using Quartz;
    using Quartz.Impl;
    using Supervisor.Configuration;
    using System.ServiceProcess;

    internal static class Program
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        [STAThread]
        public static void Main(string[] args)
        {
            log.Debug("Starting");

            IConfiguration config;
            string filename = @"C:\Temp\Config.js";
            if (File.Exists(filename))
            {
                config = new JsonBackedConfiguration();
                var configText = File.ReadAllText(filename);
                ((JsonBackedConfiguration)config).LoadFromString(configText);
            }
            else
            {
                config = new JsonBackedConfiguration();
                var configString = ((JsonBackedConfiguration)config).ToPersistableString();
                File.WriteAllText(filename, configString);
            }

            var logConfig = new LoggingConfiguration();

            foreach (var target in config.MessageTargets)
            {
                var asyncTargetWrapper = new AsyncTargetWrapper()
                {
                    Name = target.Id.ToString(),
                    WrappedTarget = target.Type.GetConstructor(new Type[] { }).Invoke(null) as Target,
                    BatchSize = 1
                };
                logConfig.AddTarget(target.Id.ToString(), asyncTargetWrapper);
            }

            foreach (var monitor in config.Monitors)
            {
                foreach (var target in logConfig.ConfiguredNamedTargets)
                {
                    if (target.GetType() != typeof(AsyncTargetWrapper))
                    {
                        continue;
                    }

                    var rule = new LoggingRule("*", LogLevel.Debug, target);
                    logConfig.LoggingRules.Add(rule);
                }
            }

            LogManager.Configuration = logConfig;

            log.Debug("Finished building config");

            var schedulerFactory = new StdSchedulerFactory();
            var scheduler = schedulerFactory.GetScheduler();

            var jobDetail = JobBuilder.Create<Monitoring.AlwaysErrorMonitor>()
                                      .WithIdentity("AlwaysErrorMonitor")
                                      .Build();

            var trigger = TriggerBuilder.Create()
                                        .WithIdentity("Trigger")
                                        .StartNow()
                                        .WithSimpleSchedule(x => x.WithIntervalInSeconds(5).RepeatForever())
                                        .Build();

            scheduler.ScheduleJob(jobDetail, trigger);

            log.Debug("Finished building scheduler");

            if (args.Length == 0)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm(scheduler));
            }
            else
            {
                ServiceBase.Run(new MainServiceBase(scheduler));
            }
        }
    }
}
