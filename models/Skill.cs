// Models/Skill.cs
namespace Althoria.Models
{
    public class Skill
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Damage { get; set; }
        public int ManaCost { get; set; }

        public Skill(string name, string description, int damage, int manaCost)
        {
            Name = name;
            Description = description;
            Damage = damage;
            ManaCost = manaCost;
        }
    }
}
