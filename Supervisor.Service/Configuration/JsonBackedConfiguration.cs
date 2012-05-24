//----------------------------------------------------------------------------------------
// <copyright file="JsonBackedConfiguration.cs" company="The Supervisor Project">
//   Copyright 2011 Various Contributors. Licensed under the Apache License, Version 2.0.
// </copyright>
//----------------------------------------------------------------------------------------

namespace Supervisor.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json;

    internal class JsonBackedConfiguration : IConfiguration
    {
        private IConfiguration configuration;

        public JsonBackedConfiguration()
        {
            this.configuration = new ListBackedConfiguration(true);
        }

        public ICollection<MessageTargetConfiguration> MessageTargets
        {
            get { return this.configuration.MessageTargets; }
        }

        public ICollection<MonitorConfiguration> Monitors
        {
            get { return this.configuration.Monitors; }
        }

        public void LoadFromString(string text)
        {
            var config = JsonConvert.DeserializeObject<ListBackedConfiguration>(text);
            this.configuration = config;
        }

        public string ToPersistableString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
