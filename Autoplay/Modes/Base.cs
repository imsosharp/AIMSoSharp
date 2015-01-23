﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using AIM.Autoplay.Util;
using AIM.Autoplay.Util.Data;
using AIM.Autoplay.Util.Objects;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using AutoLevel = LeagueSharp.Common.AutoLevel;

namespace AIM.Autoplay.Modes
{
    internal class Base
    {
        public Constants ObjConstants { get; protected set; }
        public Heroes ObjHeroes { get; protected set; }
        public Minions ObjMinions { get; protected set; }
        public Turrets ObjTurrets { get; protected set; }
        public Autoplay.Util.Orbwalker OrbW { get; set; }
        public virtual void OnGameLoad(EventArgs args) { }
        public virtual void OnGameUpdate(EventArgs args) { }

        #region Menu
        public Menu Menu;
        public void LoadMenu()
        {
            Menu = new Menu("AIM", "AIM", true);
            Menu.AddItem(new MenuItem("Enabled", "Enabled").SetValue(new KeyBind(32, KeyBindType.Toggle)));
            Menu.AddItem(new MenuItem("LowHealth", "Self Low Health %").SetValue(new Slider(20, 10, 50)));
            Menu.AddToMainMenu();
        }
        #endregion Menu

    }
}
