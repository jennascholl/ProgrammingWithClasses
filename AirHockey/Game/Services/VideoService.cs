using System.Collections.Generic;
using Raylib_cs;
using Unit04.Game.Casting;
using System;
namespace Unit04.Game.Services
{
    /// <summary>
    /// <para>Outputs the game state.</para>
    /// <para>
    /// The responsibility of the class of objects is to draw the game state on the screen. 
    /// </para>
    /// </summary>
    /// 

    public class VideoService
    {
        private int cellSize = 5;
        private string caption = "";
        private int width = 600;
        private int height = 900;
        private int frameRate = 0;
        private bool debug = false;
        private static Casting.Color BLACK = new Casting.Color(0, 0, 0);

        /// <summary>
        /// Constructs a new instance of KeyboardService using the given cell size.
        /// </summary>
        /// <param name="cellSize">The cell size (in pixels).</param>
        public VideoService(string caption, int width, int height, int cellSize, int frameRate, 
                bool debug)
        {
            this.caption = caption;
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.frameRate = frameRate;
            this.debug = debug;
        }

        /// <summary>
        /// Closes the window and releases all resources.
        /// </summary>
        public void CloseWindow()
        {
            Raylib.CloseWindow();
        }

        /// <summary>
        /// Clears the buffer in preparation for the next rendering. This method should be called at
        /// the beginning of the game's output phase.
        /// </summary>
        public void ClearBuffer()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib_cs.Color.WHITE);
        }
        /// <summary>
        /// Draws a rectangular box on the screen at the provided coordinates.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        private void DrawBox(int x, int y, int width, int height, Actor actor)
        {
            Raylib_cs.Color color = ToRaylibColor(actor.GetColor());
            Raylib.DrawRectangle(x, y, width, height, color);         
        }

        /// <summary>
        /// Displays text on the screen at the provided coordinates.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="text"></param>
        /// <param name="darkText"></param>
        private void DrawText(int x, int y, string text, Actor actor)
        {
            Raylib_cs.Color color = ToRaylibColor(BLACK);
            Raylib.DrawText(text, x - 15, y - 15, actor.GetFontSize(), color);
        }
        private void DrawCircle(int x, int y, int r, Actor actor)
        {
            Raylib_cs.Color color = ToRaylibColor(actor.GetColor());
            Raylib.DrawCircle(x, y, r, color);
        }
        /// <summary>
        /// Draws the given actor's text on the screen.
        /// </summary>
        /// <param name="actor">The actor to draw.</param>
        public void DrawActor(Actor actor)
        {
            int x = actor.GetPosition().GetX();
            int y = actor.GetPosition().GetY();
            int width = actor.GetWidth();
            int height = actor.GetHeight();

            if (actor.HasBox())
            {
                DrawBox(x, y, width, height, actor);
            }
            if (actor.HasRadius())
            {
                Puck puck = (Puck)actor;
                int r = puck.radius;
                DrawCircle(x, y, r, actor);
            }
            if (actor.HasText())
            {
                string text = actor.GetText();
                DrawText(x, y, text, actor);
            }

        }
        /// <summary>
        /// Draws the given list of actors on the screen.
        /// </summary>
        /// <param name="actors">The list of actors to draw.</param>
        public void DrawActors(List<Actor> actors)
        {
            foreach (Actor actor in actors)
            {
                DrawActor(actor);              
            }
        }
        
        /// <summary>
        /// Copies the buffer contents to the screen. This method should be called at the end of
        /// the game's output phase.
        /// </summary>
        public void FlushBuffer()
        {
            Raylib.EndDrawing();
        }
        /// <summary>
        /// Gets the grid's cell size.
        /// </summary>
        /// <returns>The cell size.</returns>
        public int GetCellSize()
        {
            return cellSize;
        }

        /// <summary>
        /// Gets the screen's height.
        /// </summary>
        /// <returns>The height (in pixels).</returns>
        public int GetHeight()
        {
            return height;
        }

        /// <summary>
        /// Gets the screen's width.
        /// </summary>
        /// <returns>The width (in pixels).</returns>
        public int GetWidth()
        {
            return width;
        }

        /// <summary>
        /// Whether or not the window is still open.
        /// </summary>
        /// <returns>True if the window is open; false if otherwise.</returns>
        public bool IsWindowOpen()
        {
            return !Raylib.WindowShouldClose();
        }

        /// <summary>
        /// Opens a new window with the provided title.
        /// </summary>
        public void OpenWindow()
        {
            Raylib.InitWindow(width, height, caption);
            Raylib.SetTargetFPS(frameRate);
        }

        /// <summary>
        /// Draws a grid on the screen.
        /// </summary>

        /// <summary>
        /// Converts the given color to it's Raylib equivalent.
        /// </summary>
        /// <param name="color">The color to convert.</param>
        /// <returns>A Raylib color.</returns>
        private Raylib_cs.Color ToRaylibColor(Casting.Color color)
        {
            int r = color.GetRed();
            int g = color.GetGreen();
            int b = color.GetBlue();
            int a = color.GetAlpha();
            return new Raylib_cs.Color(r, g, b, a);
        }

    }
}