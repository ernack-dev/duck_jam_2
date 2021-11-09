using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Duck_Jam_2
{
	public class Camera
	{
		public Vector2 position { get; set; }
		private Vector2 velocity;
		private float speed;

		public Camera(Vector2 position)
		{
			this.position = position;
			this.speed = 512.0f;
			velocity = new Vector2();
		}

		public void Update(float dt)
        {
			this.position += this.velocity * dt;
        }

		public void Move(Vector2 direction)
        {
			direction.Normalize();
			this.velocity = direction * this.speed;
        }
		public void Stop()
        {
			this.velocity = new Vector2();
        }

		public Vector2 Center()
        {
			return this.position + Screen.Dimension() / 2;
        }

		public void SetCenter(Vector2 center)
		{
			this.position = center - Screen.Dimension() / 2;
		}
	}
}