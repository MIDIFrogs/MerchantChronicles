using SibGameJam.Inventory;
using UnityEngine;

namespace SibGameJam
{
	public static class PlayerStats
	{
		public static int Reputation
        {
            get => PlayerPrefs.GetInt(nameof(Reputation));
            set
            {
                PlayerPrefs.SetInt(nameof(Reputation), value);
                PlayerPrefs.Save();
            }
        }

        public static PlayerInventory Inventory { get; set; }
	}
}