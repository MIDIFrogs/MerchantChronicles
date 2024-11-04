using System.Collections.Generic;
using UnityEngine;

namespace SibGameJam.Inventory
{
    [CreateAssetMenu(fileName = "New price maker", menuName = "Items/Price maker")]
	public class PriceMaker : ScriptableObject
	{
		[SerializeField] private List<ItemInfo> allItems;
		[SerializeField] private ItemInfo leastValuable;
		[SerializeField] private ItemInfo mostValuable;

		[SerializeField] private List<int> itemPrices = new();

        private void ReloadPrices()
        {
            if (leastValuable != null && mostValuable != null)
            {
                itemPrices.Clear();
                int leastIndex = allItems.IndexOf(leastValuable);
                int mostIndex = allItems.IndexOf(mostValuable);
                int leastPrice = Random.Range(0, 100);
                int mostPrice = Random.Range(500, 600);
                int rarityCount = System.Enum.GetValues(typeof(ItemRarity)).Length;
                int i = 0;
                foreach (var item in allItems)
                {
                    if (i == leastIndex)
                    {
                        itemPrices.Add(leastPrice);
                    }
                    else if (i == mostIndex)
                    {
                        itemPrices.Add(mostPrice);
                    }
                    else
                    {
                        int price = (int)(Mathf.Lerp(leastPrice, mostPrice, (float)item.ItemRarity / rarityCount) * Random.Range(0.8f, 1.2f));
                        Debug.Log($"Generated price: {price} (least: {leastPrice}, most: {mostPrice}) rarity: {item.ItemRarity}");
                        itemPrices.Add(price);
                    }
                    i++;
                }
            }
        }

        public int CalculateRecommendedPrice(ItemInfo item)
        {
            decimal reputationPriceCoefficient = PlayerStats.Session.Reputation switch
            {
                < -10 => 0.5m,
                < 0 => 0.8m,
                < 5 => 1m,
                < 10 => 1.25m,
                _ => 1.5m,
            };
            int index = allItems.IndexOf(item);
            int defaultPrice = 0;
            if (index > 0)
            {
                defaultPrice = itemPrices[index];
            }
            return (int)decimal.Round(defaultPrice * reputationPriceCoefficient);
        }

        public int GetMaxPrice(ItemInfo item)
        {
            int price = CalculateRecommendedPrice(item);
            // Add random percent from 20 to 100 and then round to nearest tens.
            return (int)decimal.Round(price * (decimal)Random.Range(1.2f, 2f) * 0.1m) * 10;
        }

        public bool IsPriceGood(ItemInfo item, int price)
        {
            int likePrice = (int)(CalculateRecommendedPrice(item) * Random.Range(0.95f, 1.05f));
            return price <= likePrice;
        }

        private void OnValidate()
        {
            ReloadPrices();
        }

        private void Awake()
        {
            ReloadPrices();
        }
    }
}