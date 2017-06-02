using ICities;
using System;
using System.Collections;
using SexyFishHorse.CitiesSkylines.Infrastructure;
using UnityEngine;


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
            
        }
    }
}
