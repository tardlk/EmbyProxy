using Emby.Web.GenericEdit;
using Emby.Web.GenericEdit.Elements;
using MediaBrowser.Model.Attributes;
using System.ComponentModel;

namespace EmbyProxy.Options
{
    public class NetworkOptions : EditableOptionsBase
    {
        public override string EditorTitle => "网络";

        [DisplayName("启用代理服务器")]
        [Required]
        public bool EnableProxyServer { get; set; } = false;

        [DisplayName("代理服务器地址")]
        [VisibleCondition(nameof(EnableProxyServer), SimpleCondition.IsTrue)]
        public string ProxyServerUrl { get; set; } = string.Empty;

        [Browsable(false)]
        public bool ShowProxyServerStatus { get; set; } = false;

        [VisibleCondition(nameof(ShowProxyServerStatus), SimpleCondition.IsTrue)]
        public StatusItem ProxyServerStatus { get; set; } = new StatusItem
        {
            Status = ItemStatus.Unavailable,
            Caption = "不可用",
            StatusText = string.Empty
        };

        [DisplayName("代理域名列表")]
        [EditMultiline(5)]
        [VisibleCondition(nameof(EnableProxyServer), SimpleCondition.IsTrue)]
        public string ProxyDomains { get; set; } =
            "api.themoviedb.org\r\nimage.tmdb.org\r\napi.tmdb.org\r\napi.tvdb.com\r\nartworks.thetvdb.com\r\nwebservice.fanart.tv\r\nassets.fanart.tv";

        [DisplayName("强制 IPv4 连接")]
        [Required]
        public bool EnableIPv4Only { get; set; } = false;

        [DisplayName("IPv4 Only 域名列表")]
        [EditMultiline(4)]
        [VisibleCondition(nameof(EnableIPv4Only), SimpleCondition.IsTrue)]
        public string IPv4OnlyDomains { get; set; } = "image.tmdb.org";
    }
}
