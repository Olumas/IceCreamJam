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

            foreach(Weapon w in weapons)
                Entity.Scene.AddEntity(w);

            activeWeapon.defaultVisible = true;
        }

        public void CycleForward() {
            activeWeapon.OnUnequipped();
            activeWeapon = weapons.RemoveFront();
            activeWeapon.OnEquipped();

            weapons.AddBack(activeWeapon);
        }

        public void CycleBackwards() {
            activeWeapon.OnUnequipped();
            activeWeapon = weapons.RemoveBack();
            activeWeapon.OnEquipped();

            weapons.AddFront(activeWeapon);
        }

        public void Shoot() {
            activeWeapon.Shoot();
        }

        public void Update() {
            if(animationComponent == null)
                animationComponent = Entity.GetComponent<PlayerAnimationComponent>();

            Vector2 weaponOffset;
            if(animationComponent.Animator.CurrentFrame == 1)
                weaponOffset = new Vector2(0, -15);
            else
                weaponOffset = new Vector2(0, -16);

            foreach(Weapon w in weapons) {
                w.Position = Entity.Position + weaponOffset;
            }

            if (InputManager.shoot.IsDown)
                Shoot();
            if (InputManager.switchWeapon.Value > 0)
                CycleForward();
            if (InputManager.switchWeapon.Value < 0)
                CycleBackwards();
        }
    }
}
