using ICities;
using InvertXMouse.Configuration;
using InvertXMouse.Detours;
using InvertXMouse.Logging;
using System;
using System.Collections;
using SexyFishHorse.CitiesSkylines.Infrastructure;
using SexyFishHorse.CitiesSkylines.Logger;
using UnityEngine;
using ILogger = SexyFishHorse.CitiesSkylines.Logger.ILogger;


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


        public void OnEnabled()
        {
            CameraControllerDetour.Hook();
            _logger.Info("Enabled");
        }


        public void OnDisabled()
        {
            CameraControllerDetour.Unhook();
            _logger.Info("Disabled");
        }
    }
}
