using SibGameJam.Inventory;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SibGameJam.Minigames.UI
{
	public class MinigameItemSelector : MonoBehaviour
	{
        [SerializeField] private ItemSelectVM itemSelectPrefab;
        [SerializeField] private Button confirmButton;
        [SerializeField] private Transform gridToAttach;
        
		private ItemSelectVM selectedItem;

        public Transform Canvas { get; set; }

		public MinigameInfo Minigame { get; set; }

        private void Start()
        {
            foreach (var item in Minigame.SelectableItems)
            {
                var vm = Instantiate(itemSelectPrefab, gridToAttach);
                vm.Selectable = item;
                vm.Selector = this;
            }
        }

        public void OnConfirm()
        {
            var minigame = Instantiate(Minigame.MinigamePrefab, Canvas);
            minigame.StartGame(selectedItem.Selectable.RequestedItem, selectedItem.Selectable.FailItem);
            Destroy(gameObject);
        }

        public void OnItemSelected(ItemSelectVM selectVM)
        {
            if (selectedItem != null)
            {
                selectedItem.OnSelectionChanged(false);
            }
            selectedItem = selectVM;
            selectedItem.OnSelectionChanged(true);
            confirmButton.interactable = true;
        }

        public void OnCancel()
        {
            Destroy(gameObject);
        }
    }
}