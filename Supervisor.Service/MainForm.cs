//----------------------------------------------------------------------------------------
// <copyright file="MainForm.cs" company="The Supervisor Project">
//   Copyright 2011 Various Contributors. Licensed under the Apache License, Version 2.0.
// </copyright>
//----------------------------------------------------------------------------------------

namespace Supervisor
{
    using System;
    using System.Windows.Forms;
    using Quartz;

    /// <summary>
    ///     The form which owns the scheduler. Should be replaced
    ///     at some point with something deriving from ServiceBase.
    /// </summary>
    public partial class MainForm : Form
    {
        private IScheduler scheduler;

        public MainForm(IScheduler scheduler)
        {
            this.InitializeComponent();

            this.scheduler = scheduler;
            this.FormClosed += (sender, e) => { this.scheduler.Shutdown(); };
            this.scheduler.Start();
        }
    }
}
