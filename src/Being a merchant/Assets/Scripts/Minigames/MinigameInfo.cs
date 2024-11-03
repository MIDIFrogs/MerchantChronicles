using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SibGameJam.Minigames
{
    [CreateAssetMenu(fileName = "New Minigame Info", menuName = "Minigames/Minigame definition")]
    public class MinigameInfo : ScriptableObject
    {
        [SerializeField] private Minigame minigamePrefab;
        [SerializeField] private List<MinigameSelectableItem> selectableItems;
        [SerializeField] private MinigameTask level;

        public Minigame MinigamePrefab => minigamePrefab;

        public IReadOnlyList<MinigameSelectableItem> SelectableItems => selectableItems;

        public MinigameTask Level => level;
    }
}