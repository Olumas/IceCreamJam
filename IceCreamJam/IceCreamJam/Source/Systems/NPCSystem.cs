using IceCreamJam.Source.Entities;
using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections.Generic;

namespace IceCreamJam.Source.Systems {
    class NPCSystem : EntitySystem {
        private Truck truck;

        private const int approachRadius = 150;
        private const int stopRadius = 75;
        private const int awayRadius = 50;

        public NPCSystem(Matcher matcher) : base(matcher) { }

        protected override void Process(List<Entity> entities) {
            base.Process(entities);

            if(truck == null)
                truck = (Truck)Scene.FindEntity("Truck");

            foreach(Entity e in entities) {
                var npc = (NPC)e;

                var distance = Vector2.Distance(truck.Position, npc.Position);
                if(distance <= 0)
                    continue;

                // Flip to face truck
                npc.Flip(truck.Position.X < npc.Position.X);

                var direction = Vector2.Normalize(truck.Position - npc.Position);

                var vector = Vector2.Zero;
                var avoid = GetAvoidVector(npc, entities);

                if(distance <= approachRadius && distance >= stopRadius)
                    vector = direction;
                else if(distance <= awayRadius) {
                    var angle = Mathf.Atan2(truck.rb.Velocity.Y, truck.rb.Velocity.X);

                    var b = truck.Position;
                    var a = truck.Position + truck.rb.Velocity;
                    var position = Math.Sign((b.X - a.X) * (npc.Position.Y - a.Y) - (b.X - a.X) * (npc.Position.X - a.X));
                    var offset = Mathf.Deg2Rad * 90 * position;
                    angle += offset;

                    var perpendicular = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                    direction = Vector2.Normalize(direction + perpendicular);

                    vector = (-direction) * truck.rb.Velocity.Length() / 2 * (1 / distance);
                }

                var final = vector + avoid;

                npc.Move(final);
                npc.PauseWalk(final.Length() == 0);
            }
        }

        private Vector2 GetAvoidVector(NPC npc, List<Entity> entities) {
            var avoid = new Vector2();
            var count = 0;

            foreach(Entity o in entities) {
                var dir = (npc.Position - o.Position);
                var dist = dir.Length();

                if(dist < 20f && dist > 0) {
                    avoid += dir / (float)Mathf.Pow(dist, 5);
                    count++;
                }
            }

            if(count != 0)
                avoid /= count;

            if(avoid.Length() > 0)
                avoid.Normalize();
            else
                avoid = Vector2.Zero;

            return avoid;
        }
    }
}
