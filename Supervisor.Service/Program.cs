//----------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="The Supervisor Project">
//   Copyright 2011 Various Contributors. Licensed under the Apache License, Version 2.0.
// </copyright>
//----------------------------------------------------------------------------------------

namespace Supervisor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using NLog;
    using NLog.Config;
    using NLog.Targets;
    using NLog.Targets.Wrappers;
    using Supervisor.Configuration;

    internal static class Program
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        [STAThread]
        public static void Main()
        {
            log.Debug("Starting");

            IConfiguration config = new ListBackedConfiguration();

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

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
