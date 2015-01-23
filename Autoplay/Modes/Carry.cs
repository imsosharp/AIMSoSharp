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
        public void Init()
        {
            Game.OnGameUpdate += OnGameUpdate;
            CustomEvents.Game.OnGameLoad += OnGameLoad;
        }

        public override void OnGameLoad(EventArgs args)
        {
            //LoadMenu();
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
            if (ObjHeroes.AllHeroes != null) {ObjHeroes.SortHeroesListByDistance();}
            if (ObjMinions.AllMinions != null) {ObjMinions.UpdateMinions();}
            if (ObjTurrets.AllTurrets != null) {ObjTurrets.UpdateTurrets();}

            ImpingAintEasy();

        }

        public void ImpingAintEasy()
        {
            new AutoLevel(Util.Data.AutoLevel.GetSequence());
            MetaHandler.DoChecks(); //#TODO rewrite MetaHandler with BehaviorSharp
            #region SetLeadingMinion
            Obj_AI_Minion leadingMinion = null;
            new BehaviorAction(
                () =>
                {
                    if (Utility.Map.GetMap().Type == Utility.Map.MapType.SummonersRift)
                    {
                        if (leadingMinion == null || !leadingMinion.IsValid)
                        {
                            leadingMinion =
                                MetaHandler.LeadMinion(SummonersRift.BottomLane.Bottom_Zone.CenterOfPolygone().To3D());
                            Game.PrintChat("Leading minion assigned");
                            return BehaviorState.Success;
                        }
                    }
                    else
                    {
                        if (leadingMinion == null || !leadingMinion.IsValid)
                        {
                            leadingMinion = MetaHandler.LeadMinion();
                            return BehaviorState.Success;
                        }
                    }
                    return BehaviorState.Failure;
                }).Tick();
            #endregion SetLeadingMinion
            #region OrbwalkAtLeadingMinionLocation
            new BehaviorAction(
                () =>
                {
                    try
                    {
                        if (leadingMinion != null)
                        {
                            Vector2 orbwalkingPos = new Vector2();
                            orbwalkingPos.X = leadingMinion.ServerPosition.X + ObjConstants.DefensiveAdditioner;
                            orbwalkingPos.Y = leadingMinion.ServerPosition.Y + ObjConstants.DefensiveAdditioner;
                            OrbW.ExecuteMixedMode(orbwalkingPos.To3D());
                            return BehaviorState.Success;
                        }
                        return BehaviorState.Failure;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    return BehaviorState.Failure;

                }).Tick();
            #endregion OrbwalkAtLeadingMinionLocation
        }
    }
}
