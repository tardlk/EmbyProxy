using Emby.Web.GenericEdit;
using Emby.Web.GenericEdit.Elements;
using MediaBrowser.Model.Attributes;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace EmbyProxy.Options
{
    public class PluginOptions : EditableOptionsBase
    {
        private static readonly string BuildDate =
            Assembly.GetExecutingAssembly()
                .GetCustomAttributes<AssemblyMetadataAttribute>()
                .FirstOrDefault(a => a.Key == "BuildDate")?.Value ?? "未知";

        public override string EditorTitle => "EmbyProxy";
        public override string EditorDescription =>
            "代理：白名单机制，仅对列表中域名走代理，其余直连。\n" +
            "TMDB 替代：改写 api.themoviedb.org 至自定义地址。\n" +
            "强制 IPv4：对指定域名仅解析 IPv4，防止 IPv6 超时。\n" +
            "修改配置后需重启 Emby 生效。";

        [DisplayName("TMDB 配置")]
        public TmdbOptions TmdbOptions { get; set; } = new TmdbOptions();

        [DisplayName("网络")]
        public NetworkOptions NetworkOptions { get; set; } = new NetworkOptions();

        [DisplayName("调试模式")]
        [Description("输出详细的请求拦截日志到 Emby 日志，用于排查问题。")]
        [Required]
        public bool EnableDebugMode { get; set; } = false;

        public SpacerItem Spacer1 { get; set; } = new SpacerItem();

        public CaptionItem VersionCaption { get; set; } = new CaptionItem(
            "构建 " + BuildDate);

        public CaptionItem ReleaseLink { get; set; } = new CaptionItem(
            "最新版本: github.com/tardlk/EmbyProxy/releases");
    }
}

