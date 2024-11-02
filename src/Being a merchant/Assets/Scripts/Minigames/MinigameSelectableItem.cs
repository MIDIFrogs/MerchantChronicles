using SibGameJam.Inventory;
using System;
using UnityEngine;

namespace SibGameJam.Minigames
{
	[Serializable]
	public struct MinigameSelectableItem
	{
		[SerializeField] private ItemInfo requestedItem;
		[SerializeField] private ItemInfo failItem;

		public readonly ItemInfo RequestedItem => requestedItem;
		public readonly ItemInfo FailItem => failItem;
	}
}