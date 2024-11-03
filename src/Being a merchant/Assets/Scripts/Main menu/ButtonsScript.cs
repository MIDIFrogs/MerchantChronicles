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
            Debug.Log("������ ������");
            clickSound.Play();
            Application.Quit();
        }

        public void ClickToStartNewGame()
        {
            Debug.Log("������ ������");
            clickSound.Play();
            SceneManager.LoadScene("Main Screen");
        }

        public void ClickToSeeAuthors()
        {
            Debug.Log("������ ������");

            clickSound.Play();
        }

        public void ClickToSettings()
        {
            Debug.Log("������ ������");

            clickSound.Play();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            StartCoroutine(fdf(0.1f));
            Debug.Log("��������!");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            StartCoroutine(fdf(-0.1f));
            Debug.Log("�� ��������");
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