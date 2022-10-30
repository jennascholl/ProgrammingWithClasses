using System;
using System.Numerics;
using Raylib_cs;
using Unit04.Game.Casting;

namespace Unit04.Game.Services
{
    public class CollisionService
    {
        public bool IsPuckCollision(Puck puck, Puck player)
        {
            int puckX = puck.GetPosition().GetX();
            int puckY = puck.GetPosition().GetY();
            Vector2 puckV = new Vector2(puckX, puckY);
            int puckR = puck.radius;
            
            int playerX = player.GetPosition().GetX();
            int playerY = player.GetPosition().GetY();
            Vector2 playerV = new Vector2(playerX, playerY);
            int playerR = player.radius;

            return Raylib.CheckCollisionCircles(puckV, puckR + 3, playerV, playerR + 3);
        }
        public bool IsWallCollision(Puck puck, Actor wall)
        {
            int puckX = puck.GetPosition().GetX();
            int puckY = puck.GetPosition().GetY();
            Vector2 puckV = new Vector2(puckX, puckY);
            int puckR = puck.radius;

            int wallX = wall.GetPosition().GetX();
            int wallY = wall.GetPosition().GetY();
            int wallWidth = wall.GetWidth();
            int wallHeight = wall.GetHeight();

            Raylib_cs.Rectangle rectangle
                = new Raylib_cs.Rectangle(wallX, wallY, wallWidth, wallHeight);

            return Raylib.CheckCollisionCircleRec(puckV, puckR + 5, rectangle);
        }
        public bool IsGoal(Puck puck, int screenHeight)
        {
            if (puck.GetPosition().GetY() >= screenHeight || puck.GetPosition().GetY() <= 0)
                return true;
            return false;
        }
    }
}
