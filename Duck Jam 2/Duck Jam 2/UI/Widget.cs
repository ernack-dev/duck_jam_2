using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Duck_Jam_2
{ 
	public class Widget
	{
		public Widget parent { get; private set; }
		protected ArrayList children;
		public Vector2 position { get; set; }
		public Vector2 size { get; set; }
		public bool is_fixed { get; set; }

		public Widget( Vector2 position )
		{ 
			Init(position, true);
		}

		public Widget(Vector2 position, bool is_fixed)
		{
			Init(position, is_fixed);
		}

		public void Init(Vector2 position, bool is_fixed)
		{
			this.is_fixed = is_fixed;
			this.parent = null;
			this.position = position;
			this.size = new Vector2();
			this.children = new ArrayList();
			this.is_fixed = false;
		}

		public virtual Vector2 AbsolutePosition()
        {
			if (this.parent == null)
            {
				return this.position;
            }

			return this.parent.AbsolutePosition() + this.position;
        }

		public Widget AddChild(Widget widget)
		{
			widget.parent = this;
			this.children.Add(widget);
			return widget;
		}

		public virtual void Draw(SpriteBatch batch)
        {
			foreach (Widget child in this.children)
            {
				child.Draw(batch);
            }
        }

		public virtual bool IsAtPosition(Vector2 position)
		{
			return position.X >= this.position.X && position.X <= this.position.X + this.size.X &&
			   position.Y >= this.position.Y && position.Y <= this.position.Y + this.size.Y;
		}
		public virtual void Update(float dt)
		{
			foreach (Widget child in this.children)
			{
				child.Update(dt);
			}
		}
	}
}