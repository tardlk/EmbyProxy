using MediaBrowser.Model.Plugins;
using MediaBrowser.Model.Plugins.UI.Views;
using EmbyProxy.Options.Store;
using EmbyProxy.UIBaseClasses;
using System.Threading.Tasks;

namespace EmbyProxy.Options.View
{
    internal class MainPageController : ControllerBase
    {
        private readonly PluginOptionsStore _store;

        public MainPageController(PluginInfo pluginInfo, PluginOptionsStore store)
            : base(pluginInfo.Id)
        {
            _store = store;
            PageInfo = new PluginPageInfo
            {
                Name = "EmbyProxy",
                EnableInMainMenu = true,
                DisplayName = "EmbyProxy",
                MenuIcon = "video_settings",
            };
        }

        public override PluginPageInfo PageInfo { get; }

        public override Task<IPluginUIView> CreateDefaultPageView()
        {
            return Task.FromResult<IPluginUIView>(new HomePageView(PluginId, _store));
        }
    }
}
