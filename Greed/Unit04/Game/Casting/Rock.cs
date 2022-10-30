namespace Unit04.Game.Casting
{
    /// <summary>
    /// <para>A non-precious stone.</para>
    /// <para>
    /// Touching a Rock takes a point away from the total score.
    /// </para>
    /// </summary>
    public class Rock : Actor
    {
        private int prize = -1;

        /// <summary>
        /// Constructs a new instance of an Artifact.
        /// </summary>
        public Rock()
        {
        }

        /// <summary>
        /// Gets the point value of the rock
        /// </summary>
        public int GetPrize()
        {
            return prize;
        }

    }
}