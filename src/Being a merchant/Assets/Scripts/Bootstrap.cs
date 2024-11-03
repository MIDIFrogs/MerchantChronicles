using SibGameJam.Inventory;
using UnityEngine;

namespace SibGameJam
{
	public class Bootstrap : MonoBehaviour
	{
        [SerializeField] private PlayerInventory inventory;

        private void Start()
        {
            PlayerStats.Session.Inventory = inventory;
        }
    }
}