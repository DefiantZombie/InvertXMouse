using InvertXMouse.Configuration;
using InvertXMouse.Logging;
using SexyFishHorse.CitiesSkylines.Infrastructure;
using SexyFishHorse.CitiesSkylines.Logger;
using System;


namespace InvertXMouse
{
    public class InvertXMouse : UserModBase
    {
        public const string ModName = "Invert X Mouse";

        private readonly ILogger _logger;


        public override string Name
        {
            get { return ModName; }
        }

        public override string Description
        {
            get { return "Adds the missing Invert X option for the camera."; }
        }


        public InvertXMouse()
        {
            try
            {
                _logger = PanelLogger.Instance;
                _logger.Info("InvertXMouse created");

                ModConfig.Instance.Logger = _logger;

                OptionsPanelManager = new OptionsPanelManager(_logger);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                throw;
            }
        }
    }
}
