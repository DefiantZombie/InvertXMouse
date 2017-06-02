using ColossalFramework.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using SexyFishHorse.CitiesSkylines.Infrastructure.UI;
using SexyFishHorse.CitiesSkylines.Infrastructure.UI.Configuration;
using SexyFishHorse.CitiesSkylines.Infrastructure.UI.Extensions;
using SexyFishHorse.CitiesSkylines.Logger;
using InvertXMouse.Logging;


namespace InvertXMouse.Configuration
{
    public class OptionsPanelManager : IOptionsPanelManager
    {
        private readonly ILogger _logger;


        public OptionsPanelManager(ILogger logger)
        {
            _logger = logger;
            _logger.Info("OptionsPanelManager created");
        }


        public void ConfigureOptionsPanel(IStronglyTypedUiHelper uiHelper)
        {
            try
            {
                ConfigureGeneralGroup(uiHelper);
                ConfigureDebugGroup(uiHelper);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                throw;
            }
        }


        private void ConfigureGeneralGroup(IStronglyTypedUiHelper uiHelper)
        {
            var generalGroup = uiHelper.AddGroup("General");

            generalGroup.AddCheckBox(
                "Invert X",
                ModConfig.Instance.GetSetting<bool>(SettingKeys.InvertXMouse),
                OnInvertXMouseChanged);
        }


        [Conditional("DEBUG")]
        private void ConfigureDebugGroup(IStronglyTypedUiHelper uiHelper)
        {
            var debugGroup = uiHelper.AddGroup("Debugging");

            debugGroup.AddCheckBox(
                "Enable logging",
                ModConfig.Instance.GetSetting<bool>(SettingKeys.EnableLogging),
                isChecked =>
                {
                    ModConfig.Instance.SaveSetting(SettingKeys.EnableLogging, isChecked);
                    PanelLogger.Enabled = isChecked;
                });
        }


        private void OnInvertXMouseChanged(bool isChecked)
        {
            SaveSetting(SettingKeys.InvertXMouse, isChecked);
        }


        private void SaveSetting(string settingKey, object value)
        {
            _logger.Info("Saving setting {0} with value {1}", settingKey, value);

            ModConfig.Instance.SaveSetting(settingKey, value);
        }
    }
}
