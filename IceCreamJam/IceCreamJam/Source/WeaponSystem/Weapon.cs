﻿using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;
using System;
using System.Collections;

namespace IceCreamJam.Source.WeaponSystem {
    abstract class Weapon : Entity {

        public Type projectileType;
        public string name;
        public float reloadTime;
        public bool canShoot = true;
        public string texturePath;

        private ICoroutine reloadCoroutine;

        public Component weaponComponent;

        public RenderableComponent renderer;

        public Weapon() {
            projectileType = typeof(Projectile);
        }

        public virtual void OnEquipped() { }
        public virtual void OnUnequipped() { }

        public override void OnAddedToScene() {
            base.OnAddedToScene();

            InitializeRenderer();
        }

        public virtual void InitializeRenderer() {
            var texture = Scene.Content.LoadTexture(texturePath);
            this.renderer = AddComponent(new SpriteRenderer(texture));
        }

        public virtual void Shoot() {
            if(canShoot) {
                canShoot = false;
                reloadCoroutine = Core.StartCoroutine(ReloadTimer());

                // Instantiate a projectile using the weapon's projectile type
                var scene = weaponComponent.Entity.Scene;
                var dir = Vector2.Normalize(scene.Camera.MouseToWorldPoint() - weaponComponent.Entity.Position);
                var p = InstantiateProjectile(dir, this.Position);
                weaponComponent.Entity.Scene.AddEntity(p);
            }
        }

        public virtual Projectile InstantiateProjectile(Vector2 dir, Vector2 pos) {
            var p = (Projectile)Activator.CreateInstance(projectileType, dir);
            p.Position = pos;
            return p;
        }

        IEnumerator ReloadTimer() {
            yield return Coroutine.WaitForSeconds(reloadTime);
            canShoot = true;
        }
    }
}
