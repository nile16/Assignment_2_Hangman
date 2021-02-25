using System;
using System.Text;

namespace Assignment_2_Hangman
{
    class Program
    {
        static void Main()
        {
            Random ranGen = new Random();
            string[] arrayOfWords = new string[] { "lexicon" };
            string secretWord = arrayOfWords[ranGen.Next(arrayOfWords.Length)];
            StringBuilder wrongLetters = new StringBuilder();
            char[] correctLetters = new char[secretWord.Length];
            int guessesLeft = 10;
            Array.Fill<char>(correctLetters, '_');

            while (true)
            {
                UpdateScreen(correctLetters, wrongLetters, guessesLeft);

                string userInput = Console.ReadLine();

                // User guess a whole word
                if (userInput.Length > 1)
                {
                    if (userInput.Equals(secretWord))
                    {
                        PresentWin();
                        break;
                    }

                    guessesLeft--;

                    if (guessesLeft == 0)
                    {
                        PresentLoss();
                        break;
                    }
                }

                // User guess a single char
                if (userInput.Length == 1)
                {
                    char singleLetterGuess = userInput[0];

                    if (!AlreadyGuessed(singleLetterGuess, correctLetters, wrongLetters))
                    {
                        if (!CheckMatch(singleLetterGuess, secretWord, ref correctLetters))
                        {
                            wrongLetters.Append(singleLetterGuess);
                        }

                        if (CheckWin(correctLetters))
                        {
                            PresentWin();
                            break;
                        }

                        guessesLeft--;

                        if (guessesLeft == 0)
                        {
                            PresentLoss();
                            break;
                        }

                    }
                }
            }

            Console.Write("\n\n\n\n");
        }


        static bool CheckMatch(char singleLetterGuess, string secretWord, ref char[] correctLetters)
        {
            bool match = false;

            for (int i = 0; i < secretWord.Length; i++)
            {
                if (secretWord[i] == singleLetterGuess)
                {
                    correctLetters[i] = secretWord[i];
                    match = true;
                }
            }

            return match;
        }

        static bool CheckWin(char[] correctLetters)
        {
            return !Array.Exists<char>(correctLetters, ele => ele == '_');
        }

        static bool AlreadyGuessed(char userGuess, char[] correctLetters, StringBuilder wrongLetters)
        {
            return wrongLetters.ToString().Contains(userGuess) || Array.Exists<char>(correctLetters, ele => ele == userGuess);
        }

        static void UpdateScreen(char[] correctLetters, StringBuilder wrongLetters, int guessesLeft)
        {
            Console.Clear();

            Console.Write("\n*** HANGMAN ***\n\n");

            foreach (char letter in correctLetters)
            {
                Console.Write(letter + " ");
            }

            Console.Write("\n\n\nWrong: ");

            foreach (char letter in wrongLetters.ToString())
            {
                Console.Write(letter);
            }

            Console.Write($"\nGuesses left: {guessesLeft}\n\nEnter quess: ");
        }

        static void PresentWin()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n");
            Console.WriteLine("Y     Y      OO      U      U         W         W       OO      N       N");
            Console.WriteLine("Y     Y    O    O    U      U         W         W     O    O    N N     N");
            Console.WriteLine(" Y   Y    O      O   U      U         W         W    O      O   N  N    N");
            Console.WriteLine("  Y Y     O      O   U      U         W         W    O      O   N   N   N");
            Console.WriteLine("   Y      O      O   U      U          W   W   W     O      O   N    N  N");
            Console.WriteLine("   Y       O    O     U    U           W  W W  W      O    O    N     N N");
            Console.WriteLine("   Y         OO         UU              WW   WW         OO      N       N");
        }

        static void PresentLoss()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n");
            Console.WriteLine("Y     Y      OO      U      U         L             OO      TTTTTTTT");
            Console.WriteLine("Y     Y    O    O    U      U         L           O    O        T");
            Console.WriteLine(" Y   Y    O      O   U      U         L          O      O       T");
            Console.WriteLine("  Y Y     O      O   U      U         L          O      O       T");
            Console.WriteLine("   Y      O      O   U      U         L          O      O       T");
            Console.WriteLine("   Y       O    O     U    U          L           O    O        T");
            Console.WriteLine("   Y         OO         UU            LLLLLLLL      OO          T");
        }
    }
}

