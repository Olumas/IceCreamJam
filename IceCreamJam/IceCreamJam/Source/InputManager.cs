using Microsoft.Xna.Framework.Input;
using Nez;

namespace IceCreamJam.Source {
	static class InputManager {
		public static readonly VirtualButton up, down, left, right;
		public static readonly VirtualAxis drive, steer;

		public static readonly VirtualButton shoot;
		// not sure if necessary
		public static readonly VirtualIntegerAxis switchWeapon;

		static InputManager() {
			up = new VirtualButton().AddKeyboardKey(Keys.W);
			down = new VirtualButton().AddKeyboardKey(Keys.D);
			left = new VirtualButton().AddKeyboardKey(Keys.A);
			right = new VirtualButton().AddKeyboardKey(Keys.D);

			drive = new VirtualAxis();
			drive.Nodes.Add(new VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.W, Keys.S));
			drive.Nodes.Add(new VirtualAxis.GamePadLeftStickY());

			steer = new VirtualAxis();
			steer.Nodes.Add(new VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.A, Keys.D));
			steer.Nodes.Add(new VirtualAxis.GamePadLeftStickX());

			shoot = new VirtualButton();
			shoot.AddMouseLeftButton();
			shoot.AddGamePadRightTrigger(0, 0.2f);

			var prevKey = new VirtualButton(new VirtualButton.KeyboardKey(Keys.Q));
			var nextKey = new VirtualButton(new VirtualButton.KeyboardKey(Keys.E));
			var prevGamepad = new VirtualButton(new VirtualButton.GamePadButton(0, Buttons.X));
			var nextGamepad = new VirtualButton(new VirtualButton.GamePadButton(0, Buttons.A));

			switchWeapon = new VirtualIntegerAxis();
			switchWeapon.Nodes.Add(new ButtonAxis(prevKey, nextKey));
			switchWeapon.Nodes.Add(new ScrollAxis());
			switchWeapon.Nodes.Add(new ButtonAxis(prevGamepad, nextGamepad));
		}

		// this class are a bit of a hack
		public class ButtonAxis : VirtualAxis.Node {
			public override float Value => _value;
			float _value;
			bool towardsPositive;

			public VirtualButton Positive;
			public VirtualButton Negative;

			public ButtonAxis(VirtualButton negative, VirtualButton positive) {
				Negative = negative;
				Positive = positive;
			}

			public override void Update() {
				if (Positive.IsDown && Negative.IsDown) {
					// new press means change the direction accordingly
					if (Positive.IsPressed && !Positive.IsRepeating)
						towardsPositive = true;
					else if (Negative.IsPressed && !Negative.IsRepeating)
						towardsPositive = false;
					_value = towardsPositive ? Positive.IsPressed ? 1 : 0 : Negative.IsPressed ? -1 : 0;
				} else if (Positive.IsPressed && !Negative.IsDown) {
					_value = 1;
				} else if (Negative.IsPressed && !Positive.IsDown) {
					_value = -1;
				} else {
					_value = 0;
				}
			}
		}

		public class ScrollAxis : VirtualAxis.Node {
			public override float Value => _value;
			float _value;
			public override void Update() {
				if (Input.MouseWheelDelta >= 120) {
					_value = 1;
				} else if (Input.MouseWheelDelta <= -120) {
					_value = -1;
				} else {
					_value = 0;
				}
			}
		}
	}
}
