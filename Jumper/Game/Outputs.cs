using System;
using System.Collections.Generic;

namespace Jumper.Game
{
    public class Output
    {
        public void DisplayParachute(List<string> parachute)
        {
            for (int i = 0; i < parachute.Count; i++)
            {
                Console.WriteLine(parachute[i]);
            }
            Console.WriteLine("   0");
            Console.WriteLine(@"  /|\");
            Console.WriteLine(@"  / \");
            Console.WriteLine("");
            Console.WriteLine("^^^^^^^");
        }
        public void DisplayEncryption(List<char> encryption)
        {
            for (int i = 0; i < encryption.Count; i++)
            {
                Console.Write(encryption[i] + " ");
            }
            Console.WriteLine("\n");
        }
                public char PromptLetter()
        {
            char letter = 'a';
            do
            {
                Console.Write("Guess a letter [a-z]: ");
                try
                {
                    letter = char.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Input one letter.");
                }
                if(!Char.IsLetter(letter))
                    Console.WriteLine("Not a letter.");
            } while (!Char.IsLetter(letter));

            return letter;
        }
        public void DisplayWonMessage(List<char> encryption)
        {
            this.DisplayEncryption(encryption);
            Console.WriteLine("Good job, you won!");

        }
        public void DisplayLostMessage(String word)
        {
            Console.WriteLine("   X");
            Console.WriteLine(@"  /|\");
            Console.WriteLine(@"  / \");
            Console.WriteLine("");
            Console.WriteLine("^^^^^^^");
            Console.WriteLine("Better luck next time! The word was " + word);

        }
    }
}
