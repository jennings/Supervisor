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

    internal static class Program
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        [STAThread]
        public static void Main()
        {
            var logConfig = new LoggingConfiguration();

            var asyncTargetWrapper = new AsyncTargetWrapper();

            asyncTargetWrapper.WrappedTarget = new MessageBoxTarget();
            logConfig.AddTarget("MessageBoxTarget", asyncTargetWrapper);

            var rule = new LoggingRule("*", LogLevel.Debug, asyncTargetWrapper);
            logConfig.LoggingRules.Add(rule);

            LogManager.Configuration = logConfig;

            log.Debug("Starting");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
