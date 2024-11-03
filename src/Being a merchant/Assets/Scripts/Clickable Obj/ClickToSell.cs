using SibGameJam.DialogSystem;
using UnityEngine;

namespace SibGameJam
{
	public class ClickToSell : MonoBehaviour
	{
		[SerializeField] private LevelDialogRunner dialogRunner;
		[SerializeField] private ItemSellDialog sellDialogPrefab;
		[SerializeField] private Transform canvas;

		public void OnClick()
		{
			var sellDialog = Instantiate(sellDialogPrefab, canvas);
			sellDialog.Begin(dialogRunner);
		}
	}
}