using System;
using Microsoft.Xna.Framework;

namespace Duck_Jam_2
{
	public class HomeBuilding : Entity
	{
		public HomeBuilding(Vector2 position): base(EntityType.Building, "home", position)
		{
			this.name = "Home";
		}
	}
}