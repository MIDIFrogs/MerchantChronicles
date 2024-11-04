using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

namespace SibGameJam.MainMenu
{
    public class CardScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        Camera MainCamera;
        Vector3 offset;
        public Transform DefaultParent;
        public AudioSource clickSound;
        public float animationDuration;
        public Button btn;
        public GameObject settings; 

        void Awake()
        {
            MainCamera = Camera.allCameras[0];
            btn.interactable = GameSession.SessionExists;                        
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
            PlayerStats.Session = new GameSession();
            SceneManager.LoadScene("Main Screen");
        }

        public void ClickToStartGame()
        {
            Debug.Log("������ ������");
            clickSound.Play();
            PlayerStats.Session = GameSession.LoadOrCreate();
            SceneManager.LoadScene("Main Screen");
        }

        public void ClickToSeeAuthors()
        {
            Debug.Log("������ ������");
            SceneManager.LoadScene("Credits");
            clickSound.Play();
        }

        public void ClickToSettings()
        {
            Debug.Log("������ ������");
            clickSound.Play();
            settings.SetActive(true);
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