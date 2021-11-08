using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Duck_Jam_2
{
	public class SceneWidget : Widget
	{
		private Scene scene;

		public SceneWidget(Widget parent, Scene scene, Vector2 position): base(position)
		{
			this.scene = scene;
		}

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
			scene.Display(batch);
        }
    }
}
