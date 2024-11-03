using System;
using UnityEngine;

namespace SibGameJam.Minigames
{
	public class SlotCard : MonoBehaviour
	{
		private StepCard card;

		public RecipeSlot Slot { get; set; }

		public event EventHandler SlotClicked;

        public void UpdateCard(StepCard newCard)
		{
            card?.StartTimer();
			Slot.Step = newCard.Step;
			newCard.transform.SetParent(transform);
			newCard.transform.localPosition = Vector3.zero;
			newCard.StopTimer();
		}

		public void OnClick()
		{
			SlotClicked?.Invoke(this, EventArgs.Empty);
		}
    }
}