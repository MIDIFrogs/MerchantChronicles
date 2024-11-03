using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SibGameJam.Minigames
{
    public class CodeLockGame : Minigame
    {
        private static readonly Regex textFilter = new Regex("[^a-zA-Z0-9]", RegexOptions.Compiled);

        [SerializeField] private Image questionImage;
        [SerializeField] private TextMeshProUGUI answerField;
        [SerializeField] private TextMeshProUGUI attemptsText;

        int attempts = 3;

        private CodeLockTask level;

        protected override void OnGameFinished(bool success)
        {
            Debug.Log($"Code lock completed: {success}");
            Destroy(gameObject);
        }

        protected override void OnGameStarted()
        {
            level = (CodeLockTask)Level;
            questionImage.sprite = level.Question;
            attemptsText.text = attempts.ToString();
        }

        public void OnCheckClick()
        {
            Debug.Log($"Text to check: {answerField.text}. Answer: {level.Answer}");
            string trueText = textFilter.Replace(answerField.text, "");
            if (trueText != level.Answer)
            {
                Debug.Log("They're not equal.");
                attempts--;
                attemptsText.text = attempts.ToString();
                if (attempts == 0)
                {
                    EndGame(false);
                }
            }
            else
            {
                EndGame(true);
            }
        }
    }
}