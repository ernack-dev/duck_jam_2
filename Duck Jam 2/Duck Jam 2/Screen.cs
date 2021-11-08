using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Duck_Jam_2
{
	public class Screen
	{
		public static GraphicsDevice device;
		public static Vector2 scroll_limits = new Vector2(64.0f, 64.0f);
		public static Camera camera = new Camera(new Vector2());
		public static int Width()
		{
			return Screen.device.Viewport.Width;
		}

		public static int Height()
		{
			return Screen.device.Viewport.Height;
		}

		public static Vector2 Dimension()
		{
			return new Vector2(Screen.Width(), Screen.Height());
		}

		public static Vector2 GridPosition(int x, int y)
		{
			return new Vector2((float) x * Screen.Width() / 12.0f, (float) y * Screen.Height() / 12.0f);
		}

		public static float GridWidth(float w)
		{
			return w * Screen.Width() / 12.0f;
		}

		public static float GridHeight(float h)
		{
			return h * Screen.Height() / 12.0f;
		}
	}
}