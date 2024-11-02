using System;

namespace SibGameJam.Inventory.CustomEventArgs
{
    public class InventoryEventArgs : EventArgs
    {
        public ItemInfo ItemInfo { get; private set; }

        public InventoryEventArgs(ItemInfo itemInfo)
        {
            ItemInfo = itemInfo;
        }
    }
}