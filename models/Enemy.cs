// Models/Enemy.cs
using System;

namespace Althoria.Models
{
    public class Enemy
    {
        public string Name { get; set; }
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int ExperienceReward { get; set; }
        public int GoldReward { get; set; }

        public Enemy(string name, int maxHealth, int attack, int defense, int experience, int gold)
        {
            Name = name;
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
            Attack = attack;
            Defense = defense;
            ExperienceReward = experience;
            GoldReward = gold;
        }

        public void TakeDamage(int damage)
        {
            int actualDamage = Math.Max(1, damage - Defense);
            CurrentHealth -= actualDamage;
            if (CurrentHealth < 0)
            {
                CurrentHealth = 0;
            }
        }

        public bool IsAlive()
        {
            return CurrentHealth > 0;
        }
    }
}
