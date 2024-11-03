using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SibGameJam.Minigames
{
	public class StepCard : MonoBehaviour
	{
		[SerializeField] private Image timeIndicator;
		[SerializeField] private TextMeshProUGUI titleField;
		[SerializeField] private Image icon;

		bool timerRunning;

		public RecipeStep Step { get; set; }

		public float TimeRemains { get; set; }

		public event EventHandler TimeOut;
		public event EventHandler CardClicked;

        private void Start()
        {
			TimeRemains = Step.Duration;
			titleField.text = Step.StepName;
			icon.sprite = Step.Icon;
        }

        public void StartTimer()
		{
			timerRunning = true;
		}

		public void StopTimer()
		{
			timerRunning = false;
		}

		public void OnClick()
		{
			CardClicked?.Invoke(this, EventArgs.Empty);
		}

        private void FixedUpdate()
        {
            if (timerRunning)
			{
				TimeRemains -= Time.fixedDeltaTime;
				if (TimeRemains < 0)
				{
					TimeOut?.Invoke(this, EventArgs.Empty);
					StopTimer();
				}
				timeIndicator.fillAmount = TimeRemains / Step.Duration;
			}
        }
    }
}