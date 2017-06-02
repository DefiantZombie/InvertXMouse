using ColossalFramework;
using ColossalFramework.UI;
using InvertXMouse.Configuration;
using InvertXMouse.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using SexyFishHorse.CitiesSkylines.Logger;
using SexyFishHorse.CitiesSkylines.Redirection;
using SexyFishHorse.CitiesSkylines.Redirection.Attributes;
using UnityEngine;
using ILogger = SexyFishHorse.CitiesSkylines.Logger.ILogger;


namespace InvertXMouse.Detours
{
    [TargetType(typeof(CameraController))]
    public class CameraControllerDetour : CameraController
    {
        private static Dictionary<MethodInfo, RedirectCallsState> _redirects;

        private static FieldInfo _cameraMouseRotateField = typeof(CameraController).GetField("m_cameraMouseRotate", BindingFlags.NonPublic | BindingFlags.Instance);
        private static FieldInfo _invertYMouseField = typeof(CameraController).GetField("m_invertYMouse", BindingFlags.NonPublic | BindingFlags.Instance);
        private static FieldInfo _angleVelocityField = typeof(CameraController).GetField("m_angleVelocity", BindingFlags.NonPublic | BindingFlags.Instance);
        private static FieldInfo _mouseSensitivityField = typeof(CameraController).GetField("m_mouseSensitivity", BindingFlags.NonPublic | BindingFlags.Instance);
        private static FieldInfo _edgeScrollingField = typeof(CameraController).GetField("m_edgeScrolling", BindingFlags.NonPublic | BindingFlags.Instance);
        private static FieldInfo _velocityField = typeof(CameraController).GetField("m_velocity", BindingFlags.NonPublic | BindingFlags.Instance);
        private static FieldInfo _edgeScrollSensitivity = typeof(CameraController).GetField("m_edgeScrollSensitivity", BindingFlags.NonPublic | BindingFlags.Instance);

        private static ILogger _logger;


        public static void Hook(ILogger logger = null)
        {
            if (_redirects != null) return;

            _logger = logger ?? PanelLogger.Instance;

            _redirects = RedirectionUtil.RedirectType(typeof(CameraControllerDetour));

            _logger.Info("CameraController hook set.");
        }


        public static void Unhook()
        {
            if (_redirects == null) return;

            foreach(var redirect in _redirects)
            {
                RedirectionHelper.RevertRedirect(redirect.Key, redirect.Value);
            }

            _redirects = null;

            _logger.Info("CameraController hook removed.");
        }


        [RedirectMethod]
        private void HandleMouseEvents(float multiplier)
        {
            var invertYMouse = (SavedBool)_invertYMouseField.GetValue(this);

            Vector2 vector2 = Vector2.zero;
            var invertXMouse = ModConfig.Instance.GetSetting<bool>(SettingKeys.InvertXMouse);

            if (((SavedInputKey)_cameraMouseRotateField.GetValue(this)).IsPressed() || SteamController.GetDigitalAction(SteamController.DigitalInput.RotateMouse))
            {
                var mouseX = !invertXMouse ? Input.GetAxis("Mouse X") : -Input.GetAxis("Mouse X");
                var mouseY = !(bool)invertYMouse ? Input.GetAxis("Mouse Y") : -Input.GetAxis("Mouse Y");
                vector2 = new Vector2(mouseX, mouseY);

                if((double)vector2.magnitude > 0.0500000007450581 && Cursor.visible)
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
            else if(!Cursor.visible)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }

            var angleVelocity = (Vector2)_angleVelocityField.GetValue(this);
            var mouseSensitivity = (SavedFloat)_mouseSensitivityField.GetValue(this);
            _angleVelocityField.SetValue(this, angleVelocity + vector2 * (12f * (float)mouseSensitivity * multiplier));

            if(this.m_analogController)
            {
                float axis = Input.GetAxis("RotationHorizontalCamera");
                float num = Input.GetAxis("RotationVerticalCamera");

                if ((bool)invertYMouse)
                    num = -num;

                if (invertXMouse)
                    axis = -axis;

                if ((double)axis != 0.0 || (double)num != 0.0)
                    _angleVelocityField.SetValue(this, angleVelocity + new Vector2(axis * this.m_GamepadAngleVelocityScalar.x, num * this.m_GamepadAngleVelocityScalar.y) * multiplier);
            }

            var edgeScrolling = (SavedBool)_edgeScrollingField.GetValue(this);
            if (!(bool)edgeScrolling)
                return;

            Vector3 mousePosition = Input.mousePosition;
            float width = (float)Screen.width;
            float height = (float)Screen.height;
            float num1 = 10f;
            float min = 0.0f;

            if ((double)mousePosition.x < (double)num1)
                min = Mathf.Clamp((float)(1.0 - (double)mousePosition.x / (double)num1), min, 1f);
            if ((double)mousePosition.x > (double)width - (double)num1)
                min = Mathf.Clamp((float)(1.0 - ((double)width - (double)mousePosition.x) / (double)num1), min, 1f);
            if ((double)mousePosition.y < (double)num1)
                min = Mathf.Clamp((float)(1.0 - (double)mousePosition.y / (double)num1), min, 1f);
            if ((double)mousePosition.y > (double)height - (double)num1)
                min = Mathf.Clamp((float)(1.0 - ((double)height - (double)mousePosition.y) / (double)num1), min, 1f);
            if ((double)min == 0.0)
                return;

            Vector3 vector3 = new Vector3(mousePosition.x - width * 0.5f, 0.0f, mousePosition.y - height * 0.5f);
            vector3.Normalize();

            var velocity = (Vector3)_velocityField.GetValue(this);
            var edgeScrollSensitivity = (SavedFloat)_edgeScrollSensitivity.GetValue(this);

            _velocityField.SetValue(this, velocity + vector3 * (min * 2f * (float)edgeScrollSensitivity) * multiplier * Time.deltaTime);
            this.ClearTarget();
        }
    }
}
