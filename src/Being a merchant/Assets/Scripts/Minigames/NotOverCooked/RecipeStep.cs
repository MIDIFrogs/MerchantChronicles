using UnityEngine;

namespace SibGameJam.Minigames
{
	[CreateAssetMenu(fileName = "New recipe step", menuName = "Minigames/Recipe step")]
	public class RecipeStep : ScriptableObject
	{
		[SerializeField] private string stepName;
		[SerializeField] [Range(0, 40)] private float duration;
		[SerializeField] private Sprite icon;

		public string StepName => stepName;
		public float Duration => duration;
		public Sprite Icon => icon;
	}
}