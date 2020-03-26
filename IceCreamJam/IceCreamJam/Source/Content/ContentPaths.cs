namespace IceCreamJam.Source.Content {
    class ContentPaths {
        public const string Content = "Content/";
        public const string Sprites = Content + "Sprites/";

        public const string BoxSprite = Sprites + "Box.png";

        public const string Truck = Sprites + "Truck/";

        #region NPC
        public const string NPC = Sprites + "NPC/";
        #endregion

        #region Weapons
        public const string Weapons = Sprites + "Weapons/";
        public const string TestProjectile = Weapons + "TestProjectile.png";
        public const string TestProjectile2 = Weapons + "TestProjectile2.png";

        public const string Scoop = Weapons + "Scoop/";
        public const string Scoop_Base = Scoop + "Scoop_Base.png";
        public const string Scoop_Cone = Scoop + "Scoop_Cone.png";

        public const string Popsicle = Weapons + "Popsicle/";
        public const string Popsicle_Base = Popsicle + "Popsicle_Base.png";
        public const string Popsicle_Stick = Popsicle + "Popsicle_Stick.png";
        #endregion

        #region Maps
        public const string TiledMaps = Content + "TiledMaps/";
        public const string Test1 = TiledMaps + "Test1.tmx";
        #endregion
    }
}
