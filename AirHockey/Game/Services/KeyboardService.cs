using Raylib_cs;
using Unit04.Game.Casting;
using System;

namespace Unit04.Game.Services
{
    /// <summary>
    /// <para>Detects player input.</para>
    /// <para>
    /// The responsibility of a KeyboardService is to detect player key presses and translate them 
    /// into a point representing a direction.
    /// </para>
    /// </summary>
    public class KeyboardService
    {
        private int cellSize = 1;
        private int startY = 50;

        /// <summary>
        /// Constructs a new instance of KeyboardService using the given cell size.
        /// </summary>
        /// <param name="cellSize">The cell size (in pixels).</param>
        public KeyboardService(int cellSize)
        {
            this.cellSize = cellSize;
        }
        public Point GetDirection1()
        {
            int dx = 0;
            int dy = 0;

            if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
                dx = -1;

            if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
                dx = 1;

            Point direction = new Point(dx, dy);
            direction = direction.Scale(cellSize);

            return direction;
        }
        public Point GetDirection2()
        {
            int dx = 0;
            int dy = 0;

            if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
                dx = -1;

            if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
                dx = 1;

            Point direction = new Point(dx, dy);
            direction = direction.Scale(cellSize);

            return direction;
        }
        public Point GetStart(Cast cast)
        {
            Puck puck = (Puck)cast.GetFirstActor("puck");
            Actor startMessage = cast.GetFirstActor("startMessage");
            int dx = 0;
            int dy = 0;

            if(Raylib.IsKeyDown(KeyboardKey.KEY_SPACE))
            {
                Random rnd = new Random();
                int startX  = rnd.Next(1, 30);
                dx = startX;
                startY *= -1;
                dy = startY;
                puck.isActive = true;
                cast.RemoveActor("startMessage", startMessage);
            }
            Point velocity = new Point(dx, dy);
            return velocity;
        }
    }
}