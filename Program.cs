using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace Packman
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            char[,] map = ReadMap("map.txt");
            int score = 0;
            int userX = 1;
            int userY = 1;
            ConsoleKeyInfo charKey = new ConsoleKeyInfo('w', ConsoleKey.W, false, false, false);

            Task.Run(() =>
            {
                while(true)
                { 
                charKey = Console.ReadKey();
                }
            });

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                DrawMap(map);


                Console.SetCursorPosition(userX, userY);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write('@');
                Console.SetCursorPosition(52, 0);
                Console.Write($"score {score}");

                Thread.Sleep(250);

                HandleInput(charKey, ref userX, ref userY, map, ref score);

            }
        }
        private static void DrawMap(char[,] map)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    Console.Write(map[x, y]);
                }
                Console.WriteLine();
            }
        }
        private static void HandleInput(ConsoleKeyInfo charKey, ref int userX, ref int userY, char[,] map, ref int score)
        {
            switch (charKey.Key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    if (map[userX, userY - 1] == ' ' || map[userX, userY - 1] == '.')
                    {
                        userY--;
                        AddScore(ref score, map, userX, userY);
                    }
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    if (map[userX, userY + 1] == ' ' || map[userX, userY + 1] == '.')
                    {
                        userY++;
                        AddScore(ref score, map, userX, userY);
                    }
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    if (map[userX + 1, userY] == ' ' || map[userX + 1, userY] == '.')
                    {
                        userX++;
                        AddScore(ref score, map, userX, userY);
                    }
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    if (map[userX - 1, userY] == ' ' || map[userX - 1, userY] == '.')
                    {
                        userX--;
                        AddScore(ref score, map, userX, userY);
                    }
                    break;
            }
        }

        private static char[,] ReadMap(string path)
        {
            string[] file = File.ReadAllLines(path);

            char[,] map = new char[GetMaxLengthOfLine(file), file.Length];

            for (int x = 0; x < map.GetLength(0); x++)
                for (int y = 0; y < map.GetLength(1); y++)
                    map[x, y] = file[y][x];

            return map;
        }
        private static int GetMaxLengthOfLine(string[] lines)
        {
            int maxLenth = lines[0].Length;

            foreach (var line in lines)
            {
                if (line.Length > maxLenth)
                {
                    maxLenth = line.Length;
                }
            }
            return maxLenth;
        }
        private static int AddScore(ref int score, char[,] map, int userX, int userY)
        {
            if (map[userX, userY] == '.')
            {
                score++;
                map[userX, userY] = ' ';
            }
            return score;
        }

    }
}


