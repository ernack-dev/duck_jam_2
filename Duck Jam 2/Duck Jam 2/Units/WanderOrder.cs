using System;
using Microsoft.Xna.Framework;

namespace Duck_Jam_2
{
	public class WanderOrder : UnitOrder
	{
		private UnitTransportLayer transport;
		private UnitSteeringLayer steering;
        private Vector2 destination;
        private float slow_down;

		public WanderOrder(Unit unit) : base(unit)
        {
            this.transport = new UnitTransportLayer(unit);
            this.steering = new UnitSteeringLayer(unit);

            Random rand = new Random();
            this.slow_down = (float)rand.NextDouble();

            this.destination = new Vector2(
                    rand.Next(-3000, 3000),
                    rand.Next(-3000, 3000)
            );
        }

        public override void Update(float dt)
        {
            base.Update(dt);

            Vector2 arrive = this.steering.Arrive(this.destination);
            Vector2 avoid = this.steering.Avoid(this.unit.entities);

            Random rand = new Random();

            if (arrive.LengthSquared() == 0)
            {
                this.is_done = true;

                WanderOrder next_order = new WanderOrder(this.unit);
                next_order.destination = new Vector2(
                    rand.Next(-3000, 3000),
                    rand.Next(-3000, 3000)
                );

                this.unit.AddOrder(next_order);
            }

            this.transport.Move(this.slow_down * arrive + avoid);
            this.transport.Update(dt);
        }
    }
}