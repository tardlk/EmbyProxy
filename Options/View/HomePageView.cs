using MediaBrowser.Model.Plugins.UI.Views;
using EmbyProxy.Options.Store;
using EmbyProxy.UIBaseClasses.Views;
using System.Threading.Tasks;

namespace EmbyProxy.Options.View
{
    internal class HomePageView : PluginPageView
    {
        private readonly PluginOptionsStore _store;

        public HomePageView(string pluginId, PluginOptionsStore store)
            : base(pluginId)
        {
            _store = store;
            ContentData = store.GetOptions();
        }

        public PluginOptions PluginOptions => ContentData as PluginOptions;

        public override Task<IPluginUIView> OnSaveCommand(string itemId, string commandId, string data)
        {
            _store.SetOptions(PluginOptions);
            return base.OnSaveCommand(itemId, commandId, data);
        }
    }
}
