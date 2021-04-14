using System.Collections.Generic;
using System;

namespace anthill
{
    class Food : Movier
    {
        double targetx = rnd.Next(-5, 5);
        double targety = rnd.Next(-5, 5);
        const int EYE_RANGE = 5;

        static Random rnd = new Random();

        public override void Draw()
        {
            ConsoleEx.Print((int)x, (int)y, ".");
        }

        public override (double dx, double dy) GetNextMove(IEnumerable<Movier> others)
        {
            var target = Nearest(others, obj => obj is Food && Dist(obj) < EYE_RANGE && Dist(obj) > 0);
            if (target == null)
                target = new Movier() {x = this.x + targetx, y = this.y + targety};
            return Shift(target, -speed);
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
    }
}