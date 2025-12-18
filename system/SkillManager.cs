// System/SkillManager.cs
using System.Collections.Generic;
using Althoria.Models;

namespace Althoria.System
{
    public static class SkillManager
    {
        public static void LearnSkillsForLevel(Player player)
        {
            player.Skills.Clear();
            switch (player.Class.ToLower())
            {
                case "warrior":
                    LearnWarriorSkills(player);
                    break;
                case "mage":
                    LearnMageSkills(player);
                    break;
                case "rogue":
                    LearnRogueSkills(player);
                    break;
            }
        }

        private static void LearnWarriorSkills(Player player)
        {
            if (player.Level >= 1) player.Skills.Add(new Skill("Power Strike", "A powerful attack.", 20, 10));
            if (player.Level >= 5) player.Skills.Add(new Skill("Shield Bash", "A defensive attack.", 15, 15));
            if (player.Level >= 10) player.Skills.Add(new Skill("Whirlwind", "Attack all enemies.", 40, 25));
            if (player.Level >= 20) player.Skills.Add(new Skill("Cleave", "A mighty blow that hits multiple targets.", 60, 35));
            if (player.Level >= 30) player.Skills.Add(new Skill("War Cry", "Boosts attack power.", 0, 30)); // a buff, not a direct damage skill
            if (player.Level >= 40) player.Skills.Add(new Skill("Berserker Rage", "Increases attack speed and damage.", 0, 40));
            if (player.Level >= 50) player.Skills.Add(new Skill("Ground Slam", "Slams the ground, stunning enemies.", 70, 50));
            if (player.Level >= 60) player.Skills.Add(new Skill("Execute", "A deadly blow to a low-health enemy.", 150, 60));
            if (player.Level >= 70) player.Skills.Add(new Skill("Last Stand", "Become invincible for a short duration.", 0, 70));
            if (player.Level >= 80) player.Skills.Add(new Skill("Avatar", "Transform into a colossal warrior.", 0, 80));
            if (player.Level >= 90) player.Skills.Add(new Skill("Ragnarok", "Unleash a devastating final attack.", 500, 100));
        }

        private static void LearnMageSkills(Player player)
        {
            if (player.Level >= 1) player.Skills.Add(new Skill("Fireball", "A fiery projectile.", 25, 15));
            if (player.Level >= 5) player.Skills.Add(new Skill("Ice Lance", "A shard of ice.", 30, 20));
            if (player.Level >= 10) player.Skills.Add(new Skill("Lightning Bolt", "A bolt of lightning.", 35, 25));
            if (player.Level >= 15) player.Skills.Add(new Skill("Earth Spike", "A jagged rock spike.", 40, 30));
            if (player.Level >= 20) player.Skills.Add(new Skill("Meteor", "A giant meteor from the sky.", 80, 50));
            if (player.Level >= 30) player.Skills.Add(new Skill("Blizzard", "A freezing storm that damages all enemies.", 70, 60));
            if (player.Level >= 40) player.Skills.Add(new Skill("Chain Lightning", "Lightning that jumps between enemies.", 90, 70));
            if (player.Level >= 50) player.Skills.Add(new Skill("Earthquake", "A powerful earthquake that damages all enemies.", 120, 80));
            if (player.Level >= 60) player.Skills.Add(new Skill("Teleport", "Instantly move to another location.", 0, 50));
            if (player.Level >= 70) player.Skills.Add(new Skill("Summon Elemental", "Summon a powerful elemental to fight for you.", 0, 100));
            if (player.Level >= 80) player.Skills.Add(new Skill("Arcane Power", "Greatly increases spell damage.", 0, 90));
            if (player.Level >= 90) player.Skills.Add(new Skill("Apocalypse", "A cataclysmic spell that destroys everything.", 1000, 200));
        }

        private static void LearnRogueSkills(Player player)
        {
            if (player.Level >= 1) player.Skills.Add(new Skill("Backstab", "A swift and deadly attack.", 18, 12));
            if (player.Level >= 5) player.Skills.Add(new Skill("Poison Strike", "An attack that poisons the enemy.", 12, 18));
            if (player.Level >= 10) player.Skills.Add(new Skill("Assassinate", "A powerful single-target attack.", 50, 35));
            if (player.Level >= 20) player.Skills.Add(new Skill("Vanish", "Become invisible to enemies.", 0, 40));
            if (player.Level >= 30) player.Skills.Add(new Skill("Fan of Knives", "Throw daggers at all enemies.", 50, 50));
            if (player.Level >= 40) player.Skills.Add(new Skill("Shadow Dance", "Allows the use of stealth abilities in combat.", 0, 60));
            if (player.Level >= 50) player.Skills.Add(new Skill("Eviscerate", "A vicious finishing move.", 120, 70));
            if (player.Level >= 60) player.Skills.Add(new Skill("Marked for Death", "Mark an enemy for death, increasing damage taken.", 0, 50));
            if (player.Level >= 70) player.Skills.Add(new Skill("Cloak of Shadows", "Become immune to magic damage.", 0, 80));
            if (player.Level >= 80) player.Skills.Add(new Skill("Vendetta", "Focus on a single target, dealing massive damage.", 300, 100));
            if (player.Level >= 90) player.Skills.Add(new Skill("Death from Above", "A powerful AoE attack from the shadows.", 400, 120));
        }
    }
}
