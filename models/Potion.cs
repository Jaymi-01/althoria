// Models/Potion.cs
namespace Althoria.Models
{
    public enum PotionQuality
    {
        Minor,
        Standard,
        Greater
    }

    public class Potion : Item
    {
        public int HealingAmount { get; set; }
        public PotionQuality Quality { get; set; }

        public Potion(string name, string description, int value, int healingAmount, PotionQuality quality) 
            : base(name, description, value)
        {
            HealingAmount = healingAmount;
            Quality = quality;
        }
    }
}
