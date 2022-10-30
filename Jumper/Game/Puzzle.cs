using System;
using System.Collections.Generic;

namespace Jumper.Game
{
    public class Puzzle
    {
        private List<string> _words = new List<string>{};
        private List<char> encryption = new List<char>();

        public Puzzle()
        {
            this._words.Add("dessert");
            this._words.Add("pizza");
            this._words.Add("mountain");
            this._words.Add("river");
            this._words.Add("campus");
        }
        public string GetWord()
        {
            Random random = new Random();
            int randomNumber = random.Next(this._words.Count);
            return this._words[randomNumber];
        }
        public void EncryptWord(string word)
        {
            for (int i = 0; i < word.Length; i++)
            {
                encryption.Add('_');
            }         
        }
        public List<char> GetEncryption()
        {
            return encryption;
        }
        public bool UpdateEncryption(char letter, string word)
        {
            bool updated = false;
            Console.WriteLine();
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == letter)
                {
                    encryption[i] = letter;
                    updated = true;
                }
            }
            return updated;
        }
    }
}