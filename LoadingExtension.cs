using ICities;
using InvertXMouse.Detours;
using System.Reflection;


namespace InvertXMouse
{
    public class LoadingExtension : LoadingExtensionBase
    {
        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);
            CameraControllerDetour.Hook();
            DebugLog.Log("[IXM] Hooked");
        }


        public override void OnLevelUnloading()
        {
            base.OnLevelUnloading();
            CameraControllerDetour.Unhook();
            DebugLog.Close();
        }
    }
}
