﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using SharpDX;

namespace AIM.Autoplay.Util.Data
{
    public class Variables
    {
        public static Obj_AI_Hero Carry;
        public static Obj_AI_Hero NearestAllyHero;
        public static Obj_AI_Turret NearestAllyTurret;
        public static Vector2 FrontLine;
        public static Vector2 SafePos;
        public static Vector2 SafeRecall;
        public static Vector2 OrbwalkingPos;
        public static bool BypassLoadedCheck = false;
        public static bool OverrideAttackUnitAction = false;
        public static int LastSwitched = 0;
        public static bool TookRecallDecision = false;
        public static int LastTimeTookRecallDecision = 0;
    }
}
