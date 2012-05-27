//----------------------------------------------------------------------------------------
// <copyright file="AlwaysErrorMonitor.cs" company="The Supervisor Project">
//   Copyright 2011 Various Contributors. Licensed under the Apache License, Version 2.0.
// </copyright>
//----------------------------------------------------------------------------------------

namespace Supervisor.Monitoring
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal class AlwaysErrorMonitor : MonitorBase
    {
        public override void Execute()
        {
            this.CriticalAlert("I had an error at {0}", DateTime.Now);
        }
    }
}
