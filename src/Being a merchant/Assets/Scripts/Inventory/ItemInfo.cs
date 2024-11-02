using UnityEngine;

namespace SibGameJam.Inventory
{
    [CreateAssetMenu(fileName = "New Item Info", menuName = "Items/Item definition")]
    public class ItemInfo : ScriptableObject
    {
        [SerializeField] private string title;
        [SerializeField] private string description;
        [SerializeField] private Sprite icon;
        [SerializeField] private ItemType itemType;
        [SerializeField] private RarityType rarityType;

        public string Title => title;
        public string Description => description;
        public Sprite Icon => icon;
        public ItemType ItemType => itemType;
        public RarityType RarityType => rarityType;

        public string GetItemTypeTranslated()
        {
            return itemType switch
            {
                ItemType.Equipment => "Экипировка",
                ItemType.Food => "Еда",
                ItemType.Potion => "Зелья",
                ItemType.Other => "Другое",
                _ => "Неизвестно",
            };
        }

        public string GetRarityTypeTranslated()
        {
            return RarityType switch
            {
                RarityType.Poor => "Мусор",
                RarityType.Common => "Обычная",
                RarityType.Mythical => "Мифическая",
                RarityType.Legendary => "Легендарная",
                RarityType.Quest => "Квест",
                _ => "Неизвестно",
            };
        }
    }
}