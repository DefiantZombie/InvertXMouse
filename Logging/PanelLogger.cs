using ColossalFramework.Plugins;
using InvertXMouse.Configuration;
using SexyFishHorse.CitiesSkylines.Logger;
using System;


namespace InvertXMouse.Logging
{
    public class PanelLogger : ILogger
    {
        private static ILogger _internalLogger;

        private ILogger _logger;

        public static bool Enabled { get; set; }

        public static ILogger Instance
        {
            get { return _internalLogger ?? (_internalLogger = new PanelLogger()); }
        }


        private PanelLogger()
        {
            try
            {
                _logger = LogManager.Instance.GetOrCreateLogger(InvertXMouse.ModName);
                Enabled = ModConfig.Instance.GetSetting<bool>(SettingKeys.EnableLogging);
            }
            catch (Exception ex)
            {
                DebugOutputPanel.AddMessage(PluginManager.MessageType.Error,
                    "[InvertXMouse][PanelLogger]: " + ex.Message);
            }
        }


        public void Dispose()
        {
            _logger.Dispose();
        }


        public void Error(string message, params object[] args)
        {
            try
            {
                _logger.Error(message, args);
            }
            catch (Exception ex)
            {
                DebugOutputPanel.AddMessage(PluginManager.MessageType.Error,
                    "[InvertXMouse][PanelLogger.Error] ERROR: " + ex.Message);
            }
        }


        public void Info(string message, params object[] args)
        {
            if (!Enabled)
                return;

            try
            {
                _logger.Info(message, args);
            }
            catch (Exception ex)
            {
                DebugOutputPanel.AddMessage(PluginManager.MessageType.Error,
                    "[InvertXMouse][PanelLogger.Info] ERROR: " + ex.Message);
            }
        }


        public void Log(PluginManager.MessageType messageType, string message, params object[] args)
        {
            if (!Enabled && (messageType != PluginManager.MessageType.Error))
                return;

            try
            {
                _logger.Log(messageType, message, args);
            }
            catch (Exception ex)
            {
                DebugOutputPanel.AddMessage(PluginManager.MessageType.Error,
                    "[InvertXMouse][PanelLogger.Log] ERROR: " + ex.Message);
            }
        }


        public void LogException(Exception exception)
        {
            try
            {
                _logger.LogException(exception);
            }
            catch (Exception ex)
            {
                DebugOutputPanel.AddMessage(PluginManager.MessageType.Error,
                    "[InvertXMouse][PanelLogger.LogException] ERROR: " + ex.Message);
            }
        }


        public void Warn(string message, params object[] args)
        {
            if (!Enabled)
                return;

            try
            {
                _logger.Warn(message, args);
            }
            catch (Exception ex)
            {
                DebugOutputPanel.AddMessage(PluginManager.MessageType.Error,
                    "[InvertXMouse][PanelLogger.Warn] ERROR: " + ex.Message);
            }
        }
    }
}
