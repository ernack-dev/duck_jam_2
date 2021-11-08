using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Duck_Jam_2
{
	public class Ore : Entity
	{
		public Ore(Vector2 position): base(EntityType.Resource, "ore", position)
		{
			
		}
	}
}
