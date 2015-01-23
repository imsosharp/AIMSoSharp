using System;
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
        public Constants ObjConstants { get; private set; }
        public Heroes ObjHeroes { get; private set; }
        public Minions ObjMinions { get; private set; }
        public Turrets ObjTurrets { get; private set; }
        public Autoplay.Util.Orbwalker OrbW { get; set; }



        public virtual void OnGameLoad(EventArgs args)
        {
            new AutoLevel(Util.Data.AutoLevel.GetSequence());
            ObjConstants.AssignConstants();
            ObjHeroes.CreateHeroesList();
            ObjConstants = new Constants();
            ObjHeroes = new Heroes();
            ObjMinions = new Minions();
            ObjTurrets = new Turrets();
            OrbW = new Autoplay.Util.Orbwalker();
        }

        public virtual void OnGameUpdate(EventArgs args)
        {
            ObjHeroes.SortHeroesListByDistance();
            ObjMinions.UpdateMinions();
            ObjTurrets.UpdateTurrets();
        }

    }
}
