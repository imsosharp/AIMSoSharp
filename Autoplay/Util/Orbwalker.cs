﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using SharpDX;
using AIM.Autoplay.Util.Data;
using AIM.Autoplay.Util.Objects;
using LeagueSharp.Common;

namespace AIM.Autoplay.Util
{
    public class Orbwalker
    {
        public Orbwalker()
        {

        }
        public void ExecuteMixedMode(Vector3 pos)
        {
            var spellbook = ObjectManager.Player.Spellbook;
            if (!spellbook.IsChanneling && !spellbook.IsAutoAttacking && !spellbook.IsCharging &&
                !spellbook.IsCastingSpell)
            {
                WalkAround(pos);
                if (!CanLastHit())
                {
                    Obj_AI_Hero target = TargetSelector.GetTarget(
                        Heroes.Me.AttackRange, TargetSelector.DamageType.Physical);
                    if (target != null && target.IsValid && !target.IsDead && State.IsBotSafe() &&
                        !target.UnderTurret(true) && !Variables.OverrideAttackUnitAction)
                    {
                        Heroes.Me.IssueOrder(GameObjectOrder.AttackUnit, target);
                    }
                }
                else
                {
                    LastHit();
                }
            }
        }

        private void WalkAround(Vector3 pos)
        {
            Randoms.RandRange = Randoms.Rand.Next(-267, 276);
            Randoms.RandSeconds = Randoms.Rand.Next(1000, 4000);
            if (Environment.TickCount - Randoms.StepTime >= Randoms.RandSeconds && !Variables.OverrideAttackUnitAction)
            {
                if (Heroes.Me.Team == GameObjectTeam.Order)
                {
                    int orbwalkingAdditionInteger = Randoms.RandRange * (-1);
                    Variables.OrbwalkingPos.X = pos.X + orbwalkingAdditionInteger;
                    Variables.OrbwalkingPos.Y = pos.Y + orbwalkingAdditionInteger;
                }
                else
                {
                    int orbwalkingAdditionInteger = Randoms.RandRange;
                    Variables.OrbwalkingPos.X = pos.X + orbwalkingAdditionInteger;
                    Variables.OrbwalkingPos.Y = pos.Y + orbwalkingAdditionInteger;
                }
                if (Variables.OrbwalkingPos != null)
                {
                    Heroes.Me.IssueOrder(GameObjectOrder.MoveTo, Variables.OrbwalkingPos.To3D());
                    Randoms.StepTime = Environment.TickCount;
                }
            }

        }

        private void WalkAround(Obj_AI_Hero follow)
        {
            Randoms.RandRange = Randoms.Rand.Next(-367, 376);
            Randoms.RandSeconds = Randoms.Rand.Next(500, 3500);
            if (Environment.TickCount - Randoms.StepTime >= Randoms.RandSeconds && !Variables.OverrideAttackUnitAction)
            {
                if (Heroes.Me.Team == GameObjectTeam.Order)
                {
                    int orbwalkingAdditionInteger = Randoms.RandRange * (-1);
                    Variables.OrbwalkingPos.X = follow.Position.X + orbwalkingAdditionInteger;
                    Variables.OrbwalkingPos.Y = follow.Position.Y + orbwalkingAdditionInteger;
                }
                else
                {
                    int orbwalkingAdditionInteger = Randoms.RandRange;
                    Variables.OrbwalkingPos.X = follow.Position.X + orbwalkingAdditionInteger;
                    Variables.OrbwalkingPos.Y = follow.Position.Y + orbwalkingAdditionInteger;
                }
                if (Variables.OrbwalkingPos != null && Heroes.Me.Distance(follow) < 550)
                {
                    Heroes.Me.IssueOrder(GameObjectOrder.MoveTo, Variables.OrbwalkingPos.To3D());
                    Randoms.StepTime = Environment.TickCount;
                }

            }
        }

        public bool CanLastHit()
        {
            if (ObjectManager.Get<Obj_AI_Minion>().Any(minion =>
                                minion.IsValidTarget() && Heroes.Me.Distance(minion) < Heroes.Me.AttackRange &&
                                minion.Health <
                                2 * (ObjectManager.Player.BaseAttackDamage + ObjectManager.Player.FlatPhysicalDamageMod)))
            {
                return true;
            }
            return false;
        }

        public void LastHit()
        {
            var target =
                    ObjectManager.Get<Obj_AI_Minion>()
                        .FirstOrDefault(
                            minion =>
                                minion.IsValidTarget() && Heroes.Me.Distance(minion) < Heroes.Me.AttackRange &&
                                minion.Health <
                                2 * (ObjectManager.Player.BaseAttackDamage + ObjectManager.Player.FlatPhysicalDamageMod));

            if (target != null) Heroes.Me.IssueOrder(GameObjectOrder.AttackUnit, target);
        }
    }
}
