using SibGameJam.Minigames;
using SibGameJam.Minigames.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SibGameJam
{
    public class ClickToStartMiniGame : MonoBehaviour
    {
        [SerializeField] private MinigameInfo minigameToStart;
        [SerializeField] private MinigameItemSelector itemSelector;

        [Header("Required to attach")]
        [SerializeField] private Transform canvas;

        public void OnClick()
        {
            var selector = Instantiate(itemSelector, canvas);
            selector.Minigame = minigameToStart;
            selector.Canvas = canvas;
        }
    }
}