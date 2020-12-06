﻿using Flow.Launcher.Infrastructure.Http;
using Flow.Launcher.Infrastructure.Logger;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Flow.Launcher.Plugin.PluginsManager.Models
{
    internal class PluginsManifest
    {
        internal List<UserPlugin> UserPlugins { get; private set; }
        internal PluginsManifest()
        {
            var json = string.Empty;

            using (var wc = new WebClient { Proxy = Http.WebProxy() })
            {
                try
                {
                    json = wc.DownloadString("https://raw.githubusercontent.com/Flow-Launcher/Flow.Launcher.PluginsManifest/main/plugins.json");
                }
                catch (Exception e)
                {
                    Log.Exception("|PluginManagement.GetManifest|Encountered error trying to download plugins manifest", e);

                    UserPlugins = new List<UserPlugin>();
                }
            }

            UserPlugins = !string.IsNullOrEmpty(json) ? JsonConvert.DeserializeObject<List<UserPlugin>>(json) : new List<UserPlugin>();
        }
    }
}
