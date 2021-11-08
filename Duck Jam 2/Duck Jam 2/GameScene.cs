using System;
using System.Collections;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Duck_Jam_2
{
    public class GameScene : EventObserver, Scene
    {
        private ArrayList units;
        private ArrayList entities;

        public GameScene(EventObservable observable)
        {
            this.units = new ArrayList();
            this.entities = new ArrayList();

            this.entities.Add(new Unit(new Vector2(32, 32), entities));
            this.entities.Add(new Unit(new Vector2(320, 320), entities));
            this.entities.Add(new Unit(new Vector2(32, 320), entities));
            this.entities.Add(new Unit(new Vector2(320, 32), entities));
            this.entities.Add(new Unit(new Vector2(320, 150), entities));

            foreach (Unit unit in this.entities)
            {
                this.units.Add(unit);
            }

            this.entities.Add(new Ore(new Vector2(128, 128)));
            this.entities.Add(new Food(new Vector2(256, 400)));

            foreach (Unit unit in this.units)
            {
                observable.AddObserver(unit);
            }
        }

        void EventObserver.OnEvent(Event my_event)
        {
            if (my_event.type == EventType.LeftMouseButton)
            {
                bool found_one = false;

                foreach(Unit unit in this.units)
                {
                    if (unit.IsAtPosition(GameInputs.mouse_pos()) && found_one == false)
                    {
                        unit.is_selected = true;
                        found_one = true;
                    }
                    else 
                    {
                        unit.is_selected = false;
                    }
                }
            }
        }

        void Scene.Update(float dt)
        {
            foreach (Entity entity in this.entities)
            {
                entity.Update(dt);
            }
        }

        void Scene.Display(SpriteBatch batch)
        {
            foreach (Entity entity in this.entities)
            {
                entity.Display(batch);
            }
        }
    }
}