using System;
using System.Diagnostics;
using System.Collections;
using Microsoft.Xna.Framework;

namespace Duck_Jam_2
{
	public class UnitSteeringLayer
	{
		private Unit unit;

		public UnitSteeringLayer(Unit unit)
		{
			this.unit = unit;
		}

		public Vector2 Avoid(ArrayList others)
        {
			Vector2 result = new Vector2();
			if (others.Count == 0) { return result;  }
			int count = 0;

			foreach (Entity other in others)
			{ 
				if (other.GetType() == typeof(Unit) && ((Unit)other).is_dead)
                {
					continue;
                }

				if (other != this.unit && (other.Center() - this.unit.Center()).Length() <= 64.0f)
				{
					Vector2 dist = other.position - this.unit.position + this.unit.velocity * this.unit.speed;
					dist.Normalize();
					result -= dist;
					count++;
				}
            }

			if (count > 0)
			{
				result /= (float) count;
				return result * this.unit.speed;
			}

			return new Vector2();
        }
		public Vector2 Arrive(Vector2 position)
        {
			float dist = (this.unit.Center() - position).Length();
			float destination_dist = 16.0f;
			
			Vector2 dir = position - (this.unit.Center() + this.unit.velocity);
			dir.Normalize();
			float arrive_dist = 64.0f;

			Vector2 next = dir * this.unit.speed;

			if (dist < arrive_dist && dist >= destination_dist)
            {
				float ratio = (float) Math.Sin(dist / arrive_dist);
				next = dir * ratio * this.unit.speed;
			}
			else if (dist < destination_dist)
            {
				return new Vector2();
			}

			return next;
        }

		public void Update(float dt)
        {
        }
	}
}