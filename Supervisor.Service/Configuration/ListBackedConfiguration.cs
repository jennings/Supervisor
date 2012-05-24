//----------------------------------------------------------------------------------------
// <copyright file="ListBackedConfiguration.cs" company="The Supervisor Project">
//   Copyright 2011 Various Contributors. Licensed under the Apache License, Version 2.0.
// </copyright>
//----------------------------------------------------------------------------------------

namespace Supervisor.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;

    internal class ListBackedConfiguration : IConfiguration
    {
        private ICollection<MessageTargetConfiguration> targets;
        private ICollection<MonitorConfiguration> monitors;

        public ListBackedConfiguration()
            : this(false)
        {
        }

        public ListBackedConfiguration(bool useMockData)
        {
            if (useMockData)
            {
                this.targets = new List<MessageTargetConfiguration>();
                this.targets.Add(new MessageTargetConfiguration { Id = Guid.NewGuid(), DisplayName = "TestTarget1", Type = typeof(NLog.Targets.MessageBoxTarget) });
                this.targets.Add(new MessageTargetConfiguration { Id = Guid.NewGuid(), DisplayName = "TestTarget2", Type = typeof(NLog.Targets.DebuggerTarget) });
                this.targets.Add(new MessageTargetConfiguration { Id = Guid.NewGuid(), DisplayName = "TestTarget3", Type = typeof(NLog.Targets.ConsoleTarget) });

                this.monitors = new List<MonitorConfiguration>();
                this.monitors.Add(new MonitorConfiguration { Id = Guid.NewGuid(), DisplayName = "TestMonitor1" });
            }
            else
            {
                this.targets = new List<MessageTargetConfiguration>();
                this.monitors = new List<MonitorConfiguration>();
            }
        }

        public ICollection<MessageTargetConfiguration> MessageTargets
        {
            get { return this.targets; }
            set { this.targets = value; }
        }

        public ICollection<MonitorConfiguration> Monitors
        {
            get { return this.monitors; }
            set { this.monitors = value; }
        }
    }
}
