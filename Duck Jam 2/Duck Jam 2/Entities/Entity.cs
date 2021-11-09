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

    public enum EntityEyes
    {
        Open,
        Close,
        Dead
    }

    public enum EntityMouth
    {
        Fear,
        Happy,
        Neutral,
        Sad
    }

    public class Entity : Widget
    {
        protected Texture2D texture;
        protected Texture2D eyes_texture;
        protected Texture2D mouth_texture;

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
            SetEyes(EntityEyes.Open);
            SetMouth(EntityMouth.Neutral);
        }

        public void SetEyes(EntityEyes eyes)
        {
            switch (eyes)
            {
                case EntityEyes.Open: this.eyes_texture = Assets.Get<Texture2D>("open_eyes"); break;
                case EntityEyes.Close: this.eyes_texture = Assets.Get<Texture2D>("close_eyes"); break;
                case EntityEyes.Dead: this.eyes_texture = Assets.Get<Texture2D>("dead_eyes"); break;
            }
            
        }

        public void SetMouth(EntityMouth mouth)
        {
            switch (mouth)
            {
                case EntityMouth.Happy: this.mouth_texture = Assets.Get<Texture2D>("mouth_happy"); break;
                case EntityMouth.Sad: this.mouth_texture = Assets.Get<Texture2D>("mouth_sad"); break;
                case EntityMouth.Neutral: this.mouth_texture = Assets.Get<Texture2D>("mouth_neutral"); break;
                case EntityMouth.Fear: this.mouth_texture = Assets.Get<Texture2D>("mouth_fear"); break;

            }

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
            base.Draw(batch);

            Vector2 position = this.position;

            if (!this.is_fixed)
            {
                position -= Screen.camera.position;
            }

            batch.Draw(this.texture, position, Color.White);
            batch.Draw(this.eyes_texture, position, Color.White);
            batch.Draw(this.mouth_texture, position, Color.White);
        }

    }
}