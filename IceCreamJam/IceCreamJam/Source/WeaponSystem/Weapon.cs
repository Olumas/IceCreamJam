using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections;

namespace IceCreamJam.Source.WeaponSystem {
    abstract class Weapon {

        public Type projectileType;
        public string name;
        public float reloadTime;
        public bool canShoot = true;

        private ICoroutine reloadCoroutine;

        public Component weaponComponent;

        //public void OnEquipped() { }
        //public void OnUnequipped() { }

        public Weapon() {
            projectileType = typeof(Projectile);
        }

        public void Shoot() {
            if(canShoot) {
                canShoot = false;
                reloadCoroutine = Core.StartCoroutine(ReloadTimer());

                // Instantiate a projectile using the weapon's projectile type
                var scene = weaponComponent.Entity.Scene;
                var dir = Vector2.Normalize(scene.Camera.MouseToWorldPoint() - weaponComponent.Entity.Position);
                var p = (Projectile)Activator.CreateInstance(projectileType, dir);
                weaponComponent.Entity.Scene.AddEntity(p);
            }
        }

        IEnumerator ReloadTimer() {
            yield return Coroutine.WaitForSeconds(reloadTime);
            canShoot = true;
        }
    }
}
