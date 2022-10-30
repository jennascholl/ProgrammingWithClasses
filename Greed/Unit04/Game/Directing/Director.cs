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
        private int score = 0;
        private static int COLS = 60;
        private static int ROWS = 40;
        private static int CELL_SIZE = 20;
        private static int FONT_SIZE = 30;
        private KeyboardService keyboardService = null;
        private VideoService videoService = null;

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
            int counter = 1;
            videoService.OpenWindow();
            while (videoService.IsWindowOpen())
            {
                Random random = new Random();
                int x = random.Next(1, COLS);
                int y = 0;
                Point position = new Point(x, y);
                position = position.Scale(CELL_SIZE);



                if(counter % 2 == 0)
                {
                    Color color = new Color(128, 128, 128);
                    Rock rock = new Rock();
                    rock.SetText("o");
                    rock.SetFontSize(FONT_SIZE);
                    rock.SetColor(color);
                    rock.SetPosition(position);

                    cast.AddActor("rocks", rock);
                }
                else
                {
                    int r = random.Next(0, 256);
                    int g = random.Next(0, 256);
                    int b = random.Next(0, 256);
                    Color color = new Color(r, g, b);
                    Gem gem = new Gem();

                    gem.SetText("*");
                    gem.SetFontSize(FONT_SIZE);
                    gem.SetColor(color);
                    gem.SetPosition(position);

                    cast.AddActor("gems", gem);
                }


                GetInputs(cast);
                DoUpdates(cast);
                DoOutputs(cast);

                counter++;
            }
            videoService.CloseWindow();
        }

        /// <summary>
        /// Gets directional input from the keyboard and applies it to the robot.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void GetInputs(Cast cast)
        {
            Actor robot = cast.GetFirstActor("robot");
            Point velocity = keyboardService.GetDirection();
            robot.SetVelocity(velocity);     
        }

        /// <summary>
        /// Updates the robot's position and determines if it has made contact with any objects.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void DoUpdates(Cast cast)
        {
            Actor banner = cast.GetFirstActor("banner");
            Actor robot = cast.GetFirstActor("robot");
            List<Actor> rocks = cast.GetActors("rocks");
            List<Actor> gems = cast.GetActors("gems");

            int maxX = videoService.GetWidth();
            int maxY = videoService.GetHeight();
            robot.MoveNext(maxX, maxY);

            Point newPoint = new Point(0, 10);

            foreach (Rock rock in rocks)
            {
                Point point = rock.GetPosition();
                point = point.Add(newPoint);
                rock.SetPosition(point);
                if (robot.GetPosition().Equals(rock.GetPosition()))
                {
                    score += rock.GetPrize();
                    cast.GetFirstActor("banner").SetText("SCORE: " + score);
                    cast.RemoveActor("rocks", rock);
                }
            }
            foreach (Gem gem in gems)
            {
                Point point = gem.GetPosition();
                point = point.Add(newPoint);
                gem.SetPosition(point);
                if (robot.GetPosition().Equals(gem.GetPosition()))
                {
                    score += gem.GetPrize();
                    cast.GetFirstActor("banner").SetText("SCORE: " + score);
                    cast.RemoveActor("gems", gem);
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