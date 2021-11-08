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
           

            this.entities.Add(new Unit(new Vector2(32, 32), entities));
            this.entities.Add(new Unit(new Vector2(320, 32), entities));
            this.entities.Add(new Unit(new Vector2(32, 320), entities));

            foreach (Unit unit in this.entities)
            {
                this.units.Add(unit);
            }

            this.entities.Add(new HomeBuilding(new Vector2(64, 64)));
            this.entities.Add(new Ore(new Vector2(128, 128)));
            this.entities.Add(new Food(new Vector2(256, 400)));

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
         
                foreach (Entity entity in this.entities)
                {
                    entity.is_selected = false;
                    if (choosen_one == null && entity.IsAtPosition(GameInputs.camera_mouse_pos()))
                    {
                        if (entity.parent == null ||
                            (entity.parent.is_fixed && entity.parent.IsAtPosition(GameInputs.mouse_pos())) ||
                            (entity.parent.is_fixed == false && entity.parent.IsAtPosition(GameInputs.camera_mouse_pos())))
                        {
                            choosen_one = entity;
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