using SibGameJam.Inventory;
using SibGameJam.Minigames.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SibGameJam.DialogSystem
{
	public class ItemSellDialog : MonoBehaviour
	{
		[SerializeField] private ItemSellVM itemSelectPrefab;
		[SerializeField] private Transform grid;
		[SerializeField] private Button confirmButton;
		[SerializeField] private TextMeshProUGUI priceLabel;
		[SerializeField] private TextMeshProUGUI maxPriceLabel;
		[SerializeField] private Button upButton;
		[SerializeField] private Button downButton;

		private readonly List<ItemSellVM> items = new();
		private readonly List<int> maxPrices = new();

		private int price;
		private int maxPrice;
		private ItemSellVM selectedItem;
		private LevelDialogRunner dialogRunner;

		public void Begin(LevelDialogRunner dialogRunner)
		{
			this.dialogRunner = dialogRunner;
            foreach (var item in PlayerStats.Session.Inventory)
            {
                var sel = Instantiate(itemSelectPrefab, grid);
                sel.Item = item;
				sel.SellDialog = this;
                items.Add(sel);
                maxPrices.Add(dialogRunner.GetMaxPrice(item));
            }
        }

		public void UpdatePrice()
		{
			priceLabel.text = price.ToString();
		}

        public void OnPriceUp()
		{
			price += 10;
			if (price > maxPrice)
				price = maxPrice;
			UpdatePrice();
		}

		public void OnPriceDown()
		{
			price -= 10;
			if (price < 0)
				price = 0;
			UpdatePrice();
		}

		public void OnConfirm()
		{
			Destroy(gameObject);
			dialogRunner.OnItemSold(selectedItem.Item, price);
		}

		public void OnCancel()
		{
			Destroy(gameObject);
		}

		public void OnItemSelected(ItemSellVM item)
		{
			selectedItem?.OnSelectionChanged(false);
			selectedItem = item;
			selectedItem.OnSelectionChanged(true);
			confirmButton.interactable = true;
			upButton.interactable = downButton.interactable = true;
			maxPrice = maxPrices[items.IndexOf(selectedItem)];
			maxPriceLabel.text = maxPrice.ToString();
			price = maxPrice / 20 * 10; // To round to 10.
			UpdatePrice();
		}
	}
}