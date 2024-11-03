using SibGameJam.Inventory;
using System.Collections.Generic;
using UnityEngine;

namespace SibGameJam.DialogSystem
{
	public class LevelDialogRunner : MonoBehaviour
	{
		[SerializeField] private DialogPlayer dialogPlayer;

		[Header("Level requirements")]
		[SerializeField] private List<ItemInfo> goodItems;

		[Header("Level start")]
		[SerializeField] private Dialog startDialog;

		[Header("Level trade dialogs")]
		[SerializeField] private Dialog sellGoodPriceDialog;
		[SerializeField] private Dialog sellBadPriceDialog;

		[Header("Level epilogue dialogs")]
		[SerializeField] private Dialog goodEndingDialog;
		[SerializeField] private Dialog badEndingDialog;

        public async void Start()
        {
			// Begin start dialog.
			await dialogPlayer.StartDialogAsync(startDialog);
        }

		public bool IsItemGood(ItemInfo item) => goodItems.Contains(item);

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
			return (int)decimal.Round(item.RecommendedPrice * reputationPriceCoefficient);
		}

		public int GetMaxPrice(ItemInfo item)
		{
			int price = CalculateRecommendedPrice(item);
			// Add random percent from 20 to 100 and then round to nearest tens.
			return (int)decimal.Round(price * (decimal)Random.Range(1.2f, 2f) * 0.1m) * 10;
		}

		public async void OnItemSold(ItemInfo item, int price)
		{
			bool itemGood = IsItemGood(item);
			// Max good price is 120% from recommended.
			int maxGoodPrice = (int)decimal.Round(CalculateRecommendedPrice(item) * 0.12m) * 10;
			bool priceGood = price <= maxGoodPrice;
			GameResult result;
			Dialog dialogToPlay;
			Dialog epilogue;
			if (priceGood)
			{
				dialogToPlay = sellGoodPriceDialog;
				if (itemGood)
				{
					result = GameResult.Good;
					epilogue = goodEndingDialog;
				}
				else
				{
					result = GameResult.Bad;
					epilogue = badEndingDialog;
				}
			}
			else
			{
				dialogToPlay = sellBadPriceDialog;
				if (itemGood)
				{
					result = GameResult.Neutral;
					epilogue = goodEndingDialog;
				}
				else
				{
					result = GameResult.Fail;
					epilogue = badEndingDialog;
				}
			}
			Debug.Log($"Level ended with result {result}. Starting playing dialogs.");
			await dialogPlayer.StartDialogAsync(dialogToPlay);
			await dialogPlayer.StartDialogAsync(epilogue);
			PlayerStats.Session.LevelCompleted(result, price);
		}
    }
}