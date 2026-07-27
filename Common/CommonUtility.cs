using System;

namespace EmbyProxy.Common
{
    public static class CommonUtility
    {
        public static bool TryParseProxyUrl(string proxyUrl, out string scheme, out string host,
            out int port, out string username, out string password)
        {
            scheme = host = username = password = string.Empty;
            port = 0;

            if (string.IsNullOrWhiteSpace(proxyUrl))
                return false;

            // Expected format: http[s]://[user:pass@]host:port
            var url = proxyUrl.Trim();

            // Extract scheme
            var schemeEnd = url.IndexOf("://", StringComparison.Ordinal);
            if (schemeEnd < 0) return false;
            scheme = url.Substring(0, schemeEnd).ToLowerInvariant();
            if (scheme != "http" && scheme != "https") return false;

            // Split credentials and host:port
            var rest = url.Substring(schemeEnd + 3);
            var atIndex = rest.IndexOf('@');
            var hostPart = atIndex >= 0 ? rest.Substring(atIndex + 1) : rest;

            // Parse credentials
            if (atIndex >= 0)
            {
                var creds = rest.Substring(0, atIndex);
                var colon = creds.IndexOf(':');
                if (colon >= 0)
                {
                    username = creds.Substring(0, colon);
                    password = creds.Substring(colon + 1);
                }
                else
                {
                    username = creds;
                }
            }

            // Parse host:port
            var portColon = hostPart.LastIndexOf(':');
            if (portColon >= 0 && int.TryParse(hostPart.Substring(portColon + 1), out port))
                host = hostPart.Substring(0, portColon);
            else
                return false;

            return !string.IsNullOrEmpty(host) && port > 0 && port <= 65535;
        }
    }
}
