using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace SibGameJam.DialogSystem
{
    public class DialogFrame : MonoBehaviour
    {
        [SerializeField] private TMP_Text replicText;
        [SerializeField] private TMP_Text authorName;
        [SerializeField] private Image authorAvatar;
        [SerializeField] private AudioSource voiceOver;
        [SerializeField] private Image splash;

        private TaskCompletionSource<bool> waitButton;

        public void OnNextFrame()
        {
            waitButton?.SetResult(true);
        }

        /// <summary>
        /// Animates the text writing it asynchronously char-by-char each <see langword="40"/> / <paramref name="textSpeed"/> milliseconds.
        /// </summary>
        /// <param name="text">Text to write.</param>
        /// <param name="textSpeed">Text speed to speed up or slow down the reading.</param>
        /// <param name="token">Token to cancel an animation.</param>
        public async Task AnimationReplic(string text, float textSpeed, CancellationToken token)
        {
            for (int i = 0; i <= text.Length; i++)
            {
                token.ThrowIfCancellationRequested();
                replicText.text = text[..i];
                await Task.Delay(TimeSpan.FromMilliseconds(40) / textSpeed);
            }
            
            // Assuming each 100 characters in text should be read in 1.5 seconds.
            float textReadCoefficient = text.Length / 100f;
            // For autoplay to read after text
            await Task.Delay(TimeSpan.FromSeconds(1.5) * textReadCoefficient);
        }

        /// <summary>
        /// Shows the replic in the dialog frame.
        /// </summary>
        /// <param name="replic">Replic to show.</param>
        /// <param name="textSpeed">Reading speed coefficient for the text.</param>
        /// <param name="autoplay">Set to <see langword="true"/> if the replic should be autoplayed.</param>
        public async Task ShowText(Replic replic, float textSpeed, bool autoplay)
        {
            // Prepare the token to cancel the animation.
            CancellationTokenSource cancelPreTask = new();

            // Set image if not null
            if (replic.FrameSplash == null)
            {
                splash.enabled = false;
            }
            else
            {
                splash.enabled = true;
                splash.color = new Color(1, 1, 1, 0);
                splash.sprite = replic.FrameSplash;
                splash.CrossFadeAlpha(1, 0.3f, true);
            }
            // Fill up the frame data
            if (replic.Voice != null)
            {
                voiceOver.clip = replic.Voice;
                voiceOver.Play();
            }
            var animation = AnimationReplic(replic.Message, textSpeed, cancelPreTask.Token);
            authorName.text = replic.Author.Name;
            authorAvatar.sprite = replic.Author.Avatar;
            authorName.color = replic.Author.SignColor;

            // Wait for the button click
            waitButton = new();

            if (autoplay)
            {
                await Task.WhenAny(animation, waitButton.Task);
            }
            else
            {
                await waitButton.Task;
            }

            // Cancel incompleted animations
            cancelPreTask.Cancel();
            if (replic.Voice != null)
            {
                voiceOver.Stop();
            }
        }
    }
}