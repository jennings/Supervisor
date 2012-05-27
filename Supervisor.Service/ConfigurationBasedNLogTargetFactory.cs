//----------------------------------------------------------------------------------------
// <copyright file="ConfigurationBasedNLogTargetFactory.cs" company="The Supervisor Project">
//   Copyright 2011 Various Contributors. Licensed under the Apache License, Version 2.0.
// </copyright>
//----------------------------------------------------------------------------------------

namespace Supervisor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NLog.Targets;
    using NLog.Targets.Wrappers;

    internal class ConfigurationBasedNLogTargetFactory
    {
        private IEnumerable<Configuration.MessageTargetConfiguration> targetConfigurations;

        public ConfigurationBasedNLogTargetFactory(IEnumerable<Configuration.MessageTargetConfiguration> targets)
        {
            this.targetConfigurations = targets;
        }

        public IEnumerable<NLog.Targets.Target> GetTargets()
        {
            foreach (var target in this.targetConfigurations)
            {
                var asyncTargetWrapper = new AsyncTargetWrapper()
                {
                    Name = target.Id.ToString(),
                    WrappedTarget = target.Type.GetConstructor(new Type[] { }).Invoke(null) as Target,
                    BatchSize = 1
                };

                yield return asyncTargetWrapper;
            }
        }
    }
}
