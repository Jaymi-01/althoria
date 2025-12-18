// Models/Armor.cs
namespace Althoria.Models
{
    public class Armor : Item
    {
        public int DefenseBonus { get; set; }

        public Armor(string name, string description, int value, int defenseBonus) 
            : base(name, description, value)
        {
            DefenseBonus = defenseBonus;
        }
    }
}
