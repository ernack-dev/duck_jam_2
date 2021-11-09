using System;
using Microsoft.Xna.Framework;

namespace Duck_Jam_2
{
	public class PanicOrder : UnitOrder
	{
		private UnitTransportLayer transport;
		private UnitSteeringLayer steering;
		private Unit target;
		private Unit opponent;

		public PanicOrder(Unit unit, Unit target) : base(unit)
		{
			this.transport = new UnitTransportLayer(unit);
			this.steering = new UnitSteeringLayer(unit);
			this.target = target;

			this.opponent = null;
			float best_dist2 = float.MaxValue;

			foreach (Unit opponent in this.unit.entities)
            {
				float dist2 = (opponent.position - this.unit.position).LengthSquared();

				if (opponent.team == UnitTeam.Opponent &&
					(this.opponent == null || dist2 < best_dist2))
                {
					best_dist2 = dist2;
					this.opponent = opponent;
                }
            }
		}

		public override void Update(float dt)
		{
			base.Update(dt);

			Vector2 arrive = this.steering.Arrive(this.opponent.Center());
			Vector2 avoid = this.steering.Avoid(this.unit.entities);

			this.transport.Move(arrive + avoid);
			this.transport.Update(dt);
		}
	}
}