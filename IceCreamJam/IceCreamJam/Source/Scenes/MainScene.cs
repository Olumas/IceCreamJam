using IceCreamJam.Source.Components;
using IceCreamJam.Source.Content;
using IceCreamJam.Source.Entities;
using IceCreamJam.Source.Entities.Civilians;
using IceCreamJam.Source.Entities.Enemies;
using IceCreamJam.Source.Systems;
using IceCreamJam.Source.Tiled;
using IceCreamJam.Source.UI;
using Microsoft.Xna.Framework;
using Nez;

namespace IceCreamJam.Source.Scenes {
    class MainScene : Scene {

        TilemapLoader loader;
        Entity truck;

        public override void Initialize() {
            loader = AddSceneComponent(new TilemapLoader());
            SetDesignResolution(1280, 720, SceneResolutionPolicy.ShowAll);
            AddRenderer(new DefaultRenderer());

            AddEntityProcessor(new HomingProjectileSystem(new Matcher().All(typeof(HomingTargetComponent))));
            AddEntityProcessor(new CivilianSystem(new Matcher().All(typeof(CivilianComponent))));
        }

        public override void OnStart() {
            truck = AddEntity(new Truck() { Position = new Vector2(Screen.Width / 2, Screen.Height / 2) } );

            //for(int i = 0; i < 5; i++) 
            //    AddEntity(new Civilian(ContentPaths.NPC + $"NPC{i}.png") { Position = new Vector2(Screen.Width / 2 + i * 32, Screen.Height / 2) });

            for(int i = 0; i < 3; i++) {
                var d = Pool<Doctor>.Obtain();
                d.Initialize(new Vector2(Random.NextInt(Screen.Width), Random.NextInt(Screen.Height)));

                if(d.isNewEnemy)
                    AddEntity(d);
            }

            //AddEntity(new Doctor() { Position = new Vector2(Screen.Width / 2, Screen.Height / 2 + 200) });
            //AddEntity(new Doctor() { Position = new Vector2(Screen.Width / 2, Screen.Height / 2 + 400) });

            AddEntity(new Crosshair());

            loader.Load(ContentPaths.Test1);
            Camera.ZoomIn(0.5f);
            Camera.AddComponent(new FollowCamera(truck));
        }
    }
}
