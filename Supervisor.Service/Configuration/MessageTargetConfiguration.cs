//----------------------------------------------------------------------------------------
// <copyright file="MessageTargetConfiguration.cs" company="The Supervisor Project">
//   Copyright 2011 Various Contributors. Licensed under the Apache License, Version 2.0.
// </copyright>
//----------------------------------------------------------------------------------------

namespace Supervisor.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NLog;

    internal class MessageTargetConfiguration
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
                if (!typeof(NLog.Targets.Target).IsAssignableFrom(value))
                {
                    throw new ArgumentException("Value must descend from NLog.Targets.Target");
                }

                this.type = value;
            }
        }

        public LogLevel MinimumLogLevel
        {
            get
            {
                return LogLevel.Info;
            }
        }
    }
}
