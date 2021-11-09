
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Duck_Jam_2
{
	public class GameInputs
	{
		public static int mouse_x = 0;
		public static int mouse_y = 0;
		public static bool left_mouse_released = true;
		public static bool right_mouse_released = true;
		public static Keys keypress = Keys.None;

		public static Vector2 mouse_pos()
		{
			return new Vector2(mouse_x, mouse_y);
		}

		public static Vector2 camera_mouse_pos()
		{
			return new Vector2(mouse_x, mouse_y) + Screen.camera.position;
		}
	}
}