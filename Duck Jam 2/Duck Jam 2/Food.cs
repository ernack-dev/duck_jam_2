using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Duck_Jam_2
{
	public class Food : Entity
	{
		public Food(Vector2 position) : base(EntityType.Resource, "food", position)
		{

		}
	}
}