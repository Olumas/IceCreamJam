using Nez;
using System.Collections.Generic;

namespace IceCreamJam.Source.WeaponSystem {
    // For any entity which can use weapons
    class WeaponComponent : Component, IUpdatable {
        public Deque<Weapon> weapons;
        public Weapon activeWeapon;

        public WeaponComponent(params Weapon[] weapons) : this(new Deque<Weapon>(weapons)) { }

        public WeaponComponent(Deque<Weapon> weapons) {
            this.weapons = weapons;

            foreach(Weapon w in weapons)
                w.weaponComponent = this;

            activeWeapon = weapons.Get(0);
        }

        public override void OnAddedToEntity() {
            base.OnAddedToEntity();
        }

        public void CycleForward() {
            activeWeapon = weapons.RemoveFront();
            weapons.AddBack(activeWeapon);
        }

        public void CycleBackwards() {
            activeWeapon = weapons.RemoveBack();
            weapons.AddFront(activeWeapon);
        }

        public void Shoot() {
            activeWeapon.Shoot();
        }

        public void Update() {
            if (InputManager.shoot.IsDown)
                Shoot();
            if (InputManager.switchWeapon.Value > 0)
                CycleForward();
            if (InputManager.switchWeapon.Value < 0)
                CycleBackwards();
        }
    }
}
