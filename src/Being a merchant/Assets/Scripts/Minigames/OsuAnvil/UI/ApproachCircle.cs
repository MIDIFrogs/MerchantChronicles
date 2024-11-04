using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SibGameJam.Minigames
{
	public class ApproachCircle : MonoBehaviour
	{
		[SerializeField] Image circleImage;

		public float Speed { get; set; }
		
		public bool IsRightSide { get; set; }

		public RectTransform ParentPanel { get; set; }

		public IEnumerator Approach()
		{
			// TODO
			if (IsRightSide)
			{
				((RectTransform)transform).position = new(ParentPanel.rect.xMax, ParentPanel.rect.center.y);
			}
			else
			{
				((RectTransform)transform).position = new(ParentPanel.rect.xMin, ParentPanel.rect.center.y);
			}
			yield break;
		}
	}
}