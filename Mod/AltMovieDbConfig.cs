using System;

namespace EmbyProxy.Mod
{
    public class AltMovieDbConfig
    {
        public bool IsActive { get; private set; }

        public void Apply()
        {
            if (IsActive) return;

            var options = Plugin.Instance.MainOptionsStore.GetOptions().TmdbOptions;
            if (!options.EnableAltTmdb) return;

            try
            {
                HandlerInterceptor.Apply();
                IsActive = true;

                Plugin.Instance.Logger.Info("AltMovieDbConfig enabled");
                if (!string.IsNullOrEmpty(options.AltTmdbApiUrl))
                    Plugin.Instance.Logger.Info($"  API URL: {options.AltTmdbApiUrl}");
                if (!string.IsNullOrEmpty(options.AltTmdbImageUrl))
                    Plugin.Instance.Logger.Info($"  Image URL: {options.AltTmdbImageUrl}");
            }
            catch (Exception e)
            {
                Plugin.Instance.Logger.Warn($"AltMovieDbConfig failed: {e.Message}");
            }
        }

        public void Remove()
        {
            if (!IsActive) return;
            HandlerInterceptor.Remove();
            IsActive = false;
            Plugin.Instance.Logger.Info("AltMovieDbConfig disabled");
        }
    }
}
