
using Microsoft.Xna.Framework;

public class GameInputs
{
	public static int mouse_x = 0;
	public static int mouse_y = 0;
	public static bool left_mouse_released = true;
	public static bool right_mouse_released = true;

	public static Vector2 mouse_pos()
    {
		return new Vector2(mouse_x, mouse_y);
    }
}
