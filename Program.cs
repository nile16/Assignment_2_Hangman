using System;
using System.Text;

namespace Assignment_2_Hangman
{
    class Program
    {
        static void Main()
        {
            Random ranGen = new Random();
            string[] arrayOfWords = new string[] { "LEXICON" };
            string secretWord = arrayOfWords[ranGen.Next(arrayOfWords.Length)];
            StringBuilder wrongLetters = new StringBuilder();
            char[] correctLetters = new char[secretWord.Length];
            Array.Fill<char>(correctLetters, '_');
            int guessesLeft = 10;

            while (true)
            {
                UpdateScreen(correctLetters, wrongLetters, guessesLeft);

                Console.Write("\nEnter your quess: ");
                string userGuess = Console.ReadLine().ToUpper();

                // The player guess a whole word
                if (userGuess.Length > 1)
                {
                    guessesLeft--;

                    if (userGuess.Equals(secretWord))
                    {
                        FillInCorrectLetter(secretWord, ref correctLetters);
                        UpdateScreen(correctLetters, wrongLetters, guessesLeft);
                        PresentWin();
                        break;
                    }

                    if (guessesLeft == 0)
                    {
                        UpdateScreen(correctLetters, wrongLetters, guessesLeft);
                        PresentLoss();
                        break;
                    }
                }

                // The player guess a single letter
                if (userGuess.Length == 1)
                {
                    if (IsGuessNovel(userGuess[0], correctLetters, wrongLetters))
                    {
                        guessesLeft--;

                        if (IsGuessCorrect(userGuess[0], secretWord))
                        {
                            FillInCorrectLetter(userGuess[0], secretWord, ref correctLetters);
                        }
                        else
                        {
                            wrongLetters.Append(userGuess[0]);
                        }

                        if (IsWholeWordFound(correctLetters))
                        {
                            UpdateScreen(correctLetters, wrongLetters, guessesLeft);
                            PresentWin();
                            break;
                        }

                        if (guessesLeft == 0)
                        {
                            UpdateScreen(correctLetters, wrongLetters, guessesLeft);
                            PresentLoss();
                            break;
                        }
                    }
                }
            }
            Console.Write("\n\n\n\n");
        }

        static bool IsGuessCorrect(char guess, string secretWord)
        {
            bool match = false;

            foreach (char letter in secretWord)
            {
                if (letter == guess)
                {
                    match = true;
                }
            }
            return match;
        }
        static void FillInCorrectLetter(char guess, string secretWord, ref char[] correctLetters)
        {
            for (int i = 0; i < secretWord.Length; i++)
            {
                if (secretWord[i] == guess)
                {
                    correctLetters[i] = secretWord[i];
                }
            }
        }

        static void FillInCorrectLetter(string secretWord, ref char[] correctLetters)
        {
            for (int i = 0; i < secretWord.Length; i++)
            {
                correctLetters[i] = secretWord[i];
            }
        }

        static bool IsWholeWordFound(char[] correctLetters)
        {
            return !Array.Exists<char>(correctLetters, ele => ele == '_');
        }

        static bool IsGuessNovel(char userGuess, char[] correctLetters, StringBuilder wrongLetters)
        {
            return !wrongLetters.ToString().Contains(userGuess) && !Array.Exists<char>(correctLetters, ele => ele == userGuess);
        }

        static void UpdateScreen(char[] correctLetters, StringBuilder wrongLetters, int guessesLeft)
        {
            Console.Clear();

            Console.Write("\n*** HANGMAN ***\n\n");

            foreach (char letter in correctLetters)
            {
                Console.Write(letter + " ");
            }

            Console.Write("\n\nWrong guesses: ");

            foreach (char letter in wrongLetters.ToString())
            {
                Console.Write(letter);
            }

            Console.Write($"\nGuesses left: {guessesLeft}\n");
        }

        static void PresentWin()
        {
            Console.WriteLine("\n\n");
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
            Console.WriteLine("\n\n");
            Console.WriteLine("Y     Y      OO      U      U         L             OO          SSSS    TTTTTTTT");
            Console.WriteLine("Y     Y    O    O    U      U         L           O    O      S             T");
            Console.WriteLine(" Y   Y    O      O   U      U         L          O      O     S             T");
            Console.WriteLine("  Y Y     O      O   U      U         L          O      O       SSS         T");
            Console.WriteLine("   Y      O      O   U      U         L          O      O           S       T");
            Console.WriteLine("   Y       O    O     U    U          L           O    O            S       T");
            Console.WriteLine("   Y         OO         UU            LLLLLLLL      OO         SSSS         T");
        }
    }
}

