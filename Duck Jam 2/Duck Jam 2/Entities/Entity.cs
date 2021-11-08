using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Duck_Jam_2
{
    public enum EntityType
    {
        Unit,
        Resource,
        Building
    }

    public class Entity : Widget
    {
        protected Texture2D texture;

        public bool is_selected { get; set; }
        public Vector2 velocity { get; set; }
        public float speed { get; set; }
        public EntityType type { get; private set; }
        
        public string name { get; set; }

        public Entity(EntityType type, string texture_name, Vector2 position)
            : base(position)
        {
            this.texture = Assets.Get<Texture2D>(texture_name);
            this.size = new Vector2(this.texture.Width, this.texture.Height);
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

        public override void Draw(SpriteBatch batch)
        {
            if (this.is_fixed)
            {
                batch.Draw(this.texture, this.position, Color.White);
            }
            else
            { 
                batch.Draw(this.texture, this.position - Screen.camera.position, Color.White);
            }
        }

    }
}