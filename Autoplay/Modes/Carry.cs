using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
        public Carry():base()
        {
            Game.OnGameUpdate += OnGameUpdate;
            CustomEvents.Game.OnGameLoad += OnGameLoad;
        }

        public override void OnGameLoad(EventArgs args)
        {
            LoadMenu();
            try
            {
                Game.PrintChat("AIM Loaded!");
                new AutoLevel(Util.Data.AutoLevel.GetSequence());
                ObjConstants.AssignConstants();
                ObjHeroes.CreateHeroesList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public override void OnGameUpdate(EventArgs args)
        {
            ObjHeroes.SortHeroesListByDistance();
            ObjTurrets.UpdateTurrets();

            ImpingAintEasy();
            RefreshMinions();

        }

        public void ImpingAintEasy()
        {
            new AutoLevel(Util.Data.AutoLevel.GetSequence());
            MetaHandler.DoChecks(); //#TODO rewrite MetaHandler with BehaviorSharp
            #region OrbwalkAtLeadingMinionLocation
            new BehaviorAction(
                () =>
                {
                    try
                    {
                        if (LeadingMinion != null)
                        {
                            Vector2 orbwalkingPos = new Vector2();
                            orbwalkingPos.X = LeadingMinion.ServerPosition.X + ObjConstants.DefensiveAdditioner;
                            orbwalkingPos.Y = LeadingMinion.ServerPosition.Y + ObjConstants.DefensiveAdditioner;
                            OrbW.ExecuteMixedMode(orbwalkingPos.To3D());
                            return BehaviorState.Success;
                        }
                        return BehaviorState.Failure;
                    }
                    catch (NullReferenceException e)
                    {
                        Console.WriteLine(e);
                    }
                    return BehaviorState.Failure;

                }).Tick();
            #endregion OrbwalkAtLeadingMinionLocation
        }
    }
}
