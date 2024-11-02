using UnityEngine;

namespace SibGameJam.Inventory.UI
{
	public class InventoryVM : MonoBehaviour
	{
		[SerializeField] private PlayerInventory inventory;
		[SerializeField] private InventoryCellVM cellPrefab;

        private void Start()
        {
            foreach (var slot in inventory.Slots)
            {
                var slotVM = Instantiate(cellPrefab, transform);
                slotVM.Slot = slot;
            }
        }
    }
}