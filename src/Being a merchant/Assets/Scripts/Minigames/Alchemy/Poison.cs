using UnityEngine;

namespace SibGameJam.Inventory
{
    [CreateAssetMenu(fileName = "New Poison Info", menuName = "Poison/Poison definition")]
    public class Poison : ScriptableObject
    {
        [SerializeField] private ItemInfo first;
        [SerializeField] private ItemInfo second;
        [SerializeField] private ItemInfo third;
        [SerializeField] private ItemInfo poisonR;

        public ItemInfo First => first;
        public ItemInfo Second => second;
        public ItemInfo Third => third;
        public ItemInfo PoisonR => poisonR;

    }
}