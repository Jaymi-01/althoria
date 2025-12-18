// Core/Game.cs
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Althoria.Models;
using Althoria.System;

namespace Althoria.Core
{
    public class Game
    {
        private Player _player;
        private const int MapWidth = 10;
        private const int MapHeight = 10;
        private Random _random = new Random();
        private const string SaveFileName = "savegame.json";
        private Shop _shop;

        public Game()
        {
            _shop = new Shop();
        }

        public void Start()
        {
            Console.WriteLine("Welcome to Althoria!");
            Console.WriteLine("1. New Game");
            Console.WriteLine("2. Load Game");
            Console.Write("Enter your choice: ");

            int choice;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out choice) && (choice == 1 || choice == 2))
                {
                    break;
                }
                Console.Write("Invalid choice. Please enter 1 or 2: ");
            }

            if (choice == 1)
            {
                CreatePlayer();
            }
            else
            {
                LoadGame();
            }
            
            GameLoop();
        }

        private void CreatePlayer()
        {
            Console.Write("Enter your character's name: ");
            string name = Console.ReadLine();

            Console.WriteLine("Choose your class:");
            Console.WriteLine("1. Warrior");
            Console.WriteLine("2. Mage");
            Console.WriteLine("3. Rogue");
            Console.Write("Enter the number of your choice: ");

            string className;
            int choice;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= 3)
                {
                    break;
                }
                Console.Write("Invalid choice. Please enter a number between 1 and 3: ");
            }

            switch (choice)
            {
                case 1:
                    className = "Warrior";
                    break;
                case 2:
                    className = "Mage";
                    break;
                case 3:
                    className = "Rogue";
                    break;
                default:
                    className = "Adventurer";
                    break;
            }

            _player = new Player(name, className);
            _player.DisplayStats();
        }

        private void GameLoop()
        {
            if (_player == null)
            {
                Console.WriteLine("Error: Player object is null. Exiting game.");
                return;
            }

            while (_player.IsAlive())
            {
                DrawMap();
                HandleInput();
            }

            Console.WriteLine("Game Over!");
        }

        private void DrawMap()
        {
            Console.Clear();
            Console.WriteLine("=======================");
            Console.WriteLine("        MAP            ");
            Console.WriteLine("=======================");

            for (int y = 0; y < MapHeight; y++)
            {
                for (int x = 0; x < MapWidth; x++)
                {
                    if (x == _player.PositionX && y == _player.PositionY)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("P ");
                        Console.ResetColor();
                    }
                    else if (x == 0 && y == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("S ");
                        Console.ResetColor();
                    }
                    else if (x == 1 && y == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("$ ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(". ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("=======================");
            Console.WriteLine("Use WASD or Arrow Keys to move. 'Q' to quit.");
            if (_player.PositionX == 0 && _player.PositionY == 0)
            {
                Console.WriteLine("Press 'F' to save your game.");
            }
            if (_player.PositionX == 1 && _player.PositionY == 0)
            {
                Console.WriteLine("Press 'E' to enter the shop.");
            }
            _player.DisplayStats();
        }

        private void HandleInput()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            bool playerMoved = false;
            switch (keyInfo.Key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    if (_player.PositionY > 0)
                    {
                        _player.PositionY--;
                        playerMoved = true;
                    }
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    if (_player.PositionX > 0)
                    {
                        _player.PositionX--;
                        playerMoved = true;
                    }
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    if (_player.PositionY < MapHeight - 1)
                    {
                        _player.PositionY++;
                        playerMoved = true;
                    }
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    if (_player.PositionX < MapWidth - 1)
                    {
                        _player.PositionX++;
                        playerMoved = true;
                    }
                    break;
                case ConsoleKey.Q:
                    _player.TakeDamage(_player.CurrentHealth); // A bit of a hack to end the game
                    break;
                case ConsoleKey.F:
                    if (_player.PositionX == 0 && _player.PositionY == 0)
                    {
                        SaveGame();
                    }
                    break;
                case ConsoleKey.E:
                    if (_player.PositionX == 1 && _player.PositionY == 0)
                    {
                        _shop.Show(_player);
                    }
                    break;
                case ConsoleKey.C:
                    ShowCheatConsole();
                    break;
            }

            if(playerMoved)
            {
                CheckForEncounter();
            }
        }

        private void ShowCheatConsole()
        {
            Console.WriteLine("\nEnter cheat command:");
            string input = Console.ReadLine();
            string[] parts = input.Split(' ');

            if (parts.Length < 2)
            {
                Console.WriteLine("Invalid cheat command.");
                Console.ReadKey();
                return;
            }

            string command = parts[0].ToLower();
            string target = parts[1].ToLower();
            int value = parts.Length > 2 && int.TryParse(parts[2], out int v) ? v : 0;

            switch (command)
            {
                case "buff":
                    switch (target)
                    {
                        case "health": GameMaster.ApplyBuffs(_player, value, 0, 0, 0, 0); break;
                        case "mana": GameMaster.ApplyBuffs(_player, 0, value, 0, 0, 0); break;
                        case "attack": GameMaster.ApplyBuffs(_player, 0, 0, 0, value, 0); break;
                        case "defense": GameMaster.ApplyBuffs(_player, 0, 0, 0, 0, value); break;
                        case "level": GameMaster.ApplyBuffs(_player, 0, 0, value, 0, 0); break;
                        default: Console.WriteLine("Invalid buff target."); break;
                    }
                    break;
                case "debuff":
                    switch (target)
                    {
                        case "health": GameMaster.ApplyDebuffs(_player, value, 0, 0, 0, 0); break;
                        case "mana": GameMaster.ApplyDebuffs(_player, 0, value, 0, 0, 0); break;
                        case "attack": GameMaster.ApplyDebuffs(_player, 0, 0, 0, value, 0); break;
                        case "defense": GameMaster.ApplyDebuffs(_player, 0, 0, 0, 0, value); break;
                        case "level": GameMaster.ApplyDebuffs(_player, 0, 0, value, 0, 0); break;
                        default: Console.WriteLine("Invalid debuff target."); break;
                    }
                    break;
                case "gold":
                    GameMaster.GiveGold(_player, value);
                    break;
                case "exp":
                    GameMaster.GiveExperience(_player, value);
                    break;
                default:
                    Console.WriteLine("Invalid cheat command.");
                    break;
            }
        }

        private void CheckForEncounter()
        {
            // 25% chance of encounter
            if (_random.Next(0, 4) == 0)
            {
                Console.WriteLine("An enemy appears!");
                Console.ReadKey();

                var enemies = new List<Enemy>
                {
                    new Enemy("Goblin", 30, 8, 2, 25, 10),
                    new Enemy("Skeleton", 40, 10, 3, 35, 15),
                    new Enemy("Orc", 60, 12, 5, 50, 25),
                    new Enemy("Slime", 20, 6, 1, 15, 5)
                };

                var enemy = enemies[_random.Next(enemies.Count)];
                var combat = new Combat();
                combat.StartCombat(_player, enemy);
            }
        }

        private void SaveGame()
        {
            string jsonString = JsonConvert.SerializeObject(_player, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented
            });
            File.WriteAllText(SaveFileName, jsonString);
            Console.WriteLine("Game saved!");
            Console.ReadKey();
        }

        private void LoadGame()
        {
            if (File.Exists(SaveFileName))
            {
                string jsonString = File.ReadAllText(SaveFileName);
                _player = JsonConvert.DeserializeObject<Player>(jsonString, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });

                if (_player == null)
                {
                    Console.WriteLine("Failed to load save file. Starting a new game.");
                    Console.ReadKey();
                    CreatePlayer();
                }
                else
                {
                    Console.WriteLine("Game loaded!");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("No save file found. Starting a new game.");
                Console.ReadKey();
                CreatePlayer();
            }
        }
    }
}
