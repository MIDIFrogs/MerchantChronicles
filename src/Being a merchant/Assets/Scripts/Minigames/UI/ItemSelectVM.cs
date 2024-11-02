using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SibGameJam.Minigames.UI
{
	public class ItemSelectVM : MonoBehaviour
	{
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private Image selectTint;

		public MinigameSelectableItem Selectable { get; set; }

        public MinigameItemSelector Selector { get; set; }

        private void Start()
        {
            icon.sprite = Selectable.RequestedItem.Icon;
            title.text = Selectable.RequestedItem.Title;
        }

        public void OnSelectionChanged(bool selected)
        {
            selectTint.gameObject.SetActive(selected);
        }

        public void OnClick()
        {
            Selector.OnItemSelected(this);
        }
    }
}