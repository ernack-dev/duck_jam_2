using System;

namespace Duck_Jam_2
{
	public class UnitOrder
	{
		protected Unit unit;
		protected bool is_done;

		public UnitOrder(Unit unit)
		{
			this.is_done = false;
			this.unit = unit;
		}

		public virtual void Complete()
        {
			this.is_done = true;
        }
		public virtual void Update(float dt)
        {

        }

		public virtual bool IsDone()
        {
			return this.is_done;
        }
	}
}