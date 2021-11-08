using System;
using Microsoft.Xna.Framework;

namespace Duck_Jam_2
{
	public class IdleOrder : UnitOrder
	{
		private UnitTransportLayer transport;
		private UnitSteeringLayer steering;

		public IdleOrder(Unit unit): base(unit)
		{
			this.transport = new UnitTransportLayer(unit);
			this.steering = new UnitSteeringLayer(unit);
		}

		public override void Update(float dt)
		{
			Vector2 avoid = this.steering.Avoid(this.unit.entities);

			this.transport.Move(avoid);

			this.transport.Update(dt);
			this.steering.Update(dt);
		}
	}
}