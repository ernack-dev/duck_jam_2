using System;
using Microsoft.Xna.Framework;

namespace Duck_Jam_2
{
	public class IdleOrder : UnitOrder
	{
		private UnitTransportLayer transport;
		private UnitSteeringLayer steering;
		private float time;
		private float timer;

		public IdleOrder(Unit unit, float time): base(unit)
		{
			Init(time);
		}

		public IdleOrder(Unit unit) : base(unit)
		{
			Init(-1f);
		}

		private void Init(float time)
		{
			this.transport = new UnitTransportLayer(unit);
			this.steering = new UnitSteeringLayer(unit);
			this.time = time;
			this.timer = 0.0f;
		}

		public override void Update(float dt)
		{
			Vector2 avoid = this.steering.Avoid(this.unit.entities);

			this.transport.Move(avoid);

			this.transport.Update(dt);
			this.steering.Update(dt);

			if (this.time >= 0.0f)
            {
				if (this.timer >= this.time)
                {
					this.is_done = true;
					return;
                }

				this.timer += dt;
			}
			
		}
	}
}