using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Duck_Jam_2
{
	public class Footer: Widget
	{
		private Panel panel;
		private TextWidget text;

		public Footer() : base(Screen.GridPosition(0, 8))
		{
			this.is_fixed = true;
			this.panel = (Panel) AddChild(new Panel(0, 0, 12, 4, Color.Red));
			this.panel.is_fixed = true;

			this.text = (TextWidget) AddChild(new TextWidget(new Vector2(0, 0), "Salut Monde !"));
			this.text.is_fixed = true;
		}

		public void ChangeText(string new_text)
		{
			this.text.text = new_text;
		}
	}
}