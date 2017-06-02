using ICities;
using InvertXMouse.Detours;


namespace InvertXMouse
{
    public class LoadingExtension : LoadingExtensionBase
    {
        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);
            CameraControllerDetour.Hook();
        }


        public override void OnLevelUnloading()
        {
            base.OnLevelUnloading();
            CameraControllerDetour.Unhook();
        }
    }
}
