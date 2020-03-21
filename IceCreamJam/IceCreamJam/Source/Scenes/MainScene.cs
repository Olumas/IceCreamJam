﻿using IceCreamJam.Source.Entities;
using Nez;
using Microsoft.Xna.Framework;
using IceCreamJam.Source.Content;

namespace IceCreamJam.Source.Scenes {
    class MainScene : Scene {

        TilemapLoader loader;
        Entity truck;

        public override void Initialize() {
            loader = AddSceneComponent(new TilemapLoader());
        }

        public override void OnStart() {
            truck = AddEntity(new Truck() { Position = new Vector2(Screen.Width / 2, Screen.Height / 2) } );
            loader.Load(ContentPaths.Test1);
            Camera.AddComponent(new FollowCamera(truck));
        }
    }
}
