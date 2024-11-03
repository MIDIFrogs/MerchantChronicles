using System.Collections.Generic;
using UnityEngine;

namespace SibGameJam.Minigames
{
    [CreateAssetMenu(fileName = "New cooking recipe", menuName = "Minigames/Cooking recipe")]
    public class Recipe : MinigameTask
	{
		[SerializeField] private List<RecipeStep> steps;

		public IReadOnlyList<RecipeStep> RecipeSteps => steps;
	}
}