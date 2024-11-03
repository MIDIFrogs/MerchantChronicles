using SibGameJam.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SibGameJam.Minigames
{
    public abstract class Minigame : MonoBehaviour
    {
        [SerializeField] private GameObject gameMenu;

        public ItemInfo SelectedItem { get; private set; }

        public ItemInfo FailItem { get; private set; }

        public MinigameTask Level { get; private set; }

        public void StartGame(ItemInfo selectedItem, ItemInfo failItem, MinigameTask level)
        {
            SelectedItem = selectedItem;
            FailItem = failItem;
            Level = level;
            gameMenu.SetActive(true);
            OnGameStarted();
        }

        protected abstract void OnGameStarted();

        public void EndGame(bool success)
        {
            gameMenu.SetActive(false);
            if (success)
            {
                PlayerStats.Session.Inventory.TryAddItem(SelectedItem);
            }
            else
            {
                PlayerStats.Session.Inventory.TryAddItem(FailItem);
            }
            OnGameFinished(success);
        }

        protected abstract void OnGameFinished(bool success);
    }
}
