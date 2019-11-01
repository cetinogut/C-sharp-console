using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace Console005_MoveTheStar
{
    class Program
    {
        //Task: Beginning from initial start position, move the star acoording to user inputs
        // 1-LEFT, 2-UP, 3-RIGHT, 4-DOWN, 0-EXIT
        public static string[,] startView = {
            { "-", "-", "-", "-", "-" },
            { "-", "-", "-", "-", "-" },
            { "-", "-", "x", "-", "-" },
            { "-", "-", "-", "-", "-" },
            { "-", "-", "-", "-", "-" } };
        public static int initialX = 2, initialY = 2;
        public static int previousX = 0, previousY = 0;
        public static int currentX = 2, currentY = 2;
        public static int targetX = 0, targetY = 0;
        public static int userInputDirection, messageIndex;
        public static bool isExit = false;
        public static bool isVoiceOff = false;
        public static SoundPlayer tellUserChoice = new SoundPlayer();
        

        static void Main(string[] args)
        {
            PlayBackGroundMusic(); // play background music : default is izmir marsi..
                       
            PrintxPad();// print the initial view
                do
                {
                string userIn = GetTheUserInput();
                                        
                    if (ValidateUserInput(userIn)) // if it is a valid input (1,2,3,4) then do the move
                    {
                        CalculateTheMove(userInputDirection);
                        PrintxPad();// print the new view after the user input
                    }
                    else
                    {
                       PrintWarningMessage(messageIndex); // if the user have
                    }
                } while (!isExit); // unless the user choses exitting stay in the while loop for new entry

            Console.WriteLine();
            Console.WriteLine("Hit any key to exit the program");
            
            tellUserChoice.Dispose();
            Console.ReadKey();

        }// end main

        private static void PlayBackGroundMusic()
        {
            SoundPlayer backMusic = new SoundPlayer();
            backMusic.SoundLocation = @"E:\test\Sounds\despasito.wav";
            backMusic.Play();
        }

        private static void PrintWarningMessage(int messageIn)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed; // all wrong messages goes in red background.
            switch (messageIn)
            {
                case 1:     // no entry
                    Console.WriteLine("No Entry.. Please try again...");
                    break;

                case 2: // wrong numeric value other than 0,1,2,3,4
                    Console.WriteLine("Incorrect Numeric Entry.. Please try again...");
                    break;

                case 3: // string character entry...
                    Console.WriteLine("Incorrect  String Entry.. Please try again...");
                    break;

                case 4: // string character entry...
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("GOOD BYE!!!");
                    Console.ResetColor();
                    VoiceTheChoice(5);
                    break;
            }// end of switch
            Console.ResetColor(); // after showing the warning message to the user return back to the default console color..
        }// end method prinf warning

        private static string GetTheUserInput()
        {
            return (Console.ReadLine().Trim()); // get first entry from user.. trim it first..
        }

        private static bool ValidateUserInput(string userInput)
        {
            if (userInput.Length == 0)
            {
                messageIndex = 1;
                return false;
            }

            if (int.TryParse(userInput, out userInputDirection))
            {
                if (userInputDirection == 0) // if user hits 0 then it means exit t he program..
                {
                    messageIndex = 4; // show related message
                    isExit = true; // exit the do .. while loop
                    return false;
                }
                if (userInputDirection >= 1 && userInputDirection < 5)
                {
                    return true; // this means a valid movement enrty.. readt to do the move..
                }
                else
                {
                    messageIndex = 2; // this else means the user had entered a numeric value other than (0,1,2,3,4) which is invalid for the menu items..
                    return false;
                }
            }
            else
            {
                messageIndex = 3; // this else means the user had entered a non-numeric string  value which is invalid.
                return false;
            }
            
    }// end GetValidateUserInput

    private static void CalculateTheMove(int userInputDirect)
    {
            previousX = currentX;
            previousY = currentY;

            switch (userInputDirect)
            {
                case 1:
                    Console.WriteLine("you hit 1!! then x goes to the left...");
                    targetX = currentX; // x no change
                    currentY--; // y decreases by one
                    if (currentY == -1)
                    {
                        currentY = 4;
                    }
                    targetY = currentY;
                    break;

                case 2:
                    Console.WriteLine("you hit 2!! then x goes to the up...");
                    currentX--; // x decreases by one
                    if (currentX == -1)
                    {
                        currentX = 4;
                    }
                    targetX = currentX;
                    targetY = currentY; // y no change
                    break;

                case 3:
                    Console.WriteLine("you hit 3!! then x goes to the right...");
                    targetX = currentX;
                    currentY++; // y increases by one
                    if (currentY == 5)
                    {
                        currentY = 0;
                    }
                    targetY = currentY;
                    break;

                case 4:
                    Console.WriteLine("you hit 4!! then x goes to the bottom..");
                    
                    currentX++; // x increases by one
                    if (currentX == 5)
                    {
                        currentX = 0;
                    }
                    targetX = currentX;
                    targetY = currentY; // y no change
                    break;

            }// end switch

            startView[targetX, targetY] = "x";
            startView[previousX, previousY] = "-";
            VoiceTheChoice(userInputDirect);
        }// end method dothe move

        private static void VoiceTheChoice(int voiceIndex)
        {
            string addFileName ="";
            switch (voiceIndex)
            {
                case 1:
                    addFileName = "left";
                    break;
                case 2:
                    addFileName = "up";
                    break;
                case 3:
                    addFileName = "right";
                    break;
                case 4:
                    addFileName = "down";
                    break;
                case 5:
                    addFileName = "bye";
                    break;
            }// end switch
            string soundPath = "E:\\test\\Sounds\\going-"+addFileName+".wav";
            tellUserChoice.SoundLocation = soundPath;
            tellUserChoice.Play();
        }

        private static void PrintxPad()
    {
        Console.Clear();// clear before printing

            Console.Write("                      "); // get the initial view at the screen center.
            Console.WriteLine("-----------");
            //Console.WriteLine("___________");
        for (int i = 0; i < 5; i++)
        {
            Console.Write("                      |"); // get the initial view at the screen center.
            for (int j = 0; j < 5; j++)
            {
                    if (startView[i,j] =="x") // print "x" with different bckground color.
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.Write(startView[i, j]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(startView[i, j]); //initial view
                    }
                    Console.Write("|");
                }
                //Console.Write("|");
                Console.WriteLine();
        }
            Console.Write("                      ");
            Console.WriteLine("-----------");

            Console.WriteLine();
        Console.WriteLine("****************************************************");
        Console.WriteLine("******************  MOVE THE STAR  *****************");
        Console.WriteLine("****************************************************");
        Console.WriteLine("** 1-LEFT ** 2-UP ** 3-RIGHT ** 4-DOWN ** 0-EXIT ***");
        Console.WriteLine("****************************************************");
        Console.WriteLine("--Press the related numeric key and hit the enter---");
    }// end method PrintStartView

}//end class

} // end of namespace
