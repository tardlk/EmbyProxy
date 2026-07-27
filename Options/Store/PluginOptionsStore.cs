using MediaBrowser.Common;
using MediaBrowser.Model.Logging;
using EmbyProxy.UIBaseClasses.Store;
using System;

namespace EmbyProxy.Options.Store
{
    public class PluginOptionsStore : SimpleFileStore<PluginOptions>
    {
        private readonly ILogger _logger;

        public PluginOptionsStore(IApplicationHost applicationHost, ILogger logger, string pluginFullName)
            : base(applicationHost, logger, pluginFullName)
        {
            _logger = logger;
            FileSaved += OnFileSaved;
            FileSaving += OnFileSaving;
        }

        public PluginOptions PluginOptions => GetOptions();

        private void OnFileSaving(object sender, FileSavingEventArgs e)
        {
            if (!(e.Options is PluginOptions options)) return;

            // Trim proxy URL
            options.NetworkOptions.ProxyServerUrl =
                !string.IsNullOrWhiteSpace(options.NetworkOptions.ProxyServerUrl)
                    ? options.NetworkOptions.ProxyServerUrl.Trim().TrimEnd('/')
                    : null;
        }

        private void OnFileSaved(object sender, FileSavedEventArgs e)
        {
            if (!(e.Options is PluginOptions options)) return;

            _logger.Info("配置已保存，重启 Emby 后生效");
            _logger.Info("  EnableProxyServer: {0}", options.NetworkOptions.EnableProxyServer);
            _logger.Info("  EnableIPv4Only: {0}", options.NetworkOptions.EnableIPv4Only);
            _logger.Info("  EnableAltTmdb: {0}", options.TmdbOptions.EnableAltTmdb);

            Plugin.Instance.ApplicationHost.NotifyPendingRestart();
        }
    }
}
