﻿using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace AIM.Autoplay.Util.Data
{
    public class Constants
    {
        private const int Blue = 200;
        private const int Purple = -200;
        public Vector2 BotLanePos { get; private set; }
        public Vector2 TopLanePos { get; private set; }
        public int AggressiveAdditioner { get; private set; }
        public int DefensiveAdditioner { get; private set; }
        public int LoadedTickCount { get; internal set; }
        public float LowHealthRatio { get { return 0.3f; } }
        public float LowManaRatio { get { return 0.1f; } }
        public float LowHealthIfLowManaRatio { get { return 0.6f; } }
        public Utility.Map Map;

        public  void AssignConstants()
        {
            Map = Utility.Map.GetMap();
            if (Map.Type != null && Map.Type == Utility.Map.MapType.SummonersRift)
            {
                Vector2 BotLanePos = new Vector2();
                Vector2 TopLanePos = new Vector2();

                if (Objects.Heroes.Me.Team == GameObjectTeam.Order)
                {
                    AggressiveAdditioner = Blue + Randoms.Rand.Next(-76, 76);
                    DefensiveAdditioner = Purple + Randoms.Rand.Next(-67, 67);
                    BotLanePos.X = 11376 + Randoms.Rand.Next(-50, 50);
                    BotLanePos.Y = 1062 + Randoms.Rand.Next(-50, 50);
                    TopLanePos.X = 1302 + Randoms.Rand.Next(-50, 50);
                    TopLanePos.Y = 10249 + Randoms.Rand.Next(-50, 50);
                }
                if (Objects.Heroes.Me.Team == GameObjectTeam.Chaos)
                {
                    AggressiveAdditioner = Purple + Randoms.Rand.Next(-67, 67);
                    DefensiveAdditioner = Blue + Randoms.Rand.Next(-76, 76);
                    BotLanePos.X = 13496 + Randoms.Rand.Next(-50, 50);
                    BotLanePos.Y = 4218 + Randoms.Rand.Next(-50, 50);
                    TopLanePos.X = 4849 + Randoms.Rand.Next(-50, 50);
                    TopLanePos.Y = 13535 + Randoms.Rand.Next(-50, 50);
                }
            }
        }
    }
}