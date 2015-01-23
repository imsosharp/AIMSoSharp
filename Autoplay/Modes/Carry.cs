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
using BehaviorSharp.Components.Actions;
using AutoLevel = LeagueSharp.Common.AutoLevel;

namespace AIM.Autoplay.Modes
{
    class Carry : Base
    {
        private bool _playingSummonersRift;
        public void Init(bool loadSummonersRiftLogic)
        {
            Game.OnGameUpdate += OnGameUpdate;
            CustomEvents.Game.OnGameLoad += OnGameLoad;
            _playingSummonersRift = loadSummonersRiftLogic;
        }

        public override void OnGameLoad(EventArgs args)
        {
            LoadMenu();
            new AutoLevel(Util.Data.AutoLevel.GetSequence());
            ObjConstants.AssignConstants();
            ObjHeroes.CreateHeroesList();
            ObjConstants = new Constants();
            ObjHeroes = new Heroes();
            ObjMinions = new Minions();
            ObjTurrets = new Turrets();
            OrbW = new Autoplay.Util.Orbwalker();
        }

        public override void OnGameUpdate(EventArgs args)
        {
            ObjHeroes.SortHeroesListByDistance();
            ObjMinions.UpdateMinions();
            ObjTurrets.UpdateTurrets();

            ImpingAintEasy();

        }

        public void ImpingAintEasy()
        {

            Obj_AI_Minion leadingMinion = new Obj_AI_Minion();
            BehaviorAction summonersRiftOrbwalking = new BehaviorAction(
                () =>
                {
                    if (_playingSummonersRift)
                    {
                        leadingMinion = MetaHandler.LeadMinion(SummonersRift.BottomLane.Bottom_Zone.CenterOfPolygone().To3D());
                        return BehaviorState.Success;
                    }
                    else
                    {
                        leadingMinion = MetaHandler.LeadMinion();
                        return BehaviorState.Success;
                    }
                    return BehaviorState.Failure;
                });
            summonersRiftOrbwalking.Tick();


            Vector2 orbwalkingPos = new Vector2();
            orbwalkingPos.X = leadingMinion.ServerPosition.X + ObjConstants.DefensiveAdditioner;
            orbwalkingPos.Y = leadingMinion.ServerPosition.Y + ObjConstants.DefensiveAdditioner;
            OrbW.ExecuteMixedMode(orbwalkingPos.To3D());
        }
    }
}
