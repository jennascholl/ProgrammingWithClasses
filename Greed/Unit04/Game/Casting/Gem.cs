namespace Unit04.Game.Casting
{
    /// <summary>
    /// <para>A precious stone.</para>
    /// <para>
    /// Touching a gem adds a point to the total score.
    /// </para>
    /// </summary>
    public class Gem : Actor
    {
        private int prize = 1;

        /// <summary>
        /// Constructs a new instance of an Artifact.
        /// </summary>
        public Gem()
        {
        }
        /// <summary>
        /// Gets the point value of the gem
        /// </summary>
        public int GetPrize()
        {
            return prize;
        }
    }
}