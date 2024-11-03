using SibGameJam.DialogSystem;
using SibGameJam.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SibGameJam.Minigames.UI
{
    public class ItemSellVM : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private Image selectTint;

        public ItemInfo Item { get; set; }

        public ItemSellDialog SellDialog { get; set; }

        private void Start()
        {
            icon.sprite = Item.Icon;
            title.text = Item.Title;
        }

        public void OnSelectionChanged(bool selected)
        {
            selectTint.gameObject.SetActive(selected);
        }

        public void OnClick()
        {
            SellDialog.OnItemSelected(this);
        }
    }
}