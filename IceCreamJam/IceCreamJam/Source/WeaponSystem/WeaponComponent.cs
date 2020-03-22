using IceCreamJam.Source.Components;
using Microsoft.Xna.Framework;
using Nez;
using System.Collections.Generic;

namespace IceCreamJam.Source.WeaponSystem {
    class WeaponComponent : Component, IUpdatable {
        public Deque<Weapon> weapons;
        public Weapon activeWeapon;

        public PlayerAnimationComponent animationComponent;

        public WeaponComponent(params Weapon[] weapons) : this(new Deque<Weapon>(weapons)) { }

        public WeaponComponent(Deque<Weapon> weapons) {
            this.weapons = weapons;

            foreach(Weapon w in weapons)
                w.weaponComponent = this;

            activeWeapon = weapons.Get(0);
        }

        public override void OnAddedToEntity() {
            base.OnAddedToEntity();

            foreach(Weapon w in weapons) {
                Entity.Scene.AddEntity(w);
                w.SetEnabled(false);
            }
        }

        public void CycleForward() {
            activeWeapon.SetEnabled(false);
            activeWeapon.OnUnequipped();

            activeWeapon = weapons.RemoveFront();
            activeWeapon.SetEnabled(true);
            activeWeapon.OnEquipped();

            weapons.AddBack(activeWeapon);
        }

        public void CycleBackwards() {
            activeWeapon.SetEnabled(false);
            activeWeapon.OnUnequipped();

            activeWeapon = weapons.RemoveBack();
            activeWeapon.SetEnabled(true);
            activeWeapon.OnEquipped();

            weapons.AddFront(activeWeapon);
        }

        public void Shoot() {
            activeWeapon.Shoot();
        }

        public void Update() {
            if(animationComponent == null)
                animationComponent = Entity.GetComponent<PlayerAnimationComponent>();

            if(animationComponent.Animator.CurrentFrame == 1)
                activeWeapon.Position = Entity.Position + new Vector2(0, -15);
            else
                activeWeapon.Position = Entity.Position + new Vector2(0, -16);

            if (InputManager.shoot.IsDown)
                Shoot();
            if (InputManager.switchWeapon.Value > 0)
                CycleForward();
            if (InputManager.switchWeapon.Value < 0)
                CycleBackwards();
        }
    }
}
