using MediaBrowser.Common;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Model.Drawing;
using MediaBrowser.Model.Logging;
using MediaBrowser.Model.Plugins;
using MediaBrowser.Model.Plugins.UI;
using EmbyProxy.Mod;
using EmbyProxy.Options.Store;
using EmbyProxy.Options.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace EmbyProxy
{
    public class Plugin : BasePlugin, IHasUIPages, IHasThumbImage
    {
        public static Plugin Instance { get; private set; }

        private readonly Guid _id = new Guid("B5C3E8A1-7D4F-4A2B-9E6C-1F3D8A5B2C7E");
        private List<IPluginUIPageController> _pages;

        public readonly ILogger Logger;
        public readonly IApplicationHost ApplicationHost;
        public readonly IApplicationPaths ApplicationPaths;
        public readonly PluginOptionsStore MainOptionsStore;
        public readonly EnableProxyServer EnableProxyServer;
        public readonly AltMovieDbConfig AltMovieDbConfig;
        public readonly ForceIPv4 ForceIPv4;

        public Plugin(IApplicationHost applicationHost, ILogManager logManager, IApplicationPaths applicationPaths)
        {
            Instance = this;
            Logger = logManager.GetLogger(Name);
            ApplicationHost = applicationHost;
            ApplicationPaths = applicationPaths;

            Logger.Info("Plugin is getting loaded.");

            EnableProxyServer = new EnableProxyServer();
            AltMovieDbConfig = new AltMovieDbConfig();
            ForceIPv4 = new ForceIPv4();
            MainOptionsStore = new PluginOptionsStore(applicationHost, Logger, Name);

            if (Debugger.IsAttached) DebugMode = true;
            if (MainOptionsStore.GetOptions().EnableDebugMode) DebugMode = true;

            if (DebugMode)
                Logger.Info("Debug mode enabled");

            if (MainOptionsStore.GetOptions().NetworkOptions.EnableProxyServer)
                EnableProxyServer.Apply();
            if (MainOptionsStore.GetOptions().NetworkOptions.EnableIPv4Only)
                ForceIPv4.Apply();
            if (MainOptionsStore.GetOptions().TmdbOptions.EnableAltTmdb)
                AltMovieDbConfig.Apply();
        }

        public override string Name => "EmbyProxy";
        public override string Description => "EmbyProxy - Alt TMDB Config & Selective Proxy";
        public override Guid Id => _id;
        public bool DebugMode;

        public ImageFormat ThumbImageFormat => ImageFormat.Png;
        public Stream GetThumbImage() =>
            GetType().Assembly.GetManifestResourceStream("EmbyProxy.Properties.thumb.png");

        public IReadOnlyCollection<IPluginUIPageController> UIPageControllers
        {
            get
            {
                if (_pages == null)
                    _pages = new List<IPluginUIPageController>
                    {
                        new MainPageController(GetPluginInfo(), MainOptionsStore)
                    };
                return _pages.AsReadOnly();
            }
        }
    }
}
