// Models/Weapon.cs
namespace Althoria.Models
{
    public class Weapon : Item
    {
        public int AttackBonus { get; set; }
        public int MinLevel { get; set; }
        public int MaxLevel { get; set; }

        public Weapon(string name, string description, int value, int attackBonus) 
            : base(name, description, value)
        {
            AttackBonus = attackBonus;
        }
    }
}
