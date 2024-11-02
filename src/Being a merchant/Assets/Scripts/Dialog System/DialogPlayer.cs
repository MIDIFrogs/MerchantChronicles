using AloneInInquisition.MenuSystem.PauseMenu;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace MerchantChronicles.DialogSystem
{
    public class DialogPlayer : MonoBehaviour
    {
        [SerializeField] private PauseMenu pauseMenu;

        [Header("Dialog properties")]
        [SerializeField] private float textSpeed;
        [SerializeField] private bool autoplay;
        [SerializeField] private DialogFrame dialogFrame;

        public float TextSpeed => textSpeed;

        public bool Autoplay => autoplay;

        /// <summary>
        /// Starts a dialog and waits for the completion.
        /// </summary>
        /// <param name="dialog">Dialog to read.</param>
        public async Task StartDialogAsync(Dialog dialog)
        {
            pauseMenu.PauseGame = true;
            dialogFrame.gameObject.SetActive(true);

            foreach(var replic in dialog.DialogContent)
                await dialogFrame.ShowText(replic, TextSpeed, Autoplay);

            dialogFrame.gameObject.SetActive(false);
            pauseMenu.PauseGame = false;
        }
    }
}
