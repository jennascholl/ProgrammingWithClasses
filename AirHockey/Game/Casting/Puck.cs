namespace Unit04.Game.Casting
{
    /// <summary>
    /// <para>A precious stone.</para>
    /// <para>
    /// Touching a gem adds a point to the total score.
    /// </para>
    /// </summary>
    public class Puck : Actor
    {
        public bool isActive;
        public int score;
        public int radius = 30;

        /// <summary>
        /// Constructs a new instance of a Puck.
        /// </summary>
        public Puck()
        {
            isActive = false;
            score = 0;
        }
        public override bool HasRadius()
        {
            return true;
        }
        public bool WonGame()
        {
            if (score == 7)
                return true;
            else
                return false;
        }
    }
}