using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace Duck_Jam_2
{
	public class EndScene : Scene
	{
        private TextWidget text;
        private Neptunia game;

		public EndScene(Neptunia game)
		{
            this.game = game;
            this.text = new TextWidget(new Vector2(0, 0), "Congrat, you killed your target !");
            this.text.is_fixed = true;
		}

        public void Display(SpriteBatch batch)
        {
            this.text.Draw(batch);
        }

        public void OnEvent(Event my_event)
        {
           
        }

        public void Update(float dt)
        {
           
        }
    }
}
