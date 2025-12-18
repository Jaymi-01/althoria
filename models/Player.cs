// Models/Player.cs
using System;
using System.Collections.Generic;
using Althoria.System;

namespace Althoria.Models
{
    public class Player
    {
        public string Name { get; set; }
        public string Class { get; set; }
        public int Level { get; set; }
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; set; }
        public int Mana { get; set; }
        public int MaxMana { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Experience { get; set; }
        public int ExperienceToNextLevel { get; set; }
        public int Gold { get; set; }
        public List<Item> Inventory { get; set; }
        public List<Skill> Skills { get; set; }
        public Weapon EquippedWeapon { get; set; }
        public Armor EquippedArmor { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public Player(string name, string className)
        {
            Name = name;
            Class = className;
            Level = 1;
            Experience = 0;
            ExperienceToNextLevel = 100;
            Gold = 50;
            Inventory = new List<Item>();
            Skills = new List<Skill>();
            PositionX = 0;
            PositionY = 0;
            
            // Set stats based on class
            SetClassStats(className);
            SkillManager.LearnSkillsForLevel(this); // Learn initial skills
        }

        private void SetClassStats(string className)
        {
            switch (className.ToLower())
            {
                case "warrior":
                    MaxHealth = 120;
                    MaxMana = 30;
                    Attack = 15;
                    Defense = 10;
                    break;
                case "mage":
                    MaxHealth = 80;
                    MaxMana = 80;
                    Attack = 10;
                    Defense = 5;
                    break;
                case "rogue":
                    MaxHealth = 100;
                    MaxMana = 50;
                    Attack = 12;
                    Defense = 7;
                    break;
                default:
                    MaxHealth = 100;
                    MaxMana = 40;
                    Attack = 10;
                    Defense = 8;
                    break;
            }
            CurrentHealth = MaxHealth;
            Mana = MaxMana;
        }

        public void TakeDamage(int damage)
        {
            int actualDamage = Math.Max(1, damage - Defense);
            CurrentHealth -= actualDamage;
            Console.WriteLine($"{Name} takes {actualDamage} damage!");
            
            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                Console.WriteLine($"{Name} has been defeated!");
            }
        }

        public int AttackEnemy()
        {
            int totalAttack = Attack;
            if (EquippedWeapon != null)
                totalAttack += EquippedWeapon.AttackBonus;
            
            return totalAttack;
        }

        public void Heal(int amount)
        {
            CurrentHealth = Math.Min(CurrentHealth + amount, MaxHealth);
            Console.WriteLine($"{Name} healed for {amount} HP!");
        }

        public void GainExperience(int exp)
        {
            Experience += exp;
            Console.WriteLine($"{Name} gained {exp} experience!");
            
            while (Experience >= ExperienceToNextLevel)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            Level++;
            Experience -= ExperienceToNextLevel;
            ExperienceToNextLevel = (int)(ExperienceToNextLevel * 1.5);
            
            // Increase stats
            MaxHealth += 20;
            MaxMana += 10;
            CurrentHealth = MaxHealth;
            Mana = MaxMana;
            Attack += 3;
            Defense += 2;
            
            Console.WriteLine($"\n*** LEVEL UP! {Name} is now level {Level}! ***");
            Console.WriteLine($"Max Health: +20 | Max Mana: +10 | Attack: +3 | Defense: +2\n");
            SkillManager.LearnSkillsForLevel(this);
        }

        public void AddItem(Item item)
        {
            Inventory.Add(item);
            Console.WriteLine($"{item.Name} added to inventory!");
        }

        public void DisplayStats()
        {
            Console.WriteLine("\n=== CHARACTER STATS ===");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Class: {Class}");
            Console.WriteLine($"Level: {Level}");
            Console.WriteLine($"Health: {CurrentHealth}/{MaxHealth}");
            Console.WriteLine($"Mana: {Mana}/{MaxMana}");
            Console.WriteLine($"Attack: {Attack}" + (EquippedWeapon != null ? $" (+{EquippedWeapon.AttackBonus})" : ""));
            Console.WriteLine($"Defense: {Defense}" + (EquippedArmor != null ? $" (+{EquippedArmor.DefenseBonus})" : ""));
            Console.WriteLine($"Gold: {Gold}");
            Console.WriteLine($"Experience: {Experience}/{ExperienceToNextLevel}");
            Console.WriteLine("=======================\n");
        }

        public bool IsAlive()
        {
            return CurrentHealth > 0;
        }
    }
}