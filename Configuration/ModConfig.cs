using SexyFishHorse.CitiesSkylines.Infrastructure.Configuration;


namespace InvertXMouse.Configuration
{
    public static class ModConfig
    {
        private static IConfigurationManager _instance;


        public static IConfigurationManager Instance
        {
            get { return _instance ?? (_instance = ConfigurationManager.Create(InvertXMouse.ModName)); }
        }
    }
}
