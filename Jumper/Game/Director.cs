using System;

namespace Jumper.Game
{
    /// <summary>
    /// 
    /// </summary>
    public class Director
    {
        private string word;
        /// <summary>
        /// 
        /// </summary>
        public void StartGame()
        {
            Puzzle puzzle = new Puzzle();
            Output output = new Output();
            Game game = new Game();
            Parachute parachute = new Parachute();

            word = puzzle.GetWord();
            puzzle.EncryptWord(word);

            while (!game.IsWon(puzzle.GetEncryption()) && !game.IsLost(parachute.StillExists()))
            {
                output.DisplayEncryption(puzzle.GetEncryption());
                output.DisplayParachute(parachute.GetParachute());
                char letter = output.PromptLetter();
                if (!puzzle.UpdateEncryption(letter, word))
                    parachute.LoseLine();
            }
            if (game.IsWon(puzzle.GetEncryption()))
                output.DisplayWonMessage(puzzle.GetEncryption());
            else
                 output.DisplayLostMessage(word);
        }
    }
}