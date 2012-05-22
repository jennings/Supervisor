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
        public Guid Id { get; set; }

        public string DisplayName { get; set; }

        public Type @Type
        {
            get
            {
                return typeof(NLog.Targets.MessageBoxTarget);
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
