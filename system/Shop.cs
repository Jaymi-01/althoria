// System/Shop.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Althoria.Models;

namespace Althoria.System
{
    public class Shop
    {
        private List<Weapon> _weapons;

        public Shop()
        {
            InitializeWeapons();
        }

        private void InitializeWeapons()
        {
            _weapons = new List<Weapon>
            {
                // Level 1-10
                new Weapon("Iron Sword", "A basic iron sword.", 15, 5) { MinLevel = 1, MaxLevel = 10 },
                new Weapon("Wooden Staff", "A simple wooden staff.", 10, 3) { MinLevel = 1, MaxLevel = 10 },
                new Weapon("Iron Dagger", "A small iron dagger.", 12, 4) { MinLevel = 1, MaxLevel = 10 },

                // Level 11-20
                new Weapon("Steel Sword", "A well-crafted steel sword.", 30, 10) { MinLevel = 11, MaxLevel = 20 },
                new Weapon("Enchanted Staff", "A staff with a magical aura.", 25, 8) { MinLevel = 11, MaxLevel = 20 },
                new Weapon("Steel Dagger", "A sharp steel dagger.", 28, 9) { MinLevel = 11, MaxLevel = 20 },

                // Level 21-30
                new Weapon("Mithril Sword", "A lightweight and strong sword.", 60, 20) { MinLevel = 21, MaxLevel = 30 },
                new Weapon("Archmage Staff", "A powerful staff for an archmage.", 50, 18) { MinLevel = 21, MaxLevel = 30 },
                new Weapon("Mithril Dagger", "A deadly mithril dagger.", 55, 19) { MinLevel = 21, MaxLevel = 30 },
            };
        }

        public void Show(Player player)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Weapon Shop!");
            Console.WriteLine("Here are the weapons available for your level:");

            var availableWeapons = _weapons.Where(w => player.Level >= w.MinLevel && player.Level <= w.MaxLevel).ToList();

            for (int i = 0; i < availableWeapons.Count; i++)
            {
                var weapon = availableWeapons[i];
                Console.WriteLine($"{i + 1}. {weapon.Name} - Damage: {weapon.AttackBonus}, Price: {weapon.Value}");
            }

            Console.WriteLine($"{availableWeapons.Count + 1}. Exit");
            Console.Write("Enter your choice: ");

            int choice;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= availableWeapons.Count + 1)
                {
                    break;
                }
                Console.Write($"Invalid choice. Please enter a number between 1 and {availableWeapons.Count + 1}: ");
            }

            if (choice == availableWeapons.Count + 1)
            {
                return;
            }

            var selectedWeapon = availableWeapons[choice - 1];

            if (player.Gold >= selectedWeapon.Value)
            {
                player.Gold -= selectedWeapon.Value;
                player.AddItem(selectedWeapon);
            }
            else
            {
                Console.WriteLine("You don't have enough gold!");
                Console.ReadKey();
            }
        }
    }
}
