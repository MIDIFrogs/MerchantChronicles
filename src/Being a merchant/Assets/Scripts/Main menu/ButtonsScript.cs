using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SibGameJam.MainMenu
{
    public class CardScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        Camera MainCamera;
        Vector3 offset;
        public Transform DefaultParent;
        public AudioSource clickSound;
        public float animationDuration;
        void Awake()
        {
            MainCamera = Camera.allCameras[0];
        }

        public void ClickToExit()
        {
            Debug.Log("Кнопка нажата");
            clickSound.Play();
            Application.Quit();
        }

        public void ClickToStartNewGame()
        {
            Debug.Log("Кнопка нажата");
            clickSound.Play();
            SceneManager.LoadScene("Main Screen");
        }

        public void ClickToSeeAuthors()
        {
            Debug.Log("Кнопка нажата");

            clickSound.Play();
        }

        public void ClickToSettings()
        {
            Debug.Log("Кнопка нажата");

            clickSound.Play();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            StartCoroutine(fdf(0.1f));
            Debug.Log("Навелись!");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            StartCoroutine(fdf(-0.1f));
            Debug.Log("Не навелись");
        }

        public IEnumerator fdf(float c)
        {
            float timeCounter = 0;
            while (timeCounter < animationDuration)
            {
                transform.localScale += Vector3.one * c / animationDuration * Time.deltaTime;
                timeCounter += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}