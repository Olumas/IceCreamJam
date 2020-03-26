using IceCreamJam.Source.Components;
using Microsoft.Xna.Framework;
using Nez;
using System.Collections.Generic;

namespace IceCreamJam.Source.WeaponSystem {
    class WeaponComponent : Component, IUpdatable {
        public List<Weapon> weapons;
        private int weaponIndex = 0;
        public Weapon ActiveWeapon => weapons[weaponIndex];
        public PlayerAnimationComponent animationComponent;

        public WeaponComponent(params Weapon[] weapons) : this(new List<Weapon>(weapons)) { }

        public WeaponComponent(List<Weapon> weapons) {
            this.weapons = weapons;

            foreach(Weapon w in weapons)
                w.weaponComponent = this;
        }

        public override void OnAddedToEntity() {
            base.OnAddedToEntity();

            foreach(Weapon w in weapons)
                Entity.Scene.AddEntity(w);

            ActiveWeapon.defaultVisible = true;
        }

        public void CycleForward() {
            ActiveWeapon.SetEnabled(false);
            ActiveWeapon.OnUnequipped();
            weaponIndex = Utility.Mod(weaponIndex + 1, weapons.Count);
            ActiveWeapon.SetEnabled(true);
            ActiveWeapon.OnEquipped();
        }

        public void CycleBackwards() {
            ActiveWeapon.SetEnabled(false);
            ActiveWeapon.OnUnequipped();
            weaponIndex = Utility.Mod(weaponIndex - 1, weapons.Count);
            ActiveWeapon.SetEnabled(true);
            ActiveWeapon.OnEquipped();
        }

        public void Shoot() {
            ActiveWeapon.Shoot();
        }

        public void Update() {
            UpdateWeapons();

            // Process Input
            if (InputManager.shoot.IsDown)
                Shoot();
            if (InputManager.switchWeapon.Value > 0)
                CycleForward();
            if (InputManager.switchWeapon.Value < 0)
                CycleBackwards();
        }

        private void UpdateWeapons() {
            if(animationComponent == null)
                animationComponent = Entity.GetComponent<PlayerAnimationComponent>();

            Vector2 weaponOffset;
            if(animationComponent.Animator.CurrentFrame == 1)
                weaponOffset = new Vector2(0, -15);
            else
                weaponOffset = new Vector2(0, -16);

            foreach(Weapon w in weapons)
                w.Position = Entity.Position + weaponOffset;
        }
    }
}
