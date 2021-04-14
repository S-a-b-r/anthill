using System;
using System.Collections.Generic;
using System.Linq;

namespace anthill
{
    class Ant : Movier
    {
        static Random rnd = new Random();
        const int EYE_RANGE = 10;
        const int SPAWN_HP = 20;
        double targetx = rnd.Next(-20,20);
        double targety = rnd.Next(-20,20);

        public override void Draw()
        {
            ConsoleEx.Print((int)x, (int)y, "#", ConsoleColor.Yellow);
        }
        public override void Touch(Movier other)
        {
            if (other != null && other is Food)
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
        }
        public override (double dx, double dy) GetNextMove(IEnumerable<Movier> others)
        {
            var target = Nearest(others, obj => obj is Queen && Dist(obj)<EYE_RANGE && hp>5);
            if (target == null)
                target = Nearest(others, obj => obj is Food && Dist(obj) < EYE_RANGE);
            if (target == null)
                target = new Movier() {x = this.x + targetx, y = this.y + targety};
            return Shift(target, speed);
        }

    }
}