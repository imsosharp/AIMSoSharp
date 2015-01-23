using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BehaviorSharp;
using BehaviorSharp.Components.Actions;
using BehaviorSharp.Components.Composites;
using BehaviorSharp.Components.Decorators;
using LeagueSharp;

namespace AIM.Autoplay.Behaviors
{
    class Safety
    {
        private static readonly Obj_AI_Hero Player = ObjectManager.Player;
        private static Obj_AI_Hero FollowTarget;
        public static Sequence RecallSequence;

        public static Sequence GetSequence()
        {
            var UsePots = new BehaviorAction(
                () =>
                {
                    var pot = Player.InventoryItems.FirstOrDefault(i => i.Id == ItemId.Health_Potion);
                    if (pot == null)
                    {
                        return BehaviorState.Success;
                    }
                    Player.Spellbook.CastSpell(pot.SpellSlot);
                    return BehaviorState.Failure;
                });

            var PrepareRecall = new Sequence(
                new Inverter(Utils.IsAtFountain()), new Inverter(Utils.IsDead()),
                new Inverter(Utils.IsPlayerRecalling()), Utils.StopOrbwalker());

            var CastRecall =
                new BehaviorAction(
                    () => Player.Spellbook.CastSpell(SpellSlot.Recall) ? BehaviorState.Success : BehaviorState.Failure);

            RecallSequence = new Sequence(PrepareRecall, CastRecall);

            // add move away from enemy
            var NormalRecallLogic = new Sequence(Utils.IsLowHealth(), UsePots, Utils.IsEnemyNear(500), RecallSequence);

            return new Sequence(NormalRecallLogic, RecallSequence);
        }
    }
}
