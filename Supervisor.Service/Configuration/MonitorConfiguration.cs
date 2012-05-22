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
        public Guid Id { get; set; }

        public string DisplayName { get; set; }
    }
}
