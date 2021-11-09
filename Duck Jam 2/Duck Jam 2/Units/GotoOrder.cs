using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace Duck_Jam_2
{
	public class GotoOrder : UnitOrder
	{
		private UnitSteeringLayer steering;
		private UnitTransportLayer transport;
		Vector2 position;
		float distance;

		public GotoOrder(Unit unit, Vector2 position, float distance) : base(unit)
		{
			Init(unit, position, distance);
		}

		public GotoOrder(Unit unit, Vector2 position) : base(unit)
		{
			Init(unit, position, 0.0f);
		}

		private void Init(Unit unit, Vector2 position, float distance)
        {
			this.position = position;
			this.distance = distance;
			this.transport = new UnitTransportLayer(unit);
			this.steering = new UnitSteeringLayer(unit);
		}

		public void Retarget(Vector2 new_position)
        {
			this.position = new_position;
        }

        public override void Update(float dt)
        {
            base.Update(dt);
			Vector2 arrive = (
				this.steering.Arrive(this.position)
			);
			
			Vector2 avoid = this.steering.Avoid(this.unit.entities);

			if (
				arrive.LengthSquared() <= 0 ||
				(this.unit.position - this.position).LengthSquared() < this.distance * this.distance
			)
			{ 
				this.is_done = true;
				return;
			}
			
			this.transport.Move(arrive + avoid * 0.8f);
			
			this.transport.Update(dt);

			this.steering.Update(dt);
        }
    }
}
