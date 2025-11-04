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
        static float SharkSpeed = 0.2f;

        static bool isDead = false;
        static bool isOnIsland = false;

        static char[,] Map = { {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',},
                               {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',},
                               {' ', ' ', '-', '-', '-', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',},
                               {' ', '-', '-', '-', '-', '-', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',},
                               {' ', '-', '-', '-', '-', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',},
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

                if(!isOnIsland)
                {
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
        }

        static void Draw()
        {
            Console.SetCursorPosition(0, 0);

            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {

                    if (j == PlayerPosition.Item2 && i == PlayerPosition.Item1)
                    {
                        if (Map[i, j] == '-')
                        {
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            isOnIsland = true;
                        }
                        else
                        {
                            isOnIsland = false;
                        }
                            Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(Character);
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                    }
                    else if (j == SharkPosition.Item2 && i == SharkPosition.Item1)
                    {
                        if (Map[i, j] == '-')
                        {
                            Console.BackgroundColor = ConsoleColor.Yellow;
                        }
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(Shark);
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                    }
                    else if (Map[i, j] == ' ')
                    {
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.Write(Map[i, j]);
                    }
                    else if (Map[i, j] == '-')
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(Map[i, j]);
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                    }
                }

                Console.WriteLine();

            }
            
        }

        static void ProcessInput()
        {
            while (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;

                float NewPlayerX = 0;
                float NewPlayerY = 0;

                NewPlayerX = PlayerPosition.Item2;
                NewPlayerY = PlayerPosition.Item1;

                switch (key)
                {
                    case ConsoleKey.W:
                        if (NewPlayerY > 0) NewPlayerY--;
                        break;

                    case ConsoleKey.S:
                        if(NewPlayerY < Map.GetLength(0) - 1) NewPlayerY++;
                        break;
                    case ConsoleKey.A:
                        if (NewPlayerX > 0) NewPlayerX--;
                        break;

                    case ConsoleKey.D:
                        if (NewPlayerX < Map.GetLength(1) - 1) NewPlayerX++;
                        break;
                }

                PlayerPosition = ((int)NewPlayerY, (int)NewPlayerX);
            }
        }
    }
}
