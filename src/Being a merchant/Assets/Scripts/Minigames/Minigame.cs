using SibGameJam.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SibGameJam.Minigames
{
    public abstract class Minigame : MonoBehaviour
    {
        [SerializeField] private GameObject gameMenu;

        public PlayerInventory Inventory { get; private set; }

        public ItemInfo SelectedItem { get; private set; }

        public ItemInfo FailItem { get; private set; }

        public void StartGame(PlayerInventory inventory, ItemInfo selectedItem, ItemInfo failItem)
        {
            Inventory = inventory;
            SelectedItem = selectedItem;
            FailItem = failItem;
            gameMenu.SetActive(true);
            OnGameStarted();
        }

        protected abstract void OnGameStarted();

        public void EndGame(bool success)
        {
            gameMenu.SetActive(false);
            if (success)
            {
                Inventory.TryAddItem(SelectedItem);
            }
            else
            {
                Inventory.TryAddItem(FailItem);
            }
            OnGameFinished();
        }

        protected abstract void OnGameFinished();
    }
}
