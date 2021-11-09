using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Duck_Jam_2
{
	public class Rect
	{
		
		public Vector2 Position { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		public Color FillColor { get; }

		private Texture2D texture;

		public Rect(int x, int y, int w, int h, Color color)
		{
			this.Width = Math.Max(1, w);
			this.Height = Math.Max(1, h);
			this.Position = new Vector2(x, y);
			this.FillColor = color;
			Resize();
		}

		public void Resize()
        {
			this.texture = new Texture2D(Screen.device, this.Width, this.Height);

			Color[] data = new Color[this.Width * this.Height];

			for (int i = 0; i < data.Length; i++)
			{
				data[i] = this.FillColor;
			}

			this.texture.SetData(data);
		}

		public void Draw(SpriteBatch batch)
        {
			batch.Draw(this.texture, this.Position, Color.White);
        }
	}
}
