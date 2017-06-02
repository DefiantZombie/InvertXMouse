using ICities;
using InvertXMouse.Detours;
using InvertXMouse.Logging;
using SexyFishHorse.CitiesSkylines.Logger;


namespace InvertXMouse
{
    public class LoadingExtension : LoadingExtensionBase
    {
        private readonly ILogger _logger;


        public LoadingExtension()
        {
            _logger = PanelLogger.Instance;
        }


        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);
            CameraControllerDetour.Hook();
            _logger.Info("Hook set.");
        }


        public override void OnLevelUnloading()
        {
            base.OnLevelUnloading();
            CameraControllerDetour.Unhook();
            _logger.Info("Hook removed.");
        }
    }
}
