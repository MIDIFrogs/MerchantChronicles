using System;

namespace SibGameJam.Minigames
{
	public class RecipeSlot
	{
        private RecipeStep step;

        public RecipeStep Step
        {
            get => step;
            set
            {
                if (step != value)
                {
                    step = value;
                    StepUpdated?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler StepUpdated;
	}
}