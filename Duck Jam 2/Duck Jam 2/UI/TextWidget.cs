using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Duck_Jam_2
{
	public class TextWidget: Widget
	{
		public string text { get; set; }
		private SpriteFont font;

		public TextWidget(Vector2 position, string text) : base(position)
		{
			this.text = text;
			this.font = Assets.Get<SpriteFont>("main_font");
		}

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
			Vector2 parent_pos;
			
			if (this.is_fixed)
			{
				batch.DrawString(this.font, this.text, this.AbsolutePosition(), Color.Black);
			}
			else
            {
				batch.DrawString(this.font, this.text, this.AbsolutePosition() - Screen.camera.position, Color.Black);
			}
        }
    }
}