using ICities;


namespace InvertXMouse
{
    public class InvertXMouse : IUserMod
    {
        public static bool InvertXMouseOption = true;

        public string Name
        {
            get
            {
                DebugLog.Init();
                return "Invert X Mouse";
            }
        }
    
        public string Description => "Adds the missing Invert X option for the camera.";
    }
}
