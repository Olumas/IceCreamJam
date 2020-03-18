using System;

namespace IceCreamJam.Source {
    class Constants {

        public const string TiledLayerBuildings = "Buildings";
        public const string TiledPropertyID = "TileID";

        [Flags]
        public enum PhysicsLayers {
            None = 0,
            Buildings = 1,
            Player = 2,
            NPC = 4,
            PlayerProjectiles = 8,
            EnemyProjectiles = 16
        }

    }
}
