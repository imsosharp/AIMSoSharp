using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using BehaviorSharp;
using BehaviorSharp.Components.Conditionals;
using LeagueSharp;
using LeagueSharp.Common;

namespace AIM.Autoplay
{
    class Load
    {
        public Load()
        {
            Game.OnWndProc += OnWndProc;
            CustomEvents.Game.OnGameLoad += OGU;
        }

        public static void OnWndProc(EventArgs args)
        {
            //Draw AIMLoading.jpg
        }
        public static void OGU(EventArgs args)
        {
            if (Utility.Map.GetMap().Type == Utility.Map.MapType.HowlingAbyss)
            {
                var newCarryInstance = new Modes.Carry();
                newCarryInstance.Init();
            }
        }
    }
}
