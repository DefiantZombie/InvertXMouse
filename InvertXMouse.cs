using ICities;


namespace InvertXMouse
{
    public class InvertXMouse : IUserMod
    {
        public static bool InvertXMouseOption = true;

        public string Name => "Invert X Mouse";
        public string Description => "Adds the missing Invert X option for the camera.";
    }
}
