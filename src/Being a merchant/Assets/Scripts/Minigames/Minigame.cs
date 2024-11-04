using SibGameJam.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SibGameJam.Minigames
{
    public abstract class Minigame : MonoBehaviour
    {
        [SerializeField] private GameObject gameMenu;

        [SerializeField] private AudioSource miniGameMusic;

        public ItemInfo SelectedItem { get; private set; }

        public AudioSource Amb { get; private set;  }

        public ItemInfo FailItem { get; private set; }

        public MinigameTask Level { get; private set; }


        public void StartGame(ItemInfo selectedItem, ItemInfo failItem, MinigameTask level, AudioSource amb)
        {
            SelectedItem = selectedItem;
            FailItem = failItem;
            Amb = amb;
            Level = level;
            gameMenu.SetActive(true);
            OnGameStarted();
            amb.Pause();
            miniGameMusic.Play();
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
            miniGameMusic.Stop();
            Amb.Play();
        }

        protected abstract void OnGameFinished(bool success);
    }
}
