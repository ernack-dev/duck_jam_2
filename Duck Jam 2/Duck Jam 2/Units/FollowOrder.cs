using System;
using Microsoft.Xna.Framework;

namespace Duck_Jam_2
{
	public class FollowOrder : UnitOrder
	{
		private UnitTransportLayer transport;
		private UnitSteeringLayer steering;
		private Unit target;


		public FollowOrder(Unit unit, Unit target) : base(unit)
		{
			this.transport = new UnitTransportLayer(unit);
			this.steering = new UnitSteeringLayer(unit);
			this.target = target;
		}

		public override void Update(float dt)
		{
			base.Update(dt);

			Vector2 arrive = this.steering.Arrive(this.target.Center());
			Vector2 avoid = this.steering.Avoid(this.unit.entities);

			float dist = (this.target.Center() - this.unit.Center()).Length();
			float near_dist = 128.0f;
			float slow_down_dist = near_dist + 128.0f;

			arrive.Normalize();

			if (dist < near_dist)
            {
				arrive *= this.target.velocity.Length();
			}
			else if (dist < slow_down_dist)
            {
				arrive *= this.unit.speed * (dist/slow_down_dist);
			}
			else
            {
				arrive *= this.unit.speed;
			}

			if (dist < near_dist || dist < slow_down_dist)
			{
				if (this.unit.team == UnitTeam.Player)
				{
					this.target.Analyse(dt);
				}
			}

			this.transport.Move(arrive + avoid);
			this.transport.Update(dt);
		}
	}
}