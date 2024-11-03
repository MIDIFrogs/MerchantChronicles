using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace SibGameJam.Minigames
{
    public class NotOvercookedGame : Minigame
    {
        private readonly List<SlotCard> slots = new();
        private readonly List<StepCard> steps = new();
        private int health;
        [SerializeField] private TextMeshProUGUI healthDisplay;
        private Recipe recipe;
        private StepCard selectedStep;
        [SerializeField] private SlotCard slotCardPrefab;
        [SerializeField] private GameObject slotsPanel;
        [SerializeField] private StepCard stepCardPrefab;
        [SerializeField] private GameObject stepsPanel;

        public int Health
        {
            get => health;
            set
            {
                if (health != value)
                {
                    health = value;
                    OnHealthUpdated();
                }
            }
        }

        public void OnSlotClicked(object sender, System.EventArgs e)
        {
            var slot = (SlotCard)sender;
            if (selectedStep != null)
            {
                slot.UpdateCard(selectedStep);
                CheckSlot(slot);
            }
        }

        public void OnStepClicked(object card, System.EventArgs e)
        {
            selectedStep = (StepCard)card;
        }

        protected override void OnGameFinished(bool success)
        {
            throw new System.NotImplementedException();
        }

        protected override void OnGameStarted()
        {
            recipe = (Recipe)Level;
            foreach (var step in recipe.RecipeSteps.OrderBy(_ => Random.value))
            {
                var card = Instantiate(stepCardPrefab, stepsPanel.transform);
                card.Step = step;
                card.StartTimer();
                card.TimeOut += Card_TimeOut;
                card.CardClicked += OnStepClicked;
                steps.Add(card);
            }
            for (int i = 0; i < recipe.RecipeSteps.Count; i++)
            {
                var slot = Instantiate(slotCardPrefab, slotsPanel.transform);
                slot.Slot = new();
                slot.SlotClicked += OnSlotClicked;
                slots.Add(slot);
            }
            Health = 3;
        }

        private void Card_TimeOut(object sender, System.EventArgs e)
        {
            Health--;
        }

        private void CheckSlot(SlotCard slot)
        {
            int index = slots.IndexOf(slot);
            if (recipe.RecipeSteps[index] != slot.Slot.Step)
            {
                Health--;
            }
            if (Health > 0 && slots.All(x => x.Slot.Step != null))
            {
                EndGame(true);
            }
        }

        private void OnHealthUpdated()
        {
            healthDisplay.text = Health.ToString();
            if (health <= 0)
            {
                EndGame(false);
            }
        }
    }
}