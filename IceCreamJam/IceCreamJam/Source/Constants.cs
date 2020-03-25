using System;

namespace IceCreamJam.Source {
    class Constants {

        public const string TiledLayerBuildings = "Buildings";
        public const string TiledPropertyID = "TileID";

        public const int GlobalFPS = 8;

        [Flags]
        public enum PhysicsLayers {
            None = 0,
            Buildings = 1,
            Player = 2,
            NPC = 4,
            PlayerProjectiles = 8,
            EnemyProjectiles = 16
        }

        public const int Layer_Weapon = 3;
        public const int Layer_Bullets = 4;
        public const int Layer_Truck = 5;
        public const int Layer_NPC = 5;
        public const int Layer_Buildings = 6;   
    }
}
