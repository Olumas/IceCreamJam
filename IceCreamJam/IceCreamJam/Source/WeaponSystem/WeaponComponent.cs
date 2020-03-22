using Microsoft.Xna.Framework;
using Nez;
using System.Collections.Generic;

namespace IceCreamJam.Source.WeaponSystem {
    class WeaponComponent : Component, IUpdatable {
        public List<Weapon> weapons;
        private int weaponIndex = 0;
        public Weapon ActiveWeapon => weapons[weaponIndex];

        public WeaponComponent(params Weapon[] weapons) : this(new List<Weapon>(weapons)) { }

        public WeaponComponent(List<Weapon> weapons) {
            this.weapons = weapons;

            foreach(Weapon w in weapons)
                w.weaponComponent = this;
        }

        public override void OnAddedToEntity() {
            base.OnAddedToEntity();

            foreach(Weapon w in weapons) {
                Entity.Scene.AddEntity(w);
                w.SetEnabled(false);
            }
        }

        public void CycleForward() {
            ActiveWeapon.SetEnabled(false);
            ActiveWeapon.OnUnequipped();
            weaponIndex++;
            ActiveWeapon.SetEnabled(true);
            ActiveWeapon.OnEquipped();
        }

        public void CycleBackwards() {
            ActiveWeapon.SetEnabled(false);
            ActiveWeapon.OnUnequipped();
            weaponIndex--;
            ActiveWeapon.SetEnabled(true);
            ActiveWeapon.OnEquipped();
        }

        public void Shoot() {
            ActiveWeapon.Shoot();
        }

        public void Update() {
            if (InputManager.shoot.IsDown)
                Shoot();
            if (InputManager.switchWeapon.Value > 0)
                CycleForward();
            if (InputManager.switchWeapon.Value < 0)
                CycleBackwards();

            ActiveWeapon.Position = Entity.Position + new Vector2(0, -15);
        }
    }
}
