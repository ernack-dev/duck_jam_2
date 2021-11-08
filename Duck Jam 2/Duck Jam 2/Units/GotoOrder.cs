using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace Duck_Jam_2
{
	public class GotoOrder : UnitOrder
	{
		private UnitSteeringLayer steering;
		private UnitTransportLayer transport;
		Vector2 position;

		public GotoOrder(Unit unit, Vector2 position) : base(unit)
		{
			this.position = position;
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

			if (arrive.LengthSquared() == 0)
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
