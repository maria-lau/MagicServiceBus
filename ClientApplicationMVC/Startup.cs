﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SignalRTestApplication.Startup))]

namespace SignalRTestApplication
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}