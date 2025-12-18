// System/Combat.cs
using System;
using System.Linq;
using Althoria.Models;

namespace Althoria.System
{
    public class Combat
    {
        public void StartCombat(Player player, Enemy enemy)
        {
            Console.Clear();
            Console.WriteLine($"A wild {enemy.Name} appears!");
            var random = new Random();

            while (player.IsAlive() && enemy.IsAlive())
            {
                // Player's turn
                Console.WriteLine("\n--- Your Turn ---");
                Console.WriteLine("1. Attack");
                Console.WriteLine("2. Use Skill");
                Console.WriteLine("3. Use Potion");
                Console.WriteLine("4. Flee");
                Console.Write("Choose your action: ");

                int choice;
                while (true)
                {
                    if (int.TryParse(Console.ReadLine(), out choice) && (choice >= 1 && choice <= 4))
                    {
                        break;
                    }
                    Console.Write("Invalid choice. Please enter a number between 1 and 4: ");
                }

                if (choice == 1)
                {
                    // Player attacks
                    int playerDamage = player.AttackEnemy();
                    enemy.TakeDamage(playerDamage);
                    Console.WriteLine($"{player.Name} attacks {enemy.Name} for {playerDamage} damage!");
                    Console.WriteLine($"{enemy.Name} has {enemy.CurrentHealth}/{enemy.MaxHealth} health remaining.");
                }
                else if (choice == 2)
                {
                    // Use Skill
                    if (player.Skills.Count == 0)
                    {
                        Console.WriteLine("You don't know any skills!");
                        continue;
                    }

                    Console.WriteLine("Choose a skill to use:");
                    for (int i = 0; i < player.Skills.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {player.Skills[i].Name} (Cost: {player.Skills[i].ManaCost})");
                    }

                    int skillChoice;
                    while (true)
                    {
                        if (int.TryParse(Console.ReadLine(), out skillChoice) && skillChoice >= 1 && skillChoice <= player.Skills.Count)
                        {
                            break;
                        }
                        Console.Write($"Invalid choice. Please enter a number between 1 and {player.Skills.Count}: ");
                    }

                    var skill = player.Skills[skillChoice - 1];
                    if (player.Mana >= skill.ManaCost)
                    {
                        player.Mana -= skill.ManaCost;
                        enemy.TakeDamage(skill.Damage);
                        Console.WriteLine($"{player.Name} uses {skill.Name} on {enemy.Name} for {skill.Damage} damage!");
                        Console.WriteLine($"{enemy.Name} has {enemy.CurrentHealth}/{enemy.MaxHealth} health remaining.");
                    }
                    else
                    {
                        Console.WriteLine("You don't have enough mana!");
                    }
                }
                else if (choice == 3)
                {
                    // Use Potion
                    var potion = player.Inventory.OfType<Potion>().FirstOrDefault();
                    if (potion != null)
                    {
                        player.Heal(potion.HealingAmount);
                        player.Inventory.Remove(potion);
                        Console.WriteLine($"You used a {potion.Name}.");
                    }
                    else
                    {
                        Console.WriteLine("You don't have any potions!");
                    }
                }
                else
                {
                    // Flee
                    Console.WriteLine("You attempt to flee...");
                    if (random.Next(0, 2) == 0) // 50% chance to flee
                    {
                        Console.WriteLine("You successfully fled!");
                        return; // Exit combat
                    }
                    else
                    {
                        Console.WriteLine("You failed to flee!");
                    }
                }


                if (!enemy.IsAlive())
                {
                    break;
                }

                // Enemy's turn
                Console.WriteLine("\n--- Enemy's Turn ---");
                int enemyDamage = enemy.Attack;
                player.TakeDamage(enemyDamage);
                Console.WriteLine($"{enemy.Name} attacks {player.Name} for {enemyDamage} damage!");
                player.DisplayStats();
            }

            // End of combat
            if (player.IsAlive())
            {
                Console.WriteLine($"\nYou defeated the {enemy.Name}!");
                player.GainExperience(enemy.ExperienceReward);
                player.Gold += enemy.GoldReward;
                Console.WriteLine($"You found {enemy.GoldReward} gold.");

                // Potion drop
                if (random.Next(0, 4) == 0) // 25% chance of potion drop
                {
                    var potion = new Potion("Minor Healing Potion", "Heals a small amount of health.", 10, 25, PotionQuality.Minor);
                    player.AddItem(potion);
                }
            }
            else
            {
                Console.WriteLine($"\nYou were defeated by the {enemy.Name}...");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
