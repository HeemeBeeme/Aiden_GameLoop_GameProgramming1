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
        static int movetime = 0;

        static int TreasureCollected = 0;
        static int TreasureSpawnChance;
        static int TreasureSpawnLimit = 0;

        static (int, int) SharkPosition = (7, 20);
        static (int, int) PlayerPosition = (5, 4);

        static float NewSharkPositionX = SharkPosition.Item2;
        static float NewSharkPositionY = SharkPosition.Item1;

        static float SharkSpeed = 0.4f;

        static float NewSharkX = 0;
        static float NewSharkY = 0;

        static bool isDead = false;
        static bool isOnIsland = false;

        static Random TreasureSpawnChanceRnD = new Random();

        static char[,] Map = { {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',},
                               {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',},
                               {' ', ' ', '-', '-', '-', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',},
                               {' ', '-', '-', '-', '-', '-', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',},
                               {' ', '-', '-', '-', '-', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',},
                               {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',},
                               {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',},
                               {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',},
                               {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '-',},
                               {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '-', '-', '-', '-',},
                               {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '-', '-', '-', '-',},
                               {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '-', '-', '-', '-', '-',},
                               {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '-', '-', '-', '-', '-', '-',},
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

            if(isDead)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You Have Died!\n");

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"Treasure Collected: {TreasureCollected}\n");

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Press Any Key To Quit...");
                Thread.Sleep(2000);

                Console.ReadKey(true);
            }


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

                        NewSharkX = (float)Math.Round(NewSharkPositionX);
                        NewSharkY = (float)Math.Round(NewSharkPositionY);

                        if (Map[(int)NewSharkY, (int)NewSharkX] == ' ' || Map[(int)NewSharkY, (int)NewSharkX] == '$')
                        {
                            SharkPosition = ((int)NewSharkY, (int)NewSharkX);
                        }
                        else
                        {
                            NewSharkPositionX = SharkPosition.Item2;
                            NewSharkPositionY = SharkPosition.Item1;
                        }

                        if (Math.Abs(PlayerPosition.Item2 - SharkPosition.Item2) < 1 && Math.Abs(PlayerPosition.Item1 - SharkPosition.Item1) < 1)
                        {
                            isDead = true;
                            Console.BackgroundColor = ConsoleColor.Black;
                        }

                    }
                }
                else
                {
                    if (movetime == 2)
                    {
                        Random SharkMovementRnD = new Random();

                        float SharkMovement = SharkMovementRnD.Next(0, 4);

                        //0 = up
                        //1 = down
                        //2 = left
                        //3 = right

                        NewSharkPositionX = SharkPosition.Item2;
                        NewSharkPositionY = SharkPosition.Item1;

                        switch (SharkMovement)
                        {
                            case 0:
                                if(NewSharkPositionY > 0)
                                {
                                    if (Map[(int)NewSharkPositionY - 1, (int)NewSharkPositionX] == ' ' && NewSharkPositionY > 0)
                                    {
                                        NewSharkPositionY--;
                                    }
                                }
                                break;

                            case 1:
                                if(NewSharkPositionY < Map.GetLength(0) - 1)
                                {
                                    if (Map[(int)NewSharkPositionY + 1, (int)NewSharkPositionX] == ' ' && NewSharkPositionY < Map.GetLength(0))
                                    {
                                        NewSharkPositionY++;
                                    }
                                }
                                break;

                            case 2:
                                if(NewSharkPositionX > 0)
                                {
                                    if (Map[(int)NewSharkPositionY, (int)NewSharkPositionX - 1] == ' ' && NewSharkPositionX > 0)
                                    {
                                        NewSharkPositionX--;
                                    }
                                }
                                break;

                            case 3:
                                if(NewSharkPositionX <  Map.GetLength(1) - 1)
                                {
                                    if (Map[(int)NewSharkPositionY, (int)NewSharkPositionX + 1] == ' ' && NewSharkPositionX < Map.GetLength(1) - 1)
                                    {
                                        NewSharkPositionX++;
                                    }
                                }
                                break;
                        }

                        SharkPosition = ((int)NewSharkPositionY, (int)NewSharkPositionX);
                        movetime = 0;

                    }
                    else
                    {
                        Thread.Sleep(FrameTime);
                        movetime++;
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
                            Console.BackgroundColor = ConsoleColor.Green;
                            isOnIsland = true;
                        }
                        else if (Map[i, j] == '$')
                        {
                            TreasureCollected++;
                            Map[i, j] = ' ';
                            TreasureSpawnLimit--;
                        }
                        else
                        {
                            isOnIsland = false;
                        }
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write(Character);
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                    }
                    else if (j == SharkPosition.Item2 && i == SharkPosition.Item1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(Shark);
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                    }
                    else if (Map[i, j] == ' ')
                    {
                        if (TreasureSpawnLimit < 4)
                        {
                            TreasureSpawnChance = TreasureSpawnChanceRnD.Next(0, 150);

                            if (TreasureSpawnChance == 0)
                            {
                                TreasureSpawnLimit++;
                                Map[i, j] = '$';
                            }
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                            Console.Write(Map[i, j]);
                        }

                    }
                    else if (Map[i, j] == '-')
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(Map[i, j]);
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                    }
                    else if (Map[i, j] == '$')
                    {
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("$");
                    }
                }

                Console.WriteLine();

            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Treasure Collected: {TreasureCollected}");

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
