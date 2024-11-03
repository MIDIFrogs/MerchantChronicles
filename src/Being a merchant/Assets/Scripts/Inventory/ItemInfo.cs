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
        [SerializeField] private ItemRarity itemRarity;
        [SerializeField] private int recommendedPrice;

        public string Title => title;
        public string Description => description;
        public Sprite Icon => icon;
        public ItemType ItemType => itemType;
        public ItemRarity ItemRarity => itemRarity;
        public int RecommendedPrice => recommendedPrice;

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

        public string GetItemRarityTranslated()
        {
            return ItemRarity switch
            {
                ItemRarity.Common => "Обычная",
                ItemRarity.Uncommon => "Необычная",
                ItemRarity.Rare => "Редкая",
                ItemRarity.Epic => "Эпическая",
                ItemRarity.Legendary => "Легендарная",
                _ => "Неизвестно",
            };
        }
    }
}