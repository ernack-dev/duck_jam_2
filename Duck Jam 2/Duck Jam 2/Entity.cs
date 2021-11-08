using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Duck_Jam_2
{
    public enum EntityType
    {
        Unit,
        Resource
    }

    public class Entity
    {
        protected Texture2D texture;

        public bool is_selected { get; set; }
        public Vector2 position { get; set; }
        public Vector2 velocity { get; set; }
        public float speed { get; set; }
        public EntityType type { get; private set; }

        public Entity(EntityType type, string texture_name, Vector2 position)
        {
            this.texture = Assets.Get<Texture2D>(texture_name);
            this.position = position;
            this.type = type;
        }
        public virtual Vector2 Center()
        {
            return new Vector2(
                this.position.X + this.texture.Width / 2,
                this.position.Y + this.texture.Height / 2
            );
        }

        public virtual void Update(float dt)
        {

        }

        public virtual void Display(SpriteBatch batch)
        {
            batch.Draw(this.texture, this.position, Color.White);
        }

        public virtual void OnEvent(Event my_event)
        {
     
        }
    }
}