using EmbyProxy.Common;
using System;
using System.Net;
using System.Net.Http;
using static EmbyProxy.Common.CommonUtility;

namespace EmbyProxy.Mod
{
    public class EnableProxyServer
    {
        private SelectiveProxy _selectiveProxy;
        private IWebProxy _savedDefaultProxy;

        public bool IsActive { get; private set; }

        public void Apply()
        {
            try
            {
                if (IsActive) return;

                var options = Plugin.Instance.MainOptionsStore.GetOptions().NetworkOptions;

                if (!options.EnableProxyServer || string.IsNullOrWhiteSpace(options.ProxyServerUrl))
                    return;

                if (!TryParseProxyUrl(options.ProxyServerUrl, out var schema, out var host, out var port,
                        out var username, out var password))
                    return;

                var proxyUrl = $"{schema}://{host}:{port}";

                _selectiveProxy = new SelectiveProxy(proxyUrl, options.ProxyDomains);

                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                    _selectiveProxy.Credentials = new NetworkCredential(username, password);

                _savedDefaultProxy = HttpClient.DefaultProxy;
                HttpClient.DefaultProxy = _selectiveProxy;
                IsActive = true;

                Plugin.Instance.Logger.Info($"Proxy enabled: {proxyUrl} for {_selectiveProxy.DomainCount} domains");
            }
            catch (Exception e)
            {
                Plugin.Instance.Logger.Error($"Failed to apply proxy: {e.Message}");
            }
        }

        public void Remove()
        {
            try
            {
                if (!IsActive) return;

                HttpClient.DefaultProxy = _savedDefaultProxy;
                _selectiveProxy = null;
                IsActive = false;

                Plugin.Instance.Logger.Info("Proxy disabled");
            }
            catch (Exception e)
            {
                Plugin.Instance.Logger.Error($"Failed to remove proxy: {e.Message}");
            }
        }
    }
}
