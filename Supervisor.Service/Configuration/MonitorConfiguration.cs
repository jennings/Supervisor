//----------------------------------------------------------------------------------------
// <copyright file="MonitorConfiguration.cs" company="The Supervisor Project">
//   Copyright 2011 Various Contributors. Licensed under the Apache License, Version 2.0.
// </copyright>
//----------------------------------------------------------------------------------------

namespace Supervisor.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal class MonitorConfiguration
    {
        private Type type;

        public Guid Id { get; set; }

        public string DisplayName { get; set; }

        public Type @Type
        {
            get
            {
                return this.type;
            }

            set
            {
                if (!typeof(Monitoring.MonitorBase).IsAssignableFrom(value))
                {
                    throw new ArgumentException("Value must descend from Monitoring.MonitorBase");
                }

                this.type = value;
            }
        }

        public uint CheckInterval { get; set; }
    }
}
