using System.Collections.Generic;
using System;
using Unit04.Game.Casting;
using Unit04.Game.Services;


namespace Unit04.Game.Directing
{
    /// <summary>
    /// <para>A person who directs the game.</para>
    /// <para>
    /// The responsibility of a Director is to control the sequence of play.
    /// </para>
    /// </summary>
    public class Director
    {
        private KeyboardService keyboardService = null;
        private VideoService videoService = null;
        private static Color BLACK = new Color(0, 0, 0);

        /// <summary>
        /// Constructs a new instance of Director using the given KeyboardService and VideoService.
        /// </summary>
        /// <param name="keyboardService">The given KeyboardService.</param>
        /// <param name="videoService">The given VideoService.</param>
        public Director(KeyboardService keyboardService, VideoService videoService)
        {
            this.keyboardService = keyboardService;
            this.videoService = videoService;
        }

        /// <summary>
        /// Starts the game by running the main game loop, spawning new cast indefinitely.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void StartGame(Cast cast)
        {
            videoService.OpenWindow();
            while(videoService.IsWindowOpen()) 
            {
                GetInputs(cast);
                DoUpdates(cast);
                DoOutputs(cast);
            }
            videoService.CloseWindow();
        }

        /// <summary>
        /// Gets directional input from the keyboard and applies it to the robot.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void GetInputs(Cast cast)
        {
            Actor player1 = cast.GetFirstActor("player1");
            Point velocity1 = keyboardService.GetDirection1();
            player1.SetVelocity(velocity1);   

            Actor player2 = cast.GetFirstActor("player2");
            Point velocity2 = keyboardService.GetDirection2();
            player2.SetVelocity(velocity2);

            Puck puck = (Puck)cast.GetFirstActor("puck");
            if(!puck.isActive) 
            {
                Point puckVelocity = keyboardService.GetStart(cast);
                puck.SetVelocity(puckVelocity);
            }
        }

        /// <summary>
        /// Updates the robot's position and determines if it has made contact with any objects.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void DoUpdates(Cast cast)
        {
            Puck player1 = (Puck)cast.GetFirstActor("player1");
            Puck player2 = (Puck)cast.GetFirstActor("player2");
            Actor leftWall = cast.GetFirstActor("leftWall");
            Actor rightWall = cast.GetFirstActor("rightWall");
            List<Actor> walls = cast.GetActors("walls");
            Puck puck = (Puck)cast.GetFirstActor("puck");
            Actor score1 = cast.GetFirstActor("score1");
            Actor score2 = cast.GetFirstActor("score2");

            int maxX = videoService.GetWidth();
            int maxY = videoService.GetHeight();

            CollisionService collisionService = new CollisionService();
            Point collisionResetLeft = new Point(10, 0);
            Point collisionResetRight = new Point(-10, 0);

            if(collisionService.IsWallCollision(player1, leftWall))
                player1.SetVelocity(collisionResetLeft);
            else if(collisionService.IsWallCollision(player1, rightWall))
                player1.SetVelocity(collisionResetRight);

            if(collisionService.IsWallCollision(player2, leftWall))
                player2.SetVelocity(collisionResetLeft);
            else if(collisionService.IsWallCollision(player2, rightWall))
                player2.SetVelocity(collisionResetRight);
            
            if(collisionService.IsWallCollision(puck, leftWall) 
               || collisionService.IsWallCollision(puck, rightWall))
            {
                int x = puck.GetVelocity().GetX();
                int y = puck.GetVelocity().GetY();
                Point velocity = new Point(-x, y);
                puck.SetVelocity(velocity);
            }
            if(collisionService.IsPuckCollision(puck, player1) 
               || collisionService.IsPuckCollision(puck, player2))
            {
                int x = puck.GetVelocity().GetX();
                int y = puck.GetVelocity().GetY();
                int r = puck.radius;

                Point horCollisionPointL = new Point(x - r, y);
                Point horCollisionPointR = new Point(x + r, y);

                if ((horCollisionPointL.GetX() < player1.GetPosition().GetX() + r
                    && horCollisionPointL.GetX() > player1.GetPosition().GetX() - r
                    && horCollisionPointL.GetY() > player1.GetPosition().GetY() - r
                    && horCollisionPointL.GetY() < player1.GetPosition().GetY() + r) 
                    || (horCollisionPointR.GetX() > player1.GetPosition().GetX() - r
                    && horCollisionPointR.GetX() < player1.GetPosition().GetX() + r
                    && horCollisionPointR.GetY() > player1.GetPosition().GetY() - r
                    && horCollisionPointR.GetY() < player1.GetPosition().GetY() + r)
                    || (horCollisionPointL.GetX() < player2.GetPosition().GetX() + r
                    && horCollisionPointL.GetX() > player2.GetPosition().GetX() - r
                    && horCollisionPointL.GetY() > player2.GetPosition().GetY() - r
                    && horCollisionPointL.GetY() < player2.GetPosition().GetY() + r) 
                    || (horCollisionPointR.GetX() > player2.GetPosition().GetX() - r
                    && horCollisionPointR.GetX() < player2.GetPosition().GetX() + r
                    && horCollisionPointR.GetY() > player2.GetPosition().GetY() - r
                    && horCollisionPointR.GetY() < player2.GetPosition().GetY() + r))
                {
                    Point velocity = new Point(-x, y);
                    puck.SetVelocity(velocity);
                }
                else
                {
                    Point velocity = new Point(x, -y);
                    puck.SetVelocity(velocity);
                }
            }
            foreach (Actor wall in walls) 
            {
                if (collisionService.IsWallCollision(puck, wall))
                {
                    int x = puck.GetVelocity().GetX();
                    int y = puck.GetVelocity().GetY();
                    Point velocity = new Point(x, -y);
                    puck.SetVelocity(velocity);
                }
            }

            player1.MoveNext(maxX, maxY);
            player2.MoveNext(maxX, maxY);
            puck.MoveNext(maxX, maxY);

            if(collisionService.IsGoal(puck, maxY))
            {
                if(puck.GetPosition().GetY() - puck.GetVelocity().GetY() < 0)
                    player1.score++;
                else
                    player2.score++;

                Point startPoint = new Point(maxX / 2, maxY / 2);
                puck.SetPosition(startPoint);
                puck.isActive = false;

                score1.SetText(player1.score.ToString());
                score2.SetText(player2.score.ToString());
            }

            if(player1.WonGame() || player2.WonGame())
            {
                cast.RemoveActor("puck", puck);
                Actor endMessage = new Actor();
                cast.AddActor("endMessage", endMessage);
                endMessage.SetColor(BLACK);
                Point endMessagePoint = new Point(200, maxY / 2 - 80);
                endMessage.SetPosition(endMessagePoint);
                endMessage.SetFontSize(30);
                if(player1.WonGame())
                    endMessage.SetText("Player 1 Wins!");
                else
                    endMessage.SetText("Player 2 Wins!");
                while(videoService.IsWindowOpen())
                {
                    DoOutputs(cast);
                }
            }
        }

        /// <summary>
        /// Draws the actors on the screen.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void DoOutputs(Cast cast)
        {

            List<Actor> actors = cast.GetAllActors();
            videoService.ClearBuffer();
            videoService.DrawActors(actors);
            videoService.FlushBuffer();
        }
    }
}