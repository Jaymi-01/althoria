// System/GameMaster.cs
using System;
using Althoria.Models;

namespace Althoria.System
{
    public static class GameMaster
    {
        public static void ApplyBuffs(Player player, int health, int mana, int level, int attack, int defense)
        {
            player.MaxHealth += health;
            player.CurrentHealth += health;
            player.MaxMana += mana;
            player.Mana += mana;
            player.Level += level;
            player.Attack += attack;
            player.Defense += defense;

            Console.WriteLine("You feel stronger!");
            player.DisplayStats();
            Console.ReadKey();
        }

        public static void ApplyDebuffs(Player player, int health, int mana, int level, int attack, int defense)
        {
            player.MaxHealth = Math.Max(1, player.MaxHealth - health);
            player.CurrentHealth = Math.Max(1, player.CurrentHealth - health);
            player.MaxMana = Math.Max(0, player.MaxMana - mana);
            player.Mana = Math.Max(0, player.Mana - mana);
            player.Level = Math.Max(1, player.Level - 1);
            player.Attack = Math.Max(0, player.Attack - attack);
            player.Defense = Math.Max(0, player.Defense - defense);

            Console.WriteLine("You feel weaker!");
            player.DisplayStats();
            Console.ReadKey();
        }

        public static void GiveGold(Player player, int amount)
        {
            player.Gold += amount;
            Console.WriteLine($"You received {amount} gold!");
            player.DisplayStats();
            Console.ReadKey();
        }

        public static void GiveExperience(Player player, int amount)
        {
            player.GainExperience(amount);
            Console.WriteLine($"You received {amount} experience!");
            player.DisplayStats();
            Console.ReadKey();
        }
    }
}
