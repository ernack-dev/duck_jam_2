using System.Diagnostics;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Duck_Jam_2
{
    public enum UnitTeam
    {
        Player,
        Neutral,
        Opponent,
        Target
    }

    public class Unit : Entity, EventObserver
    {
    
        private UnitTransportLayer transport;
        private UnitSteeringLayer steering;
        private ArrayList orders;
        public ArrayList entities { get; }
        public UnitTeam team { get; set; }

        private static int Counter = 0;

        public Unit(UnitTeam team, Vector2 position, ArrayList entities)
            : base(EntityType.Unit,
                  team == UnitTeam.Player ? "player" : 
                  (team == UnitTeam.Target ? "target" : 
                  (team == UnitTeam.Neutral ? "neutral" :
                  "opponent"
                  )),
              position)
        {
            this.team = team;
            this.entities = entities;
            this.speed = 128.0f;
            this.orders = new ArrayList();
      
            this.transport = new UnitTransportLayer(this);
            this.steering = new UnitSteeringLayer(this);
            this.name = "Unit_" + Unit.Counter.ToString() + "_" + this.team.ToString();
            Unit.Counter++;
        }

        void EventObserver.OnEvent(Event my_event)
        {
            if (my_event.type == EventType.RightMouseButton && this.is_selected)
            {
                this.orders.Clear();

                foreach (Unit unit in this.entities)
                {
                    if (unit.IsAtPosition(GameInputs.camera_mouse_pos()))
                    {
                        AddOrder(new FollowOrder(this, unit));
                        break;
                    }
                }

                if (this.orders.Count == 0)
                {
                    this.orders.Add(new GotoOrder(this, GameInputs.camera_mouse_pos()));
                }
            }
        }
        
        public virtual void AddOrder(UnitOrder order)
        {
            this.orders.Add(order);
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
