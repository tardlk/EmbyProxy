using System;

namespace EmbyProxy.Mod
{
    public class ForceIPv4
    {
        public bool IsActive { get; private set; }

        public void Apply()
        {
            if (IsActive) return;

            var options = Plugin.Instance.MainOptionsStore.GetOptions().NetworkOptions;
            if (!options.EnableIPv4Only) return;

            try
            {
                HandlerInterceptor.Apply();
                IsActive = true;

                var count = string.IsNullOrEmpty(options.IPv4OnlyDomains) ? 0 :
                    options.IPv4OnlyDomains.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;
                Plugin.Instance.Logger.Info($"ForceIPv4 enabled for {count} domains");
            }
            catch (Exception e)
            {
                Plugin.Instance.Logger.Warn($"ForceIPv4 failed: {e.Message}");
            }
        }

        public void Remove()
        {
            if (!IsActive) return;
            HandlerInterceptor.Remove();
            IsActive = false;
            Plugin.Instance.Logger.Info("ForceIPv4 disabled");
        }
    }
}
