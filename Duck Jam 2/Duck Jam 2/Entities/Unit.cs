using System.Diagnostics;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Duck_Jam_2
{
    public class Unit : Entity, EventObserver
    {
    
        private UnitTransportLayer transport;
        private UnitSteeringLayer steering;
        private ArrayList orders;
        public ArrayList entities { get; }

        private static int Counter = 0;

        public Unit(Vector2 position, ArrayList entities): base(EntityType.Unit, "unit", position)
        {
            this.entities = entities;
            this.speed = 128.0f;
            this.orders = new ArrayList();
      
            this.transport = new UnitTransportLayer(this);
            this.steering = new UnitSteeringLayer(this);
            this.name = "Unit_" + Unit.Counter.ToString();
            Unit.Counter++;
        }



        void EventObserver.OnEvent(Event my_event)
        {
            if (my_event.type == EventType.RightMouseButton && this.is_selected)
            {
                this.orders.Clear();
                this.orders.Add(new GotoOrder(this, GameInputs.camera_mouse_pos()));
            }
        }
        
        public override void Update(float dt)
        {
            if (this.orders.Count == 0)
            {
                this.orders.Add(new IdleOrder(this));
            }

            if (this.orders.Count > 0)
            {
                UnitOrder order = (UnitOrder)this.orders[0];
                order.Update(dt);

                if (order.IsDone())
                {
                    this.orders.RemoveAt(0);
                }
            }
        }

    }
}
