using System.Diagnostics;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Duck_Jam_2
{
	public class Panel : Widget
	{
		private Rect rect;
		public bool need_change { get; set; }

		public Panel(int x, int y, int w, int h, Color color) 
			: base(Screen.GridPosition(x, y))
		{
			this.size = new Vector2(Screen.GridWidth((float)w), Screen.GridHeight((float)h));

			this.rect = new Rect(
				(int)this.position.X,
				(int)this.position.Y,
				(int)this.size.X,
				(int)this.size.Y,
				color
			);
		}

		public Panel(float x, float y, float w, float h, Color color)
			: base(new Vector2(x, y))
		{
			this.size = new Vector2(w, h);

			this.rect = new Rect(
				(int)this.position.X,
				(int)this.position.Y,
				(int)this.size.X,
				(int)this.size.Y,
				color
			);
		}

        public override void Update(float dt)
        {
            base.Update(dt);

			if (this.need_change)
			{
				this.need_change = false;
				this.rect.Position = new Vector2(
					this.position.X,
					this.position.Y
				);

				this.rect.Width = Math.Max(1, (int)this.size.X);
				this.rect.Height = Math.Max(1, (int)this.size.Y);
				this.rect.Resize();
			}
		}

        public override void Draw(SpriteBatch batch)
		{
			if (!this.is_fixed)
			{
				Vector2 old = this.rect.Position;

				this.rect.Position -= Screen.camera.position;
				if (this.parent != null)
				{
					this.rect.Position += this.parent.AbsolutePosition();
				}
				this.rect.Draw(batch);

				this.rect.Position = old;
			}
			else
			{
				Vector2 old = this.rect.Position;

				if (this.parent != null)
				{
					this.rect.Position += this.parent.AbsolutePosition();
				}

				this.rect.Draw(batch);

				this.rect.Position = old;
			}
			base.Draw(batch);
		}
	}
}