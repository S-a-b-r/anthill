using System;
using System.Collections.Generic;
using System.Linq;

namespace anthill
{
    class Queen : Movier
    {
        static Random rnd = new Random();
        const int EYE_RANGE = 40;
        const int SPAWN_HP = 40;
        double targetx = rnd.Next(-20,20);
        double targety = rnd.Next(-20,20);

        public override void Draw()
        {
            ConsoleEx.Print((int)x, (int)y, "#~~#", ConsoleColor.Red);
        }
        public override void Touch(Movier other)
        {
            if (other != null && other is Ant && other.hp >= 5)
                DoDamage(other);
        }
        public override void Move(double dx, double dy, Collision c)
        {
            x += dx;
            y += dy;
            targetx -= dx;
            targety -= dy;
            if (Math.Abs(targetx) <= 0.00001) targetx = rnd.Next(-20,20);
            if (Math.Abs(targety) <= 0.00001) targety = rnd.Next(-10,10);
            if (c == Collision.Vertical) targetx = -targetx;
            if (c == Collision.Horizontal) targety = -targety;

            if (hp > SPAWN_HP && OnSpawn != null)
                OnSpawn(this);
        }
        public override (double dx, double dy) GetNextMove(IEnumerable<Movier> others)
        {
            var target = new Movier() {x = this.x + targetx, y = this.y + targety};
            return Shift(target, speed);
        }

        public override IEnumerable<Movier> CreateChild()
        {
            this.hp /= 2;

            var child = new Ant()
            {
                x = this.x,
                y = this.y,
                size = this.size,
                speed = 1,
                hp = this.hp,
                damage = this.damage,
                OnSpawn = this.OnSpawn
            };

            return new Ant[] { child };
        }
    }
}