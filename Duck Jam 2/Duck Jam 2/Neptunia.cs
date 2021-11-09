using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Duck_Jam_2
{
    public class NeptuniaEvent : EventObservable
    {

    }

    public class Neptunia : Game  
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Scene current_scene;
        private NeptuniaEvent events;
        
        public Neptunia()
        {
            _graphics = new GraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Assets.Init();
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = (int) (1920 * 3/4.0f);
            _graphics.PreferredBackBufferHeight = (int) (1080 * 3/4.0f);
            _graphics.ApplyChanges();

            base.Initialize();
            Screen.device = GraphicsDevice;
            this.events = new NeptuniaEvent();
            this.current_scene = new GameScene(this, this.events);
            this.events.AddObserver(this.current_scene);
        }

        public void ChangeScene(Scene next)
        {
            this.events = new NeptuniaEvent();
            this.current_scene = next;
            this.events.AddObserver(this.current_scene);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ArrayList textures = new ArrayList()
            {
                "player",
                "opponent",
                "target",
                "neutral"
            };

            foreach (string name in textures)
            {
                Assets.Add(name, Content.Load<Texture2D>(name));
            }

            Assets.Add("main_font", Content.Load<SpriteFont>("main_font"));
        }

        private void KeyReleased(Keys key, EventType event_type)
        {
            if (Keyboard.GetState().IsKeyDown(key) && GameInputs.keypress != key)
            {
                this.events.Notify(new Event(event_type));
                GameInputs.keypress = key;
            }

            if (Keyboard.GetState().IsKeyUp(key) && GameInputs.keypress == key)
            {
                GameInputs.keypress = Keys.None;
            }
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            
            GameInputs.mouse_x = Mouse.GetState().X;
            GameInputs.mouse_y = Mouse.GetState().Y;

            KeyReleased(Keys.Space, EventType.SpaceBar);

            if (Mouse.GetState().LeftButton == ButtonState.Pressed && GameInputs.left_mouse_released)
            {
                this.events.Notify(new Event(EventType.LeftMouseButton));
                GameInputs.left_mouse_released = false;
            }
            if (Mouse.GetState().RightButton == ButtonState.Pressed && GameInputs.right_mouse_released)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                {
                    this.events.Notify(new Event(EventType.ShiftRightMouseButton));
                }
                else
                {
                    this.events.Notify(new Event(EventType.RightMouseButton));
                }
               
                GameInputs.right_mouse_released = false;
            }

            if (Mouse.GetState().LeftButton == ButtonState.Released)
            {
                GameInputs.left_mouse_released = true;
            }
            if (Mouse.GetState().RightButton == ButtonState.Released)
            {
                GameInputs.right_mouse_released = true;
            }

            Screen.camera.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            this.current_scene.Update((float) gameTime.ElapsedGameTime.TotalSeconds);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
           
            _spriteBatch.Begin();

             this.current_scene.Display(_spriteBatch);
           
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
