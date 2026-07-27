using Emby.Web.GenericEdit;
using MediaBrowser.Model.Attributes;
using System.ComponentModel;

namespace EmbyProxy.Options
{
    public class TmdbOptions : EditableOptionsBase
    {
        public override string EditorTitle => "TMDB";

        [DisplayName("启用替代 TMDB 配置")]
        [Required]
        public bool EnableAltTmdb { get; set; } = false;

        [DisplayName("替代 TMDB API 地址")]
        [VisibleCondition(nameof(EnableAltTmdb), SimpleCondition.IsTrue)]
        public string AltTmdbApiUrl { get; set; } = "https://api.tmdb.org";

        [DisplayName("替代 TMDB 图片地址")]
        [VisibleCondition(nameof(EnableAltTmdb), SimpleCondition.IsTrue)]
        public string AltTmdbImageUrl { get; set; } = string.Empty;


    }
}
