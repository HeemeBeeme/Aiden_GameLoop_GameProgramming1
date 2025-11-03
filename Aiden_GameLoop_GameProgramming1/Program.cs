using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aiden_GameLoop_GameProgramming1
{
    internal class Program
    {
        static char Character = 'O';
        static char Shark = '^';

        static int FrameTime = 34;

        static (int, int) SharkPosition = (7, 20);
        static (int, int) PlayerPosition = (5, 4);

        static float NewSharkPositionX = SharkPosition.Item2;
        static float NewSharkPositionY = SharkPosition.Item1;
        static float SharkSpeed = 0.4f;

        static bool isDead = false;

        static char[,] Map = { {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',},
                               {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',},
                               {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',},
                               {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',},
                               {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',},
                               {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',},
                               {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',},
                               {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',},
                               {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',},
                               {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',},
                               {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',},
                               {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',},
                               {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',},
        };

        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            if(SharkSpeed > 1)
            {
                SharkSpeed = 1;
            }
            else if(SharkSpeed < 0)
            {
                SharkSpeed *= -1;
            }

                Update();


        }

        static void Update()
        {
            while (!isDead)
            {
                Thread.Sleep(FrameTime);

                ProcessInput();
                Draw();

                float SharkDistanceX = PlayerPosition.Item2 - SharkPosition.Item2;
                float SharkDistanceY = PlayerPosition.Item1 - SharkPosition.Item1;

                float Distance = (float)Math.Sqrt(SharkDistanceX * SharkDistanceX + SharkDistanceY * SharkDistanceY);
                if (Distance > 0)
                {
                    NewSharkPositionX += SharkDistanceX / Distance * SharkSpeed;
                    NewSharkPositionY += SharkDistanceY / Distance * SharkSpeed;

                    float NewSharkX = 0;
                    float NewSharkY = 0;

                    NewSharkX = (float)Math.Round(NewSharkPositionX);
                    NewSharkY = (float)Math.Round(NewSharkPositionY);

                    SharkPosition = ((int)NewSharkY, (int)NewSharkX);

                    if (Math.Abs(PlayerPosition.Item2 - SharkPosition.Item2) < 1 && Math.Abs(PlayerPosition.Item1 - SharkPosition.Item1) < 1)
                    {
                        isDead = true;

                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You Have Died!");
                    }
                }
            }
        }

        static void Draw()
        {
            Console.SetCursorPosition(0, 0);

            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;

                    if (j == PlayerPosition.Item2 && i == PlayerPosition.Item1)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(Character);
                    }
                    else if (j == SharkPosition.Item2 && i == SharkPosition.Item1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(Shark);
                    }
                    else
                    {
                        Console.Write(Map[i, j]);
                    }
                }

                Console.WriteLine();

            }
            
        }

        static void ProcessInput()
        {
            while(Console.KeyAvailable)
            {

            }
        }
    }
}
