using System;
using System.Collections;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Duck_Jam_2
{
    public class GameScene : Widget, EventObserver, Scene
    {
        private ArrayList units;
        private ArrayList entities;
        private Widget body;
        private Footer footer;
        private Widget navbar;

        public GameScene(EventObservable observable): base(new Vector2(0, 0))
        {
            this.units = new ArrayList();
            this.entities = new ArrayList();

            this.body =  AddChild(new Panel(0, 1, 12, 7, Color.Green));
            this.navbar = AddChild(new Panel(0, 0, 12, 1, Color.Blue));
            this.footer = (Footer) AddChild(new Footer());
    
            this.navbar.is_fixed = true;
            this.body.is_fixed = true;
            this.footer.is_fixed = true;

            Screen.camera.SetCenter(new Vector2(32, 32));
            this.entities.Add(new Unit(UnitTeam.Player, new Vector2(0, 32), entities));
            this.entities.Add(new Unit(UnitTeam.Player, new Vector2(32, 32), entities));
            this.entities.Add(new Unit(UnitTeam.Player, new Vector2(64, 32), entities));

            Random rand = new Random();

            // Spawn opponents
            for (int i=0; i<12; i++)
            {
                int x = rand.Next(-1800, 1800);

                int y = rand.Next(-1800, 1800);
                Unit opponent = new Unit(UnitTeam.Opponent, new Vector2(x, y), entities);
                opponent.AddOrder(new WanderOrder(opponent));
                this.entities.Add(opponent);
            }


            // Spawn neutrals
            for (int i = 0; i < 64; i++)
            {
                int x = rand.Next(-1800, 1800);

                int y = rand.Next(-1800, 1800);
                Unit neutral = new Unit(UnitTeam.Neutral, new Vector2(x, y), entities);
                neutral.AddOrder(new WanderOrder(neutral));
                this.entities.Add(neutral);
            }

            // Spawn targets
            for (int i = 0; i < 1; i++)
            {
                int x = rand.Next(-1800, 1800);
                int y = rand.Next(-1800, 1800);
                Unit target = new Unit(UnitTeam.Target, new Vector2(x, y), entities);
                target.AddOrder(new WanderOrder(target));
                this.entities.Add(target);

                // Spawn body guard
                for (int j = 0; j < 2; j++)
                {
                    int x0 = rand.Next(-256, 256);
                    int y0 = rand.Next(-256, 256);

                    Unit opponent = new Unit(UnitTeam.Opponent, new Vector2(x0, y0) + target.Center(), entities);
                    opponent.AddOrder(new FollowOrder(opponent, target));
                    this.entities.Add(opponent);
                }
            }

            foreach (Unit unit in this.entities)
            {
                this.units.Add(unit);
            }

            foreach (Unit unit in this.units)
            {
                observable.AddObserver(unit);
            }

            foreach (Entity entity in this.entities)
            {
                this.body.AddChild(entity);
            }
        }

        void EventObserver.OnEvent(Event my_event)
        {
           if (my_event.type == EventType.LeftMouseButton)
           {
                Entity choosen_one = null;

                foreach (Unit units in this.units)
                {
                    units.is_selected = false;
                    if (choosen_one == null && units.IsAtPosition(GameInputs.camera_mouse_pos()))
                    {
                        if (units.parent == null ||
                            (units.parent.is_fixed && units.parent.IsAtPosition(GameInputs.mouse_pos())) ||
                            (units.parent.is_fixed == false && units.parent.IsAtPosition(GameInputs.camera_mouse_pos())))
                        {
                            if (units.team == UnitTeam.Player)
                            {
                                choosen_one = units;
                            }
                            
                           
                        }
                    }
                   
                }

                if (choosen_one != null)
                {
                    choosen_one.is_selected = true;
                    this.footer.ChangeText(choosen_one.name);
                }
           }
        }

        public override void Update(float dt)
        {

            Vector2 mouse = GameInputs.mouse_pos();
            Vector2 campos = new Vector2();

            if (mouse.X < Screen.scroll_limits.X)
            {
                campos += new Vector2(-1, 0);
            }

            if (mouse.X > Screen.Width() - Screen.scroll_limits.X)
            {
                campos += new Vector2(1, 0);
            }

            if (mouse.Y < Screen.scroll_limits.Y)
            {
                campos += new Vector2(0, -1);
            }

            if (mouse.Y > Screen.Height() - Screen.scroll_limits.Y)
            {
                campos += new Vector2(0, 1);
            }

            if (campos.Length() != 0)
            {
                Screen.camera.Move(campos);
            }
            else
            {
                Screen.camera.Stop();
            }

            base.Update(dt);
        }

        void Scene.Update(float dt)
        {
            Update(dt);
        }

        void Scene.Display(SpriteBatch batch)
        {
            Draw(batch);
        }
    }
}