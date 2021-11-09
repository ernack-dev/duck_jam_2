using System;
using Microsoft.Xna.Framework;

namespace Duck_Jam_2
{
	public class AttackOrder : UnitOrder
	{
		private UnitTransportLayer transport;
		private UnitSteeringLayer steering;
		private Unit target;
		private float atk_time = 1.0f;
		private float atk_timer = 0.0f;

		public AttackOrder(Unit unit, Unit target) : base(unit)
		{
			this.transport = new UnitTransportLayer(unit);
			this.steering = new UnitSteeringLayer(unit);
			this.target = target;
			Random rand = new Random();

			this.atk_time = (float)rand.Next(50, 75)/100.0f;
			this.atk_timer = (float) rand.NextDouble() * atk_timer;
		}

		public override void Update(float dt)
		{
			base.Update(dt);

			Vector2 arrive = this.steering.Arrive(this.target.Center());
			Vector2 avoid = this.steering.Avoid(this.unit.entities);

			float dist = (this.target.Center() - this.unit.Center()).Length();
			float near_dist = 64.0f;
			float attack_dist = near_dist + 32.0f;

			arrive.Normalize();

			if (dist < near_dist)
			{
				arrive *= this.target.velocity.Length();
			}
			else if (dist < attack_dist)
			{
				arrive *= 0;
				if (atk_timer >= atk_time)
                {
					this.target.hurt(this.unit, this.unit.atk);

					if (this.target.is_dead)
                    {
						this.is_done = true;
						return;
                    }

					atk_timer = 0.0f;
				}

				atk_timer += dt;
				
			}
			else
			{
				arrive *= this.unit.speed;
			}

			this.transport.Move(arrive + avoid);
			this.transport.Update(dt);
		}
	}
}