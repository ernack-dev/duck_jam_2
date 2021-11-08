
using Microsoft.Xna.Framework;

namespace Duck_Jam_2
{
	public enum UnitSpeed
    {
		SLOW = 0,
		FAST = 1
    }

	public class UnitTransportLayer
	{
		private Unit unit;
		

		public UnitTransportLayer(Unit unit)
		{
			this.unit = unit;
			
		}
		
		public void Move(Vector2 offset)
        {
			this.unit.velocity = offset;
        }

		public void Update(float dt)
        {
			this.unit.position += this.unit.velocity * dt;
			this.unit.velocity = new Vector2();
		}
		
	}
}