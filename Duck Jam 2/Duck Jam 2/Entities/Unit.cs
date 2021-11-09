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
        public float hp { get; set; }
        public float atk { get; set; }
        public float hp_max { get; set; }
        public bool is_dead { get; set; }
        public bool is_attacking { get; private set; }
        private ProgressBar hp_progress;
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
            this.atk = 16.0f;
            this.team = team;
            this.entities = entities;
            this.speed = 128.0f;
            this.orders = new ArrayList();
            this.hp_max = 100.0f;
            this.hp = hp_max;
            this.transport = new UnitTransportLayer(this);
            this.steering = new UnitSteeringLayer(this);
            this.name = "Unit_" + Unit.Counter.ToString() + "_" + this.team.ToString();
            
            Unit.Counter++;

            float hp_height = 8.0f;
            this.hp_progress = new ProgressBar(
                new Vector2(0, hp_height), 
                new Vector2(this.size.X, hp_height)
             );

            this.hp_progress.is_fixed = false;
            this.is_dead = false;

            AddChild(this.hp_progress);
        }

        public void hurt(Unit origin, float damages)
        {
            if (this.is_dead) { return; }

            this.hp -= damages;
            
            if (this.hp <= 0)
            {
                this.is_dead = true;
                this.hp_progress.is_visible = false;
                this.hp = 0;
            }

            if (
                this.orders[0].GetType() != typeof(AttackOrder) &&
                this.team != UnitTeam.Neutral
               )
            {
                this.orders.Insert(0, (new AttackOrder(this, origin)));
            }
            else if (
                this.orders[0].GetType() != typeof(PanicOrder) &&
                this.team == UnitTeam.Neutral
           )
            {
                this.orders.Insert(0, (new PanicOrder(this, origin)));
            }
        }

        void EventObserver.OnEvent(Event my_event)
        {
            if (this.is_dead) { return; }

            if (my_event.type == EventType.ShiftRightMouseButton && this.is_selected)
            {
                this.orders.Clear();

                foreach (Unit unit in this.entities)
                {
                    if (unit.IsAtPosition(GameInputs.camera_mouse_pos()))
                    {
                        AddOrder(new AttackOrder(this, unit));
                        break;
                    }
                }
            }

            else if (my_event.type == EventType.RightMouseButton && this.is_selected)
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
            if (this.is_dead) { return; }

            this.orders.Add(order);
        }

        public virtual void AddPrioOrder(UnitOrder order)
        {
            if (this.is_dead) { return; }

            this.orders.Insert(0, order);
        }

        public override void Update(float dt)
        {
            this.is_attacking = false;

            if (this.is_dead) { this.orders.Clear();  return; }

            base.Update(dt);
            
            this.hp_progress.SetValue(this.hp / this.hp_max);


            if (this.orders.Count == 0)
            {
                this.orders.Add(new IdleOrder(this));
            }

            if (this.orders.Count > 0)
            {
                UnitOrder order = (UnitOrder)this.orders[0];
                this.is_attacking = (order.GetType() == typeof(AttackOrder));
               
                order.Update(dt);

                if (order.IsDone())
                {
                    this.orders.RemoveAt(0);
                }
            }
        }

    }
}
