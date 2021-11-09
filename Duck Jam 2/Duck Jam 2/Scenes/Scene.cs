using System;
using Microsoft.Xna.Framework.Graphics;

namespace Duck_Jam_2
{
    public interface Scene: EventObserver
    {
        public abstract void Update(float dt);
        public abstract void Display(SpriteBatch batch);
    }
}