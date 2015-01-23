using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AIM.Autoplay.Util.Data;
using AIM.Autoplay.Util.Objects;
using BehaviorSharp;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using AIM.Autoplay.Util.Helpers;
using AIM.Autoplay.Util.Data;

namespace AIM.Autoplay.Modes
{
    class Carry : Base
    {
        public void Init()
        {
            Game.OnGameUpdate += OnGameUpdate;
            CustomEvents.Game.OnGameLoad += OnGameLoad;
        }

        public override void OnGameLoad(EventArgs args) { }

        public override void OnGameUpdate(EventArgs args)
        {
            ImpingAintEasy();
        }

        public void ImpingAintEasy()
        {
            Obj_AI_Minion leadingMinion = MetaHandler.LeadMinion();
            Vector2 orbwalkingPos = new Vector2();
            orbwalkingPos.X = leadingMinion.ServerPosition.X + ObjConstants.DefensiveAdditioner;
            orbwalkingPos.Y = leadingMinion.ServerPosition.Y + ObjConstants.DefensiveAdditioner;
            OrbW.ExecuteMixedMode(orbwalkingPos.To3D());
        }
    }
}
