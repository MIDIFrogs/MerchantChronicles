using UnityEngine;

namespace SibGameJam.Minigames
{
    [CreateAssetMenu(fileName = "New Code lock Task", menuName = "Minigames/Code lock task")]
	public class CodeLockTask : MinigameTask
    {
		[SerializeField] private string answer;
		[SerializeField] private Sprite question;

		public string Answer => answer;
		public Sprite Question => question;
	}
}