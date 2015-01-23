using System.Collections.Generic;
using System.Linq;
using LeagueSharp;

namespace AIM.Autoplay.Util.Objects
{
    public class Minions
    {
        public List<Obj_AI_Minion> AllMinions;
        public List<Obj_AI_Minion> AllyMinions;
        public List<Obj_AI_Minion> EnemyMinions;

        public void UpdateMinions()
        {
            AllMinions = ObjectManager.Get<Obj_AI_Minion>().ToList();
            AllyMinions = AllMinions.FindAll(minion => minion.IsAlly);
            EnemyMinions = AllMinions.FindAll(minion => !minion.IsAlly);
        }
    }
}
