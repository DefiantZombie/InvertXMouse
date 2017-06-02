using ColossalFramework;
using ColossalFramework.UI;
using InvertXMouse.Redirection;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


namespace InvertXMouse.Detours
{
    public class CameraControllerDetour
    {
        private static Dictionary<MethodInfo, RedirectCallsState> _redirects;
        private static bool _initialized = false;


        public static void Hook()
        {
            if (_redirects != null) return;

            _redirects = RedirectionUtility.RedirectType(typeof(CameraControllerDetour));
        }


        public static void Unhook()
        {
            if (_redirects == null) return;

            foreach(var redirect in _redirects)
            {
                RedirectionHelper.RevertRedirect(redirect.Key, redirect.Value);
            }

            _redirects = null;
            _initialized = false;
        }
            throw new NotImplementedException();
        }
    }
}
