using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Duck_Jam_2
{
	public class ProgressBar : Widget
	{
        private Panel content;
        private Panel bar;
        private float ratio;
        public bool is_visible { get; set; }
		public ProgressBar(Vector2 position, Vector2 size, Color color) : base(position)
		{
            this.is_visible = true;
            this.content = new Panel(position.X, position.Y, size.X, size.Y, Color.Black);
            this.bar = new Panel(position.X, position.Y, size.X, size.Y, color);
            this.content.is_fixed = false;
            this.bar.is_fixed = false;
            this.ratio = 1.0f;

            AddChild(this.content);
            AddChild(this.bar);
        }

        public void SetPosition(Vector2 newpos)
        {
            this.content.position = newpos;
            this.bar.position = newpos;
        }

        public void SetValue(float ratio)
        {
            if (ratio != this.ratio)
            {
                this.bar.size = new Vector2(this.content.size.X * ratio, this.content.size.Y);
                this.bar.need_change = true;
                this.ratio = ratio;
            }
            
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            
        }
        public override void Draw(SpriteBatch batch)
        {
            if (this.is_visible)
            {
                base.Draw(batch);
            }
            
        }


    }
}